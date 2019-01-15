using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.IO;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;
using DreamFaction.Utils;

public class DataTemplate
{
    public Dictionary<String, TableReader> m_TableList = new Dictionary<String, TableReader> ();
    Dictionary<String, Type> m_TableTypeList = new Dictionary<String, Type> ();
    public TableReader m_HeroTable;
    public TableReader m_BufferTable;
    public TableReader m_SkillTable;
    public TableReader m_BuffGroupTable;
    public TableReader m_HeroExpTable;
    public TableReader m_HeroFuryTable;
    public TableReader m_InterfacemodelTable;
    public TableReader m_LevelamendmentTable;
    public TableReader m_MonsterGroupTable;
    public TableReader m_MonsterTable;
    public TableReader m_PlayerExpTable;
    public TableReader m_SkillaiTable;
    public TableReader m_SkillupcostTable;
    public TableReader m_StageTable;
    public TableReader m_ChsTextTable;
    public TableReader m_ArtresourceTable;
    public TableReader m_AnimEventTable;
    public GameConfig m_GameConfig;
    public TableReader m_VipTable;
    public TableReader m_RuneTable;
    public TableReader m_ItemTable;
    public TableReader m_RunepassiveTable;
    public TableReader m_ChapterTable;
    public TableReader m_NormaldropTable;
    public TableReader m_InnerdropTable;
    public TableReader m_BossboxTable;
    public TableReader m_BaseruneattributeTable;
    public TableReader m_AddruneattributeTable;
    public TableReader m_RunecostTable;
    public TableReader m_AttributetrainTable;
    public TableReader m_ArtifactTable;
    public TableReader m_ShopTable;
    public TableReader m_RechargeTable;
    public TableReader m_HerosoundTable;
    public TableReader m_ExchangecodeTable;
    public TableReader m_IllustratehandbookTable;
    public TableReader m_MedalexchangeTable;
    public TableReader m_LoginbonusTable;
    public TableReader m_MysteriousshopTable;
    public TableReader m_MonthcardTable;
    public TableReader m_ExplorequestTable;
    public TableReader m_HerocloneTable;
    public TableReader m_RuintreasureTable;
    public TableReader m_UltimatetrialmonsterTable;
    public TableReader m_UltimatetrialrewardTable;
    public TableReader m_HeroRecruitTable;
    public TableReader m_ResourceindexTemplate;
    public TableReader m_ExchangeTable;
    public TableReader m_ActivitymissionTable;
    public TableReader m_ShieldCharacterTable;
    public TableReader m_RandnameTemplate;
    public TableReader m_NewbieguideTable;
    public TableReader m_LegendexchargeTable;
    public TableReader m_BroadCastTable;
    public TableReader m_PropsacessTable;
    public TableReader m_PropsjumpuiTable;
    public TableReader m_GameactivityTable;
    public TableReader m_CaptionTable;
    public TableReader m_HeroaddstageTable;
    public TableReader m_AngerTable;
    public TableReader m_HerocultureTable;
    public TableReader m_MsTable;
    public TableReader m_EquipmentqualityTable;
    public TableReader m_EquipmentstrengthTable;
    public TableReader m_Heroupgradexp;
    public TableReader m_ShangdianTable;
    public TableReader m_ShangdiandiaoluoTable;

    private static DataTemplate Inst = null;
    public static DataTemplate GetInstance ()
    {
        if ( Inst == null )
        {
            Inst = new DataTemplate ();
        }
        return Inst;
    }

    public void Init ()
    {
        m_TableList.Clear ();
        m_TableTypeList.Clear ();

        m_HeroTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.hero_01", m_HeroTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.hero_01", typeof ( HeroTemplate ) );

