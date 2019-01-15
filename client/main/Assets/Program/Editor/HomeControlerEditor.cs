using UnityEngine;
using System.Collections;
using UnityEditor;
using DreamFaction.UI.Core;
using GNET;

[CustomEditor(typeof(UI_HomeControler))]
public class HomeControlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("跳过所有新手引导", GUILayout.Height(30)))
        {
            CSendCommand command = new CSendCommand();
            command.cmd = "gmpassnew";
            IOControler.GetInstance().SendProtocol(command);

            for (int i = 100101; i < 300604; ++i)
                GuideManager.GetInstance().guideIDList.Add(i);

            try
            {
                UI_HomeControler.Inst.ReMoveUI(UI_Intitle.UI_ResPath);
                UI_HomeControler.Inst.ReMoveUI(UI_Guide.UI_ResPath);
            }
            catch
            {

            }
        }
    }
}
