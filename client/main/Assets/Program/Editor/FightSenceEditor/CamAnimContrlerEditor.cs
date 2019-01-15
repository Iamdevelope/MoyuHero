using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(CamAnimContrler))]
    public class CamAnimContrlerEditor : Editor
    {
        private CamAnimContrler CamAnim;
        private void OnEnable()
        {
            CamAnim = (CamAnimContrler)target;
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Done"))
            {
                ExportXml();
            }
        }
        //生成Xml
        private void ExportXml()
        {
            string defaultName = "CameraAnimation";
            defaultName.Replace(" ", "_");
            string filepath = EditorUtility.SaveFilePanel("Export Camera Path Animator to XML", "Assets/StreamingAssets/ScenceEditor/", "CameraAnimation", "xml");

            if (filepath != "")
            {
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    sw.Write(CamAnim.GetComponent<CameraPath>().ToXML());//write out contents of data to XML
                }
            }
        }
    }
}

