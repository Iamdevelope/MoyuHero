using UnityEngine;
using UnityEditor;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    [CustomEditor(typeof(Formation))]
    public class FormationEditor : Editor
    {
        Formation formation;
        private void OnEnable()
        {
            formation = (Formation)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("阵型类型");
            formation.formationtype = (HeroFormationType)EditorGUILayout.EnumPopup(formation.formationtype);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("创建阵型子节点"))
            {
                //GameObject point = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //point.transform.parent = formation.transform;
                //point.name = "FormationPoint";
                //point.transform.localPosition = Vector3.zero;
                //point.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                GameObject FormationPoint = Instantiate(Resources.Load("FormationPoint")) as GameObject;
                FormationPoint.transform.parent = formation.transform;
                FormationPoint.transform.localPosition = Vector3.zero;
                FormationPoint.name = "Point";
            }
            if (GUILayout.Button("Done"))
            {
                formation.transform.name = formation.formationtype.ToString();
                PrefabUtility.CreatePrefab("Assets/Resources/" + formation.transform.name + ".prefab", formation.gameObject);
            }
        }
    }
}

