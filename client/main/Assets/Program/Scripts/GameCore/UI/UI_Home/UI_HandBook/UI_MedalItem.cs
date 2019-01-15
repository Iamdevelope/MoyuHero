using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using System.Text;
using GNET;

public class UI_MedalItem : UI_MedalItemBase
{
    private MedalexchangeTemplate m_MedalData;                        //数据表
    private Image m_MedalImg;                                         //勋章类型图标
    private Image m_Icon;                                             //奖励物品图标
    private GameObject m_YetFetchObj;                                 //已领取OBj
    private GameObject m_RuneIconObj;                                 //符文Obj
    private RuneIconItem m_RuneIconItem;                              

    public MedalexchangeTemplate GetMdedalData()
    {
        return m_MedalData;
    }
    public override void InitUIData()
    {
        base.InitUIData();
        m_MedalImg = selfTransform.FindChild("MedalImg").GetComponent<Image>();
        m_Icon = selfTransform.FindChild("Itemicon").GetComponent<Image>();
        m_YetFetchObj = selfTransform.FindChild("YetFetchObj").gameObject;
        m_RuneIconObj = selfTransform.FindChild("RuneIconItem").gameObject;
    }


    /// <summary>
    /// 显示UI数据
    /// </summary>
    /// <param name="medalData"></param>
    public void ShowUIData(MedalexchangeTemplate medalData)
    {
        m_MedalData = medalData;
        ShowMedalTypeImg();
        ShowReardData();
        ShowIsFetch();
        InitDesTxt();
    }
    /// <summary>
    /// 刷新显示UI数据
    /// </summary>
    public void UpdateUIdataShow()
    {
        ShowMedalTypeImg();
        ShowReardData();
        ShowIsFetch();
        UpdataOKPopup();
    }
    /// <summary>
    /// 初始化文本
    /// </summary>
    private void InitDesTxt()
    {
        m_YetFetchTxt.text = GameUtils.getString("pokedex_content12");
    }

