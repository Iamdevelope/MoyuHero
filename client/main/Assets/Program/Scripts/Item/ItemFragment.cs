using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GNET;

/// <summary>
/// 碎片类
/// </summary>
public class ItemFragment : BaseItem
{
    private int m_ComposeHeroid;//合成的英雄的id
    public ItemFragment()
    {
    }
    /// <summary>
    /// 获取碎片合成的英雄id 表id
    /// </summary>
    public int GetComposeHeoid()
    {
        List<IExcelBean> datas = DataTemplate.GetInstance().m_HeroTable.getDataList();
        HeroTemplate hero;
        foreach (IExcelBean item in datas)
        { 
            string endChar = item.GetID().ToString().Substring(item.GetID().ToString().Length - 1, 1);
            string paixuId = ((HeroTemplate)item).getPaxid().ToString();
            if ((hero = (HeroTemplate)item).getSyntheticItemid() == m_BaseTableID&&endChar.Contains(paixuId))
            {
                m_ComposeHeroid = hero.GetID();
                break;
            }
        }
        return m_ComposeHeroid;
    }
    public override int GetItemType()
    {
        return (int)EM_ITEM_TYPE.EM_ITEM_TYPE_FRAGMENT;
    }
    public override void SetItemGuid(long guid)
    {
        base.SetItemGuid(guid);
    }
    public override void OnSellItem(int _count)
    {
       
    }
    public override void OnUseItem(int _count)
    {
        if (_count > m_Count) return;
        CHeroCompose msg = new CHeroCompose();
        msg.heroid = m_ComposeHeroid;
        IOControler.GetInstance().SendProtocol(msg);    
 
    }
}
