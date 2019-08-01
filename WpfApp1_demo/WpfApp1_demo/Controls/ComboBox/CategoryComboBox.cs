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

using System.Windows;
using System.Windows.Controls;
using MigratorTool.WPF.View.NotesTool.Home;

namespace AvePoint.Migrator.Common.Controls
{
    public class CategoryComboBox : ComboBox
    {
        public CategoryComboBox()
        {
            this.DefaultStyleKey = typeof(CategoryComboBox);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.SelectedBoxItemToString = this.SelectedItem.ToString();

            foreach (var item in e.AddedItems)
            {
                var categoryItem = item as CategoryItem<GroupName>;
                if (categoryItem != null)
                {
                    categoryItem.IsSelected = true;
                    categoryItem.RaiseIsSelectedPropertyChanged();
                }
            }

            foreach (var item in e.RemovedItems)
            {
                var categoryItem = item as CategoryItem<GroupName>;
                if (categoryItem != null)
                {
                    categoryItem.IsSelected = false;
                    categoryItem.RaiseIsSelectedPropertyChanged();
                }
            }

            base.OnSelectionChanged(e);
        }

        public string SelectedBoxItemToString
        {
            get
            {
                return GetValue(SelectedBoxItemToStringProperty) as string;
            }
            set
            {
                SetValue(SelectedBoxItemToStringProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedBoxItemToStringProperty
            = DependencyProperty.Register("SelectedBoxItemToString", typeof(string), typeof(CategoryComboBox));
    }
}
