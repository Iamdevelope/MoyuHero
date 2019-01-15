using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;

public class UI_TestRanking : BaseUI 
{
    public static UI_TestRanking Inst = null;
    private ObjectSelf m_Self = null;                                              //玩家信息
    private List<LimitRankInfo> m_FiftyWithinLit = null;                           //1-50的玩家排行榜
    private List<LimitRankInfo> m_FiftyToHundredList = null;                       //51-100的玩家排行榜
    private List<LimitRankInfo> m_HoundUpList = null;                              //100以上的玩家排行榜
    public List<UI_LimitRankItem> m_LimitRankItemList = null;                      //排行榜Item List
    private Button m_LookRankingBtn = null;                                        //查看奖励按钮
    private Button m_CloseBtn = null;                                              //关闭按钮
    private Text m_PromptText = null;                                              //提示文本
    private Text m_MyRankingTxt = null;                                            //我的排名
    private Text m_MyRankingLevelTxt = null;                                       //我的排名（值）

    public List<Button> _BtnList = null;
    private Button m_Btn_1 = null;                                                 //1-50级排名按钮
    private Button m_Btn_2 = null;                                                 //51-100级排名按钮
    private Button m_Btn_3 = null;                                                 //100+ 排名按钮
    private Text m_LevelTxt1 = null;
    private Text m_LevelTxt2 = null;
    private Text m_LevelTxt3 = null;
    private Image m_BtnImg_1 = null;
    private Image m_BtnImg_2 = null;
    private Image m_BtnImg_3 = null;
    private Image m_BtnIconImg_1 = null;
    private Image m_BtnIconImg_2 = null;
    private Image m_BtnIconImg_3 = null;
    private GameObject m_Prefab = null;                                             //预设
    private Transform m_Grid = null;                                                //父节点
    private Sprite m_SelectBtnImg = null;
    private Sprite m_NotSelectBtnImg = null;

    //排行榜界面控件
    private GameObject m_LookRankingObj = null;



    public override void InitUIData()
    {       
        base.InitUIData();
        Inst = this;
        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Home/HeroRankingItem") as GameObject;
        m_Grid = selfTransform.FindChild("ItemList/ListLayOut");
        m_LookRankingObj = selfTransform.parent.FindChild("LookRankingObj").gameObject;
        m_MyRankingTxt = selfTransform.FindChild("LeftPanel/MyRankingTxt").GetComponent<Text>();
        m_MyRankingLevelTxt = selfTransform.FindChild("LeftPanel/MyRankingLevelTxt").GetComponent<Text>();
        m_PromptText = selfTransform.FindChild("HintObj/Bottom/Text").GetComponent<Text>();
        m_LookRankingBtn = selfTransform.FindChild("LookRankingBtn").GetComponent<Button>();
        m_CloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();

        m_Btn_1 = selfTransform.FindChild("LeftPanel/Btn_1").GetComponent<Button>();
        m_Btn_2 = selfTransform.FindChild("LeftPanel/Btn_2").GetComponent<Button>();
        m_Btn_3 = selfTransform.FindChild("LeftPanel/Btn_3").GetComponent<Button>();
        m_LevelTxt1 = m_Btn_1.transform.FindChild("Text").GetComponent<Text>();
        m_LevelTxt2 = m_Btn_2.transform.FindChild("Text").GetComponent<Text>();
        m_LevelTxt3 = m_Btn_3.transform.FindChild("Text").GetComponent<Text>();
        m_BtnImg_1 = m_Btn_1.transform.FindChild("Image").GetComponent<Image>();
        m_BtnImg_2 = m_Btn_2.transform.FindChild("Image").GetComponent<Image>();
        m_BtnImg_3 = m_Btn_3.transform.FindChild("Image").GetComponent<Image>();
        m_BtnIconImg_1 = m_Btn_1.GetComponent<Image>();
        m_BtnIconImg_2 = m_Btn_2.GetComponent<Image>();
        m_BtnIconImg_3 = m_Btn_3.GetComponent<Image>();

        m_LookRankingBtn.onClick.AddListener(new UnityAction(OnLookRankingBtn));
        m_CloseBtn.onClick.AddListener(new UnityAction(OnClose));
        m_Btn_1.onClick.AddListener(new UnityAction(OnClickBtn1));
        m_Btn_2.onClick.AddListener(new UnityAction(OnClickBtn2));
        m_Btn_3.onClick.AddListener(new UnityAction(OnClickBtn3));

        m_SelectBtnImg = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_liebiaoSel");
        m_NotSelectBtnImg = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_liebiao");


        GameEventDispatcher.Inst.addEventListener(GameEventID.F_LimitRankUpdate, UpdateUIShow);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_Self = ObjectSelf.GetInstance();
        //InitListData();
        InitRankingText();
        //InitLeftBtnsUIView();

    }
    /// <summary>
    /// 更新数据显示
    /// </summary>
    private void UpdateUIShow()
    {        
        InitListData();
        InitLeftBtnsUIView();
    }
    /// <summary>
    /// 初始化排行榜数据
    /// </summary>
    private void InitListData()
    {
        m_FiftyWithinLit = m_Self.LimitFightMgr.m_RankFirstStage;
        m_FiftyToHundredList = m_Self.LimitFightMgr.m_RankSecondStage;
        m_HoundUpList = m_Self.LimitFightMgr.m_RankLastStage;
        m_MyRankingLevelTxt.text = InitRinKingShow();
    }
    /// <summary>
    /// 初始化文本
    /// </summary>
    private void InitRankingText()
    {
        m_PromptText.text = GameUtils.getString("ultimatetrial_content46");
        m_MyRankingTxt.text = GameUtils.getString("ultimatetrial_content45");
    }

