using UnityEngine;
using System.Collections;
using GNET;
using System.Collections.Generic;

/// <summary>
/// 商店基类
/// </summary>
public class BaseStore 
{
    private int mStoreId;
    private int mRefreshTime;
    private int mRefreshCount;


    private List<Goods> mGoodsList = new List<Goods>();

    private float mTempTime = 0.0f;

    public int MStoreId
    {
        get { return mStoreId; }
        set { mStoreId = value; }
    }
    private ShangdianTemplate mStoreT;


    public int MRefreshTime
    {
        get { return mRefreshTime; }
        set { mRefreshTime = value; }
    }

    public int MRefreshCount
    {
        get { return mRefreshCount; }
        set { mRefreshCount = value; }
    }

    public List<Goods> MGoodsList
    {
        get { return mGoodsList; }
        set { mGoodsList = value; }
    }




    public BaseStore()
    {

    }


    public ShangdianTemplate GetStoreRow()
    {
        mStoreT = (ShangdianTemplate)DataTemplate.GetInstance().m_ShangdianTable.getTableData(mStoreId);
        return mStoreT;
    }


    public void UpdateRefTime()
    {
        mTempTime += Time.deltaTime;

        if (mTempTime >= 1f)
        {
            mTempTime = 0f;
            if (mRefreshTime <= 0)
            {

            }
            else
            {
                mRefreshTime--;
            }
        }

    }


}

