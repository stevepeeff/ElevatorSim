using ElevatorHal.Interfaces;

namespace ElevatorHal
{
    /// <summary>
    /// If this provider was a Singleton, the INotifyPropertyChanged is not fired.
    /// </summary>
    public class HALProvider
    {
        /// <summary>
        /// Gets the elevator cage io.
        /// </summary>
        /// <value>
        /// The elevator cage io.
        /// </value>
        public IElevatorCageIO ElevatorCageIO { get; } = new ElevatorStub(2);

        /// <summary>
        /// Gets the first floor io.
        /// </summary>
        /// <value>
        /// The first floor io.
        /// </value>
        public IFloorIO FirstFloorIO { get; } = new FloorStub();

        /// <summary>
        /// Gets the ground floor io.
        /// </summary>
        /// <value>
        /// The ground floor io.
        /// </value>
        public IFloorIO GroundFloorIO { get; } = new FloorStub();

        /// <summary>
        /// Gets the second floor io.
        /// </summary>
        /// <value>
        /// The second floor io.
        /// </value>
        public IFloorIO SecondFloorIO { get; } = new FloorStub();
    }
}