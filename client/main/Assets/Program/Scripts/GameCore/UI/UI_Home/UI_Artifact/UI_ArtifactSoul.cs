using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using System;

public class UI_ArtifactSoul : CustomUI
{
    public static UI_ArtifactSoul inst;
    public static string UI_ResPath = "UI_Artifact/UI_Artifact_Soul_2_2";

    int count = 0; // 现在选择的数量
    public int index;
    private int _sortType = 1;              // 1为品质排序 2为等级排序
    private int dirIndex = 0;

    private Button _backBtn;  // 返回按钮
    private GameObject _artifactItem;    // 神器 item
    private Artifact _artifact;         // 神器数据
    private GameObject _heroKnapsack;  // 英雄背包
    private DHGridLayout _heroLayout;
    private GameObject _emptyHero;     // 空英雄
    private UI_SlideBtn _sortSlide;             //滑动组件
    private Text _teamSortType;                 //手动排序类型显示
    private Text _bagNumberText;                         // 背包的数量显示
    private Text _emptyTips;            // 沒有英雄的提示文字
    private Button _empytLeftBtn;                               // 空的时候左边的 button
    private Button _empytRightBtn;                               // 空的时候右边的 button
    private ArtifactItem _item;
    private GameObject _heroObject;                // 英雄的对象

    private Dictionary<int, List<ObjectCard>> _dirHeroList = new Dictionary<int, List<ObjectCard>> ();
    private List<ObjectCard> _heroList = new List<ObjectCard> ();
    private List<ObjectCard> _soulHeroList = new List<ObjectCard> ();
    private List<UI_HeroListItem> _curHeroListItem = new List<UI_HeroListItem> ();
    private List<List<ObjectCard>> _allHero = new List<List<ObjectCard>> ();
    List<ObjectCard> _cardEx = new List<ObjectCard> ();

    public override void InitUIData ()
    {
        inst = this;
        _backBtn = selfTransform.FindChild ( "Title/Btn_back" ).GetComponent<Button> ();
        _backBtn.onClick.AddListener ( new UnityEngine.Events.UnityAction ( OnClickBackBtn ) );
        _artifactItem = selfTransform.FindChild ( "ArtifactItem" ).gameObject;
        _emptyHero = selfTransform.FindChild ( "EmptyHero" ).gameObject;
        _heroKnapsack = selfTransform.FindChild ( "HeroKnapsack" ).gameObject;
        _heroLayout = selfTransform.FindChild ( "HeroKnapsack/HeroList/ListLayOut" ).GetComponent<DHGridLayout> ();
        selfTransform.FindChild ( "HeroKnapsack/SortObj/QualitysortBtn" ).GetComponent<Button> ().onClick.AddListener ( new UnityEngine.Events.UnityAction ( OnQualitysortBtn ) );
        selfTransform.FindChild("HeroKnapsack/SortObj/LevelsortBtn").GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(OnLevelsortBtn));
        _sortSlide = selfTransform.FindChild("HeroKnapsack/SortObj/SortHeroBagBtn").GetComponent<UI_SlideBtn>();
        _teamSortType = selfTransform.FindChild("HeroKnapsack/SortObj/SortHeroBagBtn/Text").GetComponent<Text>();
        _bagNumberText = selfTransform.FindChild("HeroKnapsack/SortObj/NumberNoBg/Number").GetComponent<Text>();
        _emptyTips = selfTransform.FindChild ( "EmptyHero/Text" ).GetComponent<Text> ();
        _empytLeftBtn = selfTransform.FindChild ( "EmptyHero/LeftBtn" ).GetComponent<Button> ();
        _empytRightBtn = selfTransform.FindChild ( "EmptyHero/RightBtn" ).GetComponent<Button> ();
        _empytLeftBtn.onClick.AddListener ( new UnityEngine.Events.UnityAction ( OnClickLeftBtn ) );
        _empytRightBtn.onClick.AddListener ( new UnityEngine.Events.UnityAction ( OnClickRightBtn ) );
        _emptyTips.text = GameUtils.getString ( "relics_bubble3" );
        _heroObject = selfTransform.FindChild ( "HeroKnapsack/HeroList" ).gameObject;

