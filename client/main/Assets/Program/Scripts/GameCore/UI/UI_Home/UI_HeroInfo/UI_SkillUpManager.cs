using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
public class UI_SkillUpManager : BaseUI
{
    public static UI_SkillUpManager _instance;

    private ObjectCard _objCard;//临时记录的卡牌
    private Transform _Grid;
    private Image Skill_1ConImg;
    private Text Skill_1ConTxt;
    private Image Skill_2ConImg;
    private Text Skill_2ConTxt;

    private GameObject[] m_TipsImageArray;
    private IFunctionTipsController m_TipsController;
    public override void InitUIData()
    {
        _instance = this;
        _Grid = selfTransform.FindChild("Content");
        Skill_1ConImg = selfTransform.FindChild("ConI_1Img").GetComponent<Image>();
        Skill_1ConTxt = Skill_1ConImg.transform.FindChild("Text").GetComponent<Text>();
        Skill_2ConImg = selfTransform.FindChild("ConI_2Img").GetComponent<Image>();
        Skill_2ConTxt = Skill_2ConImg.transform.FindChild("Text").GetComponent<Text>();

        int _skillcount = 3;
        m_TipsImageArray = new GameObject[_skillcount];
        //动态添加脚本
        for (int i = 0; i < _Grid.childCount - 1; i++)
        {
            if (_Grid.GetChild(i).GetComponent<UI_SkillItem>() == null)
            {
                _Grid.GetChild(i).gameObject.AddComponent<UI_SkillItem>();
            }
            if (i < _skillcount)
            {
                m_TipsImageArray[i] = _Grid.GetChild(i).FindChild("Button/TipsImage").gameObject;
            }
        }
        m_TipsController = CreateFunctionTipsController();

        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, UpdateSkillUIShow);

        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, RefreshController);
        GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, RefreshController);
	}

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, UpdateSkillUIShow);

        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, RefreshController);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, RefreshController);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        SkillUIShow(UI_HeroInfoManager._instance.GetCurCard());
        //SkillUIShow(_objCard);

        RefreshController();
    }

    //技能升级显示数据
    public void SkillUIShow(ObjectCard objCard)
    {
        _objCard = objCard;
        //先通过英雄卡牌获取到对应的技能组
        SpellData[] SkillDataList = objCard.GetHeroData().SpellDataList;
        //遍历技能组
        int length = SkillDataList.Length;
        for (int i = 0; i < length - 1; i++)
        {
            UI_SkillItem skillItem = _Grid.GetChild(i).GetComponent<UI_SkillItem>();
            //UI_SkillItem skillItem = go.GetComponent<UI_SkillItem>();
            SpellData temp = SkillDataList[i];
//            skillItem.ShowSkill(temp,objCard);
        }
    }

    //接收服务器反馈结果 判断是否升级成功
    public void ReturnResult(byte pos)
    {
        SkillUIShow(_objCard);
        //根据当前升级的技能播放特效
        UI_SkillItem skillitem = _Grid.GetChild((int)pos - 1).GetComponent<UI_SkillItem>();
//        skillitem.ShowEffect();
    }

    //显示需要消耗的道具图标和数量
    public void ShowCon1Info(int ConCount,string ConImg)
    {
        Skill_1ConImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + ConImg);
        Skill_1ConTxt.text = ConCount.ToString();
    }

    public void ShowCon2Info(int ConCount, string ConImg)
    {
        Skill_2ConImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + ConImg);
        Skill_2ConTxt.text = ConCount.ToString();
    }

    void OnSelectCardHeroChanged(GameEvent ev)
    {
        if (ev == null || ev.data == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ObjectCard card = ev.data as ObjectCard;

        if (card == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        SkillUIShow(card);

    }

    void UpdateSkillUIShow()
    {
        SkillUIShow(UI_HeroInfoManager._instance.GetCurCard());
    }


    public void ShowUI()
    {
        this.gameObject.SetActive(true);

    }

    public void HideUI()
    {
        if (this.gameObject != null)
        {
            this.gameObject.SetActive(false);
        }
    }

    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        var _manager = FunctionTipsManager.GetInstance();
        if (_manager == null)
            return null;

        FunctionTipsControllerBoolArrayType _controller =
            new FunctionTipsControllerBoolArrayType(m_TipsImageArray, _manager.CheckEverySkillUpgrade);

        return _controller;
    }

    private void RefreshController()
    {
        if (m_TipsController == null)
            return;

        if (FunctionTipsManager.GetInstance().CheckHeroIsInDefaultTeam())
            m_TipsController.Refresh();
        else
            m_TipsController.CloseAllTips();
    }


}
