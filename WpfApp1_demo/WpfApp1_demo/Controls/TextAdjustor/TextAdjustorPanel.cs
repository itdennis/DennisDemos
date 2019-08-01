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
    /// <summary>
    /// 布局逻辑与<see cref="System.Windows.Controls.StackPanel"/>类似的自定义<see cref="System.Windows.Controls.Panel"/>控件，
    /// 增加了换行的功能。
    /// </summary>
    public class TextAdjustorPanel : Panel
    {
        #region == Members ==

        private readonly List<double> mMaxHeights = new List<double>();
        private bool mIsOutside = false;

        #endregion == Members ==


        #region == Properties ==

        #region == ElementWrapping ==

        /// <summary>
        /// 获取或设置是否进行换行。
        /// </summary>
        public ElementWrapping ElementWrapping
        {
            get { return (ElementWrapping)GetValue(ElementWrappingProperty); }
            set { SetValue(ElementWrappingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElementWrapping.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementWrappingProperty =
            DependencyProperty.Register("ElementWrapping", typeof(ElementWrapping), typeof(TextAdjustorPanel), new PropertyMetadata(ElementWrapping.NoWrap, OnElementWrappingPropertyChanged));

        private static void OnElementWrappingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextAdjustorPanel self = d as TextAdjustorPanel;
            if (self != null)
            {
                self.UpdateLayout();
            }
        }

        #endregion == ElementWrapping ==

        #region == VerticalAlignmentInLine ==

        /// <summary>
        /// 获取或设置一个值，该值表示在某一行的所有元素高度不一致时，控制高度比较小的元素相对于行高的内部位置。
        /// </summary>
        public VerticalAlignmentInLine VerticalAlignmentInLine
        {
            get { return (VerticalAlignmentInLine)GetValue(VerticalAlignmentInLineProperty); }
            set { SetValue(VerticalAlignmentInLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalAlignmentInLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalAlignmentInLineProperty =
            DependencyProperty.Register("VerticalAlignmentInLine", typeof(VerticalAlignmentInLine), typeof(TextAdjustorPanel), new PropertyMetadata(VerticalAlignmentInLine.Center, OnVerticalAlignmentInLinePropertyChanged));

        private static void OnVerticalAlignmentInLinePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextAdjustorPanel self = d as TextAdjustorPanel;
            if (self != null)
            {
                self.UpdateLayout();
            }
        }

        #endregion == VerticalAlignmentInLine ==

        #endregion == Properties ==


        #region == Methods ==

        #region == Measure ==

        /// <summary>
        /// 测量子元素在布局中所需的大小，然后确定派生类的大小。
        /// </summary>
        /// <param name="availableSize">此元素可以赋给子元素的可用大小。</param>
        /// <returns>此元素在布局过程中所需的大小，这是由此元素根据对其子元素大小的计算而确定的。</returns>
        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            mMaxHeights.Clear();
            mIsOutside = false;

            Size size = new Size();
            double width = 0.0;
            double height = 0.0;
            foreach (UIElement element in Children)
            {
                element.Measure(availableSize);

                if (ElementWrapping == ElementWrapping.NoWrap ||
                    width + element.DesiredSize.Width <= availableSize.Width)
                {
                    width += element.DesiredSize.Width;
                    height = Math.Max(height, element.DesiredSize.Height);
                }
                else
                {
                    size.Width = Math.Max(width, size.Width);
                    size.Height += height;

                    mMaxHeights.Add(height);

                    width = element.DesiredSize.Width;
                    height = element.DesiredSize.Height;
                }
            }

            mMaxHeights.Add(height);
            size.Width = Math.Max(width, size.Width);
            size.Height += height;

            return size;
        }

        #endregion == Measure ==

        #region == Arrange ==

        /// <summary>
        /// 排列<see cref="AUIStackPanel"/>中的内容。
        /// </summary>
        /// <param name="finalSize">父级中此元素应用来排列自身及其子元素的最终区域。</param>
        /// <returns><see cref="AUIStackPanel"/>使用的实际大小</returns>
        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            Rect rect = new Rect();
            int i = 0;
            double height = 0;

            foreach (UIElement element in Children)
            {
                rect.Width = element.DesiredSize.Width;
                rect.Height = element.DesiredSize.Height;

                if (mIsOutside)
                {
                    rect.X = 0;
                    rect.Y = 0;
                    rect.Width = 0;
                    rect.Height = 0;
                }
                else if (rect.X + element.DesiredSize.Width > finalSize.Width)
                {
                    if (ElementWrapping == ElementWrapping.NoWrap)
                    {
                        rect.X = 0;
                        rect.Y = 0;
                        rect.Width = 0;
                        rect.Height = 0;
                        mIsOutside = true;
                    }
                    else if (height + mMaxHeights[i] >= finalSize.Height)
                    {
                        rect.X = 0;
                        rect.Y = 0;
                        rect.Width = 0;
                        rect.Height = 0;
                        mIsOutside = true;
                    }
                    else
                    {
                        rect.X = 0;
                        height += mMaxHeights[i];
                        i++;
                    }
                }

                switch (VerticalAlignmentInLine)
                {
                    case VerticalAlignmentInLine.Top:
                        rect.Y = height;
                        break;
                    case VerticalAlignmentInLine.Center:
                        rect.Y = height + (mMaxHeights[i] - element.DesiredSize.Height) / 2;
                        break;
                    case VerticalAlignmentInLine.Bottom:
                        rect.Y = height + mMaxHeights[i] - element.DesiredSize.Height;
                        break;
                }

                element.Arrange(rect);

                rect.X += element.DesiredSize.Width;
            }

            return base.ArrangeOverride(finalSize);
        }

        #endregion == Arrange ==

        #endregion == Mothods ==
    }

    /// <summary>
    /// <see cref="AUIStackPanel"/>控件的回行策略枚举。
    /// </summary>
    public enum ElementWrapping
    {
        /// <summary>
        /// 不能回行
        /// </summary>
        NoWrap = 0,
        /// <summary>
        /// 可以回行
        /// </summary>
        Wrap = 1
    }

    /// <summary>
    /// 控制<see cref="AUIStackPanel"/>中每一行的元素相对于行高的内部位置的枚举。
    /// </summary>
    public enum VerticalAlignmentInLine
    {
        /// <summary>
        /// 在行中，紧贴行的上边缘。
        /// </summary>
        Top = 0,
        /// <summary>
        /// 在行中，中间的位置。
        /// </summary>
        Center = 1,
        /// <summary>
        /// 在行中，紧贴行的下边缘。
        /// </summary>
        Bottom = 2
    }
}
