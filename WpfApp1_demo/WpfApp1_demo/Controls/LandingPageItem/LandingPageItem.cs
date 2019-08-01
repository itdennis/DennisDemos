namespace WpfApp1_demo.Controls
{
    #region ==using==
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    #endregion

    public class LandingPageItem : ContentControl
    {
        static LandingPageItem()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LandingPageItem), new FrameworkPropertyMetadata(typeof(LandingPageItem)));
        }

        void LandingPageItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                this.ImageSource = this.DisableImageSource;
            }
            else
            {
                this.ImageSource = this.NormalImageSource;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
           
            this.IsEnabledChanged -= LandingPageItem_IsEnabledChanged;
            this.IsEnabledChanged += LandingPageItem_IsEnabledChanged;
        }

        public string Title
        {
            get { return GetValue(TitleProperty) as string; }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LandingPageItem));

        public string Description
        {
            get { return GetValue(DescriptionProperty) as string; }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(LandingPageItem));

        public string ImageSource
        {
            get { return GetValue(ImageSourceProperty) as string; }
            set { SetValue(ImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(LandingPageItem));

        public string NormalImageSource
        {
            get { return GetValue(NormalImageSourceProperty) as string; }
            set { SetValue(NormalImageSourceProperty, value); }
        }
        public static readonly DependencyProperty NormalImageSourceProperty = DependencyProperty.Register("NormalImageSource", typeof(string), typeof(LandingPageItem), new PropertyMetadata((d, e) => 
        {
            if (d != null)
            {
                (d as LandingPageItem).ImageSource = e.NewValue as string;
            }
        }));

        public string DisableImageSource
        {
            get { return GetValue(DisableImageSourceProperty) as string; }
            set { SetValue(DisableImageSourceProperty, value); }
        }
        public static readonly DependencyProperty DisableImageSourceProperty = DependencyProperty.Register("DisableImageSource", typeof(string), typeof(LandingPageItem));

        public ICommand Command
        {
            get { return GetValue(CommandProperty) as ICommand; }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(LandingPageItem));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(LandingPageItem));
    }
}
