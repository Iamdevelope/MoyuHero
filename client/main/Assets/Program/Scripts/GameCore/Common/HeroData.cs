using System;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;
using GNET;

namespace DreamFaction.GameNetWork.Data
{
    /// <summary>
    /// 英雄数据信息，包括等级，经验，防御力等属性
    /// </summary>
    public class HeroData
    {
        private X_GUID m_Guid = new X_GUID ();				//guid
        private int m_TableID;			                    //表格id
        private int m_Level;			                    //当前等级
        private int m_Exp;				                    //当前经验
        private int []                  m_TrainCount        = new int [ GlobalMembers.MAX_TRAIN_SLOT_COUNT ];
        private SpellData []            m_SpellData         = new SpellData [ GlobalMembers.MAX_DB_SPELL_NUM ];  //技能
        private int []                  m_CacheSpellID      = new int [ GlobalMembers.MAX_DB_SPELL_NUM ]; //技能ID缓存。只在刷新英雄的时候记录，为了可以重置英雄技能ID [7/21/2015 Zmy]
        private SpellData               m_ItemPassiveSpell  = new SpellData ();  //符文附加被动技能  [6/27/2015 Zmy]
        private X_GUID []               m_pItem             = new X_GUID [ ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER ]; //符文装备点;

        private int m_HeroViewID;       //显示ID
        private int m_AllExp;           //总经验
        private int m_IntensifyLevel;   //强化等级
        private int m_Weapon;           //武器
        private int m_Barde;            //铠甲
        private int m_Ornament;         //饰品
        private int m_IntensifyAddtion; //强化加成

        private int m_StarLevel;        //星级
        private int m_CurStage;         //当前阶数
        private int m_FigthVigor;       //战力值
        private int m_QualityLev;       //品质

        private HeroSkillDB     m_SkillDB   = new HeroSkillDB();        //技能数据
        private HeroCabalaDB    m_CabalaDB  = new HeroCabalaDB();       //秘术数据
        private HeroTrainDB     m_TrainDB   = new HeroTrainDB();        //培养数据
        private HeroEquipDB     m_EquipDB   = new HeroEquipDB();        //装备数据

        /// <summary>
        /// 构造函数，执行清理操作
        /// </summary>
        public HeroData ()
        {
            CleanUp ();
        }


        public int GetHeroViewID ()
        {
            return m_HeroViewID;
        }

        /// <summary>
        /// 获取英雄的 GUID
        /// </summary>
        /// <returns></returns>
        public X_GUID GUID
        {
            get
            {
                return m_Guid;
            }
        }

        /// <summary>
        /// 获取表格ID , 测试使用提供set操作，测试完删掉set操作！
        /// </summary>
        public int TableID
        {
            get
            {
                return m_TableID;
            }
            set
            {
                m_TableID = value;
            }
        }

