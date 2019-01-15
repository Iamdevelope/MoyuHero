using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using System.Collections.Generic;
using DreamFaction.GameCore;

public class UI_SealBoxItem : BaseUI 
{
    public int curPos;
    //开启按钮
    private Button m_OpenBtn = null;
    //
    private Text m_OpenBtnTxt = null;
    //消耗品图标
    private Image m_ConImg = null;
    //消耗品个数
    private Text m_ConTxt = null;
    //
    private Image m_MoheImg = null;
    

    public override void InitUIData()
    {
        base.InitUIData();

        m_MoheImg = selfTransform.FindChild("BoxImg").GetComponent<Image>();
        m_OpenBtn = selfTransform.FindChild("OpenBtn").GetComponent<Button>();
        m_OpenBtnTxt = selfTransform.FindChild("OpenBtn/Text").GetComponent<Text>();
        m_ConImg = selfTransform.FindChild("OpenBtn/ConImg").GetComponent<Image>();
        m_ConTxt = selfTransform.FindChild("OpenBtn/ConImg/Text").GetComponent<Text>();

        m_OpenBtn.onClick.AddListener(new UnityAction(OnOpenBntClick));
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_ConImg.gameObject.SetActive(false);
    }




    //设置消耗品显示
    public void SetConObjActive(bool active, int curNum, int conId)
    {
        ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(conId);
        m_ConImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
        m_ConTxt.text = curNum.ToString();
        m_ConImg.gameObject.SetActive(active);
    }

    public void SetConObjActive(bool active)
    {
        m_ConImg.gameObject.SetActive(active);
    }

    //设置按钮
    public void SetOpenBtnActive(bool active)
    {
        m_OpenBtn.gameObject.SetActive(active);

    }

    public void SetOpenBtnTxt()
    { 
        string text = GameUtils.getString("fight_bosbox_button2");
        m_OpenBtnTxt.text = text;
    }

    public void SetMoheResImg()
    {
        //_MoheImg.sprite = GameUtils.GetSpriteByResourceType(itemId);
        m_MoheImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_mohe_02");
        m_MoheImg.SetNativeSize();
        //_RewardTxt.text = ReturnConCount(conCount);
    }

    public void SetBtnImg()
    {
        m_OpenBtn.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_fenge_01");
        m_OpenBtnTxt.rectTransform.sizeDelta = new Vector2(204, 98);
        m_OpenBtnTxt.rectTransform.anchoredPosition = new Vector2(-102,0);
        //ItemTemplate item = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemId);
        //m_MoheImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + item.getIcon());
        //_RewardTxt.text = ReturnConCount(conCount);
    }


    void OnOpenBntClick()
    {
        int conCount = 0;
        GameConfig cofig = (GameConfig)DataTemplate.GetInstance().m_GameConfig;
        int conId = cofig.getOpen_bossbox_cost_id();
        int[] conNums = cofig.getOpen_bossbox_cost_num();
        List<BaseItem> baseItemList = ObjectSelf.GetInstance().CommonItemContainer.GetItemList(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON);
        int baseItemCount = baseItemList.Count;
        for (int i = 0; i < baseItemCount; ++i)
        {
            int baseItemId = baseItemList[i].GetItemTableID();
            if (baseItemId == conId)
            {
                int tempNum = baseItemList[i].GetItemCount();
                conCount += tempNum;
            }
        }

        if (conCount >= conNums[UI_SealBox.Inst.GetCurOpenNum()])
        {
            UI_SealBox.Inst.SendMsg(curPos);

        }
        else
        {
            string text = GameUtils.getString("fight_bosbox_tip1");
            InterfaceControler.GetInst().AddMsgBox(text,transform);
        }
    }

}
