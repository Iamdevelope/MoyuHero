using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI;
public class UI_RuneButtonMassage : CellItem
{
   
   

    public Text mName;
    public List<GameObject> mStarList = new List<GameObject>();
    public Image[] mTypes = null;
    public GameObject bg;
    public GameObject specBg;
    public Image mIcon;
    public Text mLevel;
    public GameObject mEquip;
    private Button mySelfBtn;
    public int tableID;
    public int id;
    public ItemTemplate rune;
    private Transform Parent;                                    //父节点
    private Transform Pos;                                       //边界点
    private bool isAddItem;
    private GameObject maxLevel;       //满级时特效
    private GameObject m_Tip;
    private ItemEquip m_CurItem;
    /// <summary>
    /// item状态
    /// </summary>
    public enum ItemSate
    {
        FillDate = 0,  //正常有数据状态
        Empty = 1,     //空数据状态
        Lock = 2,      //锁定状态
    }
    public ItemSate itemState; //item 状态
    public override void InitUIData()
    {
        base.InitUIData();
        Parent = selfTransform.FindChild("Parent");
        Pos = Parent.FindChild("Pos");
        mySelfBtn = this.transform.GetComponent<Button>();
        maxLevel = Parent.FindChild("maxLevel").gameObject;
        m_Tip = Parent.FindChild("tips").gameObject;
       // mySelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnMyselfClick));
        isAddItem = true;

        mTypes = new Image[4];

        for (int i = 0; i < 4; i++ )
        {
            mTypes[i] = Parent.FindChild("Image/type" + (i + 1)).GetComponent<Image>();
        }
    }


    public void RuneShow()
    {
        //Debug.Log(tableID);

        if (itemState == ItemSate.Empty)
        {
            for (int i = 0; i < Parent.childCount; i++)
            {
                Parent.GetChild(i);
                if (i == 0)
                {
                    Parent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Parent.GetChild(i).gameObject.SetActive(false);
                }
            }
            mySelfBtn.onClick.RemoveListener(new UnityEngine.Events.UnityAction(OnMyselfClick));
            return;
        }
        else if (itemState == ItemSate.Lock)
        {
            for (int i = 0; i < Parent.childCount; i++)
            {
                Parent.GetChild(i);
                if (i == 0||i==1)
                {
                    Parent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Parent.GetChild(i).gameObject.SetActive(false);
                }
            }
            mySelfBtn.onClick.RemoveListener(new UnityEngine.Events.UnityAction(OnMyselfClick));
            return;
        }
        else if(itemState==ItemSate.FillDate)
        {
            Parent.FindChild("lock").gameObject.SetActive(false) ;
            Parent.FindChild("Border").gameObject.SetActive(false);
            maxLevel.SetActive(true);
            bg.SetActive(true);
            specBg.SetActive(true);
            mIcon.gameObject.SetActive(true);
            mName.gameObject.SetActive(true);
            Parent.FindChild("Level").gameObject.SetActive(true);
            Parent.FindChild("Equip").gameObject.SetActive(false);
            Parent.FindChild("Pos").gameObject.SetActive(true);
            Parent.FindChild("stars").gameObject.SetActive(true);
            mySelfBtn.onClick.RemoveAllListeners();
            mySelfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnMyselfClick));

        }
        if (tableID == null)
        {
            return;
        }
        else
        {
            rune = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(tableID);
            //Debug.Log(rune.getIcon());
            mIcon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + rune.getIcon());
           // mIcon.SetNativeSize();
            mIcon.transform.localScale = new Vector3(0.8f,0.8f,0f);
            mName.text =GameUtils.getString(rune.getName());
            int starCount = rune.getRune_quality();

            bool isSpec = RuneModule.IsSpecialRune(rune);
            bg.SetActive(!isSpec);
            specBg.SetActive(isSpec);

            for (int i = 0; i < starCount; i++)
            {
                mStarList[i].SetActive(true);
            }

            for (int i = 1; i <= 4; i++ )
            {
                mTypes[i - 1].gameObject.SetActive(i == rune.getRune_type());
            }

            for (int i = starCount; i < mStarList.Count; i++)
            {
                mStarList[i].SetActive(false);
            }
            m_CurItem = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, UI_RuneMange._instance.rune[id].GetItemGuid());
            bool isEquiped = m_CurItem.IsEquip();//ObjectSelf.GetInstance().HeroContainerBag.IsItemEquiped(data);
            mEquip.SetActive(isEquiped);
            mLevel.text = "+" + m_CurItem.GetStrenghLevel();

            //已满级
            int strengthLv = m_CurItem.GetStrenghLevel();
            ItemTemplate itemT = m_CurItem.GetItemRowData();
            bool isFullLv = DataTemplate.GetInstance().IsRuneStrenthFullLevel(itemT, strengthLv);
            if (isFullLv)
            {
                maxLevel.SetActive(true);
            }
            else
            {
                maxLevel.SetActive(false);
            }
            //设置符文满级特效颜色
            RawImage rawImage = maxLevel.GetComponent<RawImage>();
            switch (itemT.getRune_type())
            {  
                case 1: //蓝色
                    rawImage.color = Color.blue;
                    break;
                case 2: //紫色
                    rawImage.color = new Color(153/255f, 51/255f, 250/255f);
                    break;
                case 3: //绿色
                    rawImage.color = Color.green;
                    break;
                case 4:  //红色
                    rawImage.color = Color.red;
                    break;
                case 5:  //橙色
                    rawImage.color = new Color(255/ 255f, 128 / 255f, 0 / 255f);
                    break;
                default:
                    break;
            }
            m_Tip.SetActive(ObjectSelf.GetInstance().CommonItemContainer.IsNewGetItem(m_CurItem.GetItemGuid()));

        }
    }

  
    public void OnView(float min, float max)
    {

        if (Pos.position.y < max && Pos.position.y > min)
        {
            Parent.gameObject.SetActive(true);
        }
        else
        {
            Parent.gameObject.SetActive(false);
        }
    }
    public void OnRuneView(float addItem)
    {
        if (Pos.position.y > addItem)
        {
            if (isAddItem)
            {
                int count = 5 - ((UI_RuneMange._instance.rune.Count) % 5);
                if (count == 5)
                {
                    count = 0;
                }
                if (id < UI_RuneMange._instance.rune.Count - (20 - count))
                {
                    //UI_RuneMange._instance.DynamicItem(id);
                }
                isAddItem = false;
            }

        }
    }
    public void OnMyselfClick()
    {
            UI_RuneMange._instance.RuneList(index);
            UI_RuneMassage._instance.UpdateShow(id, rune);
            //功能提示
            ObjectSelf.GetInstance().CommonItemContainer.SetItemPreview(m_CurItem.GetItemGuid());
            m_Tip.SetActive(false);
            UI_Bag._instance.m_TipsController.Refresh();

    }
}
