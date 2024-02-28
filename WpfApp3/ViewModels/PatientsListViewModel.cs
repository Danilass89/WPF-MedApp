using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.Views;

namespace WpfApp3.ViewModels
{
    internal class PatientsListViewModel : BaseViewModel
    {
        private ObservableCollection<Patients> _patients;

        public ObservableCollection<Patients> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                OnPropertyChanged("Patients");
            }
        }



        private DataManager dataManager = new DataManager();



        public ICommand OpenPatientInfoCommand { get; set; }

        private void OpenPatientInfo(object parameter)
        {
            var patient = parameter as Patients;

            if (patient != null)
            {
                PatientInfoViewModel patientViewModel = dataManager.LoadPatientInfo(patient.PatientID);
                PatientInfoForDoctor patientInfoView = new PatientInfoForDoctor(patientViewModel);
            }
        }

        private string _searchTextBox;

        public ICommand SearchCommand { get; set; }
        public ICommand AddNewPatientCommand { get; set; }

        private DataGrid _patientsDataGrid;

        public DataGrid PatientsDataGrid
        {
            get { return _patientsDataGrid; }
            set
            {
                _patientsDataGrid = value;
                OnPropertyChanged("PatientsDataGrid");
            }
        }

        public string SearchTextBox
        {
            get { return _searchTextBox; }
            set
            {
                _searchTextBox = value;
                OnPropertyChanged("SearchTextBox");
            }
        }

        private void Search(object parameter)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb; Persist Security Info=False;"))
                {
                    connection.Open();

                    string searchName = SearchTextBox;
                    string query = $"SELECT * FROM Patients WHERE PSurname LIKE '%{searchName}%'";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        ObservableCollection<Patients> searchResults = new ObservableCollection<Patients>();

                        while (reader.Read())
                        {
                            Patients patient = new Patients
                            {
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                PName = reader["PName"].ToString(),
                                PSurname = reader["PSurname"].ToString(),
                                PPatronymic = reader["PPatronymic"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                SNILS = reader["SNILS"].ToString()
                            };

                            searchResults.Add(patient);
                        }

                        // Обновляем коллекцию Patients, чтобы обновить DataGrid
                        Patients = searchResults;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error searching patients: " + ex.Message);
            }
        }


        public PatientsListViewModel()
        {
            SearchCommand = new RelayCommand(Search, CanSearch);
            AddNewPatientCommand = new RelayCommand(AddNewPatient);

            LoadPatients();
        }

        public void LoadPatients()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb; Persist Security Info=False;"))
                {
                    connection.Open();

                    string query = "SELECT * FROM Patients";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        ObservableCollection<Patients> patients = new ObservableCollection<Patients>();

                        while (reader.Read())
                        {
                            Patients patient = new Patients
                            {
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                PName = reader["PName"].ToString(),
                                PSurname = reader["PSurname"].ToString(),
                                PPatronymic = reader["PPatronymic"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                SNILS = reader["SNILS"].ToString()
                            };

                            patients.Add(patient);
                        }

                        Patients = patients;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading patients: " + ex.Message);
            }
        }

        private bool CanSearch(object parameter)
        {
            return true;
        }
        private void AddNewPatient(object parameter)
        {
            AddPatientView addPatientView = new AddPatientView();
            addPatientView.Show();


            LoadPatients();
        }


    }
}
