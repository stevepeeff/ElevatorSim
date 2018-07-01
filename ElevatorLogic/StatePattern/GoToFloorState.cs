using ElevatorHal.Interfaces;
using ElevatorLogic.Interfaces;
using log4net;
using System.Threading;

namespace ElevatorLogic.StatePattern
{
    internal class GoToFloorState : StateBase
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool m_CageAtFloor = false;

        public GoToFloorState(StateContext context) : base(context)
        {
            NextAction = new OpenFloorDoorState(context);
        }

        /// <summary>
        /// Executes the state logic.
        /// </summary>
        /// <exception cref="ElevatorException"></exception>
        public override void ExecuteStateLogic()
        {
            m_Logger.Debug($"Executing Go to floor ");

            IElevatorCageIO cage = Context.HAL.ElevatorCageIO;

            cage.PropertyChanged += Cage_PropertyChanged;

            if (cage.AtFloor.HasValue == false)
            {
                m_Logger.Error("ELevator is not at a floor");
                throw new ElevatorException($"Elevator is not at a floor cannot execute");
            }

            CageMoveLogic(cage);

            while (m_CageAtFloor == false)
            {
                Thread.Sleep(1);
            }
            cage.PropertyChanged -= Cage_PropertyChanged;
        }

        private void Cage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IElevatorCageIO cageInterface = sender as IElevatorCageIO;

            m_Logger.Debug($"Elevator state {cageInterface.EngineStatus}");

            if (cageInterface.EngineStatus != EngineStatusEnum.Idle && cageInterface.AtFloor.HasValue)
            {
                CageMoveLogic(cageInterface);
            }
        }

        private void CageMoveLogic(IElevatorCageIO cage)
        {
            int currentFloor = cage.AtFloor.Value;

            if (Context.TargetFloor < currentFloor)
            {
                cage.MoveDown();
            }
            else if (Context.TargetFloor > currentFloor)
            {
                cage.MoveUp();
            }
            else
            {
                cage.Stop();
                m_Logger.Debug("Elevator already at floor.");
                m_CageAtFloor = true;
            }
        }
    }
}