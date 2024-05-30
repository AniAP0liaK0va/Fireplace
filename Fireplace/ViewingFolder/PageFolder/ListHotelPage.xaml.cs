using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ListHotelPage : Page
    {
        public ListHotelPage()
        {
            InitializeComponent();

            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            ListHotelListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable.ToList();
            TotalHotelTextBlock.Text = AppConnectClass.connectDataBase_ACC.HotelTable.ToList().Count().ToString();
        }

        private void SearchHotelTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void SearchHotelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListHotelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClassHotelComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
