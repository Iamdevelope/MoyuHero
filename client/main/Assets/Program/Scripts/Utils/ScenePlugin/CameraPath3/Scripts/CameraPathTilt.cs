// Camera Path 3
// Available on the Unity Asset Store
// Copyright (c) 2013 Jasper Stocker http://support.jasperstocker.com/camera-path/
// For support contact email@jasperstocker.com
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

#if UNITY_EDITOR
using System.Text;

#endif
using System.Xml;
public class CameraPathTilt : CameraPathPoint
{
    public float tilt = 60;
    
#if UNITY_EDITOR
    public override string ToXML()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(base.ToXML());
        sb.AppendLine("<tilt>" + tilt + "</tilt>");
        return sb.ToString();
    }

   
#endif
    //public override void FromXML(XmlNode node, CameraPath cameraPath)
    //{
    //    base.FromXML(node, cameraPath);
    //    tilt = float.Parse(node["tilt"].FirstChild.Value);
    //}

    public override void FromXML(XMLNode node, CameraPath cameraPath)
    {
        base.FromXML(node, cameraPath);
        tilt = float.Parse(node.GetValue("tilt>0>_text"));
    }
}
