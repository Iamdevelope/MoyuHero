// Author : ZCD
// Created : 3/15/2015

using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.SkillCore;

namespace DreamFaction.UI
{
    public class UI_BuffMgr : MonoBehaviour
    {
        public UI_MenuPanel mMenuPanel = null;
        public UI_BloodPanel mBloodPanel = null;
        void Awake()
        {
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_BuffEvent_ShowUI, onBufferUpdateCall);
        }

        void Destroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.F_BuffEvent_ShowUI, onBufferUpdateCall);
        }


        /// <summary>
        /// buff更新
        /// </summary>
        private void onBufferUpdateCall(GameEvent e)
        {
            BuffUpdatePackage buffEvent = (BuffUpdatePackage)e.data;
            ObjectCreature creature = buffEvent.creature; 
            Impact pImpact = buffEvent.pImpact;
            bool isAdd = buffEvent.isAdd;
            //pImpact.GetImpactRow().m_conduce == 0
            if (!string.IsNullOrEmpty(pImpact.GetImpactRow().getBuffIconBig()))
            {
                // 光环
                // 更新到怒气条下
                EM_OBJECT_TYPE groupType = creature.GetGroupType();
                switch (groupType)
                {
                    case EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO:
                        {
                            if (isAdd)
                            {
                                mMenuPanel.onSelfAllBuffAdd(pImpact.GetImpactRow());
                            }
                            else
                            {
                                mMenuPanel.onSelfAllBuffRemove(pImpact.GetImpactRow());
                            }
                        }
                        break;
                    case EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER:
                        {
                            if (isAdd)
                            {
                                mMenuPanel.onEnemyAllBuffAdd(pImpact.GetImpactRow());
                            }
                            else
                            {
                                mMenuPanel.onEnemyAllBuffRemove(pImpact.GetImpactRow());
                            }
                        }
                        break;
                }
            }
            
            if(!string.IsNullOrEmpty(pImpact.GetImpactRow().getBuffIconSmall()))
            {
                // buff 单体
                // 更新到角色血条下
                if (isAdd)
                {
                    mBloodPanel.onSingleBuffAdd(creature.GetGuid(), pImpact.GetImpactRow());
                }
                else
                {
                    mBloodPanel.onSingleBuffRemove(creature.GetGuid(), pImpact.GetImpactRow());
                }
            }
        }
    }
}
