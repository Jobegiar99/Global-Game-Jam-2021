using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities.Audio;

namespace Utilities.UI
{
    public class PaginationUI : MonoBehaviour
    {
        public TextMeshProUGUI pageNumber;
        public Button previous;
        public Button next;
        public ClipInfo nextAudio;
        public ClipInfo previousAudio;
        public int MaxPages { get; set; }

        private int _currentPage = 0;

        public int CurrentPage
        {
            get { return _currentPage; }
            private set
            {
                previous.interactable = CurrentPage > 0;
                next.interactable = CurrentPage < MaxPages - 1;
                pageNumber.text = (value + 1).ToString();
                _currentPage = value;
            }
        }

        public System.Action<int> OnChangePage;

        private void Awake()
        {
            next.onClick.AddListener(Next);
            previous.onClick.AddListener(Previous);
        }

        public void Previous()
        {
            if (CurrentPage == 0) return;
            CurrentPage--;
            OnChangePage?.Invoke(CurrentPage);
            AudioMngr.Player(AudioMngr.Type.UI).Play(previousAudio);
        }

        public void Next()
        {
            if (CurrentPage >= MaxPages - 1) return;
            CurrentPage++;
            OnChangePage?.Invoke(CurrentPage);
            AudioMngr.Player(AudioMngr.Type.UI).Play(nextAudio);
        }

        public void ChangeCurrentPageWithoutNotify(int page)
        {
            CurrentPage = page;
        }
    }
}