using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal.Interfaces
{
    public enum DoorStatusEnum
    {
        Open,
        Opening,
        Closed,
        Closing,
        Unknown
    }

    public interface IDoorIO : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes the door
        /// </summary>
        void Initialize();

        /// <summary>
        /// Closes the door.
        /// </summary>
        void CloseDoor();

        /// <summary>
        /// Opens the door.
        /// </summary>
        void OpenDoor();

        /// <summary>
        /// Gets the door status.
        /// </summary>
        /// <value>
        /// The door status.
        /// </value>
        DoorStatusEnum DoorStatus { get; }
    }
}