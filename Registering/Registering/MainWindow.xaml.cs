using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Registering
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(User user)
        {
            InitializeComponent();
            UsernameText.Text = user.Username;
            FavoriteQuoteText.Text = user.FavoriteQuote;
            ProfileImage.Fill.SetValue(ImageBrush.ImageSourceProperty, new ImageSourceConverter().ConvertFromString(user.ProfileImagePath));
            //ProfileImage.Source = new BitmapImage(new Uri(user.ProfileImagePath, UriKind.Relative));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}