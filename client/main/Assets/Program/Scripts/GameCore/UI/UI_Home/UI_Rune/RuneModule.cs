using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;

public class RuneModule
{
    public static bool IsSpecialRune(ItemTemplate itemT)
    {
        if (itemT != null)
        {
            return itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL || itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE;
        }

        return false;
    }

    public static bool IsSpecialRune(int itemId)
    {
        ItemTemplate itemT = DataTemplate.GetInstance().GetItemTemplateById(itemId);

        if (itemT != null)
        {
            return itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL || itemT.getRune_type() == (int)EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE;
        }

        return false;
    }

    public static bool IsItemEquipEquiped(X_GUID guid)
    {
        BaseItem baseItem = ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, guid);

        if (baseItem != null)
        {
            ItemEquip itemE = baseItem as ItemEquip;

            return IsItemEquipEquiped(itemE);
        }

        return false;
    }

    public static bool IsItemEquipEquiped(ItemEquip itemE)
    {
        return ObjectSelf.GetInstance().HeroContainerBag.GetItemUser(itemE) != null;
    }

    /// <summary>
    /// 获得符文使用者的英雄名字;
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static string GetItemEuipHeroName(X_GUID guid)
    {
        BaseItem baseItem = ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, guid);

        if (baseItem != null)
        {
            ItemEquip itemE = baseItem as ItemEquip;

            return GetItemEuipHeroName(itemE);
        }

        return string.Empty;
    }

    public static string GetItemEuipHeroName(ItemEquip itemE)
    {
        if (itemE != null)
        {
            ObjectCard oc = ObjectSelf.GetInstance().HeroContainerBag.GetItemUser(itemE);
            if (oc != null)
            {
                HeroTemplate heroT = DataTemplate.GetInstance().GetHeroTemplateById(oc.GetHeroData().TableID);
                if (heroT != null)
                    return GameUtils.getString(heroT.getTitleID()) + "    " + GameUtils.getString("hero_rune_content7");
            }
        }

        return string.Empty;
    }

}
