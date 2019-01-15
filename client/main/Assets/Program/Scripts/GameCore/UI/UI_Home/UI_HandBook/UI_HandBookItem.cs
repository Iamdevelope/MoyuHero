using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using UnityEngine.Events;

public class UI_HandBookItem : CellItem 
{
    private IllustratehandbookTemplate m_HeroHandBookData;
    private HeroTemplate m_HeroData;                       //英雄表数据
    private ArtresourceTemplate m_ArtRes;                  //资源表数据               
    private Text m_Nametxt;                                //英雄名字
    private Image m_Icon;                                  //英雄图标
    private Image m_AttackTypeImg;                         //攻击类型图标
    private Image m_JobTypeImg;                            //职业类型
    private Image m_RaceTypeImg;                           //种族类型
    private Image m_MedalImg;                              //勋章类型
    private Button m_Btn;                                  //当前英雄图鉴按钮

    public override void InitUIData()
    {
        base.InitUIData();
        m_Nametxt = selfTransform.FindChild("Parent/Name_txt").GetComponent<Text>();
        m_Icon = selfTransform.FindChild("Parent/Icon_Img").GetComponent<Image>();
        m_AttackTypeImg = selfTransform.FindChild("Parent/AttackType_Img").GetComponent<Image>();
        m_JobTypeImg = selfTransform.FindChild("Parent/JobType_Img").GetComponent<Image>();
        m_RaceTypeImg = selfTransform.FindChild("Parent/RaceType_Img").GetComponent<Image>();
        m_MedalImg = selfTransform.FindChild("Parent/MedalImg").GetComponent<Image>();
        m_Btn = this.GetComponent<Button>();
        m_Btn.onClick.AddListener(new UnityAction(OnClickBtn));
    }

    /// <summary>
    /// 初始化显示英雄数据
    /// </summary>
    /// <param name="heroHandBookData"></param>
    public void InitShowViewData(IllustratehandbookTemplate heroHandBookData)
    {
        m_HeroHandBookData = heroHandBookData;
        m_HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroHandBookData.getContentId());
        InterfaceControler.GetInst().ShowHeroImg(m_HeroData,m_AttackTypeImg,m_JobTypeImg,m_RaceTypeImg);
        //InitShowHeroImg();
        InitShowHeroNameAndIcon();
        InitShowHeroShar();
        InitIsGrey();
        ShowMedalImg();
    }

    /// <summary>
    /// 根据英雄星级显示勋章类型
    /// </summary>
    private void ShowMedalImg()
    {
        switch (m_HeroData.getQuality())
        {
            case 2:
                m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Tujian_chitie");
                break;
            case 3:
                m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Tujian_qingtong");
                break;
            case 4:
                m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Tujian_baiyin");
                break;
            case 5:
                m_MedalImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Tujian_huangjin");
                break;
            default :
                break;
        }
    }
    /// <summary>
    /// 初始化是否置灰以及是否显示勋章图标
    /// </summary>
    private void InitIsGrey()
    {
        GameUtils.SetImageGrayState(m_Icon, true);
        m_MedalImg.enabled = false;
        for (int i = 0; i < ObjectSelf.GetInstance().GetHeroHandBookList().Count; i++)
        {
            if (ObjectSelf.GetInstance().GetHeroHandBookList()[i].heroid == m_HeroData.getId())
            {
                GameUtils.SetImageGrayState(m_Icon, false);

                if (ObjectSelf.GetInstance().GetHeroHandBookList()[i].flag == 1)
                    m_MedalImg.enabled = true;
            }
        }
    }

    /// <summary>
    /// 初始化英雄称号 头像
    /// </summary>
    private void InitShowHeroNameAndIcon()
    {
        m_Nametxt.text = GameUtils.getString(m_HeroData.getTitleID());
        m_ArtRes = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_HeroData.getArtresources());
        m_Icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + m_ArtRes.getHeadartresource());
    }

    /// <summary>
    /// 初始化显示星级
    /// </summary>
    private void InitShowHeroShar()
    {
        InterfaceControler.GetInst().AddSharLevel(selfTransform.FindChild("Parent/Star_Image"), m_HeroData);
    }

    /// <summary>
    /// 点击英雄图鉴按钮
    /// </summary>
    private void OnClickBtn()
    {
        UI_HomeControler.Inst.AddUI(UI_HandBookHeroInfoPop.UI_ResPath);
        UI_HandBookHeroInfoPop.Inst.ShowInfo(m_HeroData);
    }
}
