using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditorText;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;
namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 场景回调事件
    /// </summary>
    public class PathEvent : MonoBehaviour
    {
        private void FightExit()
        {
            SetHeroPathNormalMove();
            // MonsterManager.inst.Monsters.Clear();
        }
        //=====================================================================英雄阵型回调===================================================
        // 战斗触发回调
        private void FightEnterPos(int FightCount)
        {
            HeroPathFightEnter(FightCount);
        }
        //战斗就位点回调
        private void FightEnterEndPos(int FightCount)
        {
            HeroPathFightEnterEnd(FightCount);
        }
        //整队准备回调
        private void LineUpReadyPos(int FightCount)
        {
            HeroPathLineUpReady(FightCount);
        }
        // 剧情进入触发回调
        private void StoryEnterPos(int i)
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnter, i);
        }
        //剧情结束触发回调
        private void StoryEndPos(int i)
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnd, i);
        }
        // 改变英雄阵型移动速度
        private void ChangeHeroSpeed(float speed)
        {
            FightEditorContrler.GetInstantiate().HeroPathSetSpeed(speed);
        }
        // 英雄阵型进入Idle
        private void SetHeroPathIdle()
        {
            FightEditorContrler.GetInstantiate().HeroPathIdle();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_HeroPathIdle);
        }
        // 英雄阵型进入正常移动
        private void SetHeroPathNormalMove()
        {
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_HeroPathNormalMove);
        }
        //英雄阵型进入瞬间移动
        private void SetHeroPathMomentMove(int i)
        {
            if (FightControler.Inst!=null)
                FightControler.Inst.CallBack_eroPathMomentMoveEnter();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_HeroPathMomentMoveEnter);
            FightEditorContrler.GetInstantiate().HeroPathMomentMoveEnter(i);  
        }
        //英雄阵型瞬间移动中
        private void SetHeroPathMomentMoveIng()
        {
            FightEditorContrler.GetInstantiate().HeroPathMomentMoveIng();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_HeroPathMomentMoveIng);
        }
        //英雄阵型退出瞬间移动
        private void HeroPathMomentMoveExit()
        {
            FightEditorContrler.GetInstantiate().HeroPathMomentMoveExit();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_HeroPathMomentMoveExit);
        }
        //英雄阵型进入战斗状态
        private void HeroPathFightEnter(int i)
        {
            FightEditorContrler.GetInstantiate().HeroPathFightEnter(i);
            object temp = (object)i;
			GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_PrepareEnemy, temp);
        }
        // 英雄阵型战斗进入状态结束
        private void HeroPathFightEnterEnd(int i)
        {
            FightEditorContrler.GetInstantiate().HeroPathFightEnterEnd();
			GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_EnterFightState, i);
        }
        private void HeroPathLineUpReady(int i)
        {
            FightEditorContrler.GetInstantiate().HeroPathLineUpReady();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_LineUpReady, i);
        }

        /// <summary>
        /// 停止移动;
        /// </summary>
        /// <param name="data"></param>
        private void HeroOnObject(string data)
        {
            FightEditorContrler.GetInstantiate().HeroOnObject(data);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_PrepareBoard);
        }

        private void HeroOffObject()
        {
            FightEditorContrler.GetInstantiate().HeroOffObject();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_BoardingOver);
        }

        //===================================================================摄像机回调==========================================================
        //摄像机事件回调
        private void CamTriggerEvent(string ID)
        {
            FightEditorContrler.GetInstantiate().SetCamInfo(ID);
        }
        //摄像机播放
        private void CamPlay()
        {
            FightEditorContrler.GetInstantiate().CamPlay();
        }
        //摄像机暂停
        private void CamPause()
        {
            FightEditorContrler.GetInstantiate().CamPause();
        }
    }
}
