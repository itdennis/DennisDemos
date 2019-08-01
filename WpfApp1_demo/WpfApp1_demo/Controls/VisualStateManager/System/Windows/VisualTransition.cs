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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace System.Windows
{
    /// <summary>
    /// Defines a transition between VisualStates.
    /// </summary>
    [ContentProperty("Storyboard")]
    public class VisualTransition : DependencyObject
    {
        public VisualTransition()
        {
            DynamicStoryboardCompleted = true;
            ExplicitStoryboardCompleted = true;
        }

        /// <summary>
        /// Name of the state to transition from.
        /// </summary>
        public string From 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Name of the state to transition to.
        /// </summary>
        public string To 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Storyboard providing fine grained control of the transition.
        /// </summary>
        public Storyboard Storyboard 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Duration of the transition.
        /// </summary>
        [TypeConverter(typeof(System.Windows.DurationConverter))]
        public Duration GeneratedDuration 
        { 
            get { return _generatedDuration; } 
            set { _generatedDuration = value; } 
        }

        internal bool IsDefault
        {
            get { return From == null && To == null; }
        }

        internal bool DynamicStoryboardCompleted
        {
            get;
            set;
        }

        internal bool ExplicitStoryboardCompleted
        {
            get;
            set;
        }

        private Duration _generatedDuration = new Duration(new TimeSpan());
    }
}
