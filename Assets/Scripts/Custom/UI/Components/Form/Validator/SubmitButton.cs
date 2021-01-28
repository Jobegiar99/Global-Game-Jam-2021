using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class SubmitButton : FormField
    {
        private bool _loading = false;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                loaderImg.enabled = value;
                selectable.interactable = !value;
                OnLoadingChange?.Invoke(value);
            }
        }

        public Button button;
        public Image loaderImg;
        public List<FormValidatorsMngr> validators = new List<FormValidatorsMngr>();

        public System.Action<bool> OnLoadingChange { get; set; }

        private Queue<FormValidator> CreateQueue()
        {
            var queue = new Queue<FormValidator>();
            validators.ForEach(el => { queue.Enqueue(el); });
            return queue;
        }

        private UnityAction<Events.Event> SubmitAction = null;
        public UnityEvent OnRequest = new UnityEvent();
        public UnityEvent OnResponse = new UnityEvent();

        private bool alreadyAwake = false;

        public void Init(UnityAction<Events.Event> SubmitAction)
        {
            this.SubmitAction = SubmitAction;
            if (!alreadyAwake) Awake();
        }

        public void Submit()
        {
            Loading = true;
            OnRequest?.Invoke();
            SubmitAction?.Invoke(OnResponse.Invoke);
        }

        private void Awake()
        {
            /*  SubmitAction = (@event) =>
              {
                  this.ExecuteLater(@event.Invoke, 2);
              };
              */
            Loading = false;

            OnResponse?.AddListener(() =>
            {
                Loading = false;
                if (EventSystem.current.currentSelectedGameObject == null ||
                    EventSystem.current.currentSelectedGameObject == selectable.gameObject)
                {
                    selectable.Select();
                }
            });

            button?.onClick?.AddListener(() =>
            {
                if (Loading) return;
                Loading = true;
                Validate(() =>
                {
                    bool canBeSubmited = Validate(CreateQueue());
                    if (canBeSubmited == false)
                    {
                        foreach (var item in validators)
                        {
                            if (item.InputField.state == FormInputField.State.Error)
                            {
                                item.InputField.Field.Select();
                                Loading = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        Submit();
                    }
                }, CreateQueue());
            });

            alreadyAwake = true;
        }

        private void Validate(Events.Event onDone, Queue<FormValidator> remaining)
        {
            if (remaining.Count == 0) { onDone?.Invoke(); return; }

            var current = remaining.Dequeue();
            if (current.InputField.state == FormInputField.State.Unused)
            {
                current.Validate((val, msg) =>
                {
                    Validate(onDone, remaining);
                });
            }
            else
            {
                Validate(onDone, remaining);
            }
        }

        public bool Validate(Queue<FormValidator> remaining)
        {
            if (remaining.Count == 0) return true;
            var current = remaining.Dequeue();
            if (current.InputField.state == FormInputField.State.Error) { return false; }
            return Validate(remaining);
        }
    }
}