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

//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
//---------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// Event args used for Column reordering event raised after column header drag-drop
    /// </summary>
    public class DataGridColumnReorderingEventArgs : DataGridColumnEventArgs
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataGridColumn"></param>
        public DataGridColumnReorderingEventArgs(DataGridColumn dataGridColumn)
            : base(dataGridColumn)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property to specify whether the Reordering operation should be cancelled
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _cancel;
            }

            set
            {
                _cancel = value;
            }
        }

        /// <summary>
        /// The control which would be used as an indicator for drop location during column header drag drop
        /// </summary>
        public Control DropLocationIndicator
        {
            get
            {
                return _dropLocationIndicator;
            }

            set
            {
                _dropLocationIndicator = value;
            }
        }

        /// <summary>
        /// The control which would be used as a drag indicator during column header drag drop.
        /// </summary>
        public Control DragIndicator
        {
            get
            {
                return _dragIndicator;
            }

            set
            {
                _dragIndicator = value;
            }
        }

        #endregion

        #region Data

        private bool _cancel;
        private Control _dropLocationIndicator;
        private Control _dragIndicator;

        #endregion
    }
}
