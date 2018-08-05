using ElevatorHal.Interfaces;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using log4net;

namespace ElevatorHal
{
    /// <summary>
    /// This class simulates the IO of the ElevatorCage
    /// After initialization the Cage will be at the Ground Floor (0)
    /// When send of up or down, it will pass a floor.
    /// It needs to be stopped or be called to carry on (Move Up/Down) within 500 ms
    /// If not it will fall into Error and sent a EmergencyException
    /// </summary>
    /// <seealso cref="ElevatorHal.HalBase" />
    /// <seealso cref="ElevatorHal.Interfaces.IElevatorCageIO" />
    public class ElevatorStub : HalBase, IElevatorCageIO
    {
        private const int EMERGENCY_DELAY = 500;
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int? m_AtFloor = null;
        private bool m_continue = false;
        private EngineStatusEnum m_EngineStatus = EngineStatusEnum.Idle;
        private int m_LastFloor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorStub"/> class.
        /// </summary>
        /// <param name="topFloorNumber">The top floor number.</param>
        public ElevatorStub(int topFloorNumber)
        {
            TopFloorNumber = topFloorNumber;
        }

        private enum DirectionEnum
        {
            UP = 1,
            DOWN = -1
        }

        /// <summary>
        /// Gets or sets at floor.
        /// </summary>
        /// <value>
        /// At floor.
        /// </value>
        public int? AtFloor
        {
            get { return m_AtFloor; }
            private set
            {
                if (m_AtFloor != value)
                {
                    m_AtFloor = value;

                    if (m_AtFloor.HasValue)
                    {
                        m_LastFloor = m_AtFloor.Value;
                    }

                    NotifyPropertyChanged(nameof(AtFloor));
                }
            }
        }

        /// <summary>
        /// Gets the bottom floor number.
        /// </summary>
        /// <value>
        /// The bottom floor number.
        /// </value>
        public int BottomFloorNumber { get; } = 0;

        /// <summary>
        /// Gets the cage door.
        /// </summary>
        /// <value>
        /// The cage door.
        /// </value>
        public IDoorIO CageDoor { get; } = new DoorStub();

        /// <summary>
        /// Gets or sets the engine status.
        /// </summary>
        /// <value>
        /// The engine status.
        /// </value>
        public EngineStatusEnum EngineStatus
        {
            get { return m_EngineStatus; }
            set
            {
                if (m_EngineStatus != value)
                {
                    m_EngineStatus = value;
                    NotifyPropertyChanged(nameof(EngineStatus));
                }
            }
        }

        /// <summary>
        /// Gets the top floor number.
        /// </summary>
        /// <value>
        /// The top floor number.
        /// </value>
        public int TopFloorNumber { get; }

        public void CloseDoor()
        {
            CageDoor.CloseDoor();
        }

        public override void Initialize()
        {
            CageDoor.Initialize();
            AtFloor = BottomFloorNumber;
        }

        public void MoveDown()
        {
            m_Logger.Info($"MoveDown Called current status {EngineStatus}");
            ShowThreadInfo();

            switch (EngineStatus)
            {
                case EngineStatusEnum.MovingDown:
                    {
                        ContinueMovingCage(DirectionEnum.DOWN);
                    }
                    break;

                case EngineStatusEnum.Idle:
                    {
                        if (AtFloor != BottomFloorNumber)
                        {
                            MoveCage(DirectionEnum.DOWN);
                        }
                    }
                    break;
            }
        }

        public void MoveUp()
        {
            m_Logger.Info($"MoveUp Called current status {EngineStatus}");
            ShowThreadInfo();

            switch (EngineStatus)
            {
                case EngineStatusEnum.MovingUp:
                    {
                        ContinueMovingCage(DirectionEnum.UP);
                    }
                    break;

                case EngineStatusEnum.Idle:
                    {
                        if (AtFloor != TopFloorNumber)
                        {
                            MoveCage(DirectionEnum.UP);
                        }
                    }
                    break;
            }
        }

        public void OpenDoor()
        {
            if (AtFloor == null)
            {
                throw new EmergencyException("Elevator was is not at a floor level");
            }
            else
            {
                CageDoor.OpenDoor();
            }
        }

        public void Stop()
        {
            if (AtFloor.HasValue)
            {
                m_continue = false;
                EngineStatus = EngineStatusEnum.Idle;
            }
            else
            {
                throw new EmergencyException("Elevator cannot be stopped, because it is not a floor");
            }
        }

        /// <summary>
        /// Handles the continue moving.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <exception cref="EmergencyException">Elevator was not instructed</exception>
        private void ContinueMovingCage(DirectionEnum direction)
        {
            if (m_continue == false)
            {
                m_Logger.Debug($"Enabled continue");
                m_continue = true;
                SimulateDelay(false);
                AtFloor = m_LastFloor + (int)direction;
            }
            else
            {
                SimulateDelay(true, EMERGENCY_DELAY);
                if (EngineStatus != EngineStatusEnum.Idle)
                {
                    EngineStatus = EngineStatusEnum.Error;
                    throw new EmergencyException("Elevator was not instructed");
                }
            }
        }

        private void MoveCage(DirectionEnum direction)
        {
            AtFloor = null;

            switch (direction)
            {
                case DirectionEnum.UP:
                    EngineStatus = EngineStatusEnum.MovingUp;
                    break;

                case DirectionEnum.DOWN:
                    EngineStatus = EngineStatusEnum.MovingDown;
                    break;
            }

            SimulateDelay(false);
            AtFloor = m_LastFloor + (int)direction;

            SimulateDelay(true, EMERGENCY_DELAY);
            if (m_continue == false && EngineStatus != EngineStatusEnum.Idle)
            {
                EngineStatus = EngineStatusEnum.Error;
                m_Logger.Error($"Error moving cage up");
                throw new EmergencyException("Elevator was not instructed");
            }
        }
    }
}