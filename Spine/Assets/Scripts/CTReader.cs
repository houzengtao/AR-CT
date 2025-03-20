using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UnityEngine;

public class CTReader : MonoBehaviour
{
    public TextAsset ct;
    public ComputeShader slicer;
//    public GameObject sliderH, sliderV;
//    public GameObject quadH, revQuadH, quadV, revQuadV, ctPlaneV, ctPlaneH;
    public GameObject  ctPlaneV, ctPlaneH;
    int kernel;

    [HideInInspector]
    public GameObject bottomBackLeft, bottomBackRight, topBackLeft,
        topBackRight, bottomFrontLeft, bottomFrontRight, topFrontLeft,
        topFrontRight, center;
    [HideInInspector]
    public float ctLength, ctDepth;
    [HideInInspector]
    public Vector3 ctCenter;
    [HideInInspector]
    public byte[] ct_bytes;
    [HideInInspector]
    public float minx, maxx, miny, maxy, minz, maxz, width, height, depth;
    private NRRD nrrd;
 
    public void Init()
    {
        ct_bytes = ct.bytes;
        nrrd = new NRRD(ct_bytes);

        kernel = slicer.FindKernel("CSMain");
        var buf = new ComputeBuffer(nrrd.data.Length, sizeof(float));
        buf.SetData(nrrd.data);
        slicer.SetBuffer(kernel, "data", buf);
        slicer.SetInts("dims", nrrd.dims);
        PointCloud(nrrd);
/*
        //        Vector3 refCenter = CTConstants.REFERENCE_CENTER;
        Vector3 refCenter = new Vector3(-3.244141f, -226.2559f, -67.4f);
        Vector3 newCenter = GetCenterOfCt();

        //OBJ对齐
        float xTranslation = refCenter.x - newCenter.x,
            yTranslation = refCenter.y - newCenter.y,
            zTranslation = refCenter.z - newCenter.z;
        GameObject.Find("Patient4").transform.localPosition = new Vector3(xTranslation, yTranslation, zTranslation);

        foreach (GameObject go in GetPoints())//以下CT对齐
        {
            Vector3 goPos = go.transform.localPosition;
            go.transform.localPosition = new Vector3(
                goPos.x + xTranslation, goPos.y + yTranslation, goPos.z + zTranslation);
        }
        CenterToCCCT();
*/
    }

