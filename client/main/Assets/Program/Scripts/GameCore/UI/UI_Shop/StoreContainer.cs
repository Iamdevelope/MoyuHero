using GNET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StoreContainer
{
    private List<BaseStore> m_StoreList = new List<BaseStore>();

    public StoreContainer()
    { 
        
    }

    public List<BaseStore> GetStoreList()
    {
        return m_StoreList;
    }

    public void RefreshStore(Hashtable hashTable)
    {
        List<int> hashKeys = new List<int>();
        List<NewShopList> hashValues = new List<NewShopList>();

        foreach (DictionaryEntry item in hashTable)
        {
            hashKeys.Add((int)item.Key);
            hashValues.Add((NewShopList)item.Value);
        }

        for (int i = 0; i < hashValues.Count; i++)
        {
            NewShopList nsl = hashValues[i];
            BaseStore bs;
            if (m_StoreList.Count < hashValues.Count)
            {
                bs = new BaseStore();
                m_StoreList.Add(bs);
            }
            else
                bs = m_StoreList[i];

            bs.MStoreId = hashKeys[i];
            bs.MRefreshTime = (int)nsl.lasttime;
            bs.MRefreshCount = nsl.refreshnum;
            List<NewShop> temp = nsl.shoplist.ToList<NewShop>();

            for (int j = 0; j < temp.Count; j++)
            {
                NewShop ns = temp[j];
                Goods gs;
                if (bs.MGoodsList.Count < temp.Count)
                {
                    gs = new Goods();
                    bs.MGoodsList.Add(gs);
                }
                else
                    gs = bs.MGoodsList[j];

                gs.MTabelId = ns.itemid;
                gs.MCosId = ns.costtype;
                gs.MPrice = ns.price;
                gs.MNumbar = ns.num;
                gs.MIsbuy = ns.isbuy;
            }
        } 
    }
}
