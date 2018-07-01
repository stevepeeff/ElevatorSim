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
    /// Interaction logic for DoorAnimation.xaml
    /// </summary>
    public partial class DoorAnimation : UserControl
    {
        //public IDoorIO DoorInterface { get; set; } = new DoorStub();

        public DoorAnimation()
        {
            InitializeComponent();
        }

        public bool DoorOpening { get; private set; } = false;

        public bool DoorClosing { get; private set; } = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DoorOpening)
            {
                DoorOpening = false;
                DoorClosing = true;
            }
            else
            {
                DoorOpening = true;
                DoorClosing = false;
            }

            //if (DoorInterface.DoorStatus == DoorStatusEnum.Closed)
            //{
            //    DoorInterface.OpenDoor();
            //}
            //else if (DoorInterface.DoorStatus == DoorStatusEnum.Open)
            //{
            //    DoorInterface.CloseDoor();
            //}
        }
    }
}