    /// <summary>
    /// 显示勋章类型 个数
    /// </summary>
    private void ShowMedalTypeImg()
    {
        StringBuilder _str = new StringBuilder();
        _str.Append("/");
        _str.Append(m_MedalData.getNeedNum());
        m_MaxCountTXt.text = _str.ToString();

        if (m_MedalData.getExchangeType() == 1)
        {
            m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "huangjin");
            m_CurCountTxt.text = (Mathf.Min(m_MedalData.getNeedNum(), ObjectSelf.GetInstance().HuangjinXZ)).ToString();
        }
        else if (m_MedalData.getExchangeType() == 2)
        {
            m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "baiyin");
            m_CurCountTxt.text = (Mathf.Min(m_MedalData.getNeedNum(), ObjectSelf.GetInstance().BaiJinXZ)).ToString(); 
        }
        else if (m_MedalData.getExchangeType() == 3)
        {
            m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "qingtong");
            m_CurCountTxt.text = (Mathf.Min(m_MedalData.getNeedNum(), ObjectSelf.GetInstance().QingTongXZ)).ToString(); 
        }
        else if (m_MedalData.getExchangeType() == 4)
        {
            m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "chitie");
            m_CurCountTxt.text = (Mathf.Min(m_MedalData.getNeedNum(), ObjectSelf.GetInstance().ChiTieXZ)).ToString(); 
        }
    }
    /// <summary>
    /// 显示奖励物品数据
    /// </summary>
    private void ShowReardData()
    {
        switch (GameUtils.GetObjectClassById(m_MedalData.getRewardId()))
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_INVALID:
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SPELL://技能
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BUFF://buff
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_DROPBOX://掉落包
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER://关卡与怪物
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
                m_Icon.sprite = GameUtils.GetSpriteByResourceType(m_MedalData.getRewardId());
                m_Icon.SetNativeSize();
                StringBuilder _str = new StringBuilder();
                _str.Append("×");
                _str.Append(m_MedalData.getRewardNum());
                m_ReardNumTxt.text = _str.ToString();
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
                m_Icon.enabled = false;
                m_RuneIconObj.SetActive(true);

                ItemTemplate _item1 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(m_MedalData.getRewardId());
                m_RuneIconItem = new RuneIconItem(m_RuneIconObj.transform);
                m_RuneIconItem.SetIcon(common.defaultPath + _item1.getIcon());
                m_RuneIconItem.SetRuneType(_item1.getRune_type());
                m_RuneIconItem.SetLevelInfoActive(false);
                m_RuneIconItem.SetIsSpecial(RuneModule.IsSpecialRune(_item1));
                m_ReardNumTxt.text = GameUtils.getString(_item1.getName());
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON://道具
                ItemTemplate _item2 = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(m_MedalData.getRewardId());
                m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _item2.getIcon());
                m_Icon.SetNativeSize();
                m_ReardNumTxt.text = GameUtils.getString(_item2.getName());
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO://英雄
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN://皮肤
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BOX://宝箱
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_ARTIFACT://神器
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_NUMBER:
                break;
            default:
                break;
        }

    }


    /// <summary>
    /// 显示是否已经领取
    /// </summary>
    private void ShowIsFetch()
    {
        m_YetFetchObj.SetActive(false);
        GameUtils.SetBtnSpriteGrayState(m_FetchBtn,false);
        for (int i = 0; i < ObjectSelf.GetInstance().GetHandBookBoxList().Count; i++)
        {
            if (ObjectSelf.GetInstance().GetHandBookBoxList().Contains(m_MedalData.getId()))
            {
                m_YetFetchObj.SetActive(true);
                GameUtils.SetBtnSpriteGrayState(m_FetchBtn, true);
            }
        }
    }
    /// <summary>
    /// 点击领取按钮
    /// </summary>
    protected override void OnClickFetchBtn()
    {
        //已经领取提示： “已领取，无法重复领取”
        for (int i = 0; i < ObjectSelf.GetInstance().GetHandBookBoxList().Count; i++)
        {
            if (ObjectSelf.GetInstance().GetHandBookBoxList().Contains(m_MedalData.getId()))
            {
                UI_MedalReard.Inst.PopupShow(GameUtils.getString("pokedex_bubble1"));
                return;
            }
        }
        //如果没有领取过  当前勋章数量又大于需要数量  发送领取消息
        //否则 弹窗提示 “条件不足，无法领取”
        switch (m_MedalData.getExchangeType())
        {
            case 1:
                if(m_MedalData.getNeedNum() <= ObjectSelf.GetInstance().HuangjinXZ)
                {
                    OnSendMsg();
                }
                else
                {
                    UI_MedalReard.Inst.PopupShow(GameUtils.getString("pokedex_bubble2"));
                }
                break;
            case 2:
                if (m_MedalData.getNeedNum() <= ObjectSelf.GetInstance().BaiJinXZ)
                {
                    OnSendMsg();
                }
                else
                {
                    UI_MedalReard.Inst.PopupShow(GameUtils.getString("pokedex_bubble2"));
                }
                break;
            case 3:
                if (m_MedalData.getNeedNum() <= ObjectSelf.GetInstance().QingTongXZ)
                {
                    OnSendMsg();
                }
                else
                {
                    UI_MedalReard.Inst.PopupShow(GameUtils.getString("pokedex_bubble2"));
                }
                break;
            case 4:
                if (m_MedalData.getNeedNum() <= ObjectSelf.GetInstance().ChiTieXZ)
                {
                    OnSendMsg();
                }
                else
                {
                    UI_MedalReard.Inst.PopupShow(GameUtils.getString("pokedex_bubble2"));
                }
                break;
        }

    }

    /// <summary>
    /// 消息包
    /// </summary>
    private void OnSendMsg()
    {
        CTuJianBox _cjb = new CTuJianBox();
        _cjb.boxid = m_MedalData.getId();
        IOControler.GetInstance().SendProtocol(_cjb);
    }

    /// <summary>
    /// 刷新显示弹窗
    /// </summary>
    /// <returns></returns>
    public void UpdataOKPopup()
    {
        string _text = "";
        switch (GameUtils.GetObjectClassById(m_MedalData.getRewardId()))
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_INVALID:
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SPELL://技能
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BUFF://buff
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_DROPBOX://掉落包
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER://关卡与怪物
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
                ResourceindexTemplate _resData = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(m_MedalData.getRewardId());
                StringBuilder _str = new StringBuilder();
                _str.Append(GameUtils.getString("pokedex_bubble3"));
                _str.Append(GameUtils.getString(_resData.getName()));
                _str.Append("×");
                _str.Append(m_MedalData.getRewardNum());
                _text = _str.ToString();
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
                _text = ItemShowPupop(m_MedalData.getRewardId());
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON://道具
                _text = ItemShowPupop(m_MedalData.getRewardId());
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO://英雄
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN://皮肤
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_BOX://宝箱
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_ARTIFACT://神器
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_NUMBER:
                break;
            default:
                break;
        }

        UI_MedalReard.Inst.PopupShow(_text);
    }

    /// <summary>
    /// 道具显示
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    private string ItemShowPupop(int itemID)
    {
        ItemTemplate _itemDta = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(itemID);
        StringBuilder _str = new StringBuilder();
        _str.Append(GameUtils.getString("pokedex_bubble3"));
        _str.Append(GameUtils.getString(_itemDta.getName()));
        _str.Append("×");
        _str.Append(m_MedalData.getRewardNum());
        return _str.ToString();
    }

}
