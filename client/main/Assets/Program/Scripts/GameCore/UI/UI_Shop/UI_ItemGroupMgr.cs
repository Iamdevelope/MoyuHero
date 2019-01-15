using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;

public class UI_ItemGroupMgr : BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_ItemGroup_1_1";

    Text titleTxt;
    GameObject listObj;
    GameObject itemObj;
    Button closeBtn;
    Text closeBtnTxt;

    private static ShopTemplate shopT = null;

    private List<UniversalItemCell> itemsList = new List<UniversalItemCell>();

    public override void InitUIData()
    {
        base.InitUIData();

        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        listObj = transform.FindChild("Attris/DetailObj").gameObject;
        itemObj = transform.FindChild("Items/Item").gameObject;
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();
        
        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("shop_content2");
        closeBtnTxt.text = GameUtils.getString("common_button_close");


        if (shopT != null)
        {
            SetShowData(shopT);
        }
    }

    public static void SetData(ShopTemplate _shopT)
    {
        shopT = _shopT;
    }

    public void SetShowData(ShopTemplate shopT)
    {
        string[] details = shopT.getPreviewContent().Split(new string[] { "#" }, StringSplitOptions.None);
        
        if(details == null || details.Length == 0)
        {
            LogManager.LogError("打包展示物品内容解析错误，shopid=" + shopT.getId());
            return;
        }

        for(int i = 0,j = details.Length; i < j; i++)
        {
            string[] cont = details[i].Split(new string[] { "-" }, StringSplitOptions.None);
            if(cont == null || cont.Length != 4)
            {
                LogManager.LogError("打包展示物品内容解析错误，shopid=" + shopT.getId());
            }

            try
            {
                switch(int.Parse(cont[0]))
                {
                    case 1:
                        CreateItem(int.Parse(cont[1]), cont[2], cont[3]);
                        break;
                    case 2:
                        CreateItem(cont[1], cont[2], cont[3]);
                        break;
                }
            }
            catch(Exception e)
            {
                LogManager.LogError(e.ToString());
            }
        }
    }

    void CreateItem(int tableId, string count, string name)
    {
        UniversalItemCell _cell = UniversalItemCell.GenerateItem(listObj.transform);
        _cell.transform.localScale = Vector3.one;
        _cell.InitByID(tableId, int.Parse(count));
        _cell.SetText(GameUtils.getString(name), null, null);
        _cell.AddClickListener(OnItemClick);
        itemsList.Add(_cell);
     }


    void CreateItem(string icon, string count, string name, int id = -1)
    {
        UniversalItemCell _cell = UniversalItemCell.GenerateItem(listObj.transform);
        _cell.transform.localScale = Vector3.one;
        _cell.InitBySprite(UIResourceMgr.LoadSprite(common.defaultPath + icon), int.Parse(count));
        _cell.SetText(GameUtils.getString(name), null, null);
        itemsList.Add(_cell);
    }

    void CreateItem(Sprite sprite, string count, string name, int id = -1)
    {
        GameObject go = GameObject.Instantiate(itemObj) as GameObject;
        if (go == null)
        {
            LogManager.LogError("打包物品创建失败！");
            return;
        }

        Transform trans = go.transform;

        trans.FindChild("name").GetComponent<Text>().text = GameUtils.getString(name);
        trans.FindChild("Image/count").GetComponent<Text>().text = "X" + count;
        Image icon = trans.FindChild("icon").GetComponent<Image>();
        icon.sprite = sprite;
        EventTriggerListener.Get(icon.gameObject).onClick = OnItemClick;
        EventTriggerListener.Get(icon.gameObject).param = id;
        //icon.SetNativeSize();

        trans.parent = listObj.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);

 //       itemsList.Add(go);
    }
    void OnItemClick(int itemID)
    { 
        if(itemID>0)
            ShopModule.ShowItemPreviewUIHandler(itemID);
    }
    void OnItemClick(GameObject go)
    {
        if (go == null)
            return;

        try
        {
            int id = (int)(EventTriggerListener.Get(go).param);
            if(id != -1)
            {
                ShopModule.ShowItemPreviewUIHandler(id);
            }
        }
        catch(Exception e)
        {
            LogManager.LogError(e);
        }
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();

        shopT = null;
        closeBtn.onClick.RemoveAllListeners();

        for(int i = 0; i < itemsList.Count; i++)
        {
            itemsList[i].RemoveClickListener(OnItemClick);
        }
    }

    void CloseUI()
    {
        OnReadyForClose();

        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void OnCloseBtnClick()
    {
        CloseUI();
    }
}
