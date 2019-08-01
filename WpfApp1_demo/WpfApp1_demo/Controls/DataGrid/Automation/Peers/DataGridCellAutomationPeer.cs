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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using AvePoint.Migrator.Common.Controls;

namespace Microsoft.Windows.Automation.Peers
{
    /// <summary>
    /// AutomationPeer for DataGridCell
    /// </summary>
    public sealed class DataGridCellAutomationPeer : FrameworkElementAutomationPeer
    {
        #region Constructors

        /// <summary>
        /// AutomationPeer for DataGridCell.
        /// This automation peer should not be part of the automation tree.
        /// It should act as a wrapper peer for DataGridCellItemAutomationPeer
        /// </summary>
        /// <param name="owner">DataGridCell</param>
        public DataGridCellAutomationPeer(DataGridCell owner)
            : base(owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            UpdateEventSource();
        }

        #endregion
        
        #region AutomationPeer Overrides

        /// <summary>
        /// Gets the control type for the element that is associated with the UI Automation peer.
        /// </summary>
        /// <returns>The control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Custom;
        }

        /// <summary>
        /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType, 
        /// differentiates the control represented by this AutomationPeer.
        /// </summary>
        /// <returns>The string that contains the name.</returns>
        protected override string GetClassNameCore()
        {
            return Owner.GetType().Name;
        }

        #endregion

        #region Private Helpers
        private void UpdateEventSource()
        {
            DataGridCell cell = (DataGridCell)Owner;
            DataGrid dataGrid = cell.DataGridOwner;
            if (dataGrid != null)
            {
                DataGridAutomationPeer dataGridAutomationPeer = CreatePeerForElement(dataGrid) as DataGridAutomationPeer;
                if (dataGridAutomationPeer != null)
                {
                    DataGridItemAutomationPeer itemAutomationPeer = dataGridAutomationPeer.GetOrCreateItemPeer(cell.DataContext);
                    if (itemAutomationPeer != null)
                    {
                        DataGridCellItemAutomationPeer cellItemAutomationPeer = itemAutomationPeer.GetOrCreateCellItemPeer(cell.Column);
                        this.EventsSource = cellItemAutomationPeer;
                    }
                }
            }
        }
        #endregion
    }
}
