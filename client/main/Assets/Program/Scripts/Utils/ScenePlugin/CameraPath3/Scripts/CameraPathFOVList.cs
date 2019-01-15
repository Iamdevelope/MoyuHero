// Camera Path 3
// Available on the Unity Asset Store
// Copyright (c) 2013 Jasper Stocker http://support.jasperstocker.com/camera-path/
// For support contact email@jasperstocker.com
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using UnityEngine;
#if UNITY_EDITOR

#endif
using System.Xml;
[ExecuteInEditMode]
public class CameraPathFOVList : CameraPathPointList
{
        public enum Interpolation
        {
            None,
            Linear,
            SmoothStep
        }
    
        public Interpolation interpolation = Interpolation.SmoothStep;

    private const float DEFAULT_FOV = 60;

    public bool listEnabled = true;


    private void OnEnable()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    public override void Init(CameraPath _cameraPath)
    {
        if(initialised)
            return;
        base.Init(_cameraPath);
        cameraPath.PathPointAddedEvent += AddFOV;
        pointTypeName = "FOV";
        initialised = true;
    }

    public override void CleanUp()
    {
        base.CleanUp();
        cameraPath.PathPointAddedEvent -= AddFOV;
        initialised = false;
    }

    public new CameraPathFOV this[int index] 
    {
        get { return ((CameraPathFOV)(base[index])); }
    }

    public void AddFOV(CameraPathControlPoint atPoint)
    {
        CameraPathFOV point = gameObject.AddComponent<CameraPathFOV>();//CreateInstance<CameraPathFOV>();
        point.FOV = defaultFOV;
        point.hideFlags = HideFlags.HideInInspector;
        AddPoint(point,atPoint);
        RecalculatePoints();
    }

    public CameraPathFOV AddFOV(CameraPathControlPoint curvePointA, CameraPathControlPoint curvePointB, float curvePercetage, float fov)
    {
        CameraPathFOV fovpoint = gameObject.AddComponent<CameraPathFOV>();//CreateInstance<CameraPathFOV>();
        fovpoint.hideFlags = HideFlags.HideInInspector;
        fovpoint.FOV = fov;
        AddPoint(fovpoint, curvePointA, curvePointB, curvePercetage);
        RecalculatePoints();
        return fovpoint;
    }

    public float GetFOV(float percentage)
    {
        if (realNumberOfPoints < 2)
        {
            if (realNumberOfPoints == 1)
                return (this[0]).FOV;
            return 60;
        }

//        if (percentage >= 1)
//            return ((CameraPathFOV)GetPoint(realNumberOfPoints - 1)).FOV;

        percentage = Mathf.Clamp(percentage, 0.0f, 1.0f);

        switch(interpolation)
        {
            case Interpolation.SmoothStep:
            return SmoothStepInterpolation(percentage);

            case Interpolation.Linear:
            return LinearInterpolation(percentage);

            default:
            return LinearInterpolation(percentage);
        }
    }

    private float LinearInterpolation(float percentage)
    {
        int index = GetLastPointIndex(percentage);
        CameraPathFOV pointP = (CameraPathFOV)GetPoint(index);
        CameraPathFOV pointQ = (CameraPathFOV)GetPoint(index + 1);

//        if (percentage < pointP.percent)
//            return pointP.FOV;
//        if (percentage > pointQ.percent)
//            return pointQ.FOV;

        float startPercentage = pointP.percent;
        float endPercentage = pointQ.percent;

        if (startPercentage > endPercentage)
            endPercentage += 1;

        float curveLength = endPercentage - startPercentage;
        float curvePercentage = percentage - startPercentage;
        float ct = curvePercentage / curveLength;
        return Mathf.Lerp(pointP.FOV, pointQ.FOV, ct);
    }

