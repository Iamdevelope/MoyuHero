using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.UI.Core;
using DreamFaction.GameAudio;

namespace DreamFaction.UI
{
    class SkillTargetStruct
    {
        public X_GUID mSelctRoleUID = new X_GUID();    // 正在放单体技能的人
        public int mSelectSkillID = 0;     // 正在准备的单体技能
        public bool isForSelf;             // 是否对己方有效
        public bool isMyEff;               // 是否对自己有效


        public void resetByEvent(EventRequestSkillPackage package)
        {
            this.mSelctRoleUID.Copy(package.mOwner);
            this.mSelectSkillID = package.mSkillID;
            this.isForSelf = package.isForSelf;
            this.isMyEff = package.isMyEff;
        }

        public void clear()
        {
            mSelctRoleUID.CleanUp();
            mSelectSkillID = 0;
        }
    }
    public class UI_SkillPanel : BaseUI
    {
        private Object iconPre = null;
        private Dictionary<X_GUID, UI_SkillIcon> mIcons = new Dictionary<X_GUID, UI_SkillIcon>();
        //private List<int> mCurSelectSkillInfo = new List<int>(); // 当前选中的技能的信息
        private SkillTargetStruct mSkillTargetStruct = new SkillTargetStruct();   // 当前选中的技能的信息
        // ======================= 初始化操作 =========================
        // 1: 初始化数据
        public override void InitUIData()
        {
            iconPre = UIResourceMgr.LoadPrefab("UI/Prefabs/SkillIcon");

            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_ResetSkillCD, onSkillReleaseEnd);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_HeroOnDie, onHeroDie);
        }
        
        // 3: 删除数据
        void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_ResetSkillCD, onSkillReleaseEnd);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_HeroOnDie, onHeroDie);
        }

        public void CreateSkillIcon(ObjectHero hero,GameObject icon)
        {
            //GameObject icon = Instantiate(iconPre, Vector3.zero, Quaternion.identity) as GameObject;
            //icon.transform.SetParent(selfTransform,false);
            //icon.transform.localScale = new Vector3(1, 1, 1);

            UI_SkillIcon skillIcon = icon.AddComponent<UI_SkillIcon>();
            skillIcon.setHero(hero);
            skillIcon.InitIcon();
            mIcons.Add(hero.GetGuid(), skillIcon);
        }


        private void onSkillReleaseEnd(GameEvent e)
        {
            X_GUID heroid = (X_GUID)e.data;
            UI_SkillIcon icon = mIcons[heroid];
            if (icon)
            {
                icon.onResetCD();
            }
        }

        // 点击单体技能
        public bool onSingleSkillCall(ref EventRequestSkillPackage data)
        {
            //if (list != null && list.Count == 3)
            //{
            //    mCurSelectSkillInfo.Clear();
            //    mCurSelectSkillInfo.Add(list[0]);
            //    mCurSelectSkillInfo.Add(list[1]);
            //    mCurSelectSkillInfo.Add(list[2]);

            //    return true;
            //}
            //if (data!=null && data.mOwner.Equals(mSelctRoleUID) && data.mSkillID == mSelectSkillID)
            //{
            //    return true;
            //}
            if (data != null)
            {
                mSkillTargetStruct.resetByEvent(data);
                return true;
            }
            return false;
        }

        public bool onSingleTargetFind(ObjectCreature obj)
        {
            EM_OBJECT_TYPE type = obj.GetGroupType();

            if ((mSkillTargetStruct.isForSelf && type == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
                || (!mSkillTargetStruct.isForSelf && type != EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO))
            {
                //不能选择自己 &&  选择的目标是自己
                if (!mSkillTargetStruct.isMyEff && obj.GetGuid().GUID_value == mSkillTargetStruct.mSelctRoleUID.GUID_value)
                    return false;

                onRequestReleaseSkill(obj);
                return true;
            }

            return false;
        }

        public void onRequestReleaseSkill(ObjectCreature obj)
        {
            // 请求释放技能
            EventRequestSkillPackage package = new EventRequestSkillPackage(mSkillTargetStruct.mSelctRoleUID, mSkillTargetStruct.mSelectSkillID, obj);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_RequestReleaseSkill, package);
            mSkillTargetStruct.clear();
        }

        // 验证技能是否等待锁定目标
        public bool onCheckSkillWaitLock(X_GUID uid)
        {
            if (mSkillTargetStruct.mSelctRoleUID.Equals(uid))
            {
                // 解除
                mSkillTargetStruct.clear();
                return true;
            }
            return false;
        }

        // 清理技能数据
        public void onReleaseSkillStruce()
        {
            mSkillTargetStruct.clear();
        }

        // 英雄死亡处理
        public void onHeroDie(GameEvent e)
        {
            //Debug.Log("Recieve HeroDie Call ...");
            HeroData obj = (HeroData)e.data;
            //英雄死亡播放死亡音效   调用31artresource中diesound字段
            //Debug.Log(obj.TableID);
            HeroTemplate _HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(obj.TableID);
            ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_HeroData.getArtresources());
            AudioControler.Inst.PlaySound(_Artresourcedata.getDiesound());
            if (obj != null)
            {
                UI_SkillIcon icon = mIcons[obj.GUID];
                if (icon)
                {
                    icon.OnHeroDead(obj.GUID);

                    UI_FightControler.Inst.isWaitLock(obj.GUID);
                }
            }
        }
    }
}