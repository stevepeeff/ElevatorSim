using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal.Interfaces
{
    public enum EngineStatusEnum
    {
        MovingUp,
        MovingDown,
        Idle,
        Error
    };

    /// <summary>
    /// Whilst the cage is moving, and passes a floor it must be:
    /// stopped or
    /// be issued to continue move
    /// in time, if not an exception will occur and the Cage will fall in Error
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IElevatorCageIO : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the engine status.
        /// </summary>
        /// <value>
        /// The engine status.
        /// </value>
        EngineStatusEnum EngineStatus { get; }

        /// <summary>
        /// Initializes the case by closing the door and move to the bottom floor.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Moves the cage up
        /// </summary>
        /// <exception cref="EmergencyException">Elevator was not instructed in time</exception>
        void MoveUp();

        /// <summary>
        /// Moves the cage down
        /// </summary>
        /// <exception cref="EmergencyException">Elevator was not instructed in time</exception>
        void MoveDown();

        /// <summary>
        /// Stops the cage
        /// </summary>
        void Stop();

        /// <summary>
        /// Closes the door.
        /// </summary>
        void CloseDoor();

        /// <summary>
        /// Opens the door.
        /// </summary>
        void OpenDoor();

        /// <summary>
        /// Gets at floor at which the cage is present at this moment.
        /// </summary>
        /// <value>
        /// At floor.
        /// </value>
        int? AtFloor { get; }

        /// <summary>
        /// Gets the cage door.
        /// </summary>
        /// <value>
        /// The cage door.
        /// </value>
        IDoorIO CageDoor { get; }
    }
}