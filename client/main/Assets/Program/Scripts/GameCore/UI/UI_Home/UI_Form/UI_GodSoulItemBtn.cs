using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.Utils;
using UnityEngine.Events;

public class UI_GodSoulItemBtn : BaseUI
{
    private Text m_TilteTxt = null;
    private Image m_Bg = null;
    private Button m_AddBtn = null;
    private Image m_GodSoulImg = null;
    private Button m_GodSoulBtn = null;

    private int m_No = -1;
    private int mId = -1;
    private ItemTemplate mItem = null;
    private GameObject m_Effct = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_Effct = selfTransform.FindChild("UI_Effect_Shenqizhuangpeichenggong01").gameObject;

        m_TilteTxt = selfTransform.FindChild("Text").GetComponent<Text>();
        m_Bg = selfTransform.FindChild("bg").GetComponent<Image>();
        m_GodSoulImg = selfTransform.FindChild("SoulBtn").GetComponent<Image>();
        m_AddBtn = selfTransform.FindChild("btn").GetComponent<Button>();
        m_AddBtn.onClick.AddListener(new UnityAction(onAdddBtnClick));
        m_GodSoulBtn = selfTransform.FindChild("SoulBtn").GetComponent<Button>();
        m_GodSoulBtn.onClick.AddListener(new UnityAction(onAdddBtnClick));

    }

    public void InitShowData(int id, int no)
    {
        mId = id;
        m_No = no;
        ShowUI();
        
        if (UI_FormMgr.Inst.GetGodSoulID() != -1)
        {
            if (UI_FormMgr.Inst.GetGodSoulID() == id)
            {
                m_Effct.SetActive(false);
                m_Effct.SetActive(true);
            }
         
        }
    }

    private void ShowUI()
    {
        switch (mId)
        {
            case -1:
                m_TilteTxt.text = "100级解锁";
                m_Bg.sprite = UI_FormMgr.Inst.ImgClose;
                m_GodSoulImg.gameObject.SetActive(false);
                m_AddBtn.gameObject.SetActive(false);
                break;
            case 0:
                m_TilteTxt.text = "点击添加";
                m_Bg.sprite = UI_FormMgr.Inst.Img;
                m_GodSoulImg.gameObject.SetActive(false);
                m_AddBtn.gameObject.SetActive(true);
                mItem = null;
                break;
            default:
                m_TilteTxt.text = "点击更换";
                m_Bg.sprite = UI_FormMgr.Inst.Img;
                m_GodSoulImg.gameObject.SetActive(true);
                m_AddBtn.gameObject.SetActive(false);
                mItem = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(mId);
                m_GodSoulImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + mItem.getIcon());
                break;
        }
    }

    private void onAdddBtnClick()
    {
        //UI_FormMgr.Inst.onClose();
        GameObject go = UI_HomeControler.Inst.AddUI(UI_GodSoulMgr.UI_ResPath);
        UI_GodSoulMgr uiGodSoulMgr = go.GetComponent<UI_GodSoulMgr>();
        uiGodSoulMgr.SetSelectItem(mItem, m_No);
    }

    //     private void onSoulBtnClick()
    //     {
    //         UI_FormMgr.Inst.onClose();
    //     }
    // 
    //     private void GotoGodSoulUI()
    //     { 


}