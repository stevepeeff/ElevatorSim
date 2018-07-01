namespace ElevatorLogic.Models
{
    /// <summary>
    /// DataModel which holds all properties for a ElevatorFloor
    /// It's polluted with UI components
    /// </summary>
    public class ElevatorFloor : ModelBase
    {
        public int FloorNumber { get; private set; }

        public bool RequestForElevator { get; set; } = false;

        public ElevatorFloor(int floorNumber)
        {
            FloorNumber = floorNumber;
        }
    }
}