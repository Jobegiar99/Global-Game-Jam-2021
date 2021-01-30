using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class FormPasswordConfirmationValidator : FormValidator
    {
        public TMPro.TMP_InputField password;

        public override string MsgId => "passwordConfirmValidator";

        public override void Validate(ValidateEvent doneValidating)
        {
            if (password.text != InputField.Field.text)
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