using ElevatorLogic.Interfaces;
using log4net;

namespace ElevatorLogic.StatePattern
{
    internal class OpenFloorDoorState : StateBase
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OpenFloorDoorState(StateContext context) : base(context)
        {
        }

        public override void ExecuteStateLogic()
        {
            m_Logger.Debug($"Executing Open floor Door");

            if (Context.HAL.ElevatorCageIO.AtFloor.HasValue == false)
            {
                m_Logger.Error($"Cannot open doors, elevator has no floor");
                throw new ElevatorException($"Elevator is not at a floor");
            }

            if (Context.TargetFloor == 0)
            {
                Context.HAL.GroundFloorIO.FloorDoor.OpenDoor();
            }
            else if (Context.TargetFloor == 1)
            {
                Context.HAL.FirstFloorIO.FloorDoor.OpenDoor();
            }
            else if (Context.TargetFloor == 2)
            {
                Context.HAL.SecondFloorIO.FloorDoor.OpenDoor();
            }
            else
            {
                throw new ElevatorException($"Target floor {Context.TargetFloor} is not supported");
            }
            Context.HAL.ElevatorCageIO.OpenDoor();
        }
    }
}