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
        int index = dropDownItem.value;//选中的项的索引号
                                       //        Debug.Log(dropDownItem.options[index].text);//输出options相应选择项的名字
        switch (index)
        {
            case 0:
                GameObject.Find("CuttingPlane/Plane1").transform.SetParent(GameObject.Find("Bone").transform);//虽然bone有缩放，但这个操作CuttingPlane的绝对位置和绝对大小并不变，相当于Unity里直接拖到bone下比例和位置关系自动转换了。
                GameObject.Find("CuttingPlane/Plane1").transform.localPosition = new Vector3(50, 0, 0);
                GameObject.FindGameObjectWithTag("CutPlane1").GetComponent<MeshRenderer>().enabled = true;              
                GameObject.FindGameObjectWithTag("CutCenter1").GetComponent<MeshRenderer>().enabled = true;             
                break;
            case 1:
                GameObject.FindGameObjectWithTag("CutPlane2").transform.SetParent(GameObject.Find("Bone").transform);//虽然bone有缩放，但这个操作CuttingPlane的绝对位置和绝对大小并不变，相当于Unity里直接拖到bone下比例和位置关系自动转换了。
                GameObject.FindGameObjectWithTag("CutPlane2").transform.localPosition = new Vector3(-50, 0, 0);               
                GameObject.FindGameObjectWithTag("CutPlane2").GetComponent<MeshRenderer>().enabled = true;            
                GameObject.FindGameObjectWithTag("CutCenter2").GetComponent<MeshRenderer>().enabled = true;
                break;
            

            default:
                break;
        }
    }

 
}
