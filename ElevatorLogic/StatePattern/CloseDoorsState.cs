using log4net;

namespace ElevatorLogic.StatePattern
{
    internal class CloseDoorsState : StateBase
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CloseDoorsState(StateContext context) : base(context)
        {
            NextAction = new GoToFloorState(context);
        }

        public override void ExecuteStateLogic()
        {
            m_Logger.Debug($"Executing Closing Doors");

            Context.HAL.GroundFloorIO.FloorDoor.CloseDoor();
            Context.HAL.FirstFloorIO.FloorDoor.CloseDoor();
            Context.HAL.SecondFloorIO.FloorDoor.CloseDoor();

            Context.HAL.ElevatorCageIO.CloseDoor();
        }
    }
}