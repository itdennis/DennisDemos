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
using System.Windows;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     Holds all of the information that we need to attach to row items so that we can restore rows when they're devirtualized.
    /// </summary>
    internal class DataGridItemAttachedStorage
    {
        public void SetValue(object item, DependencyProperty property, object value)
        {
            var map = EnsureItem(item);
            map[property] = value;
        }
        
        public bool TryGetValue(object item, DependencyProperty property, out object value)
        {
            value = null;
            Dictionary<DependencyProperty, object> map;
            
            EnsureItemStorageMap();
            if (_itemStorageMap.TryGetValue(item, out map))
            {
                return map.TryGetValue(property, out value);
            }

            return false;
        }

        public void ClearValue(object item, DependencyProperty property)
        {
            Dictionary<DependencyProperty, object> map;

            EnsureItemStorageMap();
            if (_itemStorageMap.TryGetValue(item, out map))
            {
                map.Remove(property);
            }
        }

        public void ClearItem(object item)
        {
            EnsureItemStorageMap();
            _itemStorageMap.Remove(item);
        }

        public void Clear()
        {
            _itemStorageMap = null;
        }

        private void EnsureItemStorageMap()
        {
            if (_itemStorageMap == null)
            {
                _itemStorageMap = new Dictionary<object, Dictionary<DependencyProperty, object>>();
            }
        }

        private Dictionary<DependencyProperty, object> EnsureItem(object item)
        {
            Dictionary<DependencyProperty, object> map;
            
            EnsureItemStorageMap();
            if (!_itemStorageMap.TryGetValue(item, out map))
            {
                map = new Dictionary<DependencyProperty, object>();
                _itemStorageMap[item] = map;
            }

            return map;
        }

        /// <summary>
        ///     A map between row items and the associated data.
        /// </summary>
        private Dictionary<object, Dictionary<DependencyProperty, object>> _itemStorageMap;        
    }
}
