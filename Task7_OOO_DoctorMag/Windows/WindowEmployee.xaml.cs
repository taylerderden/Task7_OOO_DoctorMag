using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для WindowEmployee.xaml
    /// </summary>
    public partial class WindowEmployee : Window
    {
        public WindowEmployee()
        {
            InitializeComponent();
            IEnumerable<Doctor> doctors = CoreModel.init().Doctors
                .Include(p => p.PositionIdPositionNavigation)
                .Include(p => p.TypeVisitIdTypeVisitNavigation)
                .Include(p => p.StatusIdStatusNavigation);
            ListViewDoctor.ItemsSource = doctors.ToList();

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
        private void update()
        {
            IEnumerable<Doctor> doctors = CoreModel.init().Doctors
                .Include(p => p.PositionIdPositionNavigation)
                .Include(p => p.TypeVisitIdTypeVisitNavigation)
                .Include(p => p.StatusIdStatusNavigation)
                .Where(p => p.DoctorSurname.Contains(Search.Text)
                || p.DoctorName.Contains(Search.Text)
                || p.DoctorPatronymic.Contains(Search.Text)
                || p.DoctorSchedule.Contains(Search.Text));

            switch (Sort.SelectedIndex)
            {
                case 0:
                    doctors = doctors.OrderBy(p => p.DoctorSurname);
                    break;

                case 1:
                    doctors = doctors.OrderByDescending(p => p.DoctorSurname);
                    break;
            }
            if (FiltrType.SelectedIndex > 0)
            {
                doctors = doctors.Where(p => p.TypeVisitIdTypeVisit == (FiltrType.SelectedItem as TypeVisit).IdTypeVisit);
            }
            if (FiltrStatus.SelectedIndex > 0)
            {
                doctors = doctors.Where(p => p.StatusIdStatus == (FiltrStatus.SelectedItem as Status).IdStatus);
            }

            ListViewDoctor.ItemsSource = doctors.ToList();
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

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = ListViewDoctor.SelectedItem as Doctor;
            if (doctor != null)
            {
                Window_AddVisit window_AddVisit = new Window_AddVisit(new Visit(),doctor.IdDoctor);
                window_AddVisit.Show();
                this.Close();
            }
            else
                MessageBox.Show("Выбери врача!");
        }

        private void btnVisit_Click(object sender, RoutedEventArgs e)
        {
            Window_Visit window_Visit = new Window_Visit();
            window_Visit.Show();
            this.Close();
        }
    }
}
