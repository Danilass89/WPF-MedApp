using System;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.ViewModels;


namespace WpfApp3.Views
{
    /// <summary>
    /// Логика взаимодействия для PatientsListPage.xaml
    /// </summary> 
    public partial class PatientsListPage : Page
    {
        private DataManager dataManager = new DataManager();


        public PatientsListPage(Frame mainFrame)
        {
            InitializeComponent();


            PatientsListViewModel viewModel = new PatientsListViewModel();
            DataContext = viewModel;

            viewModel.LoadPatients();
        }

        private void PatientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var patient = (Patients)PatientsDataGrid.SelectedItem;

            if (patient != null)
            {
                PatientInfoViewModel patientViewModel = dataManager.LoadPatientInfo(patient.PatientID);
                PatientInfoForDoctor patientInfoView = new PatientInfoForDoctor(patientViewModel);
                patientInfoView.Show();
            }
        }

    }
}