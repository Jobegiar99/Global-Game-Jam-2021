using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class FormValidatorsMngr : FormValidator
    {
        public List<FormValidator> validators = new List<FormValidator>();

        private Queue<FormValidator> CreateQueue()
        {
            var queue = new Queue<FormValidator>();
            validators.ForEach(el => { el._field = _field; queue.Enqueue(el); });
            return queue;
        }

        public bool validateOnChange = false;
        public bool onEndEditing = false;

        public override string MsgId => "";

        private void Awake()
        {
            if (onEndEditing) InputField.Field.onEndEdit?.AddListener((str) =>
            {
                Validate((valid, msg) => { });
            });

            if (validateOnChange) InputField.Field.onValueChanged?.AddListener((str) =>
            {
                Validate((valid, msg) => { });
            });
        }

        public override void Validate(ValidateEvent doneValidating)
        {
            ValidateEvent @event = (valid, msg) => InputField.SetState(valid, msg);
            @event += (valid, msg) => doneValidating?.Invoke(valid, msg);

            if (validators.Count == 0) @event(true, "");
            else Validate(@event, CreateQueue());
        }

        private void Validate(ValidateEvent doneValidating, Queue<FormValidator> queue)
        {
            queue.Dequeue().Validate((valid, msg) =>
            {
                if (valid)
                {
                    if (queue.Count == 0)
                    {
                        doneValidating?.Invoke(true, "");
                        return;
                    }
                    Validate(doneValidating, queue);
                }
                else
                {
                    doneValidating?.Invoke(false, msg);
                }
            });
        }
    }
}