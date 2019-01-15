using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public class UI_ArtifactAddInfoNull : BaseUI
{
    public static UI_ArtifactAddInfoNull inst;
	public static string UI_ResPath = "UI_Artifact/UI_ArtifactInfo_null_2_1";

	private Button _closeBtn;
	private Text _tipsText;  

	private List<Artifact> _artifactList = new List<Artifact>();

	public override void InitUIData()
	{
		inst = this;

		_closeBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		_closeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		_tipsText = selfTransform.FindChild("TipsText").GetComponent<Text>();
		_tipsText.text = GameUtils.getString("relics_content7");
		_tipsText.gameObject.SetActive(false);
	}

	public override void InitUIView()
	{
		base.InitUIView();

		ShowInfo();
	}

	// 显示神器的所有属性
	public void ShowInfo()
	{
		_tipsText.gameObject.SetActive(true);
	}
	
	void OnClickCloseBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}
}
