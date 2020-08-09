using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Stopwatch.Model;
namespace Stopwatch.ViewModel
{
    class StopwatchViewModel : INotifyPropertyChanged
    {
        private static readonly StopwatchModel _stopwatchModel = new StopwatchModel();

        private DispatcherTimer _timer = new DispatcherTimer();

        public bool Running { get { return _stopwatchModel.Running; } }
        public StopwatchViewModel()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += TimerTick;
            _timer.Start();
            Start();

            _stopwatchModel.LapTimeUpdated += LapTimeUpdatedEventHandler;
        }

        int _lastHours;
        int _lastMinutes;
        decimal _lastSeconds;
        bool _lastRunning;
        void TimerTick(object sender, object e)
        {
            if (_lastRunning != Running)
            {
                _lastRunning = Running;
                OnPropertyChanged("Running");
            }
            if (_lastHours != Hours)
            {
                _lastHours = Hours;
                OnPropertyChanged("Hours");
            }
            if (_lastMinutes != Minutes)
            {
                _lastMinutes = Minutes;
                OnPropertyChanged("Minutes");
            }
            if (_lastSeconds != Seconds)
            {
                _lastSeconds = Seconds;
                OnPropertyChanged("Seconds");
            }
        }

        public int Hours 
        { 
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.Elapsed;
                if (timeSpan.HasValue)
                {
                    int hours = timeSpan.Value.Hours;
                    return hours;
                }
                return 0;
            }
        }

        public int Minutes
        {
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.Elapsed;
                if (timeSpan.HasValue)
                {
                    int minutes = timeSpan.Value.Minutes;
                    return minutes;
                }
                return 0;
            }
        }

        public decimal Seconds
        {
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.Elapsed;
                if (timeSpan.HasValue)
                {
                    decimal seconds = (decimal)timeSpan.Value.Seconds
                        + (timeSpan.Value.Milliseconds * .001M);
                    return (decimal)seconds;
                }
                return 0.0M;
            }
        }

        public int LapHours
        {
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.LapTime;
                if (timeSpan.HasValue)
                {
                    int hours = timeSpan.Value.Hours;
                    return hours;
                }
                return 0;
            }
        }

        public int LapMinutes
        {
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.LapTime;
                if (timeSpan.HasValue)
                {
                    int minutes = timeSpan.Value.Minutes;
                    return minutes;
                }
                return 0;
            }
        }

        public decimal LapSeconds
        {
            get
            {
                TimeSpan? timeSpan = _stopwatchModel.LapTime;
                if (timeSpan.HasValue)
                {
                    decimal seconds = (decimal)timeSpan.Value.Seconds
                        + (timeSpan.Value.Milliseconds * .001M);
                    return (decimal)seconds;
                }
                return 0.0M;
            }
        }

        public void Start()
        {
            _stopwatchModel.Start();
        }

        public void Stop()
        {
            _stopwatchModel.Stop();
        }

        public void Reset()
        {
            bool running = Running;
            _stopwatchModel.Reset();
            if (running)
                _stopwatchModel.Start();
        }

        public void Lap()
        {
            _stopwatchModel.Lap();
        }

        int _lastLapHours;
        int _lastLapMinutes;
        decimal _lastLapSeconds;

        private void LapTimeUpdatedEventHandler(object sender, LapEventArgs e)
        {
            if (_lastLapHours != LapHours)
            {
                _lastLapHours = LapHours;
                OnPropertyChanged("LapHours");
            }
            if (_lastLapMinutes != LapMinutes)
            {
                _lastLapMinutes = LapMinutes;
                OnPropertyChanged("LapMinutes");
            }
            if (_lastLapSeconds != LapSeconds)
            {
                _lastLapSeconds = LapSeconds;
                OnPropertyChanged("LapSeconds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
