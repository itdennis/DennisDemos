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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MigratorTool.WPF.I18N;

namespace AvePoint.Migrator.Common.Controls
{
    public class AUIComboBox : ComboBox
    {
        ContentControl AdditionalElementContentControl;
        HyperlinkButton NoneHB;
        private FrameworkElement ElementPopupChild;

        static AUIComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AUIComboBox), new FrameworkPropertyMetadata(typeof(AUIComboBox)));
        }
        public AUIComboBox()
        {
            this.DropDownOpened += new EventHandler(AUIComboBox_DropDownOpened);
            this.DropDownClosed += new EventHandler(AUIComboBox_DropDownClosed);
            this.SelectionChanged += new SelectionChangedEventHandler(AUIComboBox_SelectionChanged); 
        }

        void AUIComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AUIComboBox source = sender as AUIComboBox;
            source.NoSelectionTextVisibility = source.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed; 
        }

        void AUIComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.NoSelectionText = this.MustSelectOne ?
                I18NEntity.Get("Common_eef1eceb_9f10_4437_ae73_2a6e3ad86fab", "Select One") : string.Empty;
        }

        void AUIComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.NoSelectionText = string.Empty;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AdditionalElementContentControl = GetTemplateChild("AdditionalElementContentControl") as ContentControl;
            NoneHB = GetTemplateChild("NoneHB") as HyperlinkButton;
            if (NoneHB != null)
            {
                NoneHB.Click -= new RoutedEventHandler(NoneHB_Click);
                NoneHB.Click += new RoutedEventHandler(NoneHB_Click);
            }
            if (AdditionalElementContentControl != null)
            {
                AdditionalElementContentControl.RemoveHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(AdditionalElementContentControl_MouseLeftButtonUp));
                AdditionalElementContentControl.AddHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(AdditionalElementContentControl_MouseLeftButtonUp), true);
            }
            this.ElementPopupChild = base.GetTemplateChild("PopupBorder") as FrameworkElement;
        }

        void NoneHB_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedIndex = -1;
            this.IsDropDownOpen = false;
        }

        void AdditionalElementContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.AutoHidePopupWhenAdditionalElementClicked)
            {
                this.IsDropDownOpen = false;
            }
        }

        #region AdditionalElements

        public FrameworkElement AdditionalElement
        {
            get { return (FrameworkElement)GetValue(AdditionalElementProperty); }
            set { SetValue(AdditionalElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdditionalElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdditionalElementProperty =
            DependencyProperty.Register("AdditionalElement", typeof(FrameworkElement), typeof(AUIComboBox), new PropertyMetadata(null));

        /// <summary>
        /// 控制蓝色背景区域的显示隐藏  -by hbdou
        /// </summary>
        public Visibility AdditionSpaceVisibility
        {
            get { return (Visibility)GetValue(AdditionSpaceVisibilityProperty); }
            set { SetValue(AdditionSpaceVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdditionSpaceVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdditionSpaceVisibilityProperty =
            DependencyProperty.Register("AdditionSpaceVisibility", typeof(Visibility), typeof(AUIComboBox), new PropertyMetadata(Visibility.Visible));

        #endregion AdditionalElements

        #region AutoHidePopupWhenAdditionalElementClicked

        public bool AutoHidePopupWhenAdditionalElementClicked
        {
            get { return (bool)GetValue(AutoHidePopupWhenAdditionalElementClickedProperty); }
            set { SetValue(AutoHidePopupWhenAdditionalElementClickedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoHidePopupWhenAdditionalElementClicked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoHidePopupWhenAdditionalElementClickedProperty =
            DependencyProperty.Register("AutoHidePopupWhenAdditionalElementClicked", typeof(bool), typeof(AUIComboBox), new PropertyMetadata(true));

        #endregion AutoHidePopupWhenAdditionalElementClicked

        #region MustSelectOne

        public bool MustSelectOne
        {
            get { return (bool)GetValue(MustSelectOneProperty); }
            set { SetValue(MustSelectOneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MustSelectOne.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MustSelectOneProperty =
            DependencyProperty.Register("MustSelectOne", typeof(bool), typeof(AUIComboBox), new PropertyMetadata(false, OnMustSelectOneChanged));

        private static void OnMustSelectOneChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AUIComboBox source = o as AUIComboBox;
            source.NoneElementVisibility = source.MustSelectOne ? Visibility.Collapsed : Visibility.Visible;
            source.NoSelectionText = source.MustSelectOne ? I18NEntity.Get("Common.GuiControls", "Select One") : ""; 

        }

        public Visibility NoneElementVisibility
        {
            get { return (Visibility)GetValue(NoneElementVisibilityProperty); }
            set { SetValue(NoneElementVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoneElementVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoneElementVisibilityProperty =
            DependencyProperty.Register("NoneElementVisibility", typeof(Visibility), typeof(AUIComboBox), new PropertyMetadata(Visibility.Visible));

        #endregion MustSelectOne

        #region NoSelectionText

        public Visibility NoSelectionTextVisibility
        {
            get { return (Visibility)GetValue(NoSelectionTextVisibilityProperty); }
            set { SetValue(NoSelectionTextVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoSelectionTextVisibility.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty NoSelectionTextVisibilityProperty =
            DependencyProperty.Register("NoSelectionTextVisibility", typeof(Visibility), typeof(AUIComboBox), new PropertyMetadata(Visibility.Visible));

        public string NoSelectionText
        {
            get { return (string)GetValue(NoSelectionTextProperty); }
            set { SetValue(NoSelectionTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoSelectionText.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty NoSelectionTextProperty =
            DependencyProperty.Register("NoSelectionText", typeof(string), typeof(AUIComboBox), new PropertyMetadata(""));

        #endregion NoSelectionText






        protected override void OnDropDownOpened(EventArgs e)
        {
            if (ElementPopupChild != null)
            {
                this.ElementPopupChild.MaxHeight = this.MaxDropDownHeight; ;
            }
            base.OnDropDownOpened(e);
            if (this.AdditionalElementContentControl != null && this.SelectedItem == null)
            {
                this.AdditionalElementContentControl.Focus();
            }
        }
    }
}