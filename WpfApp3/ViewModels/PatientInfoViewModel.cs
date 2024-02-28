using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.Views;
using System.Diagnostics;


namespace WpfApp3.ViewModels
{
    public class PatientInfoViewModel : BaseViewModel
    {

        private int _patientID;



        public PatientInfoViewModel(int patientID)
        {
            _patientID = patientID;
            LoadPatientInfo(patientID);
            EnsureData();
            OpenAddDiseaseViewCommand = new RelayCommand(OpenAddDiseaseView);
        }

        private void LoadPatientInfo(FrameNavigationService navigationService)
        {

            List<DiseaseViewModel> diseases = dataManager.LoadPatientDiseases(_patientID);
            Diseases = new ObservableCollection<DiseaseViewModel>(diseases);
        }
        private readonly FrameNavigationService _navigationService;

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                _patientName = value;
                OnPropertyChanged(nameof(PatientName));
            }
        }

        private DataManager dataManager = new DataManager();
        public ICommand AddDiseaseCommand { get; set; }



        private ObservableCollection<DiseaseViewModel> _diseases;
        public ObservableCollection<DiseaseViewModel> Diseases
        {
            get { return _diseases; }
            set
            {
                _diseases = value;
                OnPropertyChanged(nameof(Diseases));
            }
        }

        public ICommand OpenAddDiseaseViewCommand { get; set; }

        private void OpenAddDiseaseView(object parameter)
        {

            var addDiseaseViewModel = new AddDiseaseViewModel(_patientID);
            var addDiseaseView = new AddDiseaseView { DataContext = addDiseaseViewModel };

            // Откройте новое окно AddDiseaseView
            addDiseaseView.ShowDialog();
        }

        private void LoadPatientInfo(int patientID)
        {
            List<DiseaseViewModel> diseases = dataManager.LoadPatientDiseases(patientID);
            Diseases = new ObservableCollection<DiseaseViewModel>(diseases);
        }

        public void EnsureData()
        {
            if (Diseases == null || Diseases.Count == 0)
            {
                Diseases = new ObservableCollection<DiseaseViewModel>
                {
                    new DiseaseViewModel
                    {
                        RecordID = GenerateRandomNumber(5, 15),
                        DiseaseID = GenerateRandomNumber(7, 15),
                        Date = GenerateRandomDate(new DateTime(2023, 4, 5), new DateTime(2023, 9, 14)),
                        Diagnosis = "Гипертония, первая стадия",
                        EndDate = DateTime.Now.AddDays(GenerateRandomNumber(-20, 15))
                    }
                };
            }
        }

        private int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        private DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
        {
            Random random = new Random();
            int range = (endDate - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }
    }
}