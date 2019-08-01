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
using System.Windows.Controls.Primitives;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Provides VisualStateManager behavior for ProgressBar controls.
    /// </summary>
    public class ProgressBarBehavior : ControlBehavior
    {
        /// <summary>
        ///     This behavior targets ProgressBar derived Controls.
        /// </summary>
        protected override internal Type TargetType
        {
            get { return typeof(ProgressBar); }
        }

        /// <summary>
        ///     Attaches to property changes and events.
        /// </summary>
        /// <param name="control">An instance of the control.</param>
        protected override void OnAttach(Control control)
        {
            base.OnAttach(control);

            ProgressBar progressBar = (ProgressBar)control;
            Type targetType = typeof(ProgressBar);

            AddValueChanged(ProgressBar.IsIndeterminateProperty, targetType, progressBar, UpdateStateHandler);
        }

        /// <summary>
        /// Detaches property changes and events.
        /// </summary>
        /// <param name="control">The control</param>
        protected override void OnDetach(Control control)
        {
            base.OnDetach(control);

            ProgressBar progressBar = (ProgressBar)control;
            Type targetType = typeof(ProgressBar);

            RemoveValueChanged(ProgressBar.IsIndeterminateProperty, targetType, progressBar, UpdateStateHandler);
        }


        /// <summary>
        ///     Called to update the control's visual state.
        /// </summary>
        /// <param name="control">The instance of the control being updated.</param>
        /// <param name="useTransitions">Whether to use transitions or not.</param>
        protected override void UpdateState(Control control, bool useTransitions)
        {
            ProgressBar progressBar = (ProgressBar)control;

            if (!progressBar.IsIndeterminate)
            {
                VisualStateManager.GoToState(progressBar, "Determinate", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(progressBar, "Indeterminate", useTransitions);
            }

            base.UpdateState(control, useTransitions);
        }
    }
}
