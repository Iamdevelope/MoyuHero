using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using System.Collections.Generic;

public class UICommon_MsgBox : UICommon_MsgBoxBase, UICommonInterface
{
    private object mParam = null;
    private UnityAction<object> mYesAction = null;
    private UnityAction<object> mNoAction = null;

    public object Param
    {
        get
        {
            return mParam;
        }
        set
        {
            mParam = value;
        }
    }

    public override void InitUIData()
    {
        base.InitUIData();
    }

    void OnDestroy()
    {
        mParam = null;
        mYesAction = null;
        mNoAction = null;
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        if (mNoAction != null)
        {
            mNoAction(mParam);
            mNoAction = null;
        }

        Close();
    }

    private void Close()
    {
        UICommonManager.Inst.RemoveUI(UICommonType.CommonMsgBox, this);
    }

    protected override void OnClickYesBtn()
    {
        base.OnClickYesBtn();
        
        if (mYesAction != null)
        {
            mYesAction(mParam);
            mYesAction = null;
        }

        Close();
    }

    public void SetData(string title, string detail, string hint = "", string yesBtnTxt = "", UnityAction<object> yesAction = null, UnityAction<object> noAction = null, object param = null)
    {
        m_Text.text = title;
        m_DetailTxt.ShowRichText(detail);
        m_HintTxt.ShowRichText(hint);
        if (string.IsNullOrEmpty(yesBtnTxt))
        {
            m_yesTxt.text = GameUtils.getString("common_button_ok");
        }
        else
        {
            m_yesTxt.text = yesBtnTxt;
        }
        mYesAction = yesAction;
        mNoAction = noAction;
        mParam = param;
    }

    public void SetDetail(string detail)
    {
        m_DetailTxt.ShowRichText(detail);
    }

    public void SetYesAction(UnityAction<object> action)
    {
        mYesAction = action;
    }
}
