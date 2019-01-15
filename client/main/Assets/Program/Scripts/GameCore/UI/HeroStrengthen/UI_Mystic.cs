using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using DreamFaction.GameNetWork.Data;
using DreamFaction.UI;
using DreamFaction.UI.Core;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;

public class UI_Mystic : UI_MysticBase
{
    private HeroData m_HeroData;
    private ObjectCard m_ObjectCard;


    public override void InitUIView()
    {
        base.InitUIView();
        m_TipInfoText.text = GameUtils.getString("ui_yingxiongqianghua_mishu2");
    }

    public override void ShowHeroInfo(ObjectCard _objectCard)
    {
        m_ObjectCard = _objectCard;
        m_HeroData = _objectCard.GetHeroData();
        GreatMysticIcon();
    }

    /// <summary>
    /// 初始化填充秘术界面数据
    /// </summary>
    private void GreatMysticIcon()
    {

        for (int i = 0; i < m_HeroData.HeroCabalaDB.CabalaList.Count - 1; i++)
        {
            MsTemplate MysticDataT = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(m_HeroData.HeroCabalaDB.CabalaList[i].TableID);
            m_NameList[i].text = GameUtils.getString(MysticDataT.getMsname());
            m_SpritelList[i].sprite = UIResourceMgr.LoadSprite(common.defaultPath + MysticDataT.getIcon());
            m_LevelList[i].text = "Lv." + "<color=yellow>" + m_HeroData.HeroCabalaDB.CabalaList[i].IntensifyLev.ToString() + "</color>";
            m_InfoList[i].text = GameUtils.getString(MysticDataT.getLowdes());

            if (m_HeroData.StarLevel < MysticDataT.getStardemand())
            {
                m_Info_2_List[i].text =  GameUtils.getString("ui_yingxiongqianghua_mishu3").Replace("{0}", MysticDataT.getStardemand().ToString()).Replace("{1}", MysticDataT.getStagedemand().ToString());
                m_LocklList[i].SetActive(true);
               // m_LevelList[i].gameObject.SetActive(false);
                m_SpritelList[i].gameObject.GetComponent<Button>().enabled = false;
            }
            else
            {
                if (m_HeroData.CurStage < MysticDataT.getStagedemand())
                {
                    m_Info_2_List[i].text =  GameUtils.getString("ui_yingxiongqianghua_mishu3").Replace("{0}", MysticDataT.getStardemand().ToString()).Replace("{1}", MysticDataT.getStagedemand().ToString());
                    m_LocklList[i].SetActive(true);
                    //m_LevelList[i].gameObject.SetActive(false);
                    m_SpritelList[i].gameObject.GetComponent<Button>().enabled = false;
                }
                else
                {
                    m_LocklList[i].SetActive(false);
                    m_LevelList[i].gameObject.SetActive(true);
                    m_SpritelList[i].gameObject.GetComponent<Button>().enabled = true;
                    if (m_HeroData.HeroCabalaDB.CabalaList[i].IntensifyLev == 0)
                    {
                        m_Info_2_List[i].text = "+" + 0;
                    }
                    else
                    {
                        int index = m_HeroData.HeroCabalaDB.CabalaList[i].IntensifyLev - 1;
                        m_Info_2_List[i].text = "+" + MysticDataT.getConsumexpevalue()[index];
                    }
                }
            }
        }

    }

    /// <summary>
    /// 点击的秘籍ID 最上面的为0 顺时针转 
    /// </summary>
    /// <param name="mysticStyle"></param>
    /// 
    private void PopMysticWindow(int mysticId, ObjectCard m_ObjectCard)
    {
        GameObject go =  UI_HomeControler.Inst.AddUI(UI_MysticPopWindow.UI_ResPath);
        UI_MysticPopWindow _UI_MysticPopWindow = go.GetComponent<UI_MysticPopWindow>();
        if (_UI_MysticPopWindow == null)
        {
            _UI_MysticPopWindow = go.gameObject.AddComponent<UI_MysticPopWindow>();
        }
        go.SetActive(true);
        _UI_MysticPopWindow.PopMysticWindow(mysticId, m_ObjectCard);
    }


    protected override void OnClick_0_Button()
    {
        PopMysticWindow(0, m_ObjectCard);
    }
    protected override void OnClick_1_Button()
    {
        PopMysticWindow(1, m_ObjectCard);
    }
    protected override void OnClick_2_Button()
    {
        PopMysticWindow(2, m_ObjectCard);
    }
    protected override void OnClick_3_Button()
    {
        PopMysticWindow(3, m_ObjectCard);
    }
    protected override void OnClick_4_Button()
    {
        PopMysticWindow(4, m_ObjectCard);
    }
    protected override void OnClick_5_Button()
    {
        PopMysticWindow(5, m_ObjectCard);
    }
}




