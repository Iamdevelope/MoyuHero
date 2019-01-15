using UnityEngine;
using System.Collections;

public class UI_PropMessage
{
    private int itemId;
    private string itemName;
    private string itemDes;
    private string itemIcon;
    private int itemType;
    private int itemQuality;
    private int itemIfSell;
    private int itemSellPrice;
    private int itemIfStack;
    private int itemStackNum;
    private int itemIfUse;
    private int itemDropPackId;
    private int itemPreviewType;
    private int itemPreviewContent;
    #region 属性封装

    public int ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public string ItemDes
    {
        get { return itemDes; }
        set { itemDes = value; }
    }

    public string ItemIcon
    {
        get { return itemIcon; }
        set { itemIcon = value; }
    }

    public int ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    public int ItemQuality
    {
        get { return itemQuality; }
        set { itemQuality = value; }
    }

    public int ItemIfSell
    {
        get { return itemIfSell; }
        set { itemIfSell = value; }
    }

    public int ItemSellPrice
    {
        get { return itemSellPrice; }
        set { itemSellPrice = value; }
    }

    public int ItemIfStack
    {
        get { return itemIfStack; }
        set { itemIfStack = value; }
    }

    public int ItemStackNum
    {
        get { return itemStackNum; }
        set { itemStackNum = value; }
    }

    public int ItemIfUse
    {
        get { return itemIfUse; }
        set { itemIfUse = value; }
    }

    public int ItemDropPackId
    {
        get { return itemDropPackId; }
        set { itemDropPackId = value; }
    }

    public int ItemPreviewType
    {
        get { return itemPreviewType; }
        set { itemPreviewType = value; }
    }


    public int ItemPreviewContent
    {
        get { return itemPreviewContent; }
        set { itemPreviewContent = value; }
    } 
    #endregion
	
}
