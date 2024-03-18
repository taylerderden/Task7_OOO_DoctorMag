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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task7_OOO_DoctorMag.DbModels;
using Task7_OOO_DoctorMag.Windows;

namespace Task7_OOO_DoctorMag
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void BtnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == null || tbPassword.Text == null)
            {
                MessageBox.Show("Заполните данные для входа!");
            }
            else
            {
                User user = CoreModel.init().Users.FirstOrDefault(p => p.UserLogin == tbLogin.Text && p.UserPassword == tbPassword.Text);

                if (user != null)
                {
                    WindowEmployee windowEmployee = new WindowEmployee();
                    windowEmployee.Show();
                    Hide();
                }
                else
                {

                    Pacient pacient = CoreModel.init().Pacients.FirstOrDefault(p => p.PacientSurname == tbLogin.Text && p.PacientPolis == tbPassword.Text);

                    if (pacient != null)
                    {
                        WindowClient windowClient = new WindowClient(pacient.IdPacient);
                        windowClient.Show();
                        Hide();
                    }
                }
            }
        }
        
        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            Window_ManagerClientGuest window_ManagerClientGuest = new Window_ManagerClientGuest();
            window_ManagerClientGuest.Show();
            Hide();
        }
    }
}
