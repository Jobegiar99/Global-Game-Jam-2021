using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

    public new SpriteRenderer renderer;

    public bool castShadow = true;
    public bool recieveShadow = true;
	// Use this for initialization
	void Reset () {
        renderer = GetComponent<SpriteRenderer>();
        SetShadows();
    }

    private void Awake()
    {
        SetShadows();
    }



    // Update is called once per frame
    void SetShadows () {
        renderer.shadowCastingMode = castShadow ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = recieveShadow;
    }
}
