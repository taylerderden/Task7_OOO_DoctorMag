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
    /// Логика взаимодействия для Window_Visit.xaml
    /// </summary>
    public partial class Window_Visit : Window
    {
        public Window_Visit()
        {
            InitializeComponent();

            IEnumerable<Visit> visits = CoreModel.init().Visits
                .Include(p => p.PacientIdPacientNavigation)
                .Include(p => p.DoctorIdDoctorNavigation)
                .Include(p => p.TypeVisitIdTypeVisitNavigation);
            ListViewVisit.ItemsSource = visits.ToList();

            Sort.Items.Add("По возрастанию");
            Sort.Items.Add("По убыванию");
            Sort.SelectedIndex = 1;

            List<Status> statuses = CoreModel.init().Statuses.ToList();
            FiltrStatus.ItemsSource = statuses;
            statuses.Insert(0, new Status { StatusName = "Все статусы" });
            FiltrStatus.SelectedIndex = 0;

            List<TypeVisit> typeVisits = CoreModel.init().TypeVisits.ToList();
            FiltrType.ItemsSource = typeVisits;
            typeVisits.Insert(0, new TypeVisit { TypeVisitName = "Все типы" });
            FiltrType.SelectedIndex = 0;
        }
        private void btnDoctors_Click(object sender, RoutedEventArgs e)
        {
            WindowEmployee windowEmployee = new WindowEmployee();
            windowEmployee.Show();
            this.Close();
            
        }
        private void update()
        {
            IEnumerable<Visit> visits = CoreModel.init().Visits
                .Include(p => p.PacientIdPacientNavigation)
                .Include(p => p.DoctorIdDoctorNavigation)
                .Include(p => p.TypeVisitIdTypeVisitNavigation)
                .Include(p => p.DoctorIdDoctorNavigation.StatusIdStatusNavigation)
                .Where(p => p.DoctorIdDoctorNavigation.DoctorSurname.Contains(Search.Text)
                || p.DoctorIdDoctorNavigation.DoctorName.Contains(Search.Text)
                || p.DoctorIdDoctorNavigation.DoctorPatronymic.Contains(Search.Text)
                || p.DoctorIdDoctorNavigation.DoctorSchedule.Contains(Search.Text)
                || p.PacientIdPacientNavigation.PacientName.Contains(Search.Text)
                || p.PacientIdPacientNavigation.PacientSurname.Contains(Search.Text)
                || p.PacientIdPacientNavigation.PacientPatronymic.Contains(Search.Text));

            switch (Sort.SelectedIndex)
            {
                case 0:
                    visits = visits.OrderBy(p => p.VisitDateRecording);
                    break;

                case 1:
                    visits = visits.OrderByDescending(p => p.VisitDateRecording);
                    break;
            }
            if (FiltrType.SelectedIndex > 0)
            {
                visits = visits.Where(p => p.DoctorIdDoctorNavigation.TypeVisitIdTypeVisit == (FiltrType.SelectedItem as TypeVisit).IdTypeVisit);
            }
            if (FiltrStatus.SelectedIndex > 0)
            {
                visits = visits.Where(p => p.DoctorIdDoctorNavigation.StatusIdStatus == (FiltrStatus.SelectedItem as Status).IdStatus);
            }

            ListViewVisit.ItemsSource = visits.ToList();
        }
        private void SearchChange(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void SortChange(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void FiltrStatusChange(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void FiltrTypeChange(object sender, SelectionChangedEventArgs e)
        {
            update();
        }
    }
}