    private float SmoothStepInterpolation(float percentage)
    {
        int index = GetLastPointIndex(percentage);
        CameraPathFOV pointP = (CameraPathFOV)GetPoint(index);
        CameraPathFOV pointQ = (CameraPathFOV)GetPoint(index + 1);

//        if (percentage < pointP.percent)
//            return pointP.FOV;
//        if (percentage > pointQ.percent)
//            return pointQ.FOV;

        float startPercentage = pointP.percent;
        float endPercentage = pointQ.percent;

        if (startPercentage > endPercentage)
            endPercentage += 1;

        float curveLength = endPercentage - startPercentage;
        float curvePercentage = percentage - startPercentage;
        float ct = curvePercentage / curveLength;

//        Debug.DrawLine(cameraPath.GetPathPosition(percentage, true), pointP.worldPosition, Color.blue);
//        Debug.DrawLine(cameraPath.GetPathPosition(percentage, true), pointQ.worldPosition, Color.red);

        return Mathf.Lerp(pointP.FOV, pointQ.FOV, CPMath.SmoothStep(ct));
    }

    /// <summary>
    /// Attempt to find the camera in use for this scene and apply the field of view as default
    /// </summary>
    private float defaultFOV
    {
        get
        {
            if(Camera.current)
                return Camera.current.fieldOfView;

            Camera[] cams = Camera.allCameras;
            bool sceneHasCamera = cams.Length > 0;
            if(sceneHasCamera)
                return cams[0].fieldOfView;
            return DEFAULT_FOV;
        }
    }
    
#if UNITY_EDITOR
  
#endif
    //public override void FromXML(XmlNodeList nodes)
    //{
    //    Clear();
    //    foreach (XmlNode node in nodes)
    //    {
    //        CameraPathFOV newCameraPathPoint = gameObject.AddComponent<CameraPathFOV>();//CreateInstance<CameraPathFOV>();
    //        newCameraPathPoint.hideFlags = HideFlags.HideInInspector;
    //        CameraPathPoint.PositionModes positionModes = (CameraPathPoint.PositionModes)Enum.Parse(typeof(CameraPathPoint.PositionModes), node["positionModes"].FirstChild.Value);
    //        switch (positionModes)
    //        {
    //            case CameraPathPoint.PositionModes.Free:
    //                CameraPathControlPoint cPointA = cameraPath[int.Parse(node["cpointA"].FirstChild.Value)];
    //                CameraPathControlPoint cPointB = cameraPath[int.Parse(node["cpointB"].FirstChild.Value)];
    //                float curvePercentage = float.Parse(node["curvePercentage"].FirstChild.Value);
    //                AddPoint(newCameraPathPoint, cPointA, cPointB, curvePercentage);
    //                break;

    //            case CameraPathPoint.PositionModes.FixedToPoint:
    //                CameraPathControlPoint point = cameraPath[int.Parse(node["point"].FirstChild.Value)];
    //                AddPoint(newCameraPathPoint, point);
    //                break;
    //        }
    //        newCameraPathPoint.FromXML(node, cameraPath);
    //    }
    //}

    public override void FromXML(XMLNodeList nodes)
    {
        Clear();
        foreach (XMLNode node in nodes)
        {
            CameraPathFOV newCameraPathPoint = gameObject.AddComponent<CameraPathFOV>();//CreateInstance<CameraPathFOV>();
            newCameraPathPoint.hideFlags = HideFlags.HideInInspector;
            CameraPathPoint.PositionModes positionModes = (CameraPathPoint.PositionModes)Enum.Parse(typeof(CameraPathPoint.PositionModes), node.GetValue("positionModes>0>_text"));
            switch (positionModes)
            {
                case CameraPathPoint.PositionModes.Free:
                    CameraPathControlPoint cPointA = cameraPath[int.Parse(node.GetValue("cpointA>0>_text"))];
                    CameraPathControlPoint cPointB = cameraPath[int.Parse(node.GetValue("cpointB>0>_text"))];
                    float curvePercentage = float.Parse(node.GetValue("curvePercentage>0>_text"));
                    AddPoint(newCameraPathPoint, cPointA, cPointB, curvePercentage);
                    break;

                case CameraPathPoint.PositionModes.FixedToPoint:
                    CameraPathControlPoint point = cameraPath[int.Parse(node.GetValue("point>0>_text"))];
                    AddPoint(newCameraPathPoint, point);
                    break;
            }
            newCameraPathPoint.FromXML(node, cameraPath);
        }
    }
}
