using ElevatorHal;

namespace ElevatorLogic.StatePattern
{
    internal class StateContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateContext"/> class.
        /// </summary>
        /// <param name="hal">The hal.</param>
        public StateContext(HALProvider hal)
        {
            HAL = hal;
        }

        /// <summary>
        /// Gets the hal.
        /// </summary>
        /// <value>
        /// The hal.
        /// </value>
        public HALProvider HAL { get; private set; }

        /// <summary>
        /// Gets the target floor.
        /// </summary>
        /// <value>
        /// The target floor.
        /// </value>
        public int TargetFloor { get; private set; }

        /// <summary>
        /// Requests the specified target floor.
        /// </summary>
        /// <param name="targetFloor">The target floor.</param>
        public void Request(int targetFloor)
        {
            TargetFloor = targetFloor;

            IElevatorStateAction currentAction = new CloseDoorsState(this);

            while (currentAction != null)
            {
                currentAction.ExecuteStateLogic();
                currentAction = currentAction.NextAction;
            }
        }
    }
}