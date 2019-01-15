using UnityEngine;
using System.Collections;
using DreamFaction.Utils;
using DreamFaction.UI;
public class UI_LivenessDropItem : UI_LivenessDropItemBase {

    public void Data(object data)
    {
        InnerdropTemplate value = data as InnerdropTemplate;
        if (value == null) return;
        
        int itemid = value.getObjectid();
        int type = itemid / 1000000;
        switch (type)
        {
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                ResourceindexTemplate _temp_res = (ResourceindexTemplate)DataTemplate.GetInstance().m_ResourceindexTemplate.getTableData(itemid);
                if (_temp_res != null)
                {
                    this.desc.text = GameUtils.getString(_temp_res.getName());
                    string _tempIconNam_1 = _temp_res.getIcon3();
                    this.icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _tempIconNam_1);
                    this.numText.text = "x" + value.getDropnum();
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE: //符文
                {
                    ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(value.getObjectid());
                    this.desc.text = GameUtils.getString(itemTable.getName());
                    this.icon.gameObject.SetActive(false);
                    RuneIcon.SetActive(true);
                    ShowRune(itemTable);
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                {
                    ItemTemplate itemTable = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(value.getObjectid());
                    //UI_RewardsItemManage uifigt = item.GetComponent<UI_RewardsItemManage>();
                    //uifigt.id = value.getObjectid();
                    //uifigt.typeNum = 2;
                    this.desc.text = GameUtils.getString(itemTable.getName());
                    this.icon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + itemTable.getIcon());
                    this.numText.text = "x" + value.getDropnum().ToString();
                }
                break;
            case (int)EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                {
                    HeroTemplate hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(value.getObjectid());
                    //UI_RewardsItemManage uihero = item.GetComponent<UI_RewardsItemManage>();
                    //uihero.id = inner.getObjectid();
                    //uihero.typeNum = 3;
                    this.desc.text = GameUtils.getString(hero.getTitleID());//"英雄";
                    ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getArtresources());
                    this.icon.overrideSprite = UIResourceMgr.LoadSprite(common.defaultPath + art.getHeadiconresource());
                    int star = hero.getQuality();
                    int maxStar = hero.getMaxQuality();
                    this.star.Set(star, maxStar);
                }
                break;

            default:
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

        this.numText.transform.parent.gameObject.SetActive(false);
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
