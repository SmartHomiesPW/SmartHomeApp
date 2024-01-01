using Android.App;
using Android.Content;
using Android.Support.V4.App;
using AndroidX.AppCompat.Content.Res;
using SmartHome.Models;
using System.Runtime.CompilerServices;

public class SmartHomeAlarmNotification
{
    private static readonly int NOTIFICATION_ID = 1000;
    private static readonly string CHANNEL_ID = "smarthome_channel";

    public static void SendAlarmNotification(NotificationData notificationData, Android.Content.Context context)
    {
        Android.Content.Res.Resources resources = context.Resources;

        var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
            .SetContentTitle(notificationData.Title)
            .SetContentText(notificationData.Text)
            .SetSmallIcon(notificationData.Icon)
            .SetCategory(Notification.CategoryAlarm);

        // Build the notification:
        Notification notification = builder.Build();

        // Get the notification manager:
        NotificationManager notificationManager =
            context.GetSystemService(Context.NotificationService) as NotificationManager;

        // Publish the notification:
        notificationManager.Notify(NOTIFICATION_ID, notification);
    }

    public class NotificationData
    {
        public string Title = "SmartHomeApp Alarm System";
        public string Text;
        public int Icon;
    }
}