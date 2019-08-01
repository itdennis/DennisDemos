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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Linq;

namespace AvePoint.Migrator.Common.Controls
{
	[TemplatePart(Name = "Popup", Type = typeof(Popup))]
	[TemplatePart(Name = "SelectedTimeZoneHB", Type = typeof(HyperlinkButton))]
	[TemplatePart(Name = "ImageButton", Type = typeof(ImageButton))]
    public class AUITimeZonePicker : Control
    {
        private ComboBox LB;
        private CheckBox AutoAdjustCB;
        private StackPanel root;
        private Button okButton;
        private HyperlinkButton cancelLink;

        static AUITimeZonePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AUITimeZonePicker), new FrameworkPropertyMetadata(typeof(AUITimeZonePicker)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LB = GetTemplateChild("LB") as ComboBox;
            AutoAdjustCB = GetTemplateChild("AutoAdjustCB") as CheckBox;

            okButton = GetTemplateChild("okButton") as Button;
            okButton.Click -= Button_Click;
            okButton.Click += Button_Click;
            cancelLink = GetTemplateChild("cancelLink") as HyperlinkButton;
            cancelLink.Click -= Link_Click;
            cancelLink.Click += Link_Click;

            LB.SelectionChanged -= new SelectionChangedEventHandler(LB_SelectionChanged);
            LB.SelectionChanged += new SelectionChangedEventHandler(LB_SelectionChanged);

            if (!this.AllTimeZones.Any())
            {
                this.AllTimeZones = AUITimeZone.GetAllTimeZones();

                LB.SelectedItem = this.AllTimeZones.FirstOrDefault(i => AUITimeZone.Local.Id.Equals(i.Id)) ?? this.AllTimeZones.FirstOrDefault();
            }
        }
        