    private void PointCloud(NRRD nrrd)
    {
//        Debug.Log("PointCloud******************");
 //       AutoAlign autoAlign = oo.GetComponent<AutoAlign>();
//        autoAlign.SetGo(false);
 //       DummyTransformHandler dummyHandler = oo.GetComponent<DummyTransformHandler>();
 //       dummyHandler.GoToZero();

        float lengthDirection = nrrd.lengthDirection, lengthSize = nrrd.dims[2];
        ctLength = Math.Abs(lengthDirection * lengthSize);
        float depthDirection = nrrd.depthDirection, depthSize = nrrd.dims[1];
        ctDepth = Math.Abs(depthDirection * depthSize);
        ctCenter = new Vector3(
            -1 * (nrrd.origin.x + ((int)Math.Ceiling((double)(nrrd.dims[0] / 2) - 1) * nrrd.scale.x)),
            nrrd.origin.y + ((int)Math.Ceiling((double)(nrrd.dims[1] / 2) - 1) * nrrd.scale.y),
            nrrd.origin.z + ((int)Math.Ceiling((double)(nrrd.dims[2] / 2) - 1) * nrrd.scale.z)
            );

        ComputeMinMaxFloats(nrrd);//CT的界限坐标转换成了mm单位了（nrrd中的dim*scale）

        bottomBackLeft = CreateSphereFromPos(minx, miny, minz, "bottomBackLeft");
        bottomBackRight = CreateSphereFromPos(minx, miny, maxz, "bottomBackRight");
        topBackLeft = CreateSphereFromPos(minx, maxy, minz, "topBackLeft");
        topBackRight = CreateSphereFromPos(minx, maxy, maxz, "topBackRight");
        bottomFrontLeft = CreateSphereFromPos(maxx, miny, minz, "bottomFrontLeft");
        bottomFrontRight = CreateSphereFromPos(maxx, miny, maxz, "bottomFrontRight");
        topFrontLeft = CreateSphereFromPos(maxx, maxy, minz, "topFrontLeft");
        topFrontRight = CreateSphereFromPos(maxx, maxy, maxz, "topFrontRight");
        Vector3 ccct = FindCenter(minx, maxx, miny, maxy, minz, maxz);
        center = CreateSphereFromPos(ccct.x, ccct.y, ccct.z, "center");
        center.transform.localScale = new Vector3(0f, 0f, 0f);
//        Debug.Log($"ccct center's position {ccct}");
        width = maxz - minz;//就是dim[2]*scale[2][2]，因爷爷物体Bone_1绕y轴旋转了90度，Bone_1内部的相互之间x,y,z正常加减，但与其外部的要用全局方向。全局中ct的z方向和x方向对换了即你看到的最长的width在全局中是x轴上的
        height = maxy - miny;//就是dim[1]*scale[1][1],对应全局的y轴不变
        depth = maxx - minx;//就是dim[0]*scale[0][0],因爷爷物体Bone_1绕y轴旋转了90度，所以全局中对应z轴
        /*        Vector3 quadLocalScaleH = new Vector3(depth, height, 1f),
                    revQuadLocalScaleH = new Vector3(-depth, height, 1f),
                    quadLocalScaleV = new Vector3(width, height, 1f),
                    revQuadLocalScaleV = new Vector3(width, -height, 1f);
                quadH.transform.localScale = quadLocalScaleH * 0.0025f;
                revQuadH.transform.localScale = revQuadLocalScaleH * 0.0025f;
                quadV.transform.localScale = quadLocalScaleV * 0.0025f;
                revQuadV.transform.localScale = revQuadLocalScaleV * 0.0025f;*/
        //        Debug.Log($"quadH's Scale {quadH.transform.localScale}");
        //        Debug.Log($"quadV's Scale {quadV.transform.localScale}");
        /*        Vector3 ctPlaneVLocalScale = new Vector3(ctPlaneV.transform.localScale.y / Math.Abs(height / width),ctPlaneV.transform.localScale.y, ctPlaneV.transform.localScale.z);//官网的ctPlaneV的设置，以原有的ctPlaneV的y值为基础，x值放大width/height倍
                ctPlaneV.transform.localScale = ctPlaneVLocalScale;*/

        //        Vector3 ctPlaneVLocalScale = new Vector3(width , height, ctPlaneV.transform.localScale.z);//自己后来加的,CT图像原尺寸显示
        //        Vector3 ctPlaneHLocalScale = new Vector3(depth, height, ctPlaneH.transform.localScale.z);//自己后来加的,CT图像原尺寸显示
        Vector3 ctPlaneVLocalScale = new Vector3(width * 2, height * 2, ctPlaneV.transform.localScale.z);//自己后来加的,CT图像放大2倍显示用,想几倍显示把2改为几就行了，不影响screwslice.cs中的screw位置显示，因screw是ctplane的子物体会自动跟随放大
        Vector3 ctPlaneHLocalScale = new Vector3(depth * 2, height * 2, ctPlaneH.transform.localScale.z);//自己后来加的,CT图像放大2倍显示用,想几倍显示把2改为几就行了，不影响screwslice.cs中的screw位置显示，因screw是ctplane的子物体会自动跟随放大
        ctPlaneV.transform.localScale = ctPlaneVLocalScale;//显示
        ctPlaneH.transform.localScale = ctPlaneHLocalScale ;//自己后来加的
//        Debug.Log($"ctPlaneV's Scale {ctPlaneV.transform.localScale}");
//        Debug.Log($"ctPlaneH's Scale {ctPlaneH.transform.localScale}");

        transform.localPosition = center.transform.localPosition;
        transform.localScale = new Vector3(height, depth, width);
//        Debug.Log($"CT's new Position {transform.localPosition}");
//        Debug.Log($"CT's new Scale {transform.localScale}");
 //       autoAlign.SetGo(true);
    }

    public void CenterToCCCT()
    {
        if (transform.localPosition != center.transform.localPosition)
        {
            transform.localPosition = center.transform.localPosition;
            Debug.Log($"adjusted cc local position {transform.localPosition}");
        }
    }

    /*
    public void ComputeOffsets()
    {
        foreach (var pt in GetPoints())
        {
            pt.GetComponent<AutoAlign>().ComputeOffset();
        }
    }
    */

    private void ComputeMinMaxFloats(NRRD nrrd)
    {
        int rounds = 2;
        minx = float.MaxValue;
        maxx = float.MinValue;
        miny = float.MaxValue;
        maxy = float.MinValue;
        minz = float.MaxValue;
        maxz = float.MinValue;

        for (int i = 0; i < rounds; i++)
        {
            // i : 10 == x : dim --> x = dim*i/10
            int dx = (int)(nrrd.dims[0] * i / (rounds - 1));
            for (int j = 0; j < rounds; j++)
            {
                int dy = (int)(nrrd.dims[1] * j / (rounds - 1));
                for (int k = 0; k < rounds; k++)
                {
                    int dz = (int)(nrrd.dims[2] * k / (rounds - 1));
                    float x = -1 * (nrrd.origin.x + dx * nrrd.scale.x);
                    float y = nrrd.origin.y + dy * nrrd.scale.y;
                    float z = nrrd.origin.z + dz * nrrd.scale.z;
                    minx = Math.Min(minx, x);
                    maxx = Math.Max(maxx, x);
                    miny = Math.Min(miny, y);
                    maxy = Math.Max(maxy, y);
                    minz = Math.Min(minz, z);
                    maxz = Math.Max(maxz, z);
                }
            }
        }
    }

