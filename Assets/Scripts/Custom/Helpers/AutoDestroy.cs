using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float t = 1;
    public bool smallFirst = true;

    
    private Vector3 startScale;
    private bool destroying = false;
    private bool alreadyDestroyed = false;
    private float timer = 0;

    private void Awake()
    {
        startScale = transform.localScale;
        this.ExecuteLater(() => { destroying = true; }, t);
    }



    public void Update()
    {
        if (!destroying || alreadyDestroyed) return;


        if (smallFirst)
        {
            timer += Time.deltaTime;

            transform.localScale =
                Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime/1f);

            if (timer >= 1) { Destroy(gameObject); alreadyDestroyed = true; }

            
        }
        else
        {
            alreadyDestroyed = true;
            Destroy(gameObject);
        }



    }


}
