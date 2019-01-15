using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;

namespace DreamFaction.UI
{
    public class UI_BloodPanel : BaseUI
    {

        [HideInInspector]
        public bool isFull = false;
        public static string UI_ResPath = "UI_Login/UI_LoginWin_2_0";  
        //所有的血条脚本，用来控制血条对象
        private List<UI_Blood> bloodInfo = new List<UI_Blood>();


        void Start()
        {
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_EnemyOnDie, onMonsterDieCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_HeroOnDie, onHeroDieCall);
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_ShowSkillName, OnRecieveSkillRelease);
        }

        void OnDestroy()
        {
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_ShowSkillName, OnRecieveSkillRelease);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_EnemyOnDie, onMonsterDieCall);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_HeroOnDie, onHeroDieCall);
        }

        /// <summary>
        /// 受伤处理
        /// </summary>
        /// <param name="e"></param>
        private void onHeroHurtCall(GameEvent e)
        {
            ObjectHero hero = (ObjectHero)e.data;
            if (hero != null)
            {
                UpdateBloodValue(hero.GetGuid(), (float)hero.GetHP() / hero.GetMaxHP());
            }
        }

        private void onMonsterHurtCall(GameEvent e)
        {
            ObjectMonster monster = (ObjectMonster)e.data;
            if (monster != null)
            {
                UpdateBloodValue(monster.GUID, (float)monster.GetHP() / monster.GetMaxHP());
            }
        }

        // 英雄死亡
        private void onHeroDieCall(GameEvent e)
        {
            HeroData obj = (HeroData)e.data;
            X_GUID uid = obj.GUID;
            UI_Blood fb = FindUIFBlood(uid);
            if (fb)
            {
                bloodInfo.Remove(fb);
                GameObject.Destroy(fb.gameObject);
            }
        }

        // 怪物死亡
        private void onMonsterDieCall(GameEvent e)
        {
            X_GUID uid = (X_GUID)e.data;
            UI_Blood fb = FindUIFBlood(uid);
            if (fb)
            {
                bloodInfo.Remove(fb);
                GameObject.Destroy(fb.gameObject);
            }
        }

        //从所有血条中找到目标血条
        private UI_Blood FindUIFBlood(X_GUID uid)
        {
            int size = bloodInfo.Count;
            for (int i = 0; i < size; ++i)
            {
                if (bloodInfo[i].uid.Equals(uid))
                    return bloodInfo[i];
            }
            return null;
        }

        //创建血条
        public void CreateBloodBar(ObjectHero hero)
        {
            Transform tans = hero.GetAnimation().EventControl.Pre_Head_T_EffectPoint;

            GameObject barObj = Instantiate(UI_FightControler.Inst.heroBloodPre, Vector3.zero, Quaternion.identity) as GameObject;
            barObj.transform.SetParent(transform, false);
            UI_Blood mBlood = barObj.AddComponent<UI_Blood>();
            mBlood.SetPosition(tans.position);
            barObj.transform.localScale = new Vector3(1, 1, 1);
            bloodInfo.Add(mBlood);
            mBlood.isHero = true;
            mBlood.setHeroLevel(hero.GetHeroData().Level);
            if (!ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
                hero.SetHP(hero.GetMaxHP());
            mBlood.SetValue((float)hero.GetHP() / (float)hero.GetMaxHP());
            mBlood.setHeadPosition(tans);
            mBlood.uid.Copy(hero.GetGuid());
        }

        public UI_Blood CreateBloodBar(ObjectMonster monster)
        {
            Transform tans = monster.GetAnimation().EventControl.Pre_Head_T_EffectPoint;

            GameObject barObj = null;
            MonsterTemplate template = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(monster.GetTableID());
            if (template.getMonstertype() == 1)
            {
                barObj = Instantiate(UI_FightControler.Inst.monsterBloodPre) as GameObject;
            }

            else
            {
                barObj = Instantiate(UI_FightControler.Inst.bossBloodBar) as GameObject;
            }
            barObj.transform.SetParent(transform, false);

            UI_Blood mBlood = barObj.AddComponent<UI_Blood>();
            mBlood.SetPosition(tans.position);
            barObj.transform.localScale = new Vector3(1, 1, 1);
            bloodInfo.Add(mBlood);
            mBlood.isHero = false;
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                int CurRound = ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum;
                UltimatetrialmonsterTemplate row = (UltimatetrialmonsterTemplate)DataTemplate.GetInstance().m_UltimatetrialmonsterTable.getTableData(CurRound);
                int nLevel = template.getMonsterlevel() + row.getAdditionalLevel();
                mBlood.setHeroLevel(nLevel);
            } 
            else
            {
                mBlood.setHeroLevel(template.getMonsterlevel());
            }
            mBlood.setHeadPosition(tans);
            mBlood.uid.Copy( monster.GetGuid());

            return mBlood;
        }

        //更新血条数值
        public void UpdateBloodValue(X_GUID uid, float fValue)
        {
            UI_Blood mBlood = FindUIFBlood(uid);
            if (mBlood == null) return;
            mBlood.SetValue(fValue);
        }

        // 隐藏所有血条
        public void HideAllBlood()
        {
            int size = bloodInfo.Count;
            for (int i = 0; i < size; ++i)
            {
                UI_Blood blood = bloodInfo[i];
                if (blood && !blood.isHero)
                {
                    blood.gameObject.SetActive(false);
                }
            }
        }

        // 显示所有血条
        public void ShowAllBlood()
        {
            int size = bloodInfo.Count;
            for (int i = 0; i < size; ++i)
            {
                UI_Blood blood = bloodInfo[i];
                if (blood && !blood.isHero)
                {
                    blood.gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 显示flag
        /// </summary>
        /// <param name="type">0表示对我方释放，1表示敌方</param>
        public void showFlag(bool type, bool isMy, X_GUID mOwner)
        {
            int size = bloodInfo.Count;
            for (int i = 0; i < size; ++i)
            {
                UI_Blood blood = null;
                if (!isMy)
                {
                    if (mOwner.GUID_value == bloodInfo[i].uid.GUID_value)
                    {
                        continue;
                    }
                    else
                    {
                        blood = bloodInfo[i];
                    }
                }
                else
                {
                    blood = bloodInfo[i];
                }
                
                if (blood)
                {
                    blood.RemoveTargetFlag();
                    switch (type)
                    {
                        case true:
                            {
                                if (blood && blood.isHero)
                                {
                                    blood.AddTargetFlag();
                                }
                            }
                            break;
                        case false:
                            {
                                if (blood && !blood.isHero)
                                {
                                    blood.AddTargetFlag();
                                }
                            }
                            break;
                    }
                }
            }
        }

        // 目标选择完毕
        public void hideFlag()
        {
            int size = bloodInfo.Count;
            for (int i = 0; i < size; ++i)
            {
                UI_Blood blood = bloodInfo[i];
                if (blood)
                {
                    blood.RemoveTargetFlag();
                }
            }
        }

        /// <summary>
        /// buffer 图标处理
        /// </summary>
        public void onSingleBuffRemove(X_GUID uid,BuffTemplate info)
        {
            UI_Blood mBlood = FindUIFBlood(uid);
            if (mBlood == null) return;
            mBlood.onSingleBuffRemove(info);
        }

        public void onSingleBuffAdd(X_GUID uid, BuffTemplate info)
        {
            UI_Blood mBlood = FindUIFBlood(uid);
            if (mBlood == null) return;
            mBlood.onSingleBuffAdd(info);
        }

        /// <summary>
        /// 接收到释放技能的消息
        /// </summary>
        /// <param name="e">EventRequestSkillPackage</param>
        private void OnRecieveSkillRelease(GameEvent e)
        {
            SkillShowNamePackage data = (SkillShowNamePackage)e.data;
            UI_Blood mBlood = FindUIFBlood(data.pOwner);
            if (mBlood == null) return;
            mBlood.onShowSkillName(data.strName);
        }

        void Update()
        {

        }
    }
}