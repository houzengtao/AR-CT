using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Screwcontrol : MonoBehaviour
{
    public GameObject Screw1;
    public GameObject Screw2;
    public void toggleScrew()
    {
        if (!Screw1.activeInHierarchy)
        {
            Screw1.SetActive(true);
        }
/*        else
        {
            Screw1.SetActive(false);
        }*/
        if (!Screw2.activeInHierarchy)
        {
            Screw2.SetActive(true);
        }
/*        else
        {
            Screw2.SetActive(false);
        }*/
    }
    /*   public GameObject myPrefab;
       public GameObject bone;
       public GameObject muscle;
       public GameObject tooltips;

       public void NewScrew()
       {
           Vector3 pos = GameObject.Find("BackButton2").transform.position;

           GameObject screw = Instantiate(myPrefab, pos, Quaternion.identity) as GameObject;
           screw.transform.parent = GameObject.Find("ImageTarget2").transform;

           //GameObject.Find("ScrewsObjManipulation").transform.position;
           Debug.Log("Newscrew");
           Debug.Log(pos);

           //Debug.Log(GameObject.Find("ScrewsObjManipulation").transform.position);
       }

       public void toggleToolTips()
       {
           if(!tooltips.activeInHierarchy){
                tooltips.SetActive(true);
            } else {
                tooltips.SetActive(false);
            }
       }

       public void toggleBone()
       {
           if(!bone.activeInHierarchy){
                bone.SetActive(true);
            } else {
                bone.SetActive(false);
            }
       }

       public void toggleMuscle()
       {
           if(!muscle.activeInHierarchy){
                muscle.SetActive(true);
            } else {
                muscle.SetActive(false);
            }
       }*/
    /*StartCoroutine��unity3d�İ����н���Э�̣���˼��������һ���������̡߳�

��C#��ֱ����Thread����̣߳�������unity����ЩԪ���ǲ��ܲ����ġ����ʱ���ܹ�ʹ��Э������ϡ�

ʹ���̵߳��ŵ���ǲ�����ֽ��濨���������������һ�κܴ��������㡣û���߳̾ͻ���ּ����������
   ��C#��Э��Ҫ����ΪIEnumerator ������ͣ�javascript�в���Ҫ��ʹ��IEnumerator �������ʱ��������yield return�����ؽ��������Ϊ����ʱ��ʾΪ֡����
��yield return 1 ��ʾÿһ֡����һ�ν����*/
    public void getNewPosition()
    {
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        //       using (UnityWebRequest request = UnityWebRequest.Get("https://no-fall.000webhostapp.com/position.json"))
        using (UnityWebRequest request = UnityWebRequest.Get("http://houzengtao.cn/position.json"))
        {
            yield return request.Send();

            if (request.isNetworkError) // Error
            {
                Debug.Log("Get request error");
                Debug.Log(request.error);
            }
            else // Success
            {   
                Debug.Log("Get request successful");
         
                ScrewPosArray newPosArray = JsonUtility.FromJson<ScrewPosArray>(request.downloadHandler.text);
                Debug.Log("Refreshing screw positions from network");

                Debug.Log("Srew1 Original Position:");
                Debug.Log(GameObject.Find("Screw1").transform.position);

                ScrewPos newPos1 = newPosArray.getItems()[0];
                ScrewPos newPos2 = newPosArray.getItems()[1];

                GameObject.Find("Screw1").transform.position = new Vector3(newPos1.getXPos(), newPos1.getYPos(), newPos1.getZPos());
         //       GameObject.Find("Screw1").transform.localEulerAngles = new Vector3(6.679f, -0.256f, -0.9717f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);Ҳ����
                GameObject.Find("Screw1").transform.rotation = Quaternion.Euler(6.679f, -0.256f, -0.9717f);
                GameObject.Find("Screw2").transform.position = new Vector3(newPos2.getXPos(), newPos2.getYPos(), newPos2.getZPos());
         //       GameObject.Find("Screw2").transform.localEulerAngles= new Vector3(-6.342f, 0.681f, -8.19f);//xxx.transform.rotation=Quaternion.Euler(0.0f,0.0f,0.0f);Ҳ����
                GameObject.Find("Screw2").transform.rotation = Quaternion.Euler(-6.342f, 0.681f, -8.19f);
                Debug.Log("Screw1 New Position:");
                Debug.Log(GameObject.Find("Screw1").transform.position);
                Debug.Log($"Screw2 New Position:{newPos2.getXPos()},{newPos2.getYPos()},{newPos2.getZPos()}");
               // Debug.Log(GameObject.Find("Screw2").transform.position);

            }
        }
    }

}

[Serializable]
 public class ScrewPosArray
 {
     public ScrewPos[] Items;
     public ScrewPos[] getItems(){
         return Items;
     }
 }

[Serializable]
public class ScrewPos
{
    public float xPos;
    public float yPos;
    public float zPos;
    public float getXPos(){
        return xPos;
    }
    public float getYPos(){
        return yPos;
    }
    public float getZPos(){
        return zPos;
    }
}
