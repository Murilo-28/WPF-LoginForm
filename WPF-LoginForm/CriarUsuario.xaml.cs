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
using WPF_LoginForm.Views;

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

        }

        private void BtnCriar_Click(object sender, RoutedEventArgs e)
        {
            LoginView tela = new LoginView();
            tela.Show();
        }
    }
}
