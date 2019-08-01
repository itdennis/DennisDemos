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

// -------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All Rights Reserved.
// -------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Provides VisualStateManager base behavior for controls.
    /// </summary>
    /// <remarks>
    ///     Provides focus states.
    ///     Forwards the Loaded event to UpdateState.
    /// </remarks>
    public class ControlBehavior : VisualStateBehavior
    {
        /// <summary>
        ///     This behavior targets Control derived Controls.
        /// </summary>
        protected override internal Type TargetType
        {
            get { return typeof(Control); }
        }

        /// <summary>
        ///     Attaches to property changes and events.
        /// </summary>
        /// <param name="control">An instance of the control.</param>
        protected override void OnAttach(Control control)
        {
            control.Loaded += delegate(object sender, RoutedEventArgs e) { UpdateState(control, false);};
            AddValueChanged(UIElement.IsKeyboardFocusWithinProperty, typeof(Control), control, UpdateStateHandler);
        }

        /// <summary>
        /// Detaches property changes and events.
        /// </summary>
        /// <param name="control">The control</param>
        protected override void OnDetach(Control control)
        {
            RemoveValueChanged(UIElement.IsKeyboardFocusWithinProperty, typeof(Control), control, UpdateStateHandler);
        }

        protected override void UpdateStateHandler(Object o, EventArgs e)
        {
            Control cont = o as Control;
            if (cont == null)
            {
                throw new InvalidOperationException("This should never be used on anything other than a control.");
            }
            UpdateState(cont, true);
        }

        /// <summary>
        ///     Called to update the control's visual state.
        /// </summary>
        /// <param name="control">The instance of the control being updated.</param>
        /// <param name="useTransitions">Whether to use transitions or not.</param>
        protected override void UpdateState(Control control, bool useTransitions)
        {
            if (control.IsKeyboardFocusWithin)
            {
                VisualStateManager.GoToState(control, "Focused", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(control, "Unfocused", useTransitions);
            }
        }
    }
}
