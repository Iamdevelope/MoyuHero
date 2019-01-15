using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;

public class UI_HeroLocatUIMgr : BaseUI
{
    public static string UI_ResPath = "UI_Common/UI_HeroLocateUI_1_2";

    private Button m_CloseBtn = null;
    private Text m_AttackTypeTxt = null;
    private Text m_AttackTypeDesTxt = null;
    private Text m_DefTypeTxt = null;
    private Text m_DefTypeDesTxt = null;
    private Text m_SkillTypeTxt = null;
    private Text m_SkillTypeDesTxt = null;
    private Text m_AuxTypeTxt = null;
    private Text m_AuxTypeDesTxt = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_AttackTypeTxt = selfTransform.FindChild("Panel/Words/Text_AttackTypeTxt").GetComponent<Text>();
        m_AttackTypeDesTxt = selfTransform.FindChild("Panel/Words/Text_AttackTypeDesTxt").GetComponent<Text>();
        m_DefTypeTxt = selfTransform.FindChild("Panel/Words/Text_DefTypeTxt").GetComponent<Text>();
        m_DefTypeDesTxt = selfTransform.FindChild("Panel/Words/Text_DefTypeDesTxt").GetComponent<Text>();
        m_SkillTypeDesTxt = selfTransform.FindChild("Panel/Words/Text_SkillTypeDesTxt").GetComponent<Text>();
        m_AuxTypeTxt = selfTransform.FindChild("Panel/Words/Text_AuxTypeTxt").GetComponent<Text>();
        m_AuxTypeDesTxt = selfTransform.FindChild("Panel/Words/Text_AuxTypeDesTxt").GetComponent<Text>();

        m_CloseBtn = selfTransform.FindChild("Panel/Btn_Close").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onClose));
    }

    private void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}
