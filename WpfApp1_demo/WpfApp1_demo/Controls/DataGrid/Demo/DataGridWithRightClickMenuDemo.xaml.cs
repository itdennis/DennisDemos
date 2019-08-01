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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using MigratorTool.WPF.ViewModel.Common;

namespace MigratorTool.WPF.View.Controls.DataGrid.Demo
{
    /// <summary>
    /// Interaction logic for DataGridWithRightClickMenuDemo.xaml
    /// </summary>
    public partial class DataGridWithRightClickMenuDemo : UserControl
    {
        public DataGridWithRightClickMenuDemo()
        {
            InitializeComponent();
            this.DataContext = new DataGridWithRightClickMenuDemoViewModel();
        }

        private class DataGridWithRightClickMenuDemoViewModel
        {
            public ObservableCollection<DataGridWithRightClickMenuDemoItem> ItemsSource { get; set; }

            public DataGridWithRightClickMenuDemoViewModel()
            {
                ItemsSource = new ObservableCollection<DataGridWithRightClickMenuDemoItem> { };
                for (int i = 0; i < 10; i++)
                {
                    var item = new DataGridWithRightClickMenuDemoItem()
                    {
                        Name = i.ToString(),
                        Size = (i * 2).ToString() + "MB",
                    };
                    item.Deleting += (it) => this.ItemsSource.Remove(it);
                    ItemsSource.Add(item);
                }
            }
        }

        private class DataGridWithRightClickMenuDemoItem
        {
            public ICommand CopyComamnd { get { return null; } }
            public ICommand DeleteComamnd
            {
                get
                {
                    return new DelegateCommand(() => Deleting.Execute(this));
                }
            }
            public event Action<DataGridWithRightClickMenuDemoItem> Deleting;
            public ICommand AlertComamnd
            {
                get
                {
                    return new DelegateCommand(() => new Alert.Confirm(AvePoint.Migrator.Common.Controls.AlertType.Info, this.Name, null).Show());
                }
            }
            public string Name { get; set; }
            public string Size { get; set; }
        }
    }
}
