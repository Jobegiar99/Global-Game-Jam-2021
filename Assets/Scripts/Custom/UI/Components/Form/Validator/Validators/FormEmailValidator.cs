using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;

namespace Utilities.UI
{
    public class FormEmailValidator : FormValidator
    {
        public override string MsgId => "emailFormatValidator";

        public override void Validate(ValidateEvent doneValidating)
        {
            try
            {
                MailAddress m = new MailAddress(InputField.Field.text);
                doneValidating(true, "");
            }
            catch
            {
                doneValidating(false, MsgId);
            }
        }
    }
}