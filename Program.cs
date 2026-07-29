using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using BatteryProtectionWidget.Models;

namespace BatteryProtectionWidget;

[BroadcastReceiver(Label = "Battery Protection Widget", Exported = true)]
[IntentFilter(new[] { AppWidgetManager.ActionAppwidgetUpdate })]
[MetaData("android.appwidget.provider", Resource = "@xml/battery_widget_info")]
public class BatteryProtectionWidgetProvider : AppWidgetProvider
{
    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
        foreach (var id in appWidgetIds)
        {
            Intent intent;
            try
            {
                var samsung = new ComponentName(
                    SamsungBatteryProtection.PackageName,
                    SamsungBatteryProtection.ActivityName
                    );
                context.PackageManager?.GetActivityInfo(samsung, 0);
                intent = new Intent();
                intent.SetComponent(samsung);
            }
            catch
            {
                intent = new Intent(Intent.ActionPowerUsageSummary);
            }
            var pending = PendingIntent.GetActivity(context,id,intent,PendingIntentFlags.Immutable|PendingIntentFlags.UpdateCurrent);
            var views = new RemoteViews(context.PackageName, Resource.Layout.widget_battery);
            views.SetOnClickPendingIntent(Resource.Id.widgetRoot,pending);
            appWidgetManager.UpdateAppWidget(id,views);
        }
    }
}