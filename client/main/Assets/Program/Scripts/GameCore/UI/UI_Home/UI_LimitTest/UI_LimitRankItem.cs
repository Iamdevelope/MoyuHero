using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using System.Collections.Generic;

public class UI_LimitRankItem : UI_LimitRankItemBase 
{
    public long m_RoleId = 0;           // 玩家guid
    public string m_RoleName = "";      // 玩家名称
    public int m_Level = 0;             // 玩家等级
    public int m_GroupNum = 0;          // 挑战关数
    public int m_TroopType = 0;         // 战队类型
    public int m_AlldropNum = 0;        // 勇者证明总数量
    public int m_OnRankNum = 0;         // 连续在榜次数
    public Dictionary<int, OtherHeroInfo> m_HeroAttribute = null;   //玩家编队

    private GameObject m_InfoStateObj = null;
    private GameObject m_NullInfoStateObj = null;
    public override void InitUIData()
    {
        base.InitUIData();
        m_InfoStateObj = selfTransform.FindChild("InfoState").gameObject;
        m_NullInfoStateObj = selfTransform.FindChild("NullInfoState").gameObject;
    }

    public override void InitUIView()
    {
        base.InitUIView();
        
    }


    /// <summary>
    /// 显示数据
    /// </summary>
    public void ShowRankItemData()
    {
        SetActiveObj(true);

        m_PlayerNameTxt.text = m_RoleName;
        m_LevelNumTxt.text = m_GroupNum.ToString();
        m_BraveStaTxt.text = m_AlldropNum.ToString();
        m_LevelTxt.text = m_Level.ToString();
        string _text = string.Format(GameUtils.getString("ultimatetrial_content44"), m_OnRankNum);
        m_InListTimeTxt.text = _text;
    }

    /// <summary>
    /// 设置显示隐藏 隐藏没有数据的OBJ 
    /// </summary>
    public void SetActiveObj(bool active)
    {
        m_InfoStateObj.SetActive(active);
        m_NullInfoStateObj.SetActive(!active);
    }



    /// <summary>
    /// 查看阵容按钮
    /// </summary>
    protected override void OnClickBattleBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_LookForm.UI_ResPath);
        UI_LookForm.Inst.SetTroopType(m_TroopType);
        UI_LookForm.Inst.SetHeroAttribute(m_HeroAttribute);
        UI_LookForm.Inst.InitTeamShow();
    }

}
