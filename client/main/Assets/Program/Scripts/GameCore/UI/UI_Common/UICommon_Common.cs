using UnityEngine;
using UnityEngine.UI;

using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;

public class UICommon_Common : UICommon_CommonBase, UICommonInterface
{
    protected Image icon = null;

    public override void InitUIData()
    {
        base.InitUIData();
        
        icon = selfTransform.FindChild("Panel/ItemInfo/Icon").GetComponent<Image>();
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.Common, this);
    }

    public void SetData(int tableID)
    {
        int quality = -1;
        string iconStr = string.Empty;
        string name = string.Empty;
        string hintTxt = string.Empty;
        string detail = string.Empty;

        EM_OBJECT_CLASS eoc = GameUtils.GetObjectClassById(tableID);
        switch (eoc)
        {
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RUNE:
                ItemTemplate runeItemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (runeItemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                    return;
                }
                iconStr = runeItemT.getIcon_s();
                name = GameUtils.getString(runeItemT.getName());
                int count = 0;
                if (ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, tableID, ref count))
                {
                }
                else
                {
                    count = 0;
                }
                hintTxt = string.Format(GameUtils.getString("tongyong_daoju1"), count);
                detail = GameUtils.getString(runeItemT.getDes());
                quality = runeItemT.getRune_quality();
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_COMMON:
                ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(tableID);
                if (itemT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                    return;
                }

                iconStr = itemT.getIcon_s();
                name = GameUtils.getString(itemT.getName());

                int num = 0;
                if (ObjectSelf.GetInstance().TryGetItemCountById(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON, tableID, ref num))
                {
                }
                else
                {
                    num = 0;
                }
                hintTxt = string.Format(GameUtils.getString("tongyong_daoju1"), num);
                detail = GameUtils.getString(itemT.getDes());
                quality = itemT.getQuality();
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER:
                MonsterTemplate monsterT = DataTemplate.GetInstance().m_MonsterTable.getTableData(tableID) as MonsterTemplate;
                if (monsterT == null)
                {
                    LogManager.LogError("item表格中缺少物品id=" + tableID);
                    return;
                }

                ArtresourceTemplate artMonster = DataTemplate.GetInstance().GetArtResourceTemplate(monsterT.getArtresources());
                if (artMonster == null)
                {
                    LogManager.LogError("ArtResource时装表格中缺少物品id=" + monsterT.getArtresources());
                    return;
                }

                iconStr = artMonster.getHeadiconresource();
                name = GameUtils.getString(monsterT.getMonstername());
                hintTxt = string.Format("Lv{0}", monsterT.getMonsterlevel());
                detail = GameUtils.getString(monsterT.getDescriptionID());
                quality = monsterT.getMonsterstar();
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_HERO:
                HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(tableID);
                if (heroT == null)
                {
                    LogManager.LogError("hero表格中缺少物品id=" + tableID);
                    return;
                }
                ArtresourceTemplate artHero = DataTemplate.GetInstance().GetArtResourceTemplate(heroT.getArtresources());
                if (artHero == null)
                {
                    LogManager.LogError("ArtResource时装表格中缺少物品id=" + heroT.getArtresources());
                    return;
                }

                iconStr = artHero.getHeadiconresource();
                //英雄的初始品质为HeroTemplate中的Quality;
                name = string.Format(GameUtils.GetHeroNameFontColor(heroT.getQuality()), GameUtils.getString(heroT.getNameID()));
                ObjectCard oc = ObjectSelf.GetInstance().HeroContainerBag.FindHero(tableID);
                hintTxt = string.Format("Lv{0}", oc == null ? 1 : oc.GetHeroData().Level);
                detail = GameUtils.getString(heroT.getDescriptionID());
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_SKIN:
                ArtresourceTemplate artT = DataTemplate.GetInstance().GetArtResourceTemplate(tableID);
                if (artT == null)
                {
                    LogManager.LogError("ArtResource时装表格中缺少物品id=" + tableID);
                    return;
                }
                
                iconStr = string.Empty;
                name = string.Empty;
                hintTxt = string.Empty;
                detail = string.Empty;
                break;
            case EM_OBJECT_CLASS.EM_OBJECT_CLASS_RES:
                //资源类型点击无响应;
                iconStr = string.Empty;
                name = string.Empty;
                hintTxt = string.Empty;
                detail = string.Empty;
                break;
            default:
                LogManager.LogError("未处理的商城物品预览类型");
                break;
        }
        name = string.Format(GameUtils.GetHeroNameFontColor(quality), name);
        SetData(iconStr, name, hintTxt, detail);
    }

    /// <summary>
    /// iconStr 为sprite 名字，不带路径的;
    /// </summary>
    /// <param name="iconStr"></param>
    /// <param name="name"></param>
    /// <param name="hintTxt"></param>
    /// <param name="deital"></param>
    public void SetData(string iconStr, string name, string hintTxt, string deital)
    {
        icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + iconStr);
        m_Name.text = name;
        m_HintTxt.text = hintTxt;
        m_Text.text = deital;
    }
}