    private GameObject CreateSphereFromPos(float x, float y, float z, String n)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, y, z);
        sphere.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        sphere.name = n;
//        sphere.transform.parent = oo.transform;

        return sphere;
    }

    private Vector3 FindCenter(float minx, float maxx, float miny, float maxy, float minz, float maxz)
    {
        float middlex = (maxx + minx) / 2,
            middley = (maxy + miny) / 2,
            middlez = (maxz + minz) / 2;

        return new Vector3(middlex, middley, middlez);
    }

    public Vector3 GetCenterOfCt()
    {
/*        AutoAlign autoAlign = oo.GetComponent<AutoAlign>();
        autoAlign.SetGo(false);
        DummyTransformHandler dummyHandler = oo.GetComponent<DummyTransformHandler>();
        dummyHandler.GoToZero();*/

        float lengthDirection = nrrd.lengthDirection, lengthSize = nrrd.dims[2];
        ctLength = Math.Abs(lengthDirection * lengthSize);
        float depthDirection = nrrd.depthDirection, depthSize = nrrd.dims[1];
        ctDepth = Math.Abs(depthDirection * depthSize);
        ctCenter = new Vector3(
            -1 * (nrrd.origin.x + ((int)Math.Ceiling((double)(nrrd.dims[0] / 2) - 1) * nrrd.scale.x)),
            nrrd.origin.y + ((int)Math.Ceiling((double)(nrrd.dims[1] / 2) - 1) * nrrd.scale.y),
            nrrd.origin.z + ((int)Math.Ceiling((double)(nrrd.dims[2] / 2) - 1) * nrrd.scale.z)
            );//不知为何x要加负号

        ComputeMinMaxFloats(nrrd);
        Vector3 ccct = FindCenter(minx, maxx, miny, maxy, minz, maxz);

 //       dummyHandler.RestoreBackup();
 //       autoAlign.SetGo(true);
        return ccct;
    }

    public GameObject[] GetPoints()
    {
        GameObject[] arr = new GameObject[] { bottomBackLeft, bottomBackRight, topBackLeft, topBackRight, bottomFrontLeft, bottomFrontRight, topFrontLeft, topFrontRight, center };
        return arr;
    }

    public void Slice(Vector3 orig, Vector3 dx, Vector3 dy, Texture2D result, bool disaligned, Vector4 bCol) {
        var rtex = new RenderTexture(result.width, result.height, 1);
        rtex.enableRandomWrite = true;
        rtex.Create();
        slicer.SetTexture(kernel, "slice", rtex);
        slicer.SetInts("outDims", new int[] { rtex.width, rtex.height });

        dx = dx * RtexConstants.SCALE;
        dy = dy * RtexConstants.SCALE;

        slicer.SetFloats("orig", new float[] { orig.x, orig.y, orig.z }); 
        slicer.SetFloats("dx", new float[] { dx.x, dx.y, dx.z });
        slicer.SetFloats("dy", new float[] { dy.x, dy.y, dy.z });
        slicer.SetFloats("borderColor", new float[] { bCol.x, bCol.y, bCol.z, bCol.w });
        slicer.Dispatch(kernel, (rtex.width + 7) / 8, (rtex.height + 7) / 8, 1);

        var oldRtex = RenderTexture.active;
        RenderTexture.active = rtex;
        result.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);
        result.Apply();
        RenderTexture.active = oldRtex;
        rtex.Release();
    }

    public Vector3 TransformWorldCoords(Vector3 p) {//此函数仅是给官网例子中的HandSlice.cs用的
        return GetComponent<Transform>().InverseTransformPoint(p);
    }

    /*    public Vector3 GetPositionFromSlider(float v, SliderSlice.Axis ax) {
            // (v+0.5) : 1 == x : delta
            Vector3 pos = new Vector3(center.transform.localPosition.x,
                center.transform.localPosition.y,
                center.transform.localPosition.z);
            switch (ax)
            {
                case SliderSlice.Axis.X:
                    // depth
                    pos.x = bottomFrontLeft.transform.localPosition.x +
                        ((bottomBackLeft.transform.localPosition.x - 
                        bottomFrontLeft.transform.localPosition.x) * (v + 0.5f));
                    break;
                case SliderSlice.Axis.Y:
                    // height
                    pos.y = bottomFrontLeft.transform.localPosition.y +
                        ((topFrontLeft.transform.localPosition.y -
                        bottomFrontLeft.transform.localPosition.y) * (v + 0.5f));
                    break;
                case SliderSlice.Axis.Z:
                    // length
                    pos.z = bottomFrontLeft.transform.localPosition.z +
                        ((bottomFrontRight.transform.localPosition.z -
                        bottomFrontLeft.transform.localPosition.z ) * (v + 0.5f));
                    break;
            }
            return pos;
        }*/
}

