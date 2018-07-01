using ElevatorHal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal
{
    public class DoorStub : HalBase, IDoorIO
    {
        private DoorStatusEnum m_DoorStatus = DoorStatusEnum.Unknown;

        /// <summary>
        /// Gets or sets the door status.
        /// </summary>
        /// <value>
        /// The door status.
        /// </value>
        public DoorStatusEnum DoorStatus
        {
            get { return m_DoorStatus; }
            set
            {
                if (m_DoorStatus != value)
                {
                    m_DoorStatus = value;
                    NotifyPropertyChanged(nameof(DoorStatus));
                }
            }
        }

        public void CloseDoor()
        {
            if (DoorStatus == DoorStatusEnum.Open)
            {
                DoorStatus = DoorStatusEnum.Closing;
                SimulateDelay(false);
                DoorStatus = DoorStatusEnum.Closed;
            }
        }

        public void OpenDoor()
        {
            if (DoorStatus == DoorStatusEnum.Closed)
            {
                DoorStatus = DoorStatusEnum.Opening;
                SimulateDelay(false);
                DoorStatus = DoorStatusEnum.Open;
            }
        }

        public override void Initialize()
        {
            DoorStatus = DoorStatusEnum.Closing;
            SimulateDelay(true, 200);
            DoorStatus = DoorStatusEnum.Closed;
        }
    }
}