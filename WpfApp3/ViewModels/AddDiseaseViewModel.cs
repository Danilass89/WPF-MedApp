using System;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Input;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public class AddDiseaseViewModel : BaseViewModel
    {
        private int _patientId;
        private string _recordId;
        private string _diseaseId;
        private string _diseaseName;
        private string _description;
        private string _resultTextBlock;

        public ICommand AddDiseaseCommand { get; set; }

        public string ResultTextBlock
        {
            get { return _resultTextBlock; }
            set
            {
                _resultTextBlock = value;
                OnPropertyChanged(nameof(ResultTextBlock));
            }
        }

        public string DiseaseName
        {
            get { return _diseaseName; }
            set
            {
                _diseaseName = value;
                OnPropertyChanged(nameof(DiseaseName));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string RecordID
        {
            get { return _recordId; }
            set
            {
                _recordId = value;
                OnPropertyChanged(nameof(RecordID));
            }
        }

        public string DiseaseID
        {
            get { return _diseaseId; }
            set
            {
                _diseaseId = value;
                OnPropertyChanged(nameof(DiseaseID));
            }
        }

        private string _dDate;

        public string DDate
        {
            get { return _dDate; }
            set
            {
                _dDate = value;
                OnPropertyChanged(nameof(DDate));
            }
        }

        public AddDiseaseViewModel(int patientId)
        {
            _patientId = patientId;
            AddDiseaseCommand = new RelayCommand(AddDisease);
        }



        private void AddDisease(object parameter)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
                {
                    connection.Open();

                    string maxRecordIdQuery = "SELECT MAX(RecordID) AS MaxRecordID FROM MedicalHistory";
                    using (OleDbCommand command = new OleDbCommand(maxRecordIdQuery, connection))
                    {
                        int maxRecordId = (int)command.ExecuteScalar();
                        RecordID = (maxRecordId + 1).ToString();
                    }

                    using (OleDbConnection connection1 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb"))
                    {
                        connection1.Open();

                        string query = "INSERT INTO MedicalHistory (RecordID, PatientID, DiseaseID, Diagnosis, [Date], EndDate) VALUES " +
                            "(@RecordID, @PatientID, @DiseaseID, @DiseaseName, @DDate, NULL)";

                        using (OleDbCommand command1 = new OleDbCommand(query, connection1))
                        {
                            command1.Parameters.AddWithValue("@RecordID", RecordID);
                            command1.Parameters.AddWithValue("@PatientID", _patientId);
                            command1.Parameters.AddWithValue("@DiseaseID", DiseaseID);
                            command1.Parameters.AddWithValue("@DiseaseName", DiseaseName);

                            command1.Parameters.AddWithValue("@DDate", DateTime.ParseExact(DDate, "dd.MM.yyyy", CultureInfo.InvariantCulture));

                            command1.ExecuteNonQuery();
                        }

                        ResultTextBlock = "Новая болезнь добавлена";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding disease: " + ex.Message);
                ResultTextBlock = "Ошибка при добавлении болезни";
            }
        }





    }
}
