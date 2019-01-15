using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameCore;
using DreamFaction.UI;
using GNET;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

public class UI_SkillShow : BaseUI 
{
    public static UI_SkillShow Inst;
    public static string UI_ResPath = "UI_Home/UI_SkillShow_2_1";
    private static ObjectCard m_Card;                       //当前卡牌
    private static int m_HeroTableId = 0;                   //当前英雄表ID
    private HeroTemplate m_HeroData;                        //当前英雄表数据

    private Text m_TilteTxt;                                //标题文本
    private Button m_BackBtn;                               //返回按钮
    private UI_SkillIcon m_SkillIcon;                       //技能脚本    
    private UI_TouchControler mTouchControl = null;         //触摸管理
    private SkillTargetStruct mSkillTargetStruct = new SkillTargetStruct();   // 当前选中的技能的信息

    public UI_SkillIcon GetSkillIconScript() { return m_SkillIcon; }

    /// <summary>
    /// 设置英雄的数据
    /// </summary>
    /// <param name="artRes">资源表</param>
    /// <param name="card">当前卡牌</param>
    public static void SetHeroData(ObjectCard card,int heroTabId)
    {
        m_Card = card;
        m_HeroTableId = heroTabId;
    }
    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_SkillIcon = selfTransform.FindChild("SkillIcon1").GetComponent<UI_SkillIcon>();
        m_TilteTxt = selfTransform.FindChild("PlayerInfoItem/Image/Text").GetComponent<Text>();
        m_BackBtn = selfTransform.FindChild("PlayerInfoItem/backBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityAction(OnClickBackBtn));

        mTouchControl = new UI_TouchControler();
        GameEventDispatcher.Inst.addEventListener(GameEventID.SE_ShowSkillTarget, onSingleSkillCall);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        InitHeroData();
        ObjectSelf.GetInstance().isSkillShow = true;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void InitHeroData()
    {
        m_HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_HeroTableId);

        ObjectHero _objHero = new ObjectHero();
        Hero _hero = new Hero();
        _hero.heroid = m_HeroTableId;
        _hero.skill1 = m_HeroData.getSkill1ID();
        _hero.skill2 = m_HeroData.getSkill2ID();
        _hero.skill3 = m_HeroData.getSkill3ID();
        _objHero.GetHeroData().Init(_hero);

        //_objHero.UpdateItemEffectValue();
        //_objHero.UpdateTeamEffectValue();
        //_objHero.InitEventData();
        _objHero.SetSpellNormalData();
        //_objHero.InitBaseData();
        //_objHero.UpdateSpellEffectValue();
        m_SkillIcon.setHero(_objHero);
        m_SkillIcon.InitIcon();
    }


    public override void UpdateUIState()
    {
        if (SceneObjectManager.GetInstance() != null)
        {
            SceneObjectManager.GetInstance().OnUpdateFightingLogic();
        }
        mTouchControl.Update();
    }
    /// <summary>
    /// 点击选择单体敌人
    /// </summary>
    /// <param name="e"></param>
    public void onSingleSkillCall(GameEvent e)
    {
        EventRequestSkillPackage package = (EventRequestSkillPackage)e.data;
        if (package != null)
        {
            mSkillTargetStruct.resetByEvent(package);
            mTouchControl.ChangeTouchState(TouchState.SelectSkillTarget_state);
        }
        package = null;
    }

    /// <summary>
    /// 查找是否有该目标
    /// </summary>
    /// <param name="obj"></param>
    public void onSingleTargetFind(ObjectCreature obj)
    {
        EM_OBJECT_TYPE type = obj.GetGroupType();

        if ((mSkillTargetStruct.isForSelf && type == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            || (!mSkillTargetStruct.isForSelf && type != EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO))
        {
            onRequestReleaseSkill(obj);
            // 找到目标
            mTouchControl.ChangeTouchState(TouchState.FireSign_state);
        }
    }
    /// <summary>
    /// 请求释放技能
    /// </summary>
    /// <param name="obj"></param>
    public void onRequestReleaseSkill(ObjectCreature obj)
    {
        // 请求释放技能
        EventRequestSkillPackage package = new EventRequestSkillPackage(mSkillTargetStruct.mSelctRoleUID, mSkillTargetStruct.mSelectSkillID, obj);
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_RequestReleaseSkill, package);
        mSkillTargetStruct.clear();
    }


    /// <summary>
    /// 返回按钮
    /// </summary>
    private void OnClickBackBtn()
    {
        ObjectSelf.GetInstance().SetHandBook(true);
        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        ObjectSelf.GetInstance().isSkillShow = false;
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.SE_ShowSkillTarget, onSingleSkillCall);
    }
    
    

}
