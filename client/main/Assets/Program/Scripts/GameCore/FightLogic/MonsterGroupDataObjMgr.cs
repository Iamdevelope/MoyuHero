using UnityEngine;
using System.Collections;
using DreamFaction.GameNetWork;
using DreamFaction.GameSceneEditor;
using DreamFaction.LogSystem;
public class MonsterGroupDataObjMgr
{
    private MonsterGroupDataObj mGroupData;

    public MonsterGroupDataObjMgr(MonsterGroupDataObj dataObj)
    {
        mGroupData = dataObj;
    }


    public int Count
    {
        get
        {
            if (ObjectSelf.GetInstance().IsLimitFight)
            {
                return mGroupData.MonsterGroupDataList.Count / 2;
            }

            return mGroupData.MonsterGroupDataList.Count;
        }
    }

    public MonsterGroupDataObj GroupData
    {
        get
        {
            return mGroupData;
        }
    }

    public MonstersGroupData this[int idx]
    {
        get
        {
            int troopType = 1;
            
            if (ObjectSelf.GetInstance().IsLimitFight)
            {
                troopType = ObjectSelf.GetInstance().LimitFightMgr.m_MonsterTroopType;
            }

            int curTroopType = 0;
            for (int i = 0; i < mGroupData.MonsterGroupDataList.Count; i++ )
            {
                if (mGroupData.MonsterGroupDataList[i].MonstersGroupID == idx)
                {
                    curTroopType++;

                    if (curTroopType == troopType)
                    {
                        return mGroupData.MonsterGroupDataList[i];
                    }
                }
            }

            string errStr = "第" + idx + "怪物组数据，缺少第" + troopType + "阵型点";
            LogManager.LogError(errStr);
            LogManager.LogToFile(errStr);

            return mGroupData.MonsterGroupDataList[idx];
        }
    }
}
