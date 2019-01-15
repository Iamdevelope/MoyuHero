using UnityEngine;
using System.Collections;

public class HeroPathDataObj : ScriptableObject
{
    public float HeroPathWaitTime;                  //英雄入场等待时间
    public Vector3 InitPos;                         //阵型，中心点初始化坐标
    public Vector3 LineUpFollowCamPos;              //整队时候摄像机追踪目标相对坐标
    public Vector3 FightFollowCamPos;               //战斗时候摄像机追踪目标相对坐标
    public Vector3 FightDefaultCamPos;              //战斗摄像机默认相对坐标
    public Quaternion InitAngles;                   //阵型初始化角度
    public int HeroEnterType;                       //英雄入场类型
    public float Tension;
    public float MoveDistance;                      //前排英雄死亡对方阵型移动相对距离;
}
