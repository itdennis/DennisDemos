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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MigratorTool.WPF.I18N;
using MigratorTool.WPF.ViewModel.Common;
using System.Diagnostics.CodeAnalysis;
using MigratorTool.WPF.View.Common.MigratorLogger;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// 为了能达到如ComboBox一样的效果, AUIMultiComboBox集成自ComboBox。
    /// 但是废弃了来自ComboBox的许多属性, 如SelectedIndex, SelectedItem等。
    /// AUIMultiComboBox有用的属性不多, 可用的有: IsSelectAll(只读属性), SelectAllText, DisplayStringFormat, SelectedItems。
    /// 此外, 还有SelectAll和UnSelectAll两个方法供调用, 并提供SelectedItemsChanged(事件)。
    /// </summary>
    public class MultiComboBox : ComboBox
    {
        private static Logger logger = Logger.CreateInstance();
        private ContentPresenter ElementContentPresenter; //用于重写ToggleButton的Content显示逻辑。
        private MultiComboItem SelectAllComboBoxItem; //用于选中所有的Item虚子项。
        private ButtonBase OKButton; //OK按钮
        private ButtonBase CancelButton;//Cancel按钮
        private ContentControl AdditionalElementContentControl;
        private FrameworkElement ElementPopupChild;

        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public MultiComboBox()
        {
            //DefaultStyleKey = typeof(MultiComboBox);

            ObservableCollection<object> items = new ObservableCollection<object>();
            items.CollectionChanged += OnSelectedItemsCollectionChanged;
            SelectedItems = items; //SelectedItems被限定为ObservableCollection<object>类型。
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ElementContentPresenter = this.GetTemplateChild("ContentSite") as ContentPresenter;
            if (ElementContentPresenter != null)
            {
                SetContentPresenterBySelectedItems();
            }
            SelectAllComboBoxItem = this.GetTemplateChild("SelectAllComboBoxItem") as MultiComboItem;
            if (SelectAllComboBoxItem != null)
            {
                SelectAllComboBoxItem.ParentComboBox = this;

                SelectAllComboBoxItem.Clicked -= SelectAllCheckBox_Click;
                SelectAllComboBoxItem.Clicked += SelectAllCheckBox_Click;
            }
            OKButton = this.GetTemplateChild("OKButton") as ButtonBase;
            CancelButton = this.GetTemplateChild("CancelButton") as ButtonBase;
            if (this.OKButton != null)
            {
                this.OKButton.Click -= OKButton_Click;
                this.OKButton.Click += OKButton_Click;
            }
            if (this.CancelButton != null)
            {
                this.CancelButton.Click -= CancelButton_Click;
                this.CancelButton.Click += CancelButton_Click;
            }
            AdditionalElementContentControl = GetTemplateChild("AdditionalElementContentControl") as ContentControl;
            if (AdditionalElementContentControl != null)
            {
                AdditionalElementContentControl.RemoveHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(AdditionalElementContentControl_MouseLeftButtonUp));
                AdditionalElementContentControl.AddHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(AdditionalElementContentControl_MouseLeftButtonUp), true);
            }
            ElementPopupChild = GetTemplateChild("DropDownBorder") as FrameworkElement;

            this.SelectAllText = I18NEntity.Get("Common_dcbb34f5_ca41_41eb_9f10_8585a33ceb4e", "Select All");
        }

        void AdditionalElementContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.IsDropDownOpen = false;
        }

        #region Internal Event Handler

        //SelectAll CheckBox的点击事件处理函数
        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            bool newValue = box.IsChecked.Value;
            if (this.OKButton != null)
            {
                if (IsAtLeastSelectOne)
                {
                    this.OKButton.IsEnabled = (Items.Count == 0) ? false : newValue;
                }
                else
                {
                    this.OKButton.IsEnabled = true;
                }
            }
            foreach (object item in Items)
            {
                MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                if (container != null)
                {
                    container._isSelectedNestedLevel++; //_isSelectedNestedLevel保证不会循环触发IsChecked事件
                    container.IsSelected = newValue;
                    container._isSelectedNestedLevel--;
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //收集当前选中的子项信息。
            IList currentSelectedItems = new ObservableCollection<object>();
            foreach (object item in Items)
            {
                MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                if (container.IsSelected)
                {
                    currentSelectedItems.Add(item);
                }
            }
            //判断是否该触发SelectedItemsChanged事件。
            bool isSelectedItemsChanged = IsSelectedItemsChanged(SelectedItems, currentSelectedItems);
            if (isSelectedItemsChanged)
            {
                SelectedItems.Clear();
                foreach (var item in currentSelectedItems)
                {
                    SelectedItems.Add(item);
                }
            }
            //_allowSelectedItemsChanged = true;
            this.IsDropDownOpen = false;
            if (isSelectedItemsChanged)
            {
                RaiseSelectedItemsChanged();
            }
            if (ButtonClick != null)
            {
                IsOKClicked = true;
                ButtonClick(this, e);
            }
        }

        /// <summary>
        /// 判断两个集合中的子项是否一致。
        /// </summary>
        /// <param name="oldItems"></param>
        /// <param name="newItems"></param>
        /// <returns>顺序和元素完全一致返回True,否则false</returns>
        private bool IsSelectedItemsChanged(IList oldItems, IList newItems)
        {
            if (oldItems.Count != newItems.Count)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < newItems.Count; i++)
                {
                    if (!object.ReferenceEquals(newItems[i], oldItems[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _allowSelectedItemsChanged = false;

        /// <summary>
        /// 将SelectedItems对应项的选中状态同步。
        /// </summary>
        private void RevertSelectedItemsState()
        {
            foreach (object item in Items)
            {
                MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                if (container != null)
                {
                    bool isSelected = SelectedItems.Contains(item);
                    container._isSelectedNestedLevel++;
                    container.IsSelected = isSelected;
                    container._isSelectedNestedLevel--;
                }
            }
            SelectAllComboBoxItem._isSelectedNestedLevel++;
            SelectAllComboBoxItem.IsSelected = IsSelectAll;
            SelectAllComboBoxItem._isSelectedNestedLevel--;
        }
        #endregion

        #region public string DisplayStringFormat

        /// <summary>
        /// ToggleButton中显示的字符串格式，
        /// 默认是"Select {0} Items"。
        /// </summary>
        public string DisplayStringFormat
        {
            get { return (string)GetValue(DisplayStringFormatProperty); }
            set { SetValue(DisplayStringFormatProperty, value); }
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty DisplayStringFormatProperty =
            DependencyProperty.Register(
                "DisplayStringFormat",
                typeof(string),
                typeof(MultiComboBox),
                new PropertyMetadata(I18NEntity.Get("Common_4ad9cbc3_851c_4332_b011_d4061f6ce31e", "Select {0} Items")));

        /// <summary>
        /// 根据选中的Item个数更新ToggleButton的显示内容。
        /// </summary>
        internal void SetContentPresenterBySelectedItems()
        {
            string formatString = null;
            NoSelectionTextVisibility = Visibility.Collapsed;
            if (SelectedItems.Count == 0)
            {
                if (IsAtLeastSelectOne)
                {
                    formatString = string.Empty;
                    if (!this.IsDropDownOpen)
                    {
                        NoSelectionTextVisibility = Visibility.Visible;
                        AutomationProperties.SetHelpText(this, SelectionText);
                    }
                }
                else
                {
                    formatString = SelectorPrompt;
                }

            }
            else if (SelectedItems.Count == Items.Count)
            {
                formatString = this.SelectAllText;
            }
            else if (SelectedItems.Count == 1)
            {
                formatString = GetDefaultDisplayName(SelectedItems[0]);
            }
            else
            {
                try
                {
                    formatString = string.Format(DisplayStringFormat, SelectedItems.Count.ToString());
                }
                catch (Exception ex) //格式化出错Log信息
                {
                    logger.Log(LogLevel.Debug, "An error occurred while set content presenter . Reason:{0}", ex.ToString());
                    formatString = string.Empty;
                }
            }
            ElementContentPresenter.Content = formatString;
            if (!string.IsNullOrEmpty(formatString))
            {
                AutomationProperties.SetHelpText(this, formatString);
            }
        }

        private string GetDefaultDisplayName(object item)
        {
            if (string.IsNullOrEmpty(DisplayMemberPath))
            {
                return item.ToString();
            }
            else
            {
                return GetValueFromPath(item, DisplayMemberPath).ToString();
            }
        }

        private object GetValueFromPath(object item, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            else
            {
                string[] pathArr = path.Split('.');
                object lastvalue = item.GetType().GetProperty(pathArr[0]).GetValue(item, null);
                for (int i = 1; i < pathArr.Count(); i++)
                {
                    lastvalue = lastvalue.GetType().GetProperty(pathArr[i]).GetValue(lastvalue, null);
                    if (lastvalue == null)
                    {
                        return string.Empty;
                    }
                }
                return lastvalue;
            }
        }

        #endregion

        #region public string SelectAllText

        /// <summary>
        /// 定义选中所有子项的复选框的提示内容，
        /// 默认为"Select All"。
        /// </summary>
        public string SelectAllText
        {
            get { return (string)GetValue(SelectAllTextProperty); }
            set { SetValue(SelectAllTextProperty, value); }
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty SelectAllTextProperty =
            DependencyProperty.Register(
                "SelectAllText",
                typeof(string),
                typeof(MultiComboBox),
                new PropertyMetadata(null));

        #endregion

        #region public string SelectorPrompt

        /// <summary>
        /// 可一项也不选的时候，什么也不选中的情况下默认给用户的提示语。
        /// </summary>
        public string SelectorPrompt
        {
            get { return (string)GetValue(SelectorPromptProperty); }
            set { SetValue(SelectorPromptProperty, value); }
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        protected static readonly DependencyProperty SelectorPromptProperty =
            DependencyProperty.Register("SelectorPrompt", typeof(string), typeof(MultiComboBox), new PropertyMetadata(I18NEntity.Get("Common_dcbb34f5_ca41_41eb_9f10_8585a33ceb4e", "None")));

        #endregion public string SelectAllText

        #region public string NoSelectionText

        /// <summary>
        /// 至少选一项都时，什么也不选中的情况下默认给用户的提示语，水印形式
        /// </summary>
        public string SelectionText
        {
            get { return (string)GetValue(SelectionTextProperty); }
            set { SetValue(SelectionTextProperty, value); }
        }

        public static readonly DependencyProperty SelectionTextProperty =
            DependencyProperty.Register("SelectionText", typeof(string), typeof(MultiComboBox), new PropertyMetadata(I18NEntity.Get("Common_d311146e-450a-423b-9be6-5454ea174741", "Select Value(s)")));

        #endregion public string SelectAllText

        #region public string NoSelectionTextVisibility

        /// <summary>
        /// 至少选一项时，水印形式的显示
        /// </summary>
        public Visibility NoSelectionTextVisibility
        {
            get { return (Visibility)GetValue(NoSelectionTextVisibilityProperty); }
            set { SetValue(NoSelectionTextVisibilityProperty, value); }
        }

        protected static readonly DependencyProperty NoSelectionTextVisibilityProperty =
            DependencyProperty.Register("NoSelectionTextVisibility", typeof(Visibility), typeof(MultiComboBox), new PropertyMetadata(Visibility.Visible));

        #endregion public string SelectAllText

        #region public bool IsSelectAll

        /// <summary>
        /// 只读属性，获取所有子项的复选框是否都选中。
        /// </summary>
        public bool IsSelectAll
        {
            get { return (bool)GetValue(IsSelectAllProperty); }
            protected set { SetValue(IsSelectAllProperty, value); }
        }

        public static readonly DependencyProperty IsSelectAllProperty =
            DependencyProperty.Register(
                "IsSelectAll",
                typeof(bool),
                typeof(MultiComboBox),
                new PropertyMetadata(false, OnIsSelectAllPropertyChanged));

        private static void OnIsSelectAllPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiComboBox accordion = (MultiComboBox)d;
            bool newValue = (bool)e.NewValue;

            MultiComboItem selectAllContainer = accordion.SelectAllComboBoxItem;
            if (selectAllContainer != null)
            {
                selectAllContainer._isSelectedNestedLevel++;
                selectAllContainer.IsSelected = newValue;
                selectAllContainer._isSelectedNestedLevel--;
            }
        }

        private void CheckAll(bool isChecked)
        {
            IList oldSelectedItems = GetSelectedItemsCopy();
            foreach (object item in Items)
            {
                MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                if (container != null)
                {
                    container._isSelectedNestedLevel++; //_isSelectedNestedLevel保证不会循环触发IsChecked事件
                    container.IsSelected = isChecked;
                    container._isSelectedNestedLevel--;
                }
                if (isChecked && !SelectedItems.Contains(item))
                {
                    SelectedItems.Add(item);
                }
                else if (!isChecked && SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
            }
            if (IsSelectedItemsChanged(oldSelectedItems, SelectedItems))
            {
                RaiseSelectedItemsChanged();
            }
        }

        #endregion public bool IsSelectAll

        #region public IList SelectedItems

        /// <summary>
        /// Gets the selected items.
        /// </summary>
        /// <remarks>Does not allow setting.</remarks>
        public IList SelectedItems
        {
            get { return GetValue(SelectedItemsProperty) as IList; }
            set
            {
                _isAllowedToWriteSelectedItems = true;
                SetValue(SelectedItemsProperty, value);
                _isAllowedToWriteSelectedItems = false;
            }
        }

        /// <summary>
        /// Identifies the SelectedItems dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems",
                typeof(IList),
                typeof(MultiComboBox),
                new PropertyMetadata(OnSelectedItemsChanged));

        /// <summary>
        /// Property changed handler of SelectedItems.
        /// </summary>
        /// <param name="d">Accordion that changed the collection.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiComboBox accordion = (MultiComboBox)d;
            if (!accordion._isAllowedToWriteSelectedItems)
            {
                // revert to old value
                accordion.SelectedItems = e.OldValue as IList;

                //throw new InvalidOperationException("resource string");//Properties.Resources.Accordion_OnSelectedItemsChanged_InvalidWrite
            }
        }

        /// <summary>
        /// Determines whether the SelectedItemsProperty may be written.
        /// </summary>
        private bool _isAllowedToWriteSelectedItems;

        /// <summary>
        /// Called when selected items collection changed.
        /// </summary>
        private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //_isSelectAllNestedLevel++;
            IsSelectAll = (SelectedItems.Count > 0 && SelectedItems.Count == Items.Count);
            //_isSelectAllNestedLevel--;

            if (ElementContentPresenter != null) //初始化时不执行下面的操作。
            {
                SetContentPresenterBySelectedItems();
                foreach (object item in Items)
                {
                    MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                    if (container != null)
                    {
                        bool isSelected = SelectedItems.Contains(item);
                        container._isSelectedNestedLevel++;
                        container.IsSelected = isSelected;
                        container._isSelectedNestedLevel--;
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前选中项的列表拷贝。
        /// </summary>
        /// <returns>ObservableCollection<object>类型的列表</returns>
        private IList GetSelectedItemsCopy()
        {
            IList currentItems = new ObservableCollection<object>();
            foreach (var item in SelectedItems)
            {
                currentItems.Add(item);
            }
            return currentItems;
        }

        #endregion public IList SelectedItems

        #region AdditionalElements

        /// <summary>
        /// 控制当没有选中项是OK button是否Enable
        /// </summary>
        public bool IsAtLeastSelectOne
        {
            get { return (bool)GetValue(IsAtLeastSelectOneProperty); }
            set { SetValue(IsAtLeastSelectOneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAtLeastSelectOne.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAtLeastSelectOneProperty =
            DependencyProperty.Register("IsAtLeastSelectOne", typeof(bool), typeof(MultiComboBox), new PropertyMetadata(true));


        #endregion

        #region AdditionalElements

        /// <summary>
        /// 控制AdditionalContent是否显示隐藏
        /// </summary>
        public Visibility AdditionalContentVisibility
        {
            get { return (Visibility)GetValue(AdditionalContentVisibilityProperty); }
            set { SetValue(AdditionalContentVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdditionalContentVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdditionalContentVisibilityProperty =
            DependencyProperty.Register("AdditionalContentVisibility", typeof(Visibility), typeof(MultiComboBox), new PropertyMetadata(Visibility.Collapsed));


        /// <summary>
        /// AdditionalContent的内容
        /// </summary>
        public FrameworkElement AdditionalElement
        {
            get { return (FrameworkElement)GetValue(AdditionalElementProperty); }
            set { SetValue(AdditionalElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdditionalElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdditionalElementProperty =
            DependencyProperty.Register("AdditionalElement", typeof(FrameworkElement), typeof(MultiComboBox), new PropertyMetadata(null));

        #endregion AdditionalElements

        #region == ComboBox Override==
        /// <summary>
        /// 将焦点转移到当前的Item子项上。
        /// </summary>
        /// <param name="container"></param>
        public void ItemFocused(MultiComboItem container)
        {
            if (object.ReferenceEquals(container, SelectAllComboBoxItem))
            {
                foreach (object item in Items)
                {
                    MultiComboItem comboItem = this.ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                    if (comboItem != null)
                    {
                        comboItem.IsFocused = false;
                        comboItem.ChangeVisualState();
                    }
                }
            }
            else
            {
                SelectAllComboBoxItem.IsFocused = false;
                SelectAllComboBoxItem.ChangeVisualState();
            }
            container.IsFocused = container.Focus();
            container.ChangeVisualState();
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);

            if (IsAtLeastSelectOne)
            {
                if (SelectedItems != null)
                {
                    this.OKButton.IsEnabled = !(SelectedItems.Count == 0);
                }
            }
            else
            {
                this.OKButton.IsEnabled = true;
            }
            SetContentPresenterBySelectedItems();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            SetContentPresenterBySelectedItems();
            if (!_allowSelectedItemsChanged)
            {
                RevertSelectedItemsState(); //如果不是通过点击OK关闭下拉框的情况，都应该去重置ComboItem的IsSelected状态状态。
            }
            else
            {
                _allowSelectedItemsChanged = false;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //屏蔽掉一些不适合AUIMultiComboBox的键。
            List<Key> shieldKeys = new List<Key>() { Key.Up, Key.Down, Key.Left, Key.Right, Key.Home, Key.End };
            if (!this.IsDropDownOpen && shieldKeys.Contains(e.Key))
            {
                e.Handled = true;
            }
            if (this.IsDropDownOpen)
            {
                MultiComboItem focusedElement = FocusManager.GetFocusedElement(this) as MultiComboItem;
                switch (e.Key)
                {
                    case Key.Enter:
                        IsDropDownOpen = false;
                        e.Handled = true;
                        break;
                    case Key.Space:
                        if (focusedElement != null)
                        {
                            focusedElement.IsSelected = !focusedElement.IsSelected;
                            if (object.ReferenceEquals(focusedElement, SelectAllComboBoxItem))
                            {
                                //this.IsSelectAll =!IsSelectAll;
                                bool isChecked = SelectAllComboBoxItem.IsSelected;
                                if (this.OKButton != null)
                                {
                                    if (IsAtLeastSelectOne)
                                    {
                                        this.OKButton.IsEnabled = (Items.Count == 0) ? false : isChecked;
                                    }
                                    else
                                    {
                                        this.OKButton.IsEnabled = true;
                                    }
                                }
                                foreach (object item in Items)
                                {
                                    MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                                    if (container != null)
                                    {
                                        container._isSelectedNestedLevel++; //_isSelectedNestedLevel保证不会循环触发IsChecked事件
                                        container.IsSelected = isChecked;
                                        container._isSelectedNestedLevel--;
                                    }
                                }
                            }
                        }
                        e.Handled = true;
                        return;
                    case Key.Down:
                        if (focusedElement == null || object.ReferenceEquals(focusedElement, SelectAllComboBoxItem))
                        {
                            MultiComboItem firstContainer = ItemContainerGenerator.ContainerFromIndex(0) as MultiComboItem;
                            if (firstContainer != null)
                            {
                                this.ItemFocused(firstContainer);
                            }
                            e.Handled = true;
                        }
                        break;
                    case Key.Up:
                        if (focusedElement != null && ItemContainerGenerator.IndexFromContainer(focusedElement) == 0) //翻到第一项。
                        {
                            this.ItemFocused(SelectAllComboBoxItem);
                            e.Handled = true;
                        }
                        break;
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #region == ItemsControl Override==
        /// <summary>
        /// Creates or identifies the element that is used to display the given 
        /// item.
        /// </summary>
        /// <returns>
        /// The element that is used to display the given item.
        /// </returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MultiComboItem();
        }

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own 
        /// container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>
        /// True if the item is (or is eligible to be) its own container; 
        /// otherwise, false.
        /// </returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MultiComboItem;
        }

        /// <summary>
        /// Prepares the specified element to display the specified item.
        /// </summary>
        /// <param name="element">The element used to display the specified item.</param>
        /// <param name="item">The item to display.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            MultiComboItem comboItem = element as MultiComboItem;
            if (comboItem != null)
            {
                // give AUIMultiComboItem a reference back to the parent AUIMultiComboBox.
                comboItem.ParentComboBox = this;

                if (SelectedItems.Contains(item))
                {
                    comboItem.IsSelected = true;
                }
                // item might have been preselected when added to the item collection. 
                // at that point the parent had not been registered yet, so no notification was done.
                if (comboItem.IsSelected)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Undoes the effects of the <see cref="M:System.Windows.Controls.ItemsControl.PrepareContainerForItemOverride(System.Windows.DependencyObject,System.Object)"/> 
        /// method.
        /// </summary>
        /// <param name="element">The container element.</param>
        /// <param name="item">The item that should be cleared.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            MultiComboItem accordionItem = element as MultiComboItem;
            if (accordionItem != null)
            {
                accordionItem.IsSelected = false;
                // release the parent child relationship.
                accordionItem.ParentComboBox = null;
            }

            base.ClearContainerForItemOverride(element, item);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    object newItem = e.NewItems[0];
                    if (newItem is MultiComboItem)
                    {
                        MultiComboItem container = newItem as MultiComboItem;
                        if (container.IsSelected && !SelectedItems.Contains(newItem))
                        {
                            SelectedItems.Add(newItem); //应用于<AUIMultiComboItem IsSelected=true/>的情况。
                        }
                    }
                    else
                    {
                        if (IsSelectAll && !SelectedItems.Contains(newItem))
                        {
                            SelectedItems.Add(newItem); //应用于<AUIMultiCombBox IsSelectAll="True"/>的情况。相应的在PrepareContainerForItemOverride()时，需要选中相关的容器。
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    object oldItem = e.OldItems[0];
                    if (SelectedItems.Contains(oldItem))
                    {
                        SelectedItems.Remove(oldItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    SelectedItems.Clear();
                    break;
            }
        }
        #endregion == ItemsControl ==

        /// <summary>
        /// Occurs when the SelectedItems collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler SelectedItemsChanged;
        private void RaiseSelectedItemsChanged()
        {
            if (SelectedItemsChanged != null)
            {
                SelectedItemsChanged(this, null);
            }
            if (this.SelectedItemsChangedCommand != null)
            {
                this.SelectedItemsChangedCommand.Execute(this.SelectedItems);
            }
        }

        public static readonly DependencyProperty SelectedItemsChangedCommandProperty =
            DependencyProperty.Register("SelectedItemsChangedCommand", typeof(DelegateCommand<IList>), typeof(MultiComboBox));

        public DelegateCommand<IList> SelectedItemsChangedCommand
        {
            get { return (DelegateCommand<IList>)GetValue(SelectedItemsChangedCommandProperty); }
            set { SetValue(SelectedItemsChangedCommandProperty, value); }
        }

        /// <summary>
        /// 点击OK按钮时执行的外部事件。
        /// </summary>
        public event RoutedEventHandler ButtonClick;

        /// <summary>
        /// 区分关闭下拉框是通过点击OK按钮还是Cancel按钮，还是点击其他区域。
        /// </summary>
        public bool? IsOKClicked { get; private set; }

        #region == Methods ==

        /// <summary>
        /// 选中所有的子项。
        /// </summary>
        public void SelectAll()
        {
            //IsSelectAll = true;
            if (IsSelectAll == false)
            {
                CheckAll(true);
            }
        }

        /// <summary>
        /// 取消所有的子项。
        /// </summary>
        public void UnSelectAll()
        {
            //IsSelectAll = false;
            if (SelectedItems.Count != 0)
            {
                CheckAll(false);
            }
        }

        /// <summary>
        /// Called when an AUIMultiComboItem selected or unselected.
        /// </summary>
        /// <param name="accordionItem">The AUIMultiComboBox item that was operated now.</param>
        internal void OnItemIsSelectedPropertyChanged(MultiComboItem accordionItem)
        {
            if (object.ReferenceEquals(accordionItem, SelectAllComboBoxItem))
            {
                return; //Select All ComboBoxItem不参与SelectedItems的相关事件。
            }
            bool isAllItemsChecked = false;
            foreach (object item in Items)
            {
                MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                if (container == null || !container.IsSelected)
                {
                    isAllItemsChecked = false;
                    break;
                }
                isAllItemsChecked = true;
            }
            SelectAllComboBoxItem.IsSelected = isAllItemsChecked; //不修改IsSelectAll属性值。
            if (OKButton != null)
            {
                if (!IsAtLeastSelectOne)
                {
                    OKButton.IsEnabled = true;
                }
                else
                {
                    foreach (object item in Items)
                    {
                        MultiComboItem container = ItemContainerGenerator.ContainerFromItem(item) as MultiComboItem;
                        if (container != null && container.IsSelected)
                        {
                            OKButton.IsEnabled = true;
                            break;
                        }
                        OKButton.IsEnabled = false;
                    }
                }

            }
        }

        //add by swang，for account manager
        /// <summary>
        /// 回显数据
        /// </summary>
        /// <param name="selectedItems">想要回显的数据</param>
        public void SetSelectedItems(IList selectedItems)
        {
            if (IsSelectAll == false)
            {
                SelectedItems.Clear();

                for (int i = 0; i < selectedItems.Count; i++)
                {
                    foreach (object item in Items)
                    {
                        if (item == selectedItems[i])
                        {
                            SelectedItems.Add(item);
                        }
                    }
                }
                RaiseSelectedItemsChanged();
            }
        }

        #endregion == Methods ==
    }
}
