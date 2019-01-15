using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
public class UI_ItemGet:BaseUI  {

    public static UI_ItemGet inist;
    private Button mOK;
    private LoopLayout mLoopLayout;
    private GameObject mScrollBar;
    public  List<int> mGiftList; //掉落包id
    public override void InitUIData()
    {
        base.InitUIData();
        inist = this;
        mGiftList = new List<int>();
        Transform main= transform.FindChild("UI_BG_Main");
        mOK= main.FindChild("UI_Btn_ok").GetComponent<Button>();
        mOK.onClick.AddListener(OnOK);
        mLoopLayout = main.FindChild("goodslist/grid").GetComponent<LoopLayout>();
        mScrollBar = main.FindChild("goodslist/Scrollbar").gameObject;
    }
    public void FillData()
    {
        mLoopLayout.cellCount = mGiftList.Count;
        if (mLoopLayout.cellCount <= 18)
        {
            mScrollBar.SetActive(false);
        }
        else
        {
            mScrollBar.SetActive(true);
        }
        mLoopLayout.updateCellEvent = UpdateItem;
        mLoopLayout.Reload();
    }
    private void UpdateItem(int index, RectTransform cell)
    {   
        UI_ItemGetItem item= cell.GetComponent<UI_ItemGetItem>();
        item.index = index;
        item.FillData(mGiftList[index]);
    }

    private void OnOK()
    {
        gameObject.SetActive(false);
        UI_ItemGet.inist.mGiftList.Clear();
    }
}
