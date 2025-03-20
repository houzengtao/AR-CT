using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_WSA_10_0
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

public class PatientsController : MonoBehaviour
{
    // Manipulation Scene References
    public GameObject patientManipOne, patientManipTwo;
    private GameObject patientManip;

//    public GameObject pinchSliderHor, pinchSliderVer;

    public CTReader cTReader;
    public TextAsset scansOne;//, scansTwo;
    private TextAsset scans;

    // Screw Scene References
//    public GameObject screwPatOne, screwPatTwo;
//    private GameObject patientScrew;

    // Dummy Object at (0,0,0)
//    public GameObject dummyObject;

//    public GlobalController globalController;
//    public ScrewSceneController screwController;

//    public TextAsset latScrewOne, distScrewOne, medScrewOne,
//        latScrewTwo, distScrewTwo, medScrewTwo;
//    private TextAsset latScrew, distScrew, medScrew;

//    private char sep = Path.DirectorySeparatorChar;

    public void Init()
    {
 //       TutunePinchSliders();
//        TutuneTranslation();
    }

/*    private void TutuneTranslation()
    {
//        Debug.Log("TutuneTranslation******************");
        Vector3 refCenter = CTConstants.REFERENCE_CENTER;
        Vector3 newCenter = cTReader.GetCenterOfCt();
//        Debug.Log($"Reference's center position is {refCenter}");
//        Debug.Log($"cTReader's center position is {newCenter}");
        float xTranslation = refCenter.x - newCenter.x,
            yTranslation = refCenter.y - newCenter.y,
            zTranslation = refCenter.z - newCenter.z;
        patientManip.transform.localPosition = new Vector3(xTranslation, yTranslation, zTranslation);
//        Debug.Log($"to reference translation is {patientManip.transform.localPosition}");
        Vector3 pshPos = pinchSliderHor.transform.localPosition,
            psvPos = pinchSliderVer.transform.localPosition;
        pinchSliderHor.transform.localPosition = new Vector3(
            pshPos.x + xTranslation, pshPos.y + yTranslation, pshPos.z + zTranslation);
 //       Debug.Log($"New Hslider's position=old's+STL's: {pinchSliderHor.transform.localPosition}");
        pinchSliderVer.transform.localPosition = new Vector3(
            psvPos.x + xTranslation, psvPos.y + yTranslation, psvPos.z + zTranslation);
 //       Debug.Log($"New Vslider's position=old's+STL's: {pinchSliderVer.transform.localPosition}");
        foreach (GameObject go in cTReader.GetPoints())
        {
            Vector3 goPos = go.transform.localPosition;
            go.transform.localPosition = new Vector3(
                goPos.x + xTranslation, goPos.y + yTranslation, goPos.z + zTranslation);
        }
    }*/

 /*   private void TutunePinchSliders()
    {
//        Debug.Log("TutunePinchSliders******************");
        pinchSliderHor.transform.localPosition = cTReader.center.transform.localPosition;
        pinchSliderVer.transform.localPosition = cTReader.center.transform.localPosition;
//        Debug.Log($"cTReader's center position is {cTReader.center.transform.localPosition}");
        pinchSliderHor.transform.localScale = new Vector3(400f, 400f, 400f);
        pinchSliderHor.transform.localEulerAngles= new Vector3(0f, 90f, 0f);
        pinchSliderVer.transform.localScale = new Vector3(400f, 400f, 400f);
        pinchSliderVer.transform.localEulerAngles = new Vector3(180f, 0f, 0f);

        PinchSlider psH = pinchSliderHor.GetComponentInChildren<PinchSlider>();
        float sliderLength = (Math.Abs(cTReader.bottomBackRight.transform.localPosition.z - cTReader.bottomBackLeft.transform.localPosition.z))
            / (2 * pinchSliderHor.transform.localScale.z);//为什么除以的是这个量？？
        psH.SliderStartDistance = sliderLength;
        psH.SliderEndDistance = -sliderLength;
 //       Debug.Log($"cTReader's bottomBackRight position is {cTReader.bottomBackRight.transform.localPosition};BackLeft is {cTReader.bottomBackLeft.transform.localPosition}");
 //       Debug.Log($"HsliderLength=cTReader's right.z-left.z: {sliderLength}");
        // 1 : .250 = x : 2*sliderLength --> x = .9725 / .250
        float newScaleX = (2 * sliderLength) / 0.250f;//为什么是这个算式？跟CT图片个数有没有关系？官方例子的值大小为1.18用尺子实际测量约=11.8cm
        psH.transform.GetChild(0).transform.localScale = new Vector3(newScaleX, 1f, 1f);//psV.transform.GetChild(0)就是TrackVisuals，这里赋值其长度
        psH.transform.localPosition = new Vector3(cTReader.bottomFrontLeft.transform.localPosition.x,
            cTReader.bottomFrontLeft.transform.localPosition.y,
            psH.transform.localPosition.z);
 //       Debug.Log($"{psH.transform.GetChild(0).name}'s newScaleX is {newScaleX}");
//        Debug.Log($"psH's location {psH.transform.localPosition}");
        PinchSlider psV = pinchSliderVer.GetComponentInChildren<PinchSlider>();
        sliderLength = (Math.Abs(cTReader.bottomFrontLeft.transform.localPosition.x - cTReader.bottomBackLeft.transform.localPosition.x))
            / (2 * pinchSliderVer.transform.localScale.z);
        psV.SliderStartDistance = sliderLength;
        psV.SliderEndDistance = -sliderLength;
//        Debug.Log($"cTReader's bottomFrontLeft position is {cTReader.bottomFrontLeft.transform.localPosition};BackLeft is {cTReader.bottomBackLeft.transform.localPosition}");
//        Debug.Log($"VsliderLength=cTReader's FrontLeft.x-BackLeft.x:{sliderLength}");
        newScaleX = (2 * sliderLength) / 0.250f;
        psV.transform.GetChild(0).transform.localScale = new Vector3(newScaleX, 1f, 1f);
        psV.transform.localPosition = new Vector3(psV.transform.localPosition.x,
            cTReader.bottomFrontRight.transform.localPosition.y,
            cTReader.bottomFrontRight.transform.localPosition.z);
//        Debug.Log($"{psV.transform.GetChild(0).name}'s newScaleX is {newScaleX}");
 //       Debug.Log($"psV's location {psV.transform.localPosition}");
    }*/

