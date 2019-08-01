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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    /// The event args class to be used with AutoGeneratingColumn event.
    /// </summary>
    public class DataGridAutoGeneratingColumnEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <param name="column"></param>
        public DataGridAutoGeneratingColumnEventArgs(string propertyName, Type propertyType, DataGridColumn column) :
            this(column, propertyName, propertyType, null)
        {
        }

        internal DataGridAutoGeneratingColumnEventArgs(DataGridColumn column, ItemPropertyInfo itemPropertyInfo) : 
            this(column, itemPropertyInfo.Name, itemPropertyInfo.PropertyType, itemPropertyInfo.Descriptor)
        {
        }

        internal DataGridAutoGeneratingColumnEventArgs(
            DataGridColumn column,
            string propertyName,
            Type propertyType,
            object propertyDescriptor)
        {
            _column = column;
            _propertyName = propertyName;
            _propertyType = propertyType;
            PropertyDescriptor = propertyDescriptor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Column which is being generated
        /// </summary>
        public DataGridColumn Column
        {
            get
            {
                return _column;
            }

            set
            {
                _column = value;
            }
        }

        /// <summary>
        /// Property for which the column is getting generated
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        /// <summary>
        /// Type of the property for which the column is getting generated
        /// </summary>
        public Type PropertyType
        {
            get
            {
                return _propertyType;
            }
        }

        /// <summary>
        /// Descriptor of the property for which the column is gettign generated
        /// </summary>
        public object PropertyDescriptor
        {
            get
            {
                return _propertyDescriptor;
            }

            private set
            {
                if (value == null)
                {
                    _propertyDescriptor = null;
                }
                else
                {
                    Debug.Assert(
                        typeof(PropertyDescriptor).IsAssignableFrom(value.GetType()) ||
                        typeof(PropertyInfo).IsAssignableFrom(value.GetType()),
                        "Property descriptor should be either a PropertyDescriptor or a PropertyInfo");
                    _propertyDescriptor = value;
                }
            }
        }

        /// <summary>
        /// Flag to indicated if generation of this column has to be cancelled
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

        #endregion

        #region Data

        private DataGridColumn _column;
        private string _propertyName;
        private Type _propertyType;
        private object _propertyDescriptor;
        private bool _cancel;

        #endregion
    }
}
