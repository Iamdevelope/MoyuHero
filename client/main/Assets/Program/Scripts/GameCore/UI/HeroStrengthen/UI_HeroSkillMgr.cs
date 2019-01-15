using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using System.Text;

public class UI_HeroSkillMgr : HeroAttrPanel
{
    public static string UI_ResPath = "HeroStrengthen/UI_HeroSkill";

    private ObjectCard m_Card = null;
    private HeroTemplate m_HeroT = null;
    private List<int> m_SkillIdList = null;
    private GameObject m_Prefab = null;
    private Transform m_GridTrans = null;
    private Button m_SkillPointAddBtn = null;
    private Text m_SkillPointsTxt = null;

    public override void InitUIData()
    {
        base.InitUIData();
        m_GridTrans = selfTransform.FindChild("ScrollRect/LayoutList");
        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "HeroStrengthen/UI_SkillItem") as GameObject;
        m_SkillPointsTxt = selfTransform.FindChild("Increaseskills/Text_Skillpoints").GetComponent<Text>();
        m_SkillPointAddBtn = selfTransform.FindChild("Increaseskills/Btn_ Plussign").GetComponent<Button>();
        m_SkillPointAddBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onSkillPointBtnAddClick));

/*        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SkillPoint_Update, ShowSkillPointTxt);*/
    }

    private void ShowSkillPointTxt()
    {
        string str = "";
        int time = ObjectSelf.GetInstance().SkillPointRefreshTime;
        if (ObjectSelf.GetInstance().SkillPoint >= ObjectSelf.GetInstance().SkillPointMax)
        {
            str = string.Format(GameUtils.getString("ui_yingxiongqianghua_jineng6"), ObjectSelf.GetInstance().SkillPointMax);
        }
        else
        {
            if (time >= 0)
            {
                int minute = time / 60;
                int second = time % 60;
                string minuteStr = minute <= 9 ? "0" + minute : minute.ToString();
                string secondStr = second <= 9 ? "0" + second : second.ToString();
                StringBuilder timeStr = new StringBuilder();
                timeStr.Append(minuteStr);
                timeStr.Append(":");
                timeStr.Append(secondStr);
                str = string.Format(GameUtils.getString("ui_yingxiongqianghua_jineng7"), ObjectSelf.GetInstance().SkillPoint, timeStr.ToString());
            }
        }
        m_SkillPointsTxt.text = str;
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();

        ShowSkillPointTxt();
    }

    public override void ShowHeroInfo(ObjectCard heroCard)
    {
        base.ShowHeroInfo(heroCard);

        m_Card = heroCard;
        m_HeroT = heroCard.GetHeroRow();

        CreateSkillItem();
/*        ShowSkillPointTxt();*/
    }

    private void CreateSkillItem()
    {
        m_SkillIdList = m_Card.GetHeroData().HeroSkillDB.SkillList;
        ClearGridChild();
        for (int i = 0; i < m_SkillIdList.Count; i++)
        {
            GameObject go = Instantiate(m_Prefab) as GameObject;
            go.transform.parent = m_GridTrans;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            UI_SkillItem uiSkillItem = null;

            if (go.GetComponent<UI_SkillItem>() != null)
                uiSkillItem = go.GetComponent<UI_SkillItem>();
            else
                uiSkillItem = go.AddComponent<UI_SkillItem>();

            SkillTemplate skillT = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(m_SkillIdList[i]);

            uiSkillItem.SetOpenState(m_Card.GetHeroData().QualityLev > i);
            uiSkillItem.ShowSkillData(skillT, m_Card, i + 1);
        }
    }


    private void ClearGridChild()
    {
        for (int i = 0; i < m_GridTrans.childCount; i++)
        {
            Destroy(m_GridTrans.GetChild(i).gameObject);
        }
    }

    private void onSkillPointBtnAddClick()
    {
        if (ObjectSelf.GetInstance().SkillPoint >= ObjectSelf.GetInstance().SkillPointMax)
            return;

        VipTemplate vipT = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(ObjectSelf.GetInstance().VipLevel);
        UICommonManager.Inst.ShowMsgBox("购买技能点", string.Format("你是VIP1，今天还可以购买<color=#ff0000>{0}</color>次", vipT.getSkillconlimit()),
                                        string.Format("购买<color=#ff0000>{0}</color>技能点，需花费;<image res ='Sprites/zuanshi' height='40' width='40'/>;{1}", 20, 50),
                                        "购买", ResetOKBtn, null, null);
    }

    private void ResetOKBtn(object data)
    {

    }

    void OnDestroy()
    {
/*        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SkillPoint_Update, ShowSkillPointTxt);*/
    }


}