        void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Commit.Execute(true);
        }
        void Link_Click(object sender, RoutedEventArgs e)
        {
            this.Commit.Execute(false);
        }

        public event Action<bool> Commit;

        void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox source = sender as ComboBox;
            AUITimeZone selectedTimeZone = source.SelectedItem as AUITimeZone;
            if (selectedTimeZone != null)
            {
                if (selectedTimeZone.SupportsDaylightSavingTime)
                {
                    this.PopupAutoAdjustClockImageVisibility = Visibility.Visible;

                    AutoAdjustClockForDaylightSavingChanges = AutoAdjustClockForDaylightSavingChanges != false;
                }
                else
                {
                    this.PopupAutoAdjustClockImageVisibility = Visibility.Collapsed;
                }
            }
            this.SelectedTimeZone = selectedTimeZone;
        }

        public AUICalendar Calendar
        {
            get { return GetValue(CalendarProperty) as AUICalendar; }
            set { SetValue(CalendarProperty, value); }
        }

        public static readonly DependencyProperty CalendarProperty = DependencyProperty.Register("Calendar",
            typeof(AUICalendar), typeof(AUITimeZonePicker));

        public List<AUITimeZone> AllTimeZones
        {
            get { return (List<AUITimeZone>)GetValue(AllTimeZonesProperty); }
            set { SetValue(AllTimeZonesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllTimeZones.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllTimeZonesProperty =
            DependencyProperty.Register("AllTimeZones", typeof(List<AUITimeZone>), typeof(AUITimeZonePicker), new PropertyMetadata(AUITimeZone.GetAllTimeZones()));

        public event EventHandler<EventArgs> SelectedTimeZoneChanged;

        public AUITimeZone SelectedTimeZone
        {
            get { return (AUITimeZone)GetValue(SelectedTimeZoneProperty); }
            set { SetValue(SelectedTimeZoneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTimeZone.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTimeZoneProperty =
            DependencyProperty.Register("SelectedTimeZone", typeof(AUITimeZone), typeof(AUITimeZonePicker), new PropertyMetadata(AUITimeZone.GetCurrentTimeZone(), OnSelectedTimeZoneChanged));

        private static void OnSelectedTimeZoneChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AUITimeZonePicker source = o as AUITimeZonePicker;
            if (source == null || source.SelectedTimeZone == null || source.SelectedTimeZone.DisplayName == null || source.SelectedTimeZone.Zone == null)
                return;
            var zone = source.SelectedTimeZone.DisplayName;
            if (!string.IsNullOrEmpty(zone))
            {
                source.SelectedTimeZoneStr = zone.Substring(0, zone.IndexOf(')') + 1);
            }
            AUITimeZone _newValue = e.NewValue as AUITimeZone;
            if (_newValue.SupportsDaylightSavingTime)
            {
                source.AutoAdjustClockImageVisibility = Visibility.Visible;
            }
            else
            {
                source.AutoAdjustClockImageVisibility = Visibility.Collapsed;
            }
            if (source.SelectedTimeZoneChanged != null)
            {
                source.SelectedTimeZoneChanged(source, null);
            }
            source.PopupSelectedTimeZone = _newValue;
        }

        public AUITimeZone PopupSelectedTimeZone
        {
            get { return (AUITimeZone)GetValue(PopupSelectedTimeZoneProperty); }
            set { SetValue(PopupSelectedTimeZoneProperty, value); }
        }

        public static readonly DependencyProperty PopupSelectedTimeZoneProperty =
            DependencyProperty.Register("PopupSelectedTimeZone", typeof(AUITimeZone), typeof(AUITimeZonePicker), new PropertyMetadata(AUITimeZone.GetCurrentTimeZone()));

        public string SelectedTimeZoneStr
        {
            get { return (string)GetValue(SelectedTimeZoneStrProperty); }
            set { SetValue(SelectedTimeZoneStrProperty, value); }
        }

        public static readonly DependencyProperty SelectedTimeZoneStrProperty =
            DependencyProperty.Register("SelectedTimeZoneStr", typeof(string), typeof(AUITimeZonePicker), new PropertyMetadata(AUITimeZone.GetCurrentTimeZone().DisplayName));

        public event EventHandler<EventArgs> AutoAdjustClockForDaylightSavingChangesChanged;

        /// <summary>
        /// 是否自动执行夏令时转换
        /// </summary>
        public bool? AutoAdjustClockForDaylightSavingChanges
        {
            get { return (bool?)GetValue(AutoAdjustClockForDaylightSavingChangesProperty); }
            set { SetValue(AutoAdjustClockForDaylightSavingChangesProperty, value); }
        }

        public static readonly DependencyProperty AutoAdjustClockForDaylightSavingChangesProperty =
            DependencyProperty.Register("AutoAdjustClockForDaylightSavingChanges", typeof(bool?), typeof(AUITimeZonePicker), new PropertyMetadata(true, OnAutoAdjustClockForDaylightSavingChangesChanged));

        private static void OnAutoAdjustClockForDaylightSavingChangesChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AUITimeZonePicker source = o as AUITimeZonePicker;
            if (source.AutoAdjustClockForDaylightSavingChangesChanged != null)
            {
                source.AutoAdjustClockForDaylightSavingChangesChanged(source, null);
            }
        }

        public Visibility AutoAdjustClockImageVisibility
        {
            get { return (Visibility)GetValue(AutoAdjustClockImageVisibilityProperty); }
            set { SetValue(AutoAdjustClockImageVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoAdjustClockImageVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoAdjustClockImageVisibilityProperty =
            DependencyProperty.Register("AutoAdjustClockImageVisibility", typeof(Visibility), typeof(AUITimeZonePicker), new PropertyMetadata(Visibility.Collapsed));

        public Visibility TimeZoneVisibility
        {
            get { return (Visibility)GetValue(TimeZoneVisibilityProperty); }
            set { SetValue(TimeZoneVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeZoneVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeZoneVisibilityProperty =
            DependencyProperty.Register("TimeZoneVisibility", typeof(Visibility), typeof(AUITimeZonePicker), new PropertyMetadata(null));

        public Visibility PopupAutoAdjustClockImageVisibility
        {
            get { return (Visibility)GetValue(PopupAutoAdjustClockImageVisibilityProperty); }
            set { SetValue(PopupAutoAdjustClockImageVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupAutoAdjustClockImageVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupAutoAdjustClockImageVisibilityProperty =
            DependencyProperty.Register("PopupAutoAdjustClockImageVisibility", typeof(Visibility), typeof(AUITimeZonePicker), new PropertyMetadata(Visibility.Collapsed));
    }
}