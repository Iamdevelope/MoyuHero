using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 新商品基类
/// </summary>
public class Goods
{
    private int mTabelId; // 77表的道具ID
    private int mCosId; // 消耗资源
    private int mPrice; // 价格
    private int mNumbar; // 数量
    private int mIsbuy; // 0未购买，1为已购买

    private ItemTemplate mItemT;

    public Goods()
    {

    }

    public int MTabelId
    {
        get { return mTabelId; }
        set { mTabelId = value; }
    }

    public int MCosId
    {
        get { return mCosId; }
        set { mCosId = value; }
    }

    public int MPrice
    {
        get { return mPrice; }
        set { mPrice = value; }
    }

    public int MNumbar
    {
        get { return mNumbar; }
        set { mNumbar = value; }
    }


    public int MIsbuy
    {
        get { return mIsbuy; }
        set { mIsbuy = value; }
    }


    public ItemTemplate GetItemT()
    {
        mItemT = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(mTabelId);
        return mItemT;
    }



}
