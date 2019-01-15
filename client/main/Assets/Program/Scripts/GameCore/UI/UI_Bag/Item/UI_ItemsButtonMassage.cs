using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI;
public class UI_ItemsButtonMassage : CellItem 
{
    public BaseItem baseItem;
    public Image mIcon;
    public Text mNum;
    public Image m_itemQuality;
    //public GameObject mEquip;
    public ItemTemplate item = null;
    private Button mySelfBtn;
    public Transform[] quality = new Transform[5]; //根据品质区分物品的类型
    //private Transform Parent;                                    //父节点                                 
    //边界点
    //private bool isAddItem;
    //private Image mSelectState;        //选择状态 背景为深蓝色
    //private Image mlockState;         //锁定状态 背景为链锁
    //private GameObject mBorder;
    //private GameObject m_Tips;
    /// <summary>
    /// item状态
    /// </summary>
    public enum ItemSate   
    { 
       FillDate=0,  //正常有数据状态
       Empty=1,     //空数据状态
       Lock=2,      //锁定状态
    }
    public ItemSate itemState; //item 状态
    public override void InitUIData()
    {
        base.InitUIData();
        //Parent = selfTransform.FindChild("Parent");
        //mSelectState = Parent.FindChild("BG/selectSatte").GetComponent<Image>();
        //mlockState = Parent.FindChild("BG/lockState").GetComponent<Image>();
        //mBorder = Parent.FindChild("Border").gameObject ;
        //m_Tips = Parent.FindChild("tips").gameObject;
        mySelfBtn = this.transform.GetComponent<Button>();
        quality[0] = transform.FindChild("quality/1");
        quality[1] = transform.FindChild("quality/2");
        quality[2] = transform.FindChild("quality/3");
        quality[3] = transform.FindChild("quality/4");
        quality[4] = transform.FindChild("quality/5");
        m_itemQuality = transform.FindChild("quality").GetComponent<Image>();
        //FillEmptyData(false);
        //isAddItem = true;
        //mBorder.SetActive(false);
    }

    public void UpdateShow(BaseItem item)
    {
        baseItem = item;
        ItemShow();
    }
    public void ItemShow()
    {
            if (itemState == ItemSate.FillDate) //正常填充数据状态
            {
                item = baseItem.GetItemRowData();
                mIcon.gameObject.SetActive(true);
                mNum.gameObject.SetActive(true);
                //mlockState.gameObject.SetActive(false);
                mySelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnMyselfClick));
            }
            else if(itemState==ItemSate.Empty) //空格子状态
            {
                mIcon.gameObject.SetActive(false);
                mNum.gameObject.SetActive(false);
                //mlockState.gameObject.SetActive(false);
                //SetSelectState(false);
                //m_Tips.SetActive(false);
                //mySelfBtn.onClick.RemoveListener(OnMyselfClick);
                return;
            }
            else if(itemState==ItemSate.Lock) //
            {
                mIcon.gameObject.SetActive(false);
                mNum.gameObject.SetActive(false);
                SetSelectState(false);
                //m_Tips.SetActive(false);
                //mlockState.gameObject.SetActive(true);
                //mySelfBtn.onClick.RemoveListener(OnMyselfClick);
                return;
            }
            if (baseItem == null)
            {
                return;
            }
            //Debug.Log(item.getIcon());
            //Debug.Log(item.getIcon());
            mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
            mNum.text = "x" + baseItem.GetItemCount();
            m_itemQuality.sprite = GameUtils.GetItemQualitySprite(baseItem.GetItemTableID());
            //if (quality[4] == null) return;
            //for (int i = 0; i < quality.Length; i++)
            //{
            //    if (i + 1 == item.getQuality())
            //    {
            //        quality[i].gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        quality[i].gameObject.SetActive(false);
            //    }
            //}
           // m_Tips.SetActive(ObjectSelf.GetInstance().CommonItemContainer.IsNewGetItem(baseItem.GetItemGuid()));
    }


    public void OnMyselfClick()
    {
        //TODO
        //实现功能
        UI_ItemsMassage._instance.UpdateShow(baseItem);
        UI_ItemsManage._instance.ItemList(baseItem);
        ObjectSelf.GetInstance().CommonItemContainer.SetItemPreview(baseItem.GetItemGuid());
        UI_Bag._instance.m_TipsController.Refresh();
        //m_Tips.SetActive(false);
    }
    /// <summary>
    /// 填充空格数据
    /// </summary>
    public void FillEmptyData(bool isLockState)
    {
        SetLockStateOrUnLock(isLockState);
        //mySelfBtn.onClick.RemoveListener(OnMyselfClick);
    }
    /// <summary>
    /// 设置为选中状态
    /// </summary>
    public void SetSelectState(bool isSelect)
    {
       // mSelectState.gameObject.SetActive(isSelect);
    }
    /// <summary>
    /// 设置锁定状态或者未锁定状态 背景是空 还是锁链
    /// </summary>
    /// <param name="isLock"></param>
    public void SetLockStateOrUnLock(bool isLock)
    {
        mIcon.gameObject.SetActive(false);
        mNum.gameObject.SetActive(false);
        SetSelectState(false);
       // mlockState.gameObject.SetActive(isLock);
    }

}
