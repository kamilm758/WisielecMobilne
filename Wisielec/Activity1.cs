using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.Xna.Framework;
using System;

namespace Wisielec
{
    [Activity(Label = "Wisielec"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity, Android.Hardware.ISensorEventListener
    {
        private SensorManager sensorService; //sensor service do sensorów
        private double previousTime = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        private Vector3 previousAccelState = Vector3.Zero;
        public EventHandler onShake;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            sensorService = (SensorManager)GetSystemService(Context.SensorService);
            var acceleometerSensor = sensorService.GetDefaultSensor(SensorType.Accelerometer);
            sensorService.RegisterListener(this, acceleometerSensor, Android.Hardware.SensorDelay.Game);
            var g = new Game1(this);
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new System.NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
           // System.Diagnostics.Debug.WriteLine((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            var currentTime = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            e.Sensor = sensorService.GetDefaultSensor(SensorType.Accelerometer);
            if (Math.Abs(previousTime - currentTime) > 250)
            {
                var currentValues = new Vector3(e.Values[0], e.Values[1], e.Values[2]);
                Vector3 difference = currentValues - previousAccelState;
                float diffrenceSum = difference.X + difference.Y + difference.Z;
                if (Math.Abs(diffrenceSum) > 35)
                {
                    previousTime = currentTime;
                    previousAccelState = currentValues;
                    onShake?.Invoke(this, new EventArgs());
                    System.Diagnostics.Debug.WriteLine("shake!!");
                }
            }
            

        }
    }
}

