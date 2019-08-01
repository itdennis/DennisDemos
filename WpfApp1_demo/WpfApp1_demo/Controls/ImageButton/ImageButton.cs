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
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MigratorTool.WPF.View.Common.MigratorLogger;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplateVisualState(Name = VisualStates.StateFocused, GroupName = VisualStates.GroupFocus)]
    [TemplateVisualState(Name = VisualStates.StateUnfocused, GroupName = VisualStates.GroupFocus)]
    public class ImageButton : ButtonBase
    {
        private static Logger logger = Logger.CreateInstance();
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        /// <summary>
        /// 作为呈现不同状态图片的容器。
        /// </summary>
        private Image UIImageButton;

        //各种状态对应的图片源。
        private BitmapImage NormalImage;
        private BitmapImage DisableImage;
        private BitmapImage MouseOverImage;
        private BitmapImage MouseDownImage;

        //public ImageButton()
        //{
        //    IsTabStop = true;
        //}

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UIImageButton = this.GetTemplateChild("PhotoBrushName") as Image;
            AutomationProperties.SetName(this.UIImageButton, AutomationProperties.GetName(this));
            this.IsEnabledChanged -= new DependencyPropertyChangedEventHandler(AUImageButton_IsEnabledChanged);
            this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(AUImageButton_IsEnabledChanged);
            //InitCurrentUri();
            this.CurrentImage = ProposeCandidateImage(IsEnabled);
            if (UIImageButton != null)
            {
                UIImageButton.Source = CurrentImage;
            }
        }

        public void FireClickEvent()
        {
            this.OnClick();
        }

        #region == ImageStretchProperty ==
        /// <summary>
        /// 用来控制图片的伸缩情况。
        /// </summary>
        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageStretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.Uniform));

        #endregion == ImageStretchProperty ==

        #region == NormalUri ==
        /// <summary>
        /// 代表正常状态下Button的背景图片的Uri
        /// </summary>
        public string NormalUri
        {
            get { return (string)GetValue(NormalUriProperty); }
            set { SetValue(NormalUriProperty, value); }
        }

        public static readonly DependencyProperty NormalUriProperty = DependencyProperty.Register("NormalUri", typeof(string), typeof(ImageButton), new PropertyMetadata(null, NormalUriPropertyChanged));

        private static void NormalUriPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ImageButton button = obj as ImageButton;
            string newValue = args.NewValue as string;
            button.NormalImage = null;
            button.NormalImage = button.GetImageSourceByString(newValue);
            button.CurrentImage = button.NormalImage;
        }
        #endregion == NormalUri ==

        #region == MouseOverUri ==
        /// <summary>
        /// 代表MouseOver时Button的背景图片的Uri
        /// </summary>
        public string MouseOverUri
        {
            get { return (string)GetValue(MouseOverUriProperty); }
            set { SetValue(MouseOverUriProperty, value); }
        }

        public static readonly DependencyProperty MouseOverUriProperty = DependencyProperty.Register("MouseOverUri", typeof(string), typeof(ImageButton), new PropertyMetadata(null, OnMouseOverUriPropertyChanged));

        private static void OnMouseOverUriPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ImageButton button = obj as ImageButton;
            string newValue = args.NewValue as string;
            button.MouseOverImage = button.GetImageSourceByString(newValue);
        }
        #endregion == MouseOverUri ==

        #region == MouseDownUri ==
        /// <summary>
        /// 代表MouseDown时Button的背景图片的Uri
        /// </summary>
        public string MouseDownUri
        {
            get { return (string)GetValue(MouseDownUriProperty); }
            set { SetValue(MouseDownUriProperty, value); }
        }

        public static readonly DependencyProperty MouseDownUriProperty = DependencyProperty.Register("MouseDownUri", typeof(string), typeof(ImageButton), new PropertyMetadata(null, OnMouseDownUriPropertyChanged));

        private static void OnMouseDownUriPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ImageButton button = obj as ImageButton;
            string newValue = args.NewValue as string;
            button.MouseDownImage = button.GetImageSourceByString(newValue);
        }
        #endregion == MouseDownUri ==

        #region == DisableUri ==
        /// <summary>
        /// 代表Disable状态下Button的背景图片的Uri
        /// </summary>
        public string DisableUri
        {
            get { return (string)GetValue(DisableUriProperty); }
            set { SetValue(DisableUriProperty, value); }
        }

        public static readonly DependencyProperty DisableUriProperty = DependencyProperty.Register("DisableUri", typeof(string), typeof(ImageButton), new PropertyMetadata(DisableUriPropertyChanged));

        private static void DisableUriPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ImageButton button = obj as ImageButton;
            string newValue = args.NewValue as string;
            button.DisableImage = button.GetImageSourceByString(newValue);
        }
        #endregion == DisableUri ==

        #region ==CurrentImage==

        //在恢复CurrentUri的属性值时用来防止二次加载图片。
        private int _currentUriNestedLevel = 0;

        /// <summary>
        /// 当前的图片源。
        /// </summary>


        public BitmapImage CurrentImage
        {
            get { return (BitmapImage)GetValue(CurrentImageProperty); }
            set { SetValue(CurrentImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentImageProperty =
            DependencyProperty.Register("CurrentImage", typeof(BitmapImage), typeof(ImageButton), new PropertyMetadata(null, new PropertyChangedCallback(OnCurrentImagePropertyChanged)));


        private static void OnCurrentImagePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ImageButton button = obj as ImageButton;
            BitmapImage oldValue = args.OldValue as BitmapImage;
            BitmapImage newValue = args.NewValue as BitmapImage;
            //if (button._currentUriNestedLevel == 0)
            //{
            //    if (button.UImageButton == null || (newValue == null && oldValue != null))
            //    {
            //        button._currentUriNestedLevel++;
            //        button.SetValue(CurrentImageProperty, oldValue); //Revert to old value.
            //        button._currentUriNestedLevel--;
            //    }
            //    else if (newValue != null)
            //    {
            //        button.UImageButton.Source = newValue;
            //    }
            //}
            if (button.UIImageButton != null && args.NewValue != null)
            {
                button.UIImageButton.Source = newValue;
            }
        }
        #endregion ==CurrentImage==

        #region == Util Methods ==

        /// <summary>
        /// 通过图片的Uri加载图片。
        /// </summary>
        /// <param name="sourseString"></param>
        /// <returns></returns>
        private BitmapImage GetImageSourceByString(string sourseString)
        {
            BitmapImage image = null;
            if (!string.IsNullOrEmpty(sourseString))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    //StreamResourceInfo info = Application.GetRemoteStream(new Uri(sourseString, UriKind.Relative));
                    //bitmap.StreamSource = info.Stream;
                    bitmap.UriSource = new Uri(@"pack://application:,,,/Resource" + sourseString, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Debug, "An error occurred while get image source . Reason:{0}", ex.ToString());
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} is a invalid uri to be a image source.\n", sourseString) + ex.ToString());
                }
            }
            return image;
        }

        /// <summary>
        ///  从现有的所有图片中选取一个可用的图片。
        /// </summary>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        private BitmapImage ProposeCandidateImage(bool isEnable)
        {
            BitmapImage proposedImage = isEnable ? NormalImage : DisableImage;
            if (proposedImage != null)
            {
                return proposedImage;
            }
            if (MouseOverImage != null)
            {
                return MouseOverImage;
            }
            if (MouseDownImage != null)
            {
                return MouseDownImage;
            }
            if (NormalImage != null)
            {
                return NormalImage;
            }
            if (DisableImage != null)
            {
                return DisableImage;
            }
            return null;
        }

        #endregion == Util Methods ==

        /// <summary>
        ///  IsEnable属性改变处理函数。
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void AUImageButton_IsEnabledChanged(object o, DependencyPropertyChangedEventArgs e)
        {
            ImageButton button = o as ImageButton;
            bool newValue = (bool)e.NewValue;
            button.CurrentImage = ProposeCandidateImage(newValue);
        }

        #region == Mouse Event Handler ==

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (MouseOverImage != null)
            {
                this.CurrentImage = MouseOverImage;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (NormalImage != null && this.IsEnabled)
            {
                this.CurrentImage = NormalImage;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (MouseDownImage != null)
            {
                this.CurrentImage = MouseDownImage;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (IsEnabled && MouseOverImage != null)
            {
                this.CurrentImage = MouseOverImage;
            }
        }

        #endregion == Mouse Event Handler ==

        #region == FocusState ==
        /// <summary>
        ///  显示当前控件是否被聚焦，
        ///  只读属性。
        /// </summary>
        public new bool IsFocused { get; private set; }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            IsFocused = true;
            UpdateStates(true);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsFocused = false;
            UpdateStates(true);
        }

        private void UpdateStates(bool useTransitions)
        {
            if (IsFocused)
            {
                VisualStateManager.GoToState(this, VisualStates.StateFocused, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, VisualStates.StateUnfocused, useTransitions);
            }
        }

        #endregion == FocusState ==

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ImageButtonAutomationPeer(this);
        }
    }

    public class ImageButtonAutomationPeer : FrameworkElementAutomationPeer
    {
        public ImageButtonAutomationPeer(ImageButton owner)
            : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }

    }
}
