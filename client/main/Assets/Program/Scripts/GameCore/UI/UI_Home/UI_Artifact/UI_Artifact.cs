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

public class UI_Artifact : CustomUI
{
	public static UI_Artifact inst;
	public static string UI_ResPath = "UI_Home/UI_Artifact_2_0";

	public int curSoul; // 现在在铸魂的界面

	private LoopLayout _artifactLayout;
	private Button _backBtn;  // 返回按钮
	private Button _artifactAdd; // 加成按钮


	private List<Artifact> _artifactList = new List<Artifact>();

	public override void InitUIData()
	{
		inst = this;
		_artifactLayout = selfTransform.FindChild("Artifact/ArtifactLayout").GetComponent<LoopLayout>();
		_backBtn = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
		_backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		_artifactAdd = selfTransform.FindChild("ArtifactAdd").GetComponent<Button>();
		_artifactAdd.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickArtifactAdd));
        uiMark = DreamFaction.UI.Core.UIMark.Artifact;
        HomeControler.Inst.PushFunly(8, 32);
        captionPath = "caption";
	}

	public override void InitUIView()
	{
		base.InitUIView();
		// 初始化神器数据
		Dictionary<int, Artifact> _map = ObjectSelf.GetInstance().ArtifactContainerBag.GetArtifactMap();
		for (int i = 0; i <= _map.Count; i++)
		{
			if (_map.ContainsKey(i))
			{
				_artifactList.Add(_map[i]);
			}
		}

		_artifactLayout.cellCount = _artifactList.Count;
		_artifactLayout.updateCellEvent = LoadArtifactItem;
        _artifactLayout.Reload();
	}

	// 加载一个神器 Item
	void LoadArtifactItem(int index, RectTransform cell)
	{
		if (index < _artifactList.Count)
		{
            ArtifactItem item = cell.gameObject.GetComponent<ArtifactItem>();
            if(item == null)
            {
                item = cell.gameObject.AddComponent<ArtifactItem>();
            }

			item.index = index;
            item.artiIndex = index;
            item.ShowInfo(_artifactList[index]);
		}
	}
	
	// 更新所有神器的属性，可以有优化的空间
	public void UpdateArtiItem()
	{
		_artifactList.Clear();

		Dictionary<int, Artifact> _map = ObjectSelf.GetInstance().ArtifactContainerBag.GetArtifactMap();
		for (int i = 0; i <= _map.Count; i++)
		{
			if (_map.ContainsKey(i))
			{
				_artifactList.Add(_map[i]);
			}
		}

        _artifactLayout.UpdateCell();
	}

	// 返回按钮
	private void OnClickBackBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}

	// 神器附加属性
	void OnClickArtifactAdd()
	{
		UI_HomeControler.Inst.AddUI(UI_ArtifactAddInfo.UI_ResPath);
	}
}
