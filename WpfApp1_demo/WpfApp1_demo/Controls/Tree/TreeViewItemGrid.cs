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

namespace AvePoint.Migrator.Common.Controls
{
    #region ==using==
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using MigratorTool.WPF.I18N;
    using MigratorTool.WPF.View.Controls.Tree;
    using MigratorTool.WPF.ViewModel.Common;
    #endregion

    class TreeViewItemGrid : Grid
    {
        public TreeViewItemGrid()
        {
            if (this.RefreshRightClickMenuVisible)
            {
                var refreshItem = new MenuItem
                {
                    Command = this.RefreshCommand,
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/Resource/Image/Common/refresh_16x16.png", UriKind.RelativeOrAbsolute)),
                        Width = 16,
                        Height = 16
                    },
                    Header = I18NEntity.Get("LN_a4dc12e8_83bc_4ead_810d_5e8bf9dcec20"),
                };

                refreshItem.Click += (s, e) =>
                {
                    if (RefreshClick != null)
                    {
                        var viewModel = this.DataContext as MigrationTreeNodeModel;
                        if (viewModel != null)
                        {
                            viewModel.Children.Clear();
                            viewModel.IsLoaded = false;
                        }
                        RefreshClick(viewModel, new RoutedEventArgs());
                    }
                };
                this.ContextMenu = new ContextMenu();
                this.ContextMenu.Items.Add(refreshItem);
            }

            this.MouseLeftButtonDown += (s, e) =>
            {
                if (RefreshClick != null)
                {
                    RefreshClick(this.DataContext as MigrationTreeNodeModel, new RoutedEventArgs());
                }
                if (this.RefreshCommand != null)
                {
                    this.RefreshCommand.Execute(this.DataContext as MigrationTreeNodeModel);
                }
            };
        }

        public static readonly DependencyProperty RefreshRightClickMenuVisibleProperty = DependencyProperty.Register("RefreshRightClickMenuVisible", typeof(bool), typeof(TreeViewItemGrid), new PropertyMetadata(true));
        public bool RefreshRightClickMenuVisible
        {
            get { return (bool)GetValue(RefreshRightClickMenuVisibleProperty); }
            set { SetValue(RefreshRightClickMenuVisibleProperty, value); }
        }

        public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register("RefreshCommand", typeof(DelegateCommand<TreeViewItemGrid>), typeof(TreeViewItemGrid));
        public DelegateCommand<TreeViewItemGrid> RefreshCommand
        {
            get { return GetValue(RefreshCommandProperty) as DelegateCommand<TreeViewItemGrid>; }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public event RoutedEventHandler RefreshClick;
    }
}