using UnityEngine;
using System.Collections;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork.Data;
using DreamFaction.UI;
using DreamFaction.UI.Core;

namespace DreamFaction.UI
{
    public enum TouchState
    {
        TouchStill_state,                   // 静止状态 [3/27/2015 Zmy]
        SelectSkillTarget_state,            // 选择技能目标状态[3/27/2015 Zmy]
        FireSign_state,                     // 集火选择状态 [3/27/2015 Zmy]
    }

    public class UI_TouchControler
    {
        private TouchState m_CurTouchState;

        public void Update()
        {
            if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().GetLastID() < 100310)
                return;

            switch (m_CurTouchState)
            {
                case TouchState.TouchStill_state:
                    SelectFireSigh();
                    break;
                case TouchState.SelectSkillTarget_state:
                    ClickTarrget();
                    break;
                case TouchState.FireSign_state:
                    SelectFireSigh();
                    break;
                default:
                    break;
            }
        }

        public void ChangeTouchState(TouchState state)
        {
            m_CurTouchState = state;
        }

        /// <summary>
        /// 点击目标
        /// </summary>
        public void ClickTarrget()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj)
                    {
                        ObjectCreature objC = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(obj);
                        if (objC != null)
                        {
                            if (UI_FightControler.Inst != null)
                                UI_FightControler.Inst.onSingleTargetFind(objC);
                            else
                                UI_SkillShow.Inst.onSingleTargetFind(objC);

                            if (GuideManager.GetInstance().isGuideUser  && GuideManager.GetInstance().GetBackCount(100310))
                            {
                                // 点击【敌方一个目标】 100310
                                GuideManager.GetInstance().StopGuide();
                                GameTimeControler.Inst.SetState(UI_MenuPanel.Inst.mInitSpeed);

//                                 if (UI_MenuPanel.Inst != null)
//                                 {
//                                     UI_MenuPanel.Inst.SetMaskObjActive(true);
//                                     UI_MenuPanel.Inst.SetStageNameActive(true);
//                                 }
                            }
                        }
                    }
                }
            }
        }

        private void SelectFireSigh()
        {
            if (m_CurTouchState == TouchState.SelectSkillTarget_state)
                return;

            if (Input.GetMouseButtonDown(0))
            {

                // TODO... GUIDE
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj)
                    {
                        ObjectCreature objC = SceneObjectManager.GetInstance().GetSceneObjectByGameObject(obj);
                        if (objC != null && objC.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER && objC.IsAlive())
                        {
                            SceneObjectManager.GetInstance().UpdateFireSignCreature(objC);
                        }
                    }
                }
            }
        }


    }
}
