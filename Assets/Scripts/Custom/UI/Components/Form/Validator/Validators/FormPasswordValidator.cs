using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Utilities.UI
{
    public class FormPasswordValidator : FormValidator
    {
        public override string MsgId => "passwordLengthValidator";
        public string Msg2Id => "passwordCharsValidator";        

        public override void Validate(ValidateEvent doneValidating)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            string pass = InputField.Field.text;
            if (pass.Length < 8) { doneValidating(false, MsgId); return; }
            if (pass.Length > 100) { doneValidating(false, MsgId); return; }
            if (pass.Contains(" ")) { doneValidating(false, Msg2Id); return; }
            if (!hasUpperChar.IsMatch(pass)) { doneValidating(false, Msg2Id); return; }
            if (!hasLowerChar.IsMatch(pass)) { doneValidating(false, Msg2Id); return; }
            if (!hasNumber.IsMatch(pass)) { doneValidating(false, Msg2Id); return; }
            doneValidating(true, "");
        }
    }
}