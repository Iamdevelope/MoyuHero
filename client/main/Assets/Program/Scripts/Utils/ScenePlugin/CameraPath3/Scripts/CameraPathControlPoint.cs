// Camera Path 3
// Available on the Unity Asset Store
// Copyright (c) 2013 Jasper Stocker http://support.jasperstocker.com/camera-path/
// For support contact email@jasperstocker.com
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using UnityEngine;
#if UNITY_EDITOR
using System.Text;

#endif
using System.Xml;
[ExecuteInEditMode]
public class CameraPathControlPoint : MonoBehaviour
{
    public string givenName = "";
    public string customName = "";
    public string fullName = "";//used in debugging mostly, includes Path name

    [SerializeField]
    private Vector3 _position;

    //Bezier Control Points
    [SerializeField]
    private bool _splitControlPoints = false;
    [SerializeField]
    private Vector3 _forwardControlPoint;
    [SerializeField]
    private Vector3 _backwardControlPoint;

    //Internal stored calculations
    [SerializeField]
    private Vector3 _pathDirection = Vector3.forward;

    public int index = 0;
    public float percentage = 0;
    public float normalisedPercentage = 0;
    
    private void OnEnable()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    public Vector3 localPosition
    {
        get
        {
            return transform.rotation * _position;
        }
        set
        {
            Vector3 newValue = value;
            //Debug.Log("localPosition + newValue" + newValue);
            newValue = Quaternion.Inverse(transform.rotation) * newValue;
            _position = newValue;
        }
    }

    public Vector3 worldPosition
    {
        get
        {
            return transform.rotation * _position + transform.position;
        }
        set
        {
            Vector3 newValue = value - transform.position;
            //Debug.Log("worldPosition + newValue" + newValue);
            newValue = Quaternion.Inverse(transform.rotation) * newValue;
            _position = newValue;
        }
    }

    public Vector3 forwardControlPointWorld
    {
        set { forwardControlPoint = value - transform.position;
             //Debug.Log("forwardControlPointWorld + newValue" + forwardControlPoint);
        }
        get { return forwardControlPoint + transform.position; }
    }

    public Vector3 forwardControlPoint
    {
        get
        {
            return transform.rotation * (_forwardControlPoint + _position);
        }
        set
        {
            Vector3 newValue = value;
            newValue = Quaternion.Inverse(transform.rotation) * newValue;
            //Debug.Log("forwardControlPoint + newValue" + newValue);
            newValue += -_position;
            _forwardControlPoint = newValue;
        }
    }

    public Vector3 forwardControlPointLocal
    {
        get
        {
            return transform.rotation * _forwardControlPoint;
        }
        set
        {
            Vector3 newValue = value;
            newValue = Quaternion.Inverse(transform.rotation) * newValue;
            //Debug.Log("forwardControlPointLocal + newValue" + newValue);
            _forwardControlPoint = newValue;
        }
    }

    public Vector3 backwardControlPointWorld
    {
        set { backwardControlPoint = value - transform.position;
             //Debug.Log("backwardControlPointWorld + newValue" + backwardControlPoint);
        }
        get { return backwardControlPoint + transform.position; }
    }

    public Vector3 backwardControlPoint
    {
        get
        {
            Vector3 controlPoint = (_splitControlPoints) ? _backwardControlPoint : -_forwardControlPoint;
            return transform.rotation * (controlPoint + _position);
        }
        set
        {
            Vector3 newValue = value;
            newValue = Quaternion.Inverse(transform.rotation) * newValue;
            newValue += -_position;
            //Debug.Log("backwardControlPoint + newValue" + newValue);
            if (_splitControlPoints)
                _backwardControlPoint = newValue;
            else
                _forwardControlPoint = -newValue;
        }
    }

    public bool splitControlPoints
    {
        get { return _splitControlPoints; }
        set
        {
            if (value != _splitControlPoints)
                _backwardControlPoint = -_forwardControlPoint;
            _splitControlPoints = value;
            //Debug.Log("splitControlPoints + newValue" + _splitControlPoints);
        }
    }

    public Vector3 trackDirection
    {
        get
        {
            return _pathDirection;
        }
        set
        {
            if (value == Vector3.zero)
                return;
            _pathDirection = value.normalized;
        }
    }

    public string displayName
    {
        get
        {
            if (customName != "")
                return customName;
            return givenName;
        }
    }
    
#if UNITY_EDITOR
    public string ToXML()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<customName>"+customName+"</customName>");
        sb.AppendLine("<index>"+index+"</index>");
        sb.AppendLine("<percentage>"+percentage+"</percentage>");
        sb.AppendLine("<normalisedPercentage>"+normalisedPercentage+"</normalisedPercentage>");
        sb.AppendLine("<_positionX>"+_position.x+"</_positionX>");
        sb.AppendLine("<_positionY>"+_position.y+"</_positionY>");
        sb.AppendLine("<_positionZ>"+_position.z+"</_positionZ>");
        sb.AppendLine("<_splitControlPoints>"+_splitControlPoints+"</_splitControlPoints>");
        sb.AppendLine("<_forwardControlPointX>"+_forwardControlPoint.x+"</_forwardControlPointX>");
        sb.AppendLine("<_forwardControlPointY>"+_forwardControlPoint.y+"</_forwardControlPointY>");
        sb.AppendLine("<_forwardControlPointZ>"+_forwardControlPoint.z+"</_forwardControlPointZ>");
        sb.AppendLine("<_backwardControlPointX>"+_backwardControlPoint.x+"</_backwardControlPointX>");
        sb.AppendLine("<_backwardControlPointY>"+_backwardControlPoint.y+"</_backwardControlPointY>");
        sb.AppendLine("<_backwardControlPointZ>"+_backwardControlPoint.z+"</_backwardControlPointZ>");
        return sb.ToString();
    }

    
