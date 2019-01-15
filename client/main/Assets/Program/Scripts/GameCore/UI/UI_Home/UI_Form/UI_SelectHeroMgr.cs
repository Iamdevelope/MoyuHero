using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameEventSystem;

public class UI_SelectHeroMgr : BaseUI
{
    public static string UI_ResPath = "UI_Form/UI_SelectHero_2_2";
    private static UI_SelectHeroMgr _inst;

    private List<ObjectCard> m_HeroList = null;
    public List<GameObject> m_TeamBtns = new List<GameObject>();

    private ObjectCard m_Card = null;                                             //当前选择的卡牌
    private int m_SelectPos = -1;                                                 //当前选择的位置0是前排 1是后排
    private int m_SelectNo = -1;                                                  //当前选择在队伍中的编号

    private Button m_BackBtn = null;
    private Transform m_HeroListOBJ = null;
    private LoopLayout m_LoopLayout = null;

    private bool isGoToForm = false; 

    public static UI_SelectHeroMgr Inst 
    { 
        get { return _inst; } 
    }
    public ObjectCard GetCard()
    {
        return m_Card;
    }

    public void SetCard(ObjectCard card)
    {
        m_Card = card;
    }
    public int SelectPos
    {
        get { return m_SelectPos; }
    }

    /// <summary>
    /// 设置当前的英雄数据
    /// </summary>
    /// <param name="card">卡牌数据</param>
    /// <param name="pos">在队伍是前排还是后排</param>
    /// <param name="index">在队伍中的索引位置</param>
    public void SetSelectHeoData(ObjectCard card,int pos,int no)
    {
        this.m_Card = card;
        this.m_SelectPos = pos;
        this.m_SelectNo = no;
    }
    public override void InitUIData()
    {
        base.InitUIData();

        _inst = this;

        m_HeroListOBJ = selfTransform.FindChild("RightPanel/HerolLsit/ListLayout");
        m_LoopLayout = m_HeroListOBJ.GetComponent<LoopLayout>();

        m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityAction(onBackCall));

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_Formation_Update, EventUpadateUI);
    }

    /// <summary>
    /// 更新UI界面
    /// </summary>
    public  void UpdateUIShow()
    {
        //isGoToForm = true;
        InitHeroData();
        ShowTeamBtns();
    }

    private void EventUpadateUI()
    {
        onClose();
        UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        InitHeroData();
        //ShowTeamBtns();
    }

    /// <summary>
    /// 初始化英雄列表
    /// </summary>
    private void InitHeroData()
    {
        m_HeroList = new List<ObjectCard>();
        List<ObjectCard> temp = new List<ObjectCard>();
        List<ObjectCard> objCardList = ObjectSelf.GetInstance().HeroContainerBag.GetYetFormList(ref temp);

        objCardList.Sort(new HeroComparer());
        for (int i = 0; i < objCardList.Count; i++)
        {
            m_HeroList.Add(objCardList[i]);
        }

        temp.Sort(new HeroComparer());
        for (int i = 0; i < temp.Count; i++)
        {
            m_HeroList.Add(temp[i]);
        }

        m_LoopLayout.cellCount = m_HeroList.Count;
        m_LoopLayout.updateCellEvent = UpdateLoadHeroItem;
        m_LoopLayout.Reload();

    }

    /// <summary>
    /// 动态更新列表显示
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cell"></param>
    private void UpdateLoadHeroItem(int index, RectTransform cell)
    {
        ObjectCard obj = m_HeroList[index];
        UI_TeamHeroItem uiIt = cell.gameObject.GetComponent<UI_TeamHeroItem>();
        if (uiIt == null)
        {
            //cell.gameObject.AddComponent<Button>();
            uiIt = cell.gameObject.AddComponent<UI_TeamHeroItem>();
            //heroList.Add(uiIt);
        }
        uiIt.index = index;
        uiIt.InitHeroCardUI(obj);

    }

    /// <summary>
    /// 显示阵型按钮
    /// </summary>
    private void ShowTeamBtns()
    {
        int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        for (int i = 0; i < HeroCount; ++i)
        {
            ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);

            UI_TeamBtnItem uiTeamBtnItem = null;
            if (m_TeamBtns[i].GetComponent<UI_TeamBtnItem>() != null)
                uiTeamBtnItem = m_TeamBtns[i].GetComponent<UI_TeamBtnItem>();
            else
                uiTeamBtnItem = m_TeamBtns[i].gameObject.AddComponent<UI_TeamBtnItem>();

            uiTeamBtnItem.InitData(temp,m_SelectNo,CurUI.SelectHero);
        }
    }


    /// <summary>
    /// 发送下阵消息
    /// </summary>
    /// <param name="troopid"></param>
    public void SendDownProtocol(int troopid/*, int FormationNum*/)
    {
        CAddTroop battle = new CAddTroop();
        battle.trooptype = ObjectSelf.GetInstance().Teams.GetFormationType();
        battle.herokey = 0;
        battle.troopid = troopid;
        battle.locationid = m_SelectNo;
        IOControler.GetInstance().SendProtocol(battle);
    }

    /// <summary>
    /// 上阵或者换人
    /// </summary>
    /// <param name="troopid">阵型编号</param>
    /// <param name="FormationNum">上阵编号</param> 
    public void SendProtocol(int troopid/*, int FormationNum*/,int herokey)
    {
        CAddTroop battle = new CAddTroop();
        battle.trooptype = ObjectSelf.GetInstance().Teams.GetFormationType();
        battle.herokey = herokey;
        battle.troopid = troopid;
        battle.locationid = m_SelectNo;
        IOControler.GetInstance().SendProtocol(battle);
    }

    
    /// <summary>
    /// 返回按钮回调
    /// </summary>
    private void onBackCall()
    {
        onClose();

        if (/*isGoToForm || */m_Card == null)
        {
            UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath);
        }
        else
        {
            GameObject go = UI_HomeControler.Inst.AddUI(UI_RepartoMgr.UI_ResPath);
            UI_RepartoMgr uiRepartoMgr = go.GetComponent<UI_RepartoMgr>();
            uiRepartoMgr.SetSelectHeoData(m_Card, m_SelectPos, m_SelectNo);
        }
    }

    /// <summary>
    /// 关闭当前窗口
    /// </summary>
    public void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Formation_Update, EventUpadateUI);
    }



}
