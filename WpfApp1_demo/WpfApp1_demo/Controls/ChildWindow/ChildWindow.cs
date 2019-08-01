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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AvePoint.Migrator.Common.Controls
{
    [TemplatePart(Name = PART_DragWidget, Type = typeof(Border))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_WindowRoot, Type = typeof(Grid))]
    [TemplatePart(Name = PART_Root, Type = typeof(Grid))]
    public class ChildWindow : ContentControl
    {
        private const string PART_DragWidget = "PART_DragWidget";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_WindowRoot = "PART_WindowRoot";
        private const string PART_Root = "PART_Root";
        private const string PART_Content = "Content";

        #region Private Members

        private Grid _root;
        private TranslateTransform _moveTransform = new TranslateTransform();
        private bool _startupPositionInitialized;
        private bool _isMouseCaptured;
        private Point _clickPoint;
        private Point _oldPosition;
        private Border _dragWidget;
        private FrameworkElement _parentContainer;
        private Rectangle _modalLayer = new Rectangle();
        private Canvas _modalLayerPanel = new Canvas();

        private const int _horizaontalOffset = 3;
        private const int _verticalOffset = 3;

        bool _ignorePropertyChanged;

        #endregion //Private Members

        #region Properties

        #region Internal Properties

        internal Grid WindowRoot
        {
            get;
            private set;
        }
        internal Thumb DragWidget
        {
            get;
            private set;
        }
        internal Button MinimizeButton
        {
            get;
            private set;
        }
        internal Button MaximizeButton
        {
            get;
            private set;
        }
        internal ImageButton CloseButton
        {
            get;
            private set;
        }

        #endregion //Internal Properties

        #region Public Properties

        #region Caption
        public object Caption
        {
            get
            {
                return (object)GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(object), typeof(ChildWindow), new UIPropertyMetadata(String.Empty));
        #endregion //Caption

        #region CaptionForeground

        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(ChildWindow), new UIPropertyMetadata(null));
        public Brush CaptionForeground
        {
            get
            {
                return (Brush)GetValue(CaptionForegroundProperty);
            }
            set
            {
                SetValue(CaptionForegroundProperty, value);
            }
        }

        #endregion //CaptionForeground

        #region    CaptionVerticalAlignment

        public HorizontalAlignment CaptionHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(CaptionHorizontalAlignmentProperty); }
            set { SetValue(CaptionHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaptionHorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionHorizontalAlignmentProperty =
            DependencyProperty.Register("CaptionHorizontalAlignment", typeof(HorizontalAlignment), typeof(ChildWindow), new PropertyMetadata(HorizontalAlignment.Left));

        #endregion CaptionVerticalAlignment

        #region    CaptionFontSize

        public double CaptionFontSize
        {
            get { return (double)GetValue(CaptionFontSizeProperty); }
            set { SetValue(CaptionFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaptionFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionFontSizeProperty =
            DependencyProperty.Register("CaptionFontSize", typeof(double), typeof(ChildWindow), new PropertyMetadata(15.0));

        #endregion CaptionFontSize

        #region CloseButtonStyle

        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register("CloseButtonStyle", typeof(Style), typeof(ChildWindow), new PropertyMetadata(null));
        public Style CloseButtonStyle
        {
            get
            {
                return (Style)GetValue(CloseButtonStyleProperty);
            }
            set
            {
                SetValue(CloseButtonStyleProperty, value);
            }
        }

        #endregion //CloseButtonStyle

        #region CloseButtonVisibility

        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(ChildWindow), new PropertyMetadata(Visibility.Visible));
        public Visibility CloseButtonVisibility
        {
            get
            {
                return (Visibility)GetValue(CloseButtonVisibilityProperty);
            }
            set
            {
                SetValue(CloseButtonVisibilityProperty, value);
            }
        }

        #endregion //CloseButtonVisibility

        #region DialogResult

        private bool? _dialogResult;
        /// <summary>
        /// Gets or sets a value indicating whether the ChildWindow was accepted or canceled.
        /// </summary>
        /// <value>
        /// True if the child window was accepted; false if the child window was
        /// canceled. The default is null.
        /// </value>
        [TypeConverter(typeof(NullableBoolConverter))]
        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    Close();
                }
            }
        }

        #endregion //DialogResult

        #region DesignerWindowState

        public static readonly DependencyProperty DesignerWindowStateProperty = DependencyProperty.Register("DesignerWindowState", typeof(WindowState), typeof(ChildWindow), new PropertyMetadata(WindowState.Closed, OnDesignerWindowStatePropertyChanged));
        public WindowState DesignerWindowState
        {
            get
            {
                return (WindowState)GetValue(DesignerWindowStateProperty);
            }
            set
            {
                SetValue(DesignerWindowStateProperty, value);
            }
        }

        private static void OnDesignerWindowStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = d as ChildWindow;
            if (childWindow != null)
                childWindow.OnDesignerWindowStatePropertyChanged((WindowState)e.OldValue, (WindowState)e.NewValue);
        }

        protected virtual void OnDesignerWindowStatePropertyChanged(WindowState oldValue, WindowState newValue)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Visibility = newValue == WindowState.Open ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion //DesignerWindowState

        #region FocusedElement

        public static readonly DependencyProperty FocusedElementProperty = DependencyProperty.Register("FocusedElement", typeof(FrameworkElement), typeof(ChildWindow), new UIPropertyMetadata(null));
        public FrameworkElement FocusedElement
        {
            get
            {
                return (FrameworkElement)GetValue(FocusedElementProperty);
            }
            set
            {
                SetValue(FocusedElementProperty, value);
            }
        }

        #endregion

        #region IsModal

        public static readonly DependencyProperty IsModalProperty = DependencyProperty.Register("IsModal", typeof(bool), typeof(ChildWindow), new UIPropertyMetadata(true, new PropertyChangedCallback(OnIsModalPropertyChanged)));
        public bool IsModal
        {
            get
            {
                return (bool)GetValue(IsModalProperty);
            }
            set
            {
                SetValue(IsModalProperty, value);
            }
        }

        private static void OnIsModalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = d as ChildWindow;
            if (childWindow != null)
                childWindow.OnIsModalChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void OnIsModalChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
                ShowModalLayer();
            }
            else
            {
                KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Continue);
                HideModalLayer();
            }
        }

        #endregion //IsModal

        #region Left

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof(double), typeof(ChildWindow), new PropertyMetadata(0.0, new PropertyChangedCallback(OnLeftPropertyChanged)));
        public double Left
        {
            get
            {
                return (double)GetValue(LeftProperty);
            }
            set
            {
                SetValue(LeftProperty, value);
            }
        }

        private static void OnLeftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = obj as ChildWindow;
            if (childWindow != null)
                childWindow.OnLeftPropertyChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnLeftPropertyChanged(double oldValue, double newValue)
        {
            Left = GetRestrictedLeft();
            ProcessMove(newValue - oldValue, 0);
        }

        #endregion //Left

        #region OverlayBrush

        public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register("OverlayBrush", typeof(Brush), typeof(ChildWindow), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0)), OnOverlayBrushChanged));
        public Brush OverlayBrush
        {
            get
            {
                return (Brush)GetValue(OverlayBrushProperty);
            }
            set
            {
                SetValue(OverlayBrushProperty, value);
            }
        }

        private static void OnOverlayBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = d as ChildWindow;
            if (childWindow != null)
                childWindow.OnOverlayBrushChanged((Brush)e.OldValue, (Brush)e.NewValue);
        }

        protected virtual void OnOverlayBrushChanged(Brush oldValue, Brush newValue)
        {
            _modalLayer.Fill = newValue;
        }

        #endregion //OverlayBrush

        #region OverlayOpacity

        public static readonly DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(ChildWindow), new PropertyMetadata(0.2, OnOverlayOpacityChanged));
        public double OverlayOpacity
        {
            get
            {
                return (double)GetValue(OverlayOpacityProperty);
            }
            set
            {
                SetValue(OverlayOpacityProperty, value);
            }
        }

        private static void OnOverlayOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = d as ChildWindow;
            if (childWindow != null)
                childWindow.OnOverlayOpacityChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnOverlayOpacityChanged(double oldValue, double newValue)
        {
            _modalLayer.Opacity = newValue;
        }

        #endregion //OverlayOpacity

        #region Top

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof(double), typeof(ChildWindow), new PropertyMetadata(0.0, new PropertyChangedCallback(OnTopPropertyChanged)));
        public double Top
        {
            get
            {
                return (double)GetValue(TopProperty);
            }
            set
            {
                SetValue(TopProperty, value);
            }
        }

        private static void OnTopPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = obj as ChildWindow;
            if (childWindow != null)
                childWindow.OnTopPropertyChanged((double)e.OldValue, (double)e.NewValue);
        }

        private void OnTopPropertyChanged(double oldValue, double newValue)
        {
            Top = GetRestrictedTop();
            ProcessMove(0, newValue - oldValue);
        }

        #endregion //TopProperty

        #region WindowBackground

        public static readonly DependencyProperty WindowBackgroundProperty = DependencyProperty.Register("WindowBackground", typeof(Brush), typeof(ChildWindow), new PropertyMetadata(null));
        public Brush WindowBackground
        {
            get
            {
                return (Brush)GetValue(WindowBackgroundProperty);
            }
            set
            {
                SetValue(WindowBackgroundProperty, value);
            }
        }

        #endregion //WindowBackground

        #region WindowBorderBrush

        public static readonly DependencyProperty WindowBorderBrushProperty = DependencyProperty.Register("WindowBorderBrush", typeof(Brush), typeof(ChildWindow), new PropertyMetadata(null));
        public Brush WindowBorderBrush
        {
            get
            {
                return (Brush)GetValue(WindowBorderBrushProperty);
            }
            set
            {
                SetValue(WindowBorderBrushProperty, value);
            }
        }

        #endregion //WindowBorderBrush

        #region WindowOpacity

        public static readonly DependencyProperty WindowOpacityProperty = DependencyProperty.Register("WindowOpacity", typeof(double), typeof(ChildWindow), new PropertyMetadata(null));
        public double WindowOpacity
        {
            get
            {
                return (double)GetValue(WindowOpacityProperty);
            }
            set
            {
                SetValue(WindowOpacityProperty, value);
            }
        }

        #endregion //WindowOpacity

        #region WindowStartupLocation

        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.Register("WindowStartupLocation", typeof(WindowStartupLocation), typeof(ChildWindow), new UIPropertyMetadata(WindowStartupLocation.Center, OnWindowStartupLocationChanged));
        public WindowStartupLocation WindowStartupLocation
        {
            get
            {
                return (WindowStartupLocation)GetValue(WindowStartupLocationProperty);
            }
            set
            {
                SetValue(WindowStartupLocationProperty, value);
            }
        }

        private static void OnWindowStartupLocationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = o as ChildWindow;
            if (childWindow != null)
                childWindow.OnWindowStartupLocationChanged((WindowStartupLocation)e.OldValue, (WindowStartupLocation)e.NewValue);
        }

        protected virtual void OnWindowStartupLocationChanged(WindowStartupLocation oldValue, WindowStartupLocation newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion //WindowStartupLocation

        #region WindowState

        public WindowState WindowState
        {
            get
            {
                return (WindowState)GetValue(WindowStateProperty);
            }
            set
            {
                SetValue(WindowStateProperty, value);
            }
        }

        public static readonly DependencyProperty WindowStateProperty = DependencyProperty.Register("WindowState", typeof(WindowState), typeof(ChildWindow), new FrameworkPropertyMetadata(WindowState.Closed, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnWindowStatePropertyChanged));

        private static void OnWindowStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChildWindow childWindow = d as ChildWindow;
            if (childWindow != null)
                childWindow.OnWindowStatePropertyChanged((WindowState)e.OldValue, (WindowState)e.NewValue);
        }

        protected virtual void OnWindowStatePropertyChanged(WindowState oldValue, WindowState newValue)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (!_ignorePropertyChanged)
                    SetWindowState(newValue);
            }
            else
            {
                Visibility = DesignerWindowState == WindowState.Open ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
        }

        #endregion //WindowState

        #endregion //Public Properties

        #endregion Properties

        #region Constructors

        static ChildWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChildWindow), new FrameworkPropertyMetadata(typeof(ChildWindow)));
        }

        public ChildWindow()
        {
            DesignerWindowState = WindowState.Open;
            IsTabStop = Focusable = false;
            this.Loaded += (s, e) =>
            {
                if (_parentContainer != null)
                {
                    _parentContainer.LayoutUpdated -= ParentContainer_LayoutUpdated;
                    _parentContainer.SizeChanged -= ParentContainer_SizeChanged;
                    _parentContainer.LayoutUpdated += ParentContainer_LayoutUpdated;
                    _parentContainer.SizeChanged += ParentContainer_SizeChanged;
                }
                this.CenterChildWindow();
                if (this.CloseButton != null)
                {
                    this.CloseButton.Focus();
                }
            };

            IsVisibleChanged -= ChildWindow_IsVisibleChanged;
            IsVisibleChanged += ChildWindow_IsVisibleChanged;

            _modalLayer.Fill = OverlayBrush;
            _modalLayer.Opacity = OverlayOpacity;

            this.Unloaded += ChildWindow_Unloaded;
        }

        void ChildWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_parentContainer != null)
            {
                _parentContainer.LayoutUpdated -= ParentContainer_LayoutUpdated;
                _parentContainer.SizeChanged -= ParentContainer_SizeChanged;
            }
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_dragWidget != null)
            {
                _dragWidget.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(HeaderLeftMouseButtonDown));
                _dragWidget.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(HeaderMouseLeftButtonUp));
                _dragWidget.MouseMove -= (o, e) => HeaderMouseMove(e);
            }

            _dragWidget = GetTemplateChild(PART_DragWidget) as Border;

            if (_dragWidget != null)
            {
                _dragWidget.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(HeaderLeftMouseButtonDown), true);
                _dragWidget.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(HeaderMouseLeftButtonUp), true);
                _dragWidget.MouseMove += (o, e) => HeaderMouseMove(e);
            }

            CloseButton = GetTemplateChild(PART_CloseButton) as ImageButton;

            if (CloseButton != null)
            {
                CloseButton.Click += (o, e) => Close();
            }

            WindowRoot = GetTemplateChild(PART_WindowRoot) as Grid;
            if (WindowRoot != null)
            {
                WindowRoot.RenderTransform = _moveTransform;
            }
            //_parentContainer = VisualTreeHelper.GetParent(this) as FrameworkElement;
            _parentContainer = MigrationVisualTreeHelper.GetAppRootVisual() as FrameworkElement;
            if (_parentContainer != null)
            {
                _parentContainer.LayoutUpdated -= ParentContainer_LayoutUpdated;
                _parentContainer.SizeChanged -= ParentContainer_SizeChanged;
                _parentContainer.LayoutUpdated += ParentContainer_LayoutUpdated;
                _parentContainer.SizeChanged += ParentContainer_SizeChanged;
            }
            //initialize our modal background width/height
            _modalLayer.Height = _parentContainer.ActualHeight;
            _modalLayer.Width = _parentContainer.ActualWidth;

            //this is for XBAP applications only. When inside an XBAP the parent container has no height or width until it has loaded. Therefore
            //we need to handle the loaded event and reposition the window.
            if (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
            {
                _parentContainer.Loaded += (o, e) =>
                {
                    ExecuteOpen();
                };
            }

            _root = GetTemplateChild(PART_Root) as Grid;

            var childWindowContent = GetTemplateChild(PART_Content) as ContentPresenter;
            if (childWindowContent != null)
            {
                childWindowContent.SizeChanged += (s, e) => this.CenterChildWindow();
            }

#if VS2008
      FocusVisualStyle = null;
#else
            Style focusStyle = _root.Resources["FocusVisualStyle"] as Style;
            if (focusStyle != null)
            {
                Setter focusStyleDataContext = new Setter(Control.DataContextProperty, this);
                focusStyle.Setters.Add(focusStyleDataContext);
                FocusVisualStyle = focusStyle;
            }
#endif
            if (_root != null)
            {
                _root.Children.Add(_modalLayerPanel);
            }
            if (IsModal)
            {
                ShowModalLayer();
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Action action = () =>
            {
                if (FocusedElement != null)
                    FocusedElement.Focus();
            };

            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, action);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            #region    == 方向键控制子窗体的移动 ==
            //if (WindowState == WindowState.Open)
            //{
            //    switch (e.Key)
            //    {
            //        case Key.Left:
            //            this.Left -= _horizaontalOffset;
            //            e.Handled = true;
            //            break;

            //        case Key.Right:
            //            this.Left += _horizaontalOffset;
            //            e.Handled = true;
            //            break;

            //        case Key.Down:
            //            this.Top += _verticalOffset;
            //            e.Handled = true;
            //            break;

            //        case Key.Up:
            //            this.Top -= _verticalOffset;
            //            e.Handled = true;
            //            break;
            //    }
            //}
            #endregion 
        }

        #endregion //Base Class Overrides

        #region Event Handlers

        void ChildWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                Focus();
        }

        void HeaderLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Focus();
            if ((e as RoutedEventArgs).OriginalSource != _dragWidget)
            {
                return;
            }
            e.Handled = true;
            Focus();
            _dragWidget.CaptureMouse();
            _isMouseCaptured = true;
            _clickPoint = e.GetPosition(null); //save off the mouse position
            _oldPosition = new Point(Left, Top); //save off our original window position
        }

        private void HeaderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            _dragWidget.ReleaseMouseCapture();
            _isMouseCaptured = false;
        }

        private void HeaderMouseMove(MouseEventArgs e)
        {
            if (_isMouseCaptured && Visibility == Visibility.Visible)
            {
                Point currentPosition = e.GetPosition(null); //our current mouse position

                Left = _oldPosition.X + (currentPosition.X - _clickPoint.X);
                Top = _oldPosition.Y + (currentPosition.Y - _clickPoint.Y);

                //this helps keep our mouse position in sync with the drag widget position
                Point dragWidgetPosition = e.GetPosition(_dragWidget);
                if (dragWidgetPosition.X < 0 || dragWidgetPosition.X > _dragWidget.ActualWidth || dragWidgetPosition.Y < 0 || dragWidgetPosition.Y > _dragWidget.ActualHeight)
                {
                    return;
                }

                _oldPosition = new Point(Left, Top);
                _clickPoint = e.GetPosition(Window.GetWindow(this)); //store the point where we are relative to the window
            }
        }

        private void ParentContainer_LayoutUpdated(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            //we only want to set the start position if this is the first time the control has bee initialized
            if (!_startupPositionInitialized)
            {
                ExecuteOpen();
                _startupPositionInitialized = true;
            }
        }

        void ParentContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //resize our modal layer
            _modalLayer.Height = e.NewSize.Height;
            _modalLayer.Width = e.NewSize.Width;

            if (this.WindowStartupLocation == WindowStartupLocation.Center)
            {
                CenterChildWindow();
            }
            else
            {
                //reposition our window
                Left = GetRestrictedLeft();
                Top = GetRestrictedTop();
            }
        }

        #endregion //Event Handlers

        #region Methods

        #region Private

        private double GetRestrictedLeft()
        {
            if (Left < 0)
                return 0;

            if (_parentContainer != null)
            {
                if (Left + WindowRoot.ActualWidth > _parentContainer.ActualWidth && _parentContainer.ActualWidth != 0)
                {
                    double left = _parentContainer.ActualWidth - WindowRoot.ActualWidth;
                    return left < 0 ? 0 : left;
                }
            }

            return Left;
        }

        private double GetRestrictedTop()
        {
            if (Top < 0)
                return 0;

            if (_parentContainer != null)
            {
                if (Top + WindowRoot.ActualHeight > _parentContainer.ActualHeight && _parentContainer.ActualHeight != 0)
                {
                    double top = _parentContainer.ActualHeight - WindowRoot.ActualHeight;
                    return top < 0 ? 0 : top;
                }
            }

            return Top;
        }

        private void SetWindowState(WindowState state)
        {
            switch (state)
            {
                case WindowState.Closed:
                    {
                        ExecuteClose();
                        break;
                    }
                case WindowState.Open:
                    {
                        ExecuteOpen();
                        break;
                    }
            }
        }

        private void ExecuteClose()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnClosing(e);

            if (!e.Cancel)
            {
                if (!_dialogResult.HasValue)
                    _dialogResult = false;

                OnClosed(EventArgs.Empty);
                Panel panel = MigrationVisualTreeHelper.GetAppRootVisual().Content as Panel;
                if (panel != null)
                {
                    if (panel.Children.Contains(this))
                    {
                        panel.Children.Remove(this);
                    }
                }
            }
            else
            {
                CancelClose();
            }
        }

        private void CancelClose()
        {
            _dialogResult = null; //when the close is cancelled, DialogResult should be null

            _ignorePropertyChanged = true;
            WindowState = WindowState.Open; //now reset the window state to open because the close was cancelled
            _ignorePropertyChanged = false;
        }

        private void ExecuteOpen()
        {
            _dialogResult = null; //reset the dialogResult to null each time the window is opened

            Panel panel = MigrationVisualTreeHelper.GetAppRootVisual().Content as Panel;
            if (panel != null)
            {
                if (!panel.Children.Contains(this))
                {
                    Grid.SetColumnSpan(this, int.MaxValue);
                    Grid.SetRowSpan(this, int.MaxValue);
                    panel.Children.Add(this);
                }
            }

            if (WindowStartupLocation == WindowStartupLocation.Center)
                CenterChildWindow();

            BringToFront();
            this.Focus();
        }

        private void BringToFront()
        {
            int index = 0;

            if (_parentContainer != null)
                index = (int)_parentContainer.GetValue(Canvas.ZIndexProperty);

            SetValue(Canvas.ZIndexProperty, ++index);

            if (IsModal)
                Canvas.SetZIndex(_modalLayerPanel, index - 2);
        }

        private void CenterChildWindow()
        {
            if (_parentContainer != null)
            {
                Left = (_parentContainer.ActualWidth - WindowRoot.ActualWidth) / 2.0;
                Top = (_parentContainer.ActualHeight - WindowRoot.ActualHeight) / 2.0;
                _modalLayer.Height = _parentContainer.ActualHeight;
                _modalLayer.Width = _parentContainer.ActualWidth;
            }
        }

        private void ShowModalLayer()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (!_modalLayerPanel.Children.Contains(_modalLayer))
                    _modalLayerPanel.Children.Add(_modalLayer);

                _modalLayer.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void HideModalLayer()
        {
            _modalLayer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ProcessMove(double x, double y)
        {
            _moveTransform.X += x;
            _moveTransform.Y += y;

            InvalidateArrange();
        }

        #endregion //Private

        #region Public

        public void Show()
        {
            this.WindowState = WindowState.Open;
        }

        public virtual void Close()
        {
            this.WindowState = WindowState.Closed;
        }

        #endregion //Public

        #endregion //Methods

        #region Events

        /// <summary>
        /// Occurs when the ChildWindow is closed.
        /// </summary>
        public event EventHandler Closed;
        protected virtual void OnClosed(EventArgs e)
        {
            if (Closed != null)
                Closed(this, e);
        }

        /// <summary>
        /// Occurs when the ChildWindow is closing.
        /// </summary>
        public event EventHandler<CancelEventArgs> Closing;
        protected virtual void OnClosing(CancelEventArgs e)
        {
            if (Closing != null)
                Closing(this, e);
        }

        #endregion //Events
    }
}
