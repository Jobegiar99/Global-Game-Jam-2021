using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class FormEmptyValidator : FormValidator
    {
        public override string MsgId => "emptyValidator";

        public override void Validate(ValidateEvent doneValidating)
        {
            _field.required = true;
            if (string.IsNullOrEmpty(InputField.Field.text))
            {
                doneValidating(false, MsgId);
            }
            else
            {
                doneValidating(true, "");
            }
        }
    }
}