using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture[] texture;
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void LoadTexture(int no)
    {
        if (no < 0 || no >= texture.Length) return;
        Debug.Log("load texture no: " + no);
        renderer.material.mainTexture = texture[no];
    }
}
