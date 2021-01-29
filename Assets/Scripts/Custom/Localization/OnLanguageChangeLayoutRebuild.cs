using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities._Localization
{
    public class OnLanguageChangeLayoutRebuild : OnLanguageChange
    {
        public RectTransform layout;

        protected override void Awake()
        {
            onChange?.AddListener(() =>
            {
                Rebuild();
            });
            base.Awake();
        }

        public void Rebuild()
        {
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        }
    }
}