using UnityEngine;
using System.Collections;
using GNET;
/// <summary>
/// 礼包类道具
/// </summary>
public class ItemGift : BaseItem 
{
    public override int GetItemType()
    {
        return (int)EM_ITEM_TYPE.EM_ITEM_TYPE_GIFT;
    }

    public override void OnUseItem(int _count)
    {
        if (_count > m_Count)
        {
            return;
        }

        CUseItem msg = new CUseItem();
        msg.bagid = (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON;
        msg.itemkey = (int)m_BaseGuid.GUID_value;
        msg.num = (short)_count;
        IOControler.GetInstance().SendProtocol(msg);
    }

    public override void OnSellItem(int _count)
    {
        if (_count > m_Count)
        {
            return;
        }

        CSellItem msg = new CSellItem();
        msg.bagid = (int)EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_COMMON;
        msg.items.Add((int)m_BaseGuid.GUID_value, _count);
        IOControler.GetInstance().SendProtocol(msg);
    }
}
