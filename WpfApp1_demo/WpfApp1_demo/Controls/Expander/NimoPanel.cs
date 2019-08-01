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
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    public class NimoPanel : Panel
    {
        #region == Members ==

        /// <summary>
        /// 当前展开项对应的序号(有效序号从0开始)
        /// </summary>
        private int _mExpandIndex = -1;

        /// <summary>
        /// 保存KelpExpander控件，标题部分的高度
        /// </summary>
        private List<double> _mKelpHeaderHeight = new List<double>();

        #endregion

        #region == MeasureOverride ==

        protected override Size MeasureOverride(Size availableSize)
        {
            Size childrenSize = new Size(0, 0);
            _mKelpHeaderHeight.Clear();

            foreach (FrameworkElement child in Children)
            {
                child.Measure(availableSize);

                _mKelpHeaderHeight.Add(child.DesiredSize.Height);

                //将最宽子元素的宽度，作为Panel的宽度
                childrenSize.Width = childrenSize.Width > child.DesiredSize.Width ? childrenSize.Width : child.DesiredSize.Width;
                //子元素的高度和，作为Panel的高度
                MappingExpander ke = child as MappingExpander;
                if (ke != null && ke.IsExpanded && child.DesiredSize.Height < ke.ActualHeight)
                {
                    childrenSize.Height += ke.ActualHeight;
                }
                else
                {
                    childrenSize.Height += child.DesiredSize.Height;
                }

                if (!Double.IsInfinity(availableSize.Height))
                {
                    childrenSize.Height = availableSize.Height;
                }
            }
            return childrenSize;
        }

        #endregion

        #region == ArrangeOverride ==

        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElementCollection children = Children;
            if (children != null)
            {
                Point point = new Point(0, 0);

                for (int i = 0; i < children.Count; i++)
                {
                    if (i > 0)
                    {
                        MappingExpander ke = children[i - 1] as MappingExpander;

                        if (ke != null)
                        {
                            if (ke.IsExpanded)
                            {
                                point.Y += finalSize.Height - GetOtherKelpHeights(i - 1);
                            }
                            else
                            {
                                point.Y += children[i - 1].DesiredSize.Height;
                            }
                        }
                    }

                    MappingExpander current = children[i] as MappingExpander;
                    if (current != null)
                    {
                        if (current.IsExpanded && _mExpandIndex != i)
                        {
                            _mExpandIndex = i;
                            TurnOffOtherExpandKelp(children, _mExpandIndex);
                        }
                    }

                    double height = finalSize.Height - GetOtherKelpHeights(i) > 0 ? finalSize.Height - GetOtherKelpHeights(i) : children[i].DesiredSize.Height;

                    children[i].Arrange(new Rect(point, new Size(finalSize.Width, height)));
                }

            }

            return base.ArrangeOverride(finalSize);
        }

        #endregion

        #region == Utility ==

        /// <summary>
        /// 计算expandIndex对应展开项以外的，其他子项的高度和
        /// </summary>
        /// <param name="expandIndex">展开项的序号</param>
        /// <returns>应展开项以外的，其他子项的高度和</returns>
        private double GetOtherKelpHeights(int expandIndex)
        {
            double returnValue = 0;

            for (int i = 0; i < _mKelpHeaderHeight.Count; i++)
            {
                if (i == expandIndex)
                {
                    continue;
                }
                else
                {
                    returnValue += _mKelpHeaderHeight[i];
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将expandIndex对应展开项以外的，所有子项进行关闭
        /// </summary>
        /// <param name="children">Panel包含的子项集合</param>
        /// <param name="expandIndex">展开项对应的序号</param>
        private void TurnOffOtherExpandKelp(UIElementCollection children, int expandIndex)
        {
            return;
            for (int i = 0; i < children.Count; i++)
            {
                if (i == expandIndex)
                {
                    continue;
                }
                else
                {
                    MappingExpander ke = children[i] as MappingExpander;
                    if (ke != null)
                    {
                        ke.IsExpanded = false;
                    }
                }
            }
        }
        #endregion
    }
}
