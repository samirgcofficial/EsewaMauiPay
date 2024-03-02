using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Com.F1soft.Esewasdk;

namespace DotnetMAUIEsewa;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private static int REQUEST_CODE_PAYMENT = 1;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
    }


    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (requestCode == REQUEST_CODE_PAYMENT)
        {
            if (resultCode == Result.Ok)
            {
                if (data == null) return;
                string message = data.GetStringExtra(ESewaPayment.ExtraResultMessage);
                Android.Util.Log.Info("TAG", "Proof of Payment: " + message);
                Toast.MakeText(this, "SUCCESSFUL PAYMENT", ToastLength.Short).Show();
            }
            else if (resultCode == Result.Canceled)
            {
                Toast.MakeText(this, "Canceled By User", ToastLength.Short).Show();
            }
            else if (resultCode == (Result)ESewaPayment.ResultExtrasInvalid)
            {
                if (data == null) return;
                string message = data.GetStringExtra(ESewaPayment.ExtraResultMessage);
                Android.Util.Log.Info("TAG", "Proof of Payment: " + message);
            }
        }
    }

}

