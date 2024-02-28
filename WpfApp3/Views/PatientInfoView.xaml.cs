using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Models;
using WpfApp3.ViewModels;


namespace WpfApp3.Views
{
    /// <summary>
    /// Логика взаимодействия для PatientInfoView.xaml
    /// </summary>
    /// 
    public partial class PatientInfoView : Page
    {
        public PatientInfoView(PatientInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }

}
