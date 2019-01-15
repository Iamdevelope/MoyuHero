using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

using DreamFaction.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;

public class UICommon_Obtain : UICommon_ObtainBase, UICommonInterface
{
    protected Image iconImg = null;

    protected Transform itemListTran = null;
    protected GameObject getItemObj = null;

    private List<UICommonGetItem> getItems = new List<UICommonGetItem>();

    public override void InitUIData()
    {
        base.InitUIData();

        iconImg = selfTransform.FindChild("Panel/ItemInfo/Icon").GetComponent<Image>();

        itemListTran = selfTransform.FindChild("Panel/GetObj/ListObj/UIGrid");
        getItemObj = selfTransform.FindChild("Items/Item").gameObject;
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }
    

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.CommonObtain, this);
    }

    public void SetData(int itemid)
    {
        ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(itemid);

        if (itemT == null)
        {
            return;
        }

        PropsaccessTemplate propsT = UICommonModule.GetPropsacessTemplateByItemId(itemid);

        if (propsT == null)
        {
            return;
        }

        iconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + itemT.getIcon_s());
        m_Name.text = GameUtils.getString(itemT.getName());

        //TODO::这里应该区分物品类型，进而去不同背包获得物品数量;
        int count = 0;
        if (ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, itemid, ref count))
        {
        }
        else
        {
            count = 0;
        }
        m_Value.text = count.ToString();

        m_detail.text = GameUtils.getString(itemT.getDes());

        ClearItems();

        GenerateGetItem(propsT.getIcon1(), propsT.getTextcomment1(), propsT.getAccessType1(), propsT.getAccessThing1());
        GenerateGetItem(propsT.getIcon2(), propsT.getTextcomment2(), propsT.getAccessType2(), propsT.getAccessThing2());
        GenerateGetItem(propsT.getIcon3(), propsT.getTextcomment3(), propsT.getAccessType3(), propsT.getAccessThing3());
        GenerateGetItem(propsT.getIcon4(), propsT.getTextcomment4(), propsT.getAccessType4(), propsT.getAccessThing4());
        GenerateGetItem(propsT.getIcon5(), propsT.getTextcomment5(), propsT.getAccessType5(), propsT.getAccessThing5());
      
    }

    private void ClearItems()
    {
        for (int i = 0; i < getItems.Count; i++ )
        {
            getItems[i].Destroy();
        }
    }

    private void GenerateGetItem(string sprStr, string strId, int type, int data)
    {
        if (type != -1)
        {
            GameObject go = GameObject.Instantiate(getItemObj) as GameObject;
            if (go != null)
            {
                UICommonGetItem uiGetItem = new UICommonGetItem(go.transform, itemListTran);
                Sprite spri = UIResourceMgr.LoadSprite(common.defaultPath + sprStr);
                uiGetItem.SetData(spri, GameUtils.getString(strId), type, data);

                getItems.Add(uiGetItem);
            }
        }
    }

}


public class UICommonGetItem
{
    Transform mTrans = null;

    protected Image icon = null;
    protected Text detail = null;
    protected Button goBtn = null;
    protected Text hint = null;

    private int type = -1;
    private int data = -1;

    public UICommonGetItem(Transform trans,Transform parent)
    {
        mTrans = trans;
        mTrans.SetParent(parent, false);

        icon = mTrans.FindChild("Icon").GetComponent<Image>();
        detail = mTrans.FindChild("DropObj/detail").GetComponent<Text>();
        goBtn = mTrans.FindChild("Button").GetComponent<Button>();
        hint = mTrans.FindChild("HintTxt").GetComponent<Text>();
        
        goBtn.onClick.AddListener(OnGetBtnClick);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iconSpr"></param>
    /// <param name="text"></param>
    /// <param name="type">获得途径的类型</param>
    /// <param name="data">获得途径类型的参数</param>
    public void SetData(Sprite iconSpr, string text, int acessType,int acessData)
    {
        icon.sprite = iconSpr;
        detail.text = text;

        type = acessType;
        data = acessData;

        string errorStr = string.Empty;
        if (!UICommonModule.PropsacessChecker(type, data, out errorStr))
        {
            hint.text = errorStr;
            hint.gameObject.SetActive(true);
            goBtn.gameObject.SetActive(false);
        }
        else
        {
            hint.gameObject.SetActive(false);
            goBtn.gameObject.SetActive(true);            
        }
    }

    private void OnGetBtnClick()
    {
        UICommonModule.PropsacessHandler(type, data);
    }

    public void Destroy()
    {
        type = -1;
        data = -1;
        GameObject.Destroy(mTrans.gameObject);
    }
}