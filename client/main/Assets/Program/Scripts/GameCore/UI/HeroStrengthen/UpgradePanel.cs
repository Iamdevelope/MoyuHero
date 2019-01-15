using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using System.Collections;
using System;

public class UpgradePanel : HeroAttrPanel
{
    protected Text m_Title;
    protected Text m_Value;
    protected Button m_UpFive;
    protected Text m_UpFiveText;
    protected Button m_UpOne;
    protected Text m_UpOneText;
    protected Text m_Number;
    protected Text m_5Number;
    protected Slider m_Slider;
    protected Text m_ExpTxt;

    private HeroaddstageTemplate m_CurTData;
    private HeroaddstageTemplate m_NextTData;
    private HeroData m_HeroData;
    private HeroTemplate m_HeroDataT;

    protected GameObject m_ItemLayout;
    protected GameObject m_AtrributesLayout;

    public override void InitUIData ()
    {
        base.InitUIData ();
        m_Title = selfTransform.FindChild ( "LevelObj/Title" ).GetComponent<Text> ();
        m_Value = selfTransform.FindChild ( "LevelObj/Value" ).GetComponent<Text> ();
        m_UpFive = selfTransform.FindChild ( "UpFive" ).GetComponent<Button> ();
        m_UpFive.onClick.AddListener ( OnClickUpFive );
        m_UpFiveText = selfTransform.FindChild ( "UpFive/UpFiveText" ).GetComponent<Text> ();
        m_UpOne = selfTransform.FindChild ( "UpOne" ).GetComponent<Button> ();
        m_UpOne.onClick.AddListener ( OnClickUpOne );
        m_UpOneText = selfTransform.FindChild ( "UpOne/UpOneText" ).GetComponent<Text> ();
        m_Number = selfTransform.FindChild ( "UpOne/Number" ).GetComponent<Text> ();
        m_5Number = selfTransform.FindChild("UpFive/Number").GetComponent<Text>();
        m_Slider = selfTransform.Find ( "Slider" ).GetComponent<Slider> ();
        m_ExpTxt = selfTransform.Find ( "ExpTxt" ).GetComponent<Text> ();

        m_ItemLayout = selfTransform.Find ( "ItemList/ItemLayout" ).gameObject;
        m_AtrributesLayout = selfTransform.Find("AttributesList/StartLayout").gameObject;

        GameEventDispatcher.Inst.addEventListener ( GameEventID.HE_HeroLevelUpSucceed, UpgradeSuccess );
        GameEventDispatcher.Inst.addEventListener ( GameEventID.KE_ModItemNum, OnBuyItemSuccess );
        GameEventDispatcher.Inst.addEventListener ( GameEventID.KE_KnapsackAdd, OnBuyItemSuccess );
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    void UpgradeSuccess ()
    {
        UI_HomeControler.Inst.AddUI ( UpgradePop.UI_ResPath );
    }

    public void OnBuyItemSuccess ()
    {
        ShowHeroInfo ( HeroStrengthen.Inst.m_CurCard );
    }


    // 重写显示英雄信息方法
    public override void ShowHeroInfo ( ObjectCard heroCard )
    {
        base.ShowHeroInfo ( heroCard );
        m_HeroData = heroCard.GetHeroData();
        m_HeroDataT = heroCard.GetHeroRow();
        HeroupgradexpTemplate tempT = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level);
        // 英雄等级
        m_Value.text = heroCard.GetHeroData().Level.ToString();// +"/" + heroCard.GetHeroRow().getMaxLevel().ToString();
        // 进度条
        m_Slider.value = heroCard.GetHeroData().Exp * 1.0f / tempT.getExp(); //DataTemplate.GetInstance ().GetHeroAllExp ( heroCard );

        m_ExpTxt.text = heroCard.GetHeroData().Exp + "/" + tempT.getExp(); //DataTemplate.GetInstance ().GetHeroAllExp ( heroCard );

        int id1 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_1();
        int number1 = 0;
        ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id1, ref number1);
        ItemTemplate item1 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id1);
        m_ItemLayout.transform.GetChild(0).GetComponent<PotionItem>().ShowInfo(number1, item1);
        m_ItemLayout.transform.GetChild(0).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id1);

        int id2 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_2();
        int number2 = 0;
        ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id2, ref number2);
        ItemTemplate item2 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id2);
        m_ItemLayout.transform.GetChild(1).GetComponent<PotionItem>().ShowInfo(number2, item2);
        m_ItemLayout.transform.GetChild(1).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id2);

        int id3 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_3();
        int number3 = 0;
        ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id3, ref number3);
        ItemTemplate item3 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id3);
        m_ItemLayout.transform.GetChild(2).GetComponent<PotionItem>().ShowInfo(number3, item3);
        m_ItemLayout.transform.GetChild(2).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id3);

        int id4 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_4();
        int number4 = 0;
        ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id4, ref number4);
        ItemTemplate item4 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id4);
        m_ItemLayout.transform.GetChild(3).GetComponent<PotionItem>().ShowInfo(number4, item4);
        m_ItemLayout.transform.GetChild(3).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id4);

        //int id5 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_5();
        //int number5 = 0;
        //ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id5, ref number5);
        //ItemTemplate item5 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id5);
        //m_ItemLayout.transform.GetChild(4).GetComponent<PotionItem>().ShowInfo(number5, item5);
        //m_ItemLayout.transform.GetChild(4).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id5);

        //int id6 = DataTemplate.GetInstance().m_GameConfig.getItem_exp_6();
        //int number6 = 0;
        //ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id6, ref number6);
        //ItemTemplate item6 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(id6);
        //m_ItemLayout.transform.GetChild(5).GetComponent<PotionItem>().ShowInfo(number6, item6);
        //m_ItemLayout.transform.GetChild(5).Find("Image1").GetComponent<Image>().sprite = GameUtils.GetItemQualitySprite(id6);

        if (heroCard.GetHeroData().Level == 60)
        {
            m_Number.text = "";
            m_ExpTxt.text = "";
        }
        else
        {
            HeroupgradexpTemplate temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level);
            HeroupgradexpTemplate _temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level + 1);
            m_Number.text = (_temp.getConsumermoney() - temp.getConsumermoney()).ToString();
        }

        if (heroCard.GetHeroData().Level + 5 > 59)
        {
            HeroupgradexpTemplate temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), 59);
            HeroupgradexpTemplate _temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level);
            m_5Number.text = (temp.getConsumermoney() - _temp.getConsumermoney()).ToString();
        }
        else
        {
            HeroupgradexpTemplate _temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level);
            HeroupgradexpTemplate temp = DataTemplate.GetInstance().GetHeroupgradexpTemplate(heroCard.GetHeroRow().getBorn(), heroCard.GetHeroData().Level +5 );
            m_5Number.text = (temp.getConsumermoney() - _temp.getConsumermoney() ).ToString();
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                string type = "战斗力";//GameUtils.GetAttriName(1);
                float num = 0;
                GameConfig _cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        num += _cofig.getCombat_attack_factor() * m_HeroDataT.getHPGrowth();
                    if (j == 1)
                        num += _cofig.getCombat_defense_factor() * m_HeroDataT.getPhysicalAttackGrowth();
                    if (j == 2)
                        num += _cofig.getCombat_blood_factor() * m_HeroDataT.getPhysicalDefenceGrowth();
                }
                m_AtrributesLayout.transform.GetChild(i).GetComponent<AttriItems>().SetInfo(m_AtrributesLayout.transform.GetChild(i).transform, type, Math.Floor(num).ToString());
            }
            if (i == 1)
            {
                string type = GameUtils.GetAttriName(1);
                m_AtrributesLayout.transform.GetChild(i).GetComponent<AttriItems>().SetInfo(m_AtrributesLayout.transform.GetChild(i).transform, type, m_HeroDataT.getHPGrowth().ToString());
            }
            if (i == 2)
            {
                string type = GameUtils.GetAttriName(3);
                m_AtrributesLayout.transform.GetChild(i).GetComponent<AttriItems>().SetInfo(m_AtrributesLayout.transform.GetChild(i).transform, type, m_HeroDataT.getPhysicalAttackGrowth().ToString());
            }
            if (i == 3)
            {
                string type = GameUtils.GetAttriName(5);
                m_AtrributesLayout.transform.GetChild(i).GetComponent<AttriItems>().SetInfo(m_AtrributesLayout.transform.GetChild(i).transform, type, m_HeroDataT.getPhysicalDefenceGrowth().ToString());
            }
        }

    }

    private int GetIdInBagNum ( int id )
    {
        int haveNum = -1;
        ObjectSelf.GetInstance ().TryGetItemCountById ( EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, id, ref haveNum );
        return haveNum;
    }

    protected virtual void OnClickUpFive ()
    {
        if ( !GetResEnough () )
        {
            InterfaceControler.GetInst ().AddMsgBox ( "升级材料不足" );
            return;
        }

        if ( ObjectSelf.GetInstance ().Level <= m_HeroData.Level )
        {
            InterfaceControler.GetInst ().AddMsgBox ( "玩家等级不足" );
            return;
        }


        CHeroLevelUpSpeed proto = new CHeroLevelUpSpeed ();
        proto.herokey = ( int ) HeroStrengthen.Inst.m_CurCard.GetGuid ().GUID_value;
        proto.levelnum = 5;

        IOControler.GetInstance ().SendProtocol ( proto );
    }

    protected virtual void OnClickUpOne ()
    {
        if ( !GetResEnough () )
        {
            InterfaceControler.GetInst ().AddMsgBox ( "升级材料不足" );
            return;
        }

        if ( ObjectSelf.GetInstance ().Level <= m_HeroData.Level )
        {
            InterfaceControler.GetInst ().AddMsgBox ( "玩家等级不足" );
            return;
        }


        CHeroLevelUpSpeed proto = new CHeroLevelUpSpeed ();
        proto.herokey = ( int ) HeroStrengthen.Inst.m_CurCard.GetGuid ().GUID_value;
        proto.levelnum = 1;

        IOControler.GetInstance ().SendProtocol ( proto );
    }

    bool GetResEnough ()
    {
        int num1 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_1 () );
        if ( num1 > 0 )
        {
            return true;
        }

        int num2 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_2 () );
        if ( num2 > 0 )
        {
            return true;
        }

        int num3 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_3 () );
        if ( num3 > 0 )
        {
            return true;
        }

        int num4 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_4 () );
        if ( num4 > 0 )
        {
            return true;
        }

        int num5 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_5 () );
        if ( num5 > 0 )
        {
            return true;
        }

        int num6 = GetIdInBagNum ( DataTemplate.GetInstance ().m_GameConfig.getItem_exp_6 () );
        if ( num6 > 0 )
        {
            return true;
        }

        return false;
    }

    public void OnDestroy ()
    {
        Destroy ( m_Title );
        Destroy ( m_Value );
        Destroy ( m_UpFive );
        Destroy ( m_UpFiveText );
        Destroy ( m_UpOne );
        Destroy ( m_UpOneText );
        Destroy ( m_Number );

        GameEventDispatcher.Inst.removeEventListener ( GameEventID.HE_HeroLevelUpSucceed, UpgradeSuccess );
        GameEventDispatcher.Inst.removeEventListener ( GameEventID.KE_ModItemNum, OnBuyItemSuccess );
        GameEventDispatcher.Inst.removeEventListener ( GameEventID.KE_KnapsackAdd, OnBuyItemSuccess );
    }
}
