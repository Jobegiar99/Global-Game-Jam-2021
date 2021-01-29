using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Utilities.UI.Menu
{
    [System.Serializable]
    public class MenuNode : Node
    {
        public string id;
        public string menuId;
        public bool EntryPoint = false;
    }
}