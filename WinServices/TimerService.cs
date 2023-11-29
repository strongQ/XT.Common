using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Services;

namespace XT.Common.WinServices
{
    public abstract class TimerService : IDisposable, IWindowService
    {
        protected Timers Timers { get; private set; }
        private bool disposed = false;

        public TimerService()
        {
            Timers = new Timers();
        }

        public void StartBase()
        {

        }

        public void StopBase()
        {
            Timers.Stop();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                StopBase();
            }

            disposed = true;
        }

        public abstract Task Start();


        public virtual async Task Stop()
        {
            StopBase();
        }


        ~TimerService()
        {
            Dispose(false);
        }
    }
}
