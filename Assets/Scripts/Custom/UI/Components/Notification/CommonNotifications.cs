using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class CommonNotifications
    {
        private static UINotificationInfo Data(string title, float duration, bool hasCloseButton,
            Utilities.Audio.ClipInfo audio = null)
        {
            return new UINotificationInfo()
            {
                title = title,
                duration = duration,
                hasCloseButton = hasCloseButton
            };
        }

        private static UINotificationInfo Data(
            string title,
            string content,
            float duration,
            bool hasCloseButton,
            string actionText = null,
            Events.Event actionHandler = null,
            Utilities.Audio.ClipInfo audio = null
            )
        {
            return new UINotificationInfo()
            {
                title = title,
                duration = duration,
                hasCloseButton = hasCloseButton,
                actionButtonHandler = actionHandler,
                actionButtonText = actionText,
                content = content
            };
        }

        public static UINotificationInfo Error(string title, float duration, bool hasCloseButton,
            Utilities.Audio.ClipInfo audio = null)
        {
            if (audio == null) audio = UINotificationMngr.Instance.notificationAudioError;
            var notification = Data(title, duration, hasCloseButton, audio);

            notification.palette = UINotificationMngr.Instance.errorPalette;
            notification.icon = UINotificationMngr.Instance.errorIcon;
            UINotificationMngr.Instance.AddNotification(notification);
            return notification;
        }

        public static UINotificationInfo Error(
            string title,
            string content,
            float duration,
            bool hasCloseButton,
            string actionText = null,
            Events.Event actionHandler = null,
            Utilities.Audio.ClipInfo audio = null)
        {
            if (audio == null) audio = UINotificationMngr.Instance.notificationAudioError;
            var notification = Data(title, content, duration, hasCloseButton, actionText, actionHandler, audio);

            notification.palette = UINotificationMngr.Instance.errorPalette;
            notification.icon = UINotificationMngr.Instance.errorIcon;
            UINotificationMngr.Instance.AddNotification(notification);
            return notification;
        }

        public static UINotificationInfo Valid(string title, float duration, bool hasCloseButton, Utilities.Audio.ClipInfo audio = null)
        {
            if (audio == null) audio = UINotificationMngr.Instance.notificationAudioValid;
            var notification = Data(title, duration, hasCloseButton, audio);

            notification.palette = UINotificationMngr.Instance.validPalette;
            notification.icon = UINotificationMngr.Instance.validIcon;
            UINotificationMngr.Instance.AddNotification(notification);
            return notification;
        }

        public static UINotificationInfo Valid(
            string title,
            string content,
            float duration,
            bool hasCloseButton,
            string actionText = null,
            Events.Event actionHandler = null,
            Utilities.Audio.ClipInfo audio = null)
        {
            if (audio == null) audio = UINotificationMngr.Instance.notificationAudioValid;
            var notification = Data(title, content, duration, hasCloseButton, actionText, actionHandler, audio);

            notification.palette = UINotificationMngr.Instance.validPalette;
            notification.icon = UINotificationMngr.Instance.validIcon;
            UINotificationMngr.Instance.AddNotification(notification);
            return notification;
        }
    }
}