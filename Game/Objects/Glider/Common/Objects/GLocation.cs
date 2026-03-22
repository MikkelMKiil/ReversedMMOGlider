// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GLocation
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Globalization;

namespace Glider.Common.Objects
{
    public class GLocation
    {
        protected float _nextHeadingTolerance;
        protected float _x;
        protected float _y;
        protected float _z;
        protected bool _zLoaded;

        public GLocation(double X, double Y)
        {
            _x = (float)X;
            _y = (float)Y;
            _zLoaded = false;
        }

        public GLocation(float X, float Y)
        {
            _x = X;
            _y = Y;
            _zLoaded = false;
        }

        public GLocation(float X, float Y, float Z)
        {
            _x = X;
            _y = Y;
            _z = Z;
            _zLoaded = true;
        }

        public GLocation()
        {
            _x = 0.0f;
            _y = 0.0f;
            _z = 0.0f;
            _zLoaded = false;
            _nextHeadingTolerance = 0.0f;
        }

        public GLocation(string What)
        {
            LoadFields(What);
        }

        public float X => _x;

        public float Y => _y;

        public float Z => _z;

        public float NextHeadingTolerance => _nextHeadingTolerance;

        public float DistanceToSelf => GetDistanceTo(GPlayerSelf.Me.Location);

        public double Bearing => GContext.Main.Me.GetHeadingDelta(this);

        public bool HasZ => _zLoaded;

        private void LoadFields(string What)
        {
            var strArray = What.Split(' ');
            _x = float.Parse(strArray[0], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
            _y = float.Parse(strArray[1], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
            _z = 0.0f;
            if (strArray.Length != 3)
                return;
            try
            {
                _z = float.Parse(strArray[2], NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
                _zLoaded = true;
            }
            catch (Exception ex)
            {
            }
        }

        public string ToString3D()
        {
            if (!HasZ)
                return ToString();
            return Math.Round(X, 2).ToString(CultureInfo.InvariantCulture.NumberFormat) + " " +
                   Math.Round(Y, 2).ToString(CultureInfo.InvariantCulture.NumberFormat) + " " +
                   Math.Round(Z, 2).ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        public override string ToString()
        {
            return Math.Round(X, 2).ToString(CultureInfo.InvariantCulture.NumberFormat) + " " +
                   Math.Round(Y, 2).ToString(CultureInfo.InvariantCulture.NumberFormat);
        }

        public float GetDistanceTo(GLocation L)
        {
            return (float)Math.Sqrt(Math.Pow(X - (double)L.X, 2.0) + Math.Pow(Y - (double)L.Y, 2.0));
        }

        public float GetHeadingTo(GLocation Target)
        {
            var num1 = Math.Abs(X - Target.X);
            var num2 = Math.Abs(Y - Target.Y);
            if (num1 < 1.0 && num2 < 1.0)
                return 0.0f;
            var num3 = 0.0;
            if (Target.X >= (double)X && Target.Y >= (double)Y)
                num3 = Degrees(Math.Atan(num2 / (double)num1));
            if (Target.X >= (double)X && Target.Y <= (double)Y)
                num3 = Degrees(Math.Atan(num2 / (double)num1)) * -1.0 + 360.0;
            if (Target.X <= (double)X && Target.Y <= (double)Y)
                num3 = Degrees(Math.Atan(num2 / (double)num1)) + 180.0;
            if (Target.X <= (double)X && Target.Y >= (double)Y)
                num3 = Degrees(Math.Atan(num2 / (double)num1) * -1.0) + 180.0;
            return (float)(Math.PI / 180.0 * num3);
        }

        private static double Degrees(double radians)
        {
            return 180.0 / Math.PI * radians;
        }
    }
}