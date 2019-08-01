﻿/********************************************************************
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

using System.Windows.Controls;

namespace System.Windows
{
    /// <summary>
    ///     EventArgs for VisualStateGroup.CurrentStateChanging and CurrentStateChanged events.
    /// </summary>
    public sealed class VisualStateChangedEventArgs : EventArgs
    {
        internal VisualStateChangedEventArgs(VisualState oldState, VisualState newState, Control control)
        {
            _oldState = oldState;
            _newState = newState;
            _control = control;
        }

        /// <summary>
        ///     The old state the control is transitioning from
        /// </summary>
        public VisualState OldState
        {
            get
            {
                return _oldState;
            }
        }

        /// <summary>
        ///     The new state the control is transitioning to
        /// </summary>
        public VisualState NewState
        {
            get
            {
                return _newState;
            }
        }

        /// <summary>
        ///     The control involved in the state change
        /// </summary>
        public Control Control
        {
            get
            {
                return _control;
            }
        }

        private VisualState _oldState;
        private VisualState _newState;
        private Control _control;
    }
}
