using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;

public class UI_StoreMgr : BaseUI 
{
    public static string UI_ResPath = "UI_Shop/UI_Store_2_3";
    private static UI_StoreMgr m_Inst = null;

    private GameObject m_Prefab = null;
    private Transform m_Grid = null;

    private Button m_BackBtn = null;

    public static UI_StoreMgr Inst
    {
        get 
        { 
            return UI_StoreMgr.m_Inst; 
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();

        m_Inst = this;

        m_Prefab = UIResourceMgr.LoadPrefab(common.prefabPath + "UI_Shop/UI_StoreItem") as GameObject;
        m_Grid = selfTransform.FindChild("Content/LayoutList");

        m_BackBtn = selfTransform.FindChild("TopPanel/TopTittle/BackBtn").GetComponent<Button>();
        m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onBackBtnClick));

        GameEventDispatcher.Inst.addEventListener(GameEventID.G_SGetStore, UpdateUIShow);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        CreateStoreItem();
    }


    private void UpdateUIShow()
    {
        CreateStoreItem();
    }


    /// <summary>
    /// 创建商店Item
    /// </summary>
    private void CreateStoreItem()
    {
        ClearStoreOBJ();
        List<BaseStore> storeList = ObjectSelf.GetInstance().StoreContainer.GetStoreList();

        for (int i = 0; i < storeList.Count; i++)
        {
            BaseStore store = storeList[i];
            if (store.GetStoreRow().getWhetherDisplay() == 1)
            {
                GameObject go = Instantiate(m_Prefab) as GameObject;
                go.transform.parent = m_Grid;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                UI_StoreItem uiStoreItem = go.GetComponent<UI_StoreItem>();
                uiStoreItem.InitItemData(store);
            }
        }
    }

    /// <summary>
    /// 清除
    /// </summary>
    private void ClearStoreOBJ()
    {
        for (int i = 0; i < m_Grid.childCount; i++)
        {
            Destroy(m_Grid.GetChild(i).gameObject);
        }
    }


    private void onBackBtnClick()
    {
        onClose();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    private void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }


    private void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_SGetStore, UpdateUIShow);
    }
    
    


}