        /// <summary>
        /// 获取等级 ，测试使用提供set操作，测试完删掉set操作！
        /// </summary>
        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
            }
        }

        public int Exp
        {
            get
            {
                return m_Exp;
            }
            set
            {
                m_Exp = value;
            }
        }

        /// <summary>
        /// 当前等级的总经验值;
        /// </summary>
        public int AllExp
        {
            get
            {
                return m_AllExp;
            }
        }

        public int StarLevel
        {
            get { return m_StarLevel; }
            set { m_StarLevel = value; }
        }

        public int CurStage
        {
            get { return m_CurStage; }
            set { m_CurStage = value; }
        }

        public int FightVigor
        {
            get { return m_FigthVigor; }
            set { m_FigthVigor = value; }
        }

        public int QualityLev
        {
            get { return m_QualityLev; }
            set { m_QualityLev = value; }
        }

        public HeroEquipDB HeroEqupDB
        {
            get { return m_EquipDB; }
        }
        public HeroTrainDB HeroTrainDB
        {
            get { return m_TrainDB; }
        }
        public HeroCabalaDB HeroCabalaDB
        {
            get { return m_CabalaDB; }
        }
        public HeroSkillDB HeroSkillDB
        {
            get { return m_SkillDB; }
        }
        /// <summary>
        /// 获取当前经验百分比
        /// </summary>
        /// <returns></returns>
        public float GetExpPercentage ()
        {
            return m_AllExp == 0 ? 0 : ( ( float ) m_Exp / m_AllExp );
        }
        /// <summary>
        ///  获取技能列表
        /// </summary>
        public SpellData [] SpellDataList
        {
            get
            {
                return m_SpellData;
            }
        }

        public SpellData ItemPassiveSpell
        {
            get
            {
                return m_ItemPassiveSpell;
            }
        }
        /// <summary>
        /// 获取培养唯一ID
        /// </summary>
        /// <returns></returns>
        public int [] GetTrainCount ()
        {
            return m_TrainCount;
        }
        /// <summary>
        /// 获取 魔法防御力
        /// </summary>
        public int TrainingMagicDefence
        {
            get
            {
                if ( m_TrainCount [ 3 ] == 0 || DataTemplate.GetInstance ().m_AttributetrainTable.tableContainsKey ( m_TrainCount [ 3 ] ) == false )
                {
                    return 0;
                }
                AttributetrainTemplate _row = ( AttributetrainTemplate ) DataTemplate.GetInstance ().m_AttributetrainTable.getTableData ( m_TrainCount [ 3 ] );

                return _row.getAttriValue ();
            }
        }

        /// <summary>
        /// 获取最大生命值
        /// </summary>
        public int TrainingMaxHP
        {
            get
            {
                if ( m_TrainCount [ 0 ] == 0 || DataTemplate.GetInstance ().m_AttributetrainTable.tableContainsKey ( m_TrainCount [ 0 ] ) == false )
                {
                    return 0;
                }
                AttributetrainTemplate _row = ( AttributetrainTemplate ) DataTemplate.GetInstance ().m_AttributetrainTable.getTableData ( m_TrainCount [ 0 ] );

                return _row.getAttriValue ();
            }
        }

        /// <summary>
        /// 获取物理攻击力
        /// </summary>
        public int TrainingPhysicalAttack
        {
            get
            {
                if ( m_TrainCount [ 1 ] == 0 || DataTemplate.GetInstance ().m_AttributetrainTable.tableContainsKey ( m_TrainCount [ 1 ] ) == false )
                {
                    return 0;
                }
                AttributetrainTemplate _row = ( AttributetrainTemplate ) DataTemplate.GetInstance ().m_AttributetrainTable.getTableData ( m_TrainCount [ 1 ] );

                return _row.getAttriValue ();
            }
        }

        /// <summary>
        /// 获取物理防御力
        /// </summary>
        public int TrainingPhysicalDefence
        {
            get
            {
                if ( m_TrainCount [ 2 ] == 0 || DataTemplate.GetInstance ().m_AttributetrainTable.tableContainsKey ( m_TrainCount [ 2 ] ) == false )
                {
                    return 0;
                }
                AttributetrainTemplate _row = ( AttributetrainTemplate ) DataTemplate.GetInstance ().m_AttributetrainTable.getTableData ( m_TrainCount [ 2 ] );

                return _row.getAttriValue ();
            }
        }

        /// <summary>
        /// 获取魔法攻击力
        /// </summary>
        public int TrainingMagicAttack
        {
            get
            {
                if ( m_TrainCount [ 1 ] == 0 || DataTemplate.GetInstance ().m_AttributetrainTable.tableContainsKey ( m_TrainCount [ 1 ] ) == false )
                {
                    return 0;
                }
                AttributetrainTemplate _row = ( AttributetrainTemplate ) DataTemplate.GetInstance ().m_AttributetrainTable.getTableData ( m_TrainCount [ 1 ] );

                return _row.getAttriValue ();
            }
        }
        /// <summary>
        /// 清理操作
        /// </summary>
        public void CleanUp ()
        {
            m_Guid.CleanUp ();
            m_TableID = -1;
            m_Level = 0;
            m_Exp = 0;
            for ( int i = 0; i < GlobalMembers.MAX_TRAIN_SLOT_COUNT; ++i )
            {
                this.m_TrainCount [ i ] = 0;
            }
            for ( int i = 0; i < GlobalMembers.MAX_DB_SPELL_NUM; ++i )
            {
                if ( m_SpellData [ i ] == null )
                {
                    m_SpellData [ i ] = new SpellData ();
                }
                m_SpellData [ i ].CleanUp ();
                m_CacheSpellID [ i ] = -1;
            }
            for ( int i = 0; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; ++i )
            {
                if ( m_pItem [ i ] == null )
                {
                    m_pItem [ i ] = new X_GUID ();
                }
                m_pItem [ i ].CleanUp ();
            }
            m_ItemPassiveSpell.CleanUp ();
            m_HeroViewID = 0;
            m_AllExp = 0;
            m_IntensifyLevel = 0;
            m_Weapon = 0;
            m_Barde = 0;
            m_Ornament = 0;
            m_IntensifyAddtion = 0;
            m_StarLevel = 0;
            m_CurStage = 0;
            FightVigor = 0;
            QualityLev = 0;

            m_SkillDB.ClearUp();
            m_CabalaDB.ClearUp();
            m_TrainDB.ClearUp();
            m_EquipDB.ClearUp();
        }


        /// <summary>
        /// 数据拷贝操作
        /// </summary>
        /// <param name="herodata">数据源</param>
        public void Copy ( HeroData herodata )
        {
            this.m_Guid.Copy ( herodata.m_Guid );
            this.m_TableID = herodata.m_TableID;
            this.m_Level = herodata.m_Level;
            this.m_Exp = herodata.m_Exp;
            for ( int i = 0; i < GlobalMembers.MAX_TRAIN_SLOT_COUNT; ++i )
            {
                this.m_TrainCount [ i ] = herodata.m_TrainCount [ i ];
            }
            for ( int i = 0; i < GlobalMembers.MAX_DB_SPELL_NUM; ++i )
            {
                this.m_SpellData [ i ].Copy ( herodata.m_SpellData [ i ] );
                OnCacheSpellData(herodata.m_SpellData);
            }
            for ( int i = 0; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; ++i )
            {
                if ( m_pItem [ i ] != null )
                {
                    this.m_pItem [ i ].Copy ( herodata.m_pItem [ i ] );
                }
            }
            this.m_ItemPassiveSpell.Copy ( herodata.m_ItemPassiveSpell );

            m_HeroViewID = herodata.m_HeroViewID;
            m_AllExp = herodata.m_AllExp;
            m_IntensifyLevel = herodata.m_IntensifyLevel;
            m_Weapon = herodata.m_Weapon;
            m_Barde = herodata.m_Barde;
            m_Ornament = herodata.m_Ornament;
            m_IntensifyAddtion = herodata.m_IntensifyAddtion;

            m_StarLevel = herodata.StarLevel;
            m_CurStage = herodata.CurStage;
            m_QualityLev = herodata.QualityLev;
            m_FigthVigor = herodata.FightVigor;

            m_SkillDB.CopyData(herodata.m_SkillDB);
            m_CabalaDB.CopyData(herodata.m_CabalaDB);
            m_TrainDB.CopyData(herodata.m_TrainDB);
            m_EquipDB.CopyData(herodata.m_EquipDB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equips"></param>
        public void SetEquipItems ( List<X_GUID> equips )
        {
            if ( equips == null || equips.Count != ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER )
                return;

            for ( int i = 0; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; i++ )
            {
                m_pItem [ i ].Copy ( equips [ i ] );
            }
        }

        public List<X_GUID> GetEquipItems ()
        {
            List<X_GUID> result = new List<X_GUID> ();

            for ( int i = 0; i < m_pItem.Length; i++ )
            {
                result.Add ( m_pItem [ i ] );
            }

            return result;
        }

        public void Init ( Hero pData, bool _isRefresh = false )
        {
            this.m_Guid.GUID_value = pData.key;
            this.m_TableID = pData.heroid;
            this.m_Level = pData.herolevel;
            this.m_Exp = pData.heroexp;
            this.m_HeroViewID = pData.heroviewid;
            this.m_AllExp = pData.heroallexp;
            this.m_IntensifyLevel = pData.qianghualevel;
            this.m_Weapon = pData.weapon;
            this.m_Barde = pData.barde;
            this.m_Ornament = pData.ornament;
            this.m_IntensifyAddtion = pData.qhadd;
            this.m_TrainCount [ 0 ] = ( int ) pData.peiyang1;
            this.m_TrainCount [ 1 ] = ( int ) pData.peiyang2;
            this.m_TrainCount [ 2 ] = ( int ) pData.peiyang3;
            this.m_TrainCount [ 3 ] = ( int ) pData.peiyang4;
            this.m_SpellData [ 0 ].SpellID = pData.skill1;
            this.m_SpellData [ 1 ].SpellID = pData.skill2;
            this.m_SpellData [ 2 ].SpellID = pData.skill3;
            this.m_StarLevel = pData.herojinjiestar;
            this.m_CurStage = pData.herojinjiesmall;
            this.m_QualityLev = pData.heropinji;
            this.m_FigthVigor = pData.weapon;
            this.m_ItemPassiveSpell.CleanUp ();

            this.m_SkillDB.ParserSkillDB(pData.heroskill);
            this.m_CabalaDB.ParserCabalaDB(pData.heromishu, _isRefresh, pData.heroid);
            this.m_TrainDB.ParserTrainDB(pData.heropeiyang);
            this.m_EquipDB.ParserEquipDB(pData.heroequip);

            OnCacheSpellData ( pData );
            InitEquipItem ( pData.items, _isRefresh );
        }
        private void InitEquipItem ( Hashtable _map, bool _refresh )
        {
            //for ( int i = ( int ) EM_RUNE_POINT.EM_RUNE_POINT_COMMON1; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; ++i )
            //{
            //    if ( _map.ContainsKey ( i + 1 ) )//服务器下发的key从1开始 [4/29/2015 Zmy]
            //    {
            //        try
            //        {
            //            if ( _refresh )//英雄刷新，先取消已装备的符文状态 [5/21/2015 Zmy]
            //            {
            //                UnityEngine.Debug.Log("guid:"+m_pItem[i].GUID_value);
            //                ObjectSelf.GetInstance ().CommonItemContainer.OnUpdateRuneEquipState ( m_pItem [ i ], false );
            //            }
            //        }
            //        catch ( Exception e )
            //        {
            //            LogManager.LogError ( "!!!!!!!InitEquipItem Error:" + e );
            //        }
            //    }
            //}

            for ( int i = ( int ) EM_RUNE_POINT.EM_RUNE_POINT_COMMON1; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; ++i )
            {
                if (_map.ContainsKey(i + 1))//服务器下发的key从1开始 [4/29/2015 Zmy]
                {
                    try
                    {
                        m_pItem[i].GUID_value = System.Convert.ToInt64(_map[i + 1]);
                        ObjectSelf.GetInstance().CommonItemContainer.OnUpdateRuneEquipState(m_pItem[i], true);
                    }
                    catch (Exception e)
                    {
                        LogManager.LogError("!!!!!!!InitEquipItem Error:" + e);
                    }
                }
                else  //没有在装备的列表里 就默认为false
                {
                    try
                    {
                          ObjectSelf.GetInstance().CommonItemContainer.OnUpdateRuneEquipState(m_pItem[i], false);
                    }
                    catch (Exception e)
                    {
                        
                        LogManager.LogError("!!!!!!!InitEquipItem Error:" + e);
                    }

                }
            }
        }
        private void OnCacheSpellData ( Hero pData )
        {
            this.m_CacheSpellID [ 0 ] = pData.skill1;
            this.m_CacheSpellID [ 1 ] = pData.skill2;
            this.m_CacheSpellID [ 2 ] = pData.skill3;
        }

        private void OnCacheSpellData(SpellData[] spellArray)
        {
            if (spellArray == null || spellArray.Length != 3)
            {
                return;
            }
            this.m_CacheSpellID[0] = spellArray[0].SpellID;
            this.m_CacheSpellID[1] = spellArray[1].SpellID;
            this.m_CacheSpellID[2] = spellArray[2].SpellID;
        }

        public void OnResetSpellData ()
        {
            this.m_SpellData [ 0 ].SpellID = m_CacheSpellID [ 0 ];
            this.m_SpellData [ 1 ].SpellID = m_CacheSpellID [ 1 ];
            this.m_SpellData [ 2 ].SpellID = m_CacheSpellID [ 2 ];
        }
        /// <summary>
        /// 根据符文位置获得符文数据;
        /// </summary>
        /// <param name="erp"></param>
        /// <returns></returns>
        public ItemEquip GetRuneItemInfo ( EM_RUNE_POINT erp )
        {
            int idx = ( int ) erp;

            if ( idx < 0 || idx >= m_pItem.Length )
                return null;

            if ( !m_pItem [ idx ].IsValid () )
                return null;

            return ( ItemEquip ) ObjectSelf.GetInstance ().CommonItemContainer.FindItem ( ( int ) EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, m_pItem [ idx ] );
        }

        /// <summary>
        /// 判断指定符文槽是否装备着符文;
        /// </summary>
        /// <param name="erp"></param>
        /// <returns></returns>
        public bool IsRuneNull ( EM_RUNE_POINT erp )
        {
            return GetRuneItemInfo ( erp ) == null;
        }

        /// <summary>
        /// 判断指定物品是否装备在英雄身上;
        /// </summary>
        /// <param name="itemEquip"></param>
        /// <returns></returns>
        public bool IsItemEquiped ( X_GUID itemGUID )
        {
            if (!itemGUID.IsValid())
            {
                return false;
            }

            for ( int i = 0, j = m_pItem.Length; i < j; i++ )
            {
                if ( m_pItem [ i ] == null )
                    continue;

                if ( m_pItem [ i ].Equals ( itemGUID ) )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 判断指定物品是否装备在英雄身上;
        /// </summary>
        /// <param name="itemEquip"></param>
        /// <returns></returns>
        public bool IsItemEquiped ( ItemEquip itemEquip )
        {
            if ( itemEquip != null && itemEquip.GetItemGuid().IsValid())
            {
                return IsItemEquiped ( itemEquip.GetItemGuid () );
            }

            return false;
        }

        public void ItemChangeSkill ( int nIndex, int nSkillID )
        {
            if ( nIndex < 0 || nIndex >= GlobalMembers.MAX_DB_SPELL_NUM )
            {
                LogManager.LogError ( "!Erro:ItemChangeSkill():Index out Range!!!" );
            }
            if ( m_SpellData [ nIndex ].SpellID == 0 )
            {
                return;
            }
            m_SpellData [ nIndex ].SpellID = nSkillID;
        }
    }
}