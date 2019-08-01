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
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplatePart(Name = AUITimePicker.HourTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = AUITimePicker.MinuteTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = AUITimePicker.SecondTextBox, Type = typeof(TextBox))]
    
    public class AUITimePicker : Control
    {
        /// <summary>
        /// 小时文本框
        /// </summary>
        private const string HourTextBox = "HourTextBox";

        /// <summary>
        /// 分钟文本框
        /// </summary>
        private const string MinuteTextBox = "MinuteTextBox";

        /// <summary>
        /// 秒文本框
        /// </summary>
        private const string SecondTextBox = "SecondTextBox";

        /// <summary>
        /// 小时文本框
        /// </summary>
        private AUITimePickerUpDown _hourTextBox;

        /// <summary>
        /// 分钟文本框
        /// </summary>
        private AUITimePickerUpDown _minuteTextBox;

        /// <summary>
        /// 秒文本框
        /// </summary>
        private AUITimePickerUpDown _secondTextBox;

        /// <summary>
        /// 获取当前时间Add by CTH [ADO-19132]
        /// </summary>
        private DateTime datetime;

        /// <summary>
        /// 标识是否正在更改时间
        /// </summary>
        //private bool ChangingSelected;//just for clear warning

        /// <summary>
        /// 当选择的时间变化时触发的方法
        /// </summary>
        public event Action<DateTime> SelectedTimeChanged;

        /// <summary>
        /// 能够选择的最大时间
        /// </summary>
        public DateTime? MaxTime
        {
            get { return (DateTime?)GetValue(MaxTimeProperty); }
            set { SetValue(MaxTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxTimeProperty =
            DependencyProperty.Register("MaxTime",
            typeof(DateTime?), 
            typeof(AUITimePicker),
            new PropertyMetadata(null, OnMaxTimeChanged));

        private static void OnMaxTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime? newMaxTime = e.NewValue as DateTime?;
            DateTime? oldMaxTime = e.OldValue as DateTime?;

            AUITimePicker tp = d as AUITimePicker;

            if(null != newMaxTime && newMaxTime.HasValue && newMaxTime != oldMaxTime)
            {
                if(DateTimeHelper.CompareTime(tp.SelectedTime,newMaxTime.Value) > 0)
                {
                    tp.SelectedTime = newMaxTime.Value;
                }
            }
        }

        /// <summary>
        /// 能够选择的最小时间
        /// </summary>
        public DateTime? MinTime
        {
            get { return (DateTime?)GetValue(MinTimeProperty); }
            set { SetValue(MinTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinTimeProperty =
            DependencyProperty.Register(
            "MinTime",
            typeof(DateTime?),
            typeof(AUITimePicker),
            new PropertyMetadata(null, OnMinTimeChanged));

        private static void OnMinTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime? newMinTime = e.NewValue as DateTime?;
            DateTime? oldMinTime = e.OldValue as DateTime?;

            AUITimePicker tp = d as AUITimePicker;

            if (null != newMinTime && newMinTime.HasValue && newMinTime != oldMinTime)
            {
                if (DateTimeHelper.CompareTime(tp.SelectedTime, newMinTime.Value) < 0)
                {
                    tp.SelectedTime = newMinTime.Value;
                }
            }
        }

        /// <summary>
        /// 选择的时间
        /// </summary>
        public DateTime SelectedTime
        {
            get { return (DateTime)GetValue(SelectedTimeProperty); }
            set { SetValue(SelectedTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeProperty =
            DependencyProperty.Register(
            "SelectedTime", 
            typeof(DateTime), 
            typeof(AUITimePicker),
            new PropertyMetadata(DateTime.Today, OnSelectedTimeChanged));

        private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime? newValue = e.NewValue as DateTime?;
            DateTime? oldValue = e.OldValue as DateTime?;

            AUITimePicker tp = d as AUITimePicker;

            if (null != newValue && newValue.HasValue)
            {
                if (newValue != oldValue)
                {
                    DateTime selectedTime = newValue.Value;
                    if (null != tp._hourTextBox && (string.Empty == tp._hourTextBox.Text || int.Parse(tp._hourTextBox.Text) != selectedTime.Hour))
                    {
                        tp._hourTextBox.Text = DateTimeHelper.AppendZero(selectedTime.Hour);
                    }
                    if (null != tp._minuteTextBox && (string.Empty == tp._minuteTextBox.Text || int.Parse(tp._minuteTextBox.Text) != selectedTime.Minute))
                    {
                        tp._minuteTextBox.Text = DateTimeHelper.AppendZero(selectedTime.Minute);
                    }
                    if (null != tp._secondTextBox && (string.Empty == tp._secondTextBox.Text || int.Parse(tp._secondTextBox.Text) != selectedTime.Second))
                    {
                        tp._secondTextBox.Text = DateTimeHelper.AppendZero(selectedTime.Second);
                    }
                    if (null != tp.SelectedTimeChanged)
                    {
                        Collection<DateTime> addedItems = new Collection<DateTime>();
                        Collection<DateTime> removedItems = new Collection<DateTime>();

                        if (newValue.HasValue)
                        {
                            addedItems.Add(newValue.Value);
                        }

                        if (oldValue.HasValue)
                        {
                            removedItems.Add(oldValue.Value);
                        }
                        tp.SelectedTimeChanged(newValue.Value);
                    }
                }
            }
        }
        
        static AUITimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AUITimePicker), new FrameworkPropertyMetadata(typeof(AUITimePicker)));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InitTextBoxes();
            /*DateTime dt = DateTime.Now;
            dt.AddSeconds(-(dt.Second));
            this.SelectedTime = dt;*/
        }

        /// <summary>
        /// 初始化文本框事件
        /// </summary>
        private void InitTextBoxes()
        {
            this._hourTextBox = GetTemplateChild(HourTextBox) as AUITimePickerUpDown;
            this._minuteTextBox = GetTemplateChild(MinuteTextBox) as AUITimePickerUpDown;
            this._secondTextBox = GetTemplateChild(SecondTextBox) as AUITimePickerUpDown;

            this._hourTextBox.KeyDown -= new KeyEventHandler(HourTextKeyDown);
            this._minuteTextBox.KeyDown -= new KeyEventHandler(MinuteTextKeyDown);
            this._secondTextBox.KeyDown -= new KeyEventHandler(SecondTextKeyDown);

            this._hourTextBox.TextChanged -= new TextChangedEventHandler(HourTextChanged);
            this._minuteTextBox.TextChanged -= new TextChangedEventHandler(MinuteTextChanged);
            this._secondTextBox.TextChanged -= new TextChangedEventHandler(SecondTextChanged);

            this._hourTextBox.LostFocus -= new RoutedEventHandler(HourTextLostFocus);
            this._minuteTextBox.LostFocus -= new RoutedEventHandler(MinuteTextLostFocus);
            this._secondTextBox.LostFocus -= new RoutedEventHandler(SecondTextLostFocus);

            this._hourTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Hour);
            this._minuteTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Minute);

            this._hourTextBox.KeyDown += new KeyEventHandler(HourTextKeyDown);
            this._minuteTextBox.KeyDown += new KeyEventHandler(MinuteTextKeyDown);
            this._secondTextBox.KeyDown += new KeyEventHandler(SecondTextKeyDown);

            this._hourTextBox.TextChanged += new TextChangedEventHandler(HourTextChanged);
            this._minuteTextBox.TextChanged += new TextChangedEventHandler(MinuteTextChanged);
            this._secondTextBox.TextChanged += new TextChangedEventHandler(SecondTextChanged);

            this._hourTextBox.LostFocus += new RoutedEventHandler(HourTextLostFocus);
            this._minuteTextBox.LostFocus += new RoutedEventHandler(MinuteTextLostFocus);
            this._secondTextBox.LostFocus += new RoutedEventHandler(SecondTextLostFocus);

            //this._hourTextBox.MouseWheel += new MouseWheelEventHandler();
            //this._minuteTextBox.LostFocus += new RoutedEventHandler(MinuteTextLostFocus);
            //this._secondTextBox.LostFocus += new RoutedEventHandler(SecondTextLostFocus);
        }

        /// <summary>
        /// 小时文本框失去焦点时,填零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HourTextLostFocus(object sender, RoutedEventArgs e)
        {
            DateTime d = this.SelectedTime;
            if (this._hourTextBox.Text.Equals(string.Empty))
            {
                this._hourTextBox.Text = this.SelectedTime.Hour.ToString();
            }
            this._hourTextBox.Text = DateTimeHelper.AppendZero(int.Parse(this._hourTextBox.Text));
        }

        /// <summary>
        /// 分钟文本框失去焦点时,填零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinuteTextLostFocus(object sender, RoutedEventArgs e)
        {
            DateTime d = this.SelectedTime;
            if (this._minuteTextBox.Text.Equals(string.Empty))
            {
                this._minuteTextBox.Text = this.SelectedTime.Minute.ToString();
            }
            this._minuteTextBox.Text = DateTimeHelper.AppendZero(int.Parse(this._minuteTextBox.Text));
        }

        /// <summary>
        /// 小秒文本框失去焦点时,填零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondTextLostFocus(object sender, RoutedEventArgs e)
        {
            DateTime d = this.SelectedTime;
            if (this._secondTextBox.Text.Equals(string.Empty))
            {
                this._secondTextBox.Text = this.SelectedTime.Second.ToString();
            }
            this._secondTextBox.Text = DateTimeHelper.AppendZero(int.Parse(this._secondTextBox.Text));
        }

        /// <summary>
        /// 小时文本框内容变化时,判断内容是否正确
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HourTextChanged(object sender, TextChangedEventArgs e)
        {
            AUITimePickerUpDown hourTextBox = sender as AUITimePickerUpDown;
            if(string.Empty == hourTextBox.Text)
            {
                return;
            }
            int outInt = 0; 
            bool isInt = int.TryParse(hourTextBox.Text, out outInt);
            if (hourTextBox.Text == "0-1")
            {
                hourTextBox.Text = "23";
                hourTextBox.SelectAll();
            }
            else if (outInt == 24)
            {
                hourTextBox.Text = "00";
                //hourTextBox.Text = this.datetime.Hour.ToString();
                hourTextBox.SelectAll();
            }
            else if (outInt > 24)
            {
                datetime = DateTime.Now;
                hourTextBox.Text = this.datetime.Hour.ToString();
                hourTextBox.SelectAll();
            }
            else if (!isInt)
            {
                hourTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Hour);
                hourTextBox.SelectAll();
            }
            else
            {
                if (!CheckNumberValid(outInt, TextBoxTypes.Hour))
                {
                    hourTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Hour);
                    hourTextBox.SelectAll();
                }
            };
            DateTime d = this.SelectedTime;
            this.SelectedTime = new DateTime(d.Year, d.Month, d.Day, int.Parse(hourTextBox.Text), d.Minute, 0);
        }

        /// <summary>
        /// 分钟文本框内容变化时,判断内容是否正确
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinuteTextChanged(object sender, TextChangedEventArgs e)
        {
            AUITimePickerUpDown minuteTextBox = sender as AUITimePickerUpDown;
            if (string.Empty == minuteTextBox.Text)
            {
                return;
            }
            int outInt = 0;
            bool isInt = int.TryParse(minuteTextBox.Text, out outInt);
            if (minuteTextBox.Text == "0-1")
            {
                minuteTextBox.Text = "59";
                minuteTextBox.SelectAll();
            }
            else if (outInt == 60)
            {
                minuteTextBox.Text = "00";
                //minuteTextBox.Text = this.datetime.Minute.ToString();
                minuteTextBox.SelectAll();
            }
            else if (outInt > 60)
            {
                datetime = DateTime.Now;
                minuteTextBox.Text = this.datetime.Minute.ToString();
                minuteTextBox.SelectAll();
            }
            else if (!isInt)
            {
                minuteTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Minute);
                minuteTextBox.SelectAll();
            }
            else
            {
                if (!CheckNumberValid(outInt, TextBoxTypes.Minute))
                {
                    minuteTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Minute);
                    minuteTextBox.SelectAll();
                }
            }
            DateTime d = this.SelectedTime;
            this.SelectedTime = new DateTime(d.Year, d.Month, d.Day, d.Hour, int.Parse(this._minuteTextBox.Text), 0);
        }

        /// <summary>
        /// 秒文本框内容变化时,判断内容是否正确
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondTextChanged(object sender, TextChangedEventArgs e)
        {
            AUITimePickerUpDown secondTextBox = sender as AUITimePickerUpDown;
            if (string.Empty == secondTextBox.Text)
            {
                return;
            }
            int outInt = 0;
            bool isInt = int.TryParse(secondTextBox.Text, out outInt);
            if (secondTextBox.Text == "0-1")
            {
                secondTextBox.Text = "59";
                secondTextBox.SelectAll();
            }
            else if (outInt > 59)
            {
               // secondTextBox.Text = "00";
                //================ Add BY CTH[ADO-19132] ==============
                datetime = DateTime.Now;
                //================ End BY CTH =========================
                secondTextBox.Text = this.datetime.Millisecond.ToString();
                secondTextBox.SelectAll();
            }
            else if (!isInt)
            {
                secondTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Second);
                secondTextBox.SelectAll();
            }
            else
            {
                if (!CheckNumberValid(outInt, TextBoxTypes.Second))
                {
                    secondTextBox.Text = DateTimeHelper.AppendZero(this.SelectedTime.Second);
                    secondTextBox.SelectAll();
                }
            }
            DateTime d = this.SelectedTime;
            this.SelectedTime = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
        }

        /// <summary>
        /// 检查数字是否正确
        /// </summary>
        /// <param name="outInt">要检查的数字</param>
        /// <param name="textTypes">枚举<see cref="TextBoxTypes"/>,检查类型,可以是小时分钟或秒</param>
        /// <returns></returns>
        private bool CheckNumberValid(int outInt, TextBoxTypes textTypes)
        {
            if(null == this.MaxTime && null == this.MinTime)
            {
                return true;
            }
            DateTime d = this.SelectedTime;
            switch (textTypes)
            {
                case TextBoxTypes.Hour:
                    d = new DateTime(d.Year,d.Month,d.Day,outInt,d.Month,d.Second);
                    break;
                case TextBoxTypes.Minute:
                    d = new DateTime(d.Year, d.Month, d.Day, d.Hour, outInt, d.Second);
                    break;
                case TextBoxTypes.Second:
                    d = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Month, outInt);
                    break;
            }
            if(null != this.MaxTime && this.MaxTime.HasValue)
            {
                if(DateTimeHelper.CompareTime(this.MaxTime.Value,d) < 0)
                {
                    return false;
                }
            }
            if (null != this.MinTime && this.MinTime.HasValue)
            {
                if (DateTimeHelper.CompareTime(this.MinTime.Value, d) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 文本框类型
        /// </summary>
        private enum TextBoxTypes 
        { 
            Hour = 0,
            Minute = 1,
            Second = 2
        }

        /// <summary>
        /// 小时文本框按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HourTextKeyDown(object sender, KeyEventArgs e)
        {
            TextKeyDown(TextBoxTypes.Hour, e);
        }

        /// <summary>
        /// 分钟文本框按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinuteTextKeyDown(object sender, KeyEventArgs e)
        {
            TextKeyDown(TextBoxTypes.Minute,e);
        }

        /// <summary>
        /// 秒文本框按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondTextKeyDown(object sender, KeyEventArgs e)
        {
            TextKeyDown(TextBoxTypes.Second,e);
        }

        /// <summary>
        /// 文本框按钮按下时的处理
        /// </summary>
        /// <param name="sourceType">文本框类型<see cref="TextBoxTypes"/></param>
        /// <param name="e">按钮时间</param>
        private void TextKeyDown(TextBoxTypes sourceType, KeyEventArgs e)
        {
            Key k = e.Key;
            if (KeyEventHelper.IsNumberKey(k) || k == Key.Tab || k == Key.Escape||k==Key.F2)
            {
                return;
            }
            else 
            {
                if (KeyEventHelper.IsDirectionkey(k))
                {
                    DirectionkeyClicked(k, sourceType);
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 当检测到方向键按下时的处理
        /// </summary>
        /// <param name="key">按钮</param>
        /// <param name="sourceType">文本框类型<see cref="TextBoxTypes"/></param>
        private void DirectionkeyClicked(Key key, TextBoxTypes sourceType)
        { 
            switch(key)
            {
                case Key.Up:
                    ChangeValue(sourceType,1);
                    break;
                case Key.Down:
                    ChangeValue(sourceType, -1);
                    break;
                case Key.Left:
                    switch (sourceType)
                    {
                        case TextBoxTypes.Hour:
                            this._secondTextBox.Focus();
                            break;
                        case TextBoxTypes.Minute:
                            this._hourTextBox.Focus();
                            break;
                        case TextBoxTypes.Second:
                            this._minuteTextBox.Focus();
                            break;
                    }
                    break;
                case Key.Right:
                    switch (sourceType)
                    {
                        case TextBoxTypes.Hour:
                            this._minuteTextBox.Focus();
                            break;
                        case TextBoxTypes.Minute:
                            this._secondTextBox.Focus();
                            break;
                        case TextBoxTypes.Second:
                            this._hourTextBox.Focus();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 根据更改量改变对应的文本框
        /// </summary>
        /// <param name="sourceType">文本框类型<see cref="TextBoxTypes"/></param>
        /// <param name="increaseValue">增加量</param>
        private void ChangeValue(TextBoxTypes sourceType,int increaseValue)
        {
            int parentIncrease = 0;
            switch(sourceType)
            {
                case TextBoxTypes.Hour:
                    this._hourTextBox.Text = this.getRemainder(0, 23, int.Parse(this._hourTextBox.Text), increaseValue, out parentIncrease).ToString();
                    break;
                case TextBoxTypes.Minute:
                    //this.ChangingSelected = true;
                    this._minuteTextBox.Text = this.getRemainder(0, 59, int.Parse(this._minuteTextBox.Text), increaseValue, out parentIncrease).ToString();
                    //this.ChangingSelected = false;
                    if (parentIncrease != 0)
                    {
                        //ChangeValue(TextBoxTypes.Hour,parentIncrease);
                    }
                    break;
                case TextBoxTypes.Second:
                    //this.ChangingSelected = true;
                    this._secondTextBox.Text = this.getRemainder(0, 59, int.Parse(this._secondTextBox.Text), increaseValue, out parentIncrease).ToString();
                    //this.ChangingSelected = false;
                    if (parentIncrease != 0)
                    {
                        //ChangeValue(TextBoxTypes.Minute,parentIncrease);
                    }
                    break;
            }

        }

        /// <summary>
        /// 取得余数和父文本框增加量
        /// </summary>
        /// <param name="min">文本框数值的最小值一般为0</param>
        /// <param name="max">文本框内数字的最大值,小时是23，分钟时59</param>
        /// <param name="oldValue">文本框内的原来的值</param>
        /// <param name="increaseValue">增加量</param>
        /// <param name="parentIncrease">out 父文本框的增加量</param>
        /// <returns>更改后的文本框的值</returns>
        private int getRemainder(int min, int max,int oldValue,int increaseValue,out int parentIncrease)
        {
            int dividend = max - min + 1;
            int newValue = oldValue + increaseValue;
            parentIncrease = newValue > max ? 1 : newValue < min ? -1 : 0;
            return (newValue + dividend + dividend) % dividend;
        }
    }
}
