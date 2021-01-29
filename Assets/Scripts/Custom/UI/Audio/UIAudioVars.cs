using Utilities.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "GS_UIAudio", menuName = "Game/Audio/UIAudio")]
public class UIAudioVars : ScriptableObject
{
    public static UIAudioVars Instance { get; private set; } = null;

    public ClipInfo backSound;
    public ClipInfo buttonDefaultSelect;
    public ClipInfo buttonDefaultClick;
    public ClipInfo buttonDefaultError;

    public ClipInfo buttonTabClick;
    public ClipInfo notificationGoodClip;
    public ClipInfo notificationErrorClip;
    public ClipInfo dragClip;
    public ClipInfo dropGoodClip;
    public ClipInfo dropErrorClip;

    [Header("Dropdown")]
    public ClipInfo dropdownOpen;

    public ClipInfo dropdownChangeValue;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Instance = (UIAudioVars)Resources.LoadAll("Data/", typeof(UIAudioVars))[0];
    }
}