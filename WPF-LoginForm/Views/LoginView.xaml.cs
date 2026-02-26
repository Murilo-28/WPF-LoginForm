using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        { 
            
        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            CriarUsuario tela = new CriarUsuario();
            tela.Show();
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                IUserRepository repository = new UserRepository();

                var credential = new NetworkCredential(
                    txtUser.Text,
                    ConvertToUnsecureString(txtPassword.Password)
                );

                bool isValidUser = repository.AuthenticateUser(credential);

                if (isValidUser)
                {
                    MessageBox.Show("Login realizado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(unmanagedString);
            }
            finally
            {
                if (unmanagedString != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(unmanagedString);
            }
        }
    }
}
