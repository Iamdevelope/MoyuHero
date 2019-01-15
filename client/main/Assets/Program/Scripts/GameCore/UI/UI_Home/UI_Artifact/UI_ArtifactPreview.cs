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

public class UI_ArtifactPreview : CustomUI
{
	public static UI_ArtifactPreview inst;
	public static string UI_ResPath = "UI_Artifact/UI_Artifact_Preview_1_1";

	Artifact _artifact;
	public int level;

	private DHGridLayout _artifactLayout;
	private Button _backBtn;  // 返回按钮

	private List<ArtifactTemplate> _artifactList = new List<ArtifactTemplate>();

	public override void InitUIData()
	{
		inst = this;
		_artifactLayout = selfTransform.FindChild("Artifact/ArtifactLayout").GetComponent<DHGridLayout>();
		_backBtn = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
		_backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));

        captionPath = "caption";
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}
	
	// 显示整个预览神器的界面信息
	public void ShowInfo(Artifact artifact)
	{
		_artifact = artifact;

		// 初始化神器数据
		int id = artifact.GetArtifactRow().getId();
		level = id % 10;
		id = id / 10 * 10 + 1;
		for (int i = 0; i < 5; i++)
		{
			ArtifactTemplate temp = (ArtifactTemplate)DataTemplate.GetInstance().m_ArtifactTable.getTableData(id + i);
			_artifactList.Add(temp);
		}

		_artifactLayout.cellCount = _artifactList.Count;
		_artifactLayout.loadCell = LoadArtifactItem;
	}

	// 加载一个预览神器 Item
	GameObject LoadArtifactItem(int index)
	{
		if (index < _artifactList.Count)
		{
			GameObject obj = Instantiate(Resources.Load("UI/Prefabs/UI_Artifact/PreviewArtifactItem")) as GameObject;
			ArtifactPreItem item = obj.AddComponent<ArtifactPreItem>();
			item.index = index;
			if (index + 1 == level)
			{
				item.ShowInfoLevel(_artifact, level);
			}
			else if(index + 1 < level)
			{
				item.ShowInfoReach(_artifactList[index], true);
			}
			else
			{
				item.ShowInfoReach(_artifactList[index]);
			}

			return obj;
		}
		return null;
	}

	// 返回按钮
	private void OnClickBackBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
	}
}
