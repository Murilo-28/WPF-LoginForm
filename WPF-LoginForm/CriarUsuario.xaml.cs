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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;
using System.Runtime.InteropServices;
namespace WPF_LoginForm
{
    /// <summary>
    /// Interação lógica para CriarUsuario.xam
    /// </summary>
    public partial class CriarUsuario : Window
    {

        public CriarUsuario()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var novoUsuario = new UserModel
            {
                Username = txtUser.Text,
                Password = ConvertToUnsecureString(txtPassword.Password),
                Name = "Administrador",
                LastName = "Sistema",
                Email = "admin@sistema.com"
            };

            IUserRepository repository = new UserRepository();
            repository.Add(novoUsuario);

            MessageBox.Show("Usuário cadastrado com sucesso!");
        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            LoginView tela = new LoginView();
            tela.Show();
        }

        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
