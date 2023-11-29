
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XT.Common.WinServices
{
    /// <summary>
    /// Betfred timer
    /// </summary>
    public class Timer
    {
        private Thread thread;
        private AutoResetEvent stopRequest;
        private bool running = true;
        private bool paused = false;

        public Action OnTimer { get; set; }

        public Action<Exception> OnException { get; set; }

        public string Name { get; private set; }

        public int Interval { get; set; }

        public Timer(string name, int interval, Action onTimer, Action<Exception> onException = null)
        {
            OnTimer = onTimer == null ? () => { } : onTimer; ;
            Name = name;
            Interval = interval;
            OnException = onException == null ? (e) => { } : onException;

        }

        private void InternalWork(object arg)
        {
            while (running)
            {
                try
                {
                    if (!paused)
                    {
                        OnTimer();
                    }
                }
                catch (Exception exception)
                {
                    OnException(exception);
                }

                try
                {
                    if (stopRequest.WaitOne(Interval))
                    {
                        return;
                    }
                }
                catch (Exception exception)
                {
                    OnException(exception);
                }

            }
        }

        public void Start()
        {
            stopRequest = new AutoResetEvent(false);
            running = true;
            thread = new Thread(InternalWork);
            thread.Start();
        }

        public void Pause()
        {
            paused = true;
        }

        public void Resume()
        {
            paused = false;
        }

        public void Stop()
        {
            if (running)
            {
                running = false;
                stopRequest.Set();
                thread.Join();

                thread = null;
                stopRequest.Dispose();
                stopRequest = null;
            }
        }
    }
}
