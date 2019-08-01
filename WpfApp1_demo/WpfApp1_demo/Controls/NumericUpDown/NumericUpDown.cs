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
using System.Globalization;
using System.Windows;

namespace AvePoint.Migrator.Common.Controls
{
    public class NumericUpDown : MigratorTool.Controls.NumericUpDown.UpDownBase<double>
	{
		public static readonly DependencyProperty MinimumProperty;
		public static readonly DependencyProperty MaximumProperty;
		public static readonly DependencyProperty IncrementProperty;
		public static readonly DependencyProperty StringFormatProperty;
		public double Minimum
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.MinimumProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.MinimumProperty, value);
			}
		}
		public double Maximum
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.MaximumProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.MaximumProperty, value);
			}
		}
		public double Increment
		{
			get
			{
				return (double)base.GetValue(NumericUpDown.IncrementProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.IncrementProperty, value);
			}
		}
		public string FormatString
		{
			get
			{
				return (string)base.GetValue(NumericUpDown.StringFormatProperty);
			}
			set
			{
				base.SetValue(NumericUpDown.StringFormatProperty, value);
			}
		}
		private static void OnMinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}
		protected virtual void OnMinimumChanged(double oldValue, double newValue)
		{
		}
		private static void OnMaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}
		protected virtual void OnMaximumChanged(double oldValue, double newValue)
		{
		}
		private static void OnIncrementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}
		protected virtual void OnIncrementChanged(double oldValue, double newValue)
		{
		}
		private static void OnStringFormatPropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDown numericUpDown = d as NumericUpDown;
			numericUpDown.OnStringFormatChanged(e.OldValue.ToString(), e.NewValue.ToString());
		}
		protected virtual void OnStringFormatChanged(string oldValue, string newValue)
		{
			base.Text = this.FormatValue();
		}
		static NumericUpDown()
		{
			NumericUpDown.MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(-1.7976931348623157E+308, new PropertyChangedCallback(NumericUpDown.OnMinimumPropertyChanged)));
			NumericUpDown.MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1.7976931348623157E+308, new PropertyChangedCallback(NumericUpDown.OnMaximumPropertyChanged)));
			NumericUpDown.IncrementProperty = DependencyProperty.Register("Increment", typeof(double), typeof(NumericUpDown), new PropertyMetadata(1.0, new PropertyChangedCallback(NumericUpDown.OnIncrementPropertyChanged)));
			NumericUpDown.StringFormatProperty = DependencyProperty.Register("FormatString", typeof(string), typeof(NumericUpDown), new PropertyMetadata("F0", new PropertyChangedCallback(NumericUpDown.OnStringFormatPropertyPropertyChanged)));
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.SetValidSpinDirection();
		}
		protected override void OnValueChanged(RoutedPropertyChangedEventArgs<double> e)
		{
			this.SetValidSpinDirection();
		}
		protected override double ParseValue(string text)
		{
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture);
			if (text.Contains(instance.PercentSymbol))
			{
				return this.TryParcePercent(text, instance);
			}
			return this.TryParceDouble(text, instance);
		}
		protected internal override string FormatValue()
		{
			return this.Value.ToString(this.FormatString, CultureInfo.CurrentCulture);
		}
		protected override void OnIncrement()
		{
			this.Value = (double)((decimal)this.Value + (decimal)this.Increment);
		}
		protected override void OnDecrement()
		{
			this.Value = (double)((decimal)this.Value - (decimal)this.Increment);
		}
		private void SetValidSpinDirection()
		{
			ValidSpinDirections validSpinDirections = ValidSpinDirections.None;
			if (this.Value < this.Maximum)
			{
				validSpinDirections |= ValidSpinDirections.Increase;
			}
			if (this.Value > this.Minimum)
			{
				validSpinDirections |= ValidSpinDirections.Decrease;
			}
			if (base.Spinner != null)
			{
				base.Spinner.ValidSpinDirection = validSpinDirections;
			}
		}
		private double TryParcePercent(string text, NumberFormatInfo info)
		{
			text = text.Replace(info.PercentSymbol, null);
			double num = this.TryParceDouble(text, info);
			return num / 100.0;
		}
		private double TryParceDouble(string text, NumberFormatInfo info)
		{
			double value;
			if (!double.TryParse(text, NumberStyles.Any, info, out value))
			{
				value = this.Value;
				base.TextBox.Text = (base.Text = this.FormatValue());
			}
			return value;
		}
    }
}
