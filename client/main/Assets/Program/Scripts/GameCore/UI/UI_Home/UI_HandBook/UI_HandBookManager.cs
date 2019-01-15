using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using System.Text;
using DreamFaction.GameEventSystem;
using System.Linq;
using DreamFaction.GameCore;

public class UI_HandBookManager : UI_HandBookManagerBase
{
    public static UI_HandBookManager Inst;
    public static string UI_ResPath = "UI_Home/UI_HandBook_2_2";

    private Transform m_Grid;
    private GameObject m_Prefab;                                                                                 //Item预设
    private List<IllustratehandbookTemplate> m_HeroHandBookList = new List<IllustratehandbookTemplate>();        //英雄图鉴
    public  List<IllustratehandbookTemplate> m_RaueHandBookList = new List<IllustratehandbookTemplate>();        //符文图鉴
    private List<IllustratehandbookTemplate> m_PeopleHandBooks = new List<IllustratehandbookTemplate>();         //生灵图鉴
    private List<IllustratehandbookTemplate> m_GodHandBooks = new List<IllustratehandbookTemplate>();            //神灵图鉴
    private List<IllustratehandbookTemplate> m_DevilHandBooks = new List<IllustratehandbookTemplate>();          //恶魔图鉴  
    private LoopLayout m_HeroLayout;                                                                             //列表
    public List<IllustratehandbookTemplate> m_TempHeroList;                                                      //用于临时记录当前的数据列表

    public int m_PigIronCount = 0;                                                                              //生铁数量 8
    public int m_BronzeCount = 0;                                                                               //青铜数量 7 
    public int m_SilverCount = 0;                                                                               //白银数量 6
    public int m_GoldCount = 0;                                                                                 //黄金数量 5
    private GameObject m_DeitiesSelectImg;                                                                      //神灵按钮选择背景
    private GameObject m_DeitiesNotSelectImg;                                                                   //神灵按钮未选择背景
    private GameObject m_PeopleSelectImg;                                                                       //生灵按钮选择背景
    private GameObject m_PeopleNotSelectImg;                                                                    //生灵按钮未选择背景
    private GameObject m_DevdilSelectImg;                                                                       //恶魔按钮选择背景
    private GameObject m_DevdilNotSelectImg;                                                                    //恶魔按钮未选择背景
    private GameObject m_MedalReardNewImg;                                                                      //勋章奖励New提示图标
    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        InitPraseXmlData();
        InitClassify();
        InitMedalMaxCountShow();

        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Home/UI_HandBookItem") as GameObject;
        m_Grid = selfTransform.FindChild("ItemList/Grid");
        m_HeroLayout = selfTransform.FindChild("ItemList/Grid").GetComponent<LoopLayout>();
        m_DeitiesSelectImg = selfTransform.FindChild("DeitiesBtn/SelectStateImg").gameObject;
        m_DeitiesNotSelectImg = selfTransform.FindChild("DeitiesBtn/NotSelectStateImg").gameObject;
        m_PeopleSelectImg = selfTransform.FindChild("PeopleBtn/SelectStateImg").gameObject;
        m_PeopleNotSelectImg = selfTransform.FindChild("PeopleBtn/NotSelectStateImg").gameObject;
        m_DevdilSelectImg = selfTransform.FindChild("DevdilBtn/SelectStateImg").gameObject;
        m_DevdilNotSelectImg = selfTransform.FindChild("DevdilBtn/NotSelectStateImg").gameObject;
        m_MedalReardNewImg = m_ReardBtn.transform.FindChild("NewImg").gameObject;
        HomeControler.Inst.PushFunly(9, 71);

        GameEventDispatcher.Inst.addEventListener(GameEventID.HB_GetMedalPop, ShowGetMedalPop);
        GameEventDispatcher.Inst.addEventListener(GameEventID.HB_GetSTuJianHeros, RefreshTips);

