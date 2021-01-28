using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public class ButtonSounds : MonoBehaviour, IPointerEnterHandler
    {
        public Button button;
        public AudioSource source;
        public AudioClip hover;
        public AudioClip click;

        private void Reset()
        {
            button = Utilities.Helper.GetAddComponent<Button>(gameObject);
            
            if (ButtonSoundsSingleton.ins != null)
            {
                source = ButtonSoundsSingleton.ins.source;
                hover = ButtonSoundsSingleton.ins.hover;
                click = ButtonSoundsSingleton.ins.click;
            }
            else
            {
                source = Helper.GetAddComponent<AudioSource>(gameObject);

            }
        }

        private void Awake()
        {
            button?.onClick.AddListener(() => { source.clip = click; source.Play(); });

            if (ButtonSoundsSingleton.ins != null)
            {
                source = ButtonSoundsSingleton.ins.source;
                hover = ButtonSoundsSingleton.ins.hover;
                click = ButtonSoundsSingleton.ins.click;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!button.interactable) return;

            source.clip = hover;
            source.Play();
        }
    }
}