using System.Windows;
using System.Windows.Input;
using WpfApp3.ViewModels;

namespace WpfApp3.Views
{
    /// <summary>
    /// Логика взаимодействия для AddPatientView.xaml
    /// </summary>
    public partial class AddPatientView : Window
    {
        public AddPatientView()
        {
            InitializeComponent();

            AddPatientViewModel viewModel = new AddPatientViewModel();
            DataContext = viewModel;
        }

        private RelayCommand addUserCommand;

        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    addUserCommand = new RelayCommand(AddUser);
                }

                return addUserCommand;
            }
        }

        private void AddUser(object commandParameter)
        {
        }

        private RelayCommand addPatientCommand;

        public ICommand AddPatientCommand
        {
            get
            {
                if (addPatientCommand == null)
                {
                    addPatientCommand = new RelayCommand(AddPatient);
                }

                return addPatientCommand;
            }
        }

        private void AddPatient(object commandParameter)
        {
        }
    }
}
