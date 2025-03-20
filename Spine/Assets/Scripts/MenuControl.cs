using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject Mainmenu,Screwmenu,Matchingmenu,Cutplanemenu;
    // Start is called before the first frame update
    bool CutplaneDisplayFlag = false;
    public void OnMatchingBtn()
    {
        Mainmenu.SetActive(false);
        Matchingmenu.SetActive(true);
    }
    public void OnScrewsBtn()
    {
        Mainmenu.SetActive(false);
        Screwmenu.SetActive(true);
        
    }
    public void OnCutplaneBtn()
    {
        Mainmenu.SetActive(false);
        Cutplanemenu.SetActive(true);
    }
    public void OnBtnOK()//screwsmenu上的
    {
        Screwmenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnFindAzureAnchor()//matchingmenu上的
    {
        Matchingmenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnBtnBack()//cutplane上的
    {
        Cutplanemenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnTransformk()//cutplane上的
    {
        GameObject.Find("CuttingPlane").transform.SetParent(GameObject.Find("Bone").transform);//虽然bone有缩放，但这个操作CuttingPlane的绝对位置和绝对大小并不变，相当于Unity里直接拖到bone下比例和位置关系自动转换了。
        GameObject.Find("CuttingPlane").transform.localPosition = new Vector3(0,0,0);
    }
    public void OnHideAndDisplay()//cutplane上的
    {
        CutplaneDisplayFlag = !CutplaneDisplayFlag;
        GameObject.FindGameObjectWithTag("CutPlane1").GetComponent<MeshRenderer>().enabled = CutplaneDisplayFlag;
        GameObject.FindGameObjectWithTag("CutPlane2").GetComponent<MeshRenderer>().enabled = CutplaneDisplayFlag;
        GameObject.FindGameObjectWithTag("CutCenter1").GetComponent<MeshRenderer>().enabled = CutplaneDisplayFlag;
        GameObject.FindGameObjectWithTag("CutCenter2").GetComponent<MeshRenderer>().enabled = CutplaneDisplayFlag;
        
        if(CutplaneDisplayFlag)
            GameObject.Find("CutplaneMenu/BtnHideDisplay/Text").GetComponent<Text>().text = "Hide";
        else
           GameObject.Find("CutplaneMenu/BtnHideDisplay/Text").GetComponent<Text>().text = "Display";
    }
}
