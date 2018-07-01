using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal.Interfaces
{
    public interface IFloorIO : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the floor door.
        /// </summary>
        /// <value>
        /// The floor door.
        /// </value>
        IDoorIO FloorDoor { get; }
    }
}