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
    /// Логика взаимодействия для Window_AddVisit.xaml
    /// </summary>
    public partial class Window_AddVisit : Window
    {
        Visit visit;
        int idDoc;
        public Window_AddVisit(Visit vis, int idDoctor)
        {
            InitializeComponent();
            this.visit = vis;
            DataContext = visit;
            idDoc = idDoctor;
            List<Pacient> pacients = CoreModel.init().Pacients.ToList();
            cbPacient.ItemsSource = pacients;

            List<TypeVisit> typeVisits = CoreModel.init().TypeVisits.ToList();
            cbType.ItemsSource = typeVisits;

            Doctor doctors = CoreModel.init().Doctors.FirstOrDefault(p => Convert.ToString(p.IdDoctor).Contains(Convert.ToString(idDoc)));
            if (doctors != null)
            {
                tbDoctor.Text = doctors.DoctorSurname + " " + doctors.DoctorName + " " + doctors.DoctorPatronymic;
            }
        }

        private void Button_SaveClick(object sender, RoutedEventArgs e)
        {
            if (cbPacient.Text != "" && tbDateVisit.Text != "" && tbTimeVisit.Text != "" && cbType.Text != "")
            {
                Pacient pacients = CoreModel.init().Pacients.FirstOrDefault(p => p.PacientPolis.Contains(cbPacient.Text));
                visit.PacientIdPacient = pacients.IdPacient;
                visit.DoctorIdDoctor = idDoc;
                string thisDay = DateTime.Now.ToShortDateString();
                DateOnly dateRec = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                visit.VisitDateRecording = dateRec;
                DateOnly dateVisit = DateOnly.Parse(tbDateVisit.Text); ;
                visit.VisitDate = dateVisit;

                CoreModel.init().Visits.Add(visit);
                CoreModel.init().SaveChanges();

                MessageBox.Show("Запись добавлена");

                WindowEmployee windowEmployee = new WindowEmployee();
                windowEmployee.Show();
                Hide();
            }
            else
                MessageBox.Show("Заполните все поля!");
        }

        private void cbPacient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pacient pacients = CoreModel.init().Pacients.FirstOrDefault(p => p.PacientPolis.Contains(cbPacient.Text));
            if (pacients != null)
            {
                tblockPacient.Text = pacients.PacientSurname + " " + pacients.PacientName + " " + pacients.PacientPatronymic;
            }
            
        }
    }
}
