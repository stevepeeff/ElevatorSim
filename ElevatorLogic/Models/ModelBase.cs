using System.ComponentModel;

namespace ElevatorLogic.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        private string m_CageAtFloor;
        private string m_CageStatus;
        private string m_DoorStatus;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the cage at floor information.
        /// </summary>
        /// <value>
        /// The cage at floor information.
        /// </value>
        public string CageAtFloorInformation
        {
            get { return m_CageAtFloor; }
            set
            {
                if (value != m_CageAtFloor)
                {
                    m_CageAtFloor = value;
                    NotifyPropertyChanged(nameof(CageAtFloorInformation));
                }
            }
        }

        /// <summary>
        /// Gets or sets the cage status.
        /// </summary>
        /// <value>
        /// The cage status.
        /// </value>
        public string CageStatus
        {
            get { return m_CageStatus; }
            set
            {
                if (value != m_CageStatus)
                {
                    m_CageStatus = value;
                    NotifyPropertyChanged(nameof(CageStatus));
                }
            }
        }

        /// <summary>
        /// Gets or sets the door status.
        /// </summary>
        /// <value>
        /// The door status.
        /// </value>
        public string DoorStatus
        {
            get { return m_DoorStatus; }
            set
            {
                if (value != m_DoorStatus)
                {
                    m_DoorStatus = value;
                    NotifyPropertyChanged(nameof(DoorStatus));
                }
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}