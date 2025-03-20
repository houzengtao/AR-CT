using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Linq;
using UnityEngine;

public class Screwslice : MonoBehaviour
{
    public CTReader ct;
    public int width, height;
    public bool disaligned=false;
    public int interval;
    int curInterval = 0;
    public Texture2D tex;
    Vector4 bCol;
    Vector3 Oldtip = Vector3.zero, Oldtip2 = Vector3.zero;
    Vector3 tip = Vector3.zero, tip2 = Vector3.zero;
    public enum Axis { X, Y, Z };
    public Axis axis;
    void Start()
    {
        Init();
    }

    public void Init()
    {
        tex = NewTexture(width, height, Color.black);
        /* 以下两句是以前打算挂在到screw上的，后来遇到点儿问题放弃了，现在改挂在CTplane上*/
        //        GameObject.Find("CTPlane1").GetComponent<Renderer>().material.mainTexture = tex;
        //        GameObject.Find("CTPlane2").GetComponent<Renderer>().material.mainTexture = tex;
        GetComponent<Renderer>().material.mainTexture = tex;
        Oldtip = GameObject.Find("Probe/DownEnd").transform.position;//顶端初始化
        tip = GameObject.Find("Probe/DownEnd").transform.position;//顶端初始化
    }

    private Texture2D NewTexture(int width, int height, Color color)
    {
        var texture = new Texture2D(width, height);
        Color[] pixels = Enumerable.Repeat(color, width * height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }

    public void UpdateHelper()
    {
              
        var orig = Vector3.zero;
        var dx = Vector3.zero;
        var dy = Vector3.zero;//以上给H面用
        var val = 0.5f;// (tip[0] - ct.transform.position.x) / (ct.width / 1000);//前面的是m为单位的，所以width要除以1000转成m
        orig = new Vector3(0, 0, val);
        var distance = tip - ct.transform.position;//ct.transform.position就是CT的中心的坐标,相减后产生一个从CT中心到tip点的向量distance
        bCol = BorderColors.CYAN;
        switch (axis)
        {
            case Axis.X://V面即CTplane2选这个
               // val = (tip[2] - ct.transform.position.z) / (ct.depth / 1000);//前面的是m为单位的，所以width要除以1000转成m
               //上面那句不严谨，当CT的x,y,z轴跟全局坐标不重合即发生旋转时，上面的话就不对了
                val = Vector3.Dot(distance, ct.transform.right) / (ct.depth / 1000);//前面的是m为单位的，所以width要除以1000转成m
                orig = new Vector3(val, 0, 0);
                dx = new Vector3(0, 0, 1);
                dy = new Vector3(0, 1, 0);
                //                float rxv = transform.GetChild(0).localEulerAngles.x;
                //                float ryv = transform.GetChild(0).localEulerAngles.y;
                //                float rzv= GameObject.Find("Screw1").transform.rotation.eulerAngles.z;
                //               transform.GetChild(0).localEulerAngles = new Vector3(rxv,ryv,rzv);
                float projectxV = Vector3.Dot(GameObject.Find("Probe").transform.up,ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float projectyV = Vector3.Dot(GameObject.Find("Probe").transform.up, ct.transform.up);//在在CT的y轴上的投影
                float projectzV = Vector3.Dot(GameObject.Find("Probe").transform.up, ct.transform.forward);//在CT的z轴上的投影
//                float angleV = 360-Mathf.Atan(projectzV/ projectyV) * Mathf.Rad2Deg;//没有考虑两个方向的不同缩放引起的角度变形
                float angleV =  - Mathf.Atan((projectzV / ct.width) / (projectyV / ct.height)) * Mathf.Rad2Deg;//因ctplane的长宽方向的localscale不同，所以求角度的长度也要从广域尺寸变为局部尺寸
                                                                                                                  //即除以相应的要去到的层级的所有父物体的localscale的乘积projectzV/(ct.width*0.001)和projectyV/(ct.height*0.001)
                                                                                                                  //                transform.GetChild(0).up= new Vector3(normalx, normaly, normalz);
                transform.GetChild(0).localEulerAngles = new Vector3(0,0,angleV);
                float distancexv = Vector3.Dot(distance, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float distanceyv = Vector3.Dot(distance, ct.transform.up);//在在CT的y轴上的投影
                float distancezv = Vector3.Dot(distance, ct.transform.forward);//在CT的z轴上的投影
                var translatevalv = (transform.GetChild(0).transform.up) * (transform.GetChild(0).transform.localScale.y);
                //                transform.GetChild(0).localPosition = new Vector3(distancezv / (ct.width / 1000) + translatevalv[0], distanceyv / (ct.height / 1000) + translatevalv[1], 0);//旋转后不准
                //                transform.GetChild(0).localPosition = new Vector3(distancezv / (ct.width / 1000), distanceyv / (ct.height / 1000), 0);//中点很准
                //                  transform.GetChild(0).localPosition = new Vector3(distancezv/ (ct.width / 1000)+ Mathf.Sin(angleV-270)/ 2, distanceyv/ (ct.height / 1000) + Mathf.Cos(angleV-270) / 2, 0);//角度不行，不知为何               
                float cylinderVx = Vector3.Dot(transform.GetChild(0).transform.up, transform.right);
                float cylinderVy = Vector3.Dot(transform.GetChild(0).transform.up, transform.up);
                transform.GetChild(0).localPosition = new Vector3(distancezv / (ct.width / 1000) + cylinderVx / 2, distanceyv / (ct.height / 1000) + cylinderVy / 2, 0);//下端点，很准,即使CTplane成倍放大也不用改此句，因screw是ctplane的子物体会自动跟随放大
              
                Debug.Log($"V localEulerAngles is {angleV}");
//                Debug.Log($"translatevalv is {translatevalv},distancezv is {distancezv},distanceyv is {distanceyv}");
                break;
            case Axis.Y://官方例程的设置，不知为何
                //val = (tip[1] - ct.transform.position.y) / (ct.height / 1000);//前面的是m为单位的，所以width要除以1000转成m
                val = Vector3.Dot(distance, ct.transform.up) / (ct.height / 1000);//前面的是m为单位的，所以width要除以1000转成m
                orig = new Vector3(val, 0, 0);
                dx = new Vector3(0, 0, 1);
                dy = new Vector3(-1, 0, 0);
                break;
            case Axis.Z://H面即CTplane1选这个
                //val = (tip[0] - ct.transform.position.x) / (ct.width / 1000);//前面的是m为单位的，所以width要除以1000转成m
                val = Vector3.Dot(distance, ct.transform.forward) / (ct.width / 1000);//前面的是m为单位的，所以width要除以1000转成m,本来是ct.transform.right对应着width的，因CT cube的z对应着全局的x轴，所以这里用forward看UNITY内容
                orig = new Vector3(0, 0, val);
                dx = new Vector3(1, 0, 0);
                dy = new Vector3(0, 1, 0);
                //                float rxh = transform.GetChild(0).localEulerAngles.x;
                //                float ryh = transform.GetChild(0).localEulerAngles.y;
                //                float rzh = GameObject.Find("Screw1").transform.rotation.eulerAngles.x;
                //                transform.GetChild(0).localEulerAngles = new Vector3(rxh, ryh, rzh);
                
                float projectxH = Vector3.Dot(GameObject.Find("Probe").transform.up, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float projectyH = Vector3.Dot(GameObject.Find("Probe").transform.up, ct.transform.up);//在在CT的y轴上的投影
                float projectzH = Vector3.Dot(GameObject.Find("Probe").transform.up, ct.transform.forward);//在CT的z轴上的投影
               //  float angleH = 360-Mathf.Atan(projectxH / projectyH)* Mathf.Rad2Deg;//没有考虑两个方向的不同缩放引起的角度变形
                float angleH =  - Mathf.Atan((projectxH / ct.depth) / (projectyH / ct.height)) * Mathf.Rad2Deg;//因ctplane的长宽方向的localscale不同，所以求角度的长度也要从广域尺寸变为局部尺寸
                                                                                                                  //即除以相应的要去到的层级的所有父物体的localscale的乘积projectxH/(ct.depth*0.001)和projectyH/(ct.height*0.001)
                transform.GetChild(0).localEulerAngles = new Vector3(0, 0, angleH);
                float distancexH = Vector3.Dot(distance, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float distanceyH = Vector3.Dot(distance, ct.transform.up);//在在CT的y轴上的投影
                float distancezH = Vector3.Dot(distance, ct.transform.forward);//在CT的z轴上的投影
                var translatevalH= (transform.GetChild(0).transform.up) * (transform.GetChild(0).transform.localScale.y);
                //                transform.GetChild(0).localPosition = new Vector3(distancexH / (ct.depth / 1000), distanceyH / (ct.height/1000), 0);//中点很准
                //                 transform.GetChild(0).localPosition = new Vector3(distancexH/(ct.depth / 1000)+ Mathf.Sin(angleH-270) /2, distanceyH/ (ct.height/1000) + Mathf.Cos(angleH-270) / 2, 0);//角度不行，不知为何
                float cylinderHx = Vector3.Dot(transform.GetChild(0).transform.up, -transform.forward);
                float cylinderHy = Vector3.Dot(transform.GetChild(0).transform.up, transform.up);
                transform.GetChild(0).localPosition = new Vector3(distancexH / (ct.depth / 1000) + cylinderHx / 2, distanceyH / (ct.height / 1000) + cylinderHy / 2, 0);//很准,即使CTplane成倍放大也不用改此句，因screw是ctplane的子物体会自动跟随放大
                //                transform.GetChild(0).localPosition = new Vector3(distancexH / (ct.depth / 1000) + translatevalH[0], distanceyH / (ct.height/1000) + translatevalH[1], 0);//旋转后不准
                Debug.Log($"H localEulerAngles is {angleH}");
                Debug.Log($"Screw1.transform.up is {GameObject.Find("Probe").transform.up.x}, {GameObject.Find("Probe").transform.up.y}, {GameObject.Find("Probe").transform.up.z}");
                //                Debug.Log($"translatevalH is {translatevalH},distancexH is {distancexH},distanceyH is {distanceyH}");
                break;
        }
        //        Debug.Log($"CT's width:{ct.width},depth:{ct.depth},height:{ct.height}");//长宽高都是以mm为单位的，所以下面的计算中除了1000
//                Debug.Log($"orig is {orig}");
       
        ct.Slice(orig, dx, dy, tex, disaligned, bCol);

/*       var orig2 = Vector3.zero;//以下给V面用
        var dx2 = Vector3.zero;
        var dy2 = Vector3.zero;
        var val2 = (tip[2] - ct.transform.position.z)/ct.depth;////前面的是m为单位的，所以depth要除以1000转成m，val2在-0.5--0.5之间
        orig2 = new Vector3(val2, 0, 0);
        dx2 = new Vector3(0, 0, 1);
        dy2 = new Vector3(0, 1, 0);
        bCol = BorderColors.YELLOW;
        Debug.Log($"CT's center location:{ct.transform.position}");       
        Debug.Log($"orig2 is {orig2}");
        ct.Slice(orig2, dx2, dy2, tex, disaligned, bCol);//给V面即CTPlane2*/
    }
    public void UpdateHelper2()
    {
        var distance2 = tip2 - ct.transform.position;
        switch (axis)
        {
            case Axis.X://V面即CTplane2选这个
                float projectxV = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float projectyV = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.up);//在在CT的y轴上的投影
                float projectzV = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.forward);//在CT的z轴上的投影
                                                                                                           //                float angleV = 360-Mathf.Atan(projectzV/ projectyV) * Mathf.Rad2Deg;//没有考虑两个方向的不同缩放引起的角度变形
                float angleV = -Mathf.Atan((projectzV / ct.width) / (projectyV / ct.height)) * Mathf.Rad2Deg;//因ctplane的长宽方向的localscale不同，所以求角度的长度也要从广域尺寸变为局部尺寸
                                                                                                             //即除以相应的要去到的层级的所有父物体的localscale的乘积projectzV/(ct.width*0.001)和projectyV/(ct.height*0.001)
                                                                                                             //                transform.GetChild(0).up= new Vector3(normalx, normaly, normalz);
                transform.GetChild(1).localEulerAngles = new Vector3(0, 0, angleV);
                float distancexv = Vector3.Dot(distance2, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float distanceyv = Vector3.Dot(distance2, ct.transform.up);//在在CT的y轴上的投影
                float distancezv = Vector3.Dot(distance2, ct.transform.forward);//在CT的z轴上的投影
                float cylinderVx = Vector3.Dot(transform.GetChild(1).transform.up, transform.right);
                float cylinderVy = Vector3.Dot(transform.GetChild(1).transform.up, transform.up);
                transform.GetChild(1).localPosition = new Vector3(distancezv / (ct.width / 1000) + cylinderVx / 2, distanceyv / (ct.height / 1000) + cylinderVy / 2, 0);//下端点，很准,即使CTplane成倍放大也不用改此句，因screw是ctplane的子物体会自动跟随放大

                break;
            case Axis.Y://官方例程的设置，不知为何                     
                break;
            case Axis.Z://H面即CTplane1选这个
                float projectxH = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float projectyH = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.up);//在在CT的y轴上的投影
                float projectzH = Vector3.Dot(GameObject.Find("Probe2").transform.up, ct.transform.forward);//在CT的z轴上的投影
                                                                                                           //  float angleH = 360-Mathf.Atan(projectxH / projectyH)* Mathf.Rad2Deg;//没有考虑两个方向的不同缩放引起的角度变形
                float angleH = -Mathf.Atan((projectxH / ct.depth) / (projectyH / ct.height)) * Mathf.Rad2Deg;//因ctplane的长宽方向的localscale不同，所以求角度的长度也要从广域尺寸变为局部尺寸
                                                                                                             //即除以相应的要去到的层级的所有父物体的localscale的乘积projectxH/(ct.depth*0.001)和projectyH/(ct.height*0.001)
                transform.GetChild(1).localEulerAngles = new Vector3(0, 0, angleH);
                float distancexH = Vector3.Dot(distance2, ct.transform.right);//点乘，对后面的单位向量来说就是在其上的投影长度
                float distanceyH = Vector3.Dot(distance2, ct.transform.up);//在在CT的y轴上的投影
                float distancezH = Vector3.Dot(distance2, ct.transform.forward);//在CT的z轴上的投影
                float cylinderHx = Vector3.Dot(transform.GetChild(1).transform.up, -transform.forward);
                float cylinderHy = Vector3.Dot(transform.GetChild(1).transform.up, transform.up);
                transform.GetChild(1).localPosition = new Vector3(distancexH / (ct.depth / 1000) + cylinderHx / 2, distanceyH / (ct.height / 1000) + cylinderHy / 2, 0);//很准,即使CTplane成倍放大也不用改此句，因screw是ctplane的子物体会自动跟随放大
                break;
        }
    }
    void Update()
    {
        if (++curInterval < interval)
        {
            return;
        }
        curInterval = 0;
//        Debug.Log($"tip eulerAngles are {GameObject.Find("Screw1").transform.rotation.eulerAngles}");
 //       Debug.Log($"tip rotation is {GameObject.Find("Screw1").transform.rotation}");
//        Debug.Log($"tip localRotation is {GameObject.Find("Screw1").transform.localRotation}");
        tip = GameObject.Find("Probe/DownEnd").transform.position; ;//时时更新顶端当前值
        tip2 = GameObject.Find("Probe2/DownEnd").transform.position; ;//时时更新顶端当前值
        if (Oldtip != tip)//如果当前值发生变化即移动了此screw
        {
//            Debug.Log($"cyclinder eulerAngles are {GameObject.Find("Screw1").transform.rotation.eulerAngles}");
//            Debug.Log($"cyclinder rotation is {GameObject.Find("Screw1").transform.rotation}");
//            Debug.Log($"cyclinder localRotation is {GameObject.Find("Screw1").transform.localRotation}");
//            Debug.Log($"CTplane eulerAngles are {GameObject.Find("BoneManipulation/Bone_1/CTGroup/CTPlane1").transform.eulerAngles}");
//            Debug.Log($"CTplane eulerAngles are {GameObject.Find("BoneManipulation/Bone_1/CTGroup/CTPlane1").transform.rotation.eulerAngles}");
////          Debug.Log($"CTplane up Angles is {Vector3.Angle(transform.up, Vector3.up)}");
//            Debug.Log($"CTplane&RIGHT Angles is {Vector3.Angle(transform.up, Vector3.right)}");
//            Debug.Log($"ScrewHview up Angles is {Vector3.Angle(GameObject.Find("BoneManipulation/Bone_1/CTGroup/CTPlane1/ScrewHview").transform.up, Vector3.up)}");
//            Debug.Log($"cyclinder NORMALized is {GameObject.Find("Screw1").transform.up.normalized}");
//            Debug.Log($"cyclinder&RIGHT Angles is {Vector3.Angle(GameObject.Find("Screw1").transform.up, Vector3.right)}");
//            Debug.Log($"cyclinder vector is {GameObject.Find("Screw1").transform.up}");
            var bottompoint = GameObject.Find("Probe").transform.position - (GameObject.Find("Probe").transform.up) * (GameObject.Find("Probe").transform.localScale.y);//圆柱的底端点
//            Debug.Log($"bottompoint position is {bottompoint}");
//            Debug.Log($"tip position is {tip}");
//            Debug.Log($"CT right vector is {ct.transform.right},forward vector is {ct.transform.forward},up vector is {ct.transform.up}");
//            Debug.Log($"CT right vector normalized is {ct.transform.right.normalized},forward normalized is {ct.transform.forward.normalized},up normalized is {ct.transform.up.normalized}");
            //            Vector3.Angle(GameObject.Find("BoneManipulation/Bone_1/CTGroup/CTPlane1/ScrewHview").transform.up, Vector3.up);
            var distance = tip - ct.transform.position;//ct.transform.position就是CT的中心的坐标,相减后产生一个从CT中心到tip点的向量distance
            
            //            Debug.Log($"ct.transform.position is {ct.transform.position}");
            //            Debug.Log($"bone.transform.position is {GameObject.Find("Bone").transform.position}");
            //            Debug.Log($"obj.transform.position is {GameObject.FindGameObjectWithTag("patientobj").transform.position}");
            //            Debug.Log($"ct.ctcenter position is {ct.ctCenter}");
            //            Debug.Log($"ct.GetCenterOfCt is {ct.GetCenterOfCt()}");

            //            Debug.Log($"tip position is {tip}");
            //                       Debug.Log($"distance is {distance}");
            //         if ((Math.Abs(distance[0]) <= ct.width/2000)&& (Math.Abs(distance[1]) <= ct.height/2000)&& (Math.Abs(distance[2]) <= ct.depth/2000))//如果screw已经在在ct内部,长宽高要另外除以1000转成m为单位
            if ((Math.Abs(Vector3.Dot(distance, ct.transform.forward)) <= ct.width / 2000) && (Math.Abs(Vector3.Dot(distance, ct.transform.up)) <= ct.height / 2000) && (Math.Abs(Vector3.Dot(distance, ct.transform.right)) <= ct.depth / 2000))//上面的那个不严谨，应该是投影到CT上再比对距离而不似乎简单的xyz比较，因CT有可能有转角呀！
                UpdateHelper();
            Oldtip = tip;//更新顶端的记忆值
        }
        if (Oldtip2 != tip2)//如果当前值发生变化即移动了此screw
        {
            var distance2 = tip2 - ct.transform.position;
            if ((Math.Abs(Vector3.Dot(distance2, ct.transform.forward)) <= ct.width / 2000) && (Math.Abs(Vector3.Dot(distance2, ct.transform.up)) <= ct.height / 2000) && (Math.Abs(Vector3.Dot(distance2, ct.transform.right)) <= ct.depth / 2000))//上面的那个不严谨，应该是投影到CT上再比对距离而不似乎简单的xyz比较，因CT有可能有转角呀！
                UpdateHelper2();
            Oldtip2 = tip2;//更新顶端的记忆值
        }
    }
}
