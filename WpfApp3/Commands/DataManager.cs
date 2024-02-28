using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using WpfApp3.Models;
using WpfApp3.ViewModels;

namespace WpfApp3.Commands
{
    public class DataManager
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=D:\\С#\\WPF_Kyrsovoi\\WpfApp3\\WpfApp3\\HMSDataBase1.accdb; Persist Security Info=False;";

        public PatientInfoViewModel LoadPatientInfo(int patientID)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM MedicalHistory WHERE PatientID = {patientID}";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        ObservableCollection<DiseaseViewModel> diseases = new ObservableCollection<DiseaseViewModel>();

                        while (reader.Read())
                        {
                            DiseaseViewModel disease = new DiseaseViewModel
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                DiseaseID = Convert.ToInt32(reader["DiseaseID"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Diagnosis = reader["Diagnosis"].ToString(),
                                EndDate = reader["EndDate"] == DBNull.Value ? null : (DateTime?)reader["EndDate"]
                            };

                            diseases.Add(disease);
                        }

                        return new PatientInfoViewModel(patientID)
                        {
                            Diseases = diseases
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading patients: " + ex.Message);
                return null;
            }
        }


        public Patients LoadPatientData(string username)
        {
            Patients patient = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
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
                return null;
            }

            return patient;
        }


        public List<DiseaseViewModel> LoadPatientDiseases(int patientID)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM MedicalHistory WHERE PatientID = {patientID}";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        List<DiseaseViewModel> patientDiseases = new List<DiseaseViewModel>();

                        while (reader.Read())
                        {
                            DiseaseViewModel disease = new DiseaseViewModel
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                DiseaseID = Convert.ToInt32(reader["DiseaseID"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Diagnosis = reader["Diagnosis"].ToString(),
                                EndDate = reader["EndDate"] == DBNull.Value ? null : (DateTime?)reader["EndDate"]
                            };

                            patientDiseases.Add(disease);
                        }

                        return patientDiseases;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading patient diseases: " + ex.Message);
                return null;
            }
        }
    }

}
