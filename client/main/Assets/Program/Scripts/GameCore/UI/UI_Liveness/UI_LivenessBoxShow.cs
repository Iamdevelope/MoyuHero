using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using GNET;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DG.Tweening;
using DreamFaction.LogSystem;
using DreamFaction.UI;
public class UI_LivenessBoxShow : BaseUI
{
    public static UI_LivenessBoxShow _instance;
    private Button mCloseBtn;
    private Image mCloseImage;
    private Text mCloseText;
    private UILoop mList;
    private Text mHeader;
    List<InnerdropTemplate> mDropList = new List<InnerdropTemplate>();
    private int heroNum;
    private int itemNum;
    private UI_LivenessBoxItem itm;

    private RectTransform m_RewardItem_List; 
    private LoopLayout m_RewardItemLayout;
    private UniversalItemCell m_Cell;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        mCloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
        mCloseImage = selfTransform.FindChild("CloseBtn").GetComponent<Image>();
        mCloseText = selfTransform.FindChild("CloseBtn/Text").GetComponent<Text>();
        mHeader = selfTransform.FindChild("Image/Text").GetComponent<Text>();
        m_RewardItem_List = selfTransform.FindChild("ItemList/Grid").GetComponent<RectTransform>();
        m_RewardItemLayout = selfTransform.FindChild("ItemList/Grid").GetComponent<LoopLayout>();
        mCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBtn));
    }
    public void Show(UI_LivenessBoxItem item)
    {
        if (item == null) return;
        itm = item;
        itemNum = heroNum = 0;
        if (item.canOpen())
        {
            if (!item.isOpend())
            {
                mHeader.text = "奖励确认";
                mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xuanze");
                mCloseText.text = GameUtils.getString("common_button_receive");//领  取
            }
            else
            {
                InterfaceControler.GetInst().AddMsgBox("已领取过该奖励", this.gameObject.transform);
                return;
            }
        }
        else
        {
            mHeader.text = GameUtils.getString("activity_content1");
            mCloseImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_xuanze");
            mCloseText.text = GameUtils.getString("common_button_close"); //关  闭
        }

        int id = DataTemplate.GetInstance().m_GameConfig.getActivitymission_reward_drop()[item.Index];
        int[] innerdropList = ((NormaldropTemplate)DataTemplate.GetInstance().m_NormaldropTable.getTableData(id)).getInnerdrop();
        Dictionary<int, IExcelBean> innerIExcel = DataTemplate.GetInstance().m_InnerdropTable.getData();

        mDropList.Clear();
        //for (int i = 0; i < innerdropList.Length; i++)
        //{
        //    Debug.Log(111);
        //    foreach (var value in innerIExcel.Values)
        //    {
        //        if (((InnerdropTemplate)value).getInnerdropid() == innerdropList[i])
        //        {
        //            mDropList.Add((InnerdropTemplate)value);

        //            int itemid = ((InnerdropTemplate)value).getObjectid();
        //            int type = itemid / 1000000;
        //            if (type == (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE)
        //                itemNum++;
        //            if (type == (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO)
        //                heroNum++;
        //        }
        //    }
        //}
        for (int i = 0, j = innerdropList.Length; i < j; i++)
        {
            foreach (int k in DataTemplate.GetInstance().m_InnerdropTable.GetDataKeys())
            {
                InnerdropTemplate _it = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(k);

                if (_it == null) continue;

                if (_it.getInnerdropid() == innerdropList[i])
                {
                    mDropList.Add(_it);
                }

            }
        }
        CreatBoxItem();
    }

    private void CreatBoxItem()
    {
        m_RewardItemLayout.cellCount = mDropList.Count;
        m_RewardItemLayout.updateCellEvent = UpdateAwardItem;
        m_RewardItemLayout.Reload();
    }

    private void UpdateAwardItem(int index, RectTransform cell)
    {
        m_Cell = cell.GetComponent<UniversalItemCell>();

        InnerdropTemplate value = mDropList[index] as InnerdropTemplate;
        if (value == null) return;
        int itemid = value.getObjectid();

        int type = itemid / 1000000;
        switch (type)
        {
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                if (_temp_res != null)
                {
                    m_Cell.InitByID(itemid, value.getDropnum());
                    m_Cell.SetText(GameUtils.getString(_temp_res.getName()), "", "");
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                {
                    ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(value.getObjectid());
                    if (itemTable != null)
                    {
                        m_Cell.InitByID(itemid, -1);
                        m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                    }    
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                {
                    ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(value.getObjectid());
                    if (itemTable != null)
                    {
                        m_Cell.InitByID(itemid, value.getDropnum());
                        m_Cell.SetText(GameUtils.getString(itemTable.getName()), "", "");
                    }
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                {
                    HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(value.getObjectid());
                    if (hero != null)
                    {
                        m_Cell.InitByID(itemid, value.getDropnum());
                        m_Cell.SetText(GameUtils.getString(hero.getTitleID()), "", "");
                    }
                }
                break;

            default:
                break;
        }
                //UI_LivenessDropItem _UI_LivenessDropItem = cell.GetComponent<UI_LivenessDropItem>();
                //if (_UI_LivenessDropItem == null)
                //{
                //    _UI_LivenessDropItem = cell.gameObject.AddComponent<UI_LivenessDropItem>();
                //}
                //_UI_LivenessDropItem.Data(mDropList[index]);
    }

    public void OnClickBtn()
    {
      
        ObjectSelf obj=ObjectSelf.GetInstance();
        if (itemNum > obj.CommonItemContainer.GetBagItemSizeMax() - obj.CommonItemContainer.GetBagItemSum())
        {
            //UI_Liveness._instance.IsItemBagMax();
        }
        if (heroNum > obj.HeroContainerBag.GetHeroBagSizeMax() - obj.HeroContainerBag.GetHeroList().Count)
        {
            //UI_Liveness._instance.IsHeroBagMax();
        }
        if (itm.canOpen())
        {
            CGetHuoYueBox cBox = new CGetHuoYueBox();
            cBox.boxnum = itm.Index + 1;
            IOControler.GetInstance().SendProtocol(cBox);
        }
        this.gameObject.SetActive(false);
    }
    
    
}
