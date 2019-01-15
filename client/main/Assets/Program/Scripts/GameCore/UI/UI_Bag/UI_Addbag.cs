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

public class UI_Addbag : BaseUI
{
    public static string RescPath = "UI_Bag/UI_BagAdd";
    public Button addBtn;
    public Button closeBtn;
    public Text diamondText;
    int diamondNum;
    private Text money;
    public override void InitUIData()
    {
        base.InitUIData();
        addBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnAddbag));
        closeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnCloseBtn));
        //Debug.Log(ObjectSelf.GetInstance().BagBuyCount);
        diamondNum = DataTemplate.GetInstance().m_GameConfig.getCommon_item_packset_expand_cost()[ObjectSelf.GetInstance().BagBuyCount];
        diamondText.text=diamondNum.ToString();

        money= transform.FindChild("moneybar/money").GetComponent<Text>();
    }

    void OnEnable()
    {
        money.text = ObjectSelf.GetInstance().Gold.ToString();
    }

    private void OnAddbag()
    {
        if (diamondNum>ObjectSelf.GetInstance().Gold)
        {
            //UI_Bag._instance.OnDiamondBuyShow();
            this.gameObject.SetActive(false);
        }
      
        else
        {
            if (ObjectSelf.GetInstance().BagBuyCount + 1 == DataTemplate.GetInstance().m_GameConfig.getCommon_item_packset_max_purchase())
            {
                UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip5"));
                CBagExpansion ce = new CBagExpansion();
                IOControler.GetInstance().SendProtocol(ce);
                this.gameObject.SetActive(false);
            }
            else
            {
                UI_Bag._instance.AddMsgBox(GameUtils.getString("bag_item_tip6") + (ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax() + DataTemplate.GetInstance().m_GameConfig.getCommon_item_packset_per_expand()));
                CBagExpansion ce = new CBagExpansion();
                IOControler.GetInstance().SendProtocol(ce);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnCloseBtn()
    {
        this.gameObject.SetActive(false);
    }
}
