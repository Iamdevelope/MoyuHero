using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using DreamFaction.Utils;
public class UI_SellManage : BaseUI
{
    public static string ui_ResPath = "UI_Bag/UI_BagItemSell_1_20";
    public static UI_SellManage _instance; 
    private Button closeBtn;
    //private Text surplusText;
    private Text goldText;
    private Image iconImage;
    private Text nameText;
    private GameObject minusBtn;
    private GameObject addBtn;
    private Text priceText;
    private Text totalText;
    private Text numText;
    private Text owerNymber;
   // private Text maxText;
    private Button sellBtn;
    private int count=1;
    private int maxCount;
    private int price;
    private int itemID;
    public bool isMinusDown = false;
    public bool isAddDown = false;
    private float time=0;
    private float timeer=0;
    private BaseItem item;
    private Button m_Max;
    //private Button ooo;
    public override void InitUIData()
    {
        base.InitUIData();
        closeBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Close").GetComponent<Button>();
        closeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnCloseSell));
        // surplusText = selfTransform.FindChild("UI_BG_Main/UI_Text_Surplus/UI_Text_Num").GetComponent<Text>();
        // goldText = selfTransform.FindChild("UI_Bg_Gold/Text").GetComponent<Text>();
        iconImage = selfTransform.FindChild("UI_BG_Main/UI_Image_Icon").GetComponent<Image>();
        nameText = selfTransform.FindChild("UI_BG_Main/UI_Text_Name").GetComponent<Text>();
        priceText = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Golds/UI_Text_Price").GetComponent<Text>();
        totalText = selfTransform.FindChild("UI_BG_Main/UI_BG_Cost/UI_BG_Total/UI_Text_Total").GetComponent<Text>();
        numText = selfTransform.FindChild("UI_BG_Main/UI_Bg_Num/UI_Text_Num").GetComponent<Text>();
        owerNymber = selfTransform.FindChild("UI_BG_Main/UI_Text_Count").GetComponent<Text>();
        //maxText = selfTransform.FindChild("UI_BG_Main/UI_Text_MAX").GetComponent<Text>();
        minusBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Minus").gameObject;
        //minusBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(MinusItemClick));
        addBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Add").gameObject;
        m_Max = selfTransform.FindChild("UI_BG_Main/UI_Btn_Max").GetComponent<Button>();
        m_Max.onClick.AddListener(OnMax);
        //addBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(AddItemClick));
        sellBtn = selfTransform.FindChild("UI_BG_Main/UI_Btn_Sell").GetComponent<Button>();
        sellBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSellClick));
        EventTriggerListener.Get(minusBtn.gameObject).onDown = MinusItemClickDown;
        EventTriggerListener.Get(minusBtn.gameObject).onUp = MinusItemClickUp;
        EventTriggerListener.Get(addBtn.gameObject).onDown = AddItemClickDown;
        EventTriggerListener.Get(addBtn.gameObject).onUp = AddItemClickUp;
    }

    public void UpdateShow(int id,BaseItem item)
    {
        //TODO
        //更新显示 
        count = 1;
        itemID = id;
        this.item = item;
   
        ItemTemplate itemtable = item.GetItemRowData();
        nameText.text = GameUtils.getString(itemtable.getName());
        iconImage.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + itemtable.getIcon());
        priceText.text = itemtable.getSellPrice().ToString();
        owerNymber.text = item.GetItemCount().ToString();
        price = itemtable.getSellPrice();
        maxCount =item.GetItemCount();
        totalText.text = (price * count ).ToString();
        ShowNum();
        if (maxCount==1)
        {
            //addBtn.interactable = false;
          
            //minusBtn.interactable = false;

        }
      
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
        if (count <= 1)
        {
            count = 1;
            ShowNum();
            //minusBtn.interactable = false;
            //minusBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Bag_05");
            GameUtils.SetImageGrayState(minusBtn.transform.GetComponent<Image>(), true);
            GameUtils.SetImageGrayState(addBtn.transform.GetComponent<Image>(), false);
            GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), false);
          
            isMinusDown = false;
        }
        if (count >= maxCount)
        {
            count = maxCount;
            ShowNum();
            //addBtn.interactable = false;
            //addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Bag_07");
            GameUtils.SetImageGrayState(minusBtn.transform.GetComponent<Image>(), false);
            GameUtils.SetImageGrayState(addBtn.transform.GetComponent<Image>(), true);
            GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), true);
            isAddDown = false;
        }
        if (isMinusDown)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                
                timeer += Time.deltaTime;
                if (timeer > 0.05f)
                {
                    count -= 1;
                    timeer = 0;
                }
                //addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Sale_06");  
                //GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), false);
            }
            else if (time >= 2.9f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.08f)
                {
                    timeer = 0;
                    count -= 1;
                }
            }
            if (time >= 2.7f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.09f)
                {
                    count -= 1;
                }
            }
            if (time >= 2.5f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.1f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 2.3f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.11f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 2.1f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.12f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 2f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.13f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 1.8f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.14f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 1.5f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.15f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            if (time >= 0.5f)
            {
                // addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Sale_06");
                timeer += Time.deltaTime;
                if (timeer > 0.16f)
                {
                    count -= 1;
                    timeer = 0;
                }

            }
            ShowNum();
        }
        if (isAddDown)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.05f)
                {
                    
                    count += 1;
                    timeer = 0;
                }
                //addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Sale_06");      
               // GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), false);
            }
            else if (time >= 2.9f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.08f)
                {
                    timeer = 0;
                    count += 1;
                   
                }
            }
            if (time >= 2.7f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.09f)
                {
                    count += 1;
                }
            }
            if (time >= 2.5f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.1f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 2.3f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.11f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 2.1f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.12f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 2f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.13f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 1.8f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.14f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 1.5f)
            {
                timeer += Time.deltaTime;
                if (timeer > 0.15f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            if (time >= 0.5f)
            {
                // addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Sale_06");
                timeer += Time.deltaTime;
                if (timeer > 0.16f)
                {
                    count += 1;
                    timeer = 0;
                }
                
            }
            ShowNum();
        }
    }

    private void ShowNum()
    {
        numText.text = count.ToString();
       // maxText.text ="/"+ maxCount;
        totalText.text = (price * count).ToString();

    }


    private void MinusItemClickDown(GameObject btn)
    {
        //TODO
        //减少按钮操作
        //Debug.Log(Time.time);
        if (btn==minusBtn.gameObject)
        {
            //addBtn.interactable = true;
            if (count <= 1)
            {
                return;
            }
            else
            {
                //addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Bag_06");
                GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), false);
                GameUtils.SetImageGrayState(addBtn.transform.GetComponent<Image>(), false);
                --count;
                ShowNum();
                time = 0;
                isMinusDown = true;
            }
        }
    
    }

    private void MinusItemClickUp(GameObject btn)
    {
        //TODO
        //减少按钮操作
        //Debug.Log(Time.time);

        //addBtn.interactable = true;
        //addBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Sale_06");
        GameUtils.SetImageGrayState(addBtn.transform.GetComponent<Image>(), false);
        //--count;
        //ShowNum();
        if (btn == minusBtn.gameObject)
        {
            isMinusDown = false;
        }
    }

    private void AddItemClickDown(GameObject btn)
    {
       //TODO
       //增加按钮操作
       if (addBtn.gameObject==btn)
        {
            if (count >= maxCount)
            {
                return;
            }
            else
            {
                //Debug.Log(count);
                //minusBtn.interactable = true;
                //minusBtn.transform.GetComponent<Image>().overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + "UI_Bag_04");
                GameUtils.SetImageGrayState(m_Max.transform.GetComponent<Image>(), false);
                GameUtils.SetImageGrayState(minusBtn.transform.GetComponent<Image>(), false);
                ++count;
                ShowNum();
                time = 0;
                isAddDown = true;
            }
        }
    }
    private void AddItemClickUp(GameObject btn)
    {
        //TODO
        //增加按钮操作
        if (addBtn.gameObject==btn)
        {
            isAddDown = false;
        }
       
    }

    private void OnSellClick()
    {
        //TODO
        //出售操作
        item.OnSellItem(count);
        UI_ItemsManage._instance.recordX_GUID = item.GetItemGuid();
        UI_HomeControler.Inst.ReMoveUI(ui_ResPath);
    }


  

    private void OnCloseSell()
    {
        UI_HomeControler.Inst.ReMoveUI(ui_ResPath);
        UI_Bag._instance.SetMoneyBarShowOrHide(true);

    }
    void OnMax()
    {
        ItemTemplate itemtable = item.GetItemRowData();
        int totalPrice= itemtable.getSellPrice() * item.GetItemCount();
        totalText.text = totalPrice.ToString();
        numText.text = item.GetItemCount().ToString();
        count = item.GetItemCount();
    }

}
