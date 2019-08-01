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
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MigratorTool.WPF.I18N;
using MigratorTool.WPF.View.Controls;
using System.Diagnostics.CodeAnalysis;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplateVisualState(Name = VisualStates.StateIdle, GroupName = VisualStates.GroupBusyStatus)]
    [TemplateVisualState(Name = VisualStates.StateBusy, GroupName = VisualStates.GroupBusyStatus)]
    [TemplateVisualState(Name = VisualStates.StateRunning, GroupName = VisualStates.GroupBusyStatus)]
    [TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]
    [TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
    [StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof(Rectangle))]
    [StyleTypedProperty(Property = "ProgressBarStyle", StyleTargetType = typeof(ProgressBar))]
    public class BusyIndicator : ContentControl
    {
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        /// <summary>
        /// Gets or sets a value indicating whether the BusyContent is visible.
        /// </summary>
        protected bool IsContentVisible { get; set; }

        /// <summary>
        /// Timer used to delay the initial display and avoid flickering.
        /// </summary>
        private DispatcherTimer _displayAfterTimer;

        private Button cancelButton;

        /// <summary>
        /// Instantiates a new instance of the BusyIndicator control.
        /// </summary>
        public BusyIndicator()
        {
            DefaultStyleKey = typeof(BusyIndicator);
            _displayAfterTimer = new DispatcherTimer();
            Loaded += delegate
            {
                _displayAfterTimer.Tick += new EventHandler(DisplayAfterTimerElapsed);
            };
            Unloaded += delegate
            {
                _displayAfterTimer.Tick -= new EventHandler(DisplayAfterTimerElapsed);
                _displayAfterTimer.Stop();
            };
        }

        /// <summary>
        /// Overrides the OnApplyTemplate method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mRunningSB = GetTemplateChild("RunningSB") as Storyboard;
            this.cancelButton = GetTemplateChild("CancelButton") as Button;
            if (this.cancelButton != null)
            {
                if (this.IsBusy)
                {
                    this.cancelButton.Visibility = this.CancelButtonVisibility;
                }
            }
            mLoadingAngle = GetTemplateChild("LoadingAngle") as RotateTransform;
            this.SizeChanged -= new SizeChangedEventHandler(BusyIndicator_SizeChanged);
            this.SizeChanged += new SizeChangedEventHandler(BusyIndicator_SizeChanged);

            ChangeVisualState(false);

            this.HintText = I18NEntity.Get("Common_8561ddb0_2a7e_4978_a698_9dad1e21536a", "Loading...");
        }

        /// <summary>
        /// Handler for the DisplayAfterTimer.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void DisplayAfterTimerElapsed(object sender, EventArgs e)
        {
            _displayAfterTimer.Stop();
            IsContentVisible = true;
            ChangeVisualState(true);
        }

        /// <summary>
        /// Changes the control's visual state(s).
        /// </summary>
        /// <param name="useTransitions">True if state transitions should be used.</param>
        protected virtual void ChangeVisualState(bool useTransitions)
        {
            // changed by ningzhang to support running
            if (IsBusy)
            {
                VisualStateManager.GoToState(this, VisualStates.StateBusy, useTransitions);
            }
            else if (IsRunning)
            {
                VisualStateManager.GoToState(this, VisualStates.StateRunning, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, VisualStates.StateIdle, useTransitions);
            }
            VisualStateManager.GoToState(this, IsContentVisible ? VisualStates.StateVisible : VisualStates.StateHidden, useTransitions);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the busy indicator should show.
        /// </summary>
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Identifies the IsBusy dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy",
            typeof(bool),
            typeof(BusyIndicator),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsBusyChanged)));

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="d">BusyIndicator that changed its IsBusy.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // changed by ningzhang to support running
            ((BusyIndicator)d).OnIsBusyOrRunningChanged(e);
        }

        /// <summary>
        /// IsBusyProperty property changed handler.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnIsBusyOrRunningChanged(DependencyPropertyChangedEventArgs e)
        {
            // changed by ningzhang to support running
            if (IsBusy || IsRunning)
            {
                if (DisplayAfter.Equals(TimeSpan.Zero))
                {
                    // Go visible now
                    IsContentVisible = true;
                }
                else
                {
                    // Set a timer to go visible
                    _displayAfterTimer.Interval = DisplayAfter;
                    _displayAfterTimer.Start();
                }
                // Added by jptian: 增加VPAT支持
                //_lastFocusedElement = FocusManager.GetFocusedElement() as Control;
                if (this.cancelButton != null)
                {
                    this.cancelButton.Visibility = this.CancelButtonVisibility;
                }
                this.IsTabStop = true;
                this.Focus();
            }
            else
            {
                // No longer visible
                _displayAfterTimer.Stop();
                IsContentVisible = false;
                if (this.cancelButton != null)
                {
                    this.cancelButton.Visibility = System.Windows.Visibility.Collapsed;
                }
                this.IsTabStop = false;
            }
            ChangeVisualState(true);
        }

        /// <summary>
        /// Gets or sets a value indicating the busy content to display to the user.
        /// </summary>
        public object BusyContent
        {
            get { return (object)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        /// <summary>
        /// Identifies the BusyContent dependency property.
        /// </summary>
        public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register(
            "BusyContent",
            typeof(object),
            typeof(BusyIndicator),
            new PropertyMetadata(I18NEntity.Get("Common_fce2d405_5451_4577_981b_6a971c51f566", "Loading...")));

        public Visibility CancelButtonVisibility
        {
            get { return (Visibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonVisibilityProperty = DependencyProperty.Register(
            "CancelButtonVisibility",
            typeof(Visibility),
            typeof(BusyIndicator),
            new PropertyMetadata(Visibility.Collapsed));

        public ICommand CancelCommand
        {
            get { return (ICommand)(GetValue(CancelCommandProperty)); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register
            ("CancelCommand", 
            typeof(ICommand), 
            typeof(BusyIndicator), 
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating the template to use for displaying the busy content to the user.
        /// </summary>
        public DataTemplate BusyContentTemplate
        {
            get { return (DataTemplate)GetValue(BusyContentTemplateProperty); }
            set { SetValue(BusyContentTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the BusyTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty BusyContentTemplateProperty = DependencyProperty.Register(
            "BusyContentTemplate",
            typeof(DataTemplate),
            typeof(BusyIndicator),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating how long to delay before displaying the busy content.
        /// </summary>
        public TimeSpan DisplayAfter
        {
            get { return (TimeSpan)GetValue(DisplayAfterProperty); }
            set { SetValue(DisplayAfterProperty, value); }
        }

        /// <summary>
        /// Identifies the DisplayAfter dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayAfterProperty = DependencyProperty.Register(
            "DisplayAfter",
            typeof(TimeSpan),
            typeof(BusyIndicator),
            new PropertyMetadata(TimeSpan.FromSeconds(0.1)));

        /// <summary>
        /// Gets or sets a value indicating the style to use for the overlay.
        /// </summary>
        public Style OverlayStyle
        {
            get { return (Style)GetValue(OverlayStyleProperty); }
            set { SetValue(OverlayStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the OverlayStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty OverlayStyleProperty = DependencyProperty.Register(
            "OverlayStyle",
            typeof(Style),
            typeof(BusyIndicator),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating the style to use for the progress bar.
        /// </summary>
        public Style ProgressBarStyle
        {
            get { return (Style)GetValue(ProgressBarStyleProperty); }
            set { SetValue(ProgressBarStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the ProgressBarStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty ProgressBarStyleProperty = DependencyProperty.Register(
            "ProgressBarStyle",
            typeof(Style),
            typeof(BusyIndicator),
            new PropertyMetadata(null));

        private Storyboard mRunningSB;
        private RotateTransform mLoadingAngle;

        void BusyIndicator_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BusyIndicator source = sender as BusyIndicator;
            if (source != null)
            {
                double height = source.ActualHeight;
                double width = source.ActualWidth;
                source.MinOfHeightAndWidth = Math.Min(height, width);
                source.RunningCircleCenter = Math.Min(source.MinOfHeightAndWidth * 0.5, 35);
                if (mLoadingAngle != null)
                {
                    mLoadingAngle.CenterX = RunningCircleCenter;
                    mLoadingAngle.CenterY = RunningCircleCenter;
                }
            }
        }

        public object RunningContent
        {
            get { return (object)GetValue(RunningContentProperty); }
            set { SetValue(RunningContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadingContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunningContentProperty =
            DependencyProperty.Register("RunningContent",
            typeof(object),
            typeof(BusyIndicator),
            new PropertyMetadata(null));

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register(
            "IsRunning",
            typeof(bool),
            typeof(BusyIndicator),
            new PropertyMetadata(false, OnIsRunningPropertyChanged));

        private static void OnIsRunningPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((BusyIndicator)obj).OnIsBusyOrRunningChanged(args);
        }

        public double MinOfHeightAndWidth
        {
            get { return (double)GetValue(MinOfHeightAndWidthProperty); }
            set { SetValue(MinOfHeightAndWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinOfHeightAndWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinOfHeightAndWidthProperty =
            DependencyProperty.Register(
            "MinOfHeightAndWidth",
            typeof(double),
            typeof(BusyIndicator),
            new PropertyMetadata(70.0));

        public double RunningCircleCenter
        {
            get { return (double)GetValue(RunningCircleCenterProperty); }
            set { SetValue(RunningCircleCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RunningCircleCenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunningCircleCenterProperty =
            DependencyProperty.Register(
            "RunningCircleCenter",
            typeof(double),
            typeof(BusyIndicator),
            new PropertyMetadata(35.0));

        public Visibility RunningBackgroundVisibility
        {
            get { return (Visibility)GetValue(RunningBackgroundVisibilityProperty); }
            set { SetValue(RunningBackgroundVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RunningBackgroundVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunningBackgroundVisibilityProperty =
            DependencyProperty.Register(
            "RunningBackgroundVisibility",
            typeof(Visibility),
            typeof(BusyIndicator),
            new PropertyMetadata(Visibility.Visible));

        public bool IsClosable
        {
            get { return (bool)GetValue(IsClosableProperty); }
            set { SetValue(IsClosableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClosable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClosableProperty =
            DependencyProperty.Register("IsClosable", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(false));

        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HintText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintText", typeof(string), typeof(BusyIndicator), new PropertyMetadata(null));

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new BusyIndicatorAutomationPeer(this);
        }
    }

    public class BusyIndicatorAutomationPeer : FrameworkElementAutomationPeer
    {
        public BusyIndicatorAutomationPeer(BusyIndicator owner)
            : base(owner)
        {

        }

        private BusyIndicator OwningBusyIndicator
        {
            get { return (BusyIndicator)this.Owner; }
        }

        protected override string GetNameCore()
        {
            return OwningBusyIndicator.BusyContent + ", " + OwningBusyIndicator.HintText;
        }
    }
}
