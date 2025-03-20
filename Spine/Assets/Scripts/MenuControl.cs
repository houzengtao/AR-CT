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
    public void OnBtnOK()//screwsmenu�ϵ�
    {
        Screwmenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnFindAzureAnchor()//matchingmenu�ϵ�
    {
        Matchingmenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnBtnBack()//cutplane�ϵ�
    {
        Cutplanemenu.SetActive(false);
        Mainmenu.SetActive(true);
    }
    public void OnTransformk()//cutplane�ϵ�
    {
        GameObject.Find("CuttingPlane").transform.SetParent(GameObject.Find("Bone").transform);//��Ȼbone�����ţ����������CuttingPlane�ľ���λ�ú;��Դ�С�����䣬�൱��Unity��ֱ���ϵ�bone�±�����λ�ù�ϵ�Զ�ת���ˡ�
        GameObject.Find("CuttingPlane").transform.localPosition = new Vector3(0,0,0);
    }
    public void OnHideAndDisplay()//cutplane�ϵ�
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
