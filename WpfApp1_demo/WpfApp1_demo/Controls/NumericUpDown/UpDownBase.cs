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
using AvePoint.Migrator.Common.Controls;

namespace MigratorTool.Controls.NumericUpDown
{
	public abstract class UpDownBase<T> : Control
	{
		internal const string ElementTextName = "Text";
		internal const string ElementSpinnerName = "Spinner";
		private bool _isSyncingTextAndValueProperties;
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(T), typeof(UpDownBase<T>), new FrameworkPropertyMetadata(default(T), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(UpDownBase<T>.OnValuePropertyChanged)));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UpDownBase<T>), new FrameworkPropertyMetadata("0", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(UpDownBase<T>.OnTextPropertyChanged)));
		public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(UpDownBase<T>), new PropertyMetadata(true, new PropertyChangedCallback(UpDownBase<T>.OnIsEditablePropertyChanged)));
		private Spinner _spinner;
		public event RoutedPropertyChangedEventHandler<T> ValueChanged;
		public virtual T Value
		{
			get
			{
				return (T)((object)base.GetValue(UpDownBase<T>.ValueProperty));
			}
			set
			{
				base.SetValue(UpDownBase<T>.ValueProperty, value);
			}
		}
		public string Text
		{
			get
			{
				return (string)base.GetValue(UpDownBase<T>.TextProperty);
			}
			set
			{
				base.SetValue(UpDownBase<T>.TextProperty, value);
			}
		}
		public bool IsEditable
		{
			get
			{
				return (bool)base.GetValue(UpDownBase<T>.IsEditableProperty);
			}
			set
			{
				base.SetValue(UpDownBase<T>.IsEditableProperty, value);
			}
		}
		internal TextBox TextBox
		{
			get;
			private set;
		}
		internal Spinner Spinner
		{
			get
			{
				return this._spinner;
			}
			private set
			{
				this._spinner = value;
				this._spinner.Spin += new EventHandler<SpinEventArgs>(this.OnSpinnerSpin);
			}
		}
		private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpDownBase<T> upDownBase = (UpDownBase<T>)d;
			T oldValue = (T)((object)e.OldValue);
			T newValue = (T)((object)e.NewValue);
			upDownBase.SyncTextAndValueProperties(e.Property, e.NewValue);
			RoutedPropertyChangedEventArgs<T> e2 = new RoutedPropertyChangedEventArgs<T>(oldValue, newValue);
			upDownBase.OnValueChanged(e2);
		}
		protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<T> e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, e);
			}
		}
		private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpDownBase<T> upDownBase = (UpDownBase<T>)d;
			upDownBase.SyncTextAndValueProperties(e.Property, e.NewValue);
		}
		private static void OnIsEditablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UpDownBase<T> upDownBase = d as UpDownBase<T>;
			upDownBase.OnIsEditableChanged((bool)e.OldValue, (bool)e.NewValue);
		}
		protected virtual void OnIsEditableChanged(bool oldValue, bool newValue)
		{
			if (this.TextBox != null)
			{
				this.TextBox.IsReadOnly = !this.IsEditable;
			}
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.TextBox = (base.GetTemplateChild("Text") as TextBox);
			this.Spinner = (base.GetTemplateChild("Spinner") as Spinner);
			if (this.TextBox != null)
			{
				this.TextBox.IsReadOnly = !this.IsEditable;
			}
		}
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			Key key = e.Key;
			if (key != Key.Return)
			{
				switch (key)
				{
				case Key.Up:
					this.DoIncrement();
					e.Handled = true;
					return;
				case Key.Right:
					break;
				case Key.Down:
					this.DoDecrement();
					e.Handled = true;
					return;
				default:
					return;
				}
			}
			else
			{
				this.SyncTextAndValueProperties(UpDownBase<T>.TextProperty, this.TextBox.Text);
			}
		}
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			base.OnMouseWheel(e);
			if (!e.Handled)
			{
				if (e.Delta < 0)
				{
					this.DoDecrement();
				}
				else
				{
					if (0 < e.Delta)
					{
						this.DoIncrement();
					}
				}
				e.Handled = true;
			}
		}
		protected abstract T ParseValue(string text);
		protected internal abstract string FormatValue();
		protected abstract void OnIncrement();
		protected abstract void OnDecrement();
		protected object GetValue()
		{
			return this.Value;
		}
		protected void SetValue(object value)
		{
			this.Value = (T)((object)value);
		}
		private void DoDecrement()
		{
			if (this.Spinner == null || (this.Spinner.ValidSpinDirection & ValidSpinDirections.Decrease) == ValidSpinDirections.Decrease)
			{
				this.OnDecrement();
			}
		}
		private void DoIncrement()
		{
			if (this.Spinner == null || (this.Spinner.ValidSpinDirection & ValidSpinDirections.Increase) == ValidSpinDirections.Increase)
			{
				this.OnIncrement();
			}
		}
		protected void SyncTextAndValueProperties(DependencyProperty p, object newValue)
		{
			if (this._isSyncingTextAndValueProperties)
			{
				return;
			}
			this._isSyncingTextAndValueProperties = true;
			if (UpDownBase<T>.TextProperty == p)
			{
				base.SetValue(UpDownBase<T>.ValueProperty, this.ParseValue(newValue.ToString()));
			}
			base.SetValue(UpDownBase<T>.TextProperty, this.FormatValue());
			this._isSyncingTextAndValueProperties = false;
		}
		protected virtual void OnSpin(SpinEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e.Direction == SpinDirection.Increase)
			{
				this.DoIncrement();
				return;
			}
			this.DoDecrement();
		}
		private void OnSpinnerSpin(object sender, SpinEventArgs e)
		{
			this.OnSpin(e);
		}
	}
}
