namespace ElevatorLogic.Models
{
    /// <summary>
    /// DataModel which holds all properties for the Elevator System
    /// </summary>
    /// <seealso cref="ElevatorLogic.Models.ViewModel.ViewModelBase" />
    public class ElevatorSystem : ModelBase
    {
        private string m_ElevatorQueue = "-";

        /// <summary>
        /// Gets or sets the elevator queue.
        /// </summary>
        /// <value>
        /// The elevator queue.
        /// </value>
        public string ElevatorQueue
        {
            get { return m_ElevatorQueue; }
            set
            {
                if (value != m_ElevatorQueue)
                {
                    m_ElevatorQueue = value;
                    NotifyPropertyChanged(nameof(ElevatorQueue));
                }
            }
        }

        /// <summary>
        /// Gets the cage.
        /// </summary>
        /// <value>
        /// The cage.
        /// </value>
        public ElevatorCage Cage { get; } = new ElevatorCage();

        /// <summary>
        /// Gets the elevator floor level 0.
        /// </summary>
        /// <value>
        /// The elevator floor level 0.
        /// </value>
        public ElevatorFloor ElevatorFloorLevel_0 { get; } = new ElevatorFloor(0);

        /// <summary>
        /// Gets the elevator floor level 1.
        /// </summary>
        /// <value>
        /// The elevator floor level 1.
        /// </value>
        public ElevatorFloor ElevatorFloorLevel_1 { get; } = new ElevatorFloor(1);

        /// <summary>
        /// Gets the elevator floor level 2.
        /// </summary>
        /// <value>
        /// The elevator floor level 2.
        /// </value>
        public ElevatorFloor ElevatorFloorLevel_2 { get; } = new ElevatorFloor(2);
    }
}