using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInteractable : MonoBehaviour
{
    public CanvasGroup cg;

    private void Reset()
    {
        cg = Utilities.Helper.GetAddComponent<CanvasGroup>(gameObject);
    }

    public void Set(bool val)
    {
        cg.interactable = cg.blocksRaycasts = val;
    }
}
