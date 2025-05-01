using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterPol.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPol
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Partner> Partners { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
        }



        private void LoadData()
        {
            using (var db = new MasterPolMaluginContext())
            {
                Partners = new ObservableCollection<Partner>(db.Partners.Include(p => p.PartnerProducts).ToList());

                ListViewPartners.ItemsSource = Partners;
            }
        }

        private void ListViewPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewPartners.SelectedItem is Partner selectedPartner)
            {
                var editWindow = new EditPartnerWindow(selectedPartner);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
                ListViewPartners.SelectedItem = null;
            }
        }

        private void AddPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddPartnerWindow();
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }
    }
}