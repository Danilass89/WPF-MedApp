using System.Windows;
using System.Windows.Controls;

namespace WpfApp3.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public Frame MainFrame { get; set; }

        public MainView()
        {
            InitializeComponent();
            MainFrame = this.mainFrame;
        }
    }

}
