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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AvePoint.Migrator.Common.Controls
{
    /// <summary>
    ///     A column definition that allows a developer to specify specific
    ///     editing and non-editing templates.
    /// </summary>
    public class DataGridTemplateColumn : DataGridColumn
    {
        #region Constructors

        static DataGridTemplateColumn()
        {
            CanUserSortProperty.OverrideMetadata(
                typeof(DataGridTemplateColumn), 
                new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceTemplateColumnCanUserSort)));
            SortMemberPathProperty.OverrideMetadata(
                typeof(DataGridTemplateColumn),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTemplateColumnSortMemberPathChanged)));
        }

        public DataGridTemplateColumn()
        {
            CoerceValue(CanUserSortProperty);
        }

        #endregion

        #region Auto Sort

        private static void OnTemplateColumnSortMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGridTemplateColumn column = (DataGridTemplateColumn)d;
            column.CoerceValue(CanUserSortProperty);
        }

        private static object OnCoerceTemplateColumnCanUserSort(DependencyObject d, object baseValue)
        {
            DataGridTemplateColumn templateColumn = (DataGridTemplateColumn)d;
            if (string.IsNullOrEmpty(templateColumn.SortMemberPath))
            {
                return false;
            }

            return DataGridColumn.OnCoerceCanUserSort(d, baseValue);
        }

        #endregion

        #region Templates

        /// <summary>
        ///     A template describing how to display data for a cell in this column.
        /// </summary>
        public DataTemplate CellTemplate
        {
            get { return (DataTemplate)GetValue(CellTemplateProperty); }
            set { SetValue(CellTemplateProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty representing the CellTemplate property.
        /// </summary>
        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register(
                                                                            "CellTemplate", 
                                                                            typeof(DataTemplate), 
                                                                            typeof(DataGridTemplateColumn),
                                                                            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataGridColumn.NotifyPropertyChangeForRefreshContent)));

        /// <summary>
        ///     A template selector describing how to display data for a cell in this column.
        /// </summary>
        public DataTemplateSelector CellTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CellTemplateSelectorProperty); }
            set { SetValue(CellTemplateSelectorProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty representing the CellTemplateSelector property.
        /// </summary>
        public static readonly DependencyProperty CellTemplateSelectorProperty = DependencyProperty.Register(
                                                                                    "CellTemplateSelector", 
                                                                                    typeof(DataTemplateSelector), 
                                                                                    typeof(DataGridTemplateColumn),
                                                                                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataGridColumn.NotifyPropertyChangeForRefreshContent)));

        /// <summary>
        ///     A template describing how to display data for a cell 
        ///     that is being edited in this column.
        /// </summary>
        public DataTemplate CellEditingTemplate
        {
            get { return (DataTemplate)GetValue(CellEditingTemplateProperty); }
            set { SetValue(CellEditingTemplateProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty representing the CellEditingTemplate
        /// </summary>
        public static readonly DependencyProperty CellEditingTemplateProperty = DependencyProperty.Register(
                                                                                    "CellEditingTemplate", 
                                                                                    typeof(DataTemplate), 
                                                                                    typeof(DataGridTemplateColumn),
                                                                                    new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataGridColumn.NotifyPropertyChangeForRefreshContent)));

        /// <summary>
        ///     A template selector describing how to display data for a cell 
        ///     that is being edited in this column.
        /// </summary>
        public DataTemplateSelector CellEditingTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CellEditingTemplateSelectorProperty); }
            set { SetValue(CellEditingTemplateSelectorProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty representing the CellEditingTemplateSelector
        /// </summary>
        public static readonly DependencyProperty CellEditingTemplateSelectorProperty = DependencyProperty.Register(
                                                                                            "CellEditingTemplateSelector", 
                                                                                            typeof(DataTemplateSelector), 
                                                                                            typeof(DataGridTemplateColumn), 
                                                                                            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(DataGridColumn.NotifyPropertyChangeForRefreshContent)));

        /// <summary>
        ///     Returns either the specified CellTemplate or CellEditingTemplate.
        ///     CellTemplate is returned if CellEditingTemplate is null.
        /// </summary>
        /// <param name="isEditing">Whether the editing template is requested.</param>
        private DataTemplate ChooseCellTemplate(bool isEditing)
        {
            DataTemplate template = null;

            if (isEditing)
            {
                template = CellEditingTemplate;
            }

            if (template == null)
            {
                template = CellTemplate;
            }

            return template;
        }

        /// <summary>
        ///     Returns either the specified CellTemplateSelector or CellEditingTemplateSelector.
        ///     CellTemplateSelector is returned if CellEditingTemplateSelector is null.
        /// </summary>
        /// <param name="isEditing">Whether the editing template selector is requested.</param>
        private DataTemplateSelector ChooseCellTemplateSelector(bool isEditing)
        {
            DataTemplateSelector templateSelector = null;

            if (isEditing)
            {
                templateSelector = CellEditingTemplateSelector;
            }

            if (templateSelector == null)
            {
                templateSelector = CellTemplateSelector;
            }

            return templateSelector;
        }

        #endregion

        #region Visual Tree Generation

        /// <summary>
        ///     Creates the visual tree that will become the content of a cell.
        /// </summary>
        /// <param name="isEditing">Whether the editing version is being requested.</param>
        /// <param name="dataItem">The data item for the cell.</param>
        /// <param name="cell">The cell container that will receive the tree.</param>
        private FrameworkElement LoadTemplateContent(bool isEditing, object dataItem, DataGridCell cell)
        {
            DataTemplate template = ChooseCellTemplate(isEditing);
            DataTemplateSelector templateSelector = ChooseCellTemplateSelector(isEditing);
            if (template != null || templateSelector != null)
            {
                ContentPresenter contentPresenter = new ContentPresenter();
                BindingOperations.SetBinding(contentPresenter, ContentPresenter.ContentProperty, new Binding());
                contentPresenter.ContentTemplate = template;
                contentPresenter.ContentTemplateSelector = templateSelector;
                return contentPresenter;
            }

            return null;
        }

        /// <summary>
        ///     Creates the visual tree that will become the content of a cell.
        /// </summary>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return LoadTemplateContent(/* isEditing = */ false, dataItem, cell);
        }

        /// <summary>
        ///     Creates the visual tree that will become the content of a cell.
        /// </summary>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return LoadTemplateContent(/* isEditing = */ true, dataItem, cell);
        }

        #endregion

        #region Property Changed Handler

        /// <summary>
        /// Override which handles property
        /// change for template properties
        /// </summary>
        /// <param name="element"></param>
        /// <param name="propertyName"></param>
        protected internal override void RefreshCellContent(FrameworkElement element, string propertyName)
        {
            DataGridCell cell = element as DataGridCell;
            if (cell != null)
            {
                bool isCellEditing = cell.IsEditing;

                if ((!isCellEditing &&
                        ((string.Compare(propertyName, "CellTemplate", StringComparison.Ordinal) == 0) ||
                        (string.Compare(propertyName, "CellTemplateSelector", StringComparison.Ordinal) == 0))) ||
                    (isCellEditing &&
                        ((string.Compare(propertyName, "CellEditingTemplate", StringComparison.Ordinal) == 0) ||
                        (string.Compare(propertyName, "CellEditingTemplateSelector", StringComparison.Ordinal) == 0))))
                {
                    cell.BuildVisualTree();
                    return;
                }
            }

            base.RefreshCellContent(element, propertyName);
        }

        #endregion
    }
}