        m_HeroExpTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.heroexp_02", m_HeroExpTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.heroexp_02", typeof ( HeroexpTemplate ) );

        m_PlayerExpTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.player_03", m_PlayerExpTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.player_03", typeof ( PlayerTemplate ) );

        m_ChsTextTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.chstext_05", m_ChsTextTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.chstext_05", typeof ( ChsTextTemplate ) );

        m_SkillTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.skill_06", m_SkillTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.skill_06", typeof ( SkillTemplate ) );

        m_BufferTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.buff_07", m_BufferTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.buff_07", typeof ( BuffTemplate ) );

        m_BuffGroupTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.buffgroup_08", m_BuffGroupTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.buffgroup_08", typeof ( BuffgroupTemplate ) );

        m_StageTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.stage_11", m_StageTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.stage_11", typeof ( StageTemplate ) );

        m_MonsterGroupTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.monstergroup_12", m_MonsterGroupTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.monstergroup_12", typeof ( MonstergroupTemplate ) );

        m_MonsterTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.monster_13", m_MonsterTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.monster_13", typeof ( MonsterTemplate ) );

        m_NormaldropTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.normaldrop_15", m_NormaldropTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.normaldrop_15", typeof ( NormaldropTemplate ) );

        m_InnerdropTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.innerdrop_16", m_InnerdropTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.innerdrop_16", typeof ( InnerdropTemplate ) );

        m_SkillupcostTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.skillupcost_17", m_SkillupcostTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.skillupcost_17", typeof ( SkillupcostTemplate ) );

        m_SkillaiTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.skillai_18", m_SkillaiTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.skillai_18", typeof ( SkillaiTemplate ) );

        m_HeroFuryTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.herofury_19", m_HeroFuryTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.herofury_19", typeof ( HerofuryTemplate ) );

        m_InterfacemodelTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.interfacemodel_20", m_InterfacemodelTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.interfacemodel_20", typeof ( InterfacemodelTemplate ) );

        m_LevelamendmentTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.levelamendment_21", m_LevelamendmentTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.levelamendment_21", typeof ( LevelamendmentTemplate ) );

        m_ChapterTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.chapterinfo_23", m_ChapterTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.chapterinfo_23", typeof ( ChapterinfoTemplate ) );

        m_BossboxTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.bossbox_25", m_BossboxTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.bossbox_25", typeof ( BossboxTemplate ) );

        m_ItemTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.item_26", m_ItemTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.item_26", typeof ( ItemTemplate ) );

        m_RunepassiveTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.runepassive_27", m_RunepassiveTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.runepassive_27", typeof ( RunepassiveTemplate ) );

        m_BaseruneattributeTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.baseruneattribute_28", m_BaseruneattributeTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.baseruneattribute_28", typeof ( BaseruneattributeTemplate ) );

        m_AddruneattributeTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.addruneattribute_29", m_AddruneattributeTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.addruneattribute_29", typeof ( AddruneattributeTemplate ) );

        m_RunecostTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.runecost_30", m_RunecostTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.runecost_30", typeof ( RunecostTemplate ) );

        m_ArtresourceTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.artresource_31", m_ArtresourceTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.artresource_31", typeof ( ArtresourceTemplate ) );

        m_AttributetrainTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.attributetrain_32", m_AttributetrainTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.attributetrain_32", typeof ( AttributetrainTemplate ) );

        m_ArtifactTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.artifact_33", m_ArtifactTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.artifact_33", typeof ( ArtifactTemplate ) );

        m_ShopTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.shop_35", m_ShopTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.shop_35", typeof ( ShopTemplate ) );

        m_VipTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.vip_39", m_VipTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.vip_39", typeof ( VipTemplate ) );

        m_IllustratehandbookTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.illustratehandbook_40", m_IllustratehandbookTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.illustratehandbook_40", typeof ( IllustratehandbookTemplate ) );

        m_MedalexchangeTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.medalexchange_41", m_MedalexchangeTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.medalexchange_41", typeof ( MedalexchangeTemplate ) );

        m_LoginbonusTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.loginbonus_42", m_LoginbonusTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.loginbonus_42", typeof ( LoginbonusTemplate ) );

        m_MysteriousshopTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.mysteriousshop_43", m_MysteriousshopTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.mysteriousshop_43", typeof ( MysteriousshopTemplate ) );

        m_MonthcardTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.monthcard_45", m_MonthcardTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.monthcard_45", typeof ( MonthcardTemplate ) );

        m_ExplorequestTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.explorequest_46", m_ExplorequestTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.explorequest_46", typeof ( ExplorequestTemplate ) );

        m_HerocloneTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.heroclone_47", m_HerocloneTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.heroclone_47", typeof ( HerocloneTemplate ) );

        m_AnimEventTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.animationevent_48", m_AnimEventTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.animationevent_48", typeof ( AnimEventTemplate ) );

        m_UltimatetrialmonsterTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.ultimatetrialmonster_49", m_UltimatetrialmonsterTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.ultimatetrialmonster_49", typeof ( UltimatetrialmonsterTemplate ) );

        m_UltimatetrialrewardTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.ultimatetrialreward_50", m_UltimatetrialrewardTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.ultimatetrialreward_50", typeof ( UltimatetrialrewardTemplate ) );

        m_HeroRecruitTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.herorecruit_51", m_HeroRecruitTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.herorecruit_51", typeof ( HerorecruitTemplate ) );

        m_RuintreasureTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.ruintreasure_52", m_RuintreasureTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.ruintreasure_52", typeof ( RuintreasureTemplate ) );

        m_ResourceindexTemplate = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.resourceindex_53", m_ResourceindexTemplate );
        m_TableTypeList.Add ( "chuhan.gsp.client.resourceindex_53", typeof ( ResourceindexTemplate ) );

        m_ActivitymissionTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.activitymission_55", m_ActivitymissionTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.activitymission_55", typeof ( ActivitymissionTemplate ) );

        m_ExchangeTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.exchange_56", m_ExchangeTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.exchange_56", typeof ( ExchangeTemplate ) );

        m_LegendexchargeTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.legendexcharge_57", m_LegendexchargeTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.legendexcharge_57", typeof ( LegendexchargeTemplate ) );

        m_ShieldCharacterTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.shieldcharacter_58", m_ShieldCharacterTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.shieldcharacter_58", typeof ( ShieldcharacterTemplate ) );

        m_RandnameTemplate = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.randname_59", m_RandnameTemplate );
        m_TableTypeList.Add ( "chuhan.gsp.client.randname_59", typeof ( RandnameTemplate ) );

        m_NewbieguideTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.newbieguide_60", m_NewbieguideTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.newbieguide_60", typeof ( NewbieguideTemplate ) );

        m_GameactivityTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.gameactivity_61", m_GameactivityTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.gameactivity_61", typeof ( GameactivityTemplate ) );

        m_CaptionTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.runhorselight_62", m_CaptionTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.runhorselight_62", typeof ( RunhorselightTemplate ) );

        m_BroadCastTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.broadcast_63", m_BroadCastTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.broadcast_63", typeof ( BroadcastTemplate ) );

        m_PropsacessTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.propsaccess_65", m_PropsacessTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.propsaccess_65", typeof ( PropsaccessTemplate ) );

        m_PropsjumpuiTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.propsjumpui_66", m_PropsjumpuiTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.propsjumpui_66", typeof ( PropsjumpuiTemplate ) );

        m_HeroaddstageTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.heroaddstage_67", m_HeroaddstageTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.heroaddstage_67", typeof ( HeroaddstageTemplate ) );

        m_AngerTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.angertable_69", m_AngerTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.angertable_69", typeof ( AngertableTemplate ) );

        m_HerocultureTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.heroculture_70", m_HerocultureTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.heroculture_70", typeof ( HerocultureTemplate ) );

        m_EquipmentqualityTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.equipmentquality_71", m_EquipmentqualityTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.equipmentquality_71", typeof ( EquipmentqualityTemplate ) );

        m_EquipmentstrengthTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.equipmentstrength_72", m_EquipmentstrengthTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.equipmentstrength_72", typeof ( EquipmentstrengthTemplate ) );

        m_MsTable = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.ms_73", m_MsTable );
        m_TableTypeList.Add ( "chuhan.gsp.client.ms_73", typeof ( MsTemplate ) );

        m_ShangdianTable = new TableReader();
        m_TableList.Add("chuhan.gsp.client.shangdian_76", m_ShangdianTable);
        m_TableTypeList.Add("chuhan.gsp.client.shangdian_76", typeof(ShangdianTemplate));

        m_ShangdiandiaoluoTable = new TableReader();
        m_TableList.Add("chuhan.gsp.client.shangdiandiaoluo_77", m_ShangdiandiaoluoTable);
        m_TableTypeList.Add("chuhan.gsp.client.shangdiandiaoluo_77", typeof(ShangdiandiaoluoTemplate));

        m_Heroupgradexp = new TableReader ();
        m_TableList.Add ( "chuhan.gsp.client.heroupgradexp_68", m_Heroupgradexp );
        m_TableTypeList.Add ( "chuhan.gsp.client.heroupgradexp_68", typeof ( HeroupgradexpTemplate ) );


        m_GameConfig = new GameConfig ();
        m_TableList.Add ( "chuhan.gsp.client.config_10", m_GameactivityTable ); //10表没有类型 就先传一个 解析的时候不用这个类型
        m_TableTypeList.Add ( "chuhan.gsp.client.config_10", typeof ( GameactivityTemplate ) );

        //m_GameConfig = new GameConfig();
        //m_GameConfig.LoadBinary("clientxml2/chuhan.gsp.client.config_10.bin");
    }

    public void InitData ( string TableName, byte [] array )
    {
        if ( m_TableList.ContainsKey ( TableName ) )
        {
            if ( TableName == "chuhan.gsp.client.config_10" )
            {
                m_GameConfig.LoadBinary ( "chuhan.gsp.client.config_10", array );
            }
            else
            {
                if ( TableName == "chuhan.gsp.client.chstext_05" )
                {
                    m_TableList [ TableName ].LoadBinary ( TableName, array, m_TableTypeList [ TableName ], true );
                }
                else
                {
                    m_TableList [ TableName ].LoadBinary ( TableName, array, m_TableTypeList [ TableName ] );

                }
            }
        }
    }

    public ItemTemplate GetItemTemplateById ( int tableId )
    {
        return ( ItemTemplate ) m_ItemTable.getTableData ( tableId );
    }

    public HeroTemplate GetHeroTemplateById ( int tableId )
    {
        return ( HeroTemplate ) m_HeroTable.getTableData ( tableId );
    }
    public MysteriousshopTemplate GetMysteriousShopItemTemplateById ( int tableId )
    {
        return ( MysteriousshopTemplate ) m_MysteriousshopTable.getTableData ( tableId );
    }

    public List<int> GetHeroRuneGroup ( HeroTemplate heroT )
    {
        if ( heroT == null )
            return null;

        List<int> result = new List<int> ();

        if ( heroT.getRunePair1 () != -1 )
        {
            result.Add ( heroT.getRunePair1 () );
        }
        if ( heroT.getRunePair2 () != -1 )
        {
            result.Add ( heroT.getRunePair2 () );
        }
        if ( heroT.getRunePair3 () != -1 )
        {
            result.Add ( heroT.getRunePair3 () );
        }
        if ( heroT.getRunePair4 () != -1 )
        {
            result.Add ( heroT.getRunePair4 () );
        }

        return result;
    }

    /// <summary>
    /// 获得当前符文可以鉴定的附加属性个数;
    /// </summary>
    /// <param name="itemT"></param>
    /// <returns></returns>
    public int GetRuneMaxRedefineTimes ( ItemTemplate itemT )
    {
        if ( itemT == null )
            return 0;

        int result = 0;
        if ( itemT.getRune_addAttri1 () != -1 )
            result++;
        if ( itemT.getRune_addAttri2 () != -1 )
            result++;
        if ( itemT.getRune_addAttri3 () != -1 )
            result++;
        if ( itemT.getRune_addAttri4 () != -1 )
            result++;

        return result;
    }

    /// <summary>
    /// 判断符文强化等级是否满级;
    /// </summary>
    /// <returns></returns>
    public bool IsRuneStrenthFullLevel ( ItemTemplate itemT, int level )
    {
        return level >= GetRuneStrenthMaxLevel ( itemT );
    }

    public int GetRuneStrenthMaxLevel ( ItemTemplate itemT )
    {
        if ( itemT == null )
            return -1;

        int bagId = itemT.getRune_strengthenId ();
        int result = 0;

        foreach ( int i in m_RunecostTable.GetDataKeys () )
        {
            RunecostTemplate rt = ( RunecostTemplate ) m_RunecostTable.getTableData ( i );
            if ( rt.getBagId () == bagId )
                result++;
        }

        return result;
    }

    /// <summary>
    /// 根据属性库id;等级获取RunecostTemplate
    /// </summary>
    /// <param name="bagId"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public RunecostTemplate GetRuneCostTemplate ( int bagId, int level )
    {
        foreach ( int i in m_RunecostTable.GetDataKeys () )
        {
            RunecostTemplate rt = ( RunecostTemplate ) m_RunecostTable.getTableData ( i );

            if ( rt == null )
                continue;

            if ( rt.getBagId () == bagId && rt.getLevel () == level )
                return rt;
        }

        return null;
    }

    public RunecostTemplate GetRuneCostTemplate ( int tableid )
    {
        return ( RunecostTemplate ) m_RunecostTable.getTableData ( tableid );
    }

    public RunepassiveTemplate GetRunepassiveTemplate ( int tableid )
    {
        return ( RunepassiveTemplate ) m_RunepassiveTable.getTableData ( tableid );
    }

    public BaseruneattributeTemplate GetBaseruneattributeTemplate ( int tableid )
    {
        return ( BaseruneattributeTemplate ) m_BaseruneattributeTable.getTableData ( tableid );
    }

    public AddruneattributeTemplate GetAddruneattributeTemplate ( int tableid )
    {
        return ( AddruneattributeTemplate ) m_AddruneattributeTable.getTableData ( tableid );
    }

    public ArtresourceTemplate GetArtResourceTemplate ( int tableid )
    {
        return ( ArtresourceTemplate ) m_ArtresourceTable.getTableData ( tableid );
    }

    public int GetArtResourceAtrriCount ( ArtresourceTemplate artT )
    {
        if ( artT == null )
            return -1;

        int [] types = artT.getAttriType ();

        if ( types == null )
            return -1;

        int res = 0;

        for ( int i = 0, j = types.Length; i < j; i++ )
        {
            if ( types [ i ] != -1 )
                res++;
        }

        return res;
    }

    public HeroTemplate GetHeroTemplateByArtresourceId ( int artresourceId )
    {
        for ( int i = 0; i < m_HeroTable.getDataCount (); i++ )
        {
            int key = m_HeroTable.GetDataKeys () [ i ];

            HeroTemplate heroT = m_HeroTable.getTableData ( key ) as HeroTemplate;

            if ( heroT == null )
            {
                continue;
            }

            if ( heroT.getUseableArtresource () == null )
            {
                continue;
            }

            for ( int m = 0, n = heroT.getUseableArtresource ().Length; m < n; m++ )
            {
                int artid = heroT.getUseableArtresource () [ m ];

                if ( artid == artresourceId )
                {
                    return heroT;
                }
            }
        }

        return null;
    }
    #region (月卡表格)
    public MonthcardTemplate GetMonthCardTemplateByID ( int tableId )
    {
        return ( MonthcardTemplate ) m_MonthcardTable.getTableData ( tableId );
    }

    public List<MonthcardTemplate> GetAllMonthCardTemplates ()
    {
        List<MonthcardTemplate> result = new List<MonthcardTemplate> ();

        foreach ( int i in m_MonthcardTable.GetDataKeys () )
        {
            MonthcardTemplate st = ( MonthcardTemplate ) m_MonthcardTable.getTableData ( i );

            if ( st == null )
                continue;

            result.Add ( st );
        }

        return result;
    }
    #endregion


    #region (充值表格)
    public ExchangeTemplate GetExchangeTemplateByID ( int tableId )
    {
        return ( ExchangeTemplate ) m_ExchangeTable.getTableData ( tableId );
    }

    /// <summary>
    /// 对应不同平台的数据;
    /// </summary>
    /// <param name="plateformName"></param>
    /// <returns></returns>
    public List<ExchangeTemplate> GetAllExchangeTemplates ( string plateformName = "" )
    {
        List<ExchangeTemplate> result = new List<ExchangeTemplate> ();

        foreach ( int i in m_ExchangeTable.GetDataKeys () )
        {
            ExchangeTemplate st = ( ExchangeTemplate ) m_ExchangeTable.getTableData ( i );

            if ( st == null )
                continue;

            if ( !string.IsNullOrEmpty ( plateformName ) && plateformName.Equals ( st.getPlatform () ) )
            {
                result.Add ( st );
            }

        }

        return result;
    }

    public ExchangeTemplate GetExchangeTemplateByMonthCardId ( int monthCardId )
    {
        foreach ( int i in m_ExchangeTable.GetDataKeys () )
        {
            ExchangeTemplate st = ( ExchangeTemplate ) m_ExchangeTable.getTableData ( i );

            if ( st.getMonthcardID () == monthCardId )
                return st;
        }

        return null;
    }

    /// <summary>
    /// 写死的快速充值物品走商城的充值页签下的非月卡商品--对应不同平台的数据;
    /// </summary>
    /// <returns></returns>
    public List<ExchangeTemplate> GetQuikChargeShopID ( string platformName = "" )
    {
        List<ExchangeTemplate> result = new List<ExchangeTemplate> ();

        foreach ( int i in m_ExchangeTable.GetDataKeys () )
        {
            ExchangeTemplate st = ( ExchangeTemplate ) m_ExchangeTable.getTableData ( i );

            if ( st == null )
                continue;

            if ( st.getPreviewType () == 1 )
                continue;

            if ( !string.IsNullOrEmpty ( platformName ) && platformName.Equals ( st.getPlatform () ) )
                result.Add ( st );
        }

        return result;
    }

    #endregion

    #region 商城表格相关;
    public ShopTemplate GetShopTemplateByID ( int shopId )
    {
        return ( ShopTemplate ) m_ShopTable.getTableData ( shopId );
    }

    public List<ShopTemplate> GetAllShopTemplates ()
    {
        List<ShopTemplate> result = new List<ShopTemplate> ();

        foreach ( int i in m_ShopTable.GetDataKeys () )
        {
            ShopTemplate st = ( ShopTemplate ) m_ShopTable.getTableData ( i );

            if ( st == null )
                continue;

            result.Add ( st );
        }

        return result;
    }


    public List<ShopTemplate> GetShopTemplatesByTabID ( SHOP_TAB tab )
    {
        return GetShopTemplatesByTabID ( ( int ) tab );
    }

    public List<ShopTemplate> GetShopTemplatesByTabID ( int tabID )
    {
        List<ShopTemplate> result = new List<ShopTemplate> ();

        foreach ( int i in m_ShopTable.GetDataKeys () )
        {
            ShopTemplate st = ( ShopTemplate ) m_ShopTable.getTableData ( i );

            if ( st == null )
                continue;

            if ( st.getTabID () == tabID )
                result.Add ( st );
        }

        return result;
    }



    /// <summary>
    /// 获取购买商品消耗资源类型;
    /// </summary>
    /// <param name="shopT"></param>
    /// <returns></returns>
    public int GetShopBuyCostType ( ShopTemplate shopT )
    {
        if ( shopT == null )
            return -1;

        return shopT.getCostType ();
    }

    /// <summary>
    /// 根据商店物品购买次数获得下次购买需要消耗的资源数;
    /// </summary>
    /// <param name="shopT"></param>
    /// <param name="buyTimes"></param>
    /// <param name="isDiscount"></param>
    /// <returns></returns>
    public int GetShopBuyCost ( ShopTemplate shopT, int buyTimes, bool isDiscount = false )
    {
        if ( shopT == null || buyTimes < 0 )
            return -1;

        int [] costs = null;

        if ( isDiscount )
        {
            costs = shopT.getDiscountCost ();
        }
        else
        {
            costs = shopT.getCost ();
        }

        if ( costs == null )
        {
            Debug.LogError ( "shop表格数据错误，id=" + shopT.getId () + "的消费数据处错误" );
            return -1;
        }

        if ( buyTimes >= costs.Length )
            return costs [ costs.Length - 1 ];

        return costs [ buyTimes ];
    }


    /// <summary>
    /// 获取商城物品每日购买次数限制---目前VIP等级会影响到这里,后期可能还会增加其他限制;
    /// </summary>
    /// <param name="shopT"></param>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public int GetShopItemDailyBuyTimes ( ShopTemplate shopT, int vipLv )
    {
        if ( shopT.getDailyMaxBuy () < 0 )
            return -1;

        int vipCount = 0;//根据VIP算出的附加值;

        //行动力补满药水;
        if ( shopT.getId () == GetGameConfig ().getEp_supplement_goods () )
        {
            VipTemplate vip = GetVipTemplateById ( vipLv );
            vipCount = vip.getMaxBuyEp ();
        }

        //活力补满药水;
        if ( shopT.getId () == GetGameConfig ().getAp_supplement_goods () )
        {
            VipTemplate vip = GetVipTemplateById ( vipLv );
            vipCount = vip.getMaxBuyAp ();
        }

        return shopT.getDailyMaxBuy () + vipCount;
    }

    /// <summary>
    /// 获取商城物品总的购买次数限制---目前VIP等级会影响到这里,后期可能还会增加其他限制;
    /// </summary>
    /// <param name="shopT"></param>
    /// <param name="vipLv"></param>
    /// <returns></returns>
    public int GetShopItemTotalBuyTimes ( ShopTemplate shopT, int vipLv )
    {
        if ( shopT.getShelveMaxBuy () < 0 )
            return -1;

        int vipCount = 0;//根据VIP算出的附加值;

        return shopT.getShelveMaxBuy () + vipCount;
    }

    #endregion

    public VipTemplate GetVipTemplateById ( int id )
    {
        return ( VipTemplate ) m_VipTable.getTableData ( id );
    }

    public GameConfig GetGameConfig ()
    {
        return m_GameConfig;
    }

    public ActivitymissionTemplate GetActivitymissionTemplateById ( int id )
    {
        return ( ActivitymissionTemplate ) m_ActivitymissionTable.getTableData ( id );
    }

    #region 关卡表相关;
    public ChapterinfoTemplate GetChapterTemplateByID ( int id )
    {
        return ( ChapterinfoTemplate ) m_ChapterTable.getTableData ( id );
    }

    /// <summary>
    /// 通过关卡id获得该关卡所在的章节信息;
    /// </summary>
    /// 策划说关卡id在章节中是唯一的，一个关卡不可能出现在多个章节中，所以该方法有效;
    /// <param name="id"></param>
    /// <returns></returns>
    public ChapterinfoTemplate GetChapterTemplateByStageID ( int stageid )
    {
        List<int> keys = m_ChapterTable.GetDataKeys ();

        if ( keys == null || keys.Count == 0 )
        {
            return null;
        }

        for ( int i = 0; i < keys.Count; i++ )
        {
            ChapterinfoTemplate ct = GetChapterTemplateByID ( keys [ i ] );

            if ( ct.getStageID ().ToList<int> ().Contains ( stageid ) )
            {
                return ct;
            }
        }

        return null;
    }

    /// <summary>
    /// 通过关卡id获得该关卡所在的章节信息;
    /// </summary>
    /// 策划说关卡id在章节中是唯一的，一个关卡不可能出现在多个章节中，所以该方法有效;
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetChapterIdByStageT ( StageTemplate stageT )
    {
        if ( stageT != null )
        {
            ChapterinfoTemplate chapterT = GetChapterTemplateByStageID ( stageT.m_stageid );

            if ( chapterT != null )
            {
                return chapterT.getId ();
            }
        }

        return -1;
    }
    #endregion

    #region 探险任务相关;
    public ExplorequestTemplate GetExplorequestTemplateById ( int id )
    {
        return ( ExplorequestTemplate ) m_ExplorequestTable.getTableData ( id );
    }

    public int GetExploreChapterIdByExploreId ( int id )
    {
        ExplorequestTemplate et = GetExplorequestTemplateById ( id );

        if ( et != null )
        {
            return et.getChapterID ();
        }

        return -1;
    }

    /// <summary>
    /// 获取探险该任务需要的英雄数量;
    /// 如果任务需求类型为1（根据阵容、等级、品质）则返回needNum字段的值;
    /// 如果任务需求类型为2（根据指定的英雄id）   则返回需要英雄id组不为-1的个数;
    /// </summary>
    /// <returns></returns>
    public int GetExploreNeedHeroCount ( ExplorequestTemplate exploreT )
    {
        switch ( exploreT.getNeedHeroType () )
        {
            case 1:
                return exploreT.getNeedNum ();
            case 2:
                int count = 0;

                if ( exploreT.getNeedHeroID1 () != null && exploreT.getNeedHeroID1 ().Length > 0 && exploreT.getNeedHeroID1 () [ 0 ] != -1 )
                    count++;
                if ( exploreT.getNeedHeroID2 () != null && exploreT.getNeedHeroID2 ().Length > 0 && exploreT.getNeedHeroID2 () [ 0 ] != -1 )
                    count++;
                if ( exploreT.getNeedHeroID3 () != null && exploreT.getNeedHeroID3 ().Length > 0 && exploreT.getNeedHeroID3 () [ 0 ] != -1 )
                    count++;
                if ( exploreT.getNeedHeroID4 () != null && exploreT.getNeedHeroID4 ().Length > 0 && exploreT.getNeedHeroID4 () [ 0 ] != -1 )
                    count++;
                if ( exploreT.getNeedHeroID5 () != null && exploreT.getNeedHeroID5 ().Length > 0 && exploreT.getNeedHeroID5 () [ 0 ] != -1 )
                    count++;

                return count;
            default:
                LogManager.LogError ( "不支持的英雄筛选类型type=" + exploreT.getNeedHeroType () );
                break;
        }

        return -1;
    }
    #endregion

    public NormaldropTemplate GetNormaldropTemplateById ( int id )
    {
        return ( NormaldropTemplate ) m_NormaldropTable.getTableData ( id );
    }

    public InnerdropTemplate GetInnerdropTemplateById ( int id )
    {
        return ( InnerdropTemplate ) m_InnerdropTable.getTableData ( id );
    }

    public PlayerTemplate GetPlayerTemplateById ( int id )
    {
        return ( PlayerTemplate ) m_PlayerExpTable.getTableData ( id );
    }

    public BroadcastTemplate GetBroadcastTemplateById ( int id )
    {
        return ( BroadcastTemplate ) m_BroadCastTable.getTableData ( id );
    }
    /// <summary>
    /// 返回72表装备强化 表数据 条件参数3个 [10/22/2015 Zmy]
    /// </summary>
    /// <param name="nPos">英雄定位</param>
    /// <param name="nParts">装备的部位，71表字段定义</param>
    /// <param name="nIntensifyLev">当前装备的强化等级</param>
    /// <returns></returns>
    public EquipmentstrengthTemplate GetEquipStrengthTemplate ( int nPos, int nParts, int nIntensifyLev )
    {
        for ( int i = 0; i < m_EquipmentstrengthTable.getDataList ().Count; ++i )
        {
            EquipmentstrengthTemplate _row = m_EquipmentstrengthTable.getDataList () [ i ] as EquipmentstrengthTemplate;
            if ( _row.getQosition () == nPos && _row.getParts () == nParts && _row.getSthequipmentlevel () == nIntensifyLev )
            {
                return _row;
            }
        }
        return null;
    }

    public HerocultureTemplate GetHerocultureTemplate ( int nBorn, int nPos, int nType, int nElementLev )
    {
        for ( int i = 0; i < m_HerocultureTable.getDataList ().Count; ++i )
        {
            HerocultureTemplate _row = m_HerocultureTable.getDataList () [ i ] as HerocultureTemplate;
            if ( _row.getBorn () == nBorn && _row.getQosition () == nPos && _row.getElement () == nType && _row.getElementLeve () == nElementLev )
            {
                return _row;
            }
        }
        return null;
    }

    public HeroupgradexpTemplate GetHeroupgradexpTemplate ( int nBorn, int level )
    {
        for ( int i = 0; i < m_Heroupgradexp.getDataList ().Count; ++i )
        {
            HeroupgradexpTemplate _row = m_Heroupgradexp.getDataList () [ i ] as HeroupgradexpTemplate;
            if ( _row.getBorn () == nBorn && _row.getLevel () == level )
            {
                return _row;
            }
        }
        return null;
    }


    /// <summary>
    /// 只有经验药水有用
    /// </summary>
    /// <param name="id">商品参数 id</param>
    /// <returns>商品一行数值表</returns>
    public ShopTemplate GetShopTemplateByParaID ( int id )
    {
        foreach ( var item in m_ShopTable.getDataList () )
        {
            ShopTemplate shop = ( ShopTemplate ) item;
            if ( int.Parse ( shop.getPara () ) == id )
            {
                return shop;
            }
        }
        return null;
    }


    // 得到所有的英雄经验
    /// <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public int GetHeroAllExp ( ObjectCard card )
    {
        foreach ( var item in m_Heroupgradexp.getDataList () )
        {
            HeroupgradexpTemplate temp = ( HeroupgradexpTemplate ) item;
            if ( temp.getBorn () == card.GetHeroRow ().getBorn () && temp.getLevel () == card.GetHeroData ().Level )
            {
                return temp.getExp ();
            }
        }

        return -1;
    }
}
