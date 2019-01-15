using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using DreamFaction.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameEventSystem;

public class UI_FormMgr : BaseUI
{
    public static string UI_ResPath = "UI_Form/UI_FormWindow_2_1";
    private static UI_FormMgr _inst;

    private List<long> m_BackHeroGuids = new List<long>();                                 //记录当前站在后排的英雄GUid
    private List<UI_ClickHero> m_HeroClickList = new List<UI_ClickHero>();                 //英雄点击按钮List
    private GameObject m_HeroClickTeam = null;                                             //选择按钮编队OBJ
    private Button m_BackBtn = null;                                                       //返回按钮
    private Text m_TotalPowerTxt = null;
    private Image m_PowerBgImg = null;

    private int m_SelctGodSoulID = -1;
    private int m_TotalPower = 0;



    public static UI_FormMgr Inst 
    { 
        get { return _inst; } 
    }

    //设置当前站在后排的英雄的GUID 
    public void SetBackHeroGuids(long guid) 
    {
        m_BackHeroGuids.Add(guid);
    }

    //获得当前站在后排的英雄GUID
    public List<long> GetBackHeroGuids() 
    { 
        return m_BackHeroGuids; 
    }

    public void SetGodSoulID(int id)
    {
        m_SelctGodSoulID = id;
    }

    public int GetGodSoulID()
    {
        return m_SelctGodSoulID;
    }

    public override void InitUIData()
    {
        base.InitUIData();

        _inst = this;

        UI_MainHome.m_CamForm.SetActive(true);
        m_HeroClickTeam = selfTransform.FindChild("TeamButtons").gameObject;
        m_PowerBgImg = selfTransform.FindChild("PowerPanel/BackGround").GetComponent<Image>();
        m_TotalPowerTxt = selfTransform.FindChild("PowerPanel/PowerTxt").GetComponent<Text>();
        m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityAction(onBackCall));
        for (int i = 0; i < 5; ++i)
        {
            m_HeroClickList.Add(m_HeroClickTeam.transform.GetChild(i).GetComponent<UI_ClickHero>());
        }

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Formation_Update, UpdateUIShow);
    }

    public void UpdateUIShow()
    {
        InitGodSoulUI();
    }

    public override void InitUIView()
    {
        base.InitUIView();

        HomeControler.Inst.UpdateFormationGameObject();
        InitClickHeroData();

        InitGodSoulUI();
// 
//         InvokeRepeating("ShowShanGuangEff", 1f, Time.deltaTime);
    }

    /// <summary>
    /// 初始化点击阵型 显示总战斗力
    /// </summary>
    private void InitClickHeroData()
    {
        m_BackHeroGuids.Clear();

        m_TotalPower = 0;

        int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < HeroCount; ++i)
        {            
            ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);

            m_HeroClickList[i].SetEffectActive(temp != null);

            if (temp == null)
                continue;

            m_TotalPower += temp.GetHeroData().FightVigor;
            m_TotalPowerTxt.text = m_TotalPower.ToString();

            m_HeroClickList[i].InitData(temp);
        }
    }

//     int count = 0;
//     private void ShowShanGuangEff()
//     {
//         //count++;
//         Material mat = m_PowerBgImg.material;
//         float matTime = mat.GetFloat("_percent");
//         mat.SetFloat("_percent", matTime+Time.deltaTime);
//         if (mat.GetFloat("_percent") >= 2f)
//         {
//             mat.SetFloat("_percent", -1.0f);
//         }
// 
//     }


    //返回按钮
    private void onBackCall()
    {
        RenderSettings.fog = true;
        UI_MainHome.m_CamForm.SetActive(false);
        GameEventDispatcher.Inst.clearEvent(GameEventID.G_Formation_Update);
        onClose();

        HomeControler.Inst.DestroyFromModel();
    }

    public void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    //=========================================================================
    //假数据  临时


    public Sprite ImgClose = null;
    public Sprite Img = null;
    public int mSouelId1 = -1;
    public int mSouelId2 = -1;
    public int mSouelId3 = -1;
    public int mSouelId4 = -1;
    //public OwnGodSoul temp = new OwnGodSoul();
    private void InitGodSoulUI()
    {
        Team team = ObjectSelf.GetInstance().Teams;
        mSouelId1 = team.m_GodSoulID1;
        mSouelId2 = team.m_GodSoulID2;
        mSouelId3 = team.m_GodSoulID3;
        mSouelId4 = team.m_GodSoulID4;

        Transform soulPanelTrans = selfTransform.FindChild("SoulPanel");
        UI_GodSoulItemBtn ui_GodSoul1 = soulPanelTrans.GetChild(0).GetComponent<UI_GodSoulItemBtn>();
        UI_GodSoulItemBtn ui_GodSoul2 = soulPanelTrans.GetChild(1).GetComponent<UI_GodSoulItemBtn>();
        UI_GodSoulItemBtn ui_GodSoul3 = soulPanelTrans.GetChild(2).GetComponent<UI_GodSoulItemBtn>();
        UI_GodSoulItemBtn ui_GodSoul4 = soulPanelTrans.GetChild(3).GetComponent<UI_GodSoulItemBtn>();

        ui_GodSoul1.InitShowData(mSouelId1,6);
        ui_GodSoul2.InitShowData(mSouelId2,7);
        ui_GodSoul3.InitShowData(mSouelId3,8);
        ui_GodSoul4.InitShowData(mSouelId4,9);

    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Formation_Update, UpdateUIShow);
    }
}
