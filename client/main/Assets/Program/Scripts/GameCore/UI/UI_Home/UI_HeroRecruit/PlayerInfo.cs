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
using DreamFaction.GameEventSystem;
using System.Text;

public class PlayerInfo : BaseUI
{

	private Button m_PlayerInfo;            //玩家信息按钮
	private Text RoleName;                //名字
	private Slider Exbar;                 //经验
	private Transform vipPos;                  //Vip等级
	private Button _VipBtn;                //VIp按钮
	private Text CurrentLevel;            //当前等级
	private Button monthCardBtn;
	public Sprite[] monthCardImgs;

    private IFunctionTipsController m_TipsController;

	public override void InitUIData()
	{
		base.InitUIData();
		m_PlayerInfo = selfTransform.FindChild("RoleName").GetComponent<Button>();
		RoleName = selfTransform.FindChild("RoleName").GetComponent<Text>();
		Exbar = selfTransform.FindChild("Exbar").GetComponent<Slider>();
		m_PlayerInfo.onClick.AddListener(new UnityEngine.Events.UnityAction((OnclickPlayerInfoBtn)));
        _VipBtn = selfTransform.FindChild("VIPIcon").GetComponent<Button>();
        vipPos = selfTransform.FindChild("VIPIcon/VIPLevel");
		_VipBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnclickVipBtn));
		CurrentLevel = selfTransform.FindChild("LevelTxt").GetComponent<Text>();
		monthCardBtn = selfTransform.FindChild("MonthCardBtn").GetComponent<Button>();

		monthCardBtn.onClick.AddListener(OnMonthCardBtnClick);
		GameEventDispatcher.Inst.addEventListener(GameEventID.G_HumanLevel_Update, UpdateCurrentLevel);
		GameEventDispatcher.Inst.addEventListener(GameEventID.G_HumanExp_Update, UpdateExbar);
		GameEventDispatcher.Inst.addEventListener(GameEventID.Net_MCHumanDetailAttribute_Name, UpdateRoleName);
		GameEventDispatcher.Inst.addEventListener(GameEventID.G_VipLevel_Update, UpdateVipLevel);
		GameEventDispatcher.Inst.addEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardInfoChange);
	}

	public override void InitUIView()
	{
		base.InitUIView();

		//等级
		UpdateCurrentLevel();
		//名字
		UpdateRoleName();
		//设置玩家经验进度
		UpdateExbar();
		//Vip等级
		UpdateVipLevel();

		UpdateMonthCardImg();

        m_TipsController = CreateFunctionTipsController();
        m_TipsController.Refresh();
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
	}

    IFunctionTipsController CreateFunctionTipsController()
    {
        GameObject _go;
        FunctionTipsController _controller = new FunctionTipsController();

        var _manager = FunctionTipsManager.GetInstance();

        _go = selfTransform.FindChild("MonthCardBtn/TipsImage").gameObject;
        _controller.AddControlledObject(_go, _manager.CheckUnclaimedMonthCard);

        return _controller;
    }

	private void UpdateVipLevel()
	{
		int vipLevel = ObjectSelf.GetInstance().VipLevel;
		//vipTxt.text = vipLevel.ToString();
        InterfaceControler.AddLevelNum(vipLevel.ToString(), vipPos,true);
	}

	/// <summary>
	/// 经验条更新
	/// </summary>
	private void UpdateExbar()
	{
		PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(ObjectSelf.GetInstance().Level);
		Exbar.value = (float)ObjectSelf.GetInstance().Exp / (float)pRow.getExp();
	}
	/// <summary>
	/// 等级更新
	/// </summary>
	private void UpdateCurrentLevel()
	{
		short Lev = ObjectSelf.GetInstance().Level;
		CurrentLevel.text = Lev.ToString();
	}

	/// <summary>
	/// 名字
	/// </summary>
	private void UpdateRoleName()
	{
		string Name = ObjectSelf.GetInstance().Name;
        char[] _strArray = Name.ToCharArray();
        if (_strArray.Length == 2)
        {
            StringBuilder _str = new StringBuilder();
            _str.Append(_strArray[0]);
            _str.Append(" ");
            _str.Append(_strArray[1]);
            Name = _str.ToString();
        }
		RoleName.text = Name;
	}

	private void OnMonthCardInfoChange()
	{
		UpdateMonthCardImg();
	}

	void UpdateMonthCardImg()
	{
		Image img = monthCardBtn.GetComponent<Image>();
		img.sprite = monthCardImgs[ExchangeModule.GetMaxMonthCard() - 1];
		img.SetNativeSize();
	}

	private void OnMonthCardBtnClick()
	{
		UI_HomeControler.Inst.AddUI(UI_YueKaMgr.UI_ResPath);
	}

	void OnclickPlayerInfoBtn()
	{
		UI_HomeControler.Inst.AddUI(UI_PlayerInfoMes.UI_ResPath);
	}

	//Vip按钮
	private void OnclickVipBtn()
	{
		UI_HomeControler.Inst.AddUI(UI_VipPrivilege.GetPath(true));
	}

	//删除监听事件
	void OnDestroy()
	{

		GameEventDispatcher.Inst.removeEventListener(GameEventID.G_HumanLevel_Update, UpdateCurrentLevel);
		GameEventDispatcher.Inst.removeEventListener(GameEventID.G_HumanExp_Update, UpdateExbar);
		GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_MCHumanDetailAttribute_Name, UpdateRoleName);
		GameEventDispatcher.Inst.removeEventListener(GameEventID.G_VipLevel_Update, UpdateVipLevel);
		GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_RefreshMonthCard, OnMonthCardInfoChange);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_InterfaceChange, m_TipsController.Refresh);
	}
}
