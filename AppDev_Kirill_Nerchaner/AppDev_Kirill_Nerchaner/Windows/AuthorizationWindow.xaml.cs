using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppDev_Kirill_Nerchaner.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            TBLogin.Text = AppDev_Kirill_Nerchaner.Properties.Settings.Default.Login;
            PBPassword.Password = AppDev_Kirill_Nerchaner.Properties.Settings.Default.Password;
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PBPassword.Password) || string.IsNullOrEmpty(TBLogin.Text))
            {
                MessageBox.Show("Please fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var auth = MainWindow.db.Employee.Where(z => z.Password == PBPassword.Password && z.Username == TBLogin.Text).FirstOrDefault();
                if (auth != null)
                {
                    MainWindow.emp = auth; //если пользователь авторизировался то передает его как авторизированного пользователя
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    App.Current.MainWindow.Close();
                    if(CBRemember.IsChecked == true)
                    {
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Login = TBLogin.Text;
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Password = PBPassword.Password;
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Save();
                    }
                    else
                    {
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Login = null;
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Password = null;
                        AppDev_Kirill_Nerchaner.Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    MessageBox.Show("Authorization error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
