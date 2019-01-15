using UnityEngine;
using System.Collections.Generic;
using DreamFaction.GameSceneEditor;
/// <summary>
/// 摄像机数据序列化
/// </summary>
public class CameraDataObj: ScriptableObject
{
    public float CamEnterTime;//摄像机入场速度
    public int CamAnimationsCount;//摄像机动画数
    public Vector3 CameraPos;//摄像机初始坐标
    public Vector3 CameraAngles;//摄像机初始角度
    public Vector3 CamEnterPos;//摄像机初始滑翔目标
    public Vector3 CamCenter;//摄像机初始朝向点坐标
    public List<Caminfo> CamPointdataList;//摄像机触发点信息组
}

