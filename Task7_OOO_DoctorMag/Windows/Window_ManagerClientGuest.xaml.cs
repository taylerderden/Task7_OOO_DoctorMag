using Microsoft.EntityFrameworkCore;
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
using Task7_OOO_DoctorMag.DbModels;

namespace Task7_OOO_DoctorMag.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window_ManagerClientGuest.xaml
    /// </summary>
    public partial class Window_ManagerClientGuest : Window
    {
        public Window_ManagerClientGuest()
        {
            InitializeComponent();
            IEnumerable<Doctor> doctors = CoreModel.init().Doctors
                .Include(p => p.PositionIdPositionNavigation)
                .Include(p => p.TypeVisitIdTypeVisitNavigation)
                .Include(p => p.StatusIdStatusNavigation);
            ListViewDoctor.ItemsSource = doctors.ToList();
        }
    }
}
