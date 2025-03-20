using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwcontrol2 : MonoBehaviour
{
    public GameObject Screw1;
    public GameObject Screw2;
    public void getNewPosition()
    {
        //        GameObject.Find("Screw1").transform.position = GameObject.Find("Screw1").transform.forward + 0.03;
        //        GameObject.Find("Screw1").transform.localPosition.x += 0.03f;
        Screw1.transform.localPosition = new Vector3(-150.3f, 76.9f, 52.3f);//
        Screw1.transform.localEulerAngles = new Vector3(87.548f, -27.88f, -37.327f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);也可以
        Screw2.transform.localPosition = new Vector3(-149.8f, 46.3f, 53.3f); //
        Screw2.transform.localEulerAngles = new Vector3(-72.837f, 17.768f, -8.17f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);也可以
    }

}
