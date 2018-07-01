namespace ElevatorLogic.StatePattern
{
    internal interface IElevatorStateAction
    {
        /// <summary>
        /// Gets the next action.
        /// </summary>
        /// <value>
        /// The next action.
        /// </value>
        IElevatorStateAction NextAction { get; }

        /// <summary>
        /// Executes the state logic.
        /// </summary>
        void ExecuteStateLogic();
    }
}