using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureUpdate : MonoBehaviour
{
    public Texture2D picture1;
    public Texture2D picture2;
    public Texture2D picture3;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i % 2 == 0)
        {
            GameObject.Find("Canvas/RawImageL").GetComponent<RawImage>().texture = picture1;
            GameObject.Find("Canvas/RawImageR").GetComponent<RawImage>().texture = picture1;
        }
        else
        {
            GameObject.Find("Canvas/RawImageL").GetComponent<RawImage>().texture = picture2;
            GameObject.Find("Canvas/RawImageR").GetComponent<RawImage>().texture = picture3;
        }

        if (i > 10) i = 0;
    }
}
