using System;
using System.ComponentModel;

namespace WpfApp3.ViewModels
{
    public class DiseaseViewModel : BaseViewModel
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
        public DateTime Date { get; set; }
        public string Diagnosis { get; set; }
        public DateTime? EndDate { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
