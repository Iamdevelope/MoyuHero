using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_LimitRankItemBase : BaseUI
{
	protected Text m_PlayerNameTxt;
	protected Text m_LevelNumStrTxt;
	protected Text m_LevelNumTxt;
	protected Text m_BraveStaTxt;
	protected Text m_BraveTxt;
	protected Text m_LvTxt;
	protected Text m_LevelTxt;
	protected Text m_InListTimeTxt;
	protected Button m_BattleBtn;
	protected Text m_Text;
    protected Text m_VText;

	public override void InitUIData()
	{
		base.InitUIData();
        m_PlayerNameTxt = selfTransform.FindChild("InfoState/PlayerNameTxt").GetComponent<Text>();
        m_LevelNumStrTxt = selfTransform.FindChild("InfoState/LevelNumStrTxt").GetComponent<Text>();
        m_LevelNumTxt = selfTransform.FindChild("InfoState/LevelNumTxt").GetComponent<Text>();
        m_BraveStaTxt = selfTransform.FindChild("InfoState/BraveStaTxt").GetComponent<Text>();
        m_BraveTxt = selfTransform.FindChild("InfoState/BraveTxt").GetComponent<Text>();
        m_LvTxt = selfTransform.FindChild("InfoState/LvTxt").GetComponent<Text>();
        m_LevelTxt = selfTransform.FindChild("InfoState/LevelTxt").GetComponent<Text>();
        m_InListTimeTxt = selfTransform.FindChild("InfoState/InListTimeTxt").GetComponent<Text>();
        m_BattleBtn = selfTransform.FindChild("InfoState/BattleBtn").GetComponent<Button>();
		m_BattleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBattleBtn));
        m_Text = selfTransform.FindChild("InfoState/BattleBtn/Text").GetComponent<Text>();
        m_VText = selfTransform.FindChild("NullInfoState/VText").GetComponent<Text>();
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBattleBtn()
	{
	}

}
