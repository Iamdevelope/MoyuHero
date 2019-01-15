using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DG.Tweening;
using DreamFaction.LogSystem;
using DreamFaction.UI;

public class LBShowItem : BaseUI
{
    public Text m_NameText;//名称
    public Image m_SpriteImage;//图标
    public Text m_NumText;//数量

    InnerdropTemplate item = new InnerdropTemplate();

    //符文
    private GameObject RuneIcon;//符文的父物体
    private Image mNorBg = null;
    private Image mSpecBg = null;
    private Image RuneImage = null;
    private GameObject[] mTypeObjs = null;
    private UIHeroStar star;

    public override void InitUIData()
    {
        base.InitUIData();

        m_SpriteImage = selfTransform.FindChild("BG").GetComponent<Image>();
        m_NameText = selfTransform.FindChild("Text").GetComponent<Text>();
        m_NumText = selfTransform.FindChild("Num/Text").GetComponent<Text>();
        star = transform.FindChild("star").GetComponent<UIHeroStar>();

        //符文
        RuneIcon = selfTransform.FindChild("RuneIconItem").gameObject;
        mNorBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg").GetComponent<Image>();
        mSpecBg = selfTransform.FindChild("RuneIconItem/RuneIconList/bg1").GetComponent<Image>();
        RuneImage = selfTransform.FindChild("RuneIconItem/RuneIconList/icon").GetComponent<Image>();
        mTypeObjs = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            mTypeObjs[i] = transform.FindChild("RuneIconItem/RuneIconList/bg/type" + (i + 1)).gameObject;
        }
        
    }
    /// <summary>
    /// 显示礼包
    /// </summary>
    /// <param name="ID">礼包的id</param>
    public void Init(int ID)
    {
        InnerdropTemplate item = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(ID);

        m_NumText.text = "x" +item.getDropnum().ToString();
        int _goid = item.getObjectid();//掉落物ID
        int itemid = item.getObjectid() / 1000000;

        switch (itemid)
        {
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES: //资源
                ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(_goid);
                if (_temp_res != null)
                {
                    string _tempIconNam_1 = _temp_res.getIcon3();
                    m_NameText.text = GameUtils.getString(_temp_res.getName());
                    m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_1);
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE://符文
                ItemTemplate _temp_rune = (ItemTemplate)DataTemplate.GetInstance().GetItemTemplateById(_goid);
                if (_temp_rune != null)
                {
                    m_NameText.text = GameUtils.getString(_temp_rune.getName());
                    m_SpriteImage.gameObject.SetActive(false);
                    RuneIcon.SetActive(true);
                    ShowRune(_temp_rune);
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON: //道具
                ItemTemplate _temp_common = (ItemTemplate)DataTemplate.GetInstance().GetItemTemplateById(_goid);
                if (_temp_common != null)
                {
                    string _tempIconNam_3 = _temp_common.getIcon_s();
                    m_NameText.text = GameUtils.getString(_temp_common.getName());
                    m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_3);
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO: //英雄
                HeroTemplate _temp_hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_goid);
                if (_temp_hero != null)
                {
                    int _tempIconNam_4 = _temp_hero.getArtresources();
                    m_NameText.text = GameUtils.getString(_temp_hero.getNameID()); 
                    ArtresourceTemplate _temp_Art = (ArtresourceTemplate)DataTemplate.GetInstance().GetArtResourceTemplate(_tempIconNam_4);
                    string _tempIconNam_5 = _temp_Art.getHeadiconresource();
                    m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_5);

                    m_NumText.transform.parent.gameObject.SetActive(false);
                    this.star.gameObject.SetActive(true);
                    int star = _temp_hero.getQuality();
                    int maxStar = _temp_hero.getMaxQuality();
                    this.star.Set(star, maxStar);
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN: //皮肤
                ArtresourceTemplate _temp_Art_2 = (ArtresourceTemplate)DataTemplate.GetInstance().GetArtResourceTemplate(_goid);
                if (_temp_Art_2 != null)
                {
                    m_NameText.text = GameUtils.getString(_temp_Art_2.getNameID());
                    string _tempIconNam_6 = _temp_Art_2.getHeadiconresource();
                    m_SpriteImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_6);
                }
                break;
        }
    }

    private void ShowRune(ItemTemplate _temp_rune)
    {
        string _tempIconNam_2 = _temp_rune.getIcon();
        RuneImage.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_2);

        bool isSpecial = RuneModule.IsSpecialRune(_temp_rune);
        SetIsSpecial(isSpecial);
        SetRuneType(_temp_rune.getRune_type());

        m_NumText.transform.parent.gameObject.SetActive(false);
        this.star.gameObject.SetActive(true);
        int star = _temp_rune.getRune_quality();
        int maxStar = 5;
        this.star.Set(star, maxStar);
    }
    public void SetIsSpecial(bool isSpecial)
    {
        mNorBg.gameObject.SetActive(!isSpecial);
        mSpecBg.gameObject.SetActive(isSpecial);
    }
    public void SetRuneType(int runeType)
    {
        SetRuneType((EM_RUNE_TYPE)runeType);
    }

    public void SetRuneType(EM_RUNE_TYPE type)
    {
        int idx = -1;
        switch (type)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                idx = 0;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                idx = 1;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                idx = 2;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                idx = 3;
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL:
                break;
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE:
                break;
            default:
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            mTypeObjs[i].SetActive(i == idx);
        }
    }
}