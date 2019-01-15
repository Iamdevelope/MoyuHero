using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameCore;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameEventSystem;
/// <summary>
/// 英雄强化--升品UI
/// </summary>
public class UI_QualityProMgr : HeroAttrPanel
{
    public static string UI_ResPath = "HeroStrengthen/UI_QualityProUI";

    private ObjectCard m_Card = null;
    private HeroTemplate m_HeroT = null;

    private ObjectCard m_PrevCard = null;

    private Image m_GoldImg = null;
    private Text m_GoldTxt = null;
    private Text m_QualityProBtnTxt = null;
    private Text m_QualityPvwBtnTxt = null;
    private Button m_QualityProBtn = null;
    private Button m_QualityPvwBtn = null;
    private Button m_HeroDebBtn = null;
    private HeroCellItem m_HeroCellItem_Now = null;
    private HeroCellItem m_HeroCellItem_Next = null;
    private GameObject m_MaxQualityOBJ = null;
    private GameObject m_QualityObj = null;

    private Text m_DebTxt = null;
    private Image m_ConsImg = null;
    public override void InitUIData()
    {
        base.InitUIData();

        m_ConsImg = selfTransform.FindChild("QualityProOBJ/Rightinformation/Hero/Img_Icon").GetComponent<Image>();
        m_DebTxt = selfTransform.FindChild("QualityProOBJ/Rightinformation/Hero/Text_Maxmin").GetComponent<Text>();
        m_QualityObj = selfTransform.FindChild("QualityProOBJ").gameObject;
        m_MaxQualityOBJ = selfTransform.FindChild("Rightinformation2").gameObject;
        m_HeroCellItem_Now = selfTransform.FindChild("QualityProOBJ/HeroCellItem_Now").GetComponent<HeroCellItem>();
        m_HeroCellItem_Next = selfTransform.FindChild("QualityProOBJ/HeroCellItem_Next").GetComponent<HeroCellItem>();
        m_HeroDebBtn = selfTransform.FindChild("QualityProOBJ/Rightinformation/Hero").GetComponent<Button>();
        m_QualityProBtn = selfTransform.FindChild("QualityProOBJ/Btn_Advanced").GetComponent<Button>();
        m_QualityPvwBtn = selfTransform.FindChild("QualityProOBJ/Rightinformation/Btn_LproductPreview").GetComponent<Button>();
        m_QualityProBtnTxt = selfTransform.FindChild("QualityProOBJ/Btn_Advanced/Text_Advanced").GetComponent<Text>();
        m_QualityPvwBtnTxt = selfTransform.FindChild("QualityProOBJ/Rightinformation/Btn_LproductPreview/Text_LproductPreview").GetComponent<Text>();
        m_GoldTxt = selfTransform.FindChild("QualityProOBJ/Btn_Advanced/Text_Gold").GetComponent<Text>();
        m_GoldImg = selfTransform.FindChild("QualityProOBJ/Btn_Advanced/Img_Gold1").GetComponent<Image>();

        m_QualityProBtn.onClick.AddListener(new UnityAction(onQualityProBtnClick));
        m_QualityPvwBtn.onClick.AddListener(new UnityAction(onQualityPvwBtnClick));
        m_HeroDebBtn.onClick.AddListener(new UnityAction(onHeroDebBtnClick));

        GameEventDispatcher.Inst.addEventListener(GameEventID.HE_BeginnerUp, ShowQualityUpWin);
    }

    public override void ShowHeroInfo(ObjectCard heroCard)
    {
        base.ShowHeroInfo(heroCard);

        m_Card = heroCard;
        m_HeroT = heroCard.GetHeroRow();

        bool isGetStageUpTarget = m_HeroT.getStageUpTargetID() != -1;

        m_QualityObj.SetActive(isGetStageUpTarget);
        m_MaxQualityOBJ.SetActive(!isGetStageUpTarget);
        if (isGetStageUpTarget)
        {            
            ShowHeroIcon();
            ShowConsUI();
        }
    }

    /// <summary>
    /// 显示升品的英雄Icon
    /// </summary>
    private void ShowHeroIcon()
    {
        m_HeroCellItem_Now.UpdateHeroShow(m_Card);
        m_HeroCellItem_Next.ShowHeroT(m_HeroT.getStageUpTargetID(),m_Card);
    }

    /// <summary>
    /// 显示消耗物品UI
    /// </summary>
    private void ShowConsUI()
    {
        m_GoldTxt.text = m_HeroT.getGold().ToString();

        if (m_HeroT.getStuff() <= 0)
            return;

        ItemTemplate itemT = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(m_HeroT.getStuff());
        int count = InterfaceControler.GetInst().ReturnItemCount(m_HeroT.getStuff());
        string countStr = count > 0 ? count.ToString() : "<color=red>" + count + "</color>";
        string str = string.Format("{0}/{1}", countStr, m_HeroT.getNumbers());
        m_DebTxt.text = str;
        m_ConsImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemT.getIcon());
    }

    //英雄碎片Icon 按钮回调
    private void onHeroDebBtnClick()
    {
        if (m_HeroT.getStuff() <= 0)
            return;

        UICommonManager.Inst.ShowHeroObtain(m_HeroT.getStuff());
    }


    //升品按钮回调
    private void onQualityProBtnClick()
    {
        m_PrevCard = new ObjectCard();
        m_PrevCard.GetHeroData().Copy(this.m_Card.GetHeroData());

        if (ObjectSelf.GetInstance().Money >= m_HeroT.getGold() &&
            m_HeroT.getStageUpTargetID() != -1 &&
            m_HeroT.getStuff() > 0 &&
            InterfaceControler.GetInst().ReturnItemCount(m_HeroT.getStuff()) >= m_HeroT.getNumbers())
        {
            CStarUpHero csh = new CStarUpHero();
            csh.herokey = (int)m_Card.GetGuid().GUID_value;
            IOControler.GetInstance().SendProtocol(csh);
        }
        else
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("ui_yingxiongqianghua_shengji6"), transform);
        }
    }

    //升品预览按钮回调
    private void onQualityPvwBtnClick()
    {
        if (m_HeroT.getStageUpTargetID() != -1)
        {
            GameObject go = UI_HomeControler.Inst.AddUI(UI_QualityPvwMgr.UI_ResPath);
            UI_QualityPvwMgr uiQualityPvwMgr = go.GetComponent<UI_QualityPvwMgr>();
            uiQualityPvwMgr.ShowUIData(m_Card);
        }
    }

    /// <summary>
    /// 显示升品成功弹窗
    /// </summary>
    private void ShowQualityUpWin()
    {
        GameObject go = UI_HomeControler.Inst.AddUI(UI_QualityProWinMgr.UI_ResPath);
        UI_QualityProWinMgr uiQualityProWinMgr = go.GetComponent<UI_QualityProWinMgr>();
        uiQualityProWinMgr.ShowHeroItem(m_PrevCard);
    }


    void OnDestroy()
    {

        GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_BeginnerUp, ShowQualityUpWin);
    }
    
    
}