    /// <summary>
    /// 初始化按钮组
    /// </summary>
    private void InitLeftBtnsUIView()
    {
        OnClickBtn1();
    }

    /// <summary>
    /// 显示排行榜数据
    /// </summary>
    /// <param name="list"></param>
    private void ShowRankDta(List<LimitRankInfo> list)
    {
        InitItemShow();
        for (int i = 0; i < list.Count; i++)
        {
            m_LimitRankItemList[i].m_RoleId = list[i].m_RoleId;
            m_LimitRankItemList[i].m_RoleName = list[i].m_RoleName;
            m_LimitRankItemList[i].m_Level = list[i].m_Level;
            m_LimitRankItemList[i].m_GroupNum = list[i].m_GroupNum;
            m_LimitRankItemList[i].m_TroopType = list[i].m_TroopType;
            m_LimitRankItemList[i].m_AlldropNum = list[i].m_AlldropNum;
            m_LimitRankItemList[i].m_OnRankNum = list[i].m_OnRankNum;
            m_LimitRankItemList[i].m_HeroAttribute = list[i].m_HeroAttribute;

            m_LimitRankItemList[i].ShowRankItemData();
        }
    }

//     /// <summary>
//     /// 显示1-50级的排行数据
//     /// </summary>
//     private void Show1_50Data()
//     {
//         InitItemShow();
//         //m_LimitRankItemList.Clear();
//         for (int i = 0; i < m_FiftyWithinLit.Count; i++)
//         {
//             //GameObject _go = Instantiate(m_Prefab) as GameObject;
//             m_LimitRankItemList[i].m_RoleId = m_FiftyWithinLit[i].m_RoleId;
//             m_LimitRankItemList[i].m_RoleName = m_FiftyWithinLit[i].m_RoleName;
//             m_LimitRankItemList[i].m_Level = m_FiftyWithinLit[i].m_Level;
//             m_LimitRankItemList[i].m_GroupNum = m_FiftyWithinLit[i].m_GroupNum;
//             m_LimitRankItemList[i].m_TroopType = m_FiftyWithinLit[i].m_TroopType;
//             m_LimitRankItemList[i].m_AlldropNum = m_FiftyWithinLit[i].m_AlldropNum;
//             m_LimitRankItemList[i].m_OnRankNum = m_FiftyWithinLit[i].m_OnRankNum;
//             m_LimitRankItemList[i].m_HeroAttribute = m_FiftyWithinLit[i].m_HeroAttribute;
// 
//             m_LimitRankItemList[i].ShowRankItemData();
//         }
//     }


