using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork.Data;
using System.Collections.Generic;

public class AdvancedSuccessWin : BaseUI 
{
    public static string UI_ResPath = "HeroStrengthen/UI_AdvancedSuccessWin_1_2";

    private HeroaddstageTemplate m_CurTData;
    private HeroaddstageTemplate m_NextTData;
    private int m_HeroStarLevel;
    private int m_HeroCurStage;

    private Transform m_AtrributesitemList;
    private GameObject m_Atrributesitem;
    private List<AdvancedSuccessItem> mPropAttributeList = null;

    public override void InitUIData()
    {
        base.InitUIData();
        mPropAttributeList =new List<AdvancedSuccessItem>();
        m_AtrributesitemList = selfTransform.FindChild("win/AttributesList/StartLayout").transform;
        m_Atrributesitem = selfTransform.FindChild("win/AttributesList/StartLayout/AdvancedSuccessItem").gameObject;
    }

    public void InitData(HeroaddstageTemplate _CurTData, HeroaddstageTemplate _NextTData)
    {
        m_CurTData = _CurTData;
        m_NextTData = _NextTData;
        m_HeroStarLevel = _CurTData.getQuality();
        m_HeroCurStage = _CurTData.getHalosPn();
        GreatAttributeItem();
        Invoke("onClose",2f);
    }

    /// <summary>
    /// 创建属性的Item
    /// </summary>
    private void GreatAttributeItem()
    {
        foreach (AdvancedSuccessItem attrItem in mPropAttributeList)
        {
            attrItem.Destroy();
        }
        mPropAttributeList.Clear();

        for (int i = 0; i < m_NextTData.getAttribute().Length; i++)
        {
            mPropAttributeList.Add(CreateNullAttriUI());
        }

        AdvancedSuccessItem ui_item = null;

        for (int i = 0; i < mPropAttributeList.Count; i++)
        {
            ui_item = mPropAttributeList[i];

            if (ui_item == null)
                continue;

            string type = GameUtils.GetAttriName(m_NextTData.getAttribute()[i]);

            int num_Cur;
            if (m_HeroStarLevel == 0 && m_HeroCurStage == 0)
                num_Cur = 0;
            else
                num_Cur = m_CurTData.getValue()[i];

            int num_Nex = m_NextTData.getValue()[i];
                ui_item.SetInfo(type, num_Cur.ToString(),num_Nex.ToString() );
                ui_item.SetActive(true);

        }
        
    }
    AdvancedSuccessItem CreateNullAttriUI()
    {
        Transform trans = (Transform)GameObject.Instantiate(m_Atrributesitem.transform);
        if (trans == null)
            return null;

        trans.parent = m_Atrributesitem.transform.parent;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
        return new AdvancedSuccessItem(trans);
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();

        if (Input.GetMouseButtonDown(0))
        {
            onClose();
        }
    }


    private void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        CancelInvoke();
    }
}

public class AdvancedSuccessItem
{
    protected Text text_1 = null;
    protected Text text_2 = null;
    protected Text text_3 = null;

    private Transform mTrans = null;

    public AdvancedSuccessItem(Transform trans)
    {
        if (trans == null)
            return;

        mTrans = trans;

        text_1 = mTrans.FindChild("Text_Number1").GetComponent<Text>();
        text_2 = mTrans.FindChild("Text_Number2").GetComponent<Text>();
        text_3 = mTrans.FindChild("Text_Number3").GetComponent<Text>();
    }

    public void SetInfo(string str1, string str2, string str3)
    {
        Clean();
        text_1.text = str1;
        text_2.text = str2;
        text_3.text = str3;
    }

    public void Destroy()
    {
        GameObject.DestroyImmediate(mTrans.gameObject);
    }

    void Clean()
    {
        text_1.text = "";
        text_2.text = "";
        text_3.text = "";
    }

    public void SetActive(bool active)
    {
        mTrans.gameObject.SetActive(active);
    }
}
