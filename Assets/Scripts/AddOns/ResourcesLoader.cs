using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour {

    public static Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>(name);
    }
}
