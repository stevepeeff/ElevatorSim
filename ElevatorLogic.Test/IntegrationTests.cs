using ElevatorLogic.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorLogic.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void StatePatternTest()
        {
            ElevatorSystem system = new ElevatorSystem();

            //Assert.That(engine.CageAtFloor.Value == 0);

            //engine.GoToFloor(2);

            //Assume.That(engine.CageAtFloor.Value == 2);
        }
    }
}