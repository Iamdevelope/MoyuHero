using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.UI;
using DreamFaction.Utils;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
using System.Text;

public class UI_SealBox : BaseUI 
{
    public static UI_SealBox Inst = null;
    private Button m_CloseBtn = null;//关闭按钮
    private Text m_ConAllNum = null;//消耗品总个数
    private Image m_ConImg = null;//消耗品图标

    private int m_CurNum = 0;
    private int m_CurConId;
    private Mohe m_CurMohe;
    private List<Mohe> moheList = new List<Mohe>();
    private List<Mohe> ShowMoheList;//显示奖励的
    public List<UI_SealBoxItem> sealBoxList = new List<UI_SealBoxItem>();
    public List<UI_RewardItem> rewardList = new List<UI_RewardItem>();

    public void SetCurOpenNum(int count = 3) { m_CurNum = 3 - count; }

    public int GetCurOpenNum() { return m_CurNum; }

    public void MoheListAdd(Mohe mohe) { moheList.Add(mohe); }

    public void MoheListClear() { moheList.Clear(); }


    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_CloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();
        m_ConAllNum = selfTransform.FindChild("TopPanel/ConImg/Text").GetComponent<Text>();
        m_ConImg = selfTransform.FindChild("TopPanel/ConImg").GetComponent<Image>();
        m_CloseBtn.onClick.AddListener(new UnityAction(OnCloseBtn));
       
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_SealBox,UpdateSealBox);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        InitRewardData();
        ShowConAllCount();
        //Sprite img = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Jisheng_04");
    }
    /// <summary>
    /// 初始化奖励显示
    /// </summary>
    void InitRewardData()
    {
        ShowMoheList = ObjectSelf.GetInstance().GetMoheData();
        for (int i = 0; i < ShowMoheList.Count; i++)
        {
            rewardList[i].m_ItemID = ShowMoheList[i].id;
        }
        for (int i = 0; i < ShowMoheList.Count; i++)
        {
            UI_RewardItem ui_item = rewardList[i];
            BossboxTemplate date = (BossboxTemplate)DataTemplate.GetInstance().m_BossboxTable.getTableData(ShowMoheList[i].id);
            int count = date.getRewardnum();
            int itemId = date.getRewardid();
            switch (GameUtils.GetObjectClassById(itemId))
            {
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_INVALID:
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SPELL://技能
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BUFF://buff
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_DROPBOX://掉落包
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER://关卡与怪物
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
                    ui_item.ShowBoxResReward(itemId, count);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
                    ui_item.ShowBoxItemReward(itemId, count);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON://道具
                    ui_item.ShowBoxItemReward(itemId, count);
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO://英雄
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN://皮肤
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BOX://宝箱
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_ARTIFACT://神器
                    break;
                case EM_OBJECT_CLASS.EM_OBJECT_CLASS_NUMBER:
                    break;
                default:
                    break;
            }
        }
    }

    private void UpdateSealBox()
    {
        UpdateRewardItem();
        UpatdaConShow();
        UpdateSealBoxImg();
        ShowConAllCount();
        
    }

    /// <summary>
    /// 更新奖励显示
    /// </summary>
    void UpdateRewardItem()
    {
        //moheList = ObjectSelf.GetInstance().GetMoheData();
        for (int i = 0; i < moheList.Count; i++)
        {
            if (moheList[i].isopen == 1)
            {
                if (moheList[i].id == rewardList[i].m_ItemID)
                {
                    rewardList[i].SetYetGetImgActive(true);
                }
            }
        }
    }


    /// <summary>
    /// 更新消耗品显示
    /// </summary>
    void UpatdaConShow()
    {
        GameConfig cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        int conId = cofig.getOpen_bossbox_cost_id();
        int[] conNums = cofig.getOpen_bossbox_cost_num();
        for (int j = 0; j < sealBoxList.Count; j++)
        {
            if (m_CurNum == 0)
                return;
            if (m_CurNum == 3)
            {
                m_CurNum = 2;
            }
            sealBoxList[j].SetConObjActive(true, conNums[m_CurNum], conId);
            sealBoxList[j].SetOpenBtnTxt();
        }
    }

    /// <summary>
    /// 更新宝盒显示/按钮
    /// </summary>
    void UpdateSealBoxImg()
    {
        for (int i = 0; i < moheList.Count; i++)
        {
            if (moheList[i].isopen == 1)
            {
                for (int j = 0; j < sealBoxList.Count; j++)
                {
                    if (moheList[i].place != 0 && moheList[i].place == sealBoxList[j].curPos)//moheList[i].place == sealBoxList[i].curPos
                    {
                        sealBoxList[j].SetOpenBtnActive(false);
                        sealBoxList[j].SetConObjActive(false);
                        sealBoxList[j].SetMoheResImg();
                        //BossboxTemplate date = (BossboxTemplate)DataTemplate.GetInstance().m_BossboxTable.getTableData(moheList[i].id);
                        //int count = date.getRewardnum();
                        //int itemId = date.getRewardid();
                        //switch (GameUtils.GetObjectClassById(itemId))
                        //{
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_INVALID:
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SPELL://技能
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BUFF://buff
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_DROPBOX://掉落包
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER://关卡与怪物
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
                        //        sealBoxList[j].SetMoheResImg(itemId, count);
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
                        //        sealBoxList[j].SetMoheItemImg(itemId, count);
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON://道具
                        //        sealBoxList[j].SetMoheItemImg(itemId, count);
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO://英雄
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN://皮肤
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BOX://宝箱
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_ARTIFACT://神器
                        //        break;
                        //    case EM_OBJECT_CLASS.EM_OBJECT_CLASS_NUMBER:
                        //        break;
                        //    default:
                        //        break;
                        //}

                    }
                    else
                    {
                        sealBoxList[j].SetBtnImg();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 显示消耗品总个数
    /// </summary>
    public void ShowConAllCount()
    {
        int conCount = 0;
        GameConfig cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        int conId = cofig.getOpen_bossbox_cost_id();
        int[] conNums = cofig.getOpen_bossbox_cost_num();
        List<BaseItem> baseItemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        int baseItemCount = baseItemList.Count;
        for (int i = 0; i < baseItemCount; ++i)
        {
            int baseItemId = baseItemList[i].GetItemTableID();
            if (baseItemId == conId)
            {
                int tempNum = baseItemList[i].GetItemCount();
                conCount += tempNum;
            }
        }
        m_ConAllNum.text = conCount.ToString();
        ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(conId);
        m_ConImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
    }
   
    
    //发送消息
    public void SendMsg(int pos)
    {
        COpenMohe msg = new COpenMohe();
        msg.place = pos;
        IOControler.GetInstance().SendProtocol(msg);
    }   

    //返回按钮
    private void OnCloseBtn()
    {        
        UI_FightControler.Inst.ShowCombat(true);
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightWin);
        UI_HomeControler.Inst.ReMoveUI(gameObject);        
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.F_SealBox,UpdateSealBox);
    }


    //for (int i = 1; i <= sealBoxHash.Count; i++)
    //{
    //    Debug.Log(sealBoxHash.Keys);
    //    Mohe mode = null;
    //    if (sealBoxHash.ContainsKey(i))
    //    {
    //        mode = sealBoxHash[i] as Mohe;
    //        moheList.Add(mode);
    //    }
    //    for (int n = 0; n < moheList.Count; n++)
    //    {
    //        if (moheList[n].id == mode.id)
    //        {
    //            moheList[n] = mode;
    //        }
    //    }
    //}
}
