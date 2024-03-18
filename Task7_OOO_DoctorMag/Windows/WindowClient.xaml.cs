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
    /// Логика взаимодействия для WindowClient.xaml
    /// </summary>
    public partial class WindowClient : Window
    {
        public WindowClient(int idPacient)
        {
            InitializeComponent();

            IEnumerable<Visit> visits = CoreModel.init().Visits
                .Include(p => p.TypeVisitIdTypeVisitNavigation)
                .Include(p => p.DoctorIdDoctorNavigation)
                .Where(p => Convert.ToString(p.PacientIdPacientNavigation.IdPacient).Contains(Convert.ToString(idPacient)));
            ListViewVisit.ItemsSource = visits.ToList();
        }
    }
}
