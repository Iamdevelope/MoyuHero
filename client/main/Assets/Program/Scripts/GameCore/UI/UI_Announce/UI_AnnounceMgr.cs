using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;
using Platform;

public class UI_AnnounceItem
{
    protected Text mTitleTxt = null;
    protected Text mContentTxt = null;

    protected Transform mTrans = null;
    private BroadcastTemplate mData = null;
    public UI_AnnounceItem(Transform trans)
    {
        mTrans = trans;

        mTitleTxt = trans.FindChild("TitleObj/TitleBg/TitleText").GetComponent<Text>();
        mContentTxt = trans.FindChild("ContentObj/ContText").GetComponent<Text>();
    }

    public void SetData(BroadcastTemplate data)
    {
        mData = data;

        if (data != null)
        {
            mTitleTxt.text = data.getTitle();
            mContentTxt.text = data.getContent().Replace("\\n", "\n");
        }
    }

    public void SetParent(Transform parent)
    {
        mTrans.SetParent(parent, false);
    }
}

public class UI_AnnounceMgr : UI_AnnounceBase
{
    public static readonly string UI_ResPath = "UI_Announce/UI_Announce_2_5";

    protected GameObject m_ItemListObj = null;
    protected GameObject m_ItemObj = null;

    private List<UI_AnnounceItem> m_ItemsList = new List<UI_AnnounceItem>();

    public override void InitUIData()
    {
        base.InitUIData();

        m_ItemListObj = selfTransform.FindChild("ContentObj/ItemsLayout").gameObject;
        m_ItemObj = selfTransform.FindChild("Items/Item").gameObject;

        m_CloseBtnText.text = GameUtils.getString("common_button_close");
    }

    public override void InitUIView()
    {
        base.InitUIView();

        CreateItems();
    }

    public override void OnReadyForClose()
    {
        base.OnReadyForClose();
    }

    void OnDestroy()
    {
        UIState = UIStateEnum.ReadyForClose;
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        //UI_HomeControler.Inst.ReMoveUI(gameObject);
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_AnnounceMgr.UI_ResPath);

        UI_LoginWin.m_StarShow = true;
        UI_SelectLoginServer.m_StarShow = true;

        string nState = ConfigsManager.Inst.GetClientConfig(ClientConfigs.State);
        if (nState == string.Empty)
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_SelectLoginServer.UI_ResPath);
        }
        else
        {
            CGuest msgLogin = new CGuest();
            msgLogin.device_key = AppManager.Inst.DeviceUniqueIdentifier;
            IOControler.GetInstance().SendPlatform(msgLogin);
        }
    }

    void CreateItems()
    {
        List<int> keys = DataTemplate.GetInstance().m_BroadCastTable.GetDataKeys();
        
        if (keys == null || keys.Count <= 0)
        {
            return;
        }

        int adder = keys.Count - m_ItemsList.Count;

        for (int i = 0; i < adder; i++ )
        {
            m_ItemsList.Add(CreateItem());
        }

        for (int i = 0; i < m_ItemsList.Count; i++ )
        {
            if (i >= keys.Count)
            {
                break;
            }

            BroadcastTemplate broadT = DataTemplate.GetInstance().GetBroadcastTemplateById(keys[i]);
            m_ItemsList[i].SetData(broadT);
        }
    }

    UI_AnnounceItem CreateItem()
    {
        GameObject mgo = GameObject.Instantiate(m_ItemObj) as GameObject;
        
        UI_AnnounceItem item = new UI_AnnounceItem(mgo.transform);
        item.SetParent(m_ItemListObj.transform);

        return item;
    }
}
