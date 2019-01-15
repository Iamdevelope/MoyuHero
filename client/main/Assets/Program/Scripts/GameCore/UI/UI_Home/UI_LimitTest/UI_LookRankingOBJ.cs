using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_LookRankingOBJ : BaseUI 
{
    public static UI_LookRankingOBJ Init = null;
    private List<UltimatetrialrewardTemplate> m_AllRankItemList = new List<UltimatetrialrewardTemplate>();
    public List<UltimatetrialrewardTemplate> m_RankItemList1 = new List<UltimatetrialrewardTemplate>();
    public List<UltimatetrialrewardTemplate> m_RankItemList2 = new List<UltimatetrialrewardTemplate>();
    public List<UltimatetrialrewardTemplate> m_RankItemList3 = new List<UltimatetrialrewardTemplate>();
    private Button m_OKBtn;      
                                                                              //确定按钮

    public override void InitUIData()
    {
        base.InitUIData();
        Init = this;
        InitParseXmlData();
        InitDataList();

        m_OKBtn = selfTransform.FindChild("lookRankingWindow/OKBtn").GetComponent<Button>();
        m_OKBtn.onClick.AddListener(new UnityAction(OnTestRankingObjClose));
    }
    void OnEnable()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(UI_LimitTest.Inst.m_cappostion);
    }
    void OnDisable()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(UI_LimitTest.Inst.m_cappostion);
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    /// <summary>
    /// 初始化解析Xml数据
    /// </summary>
    private void InitParseXmlData()
    {        
        Dictionary<int, IExcelBean> _lookRankXmlData = DataTemplate.GetInstance().m_UltimatetrialrewardTable.getData();
        foreach (var item in _lookRankXmlData)
        {
            UltimatetrialrewardTemplate _lookRankData = item.Value as UltimatetrialrewardTemplate;
            m_AllRankItemList.Add(_lookRankData);
        }
    }

    /// <summary>
    /// 初始化列表数据
    /// </summary>
    private void InitDataList()
    {
        for (int i = 0; i < m_AllRankItemList.Count; i++)
        {
            string _idStr = m_AllRankItemList[i].getId().ToString();
            string _one = _idStr.Substring(0,1);
            if (int.Parse(_one) == 1)
                m_RankItemList1.Add(m_AllRankItemList[i]);
            if (int.Parse(_one) == 2)
                m_RankItemList2.Add(m_AllRankItemList[i]);
            if (int.Parse(_one) == 3)
                m_RankItemList3.Add(m_AllRankItemList[i]);
        }
    }



    /// <summary>
    /// 关闭查看界面按钮
    /// </summary>
    private void OnTestRankingObjClose()
    {
        gameObject.SetActive(false);
    }



}


