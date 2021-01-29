using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public interface ISlotInputPC : ISlotInput
    {
        void BeginDrag();

        void EndDrag();

        void Drag();

        void Drop();

        void Click();
    }
}