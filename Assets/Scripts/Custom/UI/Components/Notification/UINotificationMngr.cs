using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.UI
{
    public class UINotificationMngr : MonoBehaviour
    {
        public List<UINotification> notificationsPool = new List<UINotification>();
        private Stack<UINotification> availableNotifications = new Stack<UINotification>();
        public Queue<UINotificationInfo> notifications = new Queue<UINotificationInfo>();
        public UnityAction newNotification;
        public float coolDown = 0.3f;
        public Sprite errorIcon;
        public Sprite validIcon;
        public static UINotificationMngr Instance;

        [SerializeField] public ColorPalette defaultPalette;
        [SerializeField] public ColorPalette validPalette;
        [SerializeField] public ColorPalette errorPalette;

        [Header("Audios")] public Utilities.Audio.ClipInfo notificationAudio;
        public Utilities.Audio.ClipInfo notificationAudioValid;
        public Utilities.Audio.ClipInfo notificationAudioError;

        public void ClearAllNotifications()
        {
            notifications.Clear();
            notificationsPool.ForEach(el =>
            {
                el.Hide();
            });
        }

        private void Awake()
        {
            this.ExecuteNextFrame(Init, 2);

            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            notificationsPool.ForEach(el =>
            {
                el.gameObject.SetActive(false);
                availableNotifications.Push(el);
                el.anim.OnAnimationFinish += val =>
                  {
                      if (!val)
                      {
                          this.ExecuteLater(() =>
                          {
                              availableNotifications.Push(el);
                              if (notifications.Count > 0) Notify();
                          }, coolDown);
                      }
                  };
            });

            if (notifications.Count > 0) Notify();
        }

        public void AddNotification(UINotificationInfo info)
        {
            notifications.Enqueue(info);
            Notify();
        }

        public void AddNotification(string title, string content)
        {
            AddNotification(new UINotificationInfo() { title = title, content = content });
        }

        private void Notify()
        {
            if (availableNotifications.Count > 0)
            {
                var notification = availableNotifications.Pop();

                if (notification.transform.GetSiblingIndex() != transform.childCount - 1)
                    notification.transform.SetSiblingIndex(transform.childCount - 1);

                notification.SetValue(notifications.Dequeue());
            }
        }
    }
}