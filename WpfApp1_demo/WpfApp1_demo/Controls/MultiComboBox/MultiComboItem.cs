/********************************************************************
 *
 *  PROPRIETARY and CONFIDENTIAL
 *
 *  This file is licensed from, and is a trade secret of:
 *
 *                   AvePoint, Inc.
 *                   Harborside Financial Center
 *                   9th Fl.   Plaza Ten
 *                   Jersey City, NJ 07311
 *                   United States of America
 *                   Telephone: +1-800-661-6588
 *                   WWW: www.avepoint.com
 *
 *  Refer to your License Agreement for restrictions on use,
 *  duplication, or disclosure.
 *
 *  RESTRICTED RIGHTS LEGEND
 *
 *  Use, duplication, or disclosure by the Government is
 *  subject to restrictions as set forth in subdivision
 *  (c)(1)(ii) of the Rights in Technical Data and Computer
 *  Software clause at DFARS 252.227-7013 (Oct. 1988) and
 *  FAR 52.227-19 (C) (June 1987).
 *
 *  Copyright © 2001-2014 AvePoint® Inc. All Rights Reserved. 
 *
 *  Unpublished - All rights reserved under the copyright laws of the United States.
 *  $Revision:  $
 *  $Author:  $        
 *  $Date:  $
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Automation;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// 为了和AUIMulitiComboBox想对应，AUIMultiComboItem继承自ComboBoxItem。
    /// 同时重写了IsSelected属性，目的是重写属性改变事件。
    /// </summary>
    public class MultiComboItem : ComboBoxItem
    {
        internal MultiComboBox ParentComboBox;
        internal CheckBox SelectedCheckBox;

        public MultiComboItem()
        {
            DefaultStyleKey = typeof(MultiComboItem);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SelectedCheckBox = this.GetTemplateChild("SelectedCheckBox") as CheckBox;
            if (SelectedCheckBox != null)
            {
                SelectedCheckBox.IsChecked = this.IsSelected;
                SelectedCheckBox.Click -= new RoutedEventHandler(SelectedCheckBox_Click);
                SelectedCheckBox.Click += new RoutedEventHandler(SelectedCheckBox_Click);
            }
        }

        #region public bool IsSelected
        /// <summary>
        /// Gets or sets a value indicating whether the AUIMultiComboItem is selected.
        /// 重写ComboBoxItem的IsSelected属性。
        /// </summary>
        public new bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Identifies the IsSelected dependency property.
        /// </summary>
        public new static readonly DependencyProperty IsSelectedProperty =
                DependencyProperty.Register(
                        "IsSelected",
                        typeof(bool),
                        typeof(MultiComboItem),
                        new PropertyMetadata(OnIsSelectedPropertyChanged));

        /// <summary>
        /// SelectedProperty PropertyChangedCallback static function.
        /// </summary>
        static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiComboItem ctrl = (MultiComboItem)d;
            bool newValue = (bool)e.NewValue;
            if (ctrl.SelectedCheckBox != null)
            {
                ctrl.SelectedCheckBox.IsChecked = newValue;
            }
            if (ctrl.ParentComboBox != null && ctrl._isSelectedNestedLevel == 0)
            {
                //if (!Object.ReferenceEquals(FocusManager.GetFocusedElement(ctrl), ctrl) && ctrl.ForSelectAllVisibility == Visibility.Collapsed)
                //{
                //    ctrl.Focus();
                //}
                ctrl.ParentComboBox.OnItemIsSelectedPropertyChanged(ctrl); //_isSelectedNestedLevel保证不会循环触发IsChecked事件
            }
        }

        private ToggleState ConvertToToggleState(bool? value)
        {
            bool valueOrDefault = value.GetValueOrDefault();
            if (value.HasValue)
            {
                switch (valueOrDefault)
                {
                    case false:
                        return ToggleState.Off;

                    case true:
                        return ToggleState.On;
                }
            }
            return ToggleState.Indeterminate;
        }

        /// <summary>
        /// Nested level for IsSelectedCoercion.
        /// </summary>
        internal int _isSelectedNestedLevel;

        private void SelectedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            this.Focus();
            SetValue(IsSelectedProperty, box.IsChecked.Value);
            if (Clicked != null)
            {
                Clicked(sender, e);
            }
        }
        #endregion public bool IsSelected

        /// <summary>
        /// 为SelectAll 的Item准备的叠加checkbox样式
        /// </summary>
        public Visibility ForSelectAllVisibility
        {
            get { return (Visibility)GetValue(ForSelectAllVisibilityProperty); }
            set { SetValue(ForSelectAllVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ForSelectAllVisibilityProperty =
            DependencyProperty.Register("ForSelectAllVisibility", typeof(Visibility), typeof(MultiComboItem), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// 提供给外界的点击事件的接口。
        /// </summary>
        public event RoutedEventHandler Clicked;

        #region == Focus State ==

        internal void ChangeVisualState()
        {
            this.ChangeVisualState(true);
        }

        internal void ChangeVisualState(bool useTransitions)
        {
            if (this.ParentComboBox != null)
            {
                if (!base.IsEnabled)
                {
                    VisualStateManager.GoToState(this, (base.Content is Control) ? "Normal" : "Disabled", useTransitions);
                }
                else if (this.IsMouseOver)
                {
                    VisualStateManager.GoToState(this, "MouseOver", useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Normal", useTransitions);
                }
                if (this.IsFocused)
                {
                    VisualStateManager.GoToState(this, "Focused", useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, "Unfocused", useTransitions);
                }
            }
        }

        private void FocusChanged(bool haveFocus)
        {
            this.IsFocused = haveFocus;
            this.ChangeVisualState();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            bool haveFocus = this.HasFocus();
            this.FocusChanged(haveFocus);
            if ((haveFocus && object.ReferenceEquals(this, e.OriginalSource)) && (this.ParentComboBox != null))
            {
                this.ParentComboBox.ItemFocused(this);
            }
        }

        private bool HasFocus()
        {
            return Object.ReferenceEquals(this, FocusManager.GetFocusedElement(this));
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            this.FocusChanged(this.HasFocus());
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.IsMouseOver = true;
            this.ChangeVisualState();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.IsMouseOver = false;
            this.ChangeVisualState();
        }

        //重写此方法是为了防止点击Item的时候使下拉菜单关闭的问题
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.ParentComboBox.ItemFocused(this);
            e.Handled = true;
        }

        internal bool IsFocused { get; set; }

        internal bool IsMouseOver { get; set; }

        #endregion == Focus State ==
    }
}
