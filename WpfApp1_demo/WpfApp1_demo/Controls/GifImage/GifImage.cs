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
using System.IO;
using System.Net;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Resources;
using System.Diagnostics.CodeAnalysis;

namespace AvePoint.Migrator.Common.Controls
{
    public class GifImageExceptionRoutedEventArgs : RoutedEventArgs
    {
        public Exception ErrorException;

        public GifImageExceptionRoutedEventArgs(RoutedEvent routedEvent, object obj)
            : base(routedEvent, obj)
        {
        }
    }

    class WebReadState
    {
        public WebRequest webRequest;
        public MemoryStream memoryStream;
        public Stream readStream;
        public byte[] buffer;
    }


    public class GifImage : ContentControl
    {
        private GifAnimation gifAnimation = null;

        private Image image = null;

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        public static readonly DependencyProperty ForceGifAnimProperty = DependencyProperty.Register("ForceGifAnim", typeof(bool), typeof(GifImage), new FrameworkPropertyMetadata(false));
        public bool ForceGifAnim
        {
            get
            {
                return (bool)this.GetValue(ForceGifAnimProperty);
            }
            set
            {
                this.SetValue(ForceGifAnimProperty, value);
            }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(GifImage), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnSourceChanged)));
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GifImage obj = (GifImage)d;
            string s = (string)e.NewValue;
            if (s != null)
            {
                obj.CreateFromSourceString(s);
            }
        }
        public string Source
        {
            get
            {
                return (string)this.GetValue(SourceProperty);
            }
            set
            {
                this.SetValue(SourceProperty, value);
            }
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(GifImage), new FrameworkPropertyMetadata(Stretch.Fill, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnStretchChanged)));
        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GifImage obj = (GifImage)d;
            Stretch s = (Stretch)e.NewValue;
            if (obj.gifAnimation != null)
            {
                obj.gifAnimation.Stretch = s;
            }
            else if (obj.image != null)
            {
                obj.image.Stretch = s;
            }
        }
        public Stretch Stretch
        {
            get
            {
                return (Stretch)this.GetValue(StretchProperty);
            }
            set
            {
                this.SetValue(StretchProperty, value);
            }
        }

        public static readonly DependencyProperty StretchDirectionProperty = DependencyProperty.Register("StretchDirection", typeof(StretchDirection), typeof(GifImage), new FrameworkPropertyMetadata(StretchDirection.Both, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnStretchDirectionChanged)));
        private static void OnStretchDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GifImage obj = (GifImage)d;
            StretchDirection s = (StretchDirection)e.NewValue;
            if (obj.gifAnimation != null)
            {
                obj.gifAnimation.StretchDirection = s;
            }
            else if (obj.image != null)
            {
                obj.image.StretchDirection = s;
            }
        }
        public StretchDirection StretchDirection
        {
            get
            {
                return (StretchDirection)this.GetValue(StretchDirectionProperty);
            }
            set
            {
                this.SetValue(StretchDirectionProperty, value);
            }
        }

        public delegate void ExceptionRoutedEventHandler(object sender, GifImageExceptionRoutedEventArgs args);

        public static readonly RoutedEvent ImageFailedEvent = EventManager.RegisterRoutedEvent("ImageFailed", RoutingStrategy.Bubble, typeof(ExceptionRoutedEventHandler), typeof(GifImage));

        public event ExceptionRoutedEventHandler ImageFailed
        {
            add
            {
                AddHandler(ImageFailedEvent, value);
            }
            remove
            {
                RemoveHandler(ImageFailedEvent, value);
            }
        }

        void image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            RaiseImageFailedEvent(e.ErrorException);
        }

        void RaiseImageFailedEvent(Exception exp)
        {
            GifImageExceptionRoutedEventArgs newArgs = new GifImageExceptionRoutedEventArgs(ImageFailedEvent, this);
            newArgs.ErrorException = exp;
            RaiseEvent(newArgs);
        }

        private void DeletePreviousImage()
        {
            if (image != null)
            {
                this.RemoveLogicalChild(image);
                image = null;
            }
            if (gifAnimation != null)
            {
                this.RemoveLogicalChild(gifAnimation);
                gifAnimation = null;
            }
        }

        private void CreateNonGifAnimationImage()
        {
            image = new Image();
            image.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(image_ImageFailed);
            ImageSource src = (ImageSource)(new ImageSourceConverter().ConvertFromString(Source));
            image.Source = src;
            image.Stretch = Stretch;
            image.StretchDirection = StretchDirection;
            this.AddChild(image);
        }

        private void CreateGifAnimation(MemoryStream memoryStream)
        {
            gifAnimation = new GifAnimation();
            gifAnimation.CreateGifAnimation(memoryStream);
            gifAnimation.Stretch = Stretch;
            gifAnimation.StretchDirection = StretchDirection;
            this.AddChild(gifAnimation);
        }

        private void CreateFromSourceString(string source)
        {
            DeletePreviousImage();
            Uri uri;

            try
            {
                uri = new Uri(@"pack://application:,,,/Resource" + source, UriKind.RelativeOrAbsolute);
                GetGifStreamFromPack(uri);
                return;
            }
            catch (Exception exp)
            {
                RaiseImageFailedEvent(exp);
                return;
            }

            //if (source.Trim().ToUpper().EndsWith(".GIF") || ForceGifAnim)
            //{
            //    if (!uri.IsAbsoluteUri)
            //    {
            //        GetGifStreamFromPack(uri);
            //    }
            //    else
            //    {
            //        string leftPart = uri.GetLeftPart(UriPartial.Scheme);

            //        if (leftPart == "http://" || leftPart == "ftp://" || leftPart == "file://")
            //        {
            //            GetGifStreamFromHttp(uri);
            //        }
            //        else if (leftPart == "./")
            //        {
            //            GetGifStreamFromPack(uri);
            //        }
            //        else
            //        {
            //            CreateNonGifAnimationImage();
            //        }
            //    }
            //}
            //else
            //{
            //    CreateNonGifAnimationImage();
            //}
        }

        private delegate void WebRequestFinishedDelegate(MemoryStream memoryStream);

        private void WebRequestFinished(MemoryStream memoryStream)
        {
            CreateGifAnimation(memoryStream);
        }

        private delegate void WebRequestErrorDelegate(Exception exp);

        private void WebRequestError(Exception exp)
        {
            RaiseImageFailedEvent(exp);
        }

        private void WebResponseCallback(IAsyncResult asyncResult)
        {
            WebReadState webReadState = (WebReadState)asyncResult.AsyncState;
            WebResponse webResponse;
            try
            {
                webResponse = webReadState.webRequest.EndGetResponse(asyncResult);
                webReadState.readStream = webResponse.GetResponseStream();
                webReadState.buffer = new byte[100000];
                webReadState.readStream.BeginRead(webReadState.buffer, 0, webReadState.buffer.Length, new AsyncCallback(WebReadCallback), webReadState);
            }
            catch (WebException exp)
            {
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new WebRequestErrorDelegate(WebRequestError), exp);
            }
        }

        private void WebReadCallback(IAsyncResult asyncResult)
        {
            WebReadState webReadState = (WebReadState)asyncResult.AsyncState;
            int count = webReadState.readStream.EndRead(asyncResult);
            if (count > 0)
            {
                webReadState.memoryStream.Write(webReadState.buffer, 0, count);
                try
                {
                    webReadState.readStream.BeginRead(webReadState.buffer, 0, webReadState.buffer.Length, new AsyncCallback(WebReadCallback), webReadState);
                }
                catch (WebException exp)
                {
                    //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new WebRequestErrorDelegate(WebRequestError), exp);
                }
            }
            else
            {
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new WebRequestFinishedDelegate(WebRequestFinished), webReadState.memoryStream);
            }
        }

        private void GetGifStreamFromHttp(Uri uri)
        {
            try
            {
                WebReadState webReadState = new WebReadState();
                webReadState.memoryStream = new MemoryStream();
                webReadState.webRequest = WebRequest.Create(uri);
                webReadState.webRequest.Timeout = 10000;

                webReadState.webRequest.BeginGetResponse(new AsyncCallback(WebResponseCallback), webReadState);
            }
            catch (SecurityException)
            {
                CreateNonGifAnimationImage();
            }
        }

        private void ReadGifStreamSynch(Stream s)
        {
            byte[] gifData;
            MemoryStream memoryStream;
            using (s)
            {
                memoryStream = new MemoryStream((int)s.Length);
                BinaryReader br = new BinaryReader(s);
                gifData = br.ReadBytes((int)s.Length);
                memoryStream.Write(gifData, 0, (int)s.Length);
                memoryStream.Flush();
            }
            CreateGifAnimation(memoryStream);
        }

        [SuppressMessage("FxCopCustomRules", "C100007:SpellCheckStringValues", Justification = "These are special tag in MigratorTool-Control")]
        private void GetGifStreamFromPack(Uri uri)
        {
            try
            {
                StreamResourceInfo streamInfo;

                if (!uri.IsAbsoluteUri)
                {
                    streamInfo = Application.GetContentStream(uri);
                    if (streamInfo == null)
                    {
                        streamInfo = Application.GetResourceStream(uri);
                    }
                }
                else
                {
                    if (uri.GetLeftPart(UriPartial.Authority).Contains("siteoforigin"))
                    {
                        streamInfo = Application.GetRemoteStream(uri);
                    }
                    else
                    {
                        streamInfo = Application.GetContentStream(uri);
                        if (streamInfo == null)
                        {
                            streamInfo = Application.GetResourceStream(uri);
                        }
                    }
                }
                if (streamInfo == null)
                {
                    throw new FileNotFoundException("Resource not found.", uri.ToString());
                }
                ReadGifStreamSynch(streamInfo.Stream);
            }
            catch (Exception exp)
            {
                RaiseImageFailedEvent(exp);
            }
        }
    }
}