    private GameObject findChildrenWithName(Transform parent, String name)
    {
        foreach (Transform child in parent)
        {
            if (child.name.Equals(name))
            {
                return child.gameObject;
            }
            GameObject res = findChildrenWithName(child, name);
            if (res != null)
            {
                return res;
            }
        }

        return null;
    }

    public static void CenterToRef(GameObject obj, Vector3 referencePosition)
    {
        obj.transform.position = obj.transform.position -
            obj.transform.gameObject.GetComponentInChildren<Renderer>().bounds.center +
            referencePosition;
    }
    public static void SameResizeToRef(GameObject obj, Vector3 referenceSize)
    {
        Vector3 oldLocalScale = obj.transform.localScale;
        Vector3 oldSize = obj.GetComponentInChildren<Renderer>().bounds.size;
        float newX = oldLocalScale.x / oldSize.x * referenceSize.x;
        obj.transform.localScale = new Vector3(newX, newX, newX);
    }

    private Bounds RetrieveCombinedBounds(GameObject parent)
    {
        Renderer renderer = parent.GetComponentInChildren<Renderer>();
        Bounds combinedBounds = renderer.bounds;
        Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renderers)
        {
            if (render != renderer) combinedBounds.Encapsulate(render.bounds);
        }
        return combinedBounds;
    }

    public void PickNewPatient(bool first)
    {
        /*        patientManip = first ? patientManipOne : patientManipTwo;
                  patientManip.SetActive(true);
                 globalController.patient = patientManip;

                 patientScrew = first ? screwPatOne : screwPatTwo;
                 patientScrew.SetActive(true);
                 screwController.patient = patientScrew;

                 latScrew = first ? latScrewOne : latScrewTwo;
                 medScrew = first ? medScrewOne : medScrewTwo;
                 distScrew = first ? distScrewOne : distScrewTwo;
                 screwController.screwLatPositions = latScrew;
                 screwController.screwMedPositions = medScrew;
                 screwController.screwDistPositions = distScrew;
            */
        //       scans = first ? scansOne : scansTwo;
        patientManip = patientManipOne;
        cTReader.ct = scansOne;

        cTReader.Init();

        //       TutunePinchSliders();
        //        TutuneTranslation();
        Vector3 refCenter = CTConstants.REFERENCE_CENTER;
        Vector3 newCenter = cTReader.GetCenterOfCt();

        //OBJ对齐
        float xTranslation = refCenter.x - newCenter.x,
            yTranslation = refCenter.y - newCenter.y,
            zTranslation = refCenter.z - newCenter.z;
        patientManip.transform.localPosition = new Vector3(xTranslation, yTranslation, zTranslation);
        
        foreach (GameObject go in cTReader.GetPoints())//以下CT对齐
        {
            Vector3 goPos = go.transform.localPosition;
            go.transform.localPosition = new Vector3(
                goPos.x + xTranslation, goPos.y + yTranslation, goPos.z + zTranslation);
        }
        cTReader.CenterToCCCT();

//        globalController.Init();
//        screwController.Init();
//        globalController.GoToManipScene();
//        GameObject.Find("PatientSelection").SetActive(false);//隐藏对话框
    }

}

static class CTConstants
{
    //   public static readonly Vector3 REFERENCE_CENTER = new Vector3(-3.244141f, -226.2559f, -248.5f);
 //   public static readonly Vector3 REFERENCE_CENTER = new Vector3(-3.244141f, -226.2559f, -67.4f);
    public static readonly Vector3 REFERENCE_CENTER = new Vector3(0.0f, 0.0f, 0.0f);
}