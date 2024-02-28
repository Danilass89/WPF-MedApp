using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.Views;

namespace WpfApp3.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        private DataManager dataManager = new DataManager();

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

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
        private List<DiseaseViewModel> _patientDiseases;

        public List<DiseaseViewModel> PatientDiseases
        {
            get { return _patientDiseases; }
            set
            {
                _patientDiseases = value;
                OnPropertyChanged(nameof(PatientDiseases));
            }
        }

        public LoginViewModel()
        {

            LoginCommand = new RelayCommand(Login, CanLogin);

        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void Login(object parameter)
        {
            bool isAuthenticated = Authenticate(Username, Password);

            if (isAuthenticated)
            {
                OpenAppropriatePage(Username);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool Authenticate(string username, string password)
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
            {
                connection.Open();

                using (OleDbCommand command = new OleDbCommand($"SELECT COUNT(*) FROM Users WHERE Username = '{username}' AND PasswordHash = '{password}'", connection))
                {
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }



        public event Action RequestClose;



        private void OpenAppropriatePage(string username)
        {
            MainView mainView = new MainView();

            if (IsDoctor(username))
            {
                mainView.MainFrame.Navigate(new PatientsListPage(mainView.MainFrame));
            }
            else
            {
                Patients patient = dataManager.LoadPatientData(username);
                List<DiseaseViewModel> patientDiseases = dataManager.LoadPatientDiseases(patient.PatientID);

                var navigationService = new FrameNavigationService(mainView.MainFrame);
                var viewModel = new PatientInfoViewModel(patient.PatientID);
                viewModel.PatientName = $"{patient.PSurname} {patient.PName} {patient.PPatronymic}";
                viewModel.Diseases = new ObservableCollection<DiseaseViewModel>(patientDiseases);

                PatientInfoView patientInfoView = new PatientInfoView(viewModel);
                mainView.MainFrame.Navigate(patientInfoView);
            }

            mainView.Show();
            RequestClose?.Invoke();
        }






        public Patients LoadPatientData(string username)
        {
            Patients patient = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
                {
                    connection.Open();

                    string query = $"SELECT Patients.* FROM Patients INNER JOIN Users ON Patients.UserID = Users.UserID WHERE Users.Username = '{username}'";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = new Patients
                            {
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                PName = reader["PName"].ToString(),
                                PSurname = reader["PSurname"].ToString(),
                                PPatronymic = reader["PPatronymic"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                SNILS = reader["SNILS"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading patient data: " + ex.Message);
            }

            return patient;
        }
        






        private bool IsDoctor(string username)
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
            {
                connection.Open();

                using (OleDbCommand command = new OleDbCommand($"SELECT IsDoctor FROM Users WHERE Username = '{username}'", connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (bool)result;
                    }
                }
            }

            return false;
        }
    }
}