        captionPath = "caption";
    }

    /// <summary>
    /// 解析图鉴Xml数据
    /// </summary>
    private void InitPraseXmlData()
    {
        Dictionary<int, IExcelBean> _handBookjXmlData = DataTemplate.GetInstance().m_IllustratehandbookTable.getData();
        foreach (var item in _handBookjXmlData)
        {
            IllustratehandbookTemplate _handBookData = (IllustratehandbookTemplate)DataTemplate.GetInstance().m_IllustratehandbookTable.getTableData(item.Key);
            if (_handBookData.getType() == 1)
                m_HeroHandBookList.Add(_handBookData);
            else
                m_RaueHandBookList.Add(_handBookData);
        }
    }

    /// <summary>
    /// 初始分类
    /// </summary>
    private void InitClassify()
    {
        for (int i = 0; i < m_HeroHandBookList.Count; i++)
        {
            int _heroId = m_HeroHandBookList[i].getContentId();
            HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_heroId);
            if (_hero.getCamp() == 1)
            {
                m_PeopleHandBooks.Add(m_HeroHandBookList[i]);
            }
            else if (_hero.getCamp() == 2)
            {
                m_GodHandBooks.Add(m_HeroHandBookList[i]);
            }
            else if (_hero.getCamp() == 3)
            {
                m_DevilHandBooks.Add(m_HeroHandBookList[i]);
            }
        }
    }
    /// <summary>
    /// 初始化最大勋章最大个数
    /// </summary>
    private void InitMedalMaxCountShow()
    {
        for (int i = 0; i < m_HeroHandBookList.Count; i++)
        {
            string _str = m_HeroHandBookList[i].getReward().ToString();
            int _num = int.Parse(_str.Substring((_str.Length - 1), 1));
            if (_num == 5)
                m_GoldCount += 1;
            else if (_num == 6)
                m_SilverCount += 1;
            else if (_num == 7)
                m_BronzeCount += 1;
            else if (_num == 8)
                m_PigIronCount += 1;
        }
    }

    public override void InitUIView()
    {
        base.InitUIView();
        RefreshTips();
        OnClickDeitiesBtn();
        ShowMedalCountText();
        InitDesTxt();

    }
    /// <summary>
    /// 初始化文本
    /// </summary>
    private void InitDesTxt()
    {
        m_PromptTxt1.text = GameUtils.getString("pokedex_content15");
        m_PromptTxt2.text = GameUtils.getString("pokedex_content2");
    }


    /// <summary>
    /// 加载Prefab 并传入数据
    /// </summary>
    private void InitLoadUI(List<IllustratehandbookTemplate> handBookList)
    {
        SortItem(handBookList);
        m_TempHeroList = handBookList;
        m_HeroLayout.cellCount = handBookList.Count;
        m_HeroLayout.updateCellEvent = UpdateHandBookItem;
        m_HeroLayout.Reload();
    }
    /// <summary>
    /// 排序Item
    /// </summary>
    /// <param name="handBookList">当前图鉴集合</param>
    private void SortItem(List<IllustratehandbookTemplate> handBookList)
    {
        for (int i = 0; i < handBookList.Count; i++)
        {
            IllustratehandbookTemplate _temp = null;
            for (int j = 0; j < handBookList.Count - 1; j++)
            {
                if (handBookList[j].getSortingId() > handBookList[j + 1].getSortingId())
                {
                    _temp = handBookList[j];
                    handBookList[j] = handBookList[j + 1];
                    handBookList[j + 1] = _temp;
                }
            }
        }
    }
    /// <summary>
    /// 加载
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cell"></param>
    void UpdateHandBookItem(int index, RectTransform cell)
    {
        UI_HandBookItem _item = cell.GetComponent<UI_HandBookItem>();
        if (_item == null)
        {
            _item = cell.gameObject.AddComponent<UI_HandBookItem>();
        }

        _item.index = index;
        _item.InitShowViewData(m_TempHeroList[index]);

    }


    /// <summary>
    /// 显示勋章个数
    /// </summary>
    private void ShowMedalCountText()
    {
        m_CurNumTxt1.text = ObjectSelf.GetInstance().ChiTieXZ.ToString();
        m_CurNumTxt2.text = ObjectSelf.GetInstance().QingTongXZ.ToString();
        m_CurNumTxt3.text = ObjectSelf.GetInstance().BaiJinXZ.ToString();
        m_CurNumTxt4.text = ObjectSelf.GetInstance().HuangjinXZ.ToString();

        StringBuilder _strBuduilder1 = new StringBuilder();
        StringBuilder _strBuduilder2 = new StringBuilder();
        StringBuilder _strBuduilder3 = new StringBuilder();
        StringBuilder _strBuduilder4 = new StringBuilder();
        _strBuduilder1.Append("/");
        _strBuduilder1.Append(m_PigIronCount);
        _strBuduilder2.Append("/");
        _strBuduilder2.Append(m_BronzeCount);
        _strBuduilder3.Append("/");
        _strBuduilder3.Append(m_SilverCount);
        _strBuduilder4.Append("/");
        _strBuduilder4.Append(m_GoldCount);



        m_MaxNumTxt1.text = _strBuduilder1.ToString();
        m_MaxNumTxt2.text = _strBuduilder2.ToString();
        m_MaxNumTxt3.text = _strBuduilder3.ToString();
        m_MaxNumTxt4.text = _strBuduilder4.ToString();

    }






    /// <summary>
    /// 返回按钮
    /// </summary>
    protected override void OnClickBackBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    /// <summary>
    /// 勋章奖励按钮
    /// </summary>
    protected override void OnClickReardBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_MedalReard.UI_ResPath);
    }

    /// <summary>
    /// 专属符文按钮
    /// </summary>
    protected override void OnClickPatentRuneBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_PatentRuneManager.UI_ResPath);
    }



    /// <summary>
    /// 神灵按钮
    /// </summary>
    protected override void OnClickDeitiesBtn()
    {
        SetBtnBackImgShow(m_DeitiesSelectImg, m_DeitiesNotSelectImg);
        InitLoadUI(m_GodHandBooks);
    }

    /// <summary>
    /// 生灵按钮
    /// </summary>
    protected override void OnClickPeopleBtn()
    {
        SetBtnBackImgShow(m_PeopleSelectImg, m_PeopleNotSelectImg);
        InitLoadUI(m_PeopleHandBooks);
    }
    /// <summary>
    /// 恶魔按钮
    /// </summary>
    protected override void OnClickDevdilBtn()
    {
        SetBtnBackImgShow(m_DevdilSelectImg, m_DevdilNotSelectImg);
        InitLoadUI(m_DevilHandBooks);
    }

    /// <summary>
    /// 设置按钮背景效果显示
    /// </summary>
    /// <param name="selectObj">选择按钮对象</param>
    /// <param name="selectObjNot">选择按钮对象的未选择状态</param>
    private void SetBtnBackImgShow(GameObject selectObj, GameObject selectObjNot)
    {
        m_DeitiesSelectImg.SetActive(false);
        m_PeopleSelectImg.SetActive(false);
        m_DevdilSelectImg.SetActive(false);

        m_DeitiesNotSelectImg.SetActive(true);
        m_PeopleNotSelectImg.SetActive(true);
        m_DevdilNotSelectImg.SetActive(true);

        selectObj.SetActive(true);
        selectObjNot.SetActive(false);
    }

    /// <summary>
    /// 弹窗管理  奖励按钮New提示Img
    /// </summary>
    /// <param name="e"></param>
    private void ShowGetMedalPop(GameEvent e)
    {
        List<int> _list = (List<int>)e.data;
        UI_HomeControler.Inst.AddUI(UI_GetMedalPopMgr.UI_ResPath);
        for (int i = 0; i < _list.Count; i++)
        {
            UI_GetMedalPopMgr.QueueAdd(_list[i]);
        }
        
    }

    /// <summary>
    /// 显示新的勋章奖励New图标
    /// </summary>
    public static bool CheckNewMedalReard()
    {
        Dictionary<int, IExcelBean> _medalXmlData = DataTemplate.GetInstance().m_MedalexchangeTable.getData();
        foreach (var item in _medalXmlData)
        {
            MedalexchangeTemplate _medalData = item.Value as MedalexchangeTemplate;
            if (ObjectSelf.GetInstance().GetHandBookBoxList().Contains(_medalData.getId()) == false)
            {
                if (_medalData.getExchangeType() == 1 && ObjectSelf.GetInstance().HuangjinXZ >= _medalData.getNeedNum())
                {
                    return true;
                }
                else if (_medalData.getExchangeType() == 2 && ObjectSelf.GetInstance().BaiJinXZ >= _medalData.getNeedNum())
                {
                    return true;
                }
                else if (_medalData.getExchangeType() == 3 && ObjectSelf.GetInstance().QingTongXZ >= _medalData.getNeedNum())
                {
                    return true;
                }
                else if (_medalData.getExchangeType() == 4 && ObjectSelf.GetInstance().ChiTieXZ >= _medalData.getNeedNum())
                {
                    return true;
                }
            }
        }

        return false;
    }
    void RefreshTips()
    {
        m_MedalReardNewImg.SetActive(CheckNewMedalReard());
    }

    void OnDestroy()
    {
        base.OnDestroy();
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HB_GetSTuJianHeros, RefreshTips);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.HB_GetMedalPop, ShowGetMedalPop);
    }



}
