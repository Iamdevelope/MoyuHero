using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;

public class UI_StoreItem : BaseUI 
{
    private BaseStore m_Store = null;
    private ShangdianTemplate m_StoreT = null;

    private Image m_StoreIcon = null;
    private Text m_StoreNameTxt = null;
    private Button m_StoreBtn = null;

    private GameObject m_LockOBJ = null;
    private Text m_OpenDesText = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_LockOBJ = selfTransform.FindChild("LockOBJ").gameObject;
        m_OpenDesText = selfTransform.FindChild("LockOBJ/Text").GetComponent<Text>();

        m_StoreIcon = selfTransform.FindChild("StoreIcon").GetComponent<Image>();
        m_StoreNameTxt = selfTransform.FindChild("StoreNameTxt").GetComponent<Text>();
        m_StoreBtn = GetComponent<Button>();
        m_StoreBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onStoreBtnClick));
    }

    public void InitItemData(BaseStore store)
    {
        m_Store = store;
        m_StoreT = store.GetStoreRow();

        m_StoreIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_StoreT.getStoreIocn());
        m_StoreNameTxt.text = GameUtils.getString(m_StoreT.getStoreName());

        m_LockOBJ.SetActive(!SetOpenState());
        if (!SetOpenState())
        {
            string str = "";
            switch (m_StoreT.getStoreOpen())
            {
                case 1:
                    str = string.Format("", m_StoreT.getConditionalData());
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    str = string.Format("", m_StoreT.getConditionalData());
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            m_OpenDesText.text = str;
        }
    }


    private bool SetOpenState()
    {
        switch (m_StoreT.getStoreOpen())
        {
            case 1:
                if (ObjectSelf.GetInstance().Level >= m_StoreT.getConditionalData())
                    return true;
                else return false;
            case 2:
                if (m_StoreT.getConditionalData() == -1)
                    return true;
                else return false;
            case 3:
                if (m_StoreT.getConditionalData() == -1)
                    return true;
                else return false;
            case 4:
                if (ObjectSelf.GetInstance().VipLevel >= m_StoreT.getConditionalData())
                    return true;
                else return false;
            case 5:
                if (m_StoreT.getConditionalData() == -1)
                    return true;
                else return false;
            default:
                return false;
        }
    }


    private void onStoreBtnClick()
    {
        GameObject go = UI_HomeControler.Inst.AddUI(UI_StoreGoodsMgr.UI_ResPath);
        UI_StoreGoodsMgr uiStoreGoogsMgr = go.GetComponent<UI_StoreGoodsMgr>();
        uiStoreGoogsMgr.InitStoreDta(m_Store);
    }

}
