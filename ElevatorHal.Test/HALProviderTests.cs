using ElevatorHal.Interfaces;
using log4net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorHal.Test
{
    [TestFixture]
    public class HALProviderTests
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [Test]
        public void ValidateInitializedState()
        {
            HALProvider halProvider = new HALProvider();

            halProvider.ElevatorCageIO.Initialize();
            halProvider.GroundFloorIO.FloorDoor.Initialize();
            halProvider.FirstFloorIO.FloorDoor.Initialize();
            halProvider.SecondFloorIO.FloorDoor.Initialize();

            Assert.That(halProvider.ElevatorCageIO.EngineStatus == Interfaces.EngineStatusEnum.Idle);
            Assert.That(halProvider.ElevatorCageIO.AtFloor == 0);
        }

        [Test]
        public void MoveUpHappyFlow()
        {
            HALProvider halProvider = new HALProvider();
            IElevatorCageIO elevator = halProvider.ElevatorCageIO;
            elevator.PropertyChanged += ElevatorCageIO_PropertyChangedActOnMovingUp;

            elevator.Initialize();

            elevator.MoveUp();
            Assert.That(elevator.AtFloor == 1);

            elevator.MoveUp();
            Assert.That(elevator.AtFloor == 2);
        }

        [Test]
        public void EmergencyException()
        {
            HALProvider halProvider = new HALProvider();
            IElevatorCageIO elevator = halProvider.ElevatorCageIO;
            elevator.Initialize();

            Assert.Throws<EmergencyException>(() => elevator.MoveUp());
        }

        [Test]
        public void MoveUpAndDownHappyFlow()
        {
            HALProvider halProvider = new HALProvider();
            IElevatorCageIO elevator = halProvider.ElevatorCageIO;
            elevator.Initialize();

            elevator.PropertyChanged += Elevator_PropertyChangedHandleTwoFloorUp;
            elevator.MoveUp();
            Assert.That(elevator.AtFloor == 2);
            elevator.PropertyChanged -= Elevator_PropertyChangedHandleTwoFloorUp;

            elevator.PropertyChanged += Elevator_PropertyChangedHandleTwoFloorDown;
            elevator.MoveDown();
            Assert.That(elevator.AtFloor == 0);
            elevator.PropertyChanged -= Elevator_PropertyChangedHandleTwoFloorDown;
        }

        private void Elevator_PropertyChangedHandleTwoFloorDown(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IElevatorCageIO cageInterface = sender as IElevatorCageIO;

            m_Logger.Debug($"Elevator state {cageInterface.EngineStatus}");
            if (cageInterface.AtFloor.HasValue)
            {
                switch (cageInterface.AtFloor.Value)
                {
                    case 0:
                        m_Logger.Debug($"Elevator Stop");
                        cageInterface.Stop();
                        break;

                    case 1:
                        m_Logger.Debug($"Elevator continue moving down");
                        cageInterface.MoveDown();
                        break;

                    case 2:
                        break;
                }
            }
        }

        private void Elevator_PropertyChangedHandleTwoFloorUp(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IElevatorCageIO cageInterface = sender as IElevatorCageIO;

            m_Logger.Debug($"Elevator state {cageInterface.EngineStatus}");
            if (cageInterface.AtFloor.HasValue)
            {
                switch (cageInterface.AtFloor.Value)
                {
                    case 0:
                        break;

                    case 1:
                        m_Logger.Debug($"Elevator continue moving");
                        cageInterface.MoveUp();
                        break;

                    case 2:
                        m_Logger.Debug($"Elevator Stop");
                        cageInterface.Stop();
                        break;
                }
            }
        }

        private void ElevatorCageIO_PropertyChangedActOnMovingUp(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IElevatorCageIO cageInterface = sender as IElevatorCageIO;

            m_Logger.Debug($"Elevator state {cageInterface.EngineStatus}");
            if (cageInterface.AtFloor.HasValue)
            {
                m_Logger.Debug($"Elevator reached a floor");
                cageInterface.Stop();
            }
            else
            {
            }
        }
    }
}