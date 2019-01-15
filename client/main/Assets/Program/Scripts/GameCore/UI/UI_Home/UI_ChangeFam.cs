using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.Utils;
namespace DreamFaction.UI
{
    public class UI_ChangeFam : BaseUI
    {
        private static UI_ChangeFam inst;

        private GameObject m_LifeSelectObj = null;       //
        private GameObject m_RightSelectObj = null;
        private Button m_CloseBtn = null;
        private Text m_PromptTxt = null;
        private Outline m_leftSelectText;
        private Outline m_RightSelecText;
        public override void InitUIData()
        {
            inst = this;
            m_LifeSelectObj = selfTransform.FindChild("leftSelectImg").gameObject;
            m_RightSelectObj = selfTransform.FindChild("RightSelectImg").gameObject;
            m_CloseBtn = selfTransform.FindChild("CeloseBtn").GetComponent<Button>();
            m_PromptTxt = selfTransform.FindChild("HintObj/Bottom/Text").GetComponent<Text>();
            m_PromptTxt.text = GameUtils.getString("embattle_window2");
            m_leftSelectText = selfTransform.FindChild("leftSelectText").GetComponent<Outline>();
            m_RightSelecText = selfTransform.FindChild("RightSelecText").GetComponent<Outline>();

            m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClose));
        }
        public static UI_ChangeFam GetInst()
        {
            return inst;
        }
        //设置阵型为突击阵型
        public void SetAttackFormation()
        {
            m_LifeSelectObj.SetActive(true);
            m_RightSelectObj.SetActive(false);
            m_leftSelectText.enabled = true;
            m_RightSelecText.enabled = false;
        }
        //设置阵型为攻坚阵型
        public void SetCrucialFormation()
        {
            m_LifeSelectObj.SetActive(false);
            m_RightSelectObj.SetActive(true);
            m_leftSelectText.enabled = false;
            m_RightSelecText.enabled = true;
        }
        //突击队形
        public void OnAttackFormation()
        {
            SetAttackFormation();
            int count = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
//             if(type==2)
//                 UI_Form.GetInst().SendProtocol_ChangeFormation(1, count);
            selfTransform.gameObject.SetActive(false);
            UI_Form.GetInst().SetHeroListLayOutActive(true);
        }
        //攻坚队形
        public void OnCrucialFormation()
        {
            SetCrucialFormation();
            int count = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            if (type == 1)
//                 UI_Form.GetInst().SendProtocol_ChangeFormation(2, count);
//             selfTransform.gameObject.SetActive(false);
            UI_Form.GetInst().SetHeroListLayOutActive(true);
        }
        public void OnClose()
        {
            gameObject.SetActive(false);
            UI_Form.GetInst().SetHeroListLayOutActive(true);
        }
    }
}

