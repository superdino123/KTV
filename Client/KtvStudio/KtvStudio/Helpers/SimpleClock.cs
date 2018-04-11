using System;
using System.Windows.Threading;

namespace KtvStudio.Helpers
{
    public class SimpleClock
    {
        #region DateTimeNow (With event when changed)

        public delegate void ClockChangedHandler(DateTime clock);

        private DateTime _mClock;

        public DateTime Clock
        {
            get { return _mClock; }
            private set
            {
                _mClock = value;
                OnClockChanged(value);
            }
        }

        public event ClockChangedHandler ClockChanged;

        protected void OnClockChanged(DateTime clock)
        {
            ClockChanged?.Invoke(clock);
        }

        #endregion DateTimeNow (With event when changed)

        public SimpleClock(int updateInSeconds)
        {
            Clock = DateTime.Now;
            var timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(updateInSeconds);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Clock = DateTime.Now;
        }
    }
}
