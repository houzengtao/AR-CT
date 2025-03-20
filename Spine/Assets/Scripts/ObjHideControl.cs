using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHideControl : MonoBehaviour
{
    bool Hideflag = false;
    // Start is called before the first frame update
    public void HideAndShow()
    {
        GameObject.FindGameObjectWithTag("patientobj").transform.GetChild(0).GetComponent<MeshRenderer>().enabled = Hideflag;
        Hideflag = !Hideflag;
    }
   

}
