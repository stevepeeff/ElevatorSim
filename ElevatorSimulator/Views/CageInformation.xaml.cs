using ElevatorLogic;
using ElevatorSimulator.ViewModel;
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

namespace ElevatorSimulator.Views
{
    /// <summary>
    /// Interaction logic for CageInformation.xaml
    /// </summary>
    public partial class CageInformation : UserControl
    {
        public CageInformation()
        {
            InitializeComponent();
            this.DataContext = ElevatorEngine.Instance.ElevatorModel.Cage;
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            ElevatorEngine.Instance.GoToFloor(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ElevatorEngine.Instance.GoToFloor(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ElevatorEngine.Instance.GoToFloor(2);
        }
    }
}