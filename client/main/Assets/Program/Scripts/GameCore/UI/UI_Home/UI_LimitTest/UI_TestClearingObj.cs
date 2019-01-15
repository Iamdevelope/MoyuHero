using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.GameCore;

public class UI_TestClearingObj : UI_TestClearingObjBase
{
    public static string UI_ResPath = "UI_Home/UI_TestClearingObj_1_1";
    private ObjectSelf m_Self = null;
    //private GameObject m_Prefab = null;
    private Transform m_Grid = null;
    private UniversalItemCell m_Cell = null;


    public override void InitUIData()
    {
        base.InitUIData();
        //m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Home/CosItem") as GameObject;
        m_Grid = selfTransform.FindChild("Grid");        
    }
    public override void InitUIView()
    {
        base.InitUIView();
        m_Self = ObjectSelf.GetInstance();
        ShowTiliteText();
        FightClearingValue();
    }

    /// <summary>
    /// 显示标题文本
    /// </summary>
    private void ShowTiliteText()
    {
        m_TiliteTxt.text = GameUtils.getString("ultimatetrial_content30");//试炼结束文本      
        m_TestRewaedTxt.text = GameUtils.getString("ultimatetrial_content35");
        
    }
    /// <summary>
    /// 显示获得的值文本
    /// </summary>
    private void FightClearingValue()
    {
        string _fightGetTxt = string.Format(GameUtils.getString("ultimatetrial_content32"),m_Self.LimitFightMgr.m_AllDropNum);
        m_FightGetTxt.ShowRichText(_fightGetTxt);//试炼获得勇者证明
        string _desTxt = string.Format(GameUtils.getString("ultimatetrial_content31"),m_Self.LimitFightMgr.m_RoundNum);
        m_DesTxt.text = _desTxt;//试炼描述
        if (m_Self.LimitFightMgr.m_PactIspass == 1)
        {
            m_PowerTxt.text = GameUtils.getString("ultimatetrial_content33");
        }
        else
        {
            m_PowerTxt.text = GameUtils.getString("ultimatetrial_content34");
        }

        //初始化显示获得奖励物品
        if (m_Self.LimitFightMgr.m_DropMap.Count > 0)
        {
            foreach (DictionaryEntry item in m_Self.LimitFightMgr.m_DropMap)
            {
                m_Cell = UniversalItemCell.GenerateItem(m_Grid);
                m_Cell.InitByID((int)item.Key, (int)item.Value);
                
                //GameObject _go = Instantiate(m_Prefab) as GameObject;
                //_go.transform.parent = m_Grid;
                //_go.transform.localPosition = Vector3.zero;
                //_go.transform.localScale = Vector3.one;
                //_go.transform.FindChild("CosIcon").GetComponent<Image>().sprite = GameUtils.GetSpriteByResourceType((int)item.Key);
                //_go.transform.FindChild("Image/CosCountTxt1").GetComponent<Text>().text = item.Value.ToString();
            }
        }



        
    }


    /// <summary>
    /// 关闭按钮
    /// </summary>
    protected override void OnClickCloseBtn()
    {
        if (UI_HomeControler.Inst != null)
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
        else
        {
            UI_FightControler.Inst.ReMoveUI(gameObject);
            SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
            ObjectSelf.GetInstance().isLimitWindow = true;
        }
        ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter = false;
    }
}
