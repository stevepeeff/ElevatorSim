using ElevatorHal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal
{
    public class InterfaceProvider
    {
        public static InterfaceProvider Instance { get; } = new InterfaceProvider();

        public IDoorIO IDoor
        {
            get { return new DoorStub(); }
        }
    }
}