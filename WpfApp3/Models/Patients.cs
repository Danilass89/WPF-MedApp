using System.ComponentModel;

namespace WpfApp3.Models
{
    public class Patients : INotifyPropertyChanged
    {

        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string PName { get; set; }
        public string PSurname { get; set; }
        public string PPatronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string SNILS { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
