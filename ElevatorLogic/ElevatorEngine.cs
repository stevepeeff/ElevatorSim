using ElevatorHal;
using ElevatorHal.Interfaces;
using ElevatorLogic.Models;
using ElevatorLogic.StatePattern;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorLogic
{
    public class ElevatorEngine : ModelBase, IElevatorLogic
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int? m_CageAtFloor = null;

        private ObservableCollection<int> m_FloorRequestQueue = new ObservableCollection<int>();

        private ElevatorEngine()
        {
            m_Logger.Info("Engine created");

            m_FloorRequestQueue.CollectionChanged += FloorRequestQueue_CollectionChanged;

            HAL.ElevatorCageIO.PropertyChanged += ElevatorCageIO_PropertyChanged;
            HAL.ElevatorCageIO.CageDoor.PropertyChanged += CageDoor_PropertyChanged;
            HAL.GroundFloorIO.FloorDoor.PropertyChanged += GroundFloorDoor_PropertyChanged;
            HAL.FirstFloorIO.FloorDoor.PropertyChanged += FirstFloorDoor_PropertyChanged;
            HAL.SecondFloorIO.FloorDoor.PropertyChanged += SecondFloorDoorIO_PropertyChanged;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static IElevatorLogic Instance { get; } = new ElevatorEngine();

        /// <summary>
        /// Gets the cage at floor.
        /// </summary>
        /// <value>
        /// The cage at floor.
        /// </value>
        public int? CageAtFloor
        {
            get { return m_CageAtFloor; }

            private set
            {
                if (value != m_CageAtFloor)
                {
                    m_CageAtFloor = value;
                    SetElevatorFloorValues(CageAtFloor);
                    NotifyPropertyChanged(nameof(CageAtFloor));
                }
            }
        }

        /// <summary>
        /// Gets the elevator model.
        /// </summary>
        /// <value>
        /// The elevator model.
        /// </value>
        public ElevatorSystem ElevatorModel { get; } = new ElevatorSystem();

        /// <summary>
        /// Gets the elevator.
        /// </summary>
        /// <value>
        /// The elevator.
        /// </value>
        private ElevatorSystem Elevator { get; } = new ElevatorSystem();

        /// <summary>
        /// Gets the hal.
        /// </summary>
        /// <value>
        /// The hal.
        /// </value>
        private HALProvider HAL { get; } = new HALProvider();

        /// <summary>
        /// Goes to floor.
        /// A button within the elevator which is
        /// </summary>
        /// <param name="floorNumber">The floor number.</param>
        public void GoToFloor(int floorNumber)
        {
            AddToQueue(floorNumber);
        }

        public void InitialezeAndStart()
        {
            m_Logger.Debug("Initializing IO");

            HAL.ElevatorCageIO.Initialize();
            HAL.GroundFloorIO.FloorDoor.Initialize();
            HAL.FirstFloorIO.FloorDoor.Initialize();
            HAL.SecondFloorIO.FloorDoor.Initialize();

            MainEngineLogic();
        }

        public void MainEngineLogic()
        {
            Task readBytesTask = Task.Factory.StartNew((Action)(() =>
            {
                while (true)
                {
                    if (m_FloorRequestQueue.Count != 0)
                    {
                        StateContext context = new StateContext((HALProvider)this.HAL);
                        context.Request(m_FloorRequestQueue.ElementAt(0));
                        /*
                         * Close all doors
                         * Go to desired floor
                         * Open the door and wait 5 seconds
                        */
                        m_FloorRequestQueue.RemoveAt(0);
                    }
                }
            }), TaskCreationOptions.LongRunning);
        }

        public void RequestElevator(int floorNumber)
        {
            AddToQueue(floorNumber);
        }

        private void AddToQueue(int floorNumber)
        {
            if (!m_FloorRequestQueue.Contains(floorNumber))
            {
                m_FloorRequestQueue.Add(floorNumber);
            }
        }

        private void CageDoor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IDoorIO doorIo = sender as IDoorIO;

            ElevatorModel.Cage.DoorStatus = doorIo.DoorStatus.ToString();
        }

        private void ElevatorCageIO_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IElevatorCageIO cageInterface = sender as IElevatorCageIO;

            string engineStatus = cageInterface.EngineStatus.ToString();
            ElevatorModel.Cage.CageStatus = engineStatus;
            ElevatorModel.ElevatorFloorLevel_0.CageStatus = engineStatus;
            ElevatorModel.ElevatorFloorLevel_1.CageStatus = engineStatus;
            ElevatorModel.ElevatorFloorLevel_2.CageStatus = engineStatus;

            if (cageInterface.AtFloor != CageAtFloor)
            {
                CageAtFloor = cageInterface.AtFloor;
            }
        }

        private void FirstFloorDoor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IDoorIO doorIo = sender as IDoorIO;

            ElevatorModel.ElevatorFloorLevel_1.DoorStatus = doorIo.DoorStatus.ToString();
        }

        private void FloorRequestQueue_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            StringBuilder displayString = new StringBuilder("-");

            if (m_FloorRequestQueue.Count != 0)
            {
                displayString = new StringBuilder();

                foreach (int item in m_FloorRequestQueue)
                {
                    displayString.Append($" -{item}- ");
                }
            }

            ElevatorModel.ElevatorQueue = displayString.ToString();
        }

        private void GroundFloorDoor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IDoorIO doorIo = sender as IDoorIO;

            ElevatorModel.ElevatorFloorLevel_0.DoorStatus = doorIo.DoorStatus.ToString();
        }

        private void SecondFloorDoorIO_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IDoorIO doorIo = sender as IDoorIO;

            ElevatorModel.ElevatorFloorLevel_2.DoorStatus = doorIo.DoorStatus.ToString();
        }

        private void SetElevatorFloorValues(int? newValue)
        {
            string floorValue = "-";
            if (newValue.HasValue)
            {
                floorValue = newValue.Value.ToString();
            }

            ElevatorModel.Cage.CageAtFloorInformation = floorValue;
            ElevatorModel.ElevatorFloorLevel_0.CageAtFloorInformation = floorValue;
            ElevatorModel.ElevatorFloorLevel_1.CageAtFloorInformation = floorValue;
            ElevatorModel.ElevatorFloorLevel_2.CageAtFloorInformation = floorValue;
        }
    }
}