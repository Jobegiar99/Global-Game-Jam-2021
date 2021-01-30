using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities._Input
{
    public interface IFocus
    {
        string FocusGroup { get; }

        void OnSelect();

        void OnDeselect();
    }
}