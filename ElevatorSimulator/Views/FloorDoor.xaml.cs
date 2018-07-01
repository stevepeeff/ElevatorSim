using ElevatorLogic;
using ElevatorLogic.Models;
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
    /// Interaction logic for FloorDoor.xaml
    /// </summary>
    public partial class FloorDoor : UserControl
    {
        public FloorDoor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ElevatorFloor floor = (ElevatorFloor)this.DataContext;
            ElevatorEngine.Instance.RequestElevator(floor.FloorNumber);
        }
    }
}