using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownControl2 : MonoBehaviour
{
    Dropdown dropDownItem;
    public void onselectitem()
    {
        dropDownItem = this.GetComponent<Dropdown>();
        int index = dropDownItem.value;//ѡ�е����������
                                       //        Debug.Log(dropDownItem.options[index].text);//���options��Ӧѡ���������
        switch (index)
        {
            case 0:
                GameObject.Find("CuttingPlane/Plane1").transform.SetParent(GameObject.Find("Bone").transform);//��Ȼbone�����ţ����������CuttingPlane�ľ���λ�ú;��Դ�С�����䣬�൱��Unity��ֱ���ϵ�bone�±�����λ�ù�ϵ�Զ�ת���ˡ�
                GameObject.Find("CuttingPlane/Plane1").transform.localPosition = new Vector3(50, 0, 0);
                GameObject.FindGameObjectWithTag("CutPlane1").GetComponent<MeshRenderer>().enabled = true;              
                GameObject.FindGameObjectWithTag("CutCenter1").GetComponent<MeshRenderer>().enabled = true;             
                break;
            case 1:
                GameObject.FindGameObjectWithTag("CutPlane2").transform.SetParent(GameObject.Find("Bone").transform);//��Ȼbone�����ţ����������CuttingPlane�ľ���λ�ú;��Դ�С�����䣬�൱��Unity��ֱ���ϵ�bone�±�����λ�ù�ϵ�Զ�ת���ˡ�
                GameObject.FindGameObjectWithTag("CutPlane2").transform.localPosition = new Vector3(-50, 0, 0);               
                GameObject.FindGameObjectWithTag("CutPlane2").GetComponent<MeshRenderer>().enabled = true;            
                GameObject.FindGameObjectWithTag("CutCenter2").GetComponent<MeshRenderer>().enabled = true;
                break;
            

            default:
                break;
        }
    }

 
}
