using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_ExploreBase : BaseUI
{
	protected Text m_NameTxt;
	protected Text m_TitleTxt;
	protected Text m_ChapterTxt;
	protected Text m_NameTxt_1;
	protected Text m_DetailTxt;
	protected Text m_NameTxt_2;
	protected Text m_DetailTxt_1;
	protected Text m_NameTxt_3;
	protected Text m_DetailTxt_2;
	protected Text m_NameTxt_4;
	protected Text m_DetailTxt_3;
	protected Text m_Text;
	protected Text m_Text_1;
	protected Button m_Add;
	protected Text m_Text_2;
	protected Button m_Add_1;
	protected Text m_Time;
	protected Button m_AddBtn;
	protected Text m_CurrentpowTxt;
	protected Text m_MaxpowTxt;
	protected Text m_Time_1;
	protected Button m_AddBtn_1;
	protected Text m_CurrentpowTxt_1;
	protected Text m_MaxpowTxt_1;
	protected Button m_Btn_back;
	protected Text m_Text_3;
	protected Text m_TitleTxt_1;
	protected Button m_ExitBtn;
	protected Text m_TitleTxt_2;
	protected Text m_RewardTxt;
	protected Text m_DetailTxt_4;
	protected Text m_TitleTxt_3;
	protected Text m_ConditionTxt;
	protected Text m_LvTxt;
	protected Text m_NameTxt_5;
	protected Text m_LvTxt_1;
	protected Text m_NameTxt_6;
	protected Text m_LvTxt_2;
	protected Text m_NameTxt_7;
	protected Text m_LvTxt_3;
	protected Text m_NameTxt_8;
	protected Text m_LvTxt_4;
	protected Text m_NameTxt_9;
	protected Button m_GetBtn;
	protected Text m_Text_4;
	protected Button m_AutoMatchBtn;
	protected Text m_Text_5;
	protected Button m_StartBtn;
	protected Text m_Text_6;
	protected Text m_Text_7;
	protected Text m_TitleTxt_4;
	protected Text m_TitleTxt_5;
	protected Text m_RewardTxt_1;
	protected Text m_TimeTitleTxt;
	protected Text m_TimeTxt;
	protected Button m_GetBtn_1;
	protected Text m_Text_8;
	protected Button m_TimeUpBtn;
	protected Text m_Text_9;
	protected Button m_DoneBtn;
	protected Text m_Text_10;
	protected Text m_Text_11;
	protected Button m_RefreshBtn;
	protected Text m_Text_12;
	protected Text m_Text_13;
	protected Text m_LvTxt_5;
	protected Text m_NameTxt_10;
	protected Button m_SelectBtn;
	protected Text m_Text_14;
	protected Button m_CancleBtn;
	protected Text m_Text_15;
	protected Button m_RefreshBtn_1;
	protected Text m_Text_16;
	protected Text m_Text_17;
	protected Text m_CurPlayers_txt;
	protected Text m_MaxPlayers_txt;
	protected Text m_BagCount_txt;
	protected Button m_add;
	protected Text m_Text_18;
	protected Button m_sort_quality;
	protected Text m_Text_19;
	protected Button m_sort_lv;
	protected Text m_Text_20;
	protected Button m_sort;
	protected Text m_Text_21;
	protected Text m_Text_22;
	protected Button m_LeftBtn;
	protected Text m_Text_23;
	protected Button m_RightBtn;
	protected Text m_Text_24;

	public override void InitUIData()
	{
		base.InitUIData();
		m_NameTxt = selfTransform.FindChild("ScenePanel/SceneObj/Items/Item/NameTxt").GetComponent<Text>();
		m_TitleTxt = selfTransform.FindChild("ScenePanel/TopPanel/TitleTxt").GetComponent<Text>();
		m_ChapterTxt = selfTransform.FindChild("ScenePanel/TopPanel/ChapterTxt").GetComponent<Text>();
		m_NameTxt_1 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team0/NameTxt").GetComponent<Text>();
		m_DetailTxt = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team0/DetailTxt").GetComponent<Text>();
		m_NameTxt_2 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team1/NameTxt").GetComponent<Text>();
		m_DetailTxt_1 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team1/DetailTxt").GetComponent<Text>();
		m_NameTxt_3 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team2/NameTxt").GetComponent<Text>();
		m_DetailTxt_2 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team2/DetailTxt").GetComponent<Text>();
		m_NameTxt_4 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team3/NameTxt").GetComponent<Text>();
		m_DetailTxt_3 = selfTransform.FindChild("ScenePanel/TeamPanel/TeamList/Team3/DetailTxt").GetComponent<Text>();
		m_Text = selfTransform.FindChild("TopPanel/TitleButton_0/Text").GetComponent<Text>();
		m_Text_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Text").GetComponent<Text>();
		m_Add = selfTransform.FindChild("TopPanel/MoneyBarUI/Diamond/Add").GetComponent<Button>();
		m_Add.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAdd));
		m_Text_2 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Text").GetComponent<Text>();
		m_Add_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/Money/Add").GetComponent<Button>();
		m_Add_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAdd_1));
		m_Time = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/Time").GetComponent<Text>();
		m_AddBtn = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/AddBtn").GetComponent<Button>();
		m_AddBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddBtn));
		m_CurrentpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/CurrentpowTxt").GetComponent<Text>();
		m_MaxpowTxt = selfTransform.FindChild("TopPanel/MoneyBarUI/Powers/MaxpowTxt").GetComponent<Text>();
		m_Time_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/ExplorePoint/Time").GetComponent<Text>();
		m_AddBtn_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/ExplorePoint/AddBtn").GetComponent<Button>();
		m_AddBtn_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddBtn_1));
		m_CurrentpowTxt_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/ExplorePoint/CurrentpowTxt").GetComponent<Text>();
		m_MaxpowTxt_1 = selfTransform.FindChild("TopPanel/MoneyBarUI/ExplorePoint/MaxpowTxt").GetComponent<Text>();
		m_Btn_back = selfTransform.FindChild("TopPanel/Btn_back").GetComponent<Button>();
		m_Btn_back.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBtn_back));
		m_Text_3 = selfTransform.FindChild("TopPanel/Btn_back/Text").GetComponent<Text>();
		m_TitleTxt_1 = selfTransform.FindChild("DetailPanel/TopObj/TitleTxt").GetComponent<Text>();
		m_ExitBtn = selfTransform.FindChild("DetailPanel/TopObj/ExitBtn").GetComponent<Button>();
		m_ExitBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickExitBtn));
		m_TitleTxt_2 = selfTransform.FindChild("DetailPanel/RewardObj/TitleTxt").GetComponent<Text>();
		m_RewardTxt = selfTransform.FindChild("DetailPanel/RewardObj/RewardTxt").GetComponent<Text>();
		m_DetailTxt_4 = selfTransform.FindChild("DetailPanel/RewardObj/DetailTxt").GetComponent<Text>();
		m_TitleTxt_3 = selfTransform.FindChild("DetailPanel/ConditionObj/TitleTxt").GetComponent<Text>();
		m_ConditionTxt = selfTransform.FindChild("DetailPanel/ConditionObj/ConditionTxt").GetComponent<Text>();
		m_LvTxt = selfTransform.FindChild("DetailPanel/HeroListObj/Item1/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_5 = selfTransform.FindChild("DetailPanel/HeroListObj/Item1/NameTxt").GetComponent<Text>();
		m_LvTxt_1 = selfTransform.FindChild("DetailPanel/HeroListObj/Item2/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_6 = selfTransform.FindChild("DetailPanel/HeroListObj/Item2/NameTxt").GetComponent<Text>();
		m_LvTxt_2 = selfTransform.FindChild("DetailPanel/HeroListObj/Item3/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_7 = selfTransform.FindChild("DetailPanel/HeroListObj/Item3/NameTxt").GetComponent<Text>();
		m_LvTxt_3 = selfTransform.FindChild("DetailPanel/HeroListObj/Item4/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_8 = selfTransform.FindChild("DetailPanel/HeroListObj/Item4/NameTxt").GetComponent<Text>();
		m_LvTxt_4 = selfTransform.FindChild("DetailPanel/HeroListObj/Item5/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_9 = selfTransform.FindChild("DetailPanel/HeroListObj/Item5/NameTxt").GetComponent<Text>();
		m_GetBtn = selfTransform.FindChild("DetailPanel/GetBtn").GetComponent<Button>();
		m_GetBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGetBtn));
		m_Text_4 = selfTransform.FindChild("DetailPanel/GetBtn/Text").GetComponent<Text>();
		m_AutoMatchBtn = selfTransform.FindChild("DetailPanel/AutoMatchBtn").GetComponent<Button>();
		m_AutoMatchBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAutoMatchBtn));
		m_Text_5 = selfTransform.FindChild("DetailPanel/AutoMatchBtn/Text").GetComponent<Text>();
		m_StartBtn = selfTransform.FindChild("DetailPanel/StartBtn").GetComponent<Button>();
		m_StartBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickStartBtn));
		m_Text_6 = selfTransform.FindChild("DetailPanel/StartBtn/Text").GetComponent<Text>();
		m_Text_7 = selfTransform.FindChild("DetailPanel/StartBtn/CostObj/Text").GetComponent<Text>();
		m_TitleTxt_4 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/TitleTxt").GetComponent<Text>();
		m_TitleTxt_5 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/RewardObj/TitleTxt").GetComponent<Text>();
		m_RewardTxt_1 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/RewardObj/RewardTxt").GetComponent<Text>();
		m_TimeTitleTxt = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/TimeObj/TimeTitleTxt").GetComponent<Text>();
		m_TimeTxt = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/TimeObj/TimeTxt").GetComponent<Text>();
		m_GetBtn_1 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/GetBtn").GetComponent<Button>();
		m_GetBtn_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGetBtn_1));
		m_Text_8 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/GetBtn/Text").GetComponent<Text>();
		m_TimeUpBtn = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/TimeUpBtn").GetComponent<Button>();
		m_TimeUpBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickTimeUpBtn));
		m_Text_9 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/TimeUpBtn/Text").GetComponent<Text>();
		m_DoneBtn = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/DoneBtn").GetComponent<Button>();
		m_DoneBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickDoneBtn));
		m_Text_10 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/DoneBtn/Text").GetComponent<Text>();
		m_Text_11 = selfTransform.FindChild("TaskPanel/TaskObj/Items/Item/Buttons/DoneBtn/CostObj/Text").GetComponent<Text>();
		m_RefreshBtn = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn").GetComponent<Button>();
		m_RefreshBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRefreshBtn));
		m_Text_12 = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn/Text").GetComponent<Text>();
		m_Text_13 = selfTransform.FindChild("TaskPanel/BottomObj/RefreshBtn/CostObj/Text").GetComponent<Text>();
		m_LvTxt_5 = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/LvObj/LvTxt").GetComponent<Text>();
		m_NameTxt_10 = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/NameTxt").GetComponent<Text>();
		m_SelectBtn = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/SelectBtn").GetComponent<Button>();
		m_SelectBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelectBtn));
		m_Text_14 = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/SelectBtn/Text").GetComponent<Text>();
		m_CancleBtn = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/CancleBtn").GetComponent<Button>();
		m_CancleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCancleBtn));
		m_Text_15 = selfTransform.FindChild("HeroListPanel/HerosObj/Items/Item/CancleBtn/Text").GetComponent<Text>();
		m_RefreshBtn_1 = selfTransform.FindChild("HeroListPanel/BottomObj/RefreshBtn").GetComponent<Button>();
		m_RefreshBtn_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRefreshBtn_1));
		m_Text_16 = selfTransform.FindChild("HeroListPanel/BottomObj/RefreshBtn/Text").GetComponent<Text>();
		m_Text_17 = selfTransform.FindChild("HeroListPanel/BottomObj/RefreshBtn/CostObj/Text").GetComponent<Text>();
		m_CurPlayers_txt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/CountObj/CurPlayers_txt").GetComponent<Text>();
		m_MaxPlayers_txt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/CountObj/MaxPlayers_txt").GetComponent<Text>();
		m_BagCount_txt = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/CountObj/BagCount_txt").GetComponent<Text>();
		m_add = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/add").GetComponent<Button>();
		m_add.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickadd));
		m_Text_18 = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/add/Text").GetComponent<Text>();
		m_sort_quality = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_quality").GetComponent<Button>();
		m_sort_quality.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClicksort_quality));
		m_Text_19 = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_quality/Text").GetComponent<Text>();
		m_sort_lv = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_lv").GetComponent<Button>();
		m_sort_lv.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClicksort_lv));
		m_Text_20 = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort_lv/Text").GetComponent<Text>();
		m_sort = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort").GetComponent<Button>();
		m_sort.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClicksort));
		m_Text_21 = selfTransform.FindChild("HeroListPanel/BottomObj/Bottom/sort/Text").GetComponent<Text>();
		m_Text_22 = selfTransform.FindChild("HeroListPanel/NullObj/Hint/Text").GetComponent<Text>();
		m_LeftBtn = selfTransform.FindChild("HeroListPanel/NullObj/LeftBtn").GetComponent<Button>();
		m_LeftBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
		m_Text_23 = selfTransform.FindChild("HeroListPanel/NullObj/LeftBtn/Text").GetComponent<Text>();
		m_RightBtn = selfTransform.FindChild("HeroListPanel/NullObj/RightBtn").GetComponent<Button>();
		m_RightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));
		m_Text_24 = selfTransform.FindChild("HeroListPanel/NullObj/RightBtn/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickAdd()
	{
	}

	protected virtual void OnClickAdd_1()
	{
	}

	protected virtual void OnClickAddBtn()
	{
	}

	protected virtual void OnClickAddBtn_1()
	{
	}

	protected virtual void OnClickBtn_back()
	{
	}

	protected virtual void OnClickExitBtn()
	{
	}

	protected virtual void OnClickGetBtn()
	{
	}

	protected virtual void OnClickAutoMatchBtn()
	{
	}

	protected virtual void OnClickStartBtn()
	{
	}

	protected virtual void OnClickGetBtn_1()
	{
	}

	protected virtual void OnClickTimeUpBtn()
	{
	}

	protected virtual void OnClickDoneBtn()
	{
	}

	protected virtual void OnClickRefreshBtn()
	{
	}

	protected virtual void OnClickSelectBtn()
	{
	}

	protected virtual void OnClickCancleBtn()
	{
	}

	protected virtual void OnClickRefreshBtn_1()
	{
	}

	protected virtual void OnClickadd()
	{
	}

	protected virtual void OnClicksort_quality()
	{
	}

	protected virtual void OnClicksort_lv()
	{
	}

	protected virtual void OnClicksort()
	{
	}

	protected virtual void OnClickLeftBtn()
	{
	}

	protected virtual void OnClickRightBtn()
	{
	}

}
