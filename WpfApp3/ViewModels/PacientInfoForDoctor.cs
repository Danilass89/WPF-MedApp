using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.ViewModels;
using WpfApp3.Views;

namespace WpfApp3.ViewModels
{
    internal class PacientInfoForDoctor : BaseViewModel
    {

        private int _patientId;

        public PacientInfoForDoctor(int patientId)
        {
        }


        public int PatientID
        {
            get { return _patientId; }
            set { _patientId = value; }
        }
    }
}

