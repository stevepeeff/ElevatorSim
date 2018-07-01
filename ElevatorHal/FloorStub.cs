using ElevatorHal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal
{
    internal class FloorStub : HalBase, IFloorIO
    {
        private bool m_HasElevatorRequest = false;

        /// <summary>
        /// Gets the floor door.
        /// </summary>
        /// <value>
        /// The floor door.
        /// </value>
        public IDoorIO FloorDoor { get; } = new DoorStub();

        /// <summary>
        /// Gets or sets a value indicating whether this instance has elevator request.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has elevator request; otherwise, <c>false</c>.
        /// </value>
        public bool HasElevatorRequest
        {
            get { return m_HasElevatorRequest; }
            set
            {
                if (m_HasElevatorRequest != value)
                {
                    m_HasElevatorRequest = value;
                    NotifyPropertyChanged(nameof(HasElevatorRequest));
                }
            }
        }

        public override void Initialize()
        {
            (FloorDoor as DoorStub).Initialize();
        }
    }
}