        captionPath = "caption";
    }

    public override void InitUIView ()
    {
        base.InitUIView ();
    }

    // 显示整个神器界面的信息
    public void ShowInfo ( Artifact artifact )
    {
        _artifact = artifact;
        _item = _artifactItem.AddComponent<ArtifactItem> ();
        _item.ShowInfoSoul ( artifact, count );
        _item.ShowHeroData ();

        _item._soulBtn.onClick.RemoveAllListeners ();
        _item._soulBtn.onClick.AddListener ( new UnityEngine.Events.UnityAction ( OnClickSoulBtn ) );

        ReloadHero ();
    }

    // 加载一个英雄
    GameObject LoadCurHero ( int index )
    {
        GameObject cell = null;
        int count = _heroList.Count;
        if ( index < count )
        {
            HeroTemplate heroData = new HeroTemplate ();
            heroData = ( HeroTemplate ) _heroList [ index ].GetHeroRow ();

            cell = Instantiate ( Resources.Load ( "UI/Prefabs/UI_Home/UI_HeroItem" ) ) as GameObject;
            cell.AddComponent<Button> ();

            ObjectCard objHero = _heroList [ index ];
            UI_HeroListItem uiIt = cell.AddComponent<UI_HeroListItem> ();
            _curHeroListItem.Add ( uiIt );
            uiIt.m_id = index;
            uiIt.tableId = objHero.GetHeroData ().TableID;
            uiIt.AddSelectBtnSoul ();
            uiIt.ShowInfo ();

            // 是否正在探险中
            if ( ObjectSelf.GetInstance ().IsInExploring ( objHero.GetHeroData ().GUID ) )
            {
                uiIt.SetExplore ( true );
                uiIt.specialValue = -1;
            }
            else
            {
                uiIt.SetExplore ( false );
                uiIt.specialValue = GetSpecialValue ( objHero, index );

                // 是否已上阵
                if ( isUpFront ( objHero ) )
                {
                    uiIt.ShowUpFront ();
                }
            }
        }
        else
        {
            string url = "UI/Prefabs/UI_Home/UI_HeroItemK2";  //空框修正值
            cell = Instantiate ( Resources.Load ( url ) ) as GameObject;
            cell.transform.FindChild ( "UI_HeroItemK" ).GetComponent<RectTransform> ().anchoredPosition = new Vector2 ( cell.transform.FindChild ( "UI_HeroItemK" ).GetComponent<RectTransform> ().anchoredPosition.x, 41 );
        }
        return cell;
    }

    // 得到特殊的值
    int GetSpecialValue ( ObjectCard card, int index )
    {
        for ( int i = 0; i < _dirHeroList.Count; ++i )
        {
            if ( _dirHeroList.ContainsKey ( i ) )
            {
                List<ObjectCard> cards = _dirHeroList [ i ];
                for ( int j = 0; j < cards.Count; j++ )
                {
                    if ( cards [ j ].GetHeroData ().TableID / 10 == card.GetHeroData ().TableID / 10 )
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }

    // 通过英雄 ID 获取现在的英雄有多少个
    void GetHeroCard ( int heroID, ref List<ObjectCard> cardnors, ref List<ObjectCard> cardUps, ref List<ObjectCard> cardEx )
    {
        int heroid = heroID / 10;
        int level = heroID % 10;

        List<ObjectCard> heroCard = new List<ObjectCard> ();
        heroCard = ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ();

        for ( int i = 0; i < heroCard.Count; ++i )
        {
            int tableID = heroCard [ i ].GetHeroData ().TableID;
            if ( tableID / 10 == heroid && tableID % 10 == level )
            {
                if ( ObjectSelf.GetInstance ().IsInExploring ( heroCard [ i ].GetHeroData ().GUID ) )
                {
                    cardEx.Add ( heroCard [ i ] );
                }
                else
                {
                    if ( isUpFront ( heroCard [ i ] ) )
                    {
                        cardUps.Add ( heroCard [ i ] );
                    }
                    else
                    {
                        cardnors.Add ( heroCard [ i ] );
                    }
                }
            }

            //if (tableID / 10 == heroid && tableID % 10 == level)
            //{

            //}
        }
    }

    // 设置现在的排序方式
    private void SetSortType ( int type )
    {
        _sortType = type;
        if ( _sortType == 1 )
        {
            _teamSortType.text = GameUtils.getString ( "hero_info_sort_quality" );
        }
        else if ( _sortType == 2 )
        {
            _teamSortType.text = GameUtils.getString ( "hero_info_sort_level" );
        }
        else
        {
            _teamSortType.text = GameUtils.getString ( "heromelt_button10" );
        }
    }

    // 添加选择的英雄
    public bool AddSelectHero ( int index, ObjectCard card, int specialValue )
    {
        _soulHeroList.Add ( card );
        count++;
        // 更新 item 的显示效果
        _item.ShowInfoSoul ( _artifact, count );

        // 置灰效果
        CaulHeroNumber ( specialValue, card.GetHeroData ().TableID );

        _item.SoulCount ( card, 1 );
        return true;
    }

    // 移除选择的英雄
    public void RemoveSelectHero ( int index, ObjectCard card, int specialValue )
    {
        _soulHeroList.Remove ( card );
        count--;
        // 更新 item 的显示效果
        _item.ShowInfoSoul ( _artifact, count );
        // 置灰效果
        CaulHeroNumber ( specialValue, card.GetHeroData ().TableID );

        _item.SoulCount ( card, -1 );
    }


    // 计算英雄的数量，并且置灰相同类型的英雄
    void CaulHeroNumber ( int specialValue, int tableID )
    {
        int [] heroID = _artifact.GetArtifactRow ().getHeroID ();
        int [] heroNum = _artifact.GetArtifactRow ().getHeroNum ();

        for ( int i = 0; i < heroID.Length; ++i )
        {
            if ( heroID [ i ] / 10 != tableID / 10 )
                continue;

            // 如果数量大于，则置灰
            if ( GetSelectNum ( heroID [ i ] ) + _artifact.GetArtifactDB ().m_IntoRecord [ specialValue ] >= heroNum [ i ] )
            {
                for ( int j = 0; j < _curHeroListItem.Count; ++j )
                {
                    if ( _curHeroListItem [ j ].specialValue == specialValue )
                        _curHeroListItem [ j ].updateSelectBtn ( true );
                }
            }
            else
            {
                for ( int j = 0; j < _curHeroListItem.Count; ++j )
                {
                    if ( _curHeroListItem [ j ].specialValue == specialValue )
                        _curHeroListItem [ j ].updateSelectBtn ( false );
                }
            }
        }
    }

    // 得到对应 ID 已经选择的英雄的数量
    int GetSelectNum ( int id )
    {
        int count = 0;
        for ( int i = 0; i < _soulHeroList.Count; ++i )
        {
            if ( _soulHeroList [ i ].GetHeroData ().TableID / 10 == id / 10 )
            {
                count++;
            }
        }

        return count;
    }

    ///////////// 按钮回调

    // 返回按钮
    private void OnClickBackBtn ()
    {
        UI_Artifact.inst.UpdateArtiItem ();
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
    }

    //品质排序
    private void OnQualitysortBtn ()
    {
        _sortSlide.OnClose ();
        if ( _sortType != 1 )
        {
            SetSortType ( 1 );
            UpdateArtifactItem();
        }
    }
    //等级排序
    private void OnLevelsortBtn ()
    {
        _sortSlide.OnClose ();
        if ( _sortType != 2 )
        {
            SetSortType ( 2 );
            UpdateArtifactItem();
        }
    }

    // 铸魂按钮
    void OnClickSoulBtn ()
    {
        // 没有选择英雄
        if ( _soulHeroList.Count <= 0 )
        {
            InterfaceControler.GetInst ().AddMsgBox ( GameUtils.getString ( "relics_content13" ), selfTransform.transform.parent );
            return;
        }

        // 是否有上阵英雄
        for ( int i = 0; i < _soulHeroList.Count; i++ )
        {
            if ( isUpFront ( _soulHeroList [ i ] ) )
            {
                // 弹窗提示
                UI_RechargeBox box = UI_HomeControler.Inst.AddUI ( UI_RechargeBox.UI_ResPath ).GetComponent<UI_RechargeBox> ();
                box.SetIsNeedDescription ( false );
                box.SetDescription_text ( GameUtils.getString ( "relics_window1" ) );
                box.SetLeftBtn_text ( GameUtils.getString ( "common_button_ok" ) );
                box.SetLeftClick ( OnClickConfirmBtn );
                box.SetRightBtn_text ( GameUtils.getString ( "common_button_cancel" ) );
                box.SetRightClick ( OnClickCancelBtn );
                return;
            }
        }

        SendMessage ();
    }

    //向服务器发送铸魂消息
    void SendMessage ()
    {
        LinkedList<int> heroKey = new LinkedList<int> ();
        for ( int i = 0; i < _soulHeroList.Count; ++i )
        {
            heroKey.AddLast ( ( int ) _soulHeroList [ i ].GetHeroData ().GUID.GUID_value );
        }

        CArtifactAddHero proto = new CArtifactAddHero ();
        proto.artifacttype = _artifact.GetArtifactRow ().getType ();
        proto.herokeylist = heroKey;
        IOControler.GetInstance ().SendProtocol ( proto );
    }

    // 弹出之后的确定按钮回调
    void OnClickConfirmBtn ()
    {
        SendMessage ();
        UI_HomeControler.Inst.ReMoveUI ( UI_RechargeBox.UI_ResPath );
    }

    void OnClickCancelBtn ()
    {
        // TODO...
        UI_HomeControler.Inst.ReMoveUI ( UI_RechargeBox.UI_ResPath );
    }

    // 一个英雄是否是上阵
    bool isUpFront ( ObjectCard card )
    {
        X_GUID guid = card.GetHeroData ().GUID;
        int index = 0;
        if ( ObjectSelf.GetInstance ().Teams.IsHeroInTeam ( guid, ref index ) )
        {
            return true;
        }
        return false;
    }

    // 铸魂成功之后的操作
    public void SoulSuccess ()
    {
        _soulHeroList.Clear ();
        count = 0;
        ReloadHero ();

        _artifact = null;
        Dictionary<int, Artifact> _map = ObjectSelf.GetInstance ().ArtifactContainerBag.GetArtifactMap ();
        for ( int i = 0; i <= _map.Count; i++ )
        {
            if ( _map.ContainsKey ( i ) )
            {
                if ( index + 1 == i )
                {
                    _artifact = _map [ i ];
                    break;
                }
            }
        }

        ArtifactItem item = _artifactItem.GetComponent<ArtifactItem> ();
        item.ShowInfoSoul ( _artifact, count );
        _item.ShowHeroData ();

        // 当到达满级之后
        if ( _artifact.GetArtifactDB ().GetLevel () == 5 )
        {
            bool ret = false;
            int [] recode = _artifact.GetArtifactDB ().m_IntoRecord;
            int [] heroNum = _artifact.GetArtifactRow ().getHeroNum ();
            for ( int i = 0; i < heroNum.Length; i++ )
            {
                if ( heroNum [ i ] > recode [ i ] )
                {
                    ret = true;
                    break;
                }
            }

            if ( !ret )
            {
                for ( int i = 1; i < _emptyHero.transform.childCount; i++ )
                {
                    _emptyHero.transform.GetChild ( i ).gameObject.SetActive ( false );
                }

                _emptyHero.transform.FindChild ( "TipsText" ).GetComponent<Text> ().text = GameUtils.getString ( "relics_content16" );
                _emptyHero.transform.FindChild ( "TipsText" ).GetComponent<Text> ().gameObject.SetActive ( true );

                return;
            }
        }
    }

    void UpdateArtifactItem()
    {
        _soulHeroList.Clear();
        count = 0;
        ReloadHero();

        _artifact = null;
        Dictionary<int, Artifact> _map = ObjectSelf.GetInstance().ArtifactContainerBag.GetArtifactMap();
        for (int i = 0; i <= _map.Count; i++)
        {
            if (_map.ContainsKey(i))
            {
                if (index + 1 == i)
                {
                    _artifact = _map[i];
                    break;
                }
            }
        }

        ArtifactItem item = _artifactItem.GetComponent<ArtifactItem>();
        item.ShowInfoSoul(_artifact, count);
        _item.ShowHeroData();

        // 当到达满级之后
        if (_artifact.GetArtifactDB().GetLevel() == 5)
        {
            bool ret = false;
            int[] recode = _artifact.GetArtifactDB().m_IntoRecord;
            int[] heroNum = _artifact.GetArtifactRow().getHeroNum();
            for (int i = 0; i < heroNum.Length; i++)
            {
                if (heroNum[i] > recode[i])
                {
                    ret = true;
                    break;
                }
            }

            if (!ret)
            {
                for (int i = 1; i < _emptyHero.transform.childCount; i++)
                {
                    _emptyHero.transform.GetChild(i).gameObject.SetActive(false);
                }

                _emptyHero.transform.FindChild("TipsText").GetComponent<Text>().text = GameUtils.getString("relics_content16");
                _emptyHero.transform.FindChild("TipsText").GetComponent<Text>().gameObject.SetActive(true);

                return;
            }
        }
    }

    // 重新加载英雄
    void ReloadHero ()
    {
        // 清除数据
        _heroLayout.Reload ();
        _heroList.Clear ();
        _curHeroListItem.Clear ();
        _dirHeroList.Clear ();
        _heroObject.SetActive ( true );

        int [] heroID = _artifact.GetArtifactRow ().getHeroID ();
        int [] heroNumebr = _artifact.GetArtifactRow ().getHeroNum ();

        List<List<ObjectCard>> _cardUps = new List<List<ObjectCard>> ();
        _cardEx.Clear ();
        // 获取现在所有的英雄数量
        for ( int i = 0; i < heroID.Length; ++i )
        {
            List<ObjectCard> _cardNors = new List<ObjectCard> ();
            List<ObjectCard> _cardUp = new List<ObjectCard> ();


            // 是否已经注满
            if ( _artifact.GetArtifactDB ().m_IntoRecord [ i ] >= heroNumebr [ i ] )
            {

            }
            else
            {
                int level = heroID [ i ] % 10;
                int heroid = heroID [ i ] / 10 * 10;
                for ( int j = heroid + level; j < heroid + 9; j++ )
                {
                    GetHeroCard ( j, ref _cardNors, ref _cardUp, ref _cardEx );
                }

                if ( _sortType == 1 )
                {
                    SortHeroWithQuailty ( ref _cardNors );
                    SortHeroWithQuailty ( ref _cardUp );
                    SortHeroWithQuailty ( ref _cardEx );
                }
                else
                {
                    SortHeroWithLevel ( ref _cardNors );
                    SortHeroWithLevel ( ref _cardUp );
                    SortHeroWithLevel ( ref _cardEx );
                }
                for ( int j = 0; j < _cardNors.Count; j++ )
                {
                    _heroList.Add ( _cardNors [ j ] );
                }
            }

            _cardUps.Add ( _cardUp );
            _dirHeroList.Add ( i, _cardNors );
        }

        // 对上阵英雄进行帅选		
        for ( int i = 0; i < _cardUps.Count; ++i )
        {
            List<ObjectCard> cards = new List<ObjectCard> ();
            cards = _cardUps [ i ];
            for ( int j = 0; j < cards.Count; j++ )
            {
                _heroList.Add ( _cardUps [ i ] [ j ] );
                _dirHeroList [ i ].Add ( _cardUps [ i ] [ j ] );
            }
        }

        // 对探险英雄进行筛选
        for ( int i = 0; i < _cardEx.Count; ++i )
        {
            _heroList.Add ( _cardEx [ i ] );
        }

        if ( _heroList.Count <= 0 )
        {
            _emptyHero.SetActive ( true );
            _heroObject.SetActive ( false );
        }
        else
        {
            int count = _heroList.Count;
            if ( count % _heroLayout.constraintCount == 0 )
            {
                _heroLayout.cellCount = count;
            }
            else
            {
                _heroLayout.cellCount = ( ( count / _heroLayout.constraintCount ) + 1 ) * _heroLayout.constraintCount;
            }

            if ( _heroLayout.cellCount < 9 )
            {
                _heroLayout.cellCount = 9;
            }
            _heroLayout.loadCell = LoadCurHero;
        }

        UpdateBagItem ();
    }

    // 
    void OnClickLeftBtn ()
    {
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
        UI_HomeControler.Inst.ReMoveUI ( UI_Artifact.UI_ResPath );
        UI_HomeControler.Inst.AddUI ( UI_Recruit.UI_ResPath );
    }

    // 
    void OnClickRightBtn ()
    {
        UI_HomeControler.Inst.ReMoveUI ( UI_Artifact.UI_ResPath );
        UI_HomeControler.Inst.ReMoveUI ( gameObject );
        UI_HomeControler.Inst.AddUI ( UI_SelectLevelMgrNew.UI_ResPath );
        //UI_HomeControler.Inst.AddUI(UI_SelectFightArea.UI_ResPath);
    }

    // 更新背包数量显示
    void UpdateBagItem ()
    {
        var player = ObjectSelf.GetInstance ();
        if ( player.CommonItemContainer.GetBagItemSum () <= player.HeroContainerBag.GetHeroBagSizeMax () )
        {
            _bagNumberText.text = "<color=#f3863a>" + _heroList.Count + "</color>/" + player.HeroContainerBag.GetHeroBagSizeMax ();
        }
        else
        {
            _bagNumberText.text = "<color=red>" + _heroList.Count + "</color>/" + player.HeroContainerBag.GetHeroBagSizeMax ();
        }
    }

    // 根据品质排序英雄
    void SortHeroWithQuailty ( ref List<ObjectCard> heroList )
    {
        for ( int i = 0; i < heroList.Count - 1; ++i )
        {
            ObjectCard item = heroList [ i ];
            for ( int j = i; j < heroList.Count; ++j )
            {
                HeroTemplate hero1 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ i ].GetHeroData ().TableID );
                int quality1 = hero1.getQuality ();

                HeroTemplate hero2 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ j ].GetHeroData ().TableID );
                int quality2 = hero2.getQuality ();

                if ( quality2 < quality1 )
                {
                    item = heroList [ j ];
                    heroList [ j ] = heroList [ i ];
                    heroList [ i ] = item;
                }
                else if ( quality2 == quality1 )
                {
                    int level1 = heroList [ i ].GetHeroData ().Level;
                    int level2 = heroList [ j ].GetHeroData ().Level;

                    if ( level2 < level1 )
                    {
                        item = heroList [ j ];
                        heroList [ j ] = heroList [ i ];
                        heroList [ i ] = item;
                    }
                    else if ( level2 == level1 )
                    {
                        HeroTemplate hero3 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ i ].GetHeroData ().TableID );
                        int count1 = hero3.getReturnBack ();
                        count1 += ( int ) ( ( float ) hero1.getExpNum () * DataTemplate.GetInstance ().m_GameConfig.getRongling_conversion_rate () / ( float ) DataTemplate.GetInstance ().m_GameConfig.getJingyanjiejing_to_jingyan () );

                        HeroTemplate hero4 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ j ].GetHeroData ().TableID );
                        int count2 = hero4.getReturnBack ();
                        count2 += ( int ) ( ( float ) hero1.getExpNum () * DataTemplate.GetInstance ().m_GameConfig.getRongling_conversion_rate () / ( float ) DataTemplate.GetInstance ().m_GameConfig.getJingyanjiejing_to_jingyan () );

                        if ( count2 > count1 )
                        {
                            item = heroList [ j ];
                            heroList [ j ] = heroList [ i ];
                            heroList [ i ] = item;
                        }
                    }
                }
            }
        }
    }

    // 根据等级排序英雄
    void SortHeroWithLevel ( ref List<ObjectCard> heroList )
    {
        for ( int i = 0; i < heroList.Count - 1; ++i )
        {
            ObjectCard item = heroList [ i ];
            for ( int j = i; j < heroList.Count; ++j )
            {
                int level1 = heroList [ i ].GetHeroData ().Level;
                int level2 = heroList [ j ].GetHeroData ().Level;

                if ( level2 < level1 )
                {
                    item = heroList [ j ];
                    heroList [ j ] = heroList [ i ];
                    heroList [ i ] = item;
                }
                else if ( level2 == level1 )
                {
                    HeroTemplate hero1 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ i ].GetHeroData ().TableID );
                    int quality1 = hero1.getQuality ();

                    HeroTemplate hero2 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ j ].GetHeroData ().TableID );
                    int quality2 = hero2.getQuality ();

                    if ( quality2 < quality1 )
                    {
                        item = heroList [ j ];
                        heroList [ j ] = heroList [ i ];
                        heroList [ i ] = item;
                    }
                    else if ( quality2 == quality1 )
                    {
                        HeroTemplate hero3 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ i ].GetHeroData ().TableID );
                        int count1 = hero3.getReturnBack ();
                        count1 += ( int ) ( ( float ) hero1.getExpNum () * DataTemplate.GetInstance ().m_GameConfig.getRongling_conversion_rate () / ( float ) DataTemplate.GetInstance ().m_GameConfig.getJingyanjiejing_to_jingyan () );

                        HeroTemplate hero4 = ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( heroList [ j ].GetHeroData ().TableID );
                        int count2 = hero4.getReturnBack ();
                        count2 += ( int ) ( ( float ) hero1.getExpNum () * DataTemplate.GetInstance ().m_GameConfig.getRongling_conversion_rate () / ( float ) DataTemplate.GetInstance ().m_GameConfig.getJingyanjiejing_to_jingyan () );

                        if ( count2 < count1 )
                        {
                            item = heroList [ j ];
                            heroList [ j ] = heroList [ i ];
                            heroList [ i ] = item;
                        }
                    }
                }
            }
        }
    }


}
