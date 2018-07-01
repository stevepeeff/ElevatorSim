using ElevatorLogic.Models;

namespace ElevatorLogic
{
    /// <summary>
    /// Complete logic interface for the Elevator system.
    /// </summary>
    public interface IElevatorLogic
    {
        /// <summary>
        /// Gets the elevator model.
        /// </summary>
        /// <value>
        /// The elevator model.
        /// </value>
        ElevatorSystem ElevatorModel { get; }

        /// <summary>
        /// Initialezes the and start.
        /// </summary>
        void InitialezeAndStart();

        /// <summary>
        /// Requests the elevator.
        /// Can be pressed on floor
        /// </summary>
        /// <param name="floorNumber">The floor number.</param>
        void RequestElevator(int floorNumber);

        /// <summary>
        /// Goes to floor.
        /// A button within the elevator which is
        /// </summary>
        /// <param name="floorNumber">The floor number.</param>
        void GoToFloor(int floorNumber);
    }
}