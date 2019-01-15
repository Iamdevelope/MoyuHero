using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameCore;
using GNET;
using DreamFaction.GameEventSystem;
using System.Text;
using DreamFaction.LogSystem;

public class UI_HeroSkinItem : BaseUI 
{
    public HE_SKIN_STATE HeroSkinState = HE_SKIN_STATE.SKIN_NOT_HAVE;

    private Image m_SkinIconImg = null;              //皮肤原画
    private Text m_SkinNameTxt = null;               //皮肤名称 
    private GameObject m_SkinSelectsed = null;       //默认选择的皮肤显示效果
    private GameObject m_SkinLock = null;            //未解锁的效果
    private Button m_Btn = null;                     //自身按钮

    private int m_SkinKey;
    private int m_HeroKey;
    private GameObject m_AttriPrefab;
    private GameObject m_AttriObj;           //皮肤描述

    public override void InitUIData()
    {
        base.InitUIData();
        m_SkinIconImg = selfTransform.FindChild("SkinIcon").GetComponent<Image>();
        m_SkinNameTxt = selfTransform.FindChild("SkinName").GetComponent<Text>();
        m_AttriObj = transform.FindChild("Attris").gameObject;
        m_AttriPrefab = transform.FindChild("Items/AttriPair").gameObject;
        m_SkinSelectsed = selfTransform.FindChild("SelectedSkin").gameObject;
        m_SkinLock = selfTransform.FindChild("lockImg").gameObject;
        m_Btn = selfTransform.GetComponent<Button>();
        m_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnBtnClick));

        //GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroSkin, ShowSelected);
    }

    void OnDestroy()
    {
        //GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_HeroSkin,ShowSelected);
    }
    
    /// <summary>
    /// 显示英雄Item
    /// </summary>
    /// <param name="SkinKey"></param>
    /// <param name="HeroKey"></param>
    public void ShowHeroSkinItem(int SkinKey,int HeroKey)
    {
        m_SkinKey = SkinKey;
        m_HeroKey = HeroKey;
        ArtresourceTemplate _ArtresData = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(SkinKey);
        m_SkinIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _ArtresData.getFashionresource());
        m_SkinNameTxt.text = GameUtils.getString(_ArtresData.getNameID());
        ShowSkinDes(_ArtresData);
        ShowSelected();
    }
    /// <summary>
    /// 根据状态显示
    /// </summary>
    void ShowSelected()
    {
        m_SkinLock.SetActive(true);
        m_SkinSelectsed.SetActive(false);
        switch (HeroSkinState)
        {
            //case HE_SKIN_STATE.SKIN_JINGDIAN:
            //    _SkinLock.SetActive(false);
            //    break;
            case HE_SKIN_STATE.SKIN_DEF:
                m_SkinLock.SetActive(false);
                m_SkinSelectsed.SetActive(true);
                break;
            case HE_SKIN_STATE.SKIN_HAVE:
                m_SkinLock.SetActive(false);
                break;
            case HE_SKIN_STATE.SKIN_NOT_HAVE:
                break;
        }
    }

    /// <summary>
    /// 时装按钮
    /// </summary>
    void OnBtnClick()
    {
        switch (HeroSkinState)
        {
            //case HE_SKIN_STATE.SKIN_JINGDIAN:
            //    //向服务器发消息
            //    break;
            case HE_SKIN_STATE.SKIN_DEF:
                //提示该时装已经穿戴
                string text = GameUtils.getString("fashion_bubble1");
                InterfaceControler.GetInst().AddMsgBox(text, transform.parent.parent.parent);
                break;
            case HE_SKIN_STATE.SKIN_HAVE:
                //向服务器发消息
                CChangeSkin cs = new CChangeSkin();
                cs.herokey = m_HeroKey;
                cs.skinid = m_SkinKey;
                IOControler.GetInstance().SendProtocol(cs);
                break;
            case HE_SKIN_STATE.SKIN_NOT_HAVE:
                //弹出商店链接
                string txt = GameUtils.getString("fashion_window1");
                UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
                box.SetIsNeedDescription(false);
                box.SetDescription_text(txt);
                box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
                box.SetLeftClick(OnGoToStore);
                break;
        }
    }

    /// <summary>
    /// 提示框按钮 用于跳转
    /// </summary>
    void OnGoToStore()
    {
        UI_HomeControler.Inst.ReMoveUI(UI_HeroInfo.UI_ResPath);
        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
        UI_HomeControler.Inst.AddUI(UI_ShopMgr.UI_ResPath);
        UI_ShopMgr.SetCurShowTab(SHOP_TAB.SKIN);
        UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
        UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;  
    }

    /// <summary>
    /// 显示时装属性描述
    /// </summary>
    /// <param name="artRes">资源表数据</param>
    private void ShowSkinDes(ArtresourceTemplate artRes)
    {
        int count = DataTemplate.GetInstance().GetArtResourceAtrriCount(artRes);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(artRes.getSymbol()[i]);

                if (artRes.getIspercentage()[i] == 1)
                {
                    float val = (float)(artRes.getAttriValue()[i]) / 10f;
                    sb.Append(val);
                    sb.Append("%");
                }
                else
                {
                    sb.Append(artRes.getAttriValue()[i]);
                }

                CreateAttriItem(artRes.getAttriDes()[i], sb.ToString());
            }
        }
    }

    /// <summary>
    /// 创建属性Item
    /// </summary>
    /// <param name="name"></param>
    /// <param name="val"></param>
    void CreateAttriItem(string name, string val)
    {
        GameObject go = GameObject.Instantiate(m_AttriPrefab) as GameObject;
        if (go == null)
        {
            LogManager.LogError("皮肤预览属性obj创建失败");
            return;
        }

        Transform trans = go.transform;

        Text left = trans.FindChild("Left_txt").GetComponent<Text>();
        left.text = name;
        Text right = trans.FindChild("Right_txt").GetComponent<Text>();
        right.text = val;

        trans.parent = m_AttriObj.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
    }

}
