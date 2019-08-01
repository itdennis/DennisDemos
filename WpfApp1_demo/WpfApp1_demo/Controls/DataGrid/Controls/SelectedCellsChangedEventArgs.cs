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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Communicates which cells were added or removed from the SelectedCells collection.
    /// </summary>
    public class SelectedCellsChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of this class.
        /// </summary>
        /// <param name="addedCells">The cells that were added. Must be non-null, but may be empty.</param>
        /// <param name="removedCells">The cells that were removed. Must be non-null, but may be empty.</param>
        public SelectedCellsChangedEventArgs(List<DataGridCellInfo> addedCells, List<DataGridCellInfo> removedCells)
        {
            if (addedCells == null)
            {
                throw new ArgumentNullException("addedCells");
            }

            if (removedCells == null)
            {
                throw new ArgumentNullException("removedCells");
            }

            _addedCells = addedCells.AsReadOnly();
            _removedCells = removedCells.AsReadOnly();
        }

        /// <summary>
        ///     Creates a new instance of this class.
        /// </summary>
        /// <param name="addedCells">The cells that were added. Must be non-null, but may be empty.</param>
        /// <param name="removedCells">The cells that were removed. Must be non-null, but may be empty.</param>
        public SelectedCellsChangedEventArgs(ReadOnlyCollection<DataGridCellInfo> addedCells, ReadOnlyCollection<DataGridCellInfo> removedCells)
        {
            if (addedCells == null)
            {
                throw new ArgumentNullException("addedCells");
            }

            if (removedCells == null)
            {
                throw new ArgumentNullException("removedCells");
            }

            _addedCells = addedCells;
            _removedCells = removedCells;
        }

        internal SelectedCellsChangedEventArgs(DataGrid owner, VirtualizedCellInfoCollection addedCells, VirtualizedCellInfoCollection removedCells)
        {
            _addedCells = (addedCells != null) ? addedCells : VirtualizedCellInfoCollection.MakeEmptyCollection(owner);
            _removedCells = (removedCells != null) ? removedCells : VirtualizedCellInfoCollection.MakeEmptyCollection(owner);

            Debug.Assert(_addedCells.IsReadOnly, "_addedCells should have ended up as read-only.");
            Debug.Assert(_removedCells.IsReadOnly, "_removedCells should have ended up as read-only.");
        }

        /// <summary>
        ///     The cells that were added.
        /// </summary>
        public IList<DataGridCellInfo> AddedCells
        {
            get { return _addedCells; }
        }

        /// <summary>
        ///     The cells that were removed.
        /// </summary>
        public IList<DataGridCellInfo> RemovedCells
        {
            get { return _removedCells; }
        }

        private IList<DataGridCellInfo> _addedCells;
        private IList<DataGridCellInfo> _removedCells;
    }
}
