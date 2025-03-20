using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownControl : MonoBehaviour
{
    public GameObject tooltip, tooltip1, tooltip2, tooltip3, tooltip4, tooltip5;
    GameObject clonescrewH, clonescrewV;
    GameObject clonescrew1H, clonescrew1V;
    GameObject clonescrew2H, clonescrew2V;
    GameObject clonescrew3H, clonescrew3V;
    GameObject clonescrew4H, clonescrew4V;
    GameObject clonescrew5H, clonescrew5V;
    Dropdown dropDownItem;
    int curInterval = 0;
    int interval = 8;//要小于CTPlane的更新间隔。否则复制的cylinder方位不对，不是移动后在CT view上的方位，而只是初始状态。
    bool allflag=false,updateflag = false, updateflag1 = false, updateflag2 = false, updateflag3 = false, updateflag4 = false, updateflag5 = false;
    public void onselectitem()
    {
        Sprite tempimage;
        float x, y, z,rx,ry,rz;
        dropDownItem = this.GetComponent<Dropdown>();
        int index = dropDownItem.value;//选中的项的索引号
//        Debug.Log(dropDownItem.options[index].text);//输出options相应选择项的名字
        switch (index)
        {
            case 0:
                   /*tempimage = dropDownItem.options[index].image;
                   dropDownItem.options[index].image = dropDownItem.options[5].image;
                   dropDownItem.options[5].image = tempimage;//两个图标交换,以达到把不用的图标放到最后面的效果*/
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = Color.yellow;
                Debug.Log("color change is ok");
                x =GameObject.FindGameObjectWithTag("Screw1").transform.position.x;
                y= GameObject.FindGameObjectWithTag("Screw1").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw1").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x,y,z);
                GameObject.Find("Probe").transform.eulerAngles= new Vector3(rx, ry, rz);               
                Debug.Log("0 is Screw A");
                break;
            case 1:
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("color change is ok");
                x = GameObject.FindGameObjectWithTag("Screw2").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw2").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw2").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Length").GetComponent<Text>().text = "10";
                GameObject.FindGameObjectWithTag("diameter").GetComponent< Text >().text="5";

                Debug.Log("1 is Screw B");
                break;
            case 2:
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = Color.red;
                x = GameObject.FindGameObjectWithTag("Screw3").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw3").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw3").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Length").GetComponent<Text>().text = "11";
                GameObject.FindGameObjectWithTag("diameter").GetComponent<Text>().text = "7";

                Debug.Log("2 is Screw C");
                break;
            case 3:
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = new Color(254, 0, 254);
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = new Color(254,0,254);//pink
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = new Color(254,0,254);//pink
                x = GameObject.FindGameObjectWithTag("Screw4").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw4").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw4").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Length").GetComponent<Text>().text = "10";
                GameObject.FindGameObjectWithTag("diameter").GetComponent<Text>().text = "6";

                Debug.Log("3 is Screw D");
                break;
            case 4:
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = Color.white;
                x = GameObject.FindGameObjectWithTag("Screw5").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw5").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw5").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Length").GetComponent<Text>().text = "9";
                GameObject.FindGameObjectWithTag("diameter").GetComponent<Text>().text = "5";

                Debug.Log("4 is Screw E");
                break;
            case 5:
                GameObject.Find("Probe").GetComponent<Renderer>().material.color = Color.cyan;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview").GetComponent<Renderer>().material.color = Color.cyan;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview").GetComponent<Renderer>().material.color = Color.cyan;
                x = GameObject.FindGameObjectWithTag("Screw6").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw6").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw6").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.z;
                GameObject.Find("Probe").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Length").GetComponent<Text>().text = "12";
                GameObject.FindGameObjectWithTag("diameter").GetComponent<Text>().text = "6";

                Debug.Log("5 is Screw F");
                break;
            case 6:
                allflag = true;           
                break;
            case 7:
                allflag = false;
                GameObject.Destroy(clonescrewH);
                GameObject.Destroy(clonescrew1H);
                GameObject.Destroy(clonescrew2H);
                GameObject.Destroy(clonescrew3H);
                GameObject.Destroy(clonescrew4H);
                GameObject.Destroy(clonescrew5H);
                GameObject.Destroy(clonescrewV);
                GameObject.Destroy(clonescrew1V);
                GameObject.Destroy(clonescrew2V);
                GameObject.Destroy(clonescrew3V);
                GameObject.Destroy(clonescrew4V);
                GameObject.Destroy(clonescrew5V);
                GameObject.Find("Probe2").transform.position = new Vector3(0.2f, 0, 0);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(0, 0, 0);//还原利用过的probe2
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition= new Vector3(0, 1.2f, 0);
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localEulerAngles= new Vector3(0, 0, 0);//要还原以备主流应用，否则以screw6的姿势贴在CT Hview上了
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition = new Vector3(0, 1.2f, 0);
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localEulerAngles= new Vector3(0, 0, 0);//要还原以备主流应用，否则以screw6的姿势贴在CT Vview上了
                GameObject.FindGameObjectWithTag("Screw1").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Screw2").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Screw3").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Screw4").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Screw5").GetComponent<MeshRenderer>().enabled = false;
                GameObject.FindGameObjectWithTag("Screw6").GetComponent<MeshRenderer>().enabled = false;
                tooltip.SetActive(false);
                tooltip1.SetActive(false);
                tooltip2.SetActive(false);
                tooltip3.SetActive(false);
                tooltip4.SetActive(false);
                tooltip5.SetActive(false);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (allflag)
        {
            if (!updateflag)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = Color.yellow;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = Color.yellow;
                x = GameObject.FindGameObjectWithTag("Screw1").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw1").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw1").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw1").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw1").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw1
                GameObject.FindGameObjectWithTag("Screw1").GetComponent<Renderer>().material.color = Color.yellow;
                tooltip.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;
                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrewH = Instantiate(ScrewObj);
                clonescrewH.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrewH.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrewH.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrewH.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrewH.GetComponent<Renderer>().material.color = Color.yellow;
                clonescrewH.name = ("clone1H");
                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrewV = Instantiate(ScrewObj);

                clonescrewV.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrewV.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrewV.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrewV.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrewV.GetComponent<Renderer>().material.color = Color.yellow;
                clonescrewV.name = ("clone1V");
                updateflag = true;//只更新一次
            }
            if (!updateflag1)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = Color.green;
                x = GameObject.FindGameObjectWithTag("Screw2").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw2").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw2").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw2").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw2").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw2
                GameObject.FindGameObjectWithTag("Screw2").GetComponent<Renderer>().material.color = Color.green;
                tooltip1.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;
                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrew1H = Instantiate(ScrewObj);
                clonescrew1H.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrew1H.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrew1H.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrew1H.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrew1H.GetComponent<Renderer>().material.color = Color.green;
                clonescrew1H.name = ("clone2H");
                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrew1V = Instantiate(ScrewObj);
                clonescrew1V.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrew1V.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrew1V.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrew1V.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrew1V.GetComponent<Renderer>().material.color = Color.green;
                clonescrew1V.name = ("clone2V");
                updateflag1 = true;//只更新一次
            }
            if (!updateflag2)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = Color.red;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = Color.red;
                x = GameObject.FindGameObjectWithTag("Screw3").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw3").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw3").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw3").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw3").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw3
                GameObject.FindGameObjectWithTag("Screw3").GetComponent<Renderer>().material.color = Color.red;
                tooltip2.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;
                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrew2H = Instantiate(ScrewObj);
                clonescrew2H.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrew2H.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrew2H.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrew2H.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrew2H.GetComponent<Renderer>().material.color = Color.red;

                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrew2V = Instantiate(ScrewObj);
                clonescrew2V.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrew2V.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrew2V.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrew2V.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrew2V.GetComponent<Renderer>().material.color = Color.red;
                updateflag2 = true;//只更新一次
            }
            if (!updateflag3)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = new Color(254, 0, 254);
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = new Color(254, 0, 254);
                x = GameObject.FindGameObjectWithTag("Screw4").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw4").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw4").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw4").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw4").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw4
                GameObject.FindGameObjectWithTag("Screw4").GetComponent<Renderer>().material.color = new Color(254, 0, 254);
                tooltip3.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;
                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrew3H = Instantiate(ScrewObj);
                clonescrew3H.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrew3H.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrew3H.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrew3H.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrew3H.GetComponent<Renderer>().material.color = new Color(254, 0, 254);

                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrew3V = Instantiate(ScrewObj);
                clonescrew3V.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrew3V.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrew3V.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrew3V.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrew3V.GetComponent<Renderer>().material.color = new Color(254, 0, 254);
                updateflag3 = true;//只更新一次
            }
            if (!updateflag4)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = Color.white;
                x = GameObject.FindGameObjectWithTag("Screw5").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw5").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw5").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw5").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw5").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw5
                GameObject.FindGameObjectWithTag("Screw5").GetComponent<Renderer>().material.color = Color.white;
                tooltip4.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;
                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrew4H = Instantiate(ScrewObj);
                clonescrew4H.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrew4H.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrew4H.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrew4H.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrew4H.GetComponent<Renderer>().material.color = Color.white;

                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrew4V = Instantiate(ScrewObj);
                clonescrew4V.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrew4V.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrew4V.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrew4V.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrew4V.GetComponent<Renderer>().material.color = Color.white;
                updateflag4 = true;//只更新一次
            }
            if (!updateflag5)
            {
                float x, y, z, rx, ry, rz;
                GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").GetComponent<Renderer>().material.color = Color.cyan;
                GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").GetComponent<Renderer>().material.color = Color.cyan;
                x = GameObject.FindGameObjectWithTag("Screw6").transform.position.x;
                y = GameObject.FindGameObjectWithTag("Screw6").transform.position.y;
                z = GameObject.FindGameObjectWithTag("Screw6").transform.position.z;
                rx = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.x;//.rotation.x;
                ry = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.y;
                rz = GameObject.FindGameObjectWithTag("Screw6").transform.eulerAngles.z;
                GameObject.Find("Probe2").transform.position = new Vector3(x, y, z);
                GameObject.Find("Probe2").transform.eulerAngles = new Vector3(rx, ry, rz);
                GameObject.FindGameObjectWithTag("Screw6").GetComponent<MeshRenderer>().enabled = true;//渲染以显示screw6
                GameObject.FindGameObjectWithTag("Screw6").GetComponent<Renderer>().material.color = Color.cyan;
                tooltip5.SetActive(true);

                if (++curInterval < interval)
                {
                    return;
                }
                curInterval = 0;

                
                //查找对象
                GameObject ScrewObj = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2");
                clonescrew5H = Instantiate(ScrewObj);
                clonescrew5H.transform.SetParent(GameObject.Find("CTPlanes/CTPlane1").transform);
                clonescrew5H.transform.localPosition = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localPosition;
                clonescrew5H.transform.localRotation = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localRotation;
                clonescrew5H.transform.localScale = GameObject.Find("CTPlanes/CTPlane1/ScrewHview2").transform.localScale;
                clonescrew5H.GetComponent<Renderer>().material.color = Color.cyan;

                ScrewObj = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2");
                clonescrew5V = Instantiate(ScrewObj);
                clonescrew5V.transform.SetParent(GameObject.Find("CTPlanes/CTPlane2").transform);
                clonescrew5V.transform.localPosition = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localPosition;
                clonescrew5V.transform.localRotation = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localRotation;
                clonescrew5V.transform.localScale = GameObject.Find("CTPlanes/CTPlane2/ScrewVview2").transform.localScale;
                clonescrew5V.GetComponent<Renderer>().material.color = Color.cyan;
                updateflag5 = true;//只更新一次
            }
        }

    }

}
