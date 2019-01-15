using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.Events;
using GNET;
using DreamFaction.GameCore;

public class HeroCellItem : CellItem
{
    private ObjectCard m_HeroCard;   // 卡牌数据
    private Button m_SelfBtn;        // 自身按钮
    private Text m_Level;            // 等级
    private GameObject m_BrightStar;  // 星级
    private Image m_Icon;            // 英雄图标
    private Image m_IconBg;            // 英雄图标
    private Image m_SelectBg;           // 选择 icon bg

    public override void InitUIData ()
    {
        base.InitUIData ();
        m_SelfBtn = selfTransform.GetComponent<Button> ();
        m_Level = selfTransform.Find ( "Parent/Level/Number" ).GetComponent<Text> ();
        m_Icon = selfTransform.Find ( "Parent/HeroIcon" ).GetComponent<Image> ();
        m_IconBg = selfTransform.Find ( "Parent/HeroBg" ).GetComponent<Image> ();
        m_BrightStar = selfTransform.Find ( "Parent/HeroStar/BrightStar" ).gameObject;
        m_SelectBg = selfTransform.Find ( "Parent/SelectBg" ).GetComponent<Image> ();
        m_SelectBg.gameObject.SetActive ( false );
    }

    public void ShowHeroT(int id,ObjectCard prevCard)
    {
        HeroTemplate heroT = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(id);
        ObjectCard card = new ObjectCard();
        Hero hero = new Hero();
        hero.heroid = id;
        hero.heropinji = prevCard.GetHeroData().QualityLev +1;
        hero.herojinjiestar = prevCard.GetHeroData().StarLevel;
        hero.heroviewid = heroT.getArtresources();
        hero.herolevel = prevCard.GetHeroData().Level;
        hero.heroskill = "";
        hero.heromishu = "";
        hero.heropeiyang = "";
        hero.heroequip = "";
        card.GetHeroData().Init(hero);

        UpdateHeroShow(card);
    }

    // 更新英雄显示信息
    public void UpdateHeroShow ( ObjectCard heroCard )
    {
        m_HeroCard = heroCard;

        // 等级
        m_Level.text = /*"Lv." +*/ "<color=yellow>" + heroCard.GetHeroData().Level.ToString() + "</color>";

        // 星级
        int star = heroCard.GetHeroData ().StarLevel;
        for ( int i = 0; i < m_BrightStar.transform.childCount; ++i )
        {
            m_BrightStar.transform.GetChild ( i ).gameObject.SetActive ( i < star );
        }

        // icon
        ArtresourceTemplate artresourcedata = ( ArtresourceTemplate ) DataTemplate.GetInstance ().m_ArtresourceTable.getTableData ( heroCard.GetHeroData ().GetHeroViewID () );
        Sprite img = UIResourceMgr.LoadSprite ( common.defaultPath
        + artresourcedata.getHeadiconresource());
        m_Icon.sprite = img;
        m_Icon.SetNativeSize ();


        // icon bg TODO...
        m_IconBg.sprite = InterfaceControler.GetInst().ReturnHeroQuailty(heroCard.GetHeroData().QualityLev);

    }

    public void SetSelectState ( bool state )
    {
        m_SelectBg.gameObject.SetActive ( state );
    }

    public void SetClickHeroIcon ()
    {
        // 按钮回调
        m_SelfBtn.onClick.RemoveAllListeners ();
        m_SelfBtn.onClick.AddListener ( OnClickHeroIcon );
    }

    public void SetClickItemIcon ()
    {
        // 按钮回调
        m_SelfBtn.onClick.RemoveAllListeners ();
        m_SelfBtn.onClick.AddListener ( OnClickItemIcon );
    }

    public void OnClickHeroIcon ()
    {
        HeroStrengthen.Inst.OnClickHeroIcon ( m_HeroCard );
    }

    public void OnClickItemIcon ()
    {
        ItemStrengthen.Inst.OnClickItemIcon ( m_HeroCard );
    }


    public void SetSelfOnClick ( UnityAction action )
    {
        m_SelfBtn.onClick.RemoveAllListeners ();
        m_SelfBtn.onClick.AddListener ( action );
    }

}
