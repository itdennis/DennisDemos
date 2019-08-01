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
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Workaround for Dev10 Bug 527138 UIElement.RaiseEvent(e) throws InvalidCastException when 
    ///     e is of type SelectionChangedEventArgs 
    ///     e.RoutedEvent was registered with a handler not of type System.Windows.Controls.SelectionChangedEventHandler
    /// </summary>
    internal class CalendarSelectionChangedEventArgs : SelectionChangedEventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventId">Routed Event</param>
        /// <param name="removedItems">Items removed from selection</param>
        /// <param name="addedItems">Items added to selection</param>
        public CalendarSelectionChangedEventArgs(RoutedEvent eventId, IList removedItems, IList addedItems) :
            base(eventId, removedItems, addedItems)
        {
        }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            EventHandler<SelectionChangedEventArgs> handler = genericHandler as EventHandler<SelectionChangedEventArgs>;
            if (handler != null)
            {
                handler(genericTarget, this);
            }
            else
            {
                base.InvokeEventHandler(genericHandler, genericTarget);
            }
        }
    }
}
