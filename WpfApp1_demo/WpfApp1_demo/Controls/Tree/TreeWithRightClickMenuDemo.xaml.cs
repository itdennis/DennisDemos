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

using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MigratorTool.WPF.View.Controls.Tree
{
    /// <summary>
    /// Interaction logic for TreeWithRightClickMenuDemo.xaml
    /// </summary>
    public partial class TreeWithRightClickMenuDemo : UserControl
    {
        public TreeWithRightClickMenuDemo()
        {
            InitializeComponent();
            this.DataContext = new TreeItemsSource
            {
                TreeItems = new ObservableCollection<TreeItem> 
                {
                    new TreeItem { Name = "First Level", Children =new ObservableCollection<TreeItem>{new TreeItem{Name = "Second"}}}
                }
            };
        }

        private class TreeItemsSource
        {
            public ObservableCollection<TreeItem> TreeItems { get; set; }
        }

        private class TreeItem
        {
            public string Name { get; set; }
            public ObservableCollection<TreeItem> Children { get; set; }
        }
    }
}
