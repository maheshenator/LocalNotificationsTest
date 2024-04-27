# LocalNotificationsTest

For Android 13 and above.. This plugin doesnt schedule :  https://github.com/thudugala/Plugin.LocalNotification/issues/491
1.  Below 3 permissions are turned on in this app manifest 
                    POST_NOTIFICATIONS" , SCHEDULE_EXACT_ALARM , USE_EXACT_ALARM 
                
2. Also inorder for Notifications to work, MANUALLY enable "Notification" permission from Settings. - Android 13 and above
                 LocalNotificationCenter.RequestNotificationPermissionAsync()  - NOT Avaialble

Android update for v13 and above, Apps should explicitly take permission. - https://source.android.com/docs/core/display/notification-perm
