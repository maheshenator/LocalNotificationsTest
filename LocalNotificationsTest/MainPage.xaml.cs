using Plugin.LocalNotification;

namespace LocalNotificationsTest
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            InitLocalNotification();
        }

        private void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
        {            
            //  Schedule again? Unfortunately schedule doesnt work if app is closed.
            //   InitLocalNotification();
        }

        private async void InitLocalNotification()
        {
            /*
             * For Android 13 and above.. This plugin doesnt schedule
             1.  Below 3 permissions are turned on in this app manifest 
                    POST_NOTIFICATIONS" , SCHEDULE_EXACT_ALARM , USE_EXACT_ALARM 
                
              2. Also inorder for Notifications to work, MANUALLY enable "Notification" permission from Settings. - Android 13 and above
                 LocalNotificationCenter.RequestNotificationPermissionAsync()  - NOT Avaialble

            */

            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            // Cancel and clear any Local notifications that exisits, and schedule a new notification
            LocalNotificationCenter.Current.ClearAll();
            LocalNotificationCenter.Current.CancelAll();
            LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;

            string strId = DateTime.Now.ToString("MHdms");
            int iLocalNotiId = Convert.ToInt32(strId);
            var request = new NotificationRequest
            {
                NotificationId = iLocalNotiId,
                Title = $"Test Notificaiton number {iLocalNotiId}",
                Subtitle = $"Notificaiton number {iLocalNotiId}",
                Description = $"Notificaiton number {iLocalNotiId}",
                BadgeNumber = 1,
                /* Notification repeats as per schedule only if App is open/active, No notifications after the application is closed. */
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(10),
                    NotifyRepeatInterval = TimeSpan.FromSeconds(10),// Test every 10 seconds.
                    RepeatType = NotificationRepeat.TimeInterval
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }
    }

}
