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
using System.Windows.Data;
using MigratorTool.WPF.View.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplateVisualState(Name = VisualStates.StateNormal, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateMouseOver, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StatePressed, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateDisabled, GroupName = VisualStates.GroupCommon)]
    [TemplateVisualState(Name = VisualStates.StateUnselected, GroupName = VisualStates.GroupSelection)]
    [TemplateVisualState(Name = VisualStates.StateSelected, GroupName = VisualStates.GroupSelection)]
    [TemplateVisualState(Name = VisualStates.StateCalendarButtonUnfocused, GroupName = VisualStates.GroupCalendarButtonFocus)]
    [TemplateVisualState(Name = VisualStates.StateCalendarButtonFocused, GroupName = VisualStates.GroupCalendarButtonFocus)]
    [TemplateVisualState(Name = VisualStates.StateInactive, GroupName = VisualStates.GroupActive)]
    [TemplateVisualState(Name = VisualStates.StateActive, GroupName = VisualStates.GroupActive)]
    [TemplateVisualState(Name = AUICalendarDayButton.StateRegularDay, GroupName = AUICalendarDayButton.GroupDay)]
    [TemplateVisualState(Name = AUICalendarDayButton.StateToday, GroupName = AUICalendarDayButton.GroupDay)]
    [TemplateVisualState(Name = AUICalendarDayButton.StateNormalDay, GroupName = AUICalendarDayButton.GroupBlackout)]
    [TemplateVisualState(Name = AUICalendarDayButton.StateBlackoutDay, GroupName = AUICalendarDayButton.GroupBlackout)]
    public sealed class AUICalendarDayButton : Button
    {
        #region Constants
        /// <summary>
        /// Default content for the CalendarDayButton
        /// </summary>
        private const int DEFAULTCONTENT = 1;

        /// <summary>
        /// Identifies the Today state.
        /// </summary>
        internal const string StateToday = "Today";

        /// <summary>
        /// Identifies the RegularDay state.
        /// </summary>
        internal const string StateRegularDay = "RegularDay";

        /// <summary>
        /// Name of the Day state group.
        /// </summary>
        internal const string GroupDay = "DayStates";

        /// <summary>
        /// Identifies the BlackoutDay state.
        /// </summary>
        internal const string StateBlackoutDay = "BlackoutDay";

        /// <summary>
        /// Identifies the NormalDay state.
        /// </summary>
        internal const string StateNormalDay = "NormalDay";

        /// <summary>
        /// Name of the BlackoutDay state group.
        /// </summary>
        internal const string GroupBlackout = "BlackoutDayStates";

        #endregion Constants

        #region Data

        private bool _shouldCoerceContent;
        private object _coercedContent;

        #endregion Data

        #region Constructors
        /// <summary>
        /// Static constructor
        /// </summary>
        static AUICalendarDayButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AUICalendarDayButton), new FrameworkPropertyMetadata(typeof(AUICalendarDayButton)));
            ContentProperty.OverrideMetadata(typeof(AUICalendarDayButton), new FrameworkPropertyMetadata(null, OnCoerceContent));
        }

        /// <summary>
        /// Represents the CalendarDayButton that is used in Calendar Control.
        /// </summary>
        public AUICalendarDayButton()
            : base()
        {
            // Attach the necessary events to their virtual counterparts
            Loaded += delegate { ChangeVisualState(false); };
        }

        #endregion Constructs

        #region Public Properties

        #region IsToday

        internal static readonly DependencyPropertyKey IsTodayPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsToday",
            typeof(bool),
            typeof(AUICalendarDayButton),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnVisualStatePropertyChanged)));

        /// <summary>
        /// Dependency property field for IsToday property
        /// </summary>
        public static readonly DependencyProperty IsTodayProperty = IsTodayPropertyKey.DependencyProperty;

        /// <summary>
        /// True if the CalendarDayButton represents today
        /// </summary>
        public bool IsToday
        {
            get { return (bool)GetValue(IsTodayProperty); }
        }

        #endregion IsToday

        #region IsSelected

        internal static readonly DependencyPropertyKey IsSelectedPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsSelected",
            typeof(bool),
            typeof(AUICalendarDayButton),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnVisualStatePropertyChanged)));

        /// <summary>
        /// Dependency property field for IsSelected property
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = IsSelectedPropertyKey.DependencyProperty;

        /// <summary>
        /// True if the CalendarDayButton is selected
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
        }

        #endregion IsSelected

        #region IsInactive

        internal static readonly DependencyPropertyKey IsInactivePropertyKey = DependencyProperty.RegisterReadOnly(
            "IsInactive",
            typeof(bool),
            typeof(AUICalendarDayButton),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnVisualStatePropertyChanged)));

        /// <summary>
        /// Dependency property field for IsActive property
        /// </summary>
        public static readonly DependencyProperty IsInactiveProperty = IsInactivePropertyKey.DependencyProperty;

        /// <summary>
        /// True if the CalendarDayButton represents a day that falls in the currently displayed month
        /// </summary>
        public bool IsInactive
        {
            get { return (bool)GetValue(IsInactiveProperty); }
        }

        #endregion IsInactive

        #region IsBlackedOut

        internal static readonly DependencyPropertyKey IsBlackedOutPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsBlackedOut",
            typeof(bool),
            typeof(AUICalendarDayButton),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnVisualStatePropertyChanged)));

        /// <summary>
        /// Dependency property field for IsBlackedOut property
        /// </summary>
        public static readonly DependencyProperty IsBlackedOutProperty = IsBlackedOutPropertyKey.DependencyProperty;

        /// <summary>
        /// True if the CalendarDayButton represents a blackout date
        /// </summary>
        public bool IsBlackedOut
        {
            get { return (bool)GetValue(IsBlackedOutProperty); }
        }

        #endregion IsBlackedOut

        #region IsHighlighted

        internal static readonly DependencyPropertyKey IsHighlightedPropertyKey = DependencyProperty.RegisterReadOnly(
            "IsHighlighted",
            typeof(bool),
            typeof(AUICalendarDayButton),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnVisualStatePropertyChanged)));

        /// <summary>
        /// Dependency property field for IsHighlighted property
        /// </summary>
        public static readonly DependencyProperty IsHighlightedProperty = IsHighlightedPropertyKey.DependencyProperty;

        /// <summary>
        /// True if the CalendarDayButton represents a highlighted date
        /// </summary>
        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
        }

        #endregion IsHighlighted

        #endregion Public Properties

        #region Internal Properties

        internal AUICalendar Owner
        {
            get;
            set;
        }

        #endregion Internal Properties

        #region Public Methods
        /// <summary>
        /// Apply a template to the button.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Sync the logical and visual states of the control
            ChangeVisualState(false);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Creates the automation peer for the CalendarDayButton.
        /// </summary>
        /// <returns></returns>
        //protected override AutomationPeer OnCreateAutomationPeer()
        //{
        //    return new CalendarDayButtonAutomationPeer(this);
        //}

        protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            ChangeVisualState(true);
            base.OnGotKeyboardFocus(e);
        }

        protected override void OnLostKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            ChangeVisualState(true);
            base.OnLostKeyboardFocus(e);
        }

        #endregion Protected Methods

        #region Internal Methods

        /// <summary>
        /// Change to the correct visual state for the button.
        /// </summary>
        /// <param name="useTransitions">
        /// true to use transitions when updating the visual state, false to
        /// snap directly to the new visual state.
        /// </param>
        internal void ChangeVisualState(bool useTransitions)
        {
            if (this.IsEnabled)
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateNormal);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateDisabled);
            }

            // Update the SelectionStates group
            if (IsSelected || IsHighlighted)
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateSelected, VisualStates.StateUnselected);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateUnselected);
            }

            // Update the ActiveStates group
            if (!IsInactive)
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateActive, VisualStates.StateInactive);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateInactive);
            }

            // Update the DayStates group
            if (IsToday && this.Owner != null && this.Owner.IsTodayHighlighted)
            {
                VisualStates.GoToState(this, useTransitions, StateToday, StateRegularDay);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, StateRegularDay);
            }

            // Update the BlackoutDayStates group
            if (IsBlackedOut)
            {
                VisualStates.GoToState(this, useTransitions, StateBlackoutDay, StateNormalDay);
            }
            else
            {
                VisualStates.GoToState(this, useTransitions, StateNormalDay);
            }

            // Update the FocusStates group
            if (IsKeyboardFocused)
            {
                VisualStates.GoToState(this, useTransitions, VisualStates.StateCalendarButtonFocused, VisualStates.StateCalendarButtonUnfocused);
            }
            else
            {
                VisualStateManager.GoToState(this, VisualStates.StateCalendarButtonUnfocused, useTransitions);
            }
        }

        internal void SetContentInternal(string value)
        {
            if (BindingOperations.GetBindingExpressionBase(this, ContentControl.ContentProperty) != null)
            {
                Content = value;
            }
            else
            {
                this._shouldCoerceContent = true;
                this._coercedContent = value;
                this.CoerceValue(ContentControl.ContentProperty);
            }
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Common PropertyChangedCallback for dependency properties that trigger visual state changes
        /// </summary>
        /// <param name="dObject"></param>
        /// <param name="e"></param>
        private static void OnVisualStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AUICalendarDayButton dayButton = sender as AUICalendarDayButton;
            if (dayButton != null)
            {
                dayButton.ChangeVisualState(true);
            }
        }

        private static object OnCoerceContent(DependencyObject sender, object baseValue)
        {
            AUICalendarDayButton button = (AUICalendarDayButton)sender;
            if (button._shouldCoerceContent)
            {
                button._shouldCoerceContent = false;
                return button._coercedContent;
            }

            return baseValue;
        }

        #endregion Private Methods
    }
}
