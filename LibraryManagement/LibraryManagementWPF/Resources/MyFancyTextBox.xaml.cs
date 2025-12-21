namespace LibraryManagementWPF
{
    /// <summary>
    /// Interaction logic for MyFancyTextBox.xaml
    /// </summary>
    public partial class MyFancyTextBox : UserControl
    {
        public MyFancyTextBox()
        {
            InitializeComponent();
        }


        public string InnerText
        {
            get { return (string)GetValue(InnerTextProperty); }
            set { SetValue(InnerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerTextProperty =
            DependencyProperty.Register("InnerText", typeof(string), typeof(MyFancyTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public string InformationTextBlock
        {
            get { return (string)GetValue(InformationTextBlockProperty); }
            set { SetValue(InformationTextBlockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InformationTextBlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InformationTextBlockProperty =
            DependencyProperty.Register("InformationTextBlock", typeof(string), typeof(MyFancyTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public double InformationTextOpacity
        {
            get { return (double)GetValue(InformationTextOpacityProperty); }
            set { SetValue(InformationTextOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InformationTextOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InformationTextOpacityProperty =
            DependencyProperty.Register("InformationTextOpacity", typeof(double), typeof(MyFancyTextBox), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public Brush Borderbrush
        {
            get { return (Brush)GetValue(BorderbrushProperty); }
            set { SetValue(BorderbrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Borderbrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderbrushProperty =
            DependencyProperty.Register("Borderbrush", typeof(Brush), typeof(MyFancyTextBox), new PropertyMetadata(Brushes.Transparent));








        private void InnerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard1 = (Storyboard)FindResource("TextBoxLostFocus");
            storyboard1.Begin();
            if (string.IsNullOrWhiteSpace(InnerTextBox.Text))
            {
                Storyboard storyboard = (Storyboard)FindResource("InformationTextBoxAnimationToVisible");
                storyboard.Begin();
            }
            else
            {
                Storyboard storyboard = (Storyboard)FindResource("InformationTextBoxAnimationToUnvisible");
                storyboard.Begin();
            }
        }

        private void InnerTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = (Storyboard)FindResource("InformationTextBoxAnimationToUnvisible");
            Storyboard storyboard1 = (Storyboard)FindResource("TextBoxGotFocus");
            storyboard1.Begin();
            storyboard.Begin();
            //InformationTextOpacity = 0;
        }
    }
}
