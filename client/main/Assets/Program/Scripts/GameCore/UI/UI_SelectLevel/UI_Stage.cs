using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.UI;
using DreamFaction.GameNetWork;
using GNET;

using DreamFaction.GameEventSystem;
public class UI_Stage : UI_StageBase 
{
    public static readonly string UI_Res = "UI_Level/UI_StageInfo_2_1";
    private static UI_Stage mInst = null;

    protected Image[] StarImgs = new Image[3];

    protected Transform EnermyListTrans = null;     //敌方阵容;
    protected Transform ItemListTrans = null;       //获得物品;
    protected GameObject ResouceObj3 = null;

    private StageTemplate mStageT = null;
    private List<UniversalItemCell> mEnermyList = new List<UniversalItemCell>();
    private List<UniversalItemCell> mItemsList = new List<UniversalItemCell>();

    public static UI_Stage Inst
    {
        get
        {
            return mInst;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        for (int i = 0, j = StarImgs.Length; i < j; i++ )
        {
            StarImgs[i] = selfTransform.FindChild("Panel/TopObj/Stars/StarsFg/Image" + i).GetComponent<Image>();
        }
        
        EnermyListTrans = selfTransform.FindChild("Panel/LeftObj/Enemys/List/Grid");
        ItemListTrans = selfTransform.FindChild("Panel/LeftObj/Items/List/Grid");

        ResouceObj3 = selfTransform.FindChild("Panel/LeftObj/Rewards/Grid/Resource3").gameObject;

        mInst = this;

        GameEventDispatcher.Inst.addEventListener(GameEventID.U_RapidClearRespond, RapidClearRespondHandler);
        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_StageDataRefresh, OnStageDataRefreshed);
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    public override void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_RapidClearRespond, RapidClearRespondHandler);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_StageDataRefresh, OnStageDataRefreshed);

        mStageT = null;
        mEnermyList.Clear();
        mEnermyList = null;
        mItemsList.Clear();
        mItemsList = null;

        mInst = null;
    }

    public void Show(StageTemplate stageT)
    {
        if (stageT == null)
        {
            Debug.LogError("关卡数据为null");
            return;
        }

        mStageT = stageT;

        //-----------------星级----------------;
        StageData sd = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(stageT.m_stageid);
        int starNum = 0;
        if (sd != null)
        {
            starNum = sd.m_StageStar;
        }

        for (int i = 0; i < 3; i++ )
        {
            StarImgs[i].gameObject.SetActive(i < starNum);
        }

        int stageNum = StageModule.GetStageNumInChapter(stageT);
        int chapterId = StageModule.GetChapterinfoIdByStageId(stageT.GetID());

        m_ChapterNameTxt.text = string.Format(GameUtils.getString("chapter_title"), GameUtils.ConverIntToString(chapterId));
        m_StageNameTxt.text = string.Format(GameUtils.getString("guanqia_mingzi"), stageNum, GameUtils.getString(stageT.m_stagename));
        m_StageDescTxt.text = GameUtils.getString(stageT.m_stageinfo);

        m_ResourceCount1.text = stageT.m_playerexp.ToString();
        m_ResourceCount2.text = stageT.m_goldreward.ToString();

        if (stageT.m_expcrystal == -1)
        {
            ResouceObj3.SetActive(false);
        }
        else
        {
            ResouceObj3.SetActive(true);
            m_ResourceCount3.text = stageT.m_expcrystal.ToString();
        }

        EM_STAGE_DIFFICULTTYPE difficultType = StageModule.GetStageDifficultType(stageT);
        string difficultStr = "";
        switch (difficultType)
        {
            case EM_STAGE_DIFFICULTTYPE.NONE:
                break;
            case EM_STAGE_DIFFICULTTYPE.NORMAL:
                difficultStr = GameUtils.getString("chapter_difficult_type1");
                break;
            case EM_STAGE_DIFFICULTTYPE.HARD:
                difficultStr = GameUtils.getString("chapter_difficult_type2");
                break;
            case EM_STAGE_DIFFICULTTYPE.HARDEST:
                difficultStr = GameUtils.getString("chapter_difficult_type2");
                break;
            default:
                break;
        }

        //-------------------关卡挑战次数--------------------
        //无限制;
        if (stageT.m_limittime < 0)
        {
            SetRapidObjActive(false);
        }
        else
        {
            SetRapidObjActive(true);
            int remineTimes = Mathf.Max(0, stageT.m_limittime - sd.m_FightSum);
            
            TEXT_COLOR tc = TEXT_COLOR.WHITE;
            if (remineTimes > 0)
            {
                tc = TEXT_COLOR.WHITE;
                m_ResetBtn.gameObject.SetActive(false);
            }
            else
            {
                m_ResetBtn.gameObject.SetActive(true);
                tc = TEXT_COLOR.RED;
            }
            m_RemindTimeTxt.text = GameUtils.getString("fight_fightprepare_content3") + GameUtils.StringWithColor(remineTimes.ToString(), tc) + "/" + stageT.m_limittime;
        }

        m_DifficultTxt.text = difficultStr;

        //行动力不足颜色标红;
        if (isEnoughPow(mStageT))
        {
            m_ConsumeCountTxt.text = stageT.m_cost.ToString();
        }
        else
        {
            m_ConsumeCountTxt.text = GameUtils.StringWithColor(stageT.m_cost.ToString(), TEXT_COLOR.RED);
        }

        switch (stageT.m_winCondition)
        {
            case 1:
                m_SucessConditionTxt.text = "◆" + GameUtils.getString("System_setting_content28");
                break;
            case 2:
                m_SucessConditionTxt.text = "◆" + string.Format(GameUtils.getString("System_setting_content29"), StageModule.GetBossName(stageT));
                break;
        }
        //--------------------敌方英雄信息------------------;
        for (int i = 0; i < mEnermyList.Count; i++ )
        {
            if (mEnermyList[i] != null)
            {
                mEnermyList[i].Destroy();
            }
        }
        mEnermyList.Clear();

        List<MonsterTemplate> _BossTemp = new List<MonsterTemplate>();
        List<MonsterTemplate> _MonsterTemp = new List<MonsterTemplate>();
        for (int i = 0, j = stageT.m_displayMonster.Length; i < j; i++)
        {
            MonsterTemplate _monster = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(stageT.m_displayMonster[i]);
            if (_monster.getMonstertype() == 2)
            {
                _BossTemp.Add(_monster);
            }
            else
            {
                _MonsterTemp.Add(_monster);
            }
        }

        for (int i = 0; i < _BossTemp.Count; ++i)
        {
            UniversalItemCell cell = UniversalItemCell.GenerateItem(EnermyListTrans);
            cell.InitByID(_BossTemp[i].getId());
            cell.SetSize(UniversalItemCell.UniversalItemSize.Type_114);
            cell.SetCount("BOSS");
            cell.AddClickListener(OnClickUniversalHeroHandler);
            mEnermyList.Add(cell);
        }

        for (int i = 0; i < _MonsterTemp.Count; i++)
        {
            UniversalItemCell cell = UniversalItemCell.GenerateItem(EnermyListTrans);
            cell.InitByID(_MonsterTemp[i].getId());
            cell.SetSize(UniversalItemCell.UniversalItemSize.Type_114);
            cell.AddClickListener(OnClickUniversalHeroHandler);
            //cell.SetCount("BOSS");
            mEnermyList.Add(cell);
        }

        //--------------------关卡掉落展示------------------;
        for (int i = 0; i < mItemsList.Count; i++)
        {
            if (mItemsList[i] != null)
            {
                mItemsList[i].Destroy();
            }
        }
        mItemsList.Clear();

        string displaydrop = stageT.m_displaydrop;
        if (displaydrop == "-1" || string.IsNullOrEmpty(displaydrop))
        {
            
        }
        else
        {
            string[] displaydropList = displaydrop.Split('#');
            if (displaydropList.Length == 0)
                return;

            for (int i = 0; i < displaydropList.Length; i++)
            {
                UniversalItemCell cell = UniversalItemCell.GenerateItem(ItemListTrans);
                cell.SetSize(UniversalItemCell.UniversalItemSize.Type_114);
                cell.AddClickListener(OnClickUniversalItemHandler);
                string[] itemList = displaydropList[i].Split('-');
                switch (int.Parse(itemList[0]))
                {
                    case 1:
                        int itemid = int.Parse(itemList[1]);
                        if (int.Parse(itemList[2]) == 0)
                        {
                            //几率掉落;
                            cell.InitByID(itemid);
                            cell.SetText(null, GameUtils.getString("fight_stageselect_content3"), null);
                        }
                        else
                        {
                            //一定掉落;
                            int count = System.Convert.ToInt32(itemList[3]);
                            cell.InitByID(itemid, count);
                        }
                        break;
                    case 2:
                        if (int.Parse(itemList[2]) == 0)
                        {
                            cell.InitBySprite(UIResourceMgr.LoadSprite(common.defaultPath + itemList[1]));
                            cell.SetText(null, GameUtils.getString("fight_stageselect_content3"), null);
                        }
                        else
                        {
                            int count = System.Convert.ToInt32(itemList[3]);
                            cell.InitBySprite(UIResourceMgr.LoadSprite(common.defaultPath + itemList[1]), count);
                        }
                        break;
                    default:
                        break;
                }

                mItemsList.Add(cell);
            }
        }
    }

    protected override void OnClickBackBtn()
    {
        base.OnClickBackBtn();
        UI_HomeControler.Inst.ReMoveUI(UI_Res);
    }

    protected override void OnClickFormationBtn()
    {
        base.OnClickFormationBtn();

        UI_HomeControler.Inst.AddUI(UI_FormMgr.UI_ResPath); 
    }

    protected override void OnClickResetBtn()
    {
        base.OnClickResetBtn();

        BuyResetHandler();
    }
    private void OnClickUniversalHeroHandler(int id)
    {
        UICommonManager.Inst.ShowCommon(id);
    }
    private void OnClickUniversalItemHandler(int id)
    {
//         //先判断是否是碎片;
//         if (GameUtils.GetItemTypeById(id) == EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT)
//         {
//             UICommonManager.Inst.ShowHeroFragment(id);
//         }
//         else
//         {

        EM_OBJECT_CLASS type = GameUtils.GetObjectClassById(id);
        switch (type)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER:
                UICommonManager.Inst.ShowCommon(id);
                break;
            default:
                UICommonManager.Inst.ShowHeroObtain(id);
                break;
        }
