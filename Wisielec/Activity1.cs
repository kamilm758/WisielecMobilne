using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;

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
        private float lightSensorValue=0;
        private SensorManager sensorService;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            sensorService = (SensorManager)GetSystemService(Context.SensorService);
            var lightSensor = sensorService.GetDefaultSensor(SensorType.Light);
            sensorService.RegisterListener(this, lightSensor, Android.Hardware.SensorDelay.Game);
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
            e.Sensor = sensorService.GetDefaultSensor(SensorType.Light);
            lightSensorValue = e.Values[0];
        }

        public float GetLightValue()
        {
            return lightSensorValue;
        }
    }
}

