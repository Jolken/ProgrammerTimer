using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerTimer
{
    enum TimerState { Work, Pause, Rest }
    class MainTimer
    {

        private TimeSpan _time;
        public delegate void TimeChange(TimeSpan val);
        public TimeChange timeChange;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                timeChange?.Invoke(value);
            }
        }

        private TimeSpan _work;
        public delegate void WorkChange(TimeSpan val);
        public WorkChange workChange;
        public TimeSpan Work
        {
            get { return _work; }
            set
            {
                _work = value;
                workChange?.Invoke(value);
            }
        }

        private TimeSpan _rest;
        public delegate void RestChange(TimeSpan val);
        public RestChange restChange;
        public TimeSpan Rest
        {
            get { return _rest; }
            set
            {
                _rest = value;
                restChange?.Invoke(value);
            }
        }

        TimerState _state = TimerState.Pause;
        public delegate void StateChange(TimerState state);
        public StateChange stateChange;
        public TimerState State
        {
            get { return _state; }
            set
            {
                _state = value;
                stateChange?.Invoke(value);
            }
        }




        //hours,minutes,seconds
        public TimeSpan sleep = new TimeSpan(0,0,1);
        public MainTimer()
        {
            stateChange += (TimerState state) =>
            {
                switch (state)
                {
                    case TimerState.Work:
                        Time = Work;
                        break;
                    case TimerState.Rest:
                        Time = Rest;
                        break;
                }
            };
        }
        public async Task MainLoop()
        {
            while (true)
            {
                if (State != TimerState.Pause)
                {
                    Time -= sleep;
                    if (Time.TotalSeconds <= 0)
                    {
                        State = State == TimerState.Work ? TimerState.Rest : TimerState.Work;
                    }

                }


                await Task.Delay(sleep);
            }
        }
    }


}
