using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI;
using Utilities.UI.Animation;

public class TooltipManager : MonoBehaviour
{
    public UIAnimator anim;
    public RectTransform background;
    public TMPro.TextMeshProUGUI label;
    public UIScreenConnector connector;

    public static TooltipManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    public void Show(Vector2 position, string text, int direction = 1)
    {
        anim.Show();
        label.text = text;
        label.margin = direction == 1 ? new Vector4(0, 30, 0, 0) : new Vector4(0, 0, 0, 30);
        background.localScale = new Vector3(1, direction, 1);
        Vector2 offset = connector.offset2D;
        offset.y = Mathf.Abs(offset.y);
        offset.y *= direction == 1 ? -1f : 1f;
        connector.offset2D = offset;
        connector.Position = position;
    }

    public void Hide()
    {
        anim.Hide();
    }
}