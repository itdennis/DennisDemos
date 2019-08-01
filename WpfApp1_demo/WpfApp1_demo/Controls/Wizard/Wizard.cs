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
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    #endregion

    public class Wizard : ItemsControl
    {
        static Wizard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Wizard), new FrameworkPropertyMetadata(typeof(Wizard)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var root = this.ItemsPanel.LoadContent() as Grid;
            for (int i = 0; i < this.Items.Count; i++)
            {
                root.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (var item in this.Items)
            {
                var wizard = item as WizardItem;
                Grid.SetColumn(wizard, AllItems.Count);
                AllItems.Add(wizard);
            }
        }

        List<WizardItem> AllItems = new List<WizardItem>();

        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(Wizard), new PropertyMetadata(1, (d, e) => 
            {
                if (d != null)
                {
                    (d as Wizard).UpdateItemsStatus((int)e.NewValue);
                }
            }));

        private void UpdateItemsStatus(int gotoIndex)
        {
            for (int i = 0; i < gotoIndex - 1; i++)
            {
                var item = AllItems[i];
                item.StepButtonStatus = StepButtonStatus.Configured;
            }
            AllItems[gotoIndex - 1].StepButtonStatus = StepButtonStatus.Selected;
            for (int i = gotoIndex; i < AllItems.Count; i++)
            {
                AllItems[i].StepButtonStatus = StepButtonStatus.Normal;
            }
        }
    }
}
