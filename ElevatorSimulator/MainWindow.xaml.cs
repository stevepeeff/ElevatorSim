using ElevatorLogic;
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

namespace ElevatorSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ElevatorEngine.Instance.ElevatorModel;
            Floor0.DataContext = ElevatorEngine.Instance.ElevatorModel.ElevatorFloorLevel_0;
            Floor1.DataContext = ElevatorEngine.Instance.ElevatorModel.ElevatorFloorLevel_1;
            Floor2.DataContext = ElevatorEngine.Instance.ElevatorModel.ElevatorFloorLevel_2;

            this.Loaded += MainWindow_Loaded;
            //ElevatorEngine.Instance.InitialezeAndStart();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ElevatorEngine.Instance.InitialezeAndStart();
        }
    }
}