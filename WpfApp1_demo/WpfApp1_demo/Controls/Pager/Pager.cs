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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AvePoint.Migrator.Common.Controls
{
    public class Pager : Control
    {
        public event EventHandler<EventArgs> PageIndexChanged;
        public event EventHandler<EventArgs> PageSizeChanged;
        public event RoutedEventHandler PreviousGroupButtonClick;
        public event RoutedEventHandler NextGroupButtonClick;

        #region Variable
        #region == PreviousNextPager Variable ==
        private ComboBox PageSizeComboBox;
        private TextBox PageIndexTextBox;
        private Button MoveToPreviousPageButton;
        private Button MoveToNextPageButton;
        #endregion == PreviousNextPager Variable ==

        #region == GroupPager Variable ==
        /// <summary>
        /// Private accessor for the panel hosting the buttons.
        /// </summary>
        private Panel _numericButtonPanel;
        private ComboBox _pageSizeComboBox;
        private Button _previousPageBtn;
        private Button _nextPageBtn;
        private Button _previousGroupBtn;
        private Button _nextGroupBtn;
        private StackPanel _sp;

        #endregion == GroupPager Variable ==

        private string lastText = "1";

        #endregion Variable

        static Pager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pager), new FrameworkPropertyMetadata(typeof(Pager)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            PageSizeComboBox = GetTemplateChild("PageSizeComboBox") as ComboBox;
            PageIndexTextBox = GetTemplateChild("PageIndexTextBox") as TextBox;
            MoveToPreviousPageButton = GetTemplateChild("MoveToPreviousPageButton") as Button;
            MoveToNextPageButton = GetTemplateChild("MoveToNextPageButton") as Button;
            PageSizeComboBox.SelectionChanged -= new SelectionChangedEventHandler(PageSizeComboBox_SelectionChanged);
            PageSizeComboBox.SelectionChanged += new SelectionChangedEventHandler(PageSizeComboBox_SelectionChanged);
            PageIndexTextBox.GotFocus -= new RoutedEventHandler(PageIndexTextBox_GotFocus);
            PageIndexTextBox.GotFocus += new RoutedEventHandler(PageIndexTextBox_GotFocus);
            PageIndexTextBox.LostFocus -= new RoutedEventHandler(PageIndexTextBox_LostFocus);
            PageIndexTextBox.LostFocus += new RoutedEventHandler(PageIndexTextBox_LostFocus);
            PageIndexTextBox.KeyDown -= new KeyEventHandler(PageIndexTextBox_KeyDown);
            PageIndexTextBox.KeyDown += new KeyEventHandler(PageIndexTextBox_KeyDown);
            MoveToPreviousPageButton.Click -= new RoutedEventHandler(MoveToPreviousPageButton_Click);
            MoveToPreviousPageButton.Click += new RoutedEventHandler(MoveToPreviousPageButton_Click);
            MoveToNextPageButton.Click -= new RoutedEventHandler(MoveToNextPageButton_Click);
            MoveToNextPageButton.Click += new RoutedEventHandler(MoveToNextPageButton_Click);

            _pageSizeComboBox = GetTemplateChild("PageSizeComboBoxGroup") as ComboBox;
            _previousPageBtn = GetTemplateChild("PreviousPageBtn") as Button;
            _nextPageBtn = GetTemplateChild("NextPageBtn") as Button;
            _previousGroupBtn = GetTemplateChild("PreviousGroupBtn") as Button;
            _nextGroupBtn = GetTemplateChild("NextGroupBtn") as Button;
            _sp = GetTemplateChild("SP") as StackPanel;
            _pageSizeComboBox.SelectionChanged -= PageSizeComboBox_SelectionChanged;
            _pageSizeComboBox.SelectionChanged += PageSizeComboBox_SelectionChanged;
            _previousPageBtn.Click -= MoveToPreviousPageButton_Click;
            _previousPageBtn.Click += MoveToPreviousPageButton_Click;
            _nextPageBtn.Click -= MoveToNextPageButton_Click;
            _nextPageBtn.Click += MoveToNextPageButton_Click;
            _previousGroupBtn.Click -= new RoutedEventHandler(_previousGroupBtn_Click);
            _previousGroupBtn.Click += new RoutedEventHandler(_previousGroupBtn_Click);
            _nextGroupBtn.Click -= new RoutedEventHandler(_nextGroupBtn_Click);
            _nextGroupBtn.Click += new RoutedEventHandler(_nextGroupBtn_Click);

            // remove previous panel + buttons.
            if (this._numericButtonPanel != null)
            {
                this._numericButtonPanel.Children.Clear();
            }

            this._numericButtonPanel = GetTemplateChild("NumericButtonPanel") as Panel;

            // add new buttons to panel
            if (this._numericButtonPanel != null)
            {
                if (this._numericButtonPanel.Children.Count > 0)
                {
                    //throw new InvalidOperationException("The NumericButtonPanel contains invalid children.");
                }
                this.UpdateButtonCount();
            }

            OnPageSizeChanged();
            UpdatePagerModeDisplay();
        }

        private void PageIndexTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var source = sender as TextBox;
            string text = source.Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            // ====================

            // modified by jptian: 解决转换失败的问题
            int number;
            bool b = int.TryParse(text, out number);
            if (b)
            {
                //[ADO-64177] by glwang : 解决控件中PageIndexTextBox可以输入0及负数字的问题。
                number = int.Parse(text) >= 0 ? int.Parse(text) : 1;
                //当输入框中的数字超过总页数时this.PageIndex不变
                this.PageIndex = number > this.PageCount ? this.PageIndex : number;
                source.Text = this.PageIndex.ToString();
                lastText = source.Text;
            }
            else
            {
                source.Text = lastText;
            }

        }

        void _nextGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CanNextGroup = Visibility.Collapsed;

            if (NextGroupButtonClick != null)
            {
                this.NextGroupButtonClick(sender, e);
            }

            if (this.PageIndex % this.ShowPagesCountInGroup != 0)
            {
                this.PageIndex += this.ShowPagesCountInGroup - this.PageIndex % this.ShowPagesCountInGroup + 1;
            }
            else
            {
                this.PageIndex += 1;
            }
        }

        void _previousGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CanPreviousGroup = Visibility.Collapsed;

            if (PreviousGroupButtonClick != null)
            {
                this.PreviousGroupButtonClick(sender, e);
            }

            this.PageIndex -= this.PageIndex % this.ShowPagesCountInGroup == 0 ?
                this.ShowPagesCountInGroup : this.PageIndex % this.ShowPagesCountInGroup;

        }

        void PageIndexTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var source = sender as TextBox;
        }

        void MoveToNextPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.PageIndex++;
        }

        void MoveToPreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.PageIndex--;
        }

        void PageIndexTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var source = sender as TextBox;
            string text = source.Text;
            if (e.Key == Key.Enter)
            {
                // modified by jptian: [ADO-5351]如果为空，则不作响应 -- 2011.09.28
                // ====================
                if (string.IsNullOrEmpty(text))
                {
                    e.Handled = true;
                    return;
                }
                // ====================

                // modified by jptian: 解决转换失败的问题
                int number;
                bool b = int.TryParse(text, out number);
                if (b)
                {
                    //[ADO-64177] by glwang : 解决控件中PageIndexTextBox可以输入0及负数字的问题。
                    number = int.Parse(text) >= 0 ? int.Parse(text) : 1;
                    //当输入框中的数字超过总页数时this.PageIndex不变
                    this.PageIndex = number > this.PageCount ? this.PageIndex : number;
                    source.Text = this.PageIndex.ToString();
                    lastText = source.Text;
                }
                else
                {
                    source.Text = lastText;
                }
            }
        }

        void PageSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox source = sender as ComboBox;
            if (source.SelectedItem != null)
            {
                int number = (int)source.SelectedItem;
                this.PageSize = number;
            }
        }

        #region Propertis

        #region == PreviousNextPager Propertis ==
        public bool CanMoveToNextPage
        {
            get { return (bool)GetValue(CanMoveToNextPageProperty); }
            set { SetValue(CanMoveToNextPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanMoveToNextPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanMoveToNextPageProperty =
            DependencyProperty.Register("CanMoveToNextPage", typeof(bool), typeof(Pager), new PropertyMetadata(false));

        public bool CanMoveToPreviousPage
        {
            get { return (bool)GetValue(CanMoveToPreviousPageProperty); }
            set { SetValue(CanMoveToPreviousPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanMoveToPreviousPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanMoveToPreviousPageProperty =
            DependencyProperty.Register("CanMoveToPreviousPage", typeof(bool), typeof(Pager), new PropertyMetadata(false));

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(Pager), new PropertyMetadata(1, OnPageCountChanged));

        private static void OnPageCountChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Pager source = o as Pager;
            if (source.PageIndex > source.PageCount)
            {
                source.PageIndex = source.PageCount;
            }
            source.CheckButtonsEnabledState();
            source.PageCountStr = source.PageCount.ToString();
            source.UpdateButtonCount();
        }

        //Only used in PreviousNextMode
        public string PageCountStr
        {
            get { return (string)GetValue(PageCountStrProperty); }
            set { SetValue(PageCountStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageCountStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountStrProperty =
            DependencyProperty.Register("PageCountStr", typeof(string), typeof(Pager), new PropertyMetadata("1"));

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(Pager), new PropertyMetadata(1, OnPageIndexChanged));

        private static void OnPageIndexChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Pager source = o as Pager;
            source.OnPageIndexChanged();
        }

        private void OnPageIndexChanged()
        {
            if (PageIndexTextBox != null)
            {
                CheckButtonsEnabledState();
                PageIndexStr = this.PageIndex.ToString();
                PageIndexTextBox.Text = PageIndexStr;
                if (this._numericButtonPanel != null)
                {
                    UpdateButtonCount();
                    foreach (UIElement ui in this._numericButtonPanel.Children)
                    {
                        ToggleButton button = ui as ToggleButton;
                        if (button != null)
                        {
                            if (button.Content.ToString() == this.PageIndex.ToString())
                            {
                                button.IsChecked = true;
                            }
                            else
                            {
                                button.IsChecked = false;
                            }
                        }
                    }
                }
                if (this.PageIndexChanged != null)
                {
                    this.PageIndexChanged(this, new EventArgs());
                }
            }
        }

        //Only used in PreviousNextMode
        public string PageIndexStr
        {
            get { return (string)GetValue(PageIndexStrProperty); }
            set { SetValue(PageIndexStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndexStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexStrProperty =
            DependencyProperty.Register("PageIndexStr", typeof(string), typeof(Pager), new PropertyMetadata("1"));

        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(Pager), new PropertyMetadata(15, OnPageSizeChanged));

        private static void OnPageSizeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Pager source = o as Pager;
            source.OnPageSizeChanged();
        }

        private void OnPageSizeChanged()
        {
            if (PageSizeComboBox != null)
            {
                PageSizeComboBox.SelectedValue = this.PageSize;
            }
            if (_pageSizeComboBox != null)
            {
                _pageSizeComboBox.SelectedValue = this.PageSize;
                UpdateButtonCount();
            }
            if (this.PageSizeChanged != null)
            {
                this.PageSizeChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// 每页数据条数的List
        /// </summary>
        public List<int> PageSizeCases
        {
            get { return (List<int>)GetValue(PageSizeCasesProperty); }
            set { SetValue(PageSizeCasesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSizeCases.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeCasesProperty =
            DependencyProperty.Register("PageSizeCases", typeof(List<int>), typeof(Pager), new PropertyMetadata(new List<int>() { 5, 8, 10, 15, 25, 50, 100 }));

        /// <summary>
        /// Pager 有两种Mode，分别是PreviousNextMode和GroupMode
        /// </summary>
        public PagerDisplayMode PagerDisplayMode
        {
            get { return (PagerDisplayMode)GetValue(PagerDisplayModeProperty); }
            set { SetValue(PagerDisplayModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PagerDisplayMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagerDisplayModeProperty =
            DependencyProperty.Register("PagerDisplayMode", typeof(PagerDisplayMode), typeof(Pager), new PropertyMetadata(PagerDisplayMode.PreviousNextMode, OnPagerDisplayModeChanged));

        private static void OnPagerDisplayModeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Pager source = o as Pager;
            source.OnPagerDisplayModeChanged();
        }

        private void OnPagerDisplayModeChanged()
        {
            UpdatePagerModeDisplay();
        }

        /// <summary>
        /// 绑定Loading图片的显示隐藏(GroupMode)
        /// </summary>
        public Visibility LoadingVisibility
        {
            get { return (Visibility)GetValue(LoadingVisibilityProperty); }
            set { SetValue(LoadingVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadingVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadingVisibilityProperty =
            DependencyProperty.Register("LoadingVisibility", typeof(Visibility), typeof(Pager), new PropertyMetadata(Visibility.Collapsed));

        #endregion == PreviousNextPager Propertis ==

        #region == GroupPager Propertis ==

        /// <summary>
        /// binding to Visibility Property of PreviousGroupBtn
        /// </summary>
        public Visibility CanPreviousGroup
        {
            get { return (Visibility)GetValue(CanPreviousGroupProperty); }
            set { SetValue(CanPreviousGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanPreviousGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanPreviousGroupProperty =
            DependencyProperty.Register("CanPreviousGroup", typeof(Visibility), typeof(Pager), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// binding to Visibility Property of NextGroupBtn
        /// </summary>
        public Visibility CanNextGroup
        {
            get { return (Visibility)GetValue(CanNextGroupProperty); }
            set { SetValue(CanNextGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanNextGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanNextGroupProperty =
            DependencyProperty.Register("CanNextGroup", typeof(Visibility), typeof(Pager), new PropertyMetadata(Visibility.Collapsed));


        /// <summary>
        /// 记录GroupPagerMode中每个group中最多显示的Page的个数
        /// </summary>
        public int ShowPagesCountInGroup
        {
            get { return (int)GetValue(ShowPagesCountInGroupProperty); }
            set { SetValue(ShowPagesCountInGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPagesCountInGroup.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPagesCountInGroupProperty =
            DependencyProperty.Register("ShowPagesCountInGroup", typeof(int), typeof(Pager), new PropertyMetadata(9, OnShowPagesCountInGroupChanged));

        private static void OnShowPagesCountInGroupChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Pager source = o as Pager;
            source.OnShowPagesCountInGroupChanged();
        }

        private void OnShowPagesCountInGroupChanged()
        {
            this.UpdateButtonCount();
        }

        #endregion == GroupPager Propertis ==

        #endregion Propertis

        #region == Method ==

        private void CheckButtonsEnabledState()
        {
            this.CanMoveToPreviousPage = this.PageIndex > 1;
            this.CanMoveToNextPage = this.PageIndex < this.PageCount;

            if (this.PageIndex <= this.ShowPagesCountInGroup)
            {
                this.CanPreviousGroup = Visibility.Collapsed;
            }
            else
            {
                //由于_previousGroupBtn click之后不会走MouseLeave状态，所以要手动设置Normal，以免下次显示的时候还是MouseOver的状态
                if (_previousGroupBtn != null)
                {
                    VisualStateManager.GoToState(_previousGroupBtn, "Normal", true);
                }
                this.CanPreviousGroup = Visibility.Visible;
            }

            int lastIndexInGroup = this.PageIndex % this.ShowPagesCountInGroup == 0 ? this.PageIndex :
                (this.PageIndex / this.ShowPagesCountInGroup + 1) * this.ShowPagesCountInGroup;
            if (this.PageCount > lastIndexInGroup)
            {
                //由于_nextGroupBtn click之后不会走MouseLeave状态，所以要手动设置Normal，以免下次显示的时候还是MouseOver的状态
                if (_nextGroupBtn != null)
                {
                    VisualStateManager.GoToState(_nextGroupBtn, "Normal", true);
                }
                this.CanNextGroup = Visibility.Visible;
            }
            else
            {
                this.CanNextGroup = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Updates the visual display of the number of buttons that we display.
        /// </summary>
        private void UpdateButtonCount()
        {
            // what we should use as the button count
            int startIndex = this.PageIndex % this.ShowPagesCountInGroup == 0 ?
                    (this.PageIndex / this.ShowPagesCountInGroup - 1) * this.ShowPagesCountInGroup + 1 :
                    (Convert.ToInt16(this.PageIndex / this.ShowPagesCountInGroup)) * this.ShowPagesCountInGroup + 1;

            //this.PageCount - startIndex:如果是最后一组数据，可能有最后一组的page数小于ShowPagesCountInGroup的情况，此时显示小的
            int buttonCount = this.ShowPagesCountInGroup;
            if (this.PageCount - startIndex + 1 > 0 && this.PageCount - startIndex + 1 < this.ShowPagesCountInGroup)
            {
                buttonCount = this.PageCount - startIndex + 1;
            }

            if (this._numericButtonPanel != null)
            {
                // add new page button
                while (this._numericButtonPanel.Children.Count < buttonCount)
                {
                    ToggleButton button = new ToggleButton();
                    button.Style = _sp.Resources["NumericButtonStyle"] as Style;
                    button.Padding = new Thickness(4, 0, 4, 0);

                    button.Checked -= new RoutedEventHandler(ToggleButton_Checked);
                    button.Checked += new RoutedEventHandler(ToggleButton_Checked);
                    this._numericButtonPanel.Children.Add(button);
                }

                // remove excess
                while (this._numericButtonPanel.Children.Count > buttonCount)
                {
                    ToggleButton button = this._numericButtonPanel.Children[0] as ToggleButton;
                    if (button != null)
                    {
                        button.Checked -= new RoutedEventHandler(this.ToggleButton_Checked);
                        this._numericButtonPanel.Children.Remove(button);
                    }
                }

                this.UpdateButtonDisplay();
            }
        }

        /// <summary>
        /// Updates the visual content and style of the buttons that we display.
        /// </summary>
        private void UpdateButtonDisplay()
        {
            if (this._numericButtonPanel != null)
            {
                // 选中pageindex的group中第一个page的index
                int startIndex = this.PageIndex % this.ShowPagesCountInGroup == 0 ?
                    (this.PageIndex / this.ShowPagesCountInGroup - 1) * this.ShowPagesCountInGroup + 1 :
                    (Convert.ToInt16(this.PageIndex / this.ShowPagesCountInGroup)) * this.ShowPagesCountInGroup + 1;

                int index = startIndex;
                foreach (UIElement ui in this._numericButtonPanel.Children)
                {
                    ToggleButton button = ui as ToggleButton;
                    if (button != null)
                    {
                        button.Content = index;
                        if (this.PageIndex == index)
                        {
                            button.IsChecked = true;
                        }
                        index++;
                    }
                }
            }
        }

        void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton checkedbutton = sender as ToggleButton;
            this.PageIndex = (int)checkedbutton.Content;
            foreach (UIElement ui in this._numericButtonPanel.Children)
            {
                ToggleButton button = ui as ToggleButton;
                if (button != checkedbutton)
                {
                    button.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// Updates the visual display to show the current pager mode
        /// we have selected.
        /// </summary>
        private void UpdatePagerModeDisplay()
        {
            VisualStateManager.GoToState(this, this.PagerDisplayMode.ToString(), true);
        }

        #endregion == Method ==
    }

    public enum PagerDisplayMode
    {
        /// <summary>
        /// Shows the Previous and Next buttons + the numeric display
        /// </summary>
        PreviousNextMode,

        /// <summary>
        /// Shows the group display
        /// </summary>
        GroupNumericMode
    }
}
