using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Debugging
{
    public class PrintFromEditor : MonoBehaviour
    {
        public string text = "Hello World";

        public void Print()
        {
            UnityEngine.Debug.Log(text);
        }
    }
}