//         }
    }

    private void OnStageDataRefreshed()
    {
        if (mStageT != null)
        {
            //-----次数刷新--------
            if (mStageT.m_limittime < 0)
            {
                SetRapidObjActive(false);
            }
            else
            {
                SetRapidObjActive(true);
                StageData sd = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(mStageT.GetID());
                int remineTimes = Mathf.Max(0, mStageT.m_limittime - sd.m_FightSum);

                TEXT_COLOR tc = TEXT_COLOR.WHITE;
                if (remineTimes > 0)
                {
                    tc = TEXT_COLOR.WHITE;
                    m_ResetBtn.gameObject.SetActive(false);
                }
                else
                {
                    m_ResetBtn.gameObject.SetActive(true);
                    tc = TEXT_COLOR.RED;
                }
                m_RemindTimeTxt.text = GameUtils.getString("fight_fightprepare_content3") + GameUtils.StringWithColor(remineTimes.ToString(), tc) + "/" + mStageT.m_limittime;
            }
        }
    }

    private void RapidClearRespondHandler(GameEvent e)
    {
        if (e.data == null)
        {
            //Debug.Log("奖励物品数量:"+((List<BattelInfo>)e.data).Count);
            return;
        }
        UI_BattleendPanel temp = null;
        if (UI_BattleendPanel._inist == null)
        {
            GameObject go = UI_HomeControler.Inst.AddUI(UI_BattleendPanel.UI_Res);
            if (temp == null)
            {
                temp = go.AddComponent<UI_BattleendPanel>();
            }
            else
            {
                temp = go.GetComponent<UI_BattleendPanel>();
            }
        }
        else
        {
            temp = UI_BattleendPanel._inist;
        }
        //关卡表目前是写死的;
        StageTemplate stageinfo = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(ObjectSelf.GetInstance().CurStageID);
        temp.SetSweepCostResourceType(1400010001, stageinfo.m_cost);
        temp.setType(UI_BattleendPanel.PanelType.Clear);
        temp.ClearUpdate((List<BattelInfo>)e.data);
        temp.enabled = true;
    }

    protected override void OnClickWipeOutTenBtn()
    {
        base.OnClickWipeOutTenBtn();

        ObjectSelf obj = ObjectSelf.GetInstance();
        if (obj == null)
            return;

        int limitVipLv = VIPModule.GetStageMopupVipLv();

        if (limitVipLv > obj.VipLevel)
        {
            InterfaceControler.GetInst().AddMsgBox("VIP" + limitVipLv + "开启该功能");
            return;
        }

        //没3星通关;
        if (RefreshStateInfo())
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips1"), selfTransform);
            return;
        }

        WipeOutHandler(10);
    }

    public override void OnClickWipeOutOneBtn()
    {
        base.OnClickWipeOutOneBtn();

        ObjectSelf obj = ObjectSelf.GetInstance();
        if (obj == null)
            return;

        //没3星通关;
        if (RefreshStateInfo())
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("UI_raids_tips1"), selfTransform);
            return;
        }

        WipeOutHandler(1);
    }

    private void WipeOutHandler(int clearTimes)
    {
        ObjectSelf obj = ObjectSelf.GetInstance();

        //是否有关卡挑战次数;
        if (!isEnoughFightCounts())
        {
            //重置逻辑;
            BuyResetHandler();
        }
        else if (!isEnoughHero())
        {
            string text = GameUtils.getString("fight_fightprepare_tip1");
            InterfaceControler.GetInst().AddMsgBox(text, selfTransform);
            return;
        }
        else if (!isEnoughPow(mStageT))
        {
            UI_HomeControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
        }
        else if (!isEnoughCount(mStageT))
        {
            UI_HomeControler.Inst.AddUI(UI_MaxFightManage.UI_ResPath);
        }
        else
        {
            ObjectSelf.GetInstance().SetOldHeroPlayer();
            CSweepBattle battle = new CSweepBattle();
            battle.battleid = ObjectSelf.GetInstance().CurStageID;
            battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            battle.num = (byte)clearTimes;
            IOControler.GetInstance().SendProtocol(battle);
        }
    }

    private void BuyResetHandler()
    {
        ObjectSelf obj = ObjectSelf.GetInstance();
        int reminResetTimes = isEnoughResetCount();
        //是否有购买重置次数;
        if (reminResetTimes > 0)
        {
            //UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            //int vipLevel = ObjectSelf.GetInstance().VipLevel;
            //StringBuilder sb = new StringBuilder();
            //sb.Append(GameUtils.getString("fight_stagepurchase_form_content"));
            //sb.Append(string.Format("<size=40><color=#F7F709> {0}</color></size>", ObjectSelf.GetInstance().RapidClearBuyTimes));
            //box.SetDescription_text(sb.ToString());
            //box.SetLeftClick(OnBuyRapidClick);
            //int curGold = ObjectSelf.GetInstance().Gold;
            //box.SetMoneyInfo((int)EM_RESOURCE_TYPE.Gold, curGold);
            //box.SetMoneyInfoActive(true);
            //int buyTimes = VIPModule.GetBuyStageMopupTimes(vipLevel) - ObjectSelf.GetInstance().RapidClearBuyTimes;
            //int cost = StageModule.GetBuyRapidCost(buyTimes);
            //box.SetConNum(cost + "");
            //UI_RechargeBox.Data = curGold >= cost;
            //box.SetConsume_Image(GameUtils.GetSpriteByResourceType(EM_RESOURCE_TYPE.Gold));
            //box.SetLeftBtn_text(GameUtils.getString("common_button_purchase"));
            long resCount = -1;
            if (obj.TryGetResourceCountById(EM_RESOURCE_TYPE.Gold, ref resCount))
            {
                int vipLevel = ObjectSelf.GetInstance().VipLevel;
                VipTemplate pRow = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLevel);

                StageData sd = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(mStageT.GetID());
                int buyCostIdx = -1;
                buyCostIdx = Mathf.Max(0, sd.m_BuyBattleNum);
                buyCostIdx = Mathf.Min(pRow.getResetcost().Length - 1, buyCostIdx);
                int cost = pRow.getResetcost()[buyCostIdx];
                bool isGoldEnough = resCount >= cost;
                TEXT_COLOR tc = isGoldEnough ? TEXT_COLOR.WHITE : TEXT_COLOR.RED;
                UICommonManager.Inst.ShowMsgBox(
                    GameUtils.getString("maoxianguanka8"),
                    string.Format(GameUtils.getString("maoxianguanka9"), reminResetTimes),
                    string.Format(GameUtils.getString("maoxianguanka10"),GameUtils.StringWithColor(cost.ToString(), tc)),
                    GameUtils.getString("huoli_tips5"),
                    OnBuyResetBtnClick,
                    null,
                    isGoldEnough
                    );
            }
            else
            {
                Debug.LogError("不存在的资源类型" + EM_RESOURCE_TYPE.Gold);
            }

            return;
        }
        else
        {
            //VIP等级是否满级15级;
            if (obj.VipLevel >= 15)
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("maoxianguanka27"), selfTransform);
                return;
            }
            else
            {
                UICommonManager.Inst.ShowMsgBox(
                    GameUtils.getString("maoxianguanka8"),
                    string.Format(GameUtils.getString("huoli_tips4"), 0),
                    GameUtils.getString("huoli_tips6"),
                    GameUtils.getString("huoli_tips7"),
                    OnShowVipBtnClick,
                    null
                    );

                return;
            }
        }
    }

    protected override void OnClickStartFightBtn()
    {
        base.OnClickStartFightBtn();

        bool isStageScene = !(ObjectSelf.GetInstance().IsLimitFight
            || ObjectSelf.GetInstance().IsInWorldBoss);
        ObjectSelf.GetInstance().SetChangeLevel(isStageScene);

        if (!isEnoughHero())
        {
            return;
        }
        else if (!isEnoughPow(mStageT))
        {
            UI_HomeControler.Inst.AddUI(UI_PowersAdd.UI_ResPath);
        }
        else if (!isEnoughCount(mStageT))
        {
            UI_HomeControler.Inst.AddUI(UI_MaxFightManage.UI_ResPath);
        }
        else
        {
            var objSelf = ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.GetWeek() == objSelf.Week)
                {
                    CBeginBattle battle = new CBeginBattle();
                    battle.battleid = ObjectSelf.GetInstance().CurStageID;
                    battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                    IOControler.GetInstance().SendProtocol(battle);
                }
                else
                {
                    objSelf.SetPromptFome(true);
                    objSelf.SetPromptTime(true);
                    SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
                }
            }
            else
            {
                CBeginBattle battle = new CBeginBattle();
                battle.battleid = ObjectSelf.GetInstance().CurStageID;
                battle.troopid = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                IOControler.GetInstance().SendProtocol(battle);
            }
        }
    }


    void OnBuyRapidClick()
    {
        UI_RechargeBox.Inst.OnCloes();
        //钱够不够;
        bool isEnougth = (bool)UI_RechargeBox.Data;

        if (isEnougth)
        {
            CBuyStateBattleNum csbn = new CBuyStateBattleNum();
            csbn.buytype = 1;
            IOControler.GetInstance().SendProtocol(csbn);
        }
        else
        {
            InterfaceControler.GetInst().ShowGoldNotEnougth();
        }
    }

    void OnBuyResetBtnClick(object data)
    {
        bool isEnougth = (bool)data;

        if (isEnougth)
        {
            CBuyStateBattleNum csbn = new CBuyStateBattleNum();
            csbn.buytype = 2;
            csbn.battleid = mStageT.GetID();
            IOControler.GetInstance().SendProtocol(csbn);
        }
        else
        {
            InterfaceControler.GetInst().ShowGoldNotEnougth();
        }
    }

    void OnShowVipBtnClick(object data)
    {
        UI_HomeControler.Inst.AddUI(UI_VipPrivilege.GetPath(true));
    }

    /// <summary>
    /// 没有三星通关-----扫荡前提;
    /// </summary>
    /// <returns></returns>
    public bool RefreshStateInfo()
    {
        bool result = true;
        ObjectSelf obj = ObjectSelf.GetInstance();
        BattleStage stage = null;
        if (obj.GetIsPrompt())
        {
            stage = obj.BattleStageData.m_BattleStageList[1001];
        }
        else
        {
            stage = obj.BattleStageData.m_BattleStageList[obj.GetCurChapterID()];
        }
        StageData stageData = stage.GetStageData(obj.CurStageID);
        result = stageData == null || stageData.m_StageStar < 3;

        return result;
    }

    private void SetRapidObjActive(bool active)
    {
        m_RemindTimeTxt.gameObject.SetActive(active);
        m_ResetBtn.gameObject.SetActive(active);
    }

    /// <summary>
    /// 关卡挑战次数是否足够;
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    private bool isEnoughFightCounts()
    {
        StageData obj = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(mStageT.GetID());

        if (obj != null)
        {
            return obj.m_FightSum < mStageT.m_limittime || mStageT.m_limittime < 0;
        }
    
        return false;
    }

    /// <summary>
    /// 当前关卡是否有重置次数-- 返回剩余次数;
    /// </summary>
    /// <returns></returns>
    public int isEnoughResetCount()
    {
        StageData obj = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(mStageT.GetID());

        if (obj != null)
        {
            int vipLevel = ObjectSelf.GetInstance().VipLevel;
            VipTemplate pRow = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLevel);

            if (pRow != null)
            {
                return pRow.getStageResetBuyTimes() - obj.m_BuyBattleNum;
            }
        }

        return -1;
    }

    private bool isEnoughHero()
    {
        int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
        
        for (int i = 0; i < HeroCount; ++i)
        {
            ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
            if (temp == null)
                continue;

            return true;
        }
            
        return false;
    }

    private bool isEnoughPow(StageTemplate stageT)
    {
        int iPower = ObjectSelf.GetInstance().ActionPoint;
        
        if (iPower >= stageT.m_cost)
        {
            return true;
        }

        return false;
    }

    //挑战次数是否足够(每个关卡都会配置上挑战次数限制的，不会出现无限次的挑战次数的);
    private bool isEnoughCount(StageTemplate stageT)
    {
        //不限制;
        if (stageT.m_limittime < 0)
        {
            return true;
        }

        //获得人物身上对应关卡的已经挑战次数;
        StageData sd = ObjectSelf.GetInstance().BattleStageData.GetStageDataByStageId(stageT.m_stageid);        
        int costTimes = sd == null ? 0 : sd.m_FightSum;
        
        //获得关卡的挑战总次数;
        int totalTimes = stageT.m_limittime;

        return (totalTimes <= costTimes) ? false : true;
    }
}
