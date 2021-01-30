using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public abstract class FormValidator : MonoBehaviour
    {
        public abstract string MsgId { get; }
        public FormInputField InputField => (FormInputField)_field;
        public FormField _field;

        public delegate void ValidateEvent(bool isValid, string msg);

        public abstract void Validate(ValidateEvent doneValidating);
    }
}