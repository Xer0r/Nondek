using System.Windows;
using System.Windows.Media.Imaging;

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
            ProfileImage.Source = new BitmapImage(new Uri(user.ProfileImagePath, UriKind.Relative));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}