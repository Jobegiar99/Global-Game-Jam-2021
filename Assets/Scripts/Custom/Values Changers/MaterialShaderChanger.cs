using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialShaderChanger : MonoBehaviour
{
    public Material mat;
    public Material mat1;
    public Material mat2;
    public Shader s1;
    public Shader s2;
    public SpriteRenderer sprite;
    public void Reset()
    {
        mat = gameObject.GetComponent<SpriteRenderer>().material;
        s1 = mat.shader;
    }

    private void Awake()
    {
        mat = new Material(gameObject.GetComponent<SpriteRenderer>().material);
        sprite= gameObject.GetComponent<SpriteRenderer>();
        //mat.shader = s1;
        mat1 = new Material(mat1);
        mat2 = new Material(mat2);
        sprite.material = mat1;
    }
    public void SetShader(int id)
    {
        //mat.shader = id == 0 ? s1 : s2;
        sprite.material = id == 0 ? mat1 : mat2;
    }
}
