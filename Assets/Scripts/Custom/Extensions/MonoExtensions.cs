using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public static class MonoExtensions
{
    public static string RemoveAccents(this string input)
    {
        string normalized = input.Normalize(NormalizationForm.FormKD);
        Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
                                                new EncoderReplacementFallback(""),
                                                new DecoderReplacementFallback(""));
        byte[] bytes = removal.GetBytes(normalized);
        return Encoding.ASCII.GetString(bytes);
    }

    public static bool SearchCompare(this string original, string compareTo)
    {
        if (string.IsNullOrWhiteSpace(compareTo)) return true;
        original = original.ToLowerInvariant().RemoveAccents();
        compareTo = compareTo.ToLowerInvariant().Trim().RemoveAccents();
        return original.StartsWith(compareTo) || original.Contains(compareTo);
    }

    public static void ExecuteNextFrame(this MonoBehaviour mono, UnityAction action)
    {
        mono.StartCoroutine(NextFrame(action));
    }

    public static void ExecuteNextFrame(this MonoBehaviour mono, UnityAction action, int frames)
    {
        mono.StartCoroutine(NextFrame(action, frames));
    }

    public static void ExecuteLater(this MonoBehaviour mono, UnityAction action, float s)
    {
        mono.StartCoroutine(Later(action, s));
    }

    public static IEnumerator NextFrame(UnityAction action, int frames = 1)
    {
        while (frames != 0)
        {
            frames--;
            yield return null;
        }
        action?.Invoke();
    }

    public static IEnumerator Later(UnityAction action, float s)
    {
        yield return new WaitForSeconds(s);
        action?.Invoke();
    }

    public static T GetAddComponent<T>(this MonoBehaviour mono) where T : Component
    {
        return Helper.GetAddComponent<T>(mono.gameObject);
    }

    public static T GetAddComponent<T>(this GameObject mono) where T : Component
    {
        return Helper.GetAddComponent<T>(mono);
    }
}