#endif
    //public void FromXML(XmlNode node)
    //{
    //    if (node["customName"].HasChildNodes)
    //        customName = node["customName"].FirstChild.Value;
    //    index = int.Parse(node["index"].FirstChild.Value);
    //    percentage = float.Parse(node["percentage"].FirstChild.Value);
    //    normalisedPercentage = float.Parse(node["normalisedPercentage"].FirstChild.Value);
    //    _position.x = float.Parse(node["_positionX"].FirstChild.Value);
    //    _position.y = float.Parse(node["_positionY"].FirstChild.Value);
    //    _position.z = float.Parse(node["_positionZ"].FirstChild.Value);
    //    //Debug.Log("FromXML _position.z" + _position.z);
    //    _splitControlPoints = bool.Parse(node["_splitControlPoints"].FirstChild.Value);
    //    _forwardControlPoint.x = float.Parse(node["_forwardControlPointX"].FirstChild.Value);
    //    _forwardControlPoint.y = float.Parse(node["_forwardControlPointY"].FirstChild.Value);
    //    _forwardControlPoint.z = float.Parse(node["_forwardControlPointZ"].FirstChild.Value);
    //    //Debug.Log("FromXML _forwardControlPoint.z" + _forwardControlPoint.z);
    //    _backwardControlPoint.x = float.Parse(node["_backwardControlPointX"].FirstChild.Value);
    //    _backwardControlPoint.y = float.Parse(node["_backwardControlPointY"].FirstChild.Value);
    //    _backwardControlPoint.z = float.Parse(node["_backwardControlPointZ"].FirstChild.Value);
    //    //Debug.Log("FromXML _backwardControlPoint.z" + _backwardControlPoint.z);
    //}

    public void FromXML(XMLNode node)
    {
        //if (node["customName"].HasChildNodes)
        //    customName = node["customName"].FirstChild.Value;
        //index = int.Parse(node["index"].FirstChild.Value);
        //percentage = float.Parse(node["percentage"].FirstChild.Value);
        //normalisedPercentage = float.Parse(node["normalisedPercentage"].FirstChild.Value);
        //_position.x = float.Parse(node["_positionX"].FirstChild.Value);
        //_position.y = float.Parse(node["_positionY"].FirstChild.Value);
        //_position.z = float.Parse(node["_positionZ"].FirstChild.Value);
        //_splitControlPoints = bool.Parse(node["_splitControlPoints"].FirstChild.Value);
        //_forwardControlPoint.x = float.Parse(node["_forwardControlPointX"].FirstChild.Value);
        //_forwardControlPoint.y = float.Parse(node["_forwardControlPointY"].FirstChild.Value);
        //_forwardControlPoint.z = float.Parse(node["_forwardControlPointZ"].FirstChild.Value);
        //_backwardControlPoint.x = float.Parse(node["_backwardControlPointX"].FirstChild.Value);
        //_backwardControlPoint.y = float.Parse(node["_backwardControlPointY"].FirstChild.Value);
        //_backwardControlPoint.z = float.Parse(node["_backwardControlPointZ"].FirstChild.Value);
        if (node.GetValue("customName>0>_text")!= null)
            customName = node.GetValue("customName>0>_text");
        index = int.Parse(node.GetValue("index>0>_text"));
        percentage = float.Parse(node.GetValue("percentage>0>_text"));
        normalisedPercentage = float.Parse(node.GetValue("normalisedPercentage>0>_text"));
        _position.x = float.Parse(node.GetValue("_positionX>0>_text"));
        _position.y = float.Parse(node.GetValue("_positionY>0>_text"));
        _position.z = float.Parse(node.GetValue("_positionZ>0>_text"));
        //Debug.Log("FromXML _position.z" + _position.z);
        _splitControlPoints = bool.Parse(node.GetValue("_splitControlPoints>0>_text"));
        _forwardControlPoint.x = float.Parse(node.GetValue("_forwardControlPointX>0>_text"));
        _forwardControlPoint.y = float.Parse(node.GetValue("_forwardControlPointY>0>_text"));
        _forwardControlPoint.z = float.Parse(node.GetValue("_forwardControlPointZ>0>_text"));
        //Debug.Log("FromXML _forwardControlPoint.z" + _forwardControlPoint.z);
        _backwardControlPoint.x = float.Parse(node.GetValue("_backwardControlPointX>0>_text"));
        _backwardControlPoint.y = float.Parse(node.GetValue("_backwardControlPointY>0>_text"));
        _backwardControlPoint.z = float.Parse(node.GetValue("_backwardControlPointZ>0>_text"));
        //Debug.Log("FromXML _backwardControlPoint.z" + _backwardControlPoint.z);
    }

    public void CopyData(CameraPathControlPoint to)
    {
        to.customName = customName;
        to.index = index;
        to.percentage = percentage;
        to.normalisedPercentage = normalisedPercentage;
        to.worldPosition = worldPosition;
        to.splitControlPoints = _splitControlPoints;
        to.forwardControlPoint = _forwardControlPoint;
        to.backwardControlPoint = _backwardControlPoint;
    }
}
