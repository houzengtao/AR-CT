using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingUpdate : MonoBehaviour
{
    public GameObject Plane1;
    public GameObject Plane2;
    public GameObject Center1;
    public GameObject Center2;
    public void CuttingPlanesHideandDisplay()
    {
        if (!Plane1.activeInHierarchy)
        {
            Plane1.SetActive(true);
        }
        else
        {
            Plane1.SetActive(false);
        }
        if (!Plane2.activeInHierarchy)
        {
            Plane2.SetActive(true);
        }
        else
        {
            Plane2.SetActive(false);
        }
        if (!Center1.activeInHierarchy)
        {
            Center1.SetActive(true);
        }
        else
        {
            Center1.SetActive(false);
        }
        if (!Center2.activeInHierarchy)
        {
            Center2.SetActive(true);
        }
        else
        {
            Center2.SetActive(false);
        }
    }
    public void getNewPosition()
    {

        //        GameObject.Find("Screw1").transform.position = GameObject.Find("Screw1").transform.forward + 0.03;
        //        GameObject.Find("Screw1").transform.localPosition.x += 0.03f;
        Plane1.transform.position = new Vector3(-0.1503f, 0.0769f, 0.0523f);//
        Plane1.transform.localEulerAngles = new Vector3(87.548f, -27.88f, -37.327f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);也可以
        Plane2.transform.position = new Vector3(-0.1498f, 0.0463f, 0.0533f); //
        Plane2.transform.localEulerAngles = new Vector3(-72.837f, 17.768f, -8.17f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);也可以
    }
}
