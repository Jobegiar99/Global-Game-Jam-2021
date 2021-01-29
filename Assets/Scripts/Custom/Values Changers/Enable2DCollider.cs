using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable2DCollider : MonoBehaviour
{
    public float t = 0.5f;


    private void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        StartCoroutine(EnableCollider(col, t));
    }

    public IEnumerator EnableCollider(Collider2D col, float t)
    {
        yield return new WaitForSeconds(t);
        col.isTrigger = false;
    }

    public void Restart()
    {
        Collider2D col = GetComponent<Collider2D>();
        StartCoroutine(EnableCollider(col, t));
    }


}
