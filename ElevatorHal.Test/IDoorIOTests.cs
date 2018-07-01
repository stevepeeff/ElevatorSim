using ElevatorHal.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorHal.Test
{
    [TestFixture]
    public class IDoorIOTests
    {
        [Test, MaxTime(2000)]
        public void IDoorInitialize()
        {
            IDoorIO IDoor = InterfaceProvider.Instance.IDoor;

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Unknown);
            IDoor.Initialize();

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Closed);
        }

        [Test, MaxTime(5000)]
        public void IDoorClose()
        {
            IDoorIO IDoor = InterfaceProvider.Instance.IDoor;

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Unknown);
            IDoor.Initialize();
            IDoor.CloseDoor();

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Closed);
        }

        [Test, MaxTime(5000)]
        public void IDoorOpen()
        {
            IDoorIO IDoor = InterfaceProvider.Instance.IDoor;

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Unknown);
            IDoor.Initialize();
            IDoor.OpenDoor();

            Assert.That(IDoor.DoorStatus == DoorStatusEnum.Open);
        }
    }
}