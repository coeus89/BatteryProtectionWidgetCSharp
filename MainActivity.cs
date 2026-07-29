using Android.App;
using Android.Content;
using Android.OS;
using BatteryProtectionWidget.Models;

namespace BatteryProtectionWidget;

[Activity(
    Label = "Battery Protection Widget",
    MainLauncher = true,
    Exported = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        Intent intent;

        try
        {
            var samsung = new ComponentName(
                SamsungBatteryProtection.PackageName,
                SamsungBatteryProtection.ActivityName);

            PackageManager?.GetActivityInfo(samsung, 0);

            intent = new Intent();
            intent.SetComponent(samsung);
        }
        catch
        {
            intent = new Intent(Intent.ActionPowerUsageSummary);
        }

        StartActivity(intent);
        Finish();
    }
}