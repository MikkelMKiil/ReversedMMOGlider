// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GSpellTimer
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GSpellTimer
    {
        private int _lastReset;

        public GSpellTimer(int MillisecondsDuration)
        {
            Duration = MillisecondsDuration;
            Reset();
        }

        public GSpellTimer(int MillisecondsDuration, bool StartReady)
        {
            Duration = MillisecondsDuration;
            if (StartReady)
                ForceReady();
            else
                Reset();
        }

        public bool IsReady => _lastReset == 0 || Environment.TickCount - _lastReset >= Duration;

        public bool IsReadySlow
        {
            get
            {
                Thread.Sleep(51);
                return IsReady;
            }
        }

        public int Duration { get; }

        public int TicksLeft => _lastReset + Duration - Environment.TickCount;

        public int TicksSinceLastReset => Environment.TickCount - _lastReset;

        public void Debug()
        {
        }

        public void Wait()
        {
            if (TicksLeft <= 0)
                return;
            Thread.Sleep(TicksLeft);
        }

        public bool WaitNoInterrupt()
        {
            var flag = false;
            while (!IsReady)
                try
                {
                    Wait();
                }
                catch (ThreadInterruptedException ex)
                {
                    flag = true;
                }

            return flag;
        }

        public void Reset()
        {
            _lastReset = Environment.TickCount;
        }

        public void ForceReady()
        {
            _lastReset = 0;
        }

        public void SetTicksLeft(int TicksLeft)
        {
            _lastReset = Environment.TickCount - TicksLeft;
        }
    }
}