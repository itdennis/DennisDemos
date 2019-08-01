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
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MigratorTool.WPF.I18N;
using MigratorTool.WPF.View.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// <para>
    /// 在页面上显示文本框和按钮,用来帮助用户选取时间
    /// </para>
    /// <para>
    /// 可以设置<see cref="IsDateRangePicker"/>为true来设置选取时间段的功能.
    /// </para>
    /// <para>
    /// 可以设置<see cref="HasTimePicker"/>为true来设置选取具体时间的功能.
    /// </para>
    /// <para>
    /// 可以设置<see cref="AUIDatePicker.Culture"/>来更改语言.
    /// </para>
    /// <para>
    /// 更多功能请参见AUI控件演示页面.
    /// </para>
    /// </summary>
    /// <example>
    /// 初始化:
    /// <code>
    /// AUIDatePicker datePicker = new AUIDatePicker();
    /// LayOutRoot.Children.Add(datePicker);
    /// </code>
    /// <code lang="xaml">
    /// <AUIDatePicker IsDateRangePicker="True" Culture="zh-CN" />
    /// </code>
    /// 获取值:
    /// <code>
    ///DateTime selectedStartDate = datePicker.SelectedStartDateTime;
    ///DateTime selectedEndDate = datePicker.SelectedEndDateTime;
    /// </code>
    /// </example>
    /// <remarks>
    /// 有任何问题请与作者联系,Yahoo:zhangning0118
    /// </remarks>
    /// <seealso cref="AUITimePicker"/>
    [TemplatePart(Name = AUIDatePicker.ElementRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = AUIDatePicker.ElementStartTimeTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = AUIDatePicker.ElementEndTimeTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = "ToTextBox", Type = typeof(TextBlock))]
    [TemplatePart(Name = AUIDatePicker.ElementButton, Type = typeof(ImageButton))]
    [TemplatePart(Name = AUIDatePicker.ElementPopup, Type = typeof(Popup))]
    [TemplateVisualState(Name = VisualStates.StateNormal, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateDisabled, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateValid, GroupName = VisualStates.GroupValidation)]
    [TemplateVisualState(Name = VisualStates.StateInvalidFocused, GroupName = VisualStates.GroupValidation)]
    [TemplateVisualState(Name = VisualStates.StateInvalidUnfocused, GroupName = VisualStates.GroupValidation)]
    public class AUIDatePicker : Control
    {
        #region Properties

        private Grid mTextArea;
        /// <summary>
        /// AUIDatePicker的根节点
        /// </summary>
        private const string ElementRoot = "Root";

        /// <summary>
        /// 用来显示开始时间的文本框
        /// </summary>
        private const string ElementStartTimeTextBox = "StartTimeTextBox";

        /// <summary>
        /// 用来显示结束时间的文本框
        /// </summary>
        private const string ElementEndTimeTextBox = "EndTimeTextBox";

        /// <summary>
        /// 存放结束时间文本框的Stackpanel
        /// 之所以加了一层是因为里面还有个横杠,它是需要和结束时间文本框一起显示或消失的
        /// </summary>
        private const string ElementEndTimeTextBoxSP = "EndTimeTextBoxSP";

        /// <summary>
        /// 下拉框按钮,点击之后会弹出calendar
        /// </summary>
        private const string ElementButton = "Button";

        /// <summary>
        /// 弹出窗口
        /// </summary>
        private const string ElementPopup = "Popup";

        /// <summary>
        /// 默认的文字
        /// </summary>
        //private string _defaultText;

        /// <summary>
        /// 默认的时间格式(短)
        /// </summary>
        private string _defaultShortFormat = "yyyy-MM-dd";

        /// <summary>
        /// 默认的时间格式(长)
        /// </summary>
        private string _defaultLongFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 下拉按钮
        /// </summary>
        private ImageButton _dropDownButton;

        /// <summary>
        /// 当需要选择时间段或者具体时间时出现的按钮,用来确认选择
        /// </summary>
        private Button _okButton;

        /// <summary>
        /// 用来显示遮罩的canvas 点击改canvas会关闭popup
        /// </summary>
        private Canvas _outsideCanvas;

        /// <summary>
        /// 用来存放timepicker的容器
        /// </summary>
        private Grid _timePickerGrid;

        /// <summary>
        /// popup内的canvas,其内存放outsidecanvas和outsidestackpanel
        /// </summary>
        private Canvas _outsidePopupCanvas;

        /// <summary>
        /// popup
        /// </summary>
        private Popup _popUp;

        /// <summary>
        /// 用来存放calendar和timepicker等元素的最外层stackpanel
        /// </summary>
        private StackPanel _outsideStackPanel;

        /// <summary>
        /// 专门用来存放calendar的stackpanel
        /// </summary>
        private StackPanel _calendarAreaSP;

        /// <summary>
        /// _calendarAreaSP内,存放开始时间calendar
        /// </summary>
        private StackPanel _calendarLeftSP;

        /// <summary>
        /// _calendarAreaSP内,存放结束时间calendar
        /// </summary>
        private StackPanel _calendarRightSP;

        /// <summary>
        /// Left Stackpanel where TimePicker in.
        /// </summary>
        //private StackPanel _timePickerLeftSP;

        /// <summary>
        /// Right Stackpanel where TimePicker in.
        /// </summary>
        //private StackPanel _timePickerRightSP;

        /// <summary>
        /// 用来存放ok按钮的stackpanel
        /// </summary>
        private SubmitPanel _buttonSP;

        /// <summary>
        /// 根节点
        /// </summary>
        private FrameworkElement _root;

        /// <summary>
        /// 最外层的border,其内包含outsidestackpanel
        /// </summary>
        private Border _outsideBorder;

        /// <summary>
        /// 标识datepicker是否已被初始化
        /// </summary>
        private bool Initialized;

        /// <summary>
        /// 当开始时间变化时触发的方法
        /// </summary>
        public event EventHandler<SelectionChangedEventArgs> StartDateTimeChanged;

        /// <summary>
        /// 点击OK时触发的事件
        /// </summary>
        public event EventHandler SelectOKClicked;

        private TextBlock mErrorMsgTB;

        //============= Add By CTH [ADO-7986] ==================

        /// <summary>
        /// 用于显示提示信息的StackPanel
        /// </summary>
        private StackPanel _PromptInfoSP;

        /// <summary>
        /// 用于显示提示信息的Image
        /// </summary>
        private Image _PromptInfoImage;

        /// <summary>
        /// 用于显示提示信息的TextBlock
        /// </summary>
        private TextBlock _PromptInfoTB;

        //======================= End ==========================

        private StackPanel _TimeZoneSP;
        private ComboBox _TimeZoneCB;
        private AUITimeZone _timeZone = AUITimeZone.GetCurrentTimeZone();


        //fix[ADO-22817] 显示即check  start

        public bool HasDaylightSavingTimeCBChecked { get; set; }

        //fix[ADO-22817] 显示即check  end

        public AUITimeZone TimeZone
        {
            get
            {
                return _timeZone;
            }
            set
            {
                _timeZone = value;
                OnTimeZoneChanged();
            }
        }

        private void OnTimeZoneChanged()
        {
            if (this.IsDropDownOpen && this._TimeZoneCB != null)
            {
                if (!this._TimeZoneCB.SelectedItem.Equals(this.TimeZone))
                {
                    this._TimeZoneCB.SelectedItem = this.TimeZone;
                }
            }
        }

        public bool? AutoAdjustClockForDaylightSavingChanges
        {
            get;
            set;
        }

        public bool HasTimeZone
        {
            get { return (bool)GetValue(HasTimeZoneProperty); }
            set { SetValue(HasTimeZoneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasTimeZone.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasTimeZoneProperty =
            DependencyProperty.Register(
            "HasTimeZone",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnHasTimeZoneChanged));

        private static void OnHasTimeZoneChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker source = o as AUIDatePicker;
            if (source != null && source.Initialized)
            {
                source.ReSet();
            }
        }

        /// <summary>
        /// 标识是否只能选择将来时
        /// </summary>
        public bool OnlyFuture
        {
            get { return (bool)GetValue(OnlyFutureProperty); }
            set { SetValue(OnlyFutureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OnlyFuture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnlyFutureProperty =
            DependencyProperty.Register(
            "OnlyFuture",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnOnlyFutureChanged));

        /// <summary>
        /// 当<see cref="OnlyFuture"/>属性由false变为true时,检查当前的选中时间,若早于当前时间则重置为当前时间<br/>
        /// 设置calendar的属性使过去时隐藏
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnOnlyFutureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;
            bool? newValue = e.NewValue as bool?;
            if (newValue.HasValue && newValue.Value)
            {
                if (dp.SelectedStartDateTime.HasValue && DateTime.Compare(dp.SelectedStartDateTime.Value, DateTime.Now) < 0)
                {
                    dp.SelectedStartDateTime = null;
                    if (dp.IsDateRangePicker && dp.SelectedEndDateTime.HasValue)
                    {
                        dp.SelectedEndDateTime = null;
                    }
                }
            }
            if (newValue.HasValue && dp.Initialized && dp.IsDropDownOpen)
            {
                dp.SetOnlyFutureStatus(dp.OnlyFuture);
            }
        }

        /// <summary>
        /// 根据OnlyFuture属性设定calendar的状态
        /// </summary>
        /// <param name="isOnlyFuture"></param>
        private void SetOnlyFutureStatus(bool isOnlyFuture)
        {
            if (null != this._startTimeCalendar)
            {
                this._startTimeCalendar.DisplayDateStart = isOnlyFuture ? DateTime.Now : DateTime.MinValue;
            }
            if (this.IsDateRangePicker && null != this._endTimeCalendar)
            {
                this._endTimeCalendar.DisplayDateStart = isOnlyFuture ? DateTime.Now : DateTime.MinValue;
            }
        }

        /// <summary>
        /// 标识是否只能选择过去时
        /// </summary>
        public bool OnlyPast
        {
            get { return (bool)GetValue(OnlyPastProperty); }
            set { SetValue(OnlyPastProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OnlyPast.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnlyPastProperty =
            DependencyProperty.Register(
            "OnlyPast",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnOnlyPastChanged));

        /// <summary>
        /// 当<see cref="OnlyPast"/>属性由false变为true时,检查当前的选中时间,若晚于当前时间则重置为当前时间<br/>
        /// 设置calendar的属性使将来时隐藏
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnOnlyPastChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;
            bool? newValue = e.NewValue as bool?;
            if (newValue.HasValue && newValue.Value)
            {
                if (dp.SelectedStartDateTime.HasValue && DateTime.Compare(dp.SelectedStartDateTime.Value, DateTime.Now) > 0)
                {
                    dp.SelectedStartDateTime = null;
                    if (dp.IsDateRangePicker && dp.SelectedEndDateTime.HasValue)
                    {
                        dp.SelectedEndDateTime = null;
                    }
                }
            }
            if (newValue.HasValue && dp.Initialized && dp.IsDropDownOpen)
            {
                dp.SetOnlyPastStatus(dp.OnlyPast);
            }
        }

        /// <summary>
        /// 根据OnlyPast属性设定calendar的状态
        /// </summary>
        /// <param name="isOnlyPast"></param>
        private void SetOnlyPastStatus(bool isOnlyPast)
        {
            if (null != this._startTimeCalendar)
            {
                this._startTimeCalendar.DisplayDateEnd = isOnlyPast ? DateTime.Now : DateTime.MaxValue;
            }
            if (this.IsDateRangePicker && null != this._endTimeCalendar)
            {
                this._endTimeCalendar.DisplayDateEnd = isOnlyPast ? DateTime.Now : DateTime.MaxValue;
            }
        }

        #region IsDateRangePicker
        /// <summary>
        /// 获得或者设置AUIDatePicker是否用来选择时间区域
        /// </summary>
        public bool IsDateRangePicker
        {
            get { return (bool)GetValue(IsDateRangePickerProperty); }
            set { SetValue(IsDateRangePickerProperty, value); }
        }

        public static readonly DependencyProperty IsDateRangePickerProperty =
            DependencyProperty.Register(
            "IsDateRangePicker",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnIsDateRangePickerChanged));

        /// <summary>
        /// IsDateRangePicker property changed handler.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e">The DependencyPropertyChangedEventArgs.</param>
        private static void OnIsDateRangePickerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;
            bool? newValue = e.NewValue as bool?;
            if (newValue.HasValue && dp.Initialized)
            {
                dp.ReSet();
            }
        }
        #endregion IsDateRangePicker

        /// <summary>
        /// 代表datepicker是否用可以选择时间
        /// </summary>
        public bool HasTimePicker
        {
            get { return (bool)GetValue(HasTimePickerProperty); }
            set { SetValue(HasTimePickerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasTimePicker.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasTimePickerProperty =
            DependencyProperty.Register(
            "HasTimePicker",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnHasTimePickerChanged));

        private static void OnHasTimePickerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;
            bool? newValue = e.NewValue as bool?;
            bool? oldvalue = e.OldValue as bool?;
            if (null != newValue && newValue != oldvalue && dp.Initialized)
            {
                dp.ReSet();
            }
        }

        /// <summary>
        /// 显示开始时间的TextBox
        /// </summary>
        private TextBox _startTimeTextBox;

        /// <summary>
        /// 显示结束时间的TextBox
        /// </summary>
        private TextBox _endTimeTextBox;

        /// <summary>
        /// 用来选择开始时间的calendar
        /// </summary>
        private AUICalendar _startTimeCalendar;

        /// <summary>
        /// 用来选择结束时间的calendar
        /// </summary>
        private AUICalendar _endTimeCalendar;

        /// <summary>
        /// 用来选择开始时间的AUITimePicker
        /// </summary>
        private AUITimePicker _startTimeTimePicker;

        /// <summary>
        /// 用来选择结束时间的AUITimePicker
        /// </summary>
        private AUITimePicker _endTimeTimePicker;

        #region Culture

        /// <summary>
        /// 设置或取得datepicker的culture,例如"zh-CN"\"en-US"\"ja-JP".更多的可能值请参见http://msdn.microsoft.com/zh-cn/library/system.globalization.cultureinfo(VS.80).aspx
        /// </summary>
        public string Culture
        {
            get { return (string)GetValue(CultureProperty); }
            set { SetValue(CultureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Culture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CultureProperty =
            DependencyProperty.Register(
            "Culture",
            typeof(string),
            typeof(AUIDatePicker),
            new PropertyMetadata(CultureInfo.CurrentCulture.Name, OnCultureChanged));

        private static void OnCultureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;

            string newCultureName = e.NewValue as string;
            CultureInfo newCulture = new CultureInfo(newCultureName);
            if (null != newCulture)
            {
                dp.ChangeCalendarInfoByCulture(newCulture);
                dp.ChangeTextsByCulture(newCulture);
            }
        }

        #endregion Culture

        #region DateTimeFormat

        /// <summary>
        /// 获取或设置当前的日期格式
        /// </summary>
        public string DateTimeFormat
        {
            get { return (string)GetValue(DateTimeFormatProperty); }
            set { SetValue(DateTimeFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeFormatProperty =
            DependencyProperty.Register(
            "DateTimeFormat",
            typeof(string),
            typeof(AUIDatePicker),
            new PropertyMetadata(string.Empty, OnDateTimeFormatChanged));

        private static void OnDateTimeFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;

            string newFormat = e.NewValue as string;
            string oldFormat = e.OldValue as string;
            if (newFormat != oldFormat)
            {
                string cultureString = string.Empty == dp.Culture ? "en-US" : dp.Culture;
                dp.ChangeTextsByCulture(new CultureInfo(cultureString));
            }
        }
        #endregion DateTimeFormat

        #region Texts
        /// <summary>
        /// 取得或者设置auidatepicker所显示的内容,string数组 里面最多只能存两个string 代表开始时间和结束时间
        /// </summary>
        public string[] Texts
        {
            get { return (string[])GetValue(TextsProperty); }
            set { SetValue(TextsProperty, value); }
        }

        public static readonly DependencyProperty TextsProperty =
            DependencyProperty.Register(
            "Texts",
            typeof(string[]),
            typeof(AUIDatePicker),
            new PropertyMetadata(null, OnTextsChanged));

        private static void OnTextsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;

            string[] newValue = e.NewValue as string[];
            if (newValue != null)
            {
                switch (newValue.Length)
                {
                    case 2:
                        {
                            if (null != dp._endTimeTextBox)
                            {
                                dp._endTimeTextBox.Text = newValue[1];
                            }
                            if (null != dp._startTimeTextBox)
                            {
                                dp._startTimeTextBox.Text = newValue[0];
                            }
                            break;
                        }
                    case 1:
                        {
                            if (null != dp._startTimeTextBox)
                            {
                                dp._startTimeTextBox.Text = newValue[0];
                            }
                            break;
                        }
                    case 3:
                        {
                            if (null != dp._endTimeTextBox)
                            {
                                dp._endTimeTextBox.Text = string.Empty;
                            }
                            if (null != dp._startTimeTextBox)
                            {
                                dp._startTimeTextBox.Text = string.Empty;
                            }
                            //dp.SetWaterMarkText();
                            break;
                        }
                }
                if (null != dp._startTimeTextBox && dp.HasTimeZone)
                {
                    ToolTipService.SetToolTip(dp._startTimeTextBox, I18NEntity.Get("Common.GuiControls", "Time Zone:") + dp.TimeZone.DisplayName);
                }
                if (null != dp._endTimeTextBox && dp.HasTimeZone)
                {
                    ToolTipService.SetToolTip(dp._endTimeTextBox, I18NEntity.Get("Common.GuiControls", "Time Zone:") + dp.TimeZone.DisplayName);
                }
            }
        }
        #endregion Texts

        #region SelectedDate

        /// <summary>
        /// 用户选择的开始日期,也代表选取单个日期时用户所选择的日期.
        /// </summary>
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime? SelectedStartDateTime
        {
            get { return (DateTime?)GetValue(SelectedStartDateTimeProperty); }
            set { SetValue(SelectedStartDateTimeProperty, value); }
        }

        public static readonly DependencyProperty SelectedStartDateTimeProperty =
            DependencyProperty.Register(
            "SelectedStartDateTime",
            typeof(DateTime?),
            typeof(AUIDatePicker),
            new PropertyMetadata(OnSelectedStartDateTimeChanged));

        private static void OnSelectedStartDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime? addedDate;
            DateTime? removedDate;
            AUIDatePicker dp = d as AUIDatePicker;

            addedDate = (DateTime?)e.NewValue;
            removedDate = (DateTime?)e.OldValue;

            if (addedDate != dp._startTimeCalendar.SelectedDate)
            {
                dp._startTimeCalendar.SelectedDate = addedDate;
            }

            if (dp.SelectedStartDateTime != null)
            {
                DateTime day = (DateTime)dp.SelectedStartDateTime;

                dp.Dispatcher.BeginInvoke((Action)(() => dp.updateTexts()));

                //if ((day.Month != dp.DisplayDateLeft.Month || day.Year != dp.DisplayDateLeft.Year) && !dp._calendarLeft.DatePickerDisplayDateFlag)
                //{
                //    dp.DisplayDateLeft = day;
                //}
                //dp._calendarLeft.DatePickerDisplayDateFlag = false;
                if (null != dp.StartDateTimeChanged && dp.Initialized)
                {
                    DateTime[] addedDates = new DateTime[1] { dp.SelectedStartDateTime.Value };
                    DateTime[] removedDates = new DateTime[1] { removedDate.HasValue ? removedDate.Value : DateTime.Today };
                    SelectionChangedEventArgs ea = new SelectionChangedEventArgs(null, removedDates, addedDates);
                    dp.StartDateTimeChanged(dp, ea);
                }
            }
            else
            {
                //dp._settingSelectedDate = true;
                dp.Texts = new string[2] { "", "" };
                //dp._settingSelectedDate = false;
                //dp.OnDateSelected(addedDate, removedDate);
            }
            if (dp.HasError)
            {
                dp.Validate();
            }
        }

        /// <summary>
        /// 用户选择的开始日期,也代表选取单个日期时用户所选择的日期.
        /// </summary>
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime? SelectedEndDateTime
        {
            get { return (DateTime?)GetValue(SelectedEndDateTimeProperty); }
            set { SetValue(SelectedEndDateTimeProperty, value); }
        }

        public static readonly DependencyProperty SelectedEndDateTimeProperty =
            DependencyProperty.Register(
            "SelectedEndDateTime",
            typeof(DateTime?),
            typeof(AUIDatePicker),
            new PropertyMetadata(OnSelectedEndDateTimeChanged));

        private static void OnSelectedEndDateTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime? addedDate;
            DateTime? removedDate;
            AUIDatePicker dp = d as AUIDatePicker;

            addedDate = (DateTime?)e.NewValue;
            removedDate = (DateTime?)e.OldValue;

            if (null != dp._endTimeCalendar && addedDate != dp._endTimeCalendar.SelectedDate)
            {
                dp._endTimeCalendar.SelectedDate = addedDate;
            }

            if (dp.SelectedEndDateTime != null)
            {
                DateTime day = (DateTime)dp.SelectedEndDateTime;

                dp.Dispatcher.BeginInvoke((Action)(() => dp.updateTexts()));
            }
            else
            {
                dp.Texts = new string[2] { "", "" };
            }
            if (dp.HasError)
            {
                dp.Validate();
            }
        }
        #endregion SelectedDate

        #region SelectionBackground

        public Brush SelectionBackground
        {
            get { return (Brush)GetValue(SelectionBackgroundProperty); }
            set { SetValue(SelectionBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SelectionBackgroundProperty =
            DependencyProperty.Register(
            "SelectionBackground",
            typeof(Brush),
            typeof(AUIDatePicker),
            null);
        #endregion SelectionBackground

        #region IsDropDownOpen
        /// <summary>
        /// 代表是否Calendar已经显示出来
        /// </summary>
        /// <value>
        /// 已显示则为true
        /// </value>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register(
            "IsDropDownOpen",
            typeof(bool),
            typeof(AUIDatePicker),
            new PropertyMetadata(false, OnIsDropDownOpenChanged));

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AUIDatePicker dp = d as AUIDatePicker;

            bool newValue = (bool)e.NewValue;
            bool oldValue = (bool)e.OldValue;

            if (dp._popUp != null && dp._popUp.Child != null)
            {
                if (newValue != oldValue)
                {
                    if (newValue)
                    {
                        dp.ResetCalendarAndButtonStates();
                        dp.SetOnlyFutureStatus(dp.OnlyFuture);
                        dp.SetOnlyPastStatus(dp.OnlyPast);
                        dp.SetDisplayDateRangeStatus();
                        dp.OpenPopUp();
                    }
                    else
                    {
                        dp.ClosePopUp();
                    }
                }
            }
        }

        /// <summary>
        /// 重置calendar和按钮的状态
        /// </summary>
        private void ResetCalendarAndButtonStates()
        {
            if (this.IsDateRangePicker)
            {
                if (null == this._endTimeCalendar)
                {
                    this.InitializeEndTimeCalendar();
                }
                //this._endTimeCalendar.ResetStates();
                this._endTimeCalendar.DisplayMode = CalendarMode.Month;
                DateTime? start = null == this.SelectedStartDateTime ? null : this.SelectedStartDateTime;
                DateTime? end = null == this.SelectedEndDateTime ? null : this.SelectedEndDateTime;
                this._startTimeCalendar.SelectedDate = start;
                this._startTimeCalendar.DisplayMode = CalendarMode.Month;
                this._endTimeCalendar.SelectedDate = end;
                this._startTimeCalendar.DisplayDate = start.HasValue ? start.Value : DateTime.Now;
                this._endTimeCalendar.DisplayDate = end.HasValue ? end.Value : DateTime.Now;
                if (this.HasTimePicker)
                {
                    if (null != this._startTimeTimePicker)
                    {
                        this._startTimeTimePicker.SelectedTime = start.HasValue ? start.Value : DateTime.Now;
                    }
                    if (null != this._endTimeTimePicker)
                    {
                        this._endTimeTimePicker.SelectedTime = end.HasValue ? end.Value : DateTime.Now;
                    }
                }
                this.CheckOKButtonState();
            }
            else
            {
                this._startTimeCalendar.SelectedDate = this.SelectedStartDateTime;
                this._startTimeCalendar.DisplayMode = CalendarMode.Month;
                this._startTimeCalendar.DisplayDate = this.SelectedStartDateTime.HasValue ? this.SelectedStartDateTime.Value : DateTime.Now;
                if (this.HasTimePicker)
                {
                    if (null != this._startTimeTimePicker)
                    {
                        this._startTimeTimePicker.SelectedTime = this.SelectedStartDateTime.HasValue ? this.SelectedStartDateTime.Value : DateTime.Now;
                    }
                    this.CheckOKButtonState();
                }
            }
            if (HasTimeZone)
            {
                if (TimeZone != null && _TimeZoneCB != null)
                {
                    _TimeZoneCB.SelectedItem = TimeZone;
                }
                else if (_TimeZoneCB != null)
                {
                    _TimeZoneCB.SelectedItem = TimeZone;
                }
                if (AutoAdjustClockForDaylightSavingChanges.HasValue && _DaylightSavingTimeCB != null)
                {
                    _DaylightSavingTimeCB.IsChecked = AutoAdjustClockForDaylightSavingChanges.Value;
                }
                else if (_DaylightSavingTimeCB != null)
                {
                    _DaylightSavingTimeCB.IsChecked = AUITimeZone.IsUsingDaylightSavingTime();
                }
                CheckOKButtonState();
            }
        }
        #endregion IsDropDownOpen

        //====================Added by glwang:添加属性控制不可选日期的时间段==============

        #region DisplayDateRange

        /// <summary>
        /// 控制不可选日期的开始时间
        /// </summary>
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime? DisplayDateRangeStart
        {
            get { return (DateTime?)GetValue(DisplayDateRangeStartProperty); }
            set { SetValue(DisplayDateRangeStartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayDateRangeStart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayDateRangeStartProperty =
            DependencyProperty.Register("DisplayDateRangeStart", typeof(DateTime?), typeof(AUIDatePicker), new PropertyMetadata(null));

        /// <summary>
        /// 控制不可选日期的结束时间
        /// </summary>
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime? DisplayDateRangeEnd
        {
            get { return (DateTime?)GetValue(DisplayDateRangeEndProperty); }
            set { SetValue(DisplayDateRangeEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayDateRangeEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayDateRangeEndProperty =
            DependencyProperty.Register("DisplayDateRangeEnd", typeof(DateTime?), typeof(AUIDatePicker), new PropertyMetadata(null));

        /// <summary>
        /// 根据DisplayDateRange属性设定calendar的状态
        /// </summary>
        private void SetDisplayDateRangeStatus()
        {
            if (null != this._startTimeCalendar)
            {
                this._startTimeCalendar.DisplayDateStart = DisplayDateRangeStart;
                this._startTimeCalendar.DisplayDateEnd = DisplayDateRangeEnd;
            }
            if (null != this._endTimeCalendar)
            {
                this._endTimeCalendar.DisplayDateEnd = DisplayDateRangeEnd;
                this._endTimeCalendar.DisplayDateStart = DisplayDateRangeStart;
            }
        }

        #endregion DisplayDateRange

        //==========================================End===================================

        #endregion Properties

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <example>
        /// <code>
        /// new AUIDatePicker()
        /// {
        ///     IsDateRangePicker = true;
        /// }
        /// </code>
        /// </example>
        public AUIDatePicker()
        {
            this.Initialized = false;
            InitializeStartTimeCalendar();
            Loaded += new RoutedEventHandler(AUIDatePicker_Loaded);
            DefaultStyleKey = typeof(AUIDatePicker);
        }

        void AUIDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            updateTexts();
        }

        #region Render
        /// <summary>
        /// 重新展现DatePicker
        /// </summary>
        private void ReSet()
        {
            this.InitializeStartTimeCalendar();
            this.InitializeEndTimeCalendar();
            this.OnApplyTemplate();
            this.SelectedStartDateTime = null;
            this.SelectedEndDateTime = null;
        }
        #endregion Render

        #region OnApplyTemplate
        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ResetWidth();
            PrepareText();
            PreparePopup();
            PrepareDropDownButton();
            if (null != this._dropDownButton)
            {
                this._dropDownButton.Click -= new RoutedEventHandler(DropDownButton_Click);
            }
            this._dropDownButton = GetTemplateChild("Button") as ImageButton;
            mTextArea = GetTemplateChild("TextArea") as Grid;
            this.mErrorMsgTB = GetTemplateChild("ErrorMsgTB") as TextBlock;
            this._dropDownButton.Click += new RoutedEventHandler(DropDownButton_Click);
            ChangeCalendarInfoByCulture(new CultureInfo(null == this.Culture ? "en-US" : this.Culture));
            this.Initialized = true;
        }

        private void ResetWidth()
        {
            if (this.Width.Equals(double.NaN))
            {
                if (this.IsDateRangePicker)
                {
                    if (this.HasTimeZone)
                    {
                        if (this.HasTimePicker)
                        {
                            this.Width = 457;
                        }
                        else
                        {
                            this.Width = 338;
                        }
                    }
                    else
                    {
                        if (this.HasTimePicker)
                        {
                            this.Width = 296;
                        }
                        else
                        {
                            this.Width = 200;
                        }
                    }
                }
                else
                {
                    if (this.HasTimeZone && this.HasTimePicker)
                    {
                        this.Width = 238;
                    }
                    else
                    {
                        this.Width = 200;
                    }
                }
            }
        }

        /// <summary>
        /// 弹出popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = !this.IsDropDownOpen;
        }

        /// <summary>
        /// 生成popup
        /// </summary>
        private void PreparePopup()
        {
            if (_popUp != null)
            {
                _popUp.Child = null;
            }

            _popUp = GetTemplateChild(ElementPopup) as Popup;
            if (_popUp != null)
            {
                if (null != _outsidePopupCanvas)
                {
                    _outsidePopupCanvas.MouseLeftButtonDown -= new MouseButtonEventHandler(OutsidePopupCanvas_MouseLeftButtonDown);
                }

                _outsideCanvas = new Canvas();
                _outsidePopupCanvas = new Canvas();

                _outsideCanvas.Background = new SolidColorBrush(Colors.White);
                _outsideCanvas.Opacity = 0;
                PrepareOutsideStackPanel();
                _outsideBorder = new Border();
                _outsideBorder.Padding = new Thickness(1);
                _outsideBorder.Background = new SolidColorBrush(Colors.White);
                _outsideBorder.CornerRadius = new CornerRadius(2);
                _outsideBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 79, 139, 185));
                _outsideBorder.BorderThickness = new Thickness(1);
                _outsideBorder.Child = _outsideStackPanel;

                _outsideCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(OutsidePopupCanvas_MouseLeftButtonDown);

                _outsidePopupCanvas.Children.Add(this._outsideCanvas);
                _outsidePopupCanvas.Children.Add(_outsideBorder);
                _popUp.Child = this._outsidePopupCanvas;
                //_popUp.Opened += new EventHandler(_popUp_Opened);
                _root = GetTemplateChild(ElementRoot) as FrameworkElement;

                if (this.IsDropDownOpen)
                {
                    this.OpenPopUp();
                }
            }
        }

        /// <summary>
        /// 当popup显示时,重置其位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _popUp_Opened(object sender, EventArgs e)
        {
            this.SetPopUpPosition();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrepareDropDownButton()
        {
            _dropDownButton = GetTemplateChild(ElementButton) as ImageButton;
            if (_dropDownButton != null)
            {
                _dropDownButton.IsTabStop = true;
            }
        }

        /// <summary>
        /// 生成文本框
        /// </summary>
        private void PrepareText()
        {
            if (_startTimeTextBox != null)
            {
                //_startTimeTextBox.KeyDown -= new KeyEventHandler(TextBox_KeyDown);
            }
            _startTimeTextBox = GetTemplateChild(ElementStartTimeTextBox) as TextBox;
            if (_startTimeTextBox != null)
            {
                //_startTimeTextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            }

            TextBlock toTB = GetTemplateChild("ToTextBox") as TextBlock;
            if (this.IsDateRangePicker)
            {
                if (_endTimeTextBox != null)
                {
                    //_endTimeTextBox.KeyDown -= new KeyEventHandler(TextBox_KeyDown);
                }
                _endTimeTextBox = GetTemplateChild(ElementEndTimeTextBox) as TextBox;

                if (null != toTB && null != _endTimeTextBox)
                {
                    toTB.Visibility = Visibility.Visible;
                    _endTimeTextBox.Visibility = Visibility.Visible;
                }
                if (null != _endTimeTextBox)
                {
                    //_endTimeTextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
                }
                ColumnDefinition cd = GetTemplateChild("EndTimeTextBoxColume") as ColumnDefinition;
                if (null != cd)
                {
                    cd.Width = new GridLength(1, GridUnitType.Star);
                }
            }
            else
            {
                _endTimeTextBox = GetTemplateChild(ElementEndTimeTextBox) as TextBox;
                ColumnDefinition cd = GetTemplateChild("EndTimeTextBoxColume") as ColumnDefinition;
                if (null != toTB)
                {
                    toTB.Visibility = Visibility.Collapsed;
                    _endTimeTextBox.Visibility = Visibility.Collapsed;
                }
                if (null != cd)
                {
                    cd.Width = GridLength.Auto;
                }
            }
        }

        /// <summary>
        /// 初始化弹出窗口内的控件
        /// </summary>
        private void PrepareOutsideStackPanel()
        {
            _outsideStackPanel = new StackPanel();
            _outsideStackPanel.SizeChanged -= new SizeChangedEventHandler(_outsideStackPanel_SizeChanged);
            _outsideStackPanel.SizeChanged += new SizeChangedEventHandler(_outsideStackPanel_SizeChanged);
            //_outsideStackPanelBorder.Padding = new Thickness(1);
            _outsideStackPanel.Orientation = Orientation.Vertical;
            _outsideStackPanel.Background = new SolidColorBrush(Colors.White);

            PrepareCalendarAreaSP();

            if (null != this._calendarAreaSP)
            {
                _outsideStackPanel.Children.Add(this._calendarAreaSP);
            }

            if (this.HasTimePicker)
            {
                PrepareTimePickers();
                _outsideStackPanel.Children.Add(this._timePickerGrid);
            }

            if (this.HasTimeZone)
            {
                this.PrepareTimeZone();
                if (_TimeZoneSP != null)
                {
                    _outsideStackPanel.Children.Add(this._TimeZoneSP);
                }
            }
            //==================== Add By CTH [ADO-7986] ======================

            PreparePromptInfo();
            _outsideStackPanel.Children.Add(_PromptInfoSP);

            //========================= End ====================================
            if (this.IsDateRangePicker || this.HasTimePicker || this.HasTimeZone)
            {
                this.PrepareButtonSP();
                if (null != this._buttonSP)
                {
                    if (this.HasTimePicker)
                    {
                        if (this.IsDateRangePicker)
                        {
                            Grid.SetColumn(this._buttonSP, 1);
                        }
                    }
                    _outsideStackPanel.Children.Add(this._buttonSP);
                }
            }
        }

        void _outsideStackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StackPanel source = sender as StackPanel;
            if (source != null && source.ActualHeight > 0)
            {
                this.SetPopUpPosition();
            }
        }

        /// <summary>
        /// 初始化timepicker
        /// </summary>
        private void PrepareTimePickers()
        {
            if (!this.HasTimePicker)
            {
                return;
            }
            this._timePickerGrid = new Grid();
            ColumnDefinition cl1 = new ColumnDefinition();
            this._timePickerGrid.ColumnDefinitions.Add(cl1);
            if (null != this._startTimeTimePicker)
            {
                this._startTimeTimePicker.KeyDown -= new KeyEventHandler(PopUp_KeyDown);
                this._startTimeTimePicker.SelectedTimeChanged -= new EventHandler<SelectionChangedEventArgs>(StartTimePickerValueChanged);
            }
            this._startTimeTimePicker = new AUITimePicker();
            this._startTimeTimePicker.KeyDown += new KeyEventHandler(PopUp_KeyDown);
            this._startTimeTimePicker.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this._startTimeTimePicker.SetValue(Grid.ColumnProperty, 0);
            this._timePickerGrid.Children.Add(this._startTimeTimePicker);
            this._startTimeTimePicker.SelectedTimeChanged += new EventHandler<SelectionChangedEventArgs>(StartTimePickerValueChanged);
            if (this.IsDateRangePicker)
            {
                ColumnDefinition cl2 = new ColumnDefinition();
                this._timePickerGrid.ColumnDefinitions.Add(cl2);
                if (null != this._endTimeTimePicker)
                {
                    this._endTimeTimePicker.KeyDown -= new KeyEventHandler(PopUp_KeyDown);
                    this._endTimeTimePicker.SelectedTimeChanged -= new EventHandler<SelectionChangedEventArgs>(EndTimePickerValueChanged);
                }
                this._endTimeTimePicker = new AUITimePicker();
                this._endTimeTimePicker.KeyDown += new KeyEventHandler(PopUp_KeyDown);
                this._endTimeTimePicker.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                this._endTimeTimePicker.SetValue(Grid.ColumnProperty, 1);
                this._timePickerGrid.Children.Add(this._endTimeTimePicker);
                this._endTimeTimePicker.SelectedTimeChanged += new EventHandler<SelectionChangedEventArgs>(EndTimePickerValueChanged);
            }
        }

        private bool SelectedTheSameDate()
        {
            return this.IsDateRangePicker
                && null != this.SelectedStartDateTime
                && this.SelectedStartDateTime.HasValue
                && null != this.SelectedEndDateTime
                && this.SelectedEndDateTime.HasValue
                && DateTimeHelper.CompareDate(this.SelectedStartDateTime.Value, this.SelectedEndDateTime.Value) == 0;
        }

        private CheckBox _DaylightSavingTimeCB;

        private void PrepareTimeZone()
        {
            _TimeZoneSP = new StackPanel();
            _DaylightSavingTimeCB = new CheckBox();
            TextBlock tb = new TextBlock() { Text = I18NEntity.Get("Common.GuiControls", "Automatically adjust clock for Daylight Saving Time"), TextWrapping = TextWrapping.Wrap };
            _DaylightSavingTimeCB.Content = tb;
            _DaylightSavingTimeCB.IsChecked = AUITimeZone.IsUsingDaylightSavingTime();

            //fix[ADO-22817] 显示即check  start
            _DaylightSavingTimeCB.Checked -= new RoutedEventHandler(_DaylightSavingTimeCB_Checked);
            _DaylightSavingTimeCB.Checked += new RoutedEventHandler(_DaylightSavingTimeCB_Checked);
            _DaylightSavingTimeCB.Unchecked -= new RoutedEventHandler(_DaylightSavingTimeCB_Checked);
            _DaylightSavingTimeCB.Unchecked += new RoutedEventHandler(_DaylightSavingTimeCB_Checked);
            //fix[ADO-22817] 显示即check  end

            _TimeZoneCB = new ComboBox();
            _TimeZoneCB.SelectionChanged -= new SelectionChangedEventHandler(_TimeZoneCB_SelectionChanged);
            _TimeZoneCB.SelectionChanged += new SelectionChangedEventHandler(_TimeZoneCB_SelectionChanged);
            _TimeZoneCB.DisplayMemberPath = "DisplayName";
            List<AUITimeZone> allZones = AUITimeZone.GetAllTimeZones();
            _TimeZoneCB.ItemsSource = allZones;
            _TimeZoneCB.SelectedItem = this.TimeZone;
            Grid g = new Grid() { Width = this.IsDateRangePicker ? 350 : 175, Margin = new Thickness(0, 3, 0, 3) };
            g.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            g.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            g.Children.Add(new TextBlock() { Text = I18NEntity.Get("Common.GuiControls", "Time Zone:"), VerticalAlignment = System.Windows.VerticalAlignment.Center });
            Grid.SetColumn(_TimeZoneCB, 1);
            Grid.SetRow(_DaylightSavingTimeCB, 1);
            Grid.SetColumnSpan(_DaylightSavingTimeCB, 2);
            g.Children.Add(_TimeZoneCB);
            g.Children.Add(_DaylightSavingTimeCB);
            _TimeZoneSP.Children.Add(g);
        }

        //fix[ADO-22817] 显示即check  start
        void _DaylightSavingTimeCB_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox current = sender as CheckBox;

            if (current.IsChecked == true)
            {
                HasDaylightSavingTimeCBChecked = true;
            }
            else
            {
                HasDaylightSavingTimeCBChecked = false;
            }
        }
        //fix[ADO-22817] 显示即check  end

        //======================== Add By CTH [ADO-7986] ===============================
        private void PreparePromptInfo()
        {
            _PromptInfoSP = new StackPanel() { Orientation = Orientation.Horizontal, Visibility = Visibility.Collapsed };
            _PromptInfoImage = new Image() { Source = new BitmapImage(new Uri("/Image/Common/error_16x16.png", UriKind.RelativeOrAbsolute)), Height = 16, Width = 16, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 3, 0, 3) };
            _PromptInfoTB = new TextBlock() { Text = I18NEntity.Get("Common.GuiControls", "The time range is invalid. The finish time cannot be earlier than the start time."), Width = this.IsDateRangePicker ? 350 : 175, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5, 3, 0, 3), Foreground = new SolidColorBrush(Colors.Red), VerticalAlignment = VerticalAlignment.Center };
            _PromptInfoSP.Children.Add(_PromptInfoImage);
            _PromptInfoSP.Children.Add(_PromptInfoTB);
        }
        //================================= End ========================================

        void _TimeZoneCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox source = sender as ComboBox;
            if (source != null)
            {
                ToolTipService.SetToolTip(source, ((AUITimeZone)source.SelectedItem).DisplayName);
                AUITimeZone timeZone = (AUITimeZone)source.SelectedItem;
                this._DaylightSavingTimeCB.Visibility = timeZone.SupportsDaylightSavingTime ? Visibility.Visible : Visibility.Collapsed;

                //fix[ADO-22817] 显示即check  start
                if (this._DaylightSavingTimeCB.Visibility == Visibility.Visible)
                {
                    this._DaylightSavingTimeCB.IsChecked = true;
                }
                else
                {
                    this._DaylightSavingTimeCB.IsChecked = false;
                    this._DaylightSavingTimeCB.IsChecked = timeZone.Equals(AUITimeZone.GetCurrentTimeZone()) ? true : false;
                }
                //fix[ADO-22817] 显示即check  end

            }
        }
        /// <summary>
        /// 初始化按钮区域
        /// </summary>
        private void PrepareButtonSP()
        {
            if (this.IsDateRangePicker || this.HasTimePicker || this.HasTimeZone)
            {
                this._buttonSP = new SubmitPanel();
                if (null != _okButton)
                {
                    _okButton.Click -= new RoutedEventHandler(OnOKButtonClicked);
                }
                _okButton = new Button()
                {
                    Width = 45,
                    Height = 20,
                    Content = I18NEntity.Get("Common.GuiControls", "OK"),
                    Margin = new Thickness(5, 5, 0, 5),
                    Padding = new Thickness(0)
                };
                _okButton.Click += new RoutedEventHandler(OnOKButtonClicked);
                HyperlinkButton cancelButton = new HyperlinkButton()
                {
                    Width = 45,
                    Height = 20,
                    Content = I18NEntity.Get("Common.GuiControls", "Cancel"),
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(0)
                };
                cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
                cancelButton.Margin = new Thickness(5, 0, 0, 0);
                _buttonSP.Items.Add(_okButton);
                _buttonSP.Items.Add(cancelButton);
            }
        }

        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// 初始化calenar区域
        /// </summary>
        private void PrepareCalendarAreaSP()
        {
            if (null != _calendarAreaSP)
            {
                _calendarAreaSP.KeyDown -= new KeyEventHandler(this.PopUp_KeyDown);
            }
            _calendarAreaSP = new StackPanel();
            _calendarAreaSP.Orientation = Orientation.Horizontal;
            _calendarAreaSP.KeyDown += new KeyEventHandler(this.PopUp_KeyDown);
            PrepareCalendarLeftSP();
            _calendarAreaSP.Children.Add(_calendarLeftSP);
            if (this.IsDateRangePicker)
            {
                PrepareCalendarRightSP();
                if (null != _calendarRightSP)
                {
                    _calendarAreaSP.Children.Add(_calendarRightSP);
                }
            }
        }

        /// <summary>
        /// 初始化开始时间calendar
        /// </summary>
        private void PrepareCalendarLeftSP()
        {
            _calendarLeftSP = new StackPanel();
            if (this.IsDateRangePicker)
            {
                TextBlock t = new TextBlock();
                t.Text = I18NEntity.Get("Common.GuiControls", "From");
                t.Padding = new Thickness(5);
                _calendarLeftSP.Children.Add(t);
            }
            _calendarLeftSP.Children.Add(this._startTimeCalendar);
        }

        /// <summary>
        /// 初始化结束时间calendar
        /// </summary>
        private void PrepareCalendarRightSP()
        {
            if (this.IsDateRangePicker)
            {
                if (null == this._endTimeCalendar)
                {
                    this.InitializeEndTimeCalendar();
                }
                _calendarRightSP = new StackPanel();
                TextBlock t = new TextBlock();
                t.Text = I18NEntity.Get("Common.GuiControls", "To");
                t.Padding = new Thickness(5);
                _calendarRightSP.Children.Add(t);
                _calendarRightSP.Children.Add(this._endTimeCalendar);
            }
        }

        #endregion Render

        #region Eventhandlers

        /// <summary>
        /// popup上点击esc时关闭popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PopUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Initialized && e.Key == Key.Escape && this.IsDropDownOpen)
            {
                this.IsDropDownOpen = false;
            }
        }

        /// <summary>
        /// 当遮罩被点击时,关闭popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutsidePopupCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// ok按钮点击之后,根据用户选择的时间更改文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOKButtonClicked(object sender, RoutedEventArgs e)
        {
            if (this.IsDateRangePicker)
            {
                DateTime? leftD = this._startTimeCalendar.SelectedDate;
                DateTime? rightD = this._endTimeCalendar.SelectedDate;
                //if (leftD.HasValue && rightD.HasValue)
                //{
                if (true)
                {
                    if (!leftD.HasValue)
                    {
                        leftD = DateTime.Today;
                    }
                    if (!rightD.HasValue)
                    {
                        rightD = DateTime.Today;
                    }
                    if (this.HasTimePicker)
                    {
                        DateTime sd = leftD.Value;
                        DateTime ed = rightD.Value;
                        DateTime st = null != this._startTimeTimePicker ? this._startTimeTimePicker.SelectedTime : DateTime.Today;
                        DateTime et = null != this._endTimeTimePicker ? this._endTimeTimePicker.SelectedTime : DateTime.Today;
                        this.SelectedStartDateTime = new DateTime(sd.Year, sd.Month, sd.Day, st.Hour, st.Minute, st.Second);
                        this.SelectedEndDateTime = new DateTime(ed.Year, ed.Month, ed.Day, et.Hour, et.Minute, et.Second);
                    }
                    else
                    {
                        if (this.OnlyFuture && DateTime.Compare(leftD.Value, DateTime.Now) < 0)
                        {
                            this.SelectedStartDateTime = DateTime.Now;
                            if (DateTime.Compare(rightD.Value, DateTime.Now) < 0)
                            {
                                this.SelectedEndDateTime = DateTime.Now;
                            }
                            else
                            {
                                this.SelectedEndDateTime = rightD.Value;
                            }
                        }
                        else if (this.OnlyPast && DateTime.Compare(leftD.Value, DateTime.Now) > 0)
                        {
                            this.SelectedStartDateTime = DateTime.Now;
                            if (DateTime.Compare(rightD.Value, DateTime.Now) > 0)
                            {
                                this.SelectedEndDateTime = DateTime.Now;
                            }
                            else
                            {
                                this.SelectedEndDateTime = rightD.Value;
                            }
                        }
                        else
                        {
                            this.SelectedStartDateTime = leftD.Value;
                            this.SelectedEndDateTime = rightD.Value;
                        }
                    }
                    this.IsDropDownOpen = false;
                    this._dropDownButton.Focus();
                }
            }
            else
            {
                DateTime? leftD = this._startTimeCalendar.SelectedDate;
                if (leftD.HasValue)
                {
                    DateTime sd = leftD.Value;
                    DateTime st = null != this._startTimeTimePicker ? this._startTimeTimePicker.SelectedTime : DateTime.Today;
                    DateTime d = DateTimeHelper.FixDateAndTime(sd, st);
                    if (this.OnlyFuture && DateTime.Compare(d, DateTime.Now) < 0)
                    {
                        this.SelectedStartDateTime = DateTime.Now;
                    }
                    else if (this.OnlyPast && DateTime.Compare(d, DateTime.Now) > 0)
                    {
                        this.SelectedStartDateTime = DateTime.Now;
                    }
                    else
                    {
                        this.SelectedStartDateTime = d;
                    }
                    this.IsDropDownOpen = false;
                    this._dropDownButton.Focus();
                }
                else
                {
                    DateTime st = null != this._startTimeTimePicker ? this._startTimeTimePicker.SelectedTime : DateTime.Today;
                    DateTime d = DateTimeHelper.FixDateAndTime(DateTime.Now, st);
                    this.SelectedStartDateTime = d;
                    this.IsDropDownOpen = false;
                    this._dropDownButton.Focus();
                }
            }
            if (this.HasTimeZone)
            {
                this.TimeZone = this._TimeZoneCB.SelectedItem as AUITimeZone;
                this.AutoAdjustClockForDaylightSavingChanges = this._DaylightSavingTimeCB.IsChecked;
            }
            this.updateTexts();
            if (this.SelectOKClicked != null)
            {
                SelectOKClicked(this, e);
            }
        }

        /// <summary>
        /// 开始时间calendar上触发按钮事件时,根据按钮决定是否关闭popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _startTimeCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.IsDateRangePicker && !this.HasTimePicker && !this.HasTimeZone
                && this.IsDropDownOpen && (e.Key == Key.Space || e.Key == Key.Enter)
                && this._startTimeCalendar.SelectedDate.HasValue)
            {
                this.IsDropDownOpen = false;
            }
            if (e.Key == Key.Escape && this.IsDropDownOpen)
            {
                this.IsDropDownOpen = false;
            }
        }

        /// <summary>
        /// 开始时间calendar鼠标左键点击时,若是最简单的datepicker则关闭popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _startTimeCalendar_DayButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsDateRangePicker && !this.HasTimePicker && !this.HasTimeZone && this.IsDropDownOpen)
            {
                this.IsDropDownOpen = false;
            }
        }

        /// <summary>
        /// 当开始时间timepicker变化时,检查用户选择的时间是否符合要求并重置ok按钮的可用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTimePickerValueChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? addedDateTime = e.AddedItems[0] as DateTime?;
            DateTime? removedDateTime = e.RemovedItems[0] as DateTime?;
            if (addedDateTime != removedDateTime && addedDateTime.HasValue)
            {
                this.CheckOKButtonState();
            }
        }

        /// <summary>
        /// 当结束时间timepicker变化时,检查用户选择的时间是否符合要求并重置ok按钮的可用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndTimePickerValueChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? addedDateTime = e.AddedItems[0] as DateTime?;
            DateTime? removedDateTime = e.RemovedItems[0] as DateTime?;
            if (addedDateTime != removedDateTime && addedDateTime.HasValue)
            {
                this.CheckOKButtonState();
            }
        }

        /// <summary>
        /// 开始时间变化时触发的方法
        /// </summary>
        private void StartTimeCalendarSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsDateRangePicker && !this.HasTimePicker && !this.HasTimeZone)
            {
                if (e.AddedItems.Count > 0)
                {
                    DateTime? d = e.AddedItems[0] as DateTime?;
                    if (this.OnlyFuture && d.HasValue && DateTime.Compare(d.Value, DateTime.Now) < 0)
                    {
                        this.SelectedStartDateTime = DateTime.Now;
                    }
                    if (this.OnlyPast && d.HasValue && DateTime.Compare(d.Value, DateTime.Now) > 0)
                    {
                        this.SelectedStartDateTime = DateTime.Now;
                    }
                    else
                    {
                        this.SelectedStartDateTime = d;
                    }
                }
            }
            else if (this.Initialized)
            {
                CheckOKButtonState();
            }
        }

        /// <summary>
        /// 结束时间变化时触发的方法
        /// </summary>
        private void EndTimeCalendarSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Initialized)
            {
                CheckOKButtonState();
            }
        }


        /// <summary>
        /// 检查用户选择的时间是否符合要求并重置ok按钮的可用状态
        /// </summary>
        private void CheckOKButtonState()
        {
            if (null == this._okButton)
            {
                return;
            }
            if (this.IsDateRangePicker && null != this._endTimeCalendar)
            {
                DateTime? leftD = this._startTimeCalendar.SelectedDate;
                DateTime? rightD = this._endTimeCalendar.SelectedDate;
                if (!leftD.HasValue)
                {
                    leftD = DateTime.Today;
                }
                if (!rightD.HasValue)
                {
                    rightD = DateTime.Today;
                }
                DateTime leftT = null != this._startTimeTimePicker ? this._startTimeTimePicker.SelectedTime : DateTime.Today;
                DateTime rightT = null != this._endTimeTimePicker ? this._endTimeTimePicker.SelectedTime : DateTime.Today;
                if (this.HasTimePicker)
                {
                    this._okButton.IsEnabled = null != leftD
                                                && leftD.HasValue
                                                && null != rightD
                                                && rightD.HasValue
                                                && DateTime.Compare(DateTimeHelper.FixDateAndTime(leftD.Value, leftT), DateTimeHelper.FixDateAndTime(rightD.Value, rightT)) <= 0
                                                && (!this.OnlyFuture || DateTime.Compare(DateTimeHelper.FixDateAndTime(leftD.Value, leftT), DateTime.Now) >= 0)
                                                && (!this.OnlyPast || DateTime.Compare(DateTimeHelper.FixDateAndTime(leftD.Value, leftT), DateTime.Now) <= 0);

                    //============================== Add By CTH [ADO-7986] =========================
                    if (_PromptInfoSP != null)
                    {
                        _PromptInfoSP.Visibility = this._okButton.IsEnabled ? Visibility.Collapsed : Visibility.Visible;
                    }
                    //============================= End ============================================
                }
                else
                {
                    this._okButton.IsEnabled = null != leftD
                                                && leftD.HasValue
                                                && null != rightD
                                                && rightD.HasValue
                                                && DateTime.Compare(leftD.Value, rightD.Value) <= 0;
                    //=========================== Add By CTH [ADO-7986] ==========================
                    if (_PromptInfoSP != null)
                    {
                        _PromptInfoSP.Visibility = this._okButton.IsEnabled ? Visibility.Collapsed : Visibility.Visible;
                    }
                    //================================== End =====================================
                }

            }
            else
            {
                DateTime? leftD = this._startTimeCalendar.SelectedDate;
                bool enabled = null != leftD && leftD.HasValue && (!this.OnlyFuture || DateTime.Compare(DateTimeHelper.FixDateAndTime(leftD.Value, null != this._startTimeTimePicker ? this._startTimeTimePicker.SelectedTime : DateTime.Today), DateTime.Now) >= 0);
                this._okButton.IsEnabled = true;
            }
        }

        #endregion Eventhandlers

        #region Initialize
        /// <summary>
        /// 初始化开始时间的calendar
        /// </summary>
        private void InitializeStartTimeCalendar()
        {
            if (null != _startTimeCalendar)
            {
                _startTimeCalendar.SelectedDatesChanged -= new EventHandler<SelectionChangedEventArgs>(StartTimeCalendarSelectedChanged);
                _startTimeCalendar.DayButtonMouseUp -= new MouseButtonEventHandler(_startTimeCalendar_DayButtonMouseUp);
                _startTimeCalendar.KeyDown -= new KeyEventHandler(_startTimeCalendar_KeyDown);
            }
            _startTimeCalendar = new AUICalendar();
            //_startTimeCalendar.AniType = 1;
            _startTimeCalendar.SelectionMode = CalendarSelectionMode.SingleDate;
            //_startTimeCalendar.SizeChanged += new SizeChangedEventHandler(Calendar_SizeChanged);
            _startTimeCalendar.IsTabStop = true;
            //_startTimeCalendar.DayButtonMouseUp += new MouseButtonEventHandler(Calendar_DayButtonMouseUp);
            //_startTimeCalendar.DisplayDateChanged += new EventHandler<GlobalCalendarDateChangedEventArgs>(Calendar_LeftDisplayDateChanged);
            _startTimeCalendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>(StartTimeCalendarSelectedChanged);

            //_startTimeCalendar.MouseLeftButtonDown += new MouseButtonEventHandler(Calendar_MouseLeftButtonDown);
            //_startTimeCalendar.KeyDown += new KeyEventHandler(CalendarLeft_KeyDown);
            _startTimeCalendar.DayButtonMouseUp += new MouseButtonEventHandler(_startTimeCalendar_DayButtonMouseUp);
            _startTimeCalendar.KeyDown += new KeyEventHandler(_startTimeCalendar_KeyDown);
        }


        /// <summary>
        /// 初始化结束时间的calendar
        /// </summary>
        private void InitializeEndTimeCalendar()
        {
            if (this.IsDateRangePicker)
            {
                if (null != _endTimeCalendar)
                {
                    _endTimeCalendar.SelectedDatesChanged -= new EventHandler<SelectionChangedEventArgs>(EndTimeCalendarSelectedChanged);
                }
                _endTimeCalendar = new AUICalendar();
                //_endTimeCalendar.AniType = 1;
                _endTimeCalendar.SelectionMode = CalendarSelectionMode.SingleDate;
                //_startTimeCalendar.SizeChanged += new SizeChangedEventHandler(Calendar_SizeChanged);
                _endTimeCalendar.IsTabStop = true;
                //_endTimeCalendar.DayButtonMouseUp += new MouseButtonEventHandler(Calendar_DayButtonMouseUp);
                //_endTimeCalendar.DisplayDateChanged += new EventHandler<GlobalCalendarDateChangedEventArgs>(Calendar_RightDisplayDateChanged);
                _endTimeCalendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>(EndTimeCalendarSelectedChanged);
                //_endTimeCalendar.MouseLeftButtonDown += new MouseButtonEventHandler(Calendar_MouseLeftButtonDown);
                //_endTimeCalendar.KeyDown += new KeyEventHandler(CalendarLeft_KeyDown);
            }
        }

        #endregion Initialize

        #region Popup
        /// <summary>
        /// 将popup控件展现出来
        /// </summary>
        private void OpenPopUp()
        {
            this._popUp.IsOpen = true;
            if (null != this._startTimeCalendar)
            {
                this._startTimeCalendar.Focus();
            }
        }

        /// <summary>
        /// 关闭popup
        /// </summary>
        private void ClosePopUp()
        {
            if (null != this._popUp)
            {
                this._popUp.IsOpen = false;
                this._dropDownButton.Focus();
            }
        }
        /// <summary>
        /// 重置弹出窗口的位置
        /// </summary>
        private void SetPopUpPosition()
        {
            return;
            if (this._calendarLeftSP != null && Application.Current != null)
            {
                double pageHeight = this.ActualHeight;
                double pageWidth = this.ActualWidth;
                double outsideStackPanelHeight = this._outsideStackPanel.ActualHeight;
                double outsideStackPanelWidth = this._outsideStackPanel.ActualWidth;
                double actualHeight = this.mTextArea.ActualHeight;

                if (this.mTextArea != null)
                {
                    GeneralTransform gt = this.mTextArea.TransformToVisual(null);

                    if (gt != null)
                    {
                        Point point00 = new Point(0, 0);
                        Point point10 = new Point(1, 0);
                        Point point01 = new Point(0, 1);
                        Point transform00 = gt.Transform(point00);
                        Point transform10 = gt.Transform(point10);
                        Point transform01 = gt.Transform(point01);

                        double dpX = transform00.X;
                        double dpY = transform00.Y;

                        double outsideStackPanelX = dpX;
                        double outsideStackPanelY = dpY + actualHeight;

                        // if the page height is less then the total height of
                        // the PopUp + DatePicker or if we can fit the PopUp
                        // inside the page, we want to place the PopUp to the
                        // bottom
                        if (pageHeight - dpY < actualHeight + outsideStackPanelHeight)
                        {
                            if (pageHeight <= outsideStackPanelHeight)
                            {
                                outsideStackPanelY = 0;
                            }
                            else
                            {
                                outsideStackPanelY = outsideStackPanelY - outsideStackPanelHeight - actualHeight - 5;
                                outsideStackPanelY = Math.Max(outsideStackPanelY, 0);
                            }
                        }
                        /*
                         * 如果控件横向超出页面宽度
                         * */
                        if (pageWidth < outsideStackPanelX + outsideStackPanelWidth)
                        {
                            if (pageWidth <= outsideStackPanelWidth)
                            {
                                outsideStackPanelX = 0;
                            }
                            else
                            {
                                outsideStackPanelX = pageWidth - outsideStackPanelWidth - 5;
                                outsideStackPanelX = Math.Max(outsideStackPanelX, 0);
                            }
                        }
                        this._popUp.HorizontalOffset = 0;
                        this._popUp.VerticalOffset = 0;
                        this._outsidePopupCanvas.Width = pageWidth;
                        this._outsidePopupCanvas.Height = pageHeight;
                        this._outsideCanvas.Width = pageWidth;
                        this._outsideCanvas.Height = pageHeight;
                        this._calendarLeftSP.HorizontalAlignment = HorizontalAlignment.Left;
                        this._calendarLeftSP.VerticalAlignment = VerticalAlignment.Top;
                        Canvas.SetLeft(this._outsideBorder, outsideStackPanelX);
                        Canvas.SetTop(this._outsideBorder, outsideStackPanelY);

                        // Transform the invisible canvas to plugin coordinate
                        // space origin
                        Matrix transformToRootMatrix = Matrix.Identity;
                        transformToRootMatrix.M11 = transform10.X - transform00.X;
                        transformToRootMatrix.M12 = transform10.Y - transform00.Y;
                        transformToRootMatrix.M21 = transform01.X - transform00.X;
                        transformToRootMatrix.M22 = transform01.Y - transform00.Y;
                        transformToRootMatrix.OffsetX = transform00.X;
                        transformToRootMatrix.OffsetY = transform00.Y;
                        MatrixTransform mt = new MatrixTransform();
                        InvertMatrix(ref transformToRootMatrix);
                        mt.Matrix = transformToRootMatrix;
                        this._outsidePopupCanvas.RenderTransform = mt;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the matrix to its inverse.
        /// </summary>
        /// <param name="matrix">Matrix to be inverted.</param>
        /// <returns>
        /// True if the Matrix is invertible, false otherwise.
        /// </returns>
        private static bool InvertMatrix(ref Matrix matrix)
        {
            double determinant = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;

            if (determinant == 0.0)
            {
                return false;
            }

            Matrix matCopy = matrix;
            matrix.M11 = matCopy.M22 / determinant;
            matrix.M12 = -1 * matCopy.M12 / determinant;
            matrix.M21 = -1 * matCopy.M21 / determinant;
            matrix.M22 = matCopy.M11 / determinant;
            matrix.OffsetX = (matCopy.OffsetY * matCopy.M21 - matCopy.OffsetX * matCopy.M22) / determinant;
            matrix.OffsetY = (matCopy.OffsetX * matCopy.M12 - matCopy.OffsetY * matCopy.M11) / determinant;

            return true;
        }
        #endregion Popup

        #region OtherTools
        /// <summary>
        /// 根据当前的时间格式将datetime对象转化为string
        /// </summary>
        /// <param name="d">要转化的datepicker对象</param>
        /// <returns>Inherited code: Requires comment 2.</returns>
        private string DateTimeToString(DateTime d)
        {
            DateTimeFormatInfo dtfi = DateTimeHelper.GetCurrentDateFormat();

            string dateTimeFormat = string.Empty;

            return string.Format(CultureInfo.CurrentCulture, d.ToString(dtfi.ShortDatePattern, dtfi));
        }

        /// <summary>
        /// 根据culture更改当前文本框内的内容
        /// </summary>
        /// <param name="culture"></param>
        private void ChangeTextsByCulture(CultureInfo culture)
        {
            string startTimeString = string.Empty;
            string endTimeString = string.Empty;
            if (null != this.SelectedStartDateTime && this.SelectedStartDateTime.HasValue)
            {
                DateTime startTime = this.SelectedStartDateTime.Value;
                string dateTimeFormat = this.getCurrentDateTimeFormat();
                startTimeString = startTime.ToString(dateTimeFormat, culture);
                if (this.HasTimeZone)
                {
                    startTimeString += " " + TimeZone.Zone;
                }
                if (this.IsDateRangePicker)
                {
                    if (this.SelectedEndDateTime.HasValue)
                    {
                        DateTime endTime = this.SelectedEndDateTime.Value;
                        endTimeString = endTime.ToString(dateTimeFormat, culture);
                        if (this.HasTimeZone)
                        {
                            endTimeString += " " + TimeZone.Zone;
                        }
                    }
                    this.Texts = new string[2] { startTimeString, endTimeString };
                }
                else
                {
                    this.Texts = new string[1] { startTimeString };
                }
            }
        }

        /// <summary>
        /// 取得当前的时间显示格式
        /// </summary>
        /// <returns>当前的时间格式,类型:<see cref="string"/></returns>
        private string getCurrentDateTimeFormat()
        {
            if (string.Empty != this.DateTimeFormat)
            {
                return this.DateTimeFormat;
            }
            //TimePicker
            return this.HasTimePicker ? this._defaultLongFormat : this._defaultShortFormat;
        }

        /// <summary>
        /// 根据culture更改calendar的culture
        /// </summary>
        /// <param name="culture"></param>
        private void ChangeCalendarInfoByCulture(CultureInfo culture)
        {
            //CultureCalendarInfo newCalendarInfo = new CultureCalendarInfo(culture);
            //this._startTimeCalendar.CalendarInfo = newCalendarInfo;
            //if (this.IsDateRangePicker && null != this._endTimeCalendar)
            //{
            //    this._endTimeCalendar.CalendarInfo = newCalendarInfo;
            //}
        }

        /// <summary>
        /// 根据<see cref="AUIDatePicker.Culture"/>属性更新文本框内容
        /// </summary>
        private void updateTexts()
        {
            string cultureString = string.Empty == this.Culture ? "en-US" : this.Culture;
            this.ChangeTextsByCulture(new CultureInfo(cultureString));
        }
        #endregion OtherTools

        private bool mHasError = false;
        public bool HasError
        {
            get
            {
                return mHasError;
            }
            set
            {
                if (mHasError != value)
                {
                    mHasError = value;
                    if (HasErrorChanged != null)
                    {
                        HasErrorChanged(this, new EventArgs());
                    }
                }
            }
        }

        public event EventHandler<EventArgs> HasErrorChanged;

        private string mErrorMsg;

        public string ErrorMsg
        {
            get
            {
                return mErrorMsg;
            }
            set
            {
                if (mErrorMsg != value)
                {
                    mErrorMsg = value;
                    this.HasError = !value.Equals(string.Empty);
                    if (NotifyOnErrorMsgChanged && this.mErrorMsgTB != null)
                    {
                        this.mErrorMsgTB.Text = value;
                        this.mErrorMsgTB.Visibility = this.HasError ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            }
        }

        public event EventHandler<EventArgs> ValidationEvent;

        public bool Validate()
        {
            if (this.ValidationEvent != null)
            {
                this.ValidationEvent(this, new EventArgs());
            }
            return !this.HasError;
        }

        public bool NotifyOnErrorMsgChanged = true;

        public void HideErrorMsg()
        {
            if (this.mErrorMsgTB != null)
            {
                this.mErrorMsgTB.Visibility = Visibility.Collapsed;
            }
        }
    }
}

