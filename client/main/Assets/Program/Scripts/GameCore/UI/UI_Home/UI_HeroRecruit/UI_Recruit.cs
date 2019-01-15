using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class UI_Recruit : UI_RecruitBase
{
	public static UI_Recruit inst;
	public static string UI_ResPath = "UI_Home/UI_Recruit_2_0";

	bool m_IsHerorecruit = true;

	GameObject m_HeroRecruit;
	GameObject m_RelicTreasure;

    private IFunctionTipsController m_TipsController;
    private GameObject m_RecruitTipsImage;
    private GameObject m_RelicTipsImage;

    public GameObject m_RecruitScene;
    public GameObject m_CimeliaScene;

    public GameObject m_Caption;

	public override void InitUIData()
	{
        inst = this;
		base.InitUIData();

		m_HeroRecruit = selfTransform.FindChild("UI_HeroRecruit").gameObject;
		m_RelicTreasure = selfTransform.FindChild("UI_RelicTreasure").gameObject;
        m_Caption = selfTransform.FindChild("caption").gameObject;

        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.AwakeUp(m_Caption.transform);
        //m_RecruitTipsImage = selfTransform.FindChild("UI_Top/HeroRecruitBtn").gameObject;
        //m_RelicTipsImage = selfTransform.FindChild("UI_Top/RelicBtn").gameObject;
        HomeControler.Inst.PushFunly(12, 101);

		m_HeroRecruit.SetActive(true);
		m_RelicTreasure.SetActive(false);
        uiMark = DreamFaction.UI.Core.UIMark.HeroRecruit;

        //m_TipsController = CreateFunctionTipsController();
	}

	public override void InitUIView()
	{
		base.InitUIView();
        RefreshController();

        try
        {
            m_RecruitScene = GameObject.Find("Recruit_Scene");
            m_CimeliaScene = GameObject.Find("Cimelia_Scene");

            m_RecruitScene.SetActive(true);
            m_CimeliaScene.SetActive(false);
        }
        catch (System.Exception ex)
        {
        	
        }
	}

	protected override void OnClickBackBtn()
	{
		UI_HomeControler.Inst.ReMoveUI(gameObject);
        if (GuideManager.GetInstance().isGuideUser && GuideManager.GetInstance().IsContentGuideID(100301) == false && GuideManager.GetInstance().GetBackCount(100204) == true)
            UI_MainHome.GetInst().InitGuideFightBtn(100301);
	}

    //引导招募返回按钮，下一个引导是战斗按钮
    public void InitGuideBack()
    {
        GuideManager.GetInstance().ShowGuideWithIndex(100204);
    }

    protected override void OnClickHeroRecruitBtn()
    {
        OpenRecruitBtn();
    }

	protected override void OnClickRelicBtn()
	{
        OpenRelicBtn();
	}

    // 打开招募
    public void OpenRecruitBtn()
    {
        if (m_IsHerorecruit)
            return;

        m_RelicTreasure.SetActive(false);
        m_RelicBtn.transform.FindChild("Image").gameObject.SetActive(false);
        m_RelicBtn.transform.FindChild("Text").GetComponent<Text>().color = new Color(172.0f / 255, 169.0f / 255, 180.0f / 255);
        m_HeroRecruit.SetActive(true);
        m_HeroRecruitBtn.transform.FindChild("Image").gameObject.SetActive(true);
        m_HeroRecruitBtn.transform.FindChild("Text").GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f);
        m_IsHerorecruit = true;
        uiMark = DreamFaction.UI.Core.UIMark.HeroRecruit;
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);

        try
        {
            m_RecruitScene.SetActive(true);
            m_CimeliaScene.SetActive(false);
        }
        catch (System.Exception ex)
        {
        	
        }
    }

    // 打开遗迹宝藏
    public void OpenRelicBtn()
    {
        if (!m_IsHerorecruit)
            return;

        m_RelicTreasure.SetActive(true);
        m_RelicBtn.transform.FindChild("Image").gameObject.SetActive(true);
        m_RelicBtn.transform.FindChild("Text").GetComponent<Text>().color = new Color(1.0f,1.0f,1.0f);
        m_HeroRecruit.SetActive(false);
        m_HeroRecruitBtn.transform.FindChild("Image").gameObject.SetActive(false);
        m_HeroRecruitBtn.transform.FindChild("Text").GetComponent<Text>().color = new Color(172.0f / 255, 169.0f / 255, 180.0f / 255);
        m_IsHerorecruit = false;
        uiMark = DreamFaction.UI.Core.UIMark.RelicTreasure;
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_InterfaceChange);

        try
        {
            m_RecruitScene.SetActive(false);
            m_CimeliaScene.SetActive(true);
        }
        catch (System.Exception ex)
        {
        	
        }
        
    }
    
    //生成功能提示控制器
    IFunctionTipsController CreateFunctionTipsController()
    {
        var _manager = FunctionTipsManager.GetInstance();
        if (_manager == null)
            return null;

        FunctionTipsController _controller = new FunctionTipsController();

        _controller.AddControlledObject(m_RecruitTipsImage, _manager.CheckHeroRecruitFree);
        _controller.AddControlledObject(m_RelicTipsImage, _manager.CheckRelicFreeCount);

        return _controller;
    }

    public void RefreshController()
    {
        if (m_TipsController == null)
            return;

        m_TipsController.Refresh();
    }

    void OnDestroy()
    {
        UI_CaptionManager _caption = UI_CaptionManager.GetInstance();
        if (_caption != null)
            _caption.Release(m_Caption.transform);
    }
}
