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
using System.Windows;
using System.Windows.Controls;

namespace AvePoint.Migrator.Common.Controls
{
    public class ButtonChrome : ContentControl
    {
        #region    ==Properties==

        #region    ==CornerRadius==

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonChrome), new UIPropertyMetadata(default(CornerRadius), new PropertyChangedCallback(OnCornerRadiusChanged)));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        private static void OnCornerRadiusChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnCornerRadiusChanged((CornerRadius)e.OldValue, (CornerRadius)e.NewValue);
        }

        /// <summary>
        /// Ensure the InnerBorderRadius to be one less than the CornerRadius
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            CornerRadius newInnerCornerRadius = new CornerRadius(Math.Max(0, newValue.TopLeft - 1),
                                                                 Math.Max(0, newValue.TopRight - 1),
                                                                 Math.Max(0, newValue.BottomRight - 1),
                                                                 Math.Max(0, newValue.BottomLeft - 1));
            InnerCornerRadius = newInnerCornerRadius;
        }

        #endregion ==CornerRadius==

        #region    ==InnerCornerRadius==

        public static readonly DependencyProperty InnerCornerRadiusProperty = DependencyProperty.Register("InnerCornerRadius", typeof(CornerRadius), typeof(ButtonChrome), new UIPropertyMetadata(default(CornerRadius), new PropertyChangedCallback(OnInnerCornerRadiusChanged)));
        public CornerRadius InnerCornerRadius
        {
            get { return (CornerRadius)GetValue(InnerCornerRadiusProperty); }
            set { SetValue(InnerCornerRadiusProperty, value); }
        }

        private static void OnInnerCornerRadiusChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnInnerCornerRadiusChanged((CornerRadius)e.OldValue, (CornerRadius)e.NewValue);
        }

        protected virtual void OnInnerCornerRadiusChanged(CornerRadius oldValue, CornerRadius newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==InnerCornerRadius==

        #region    ==RenderChecked==

        public static readonly DependencyProperty RenderCheckedProperty = DependencyProperty.Register("RenderChecked", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderCheckedChanged));
        public bool RenderChecked
        {
            get { return (bool)GetValue(RenderCheckedProperty); }
            set { SetValue(RenderCheckedProperty, value); }
        }

        private static void OnRenderCheckedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderCheckedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderCheckedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderChecked==

        #region    ==RenderEnabled==

        public static readonly DependencyProperty RenderEnabledProperty = DependencyProperty.Register("RenderEnabled", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(true, OnRenderEnabledChanged));
        public bool RenderEnabled
        {
            get
            {
                return (bool)GetValue(RenderEnabledProperty);
            }
            set
            {
                SetValue(RenderEnabledProperty, value);
            }
        }

        private static void OnRenderEnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderEnabledChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderEnabledChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderEnabled==

        #region    ==RenderFocused==

        public static readonly DependencyProperty RenderFocusedProperty = DependencyProperty.Register("RenderFocused", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderFocusedChanged));
        public bool RenderFocused
        {
            get
            {
                return (bool)GetValue(RenderFocusedProperty);
            }
            set
            {
                SetValue(RenderFocusedProperty, value);
            }
        }

        private static void OnRenderFocusedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderFocusedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderFocusedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderFocused==

        #region    ==RenderMouseOver==

        public static readonly DependencyProperty RenderMouseOverProperty = DependencyProperty.Register("RenderMouseOver", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderMouseOverChanged));
        public bool RenderMouseOver
        {
            get
            {
                return (bool)GetValue(RenderMouseOverProperty);
            }
            set
            {
                SetValue(RenderMouseOverProperty, value);
            }
        }

        private static void OnRenderMouseOverChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderMouseOverChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderMouseOverChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderMouseOver==

        #region    ==RenderNormal==

        public static readonly DependencyProperty RenderNormalProperty = DependencyProperty.Register("RenderNormal", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(true, OnRenderNormalChanged));
        public bool RenderNormal
        {
            get
            {
                return (bool)GetValue(RenderNormalProperty);
            }
            set
            {
                SetValue(RenderNormalProperty, value);
            }
        }

        private static void OnRenderNormalChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderNormalChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderNormalChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderNormal==

        #region    ==RenderPressed==

        public static readonly DependencyProperty RenderPressedProperty = DependencyProperty.Register("RenderPressed", typeof(bool), typeof(ButtonChrome), new UIPropertyMetadata(false, OnRenderPressedChanged));
        public bool RenderPressed
        {
            get
            {
                return (bool)GetValue(RenderPressedProperty);
            }
            set
            {
                SetValue(RenderPressedProperty, value);
            }
        }

        private static void OnRenderPressedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChrome buttonChrome = o as ButtonChrome;
            if (buttonChrome != null)
                buttonChrome.OnRenderPressedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnRenderPressedChanged(bool oldValue, bool newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion ==RenderPressed==

        #endregion ==Properties==

        #region    ==Contsructors==

        static ButtonChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonChrome), new FrameworkPropertyMetadata(typeof(ButtonChrome)));
        }

        #endregion ==Contsructors==
    }
}
