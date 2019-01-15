using UnityEngine;
using System.Collections;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 阵型类型
    /// </summary>
    public enum HeroFormationType
    {
        /// <summary>
        /// 0近0远
        /// </summary>
        Formation100,
        /// <summary>
        /// 0近1远 
        /// </summary>
        Formation101,
        /// <summary>
        /// 0近2远 
        /// </summary>
        Formation102,
        /// <summary>
        /// 0近3远
        /// </summary>
        Formation103,
        /// <summary>
        /// 1近0远
        /// </summary>
        Formation110,
        /// <summary>
        /// 1近1远
        /// </summary>
        Formation111,
        /// <summary>
        /// 1近2远
        /// </summary>
        Formation112,
        /// <summary>
        /// 1近3远
        /// </summary>
        Formation113,
        /// <summary>
        /// 2近0远
        /// </summary>
        Formation120,
        /// <summary>
        /// 2近1远
        /// </summary>
        Formation121,
        /// <summary>
        /// 2近2远
        /// </summary>
        Formation122,
        /// <summary>
        /// 2近3远
        /// </summary>
        Formation123,
        /// <summary>
        /// 3近0远
        /// </summary>
        Formation130,
        /// <summary>
        /// 3近1远
        /// </summary>
        Formation131,
        /// <summary>
        /// 3近2远
        /// </summary>
        Formation132,
        /// <summary>
        /// 3近3远
        /// </summary>
        Formation133
    }
    /// <summary>
    /// 阵型逻辑开关
    /// </summary>
    public enum HeroPathHoldType
    {
        /// <summary>
        /// 开始
        /// </summary>
        Play,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause
    }
    /// <summary>
    /// 英雄阵型移动状态枚举
    /// </summary>
    public enum HeroPathMoveType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// 正常移动
        /// </summary>
        NormalMove,
        /// <summary>
        /// 瞬间移动进入
        /// </summary>
        MomentMoveEnter,
        /// <summary>
        /// 瞬间移动中
        /// </summary>
        MonentMoveIng,
        /// <summary>
        /// 瞬间移动结束
        /// </summary>
        MomentMoveExit,
        /// <summary>
        /// 待机
        /// </summary>
        Idle,
        /// <summary>
        /// 战斗触发进入
        /// </summary>
        FightEnter,
        /// <summary>
        /// 战斗触发结束
        /// </summary>
        FightEnterEnd,
        /// <summary>
        /// 整队准备
        /// </summary>
        LineUpReady
    }

    /// <summary>
    /// 摄像机开关状态枚举
    /// </summary>
    public enum CamHoldType
    {
        /// <summary>
        /// 播放
        /// </summary>
        Play,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause
    }
    /// <summary>
    /// 摄像机场景状态枚举
    /// </summary>
    public enum CamType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// 进场
        /// </summary>
        Enter,
        /// <summary>
        /// 整队
        /// </summary>
        LineUp,
        /// <summary>
        /// 战斗固定位置;
        /// </summary>
        Fight,
        /// <summary>
        /// 移动到静态固定点
        /// </summary>
        StaticMove,
        /// <summary>
        /// 播放动画
        /// </summary>
        Animation,
        /// <summary>
        /// 默认战斗
        /// </summary>
        DefaultFight,
        /// <summary>
        /// 战斗固定视角;
        /// </summary>
        FightFixedAOV,
    }
    /// <summary>
    /// 摄像机看向点枚举
    /// </summary>
    public enum CamTagType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// 看向英雄中心点
        /// </summary>
        HeroCenter,
        /// <summary>
        /// 看向战斗中心点
        /// </summary>
        FightCenter,
        /// <summary>
        /// 动画模式
        /// </summary>
        AnimationCenter,
        /// <summary>
        /// 静态固定点朝向
        /// </summary>
        StaticMoveCenter
    }
    /// <summary>
    /// 摄像机Transform移动方式枚举
    /// </summary>
    public enum CamMoveType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null,
        /// <summary>
        /// 瞬间移动
        /// </summary>
        MomentMove,
        /// <summary>
        /// 按速度移动
        /// </summary>
        NormalMove,
        /// <summary>
        /// 按时间移动
        /// </summary>
        NormalMoveTime
    }
    public enum CamLookType
    {
        /// <summary>
        /// 歡動
        /// </summary>
        Slow,
        /// <summary>
        /// 不歡動
        /// </summary>
        NoSlow,
    }
    /// <summary>
    /// 剧情动画事件类型
    /// </summary>
    public enum StoryAnimEventType
    {
        /// <summary>
        /// 实例化静态物体
        /// </summary>
        CreatStaticObj,
        /// <summary>
        /// 获取动态物体
        /// </summary>
        GetDynamicObj,
        /// <summary>
        /// 播放摄像机动画
        /// </summary>
        PlayCamAnim,
        /// <summary>
        /// 摄像机播放动画播放2(DEMO)
        /// </summary>
        PlayCamAnimWait,
        /// <summary>
        /// 发送英雄战斗就位
        /// </summary>
        SetEnterFightState,
        /// <summary>
        /// 发送实例化英雄
        /// </summary>
        SetEnterFightInstantiateHero,
        
    }
    /// <summary>
    /// 动画播放模式类型
    /// </summary>
    public enum StoryAnimaHoldType
    {
        /// <summary>
        /// 并联
        /// </summary>
        Parallel,
        /// <summary>
        /// 串联
        /// </summary>
        Cascade
    }
    /// <summary>
    /// 怪物入场类型
    /// </summary>
    public enum MonsterEnterType
    {
        Null,
        /// <summary>
        /// 原地待机
        /// </summary>
        StayIdle,
        /// <summary>
        /// 巡逻
        /// </summary>
        RunAround,
        /// <summary>
        /// 保持上个动作的路线
        /// </summary>
        StayRun,
        /// <summary>
        /// 替补出来
        /// </summary>
        Bench,
        /// <summary>
        /// 删除自己(仅限过场动画编辑使用)
        /// </summary>
        DesMySelf
    }
    /// <summary>
    /// 怪物行动动类型
    /// </summary>
    public enum MonsterRunType
    {
        /// <summary>
        /// 循环
        /// </summary>
        Loop,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 返回
        /// </summary>
        Back
    }
    /// <summary>
    /// 怪物动作模式
    /// </summary>
    public enum MonsterActionType
    {
        /// <summary>
        /// 循环
        /// </summary>
        Loop,
        /// <summary>
        /// 一次
        /// </summary>
        Once
    }
    /// <summary>
    /// 怪物是否看向目标
    /// </summary>
    public enum MonsterLookType
    {
        Look,
        Nolook
    }
    public class EditorEnum 
    {
    }
}

