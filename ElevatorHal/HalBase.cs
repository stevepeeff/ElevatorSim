using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorHal
{
    public abstract class HalBase : INotifyPropertyChanged
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        protected static void ShowThreadInfo()
        {
            m_Logger.Debug($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        }

        protected void SimulateDelay(bool useTask, int? forcedDelay = null)
        {
            int delay = 0;

            if (forcedDelay != null)
            {
                delay = forcedDelay.Value;
            }
            else
            {
                delay = new Random().Next(200, 400);
            }

            if (useTask)
            {
                var t = Task.Run(() =>
                {
                    m_Logger.Debug($"SimulateDelay Task thread ID: {Thread.CurrentThread.ManagedThreadId}");
                });
                t.Wait(delay);
            }
            else
            {
                Thread.Sleep(delay);
            }
        }

        public abstract void Initialize();
    }
}