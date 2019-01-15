using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;

/// <summary>
/// 有获得新的勋章提示界面（在打开图鉴界面时弹出）
/// </summary>
public class UI_GetMedalPopMgr : BaseUI 
{
    public static string UI_ResPath = "UI_Home/UI_GetMedalPop_1_3";
    public static Queue<int> PopupQueue = new Queue<int>();
    private int m_id = 0;                                    //当前id(为英雄ID/图鉴表中的具体ID)
    private ArtresourceTemplate m_ArtResData = null;         //资源表数据
    private IllustratehandbookTemplate m_HandBookData = null;//当前图鉴数据表
    private HeroTemplate m_HeroData = null;                  //当前英雄数据表
    private Text m_Name_txt =null;                           //英雄名称
    private Text m_MedalNameTxt = null;                      //勋章名称
    private Image m_MedalIconImg = null;                     //勋章图标
    private Image m_IconImg = null;                          //英雄图标
    private Transform m_SharParent = null;                   //英雄星级父节点
    private Transform m_LevelParent = null;                  //等级父节点


    public override void InitUIData()
    {
        base.InitUIData();
        m_Name_txt = selfTransform.FindChild("Name_txt").GetComponent<Text>();
        m_MedalNameTxt = selfTransform.FindChild("MedalNameTxt").GetComponent<Text>();
        m_MedalIconImg = selfTransform.FindChild("MedalIconImg").GetComponent<Image>();
        m_IconImg = selfTransform.FindChild("IconImg").GetComponent<Image>();
        m_SharParent = selfTransform.FindChild("Star_Image");
        m_LevelParent = selfTransform.FindChild("Level_txt");
    }

    public override void InitUIView()
    {
        base.InitUIView();
        StartShow();
    }


    private void StartShow()
    {
        InitData();
        ParseXmlData();

        InitShowMedal();
        InitShowHeroData();

        Invoke("OnClose", 1.5f);
    }

    /// <summary>
    /// 向队列中添加数据
    /// </summary>
    /// <param name="id"></param>
    public static void QueueAdd(int id)
    {
        PopupQueue.Enqueue(id);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (PopupQueue.Count > 0)
        {
            m_id = PopupQueue.Dequeue();
            m_HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_id);
            m_ArtResData = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_HeroData.getArtresources());
        }
    }
    /// <summary>
    /// 初始化图鉴表数据
    /// </summary>
    /// <param name="id"></param>
    private void ParseXmlData()
    {
        Dictionary<int, IExcelBean> _handBookjXmlData = DataTemplate.GetInstance().m_IllustratehandbookTable.getData();
        foreach (var item in _handBookjXmlData)
        {
            IllustratehandbookTemplate _handBookData = item.Value as IllustratehandbookTemplate;
            if (_handBookData.getContentId() == m_id)
            {
                m_HandBookData = _handBookData;
            }
        }
    }
    /// <summary>
    /// 显示勋章名字和图标
    /// </summary>
    private void InitShowMedal()
    {
        m_MedalIconImg.rectTransform.localPosition = Vector3.zero;
        string _str = m_HandBookData.getReward().ToString();
        int _num = int.Parse(_str.Substring((_str.Length - 1), 1));
        if (_num == 5)
        {
            m_MedalIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath +"huangjin");
            m_MedalNameTxt.text = GameUtils.getString("pokedex_content4");
        }
        else if (_num == 6)
        {
            m_MedalIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "baiyin");            
            m_MedalNameTxt.text = GameUtils.getString("pokedex_content5");
            m_MedalIconImg.rectTransform.localPosition = new Vector3(0,20,0);
        }
        else if (_num == 7)
        {
            m_MedalIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "qingtong");
            m_MedalNameTxt.text = GameUtils.getString("pokedex_content6");
        }
        else if (_num == 8)
        {
            m_MedalIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "chitie");
            m_MedalNameTxt.text = GameUtils.getString("pokedex_content7");
        }
        m_MedalIconImg.SetNativeSize();
    }

    /// <summary>
    ///显示英雄信息
    /// </summary>
    private void InitShowHeroData()
    {
        m_Name_txt.text = GameUtils.getString(m_HeroData.getNameID());
        m_IconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ArtResData.getHeadartresource());
        for (int i = 5; i < 5 + m_HeroData.getQuality(); i++)
        {
            Image _img = m_SharParent.GetChild(i).GetComponent<Image>();
            _img.enabled = true;
        }
        InterfaceControler.AddLevelNum(m_HeroData.getMaxLevel().ToString(), m_LevelParent);
    }

    /// <summary>
    /// 重新打开
    /// </summary>
    private void ReStartPop()
    {
        StartShow();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭面板
    /// </summary>
    private void OnClose()
    {
        if (PopupQueue.Count > 0)
        {
            Invoke("ReStartPop", 0.1f);
            gameObject.SetActive(false);
        }
        else
            UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}

