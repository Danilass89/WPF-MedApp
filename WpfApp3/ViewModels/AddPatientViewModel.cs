using System;
using System.Data.OleDb;
using System.Windows.Input;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public class AddPatientViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phoneNumber;
        private string _snils;
        private string _resultTextBlock;

        public ICommand AddUserCommand { get; set; }
        public ICommand AddPatientCommand { get; set; }




        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }


        private int _patientID1;

        public int PatientID1
        {
            get { return _patientID1; }
            set
            {
                _patientID1 = value;
                OnPropertyChanged(nameof(PatientID1));
            }
        }

        private int _userId1;

        public int UserId1
        {
            get { return _userId1; }
            set
            {
                _userId1 = value;
                OnPropertyChanged(nameof(UserId1));
            }
        }

        public string ResultTextBlock
        {
            get { return _resultTextBlock; }
            set
            {
                _resultTextBlock = value;
                OnPropertyChanged(nameof(ResultTextBlock));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string SNILS
        {
            get { return _snils; }
            set
            {
                _snils = value;
                OnPropertyChanged(nameof(SNILS));
            }
        }


        public AddPatientViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser);
            AddPatientCommand = new RelayCommand(AddPatient);
        }

        private void AddUser(object parameter)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
                {
                    connection.Open();

                    // Получаем максимальное значение UserId
                    string maxUserIdQuery = "SELECT MAX(UserID) AS MaxUserID FROM Users";
                    using (OleDbCommand maxUserIdCommand = new OleDbCommand(maxUserIdQuery, connection))
                    {
                        int maxUserId = (int)maxUserIdCommand.ExecuteScalar();
                        UserId = maxUserId + 1;
                    }

                    // Ваш код добавления пользователя
                    string query = $"INSERT INTO Users (UserID, Username, PasswordHash, IsDoctor) VALUES ({UserId}, '{Username}', '{Password}', False)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Обновляем ResultTextBlock при успешном добавлении пользователя
                    ResultTextBlock = "Пользователь успешно добавлен.";
                    OnPropertyChanged(nameof(ResultTextBlock));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding user: " + ex.Message);

                // Обновляем ResultTextBlock при ошибке добавления пользователя
                ResultTextBlock = "Ошибка при добавлении пользователя.";
                OnPropertyChanged(nameof(ResultTextBlock));
            }
        }

        private void AddPatient(object parameter)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
                {
                    connection.Open();

                    // Получаем максимальное значение PatientID
                    string maxPatientIdQuery = "SELECT MAX(PatientID) AS MaxPatientID FROM Patients";
                    using (OleDbCommand maxPatientIdCommand = new OleDbCommand(maxPatientIdQuery, connection))
                    {
                        int maxPatientId = (int)maxPatientIdCommand.ExecuteScalar();
                        PatientID1 = maxPatientId + 1;
                    }

                    // Ваш код добавления пациента
                    string query = $"INSERT INTO Patients (PatientID, UserID, PSurname, PName, PPatronymic, PhoneNumber, SNILS) VALUES " +
                                   $"({PatientID1},{UserId}, '{Surname}', '{Name}', '{Patronymic}', '{PhoneNumber}', '{SNILS}')";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Обновляем ResultTextBlock при успешном добавлении пациента
                    ResultTextBlock = "Новый пациент добавлен";
                    OnPropertyChanged(nameof(ResultTextBlock));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding patient: " + ex.Message);

                // Обновляем ResultTextBlock при ошибке добавления пациента
                ResultTextBlock = "Ошибка при добавлении пациента";
                OnPropertyChanged(nameof(ResultTextBlock));
            }
        }


    }
}