    /// <summary>
    /// 显示排名
    /// </summary>
    /// <returns></returns>
    private string InitRinKingShow()
    {
        if (m_Self.Level > 0 && m_Self.Level <= 50)
        {
            for (int i = 0; i < m_FiftyWithinLit.Count; i++)
            {
                if (m_FiftyWithinLit[i].m_RoleId == m_Self.Guid.GUID_value)
                    return (i + 1).ToString();
                else
                    return GameUtils.getString("ultimatetrial_content11");
            }
        }
        else if (m_Self.Level > 50 && m_Self.Level <= 100)
        {
            for (int j = 0; j < m_FiftyToHundredList.Count; j++)
            {
                if (m_FiftyToHundredList[j].m_RoleId == m_Self.Guid.GUID_value)
                    return (j + 1).ToString();
                else
                    return GameUtils.getString("ultimatetrial_content11");
            }
        }
        else if(m_Self.Level > 100)
        {
            for (int k = 0; k < m_HoundUpList.Count; k++)
            { 
                if (m_HoundUpList[k].m_RoleId == m_Self.Guid.GUID_value)
                    return (k + 1).ToString();
                else
                    return GameUtils.getString("ultimatetrial_content11");
            }
        }

        //if (m_Self.LimitFightMgr.m_TodayRanking == -1)
        //    return GameUtils.getString("ultimatetrial_content10");
        //else if (m_Self.LimitFightMgr.m_TodayRanking > 20)
        //    return GameUtils.getString("ultimatetrial_content11");
        //else if (m_Self.LimitFightMgr.m_TodayRanking > 0 && m_Self.LimitFightMgr.m_TodayRanking < 20)
        //    return m_Self.LimitFightMgr.m_TodayRanking.ToString();

        return GameUtils.getString("ultimatetrial_content10");
    }
    /// <summary>
    /// 设置隐藏Item隐藏
    /// </summary>
    private void InitItemShow()
    {
        for (int i = 0; i < m_LimitRankItemList.Count; i++)
        {
            m_LimitRankItemList[i].SetActiveObj(false);
        }
    }




    /// <summary>
    /// 查看奖励按钮
    /// </summary>
    void OnLookRankingBtn()
    {
        m_LookRankingObj.SetActive(true);
    }

    /// <summary>
    /// 关闭按钮
    /// </summary>
    private  void OnClose()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 点击1-50级按钮
    /// </summary>
    private void OnClickBtn1()
    {
        SetBtnShow(m_BtnImg_1, m_LevelTxt1, m_BtnIconImg_1);
        ShowRankDta(m_FiftyWithinLit);
    }
    /// <summary>
    /// 点击51-100级按钮
    /// </summary>
    private void OnClickBtn2()
    {
        SetBtnShow(m_BtnImg_2, m_LevelTxt2, m_BtnIconImg_2);
        ShowRankDta(m_FiftyToHundredList);
    }
    /// <summary>
    /// 点击100+按钮
    /// </summary>
    private void OnClickBtn3()
    {
        SetBtnShow(m_BtnImg_3, m_LevelTxt3, m_BtnIconImg_3);
        ShowRankDta(m_HoundUpList);
    }

    /// <summary>
    /// 设置显示按钮状态
    /// </summary>
    /// <param name="btnImg">选中按钮Img控件</param>
    /// <param name="levelTxt">选中按钮等级Text控件</param>
    /// <param name="iconImg">按钮控件</param>
    private void SetBtnShow(Image btnImg,Text levelTxt,Image iconImg)
    {
        m_BtnIconImg_1.sprite = m_NotSelectBtnImg;
        m_BtnIconImg_2.sprite = m_NotSelectBtnImg;
        m_BtnIconImg_3.sprite = m_NotSelectBtnImg;
        m_BtnImg_1.enabled = false;
        m_BtnImg_2.enabled = false;
        m_BtnImg_3.enabled = false;
        m_LevelTxt1.transform.localPosition = new Vector3(-100, 0, 0);
        m_LevelTxt2.transform.localPosition = new Vector3(-100, 0, 0);
        m_LevelTxt3.transform.localPosition = new Vector3(-100, 0, 0);

        btnImg.enabled = true;
        levelTxt.transform.localPosition = new Vector3(55, 0, 0);
        iconImg.sprite = m_SelectBtnImg;

    }


    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_LimitRankUpdate, UpdateUIShow);
    }
    
    

}
