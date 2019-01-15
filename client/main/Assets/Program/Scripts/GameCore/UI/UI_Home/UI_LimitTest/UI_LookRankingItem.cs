using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;

public class UI_LookRankingItem : BaseUI 
{
    public int m_No;
    private List<UltimatetrialrewardTemplate> m_RankList;

    private List<GameObject> m_ReardRowList = new List<GameObject>();
    private Text m_ItemTilteText = null;


    public override void InitUIData()
    {
        base.InitUIData();
        m_ItemTilteText = selfTransform.FindChild("ItemTilteTxt").GetComponent<Text>();        
    }

    public override void InitUIView()
    {
        base.InitUIView();
        InitListData();
        InitShowUI();
    }

    /// <summary>
    /// 初始化集合数据
    /// </summary>
    private void InitListData()
    {
        switch (m_No)
        {
            case 1:
                m_RankList = UI_LookRankingOBJ.Init.m_RankItemList1;
                m_ItemTilteText.text = GameUtils.getString("ultimatetrial_content48");
                break;
            case 2:
                m_RankList = UI_LookRankingOBJ.Init.m_RankItemList2;
                m_ItemTilteText.text = GameUtils.getString("ultimatetrial_content49");
                break;
            case 3:
                m_RankList = UI_LookRankingOBJ.Init.m_RankItemList3;
                m_ItemTilteText.text = GameUtils.getString("ultimatetrial_content50");
                break;
        }

        m_ReardRowList.Clear();
        for (int i = 2; i < selfTransform.childCount; i++)
        {
            m_ReardRowList.Add(selfTransform.GetChild(i).gameObject);
        }
    }

    
    /// <summary>
    /// 显示数据
    /// </summary>
    private void InitShowUI()
    {
        for (int i = 0; i < m_RankList.Count; i++)
        {
            Transform _tran = m_ReardRowList[i].transform;
            _tran.FindChild("RankTxt1").GetComponent<Text>().text = m_RankList[i].getRankdes().ToString();            
            _tran.FindChild("ReardImg1").GetComponent<Image>().sprite = GameUtils.GetSpriteByResourceType(m_RankList[i].getReward_id()[0]);
            _tran.FindChild("ReardImg2").GetComponent<Image>().sprite = GameUtils.GetSpriteByResourceType(m_RankList[i].getReward_id()[1]);
            _tran.FindChild("ReardImg1/ReardCountTxt1").GetComponent<Text>().text = (m_RankList[i].getReward_num()[0]).ToString();
            _tran.FindChild("ReardImg2/ReardCountTxt2").GetComponent<Text>().text = (m_RankList[i].getReward_num()[1]).ToString();
        }
    }
}