public class NRRD {
    readonly public Dictionary<String, String> headers = new Dictionary<String, String>();
    readonly public float[] data;
    readonly public int[] dims;

    readonly public float lengthDirection;
    readonly public float depthDirection;

    readonly public Vector3 origin = new Vector3(0, 0, 0);
    readonly public Vector3 scale = new Vector3(1, 1, 1);

    public NRRD(byte[] bytes) {
        using (var reader = new BinaryReader(new MemoryStream(bytes))) {
            for (string line = reader.ReadLine(); line.Length > 0; line = reader.ReadLine()) {
                if (line.StartsWith("#") || !line.Contains(":")) continue;
                var tokens = line.Split(':');
                var key = tokens[0].Trim();
                var value = tokens[1].Trim();
                headers.Add(key, value);
            }

            if (headers["dimension"] != "3") throw new ArgumentException("NRRD is not 3D");
            if (headers["type"] != "float") throw new ArgumentException("NRRD is not of type float");
            if (headers["endian"] != "little") throw new ArgumentException("NRRD is not little endian");
            if (headers["encoding"] != "gzip") throw new ArgumentException("NRRD is not gzip encoded");

            dims = Array.ConvertAll(headers["sizes"].Split(), s => int.Parse(s));
            if (headers.ContainsKey("space origin")) {
                var origin = Array.ConvertAll(headers["space origin"].Substring(1, headers["space origin"].Length - 2).Split(','), v => float.Parse(v, CultureInfo.InvariantCulture));
                this.origin = new Vector3(origin[0], origin[1], origin[2]);
            }
            if (headers.ContainsKey("space directions")) {
                var scale = Array.ConvertAll(headers["space directions"].Split(), s => Array.ConvertAll(s.Substring(1, s.Length - 2).Split(','), v => float.Parse(v, CultureInfo.InvariantCulture)));
                if (scale[0][0] == 0 || scale[1][1] == 0 || scale[2][2] == 0) throw new ArgumentException("NRRD has 0 scale value");
                if (scale[0][1] != 0 || scale[1][0] != 0 || scale[2][0] != 0 ||
                    scale[0][2] != 0 || scale[1][2] != 0 || scale[2][1] != 0) throw new ArgumentException("NRRD is not axis-aligned");
                this.scale = new Vector3(scale[0][0], scale[1][1], scale[2][2]);
                depthDirection = scale[1][1];
                lengthDirection = scale[2][2];
            }
//            Debug.Log($"NRRD scale is {this.scale}");
            var mem = new MemoryStream();
            using (var stream = new GZipStream(reader.BaseStream, CompressionMode.Decompress)) stream.CopyTo(mem);
            data = new float[dims[0] * dims[1] * dims[2]];
//            Debug.Log($"data size dims[0] is {dims[0]};dims[1] is {dims[1]};dims[2] is {dims[2]}");
            Buffer.BlockCopy(mem.ToArray(), 0, data, 0, data.Length * sizeof(float));
//            Debug.Log($"data length is {data.Length}");
            mem.Dispose();
            reader.Dispose();
        }
    }
}

public static class BinaryReaderExtension {
    public static string ReadLine(this BinaryReader reader) {
        var line = new StringBuilder();
        for (bool done = false; !done;) {
            var ch = reader.ReadChar();
            switch (ch) {
                case '\r':
                    if (reader.PeekChar() == '\n') reader.ReadChar();
                    done = true;
                    break;
                case '\n':
                    done = true;
                    break;
                default:
                    line.Append(ch);
                    break;
            }
        }
        return line.ToString();
    }
}
/*static class CTConstants
{
    //   public static readonly Vector3 REFERENCE_CENTER = new Vector3(-3.244141f, -226.2559f, -248.5f);
    public static readonly Vector3 REFERENCE_CENTER = new Vector3(-3.244141f, -226.2559f, -67.4f);
}*/
public static class BorderColors
{
    public static readonly Vector4 YELLOW = new Vector4(1, 1, 0, 1);
    public static readonly Vector4 RED = new Vector4(1, 0, 0, 1);
    public static readonly Vector4 CYAN = new Vector4(0, 1, 1, 1);
}

static class RtexConstants
{
    public static readonly float SCALE = 0.001961119675f;
}