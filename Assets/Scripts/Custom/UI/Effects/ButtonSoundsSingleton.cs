using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class ButtonSoundsSingleton : MonoBehaviour
    {


        [HideInInspector] public static ButtonSoundsSingleton ins { get { if (!_ins) _ins = FindObjectOfType<ButtonSoundsSingleton>(); return _ins; } }
        static ButtonSoundsSingleton _ins = null;

        public AudioSource source;
        public AudioClip hover;
        public AudioClip click;

        private void Reset()
        {
            source = Helper.GetAddComponent<AudioSource>(gameObject);
        }
    }
}