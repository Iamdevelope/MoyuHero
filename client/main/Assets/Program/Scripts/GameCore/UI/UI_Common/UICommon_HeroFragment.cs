using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.UI;

public class UICommon_HeroFragment:BaseUI
{
    private Text m_HeroName; //英雄名称
    private Image m_Type; //职业类型
    private Text m_TypeText; //职业类型描述
    private Text m_FragmentCount; //当前碎片合成进度
    private Button m_Close; //关闭
    private Image m_HeroIcon; //英雄头标
    private Image m_Zizhi; //资质
    private Text m_Desc; //特点

    public override void InitUIData()
    {
        base.InitUIData();
        Transform panle = transform.FindChild("Panel");
        m_HeroName = panle.FindChild("ItemInfo/Name").GetComponent<Text>();
        m_Type = panle.FindChild("type").GetComponent<Image>();
        m_TypeText = panle.FindChild("text(type)").GetComponent<Text>();
        m_FragmentCount = panle.FindChild("ItemInfo/FagmentCount").GetComponent<Text>();
        m_Close = panle.FindChild("CloseBtn").GetComponent<Button>();
        m_Close.onClick.AddListener(OnClose);
        m_HeroIcon = panle.FindChild("ItemInfo/Icon").GetComponent<Image>();
        m_Zizhi = panle.FindChild("zizhi").GetComponent<Image>();
        m_Desc = panle.FindChild("Detail/Text").GetComponent<Text>();
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="heroId">英雄id</param>
    public void InitData(int heroId)
    {
        HeroTemplate hero=(HeroTemplate) DataTemplate.GetInstance().m_HeroTable.getTableData(heroId);
        if (hero == null)
        {
            Debug.LogError("找不到英雄数据 id:"+heroId);
            return;
        }
        m_HeroName.text = GameUtils.getString(hero.getTitleID());
        InterfaceControler.GetInst().ShowTypeIcon(hero, m_Type, m_TypeText);
        m_FragmentCount.text = string.Format("碎片数量：<color=#FF0000>{0}</color>/{1}", ObjectSelf.GetInstance().CommonItemContainer.GetFragmentCount(hero.GetID()),hero.getSyntheticCount());
        ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getArtresources());
        m_HeroIcon.sprite= UIResourceMgr.LoadSprite(common.defaultPath + _Artresourcedata.getHeadiconresource());
        m_Zizhi.sprite = InterfaceControler.GetInst().GetHeroAptImg(hero);
        m_Desc.text = GameUtils.getString(hero.getTedian());
        SetTextColorByQuilt(hero.getQuality());
    }
    void OnClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
    void SetTextColorByQuilt(int quilt)
    {
        switch (quilt)
        {
            case 1:
            case 0:
                m_HeroName.color = new Color(1, 1, 1);
                break;
            case 2:
                m_HeroName.color = new Color(15 / 255f, 208 / 255f, 0);
                break;
            case 3:
                m_HeroName.color = new Color(0, 155 / 255f, 1);
                break;
            case 4:
                m_HeroName.color = new Color(174 / 255f, 0, 1);
                break;
            case 5:
                m_HeroName.color = new Color(1, 144 / 255f, 0);
                break;
            case 6:
                m_HeroName.color = new Color(1, 0, 0);
                break;
            default:
                break;
        }
    }
}
