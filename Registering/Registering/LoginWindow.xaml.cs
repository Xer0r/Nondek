using System.IO;
using System.Windows;

namespace Registering
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private List<User> users;

        public LoginWindow()
        {
            InitializeComponent();
            users = CsvHelper.ReadUsers();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = LoginUsername.Text;
            var password = LoginPassword.Password;

            if (username == null || password == null)
            {
                RegisterError.Text = "Complete all the requirements, please";
                return;
            }

            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                LoginError.Text = "User does not exist.";
            }
            else if (user.Password != password)
            {
                LoginError.Text = "Incorrect password.";
            }
            else
            {
                var mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = RegisterUsername.Text;
            string password = RegisterPassword.Password;
            string favoriteQuote = FavouriteQuote.Text;

            if (username == "" || password == "" || favoriteQuote == "" || selectedImagePath == null)
            {
                RegisterError.Text = "Complete all the requirements, please";
                return;
            }
            if (users.Any(u => u.Username == username))
            {
                RegisterError.Text = "Username already exists.";
                return;
            }

            //string projectPath = Directory.GetCurrentDirectory();
            string filename = String.Format("{0}_profile.png", username);
            string destinationPath = Path.Combine("Users", "Profile_pic", filename) /*Path.Combine(projectPath, "Users", "Profile_pic", filename)*/;

            File.Copy(selectedImagePath, destinationPath, true);

            var user = new User
            {
                Username = username,
                Password = password,
                ProfileImagePath = destinationPath,
                FavoriteQuote = favoriteQuote
            };
            users.Add(user);
            CsvHelper.WriteUsers(users);

            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }

        public string selectedImagePath;

        private void UploadProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
            }
        }
    }
}
