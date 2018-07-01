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
        private static InterfaceProvider m_Instance = new InterfaceProvider();

        public static InterfaceProvider Instance { get { return m_Instance; } }

        public IDoorIO IDoor
        {
            get { return new DoorStub(); }
        }
    }
}