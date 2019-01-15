using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;
using DreamFaction.SkillCore;
using GNET;
public class SCANOPERATOR_INIT
{
	public int						m_Type;

	public int						m_ChildType;

	public int						m_Param;

	public List<ObjectCreature>		m_ObjectList = new List<ObjectCreature>();

	public SCANOPERATOR_INIT()
	{
		m_Type = -1;
		m_ChildType = -1;
		m_Param = -1;
		m_ObjectList.Clear();
	}
}

public class _StrSort : IComparable<_StrSort>
{
    public int nIndex;
    public float nDis;

    public int CompareTo(_StrSort other)
    {
        return 0;
    }
}
/// <summary>
/// 用于扫描目标时，根据距离自定义排序算法 [2/2/2015 Zmy]
/// </summary>
public class CComparer : IComparer<_StrSort>
{
    public int Compare(_StrSort left, _StrSort right)
    {
        if (left.nDis > right.nDis)
            return 1;
        else if (left.nDis == right.nDis)
            return 0;
        else
            return -1;
    }
}

public class SceneObjectManager : BaseControler
{
    //该时间数据用于AI逻辑，每帧累加，不会停止。
    private float m_InBattleSceneTime = 0f;
	private static SceneObjectManager _inst;
	public static SceneObjectManager GetInstance()
	{
        return _inst;
	}

	private List<ObjectHero>		    m_pObjcetHeroMgr			    = new List<ObjectHero>();
	private List<ObjectMonster>		    m_pObjectMonsterMgr			    = new List<ObjectMonster>();
    private CampaignMonsterGroupData    m_MonsterGroupData              = new CampaignMonsterGroupData();
    private List<ObjectCreature>        m_Myself                        = new List<ObjectCreature>();
    private List<ObjectCreature>        m_Enemy                         = new List<ObjectCreature>();
    private List<ObjectCreature>        m_DeadObjList                   = new List<ObjectCreature>();

    private List<FightInfo>             m_CacheFightInfo                = new List<FightInfo>();

    //当前是否处于集火目标状态。此标记状态只对本方有效 [3/27/2015 Zmy]
    private bool                        m_bIsFireSignState;
    private ObjectCreature              m_FireSignObj;//集火对象
    private int                         m_DieHeroCount = 0;//死亡英雄数量
    private GameObject                  FireSignGameObj;

    private long                        m_WorldBossDamageSum;//世界BOSS伤害累计 [6/30/2015 Zmy]

    private bool                        m_IsHeroMonveTargetDone = false;
    private bool                        m_IsMonsterMonveTargetDone = false;

    public float TimeInBattleScene
    {
        get { return m_InBattleSceneTime; }
    }
    // ============================= 继承接口 =================================
		// 第一步初始化数据 [1/19/2015 Zmy]
	protected override void InitData()
    {
        if (_inst == null)
        {
            _inst = this;
        }
        else
        {
            GameObject.Destroy(this);
        }
        m_bIsFireSignState = false;
        UpdateMonsterGroupData();

       
        //监听技能开启事件
        GameEventDispatcher.Inst.addEventListener(GameEventID.SE_RequestReleaseSkill, OnRecieveSkillTarget);
        GameEventDispatcher.Inst.addEventListener(GameEventID.F_FightStateUpdate, OnFightStatuChange);
    }
    protected override void DestroyData()
    {
        GameEventDispatcher.Inst.clearEvents();
    }
    private void OnFightStatuChange()
    { 
        if (FightControler.Inst == null || FightControler.Inst.GetFightState() != FightState.Fighting)
            return;

        m_pObjcetHeroMgr.ForEach(p=>p.FightingTimestamp = m_InBattleSceneTime);
        m_pObjectMonsterMgr.ForEach(p => p.FightingTimestamp = m_InBattleSceneTime);

    }
    private void OnRecieveSkillTarget(GameEvent temp)
    {
        EventRequestSkillPackage Temp = (EventRequestSkillPackage)temp.data;
        OnFree_PveHeroSkill(ref Temp);
    }
	protected override void UpdateState()
    {
        m_InBattleSceneTime += Time.deltaTime;
        if (FightControler.Inst != null)
        {
            switch (FightControler.Inst.GetFightState())
            {
                case FightState.prepareData:
                    break;
                case FightState.HeroMove:
                    OnUpdateFightingLogic();
                    break;
                case FightState.prepareEnemy:
                    break;
                case FightState.FightStory:
                    break;
                case FightState.FightStoryCamera:
                    break;
                case FightState.prepareFight:
                    break;
                case FightState.Fighting:
                    OnUpdateFightingLogic();
                    OnUpdateFightingAILogic();
                    break;
                case FightState.FightOver:
                    OnUpdateFightingLogic();
                    break;
                case FightState.FightWin:
                    break;
                case FightState.FightLose:
                    break;
                case FightState.PrepareBoard:
                    break;
                case FightState.PrepareBoardOver:
                    break;
                case FightState.Boarding:
                    break;
                case FightState.BoardingOver:
                    OnUpdateFightingLogic();
                    break;
                case FightState.FightInstantiateHero:
                    break;
                default:

                    break;
            }
        }
    }
    void Destroy()
    {
        m_pObjcetHeroMgr.Clear();
        m_pObjcetHeroMgr = null;
        m_pObjectMonsterMgr.Clear();
        m_pObjectMonsterMgr = null;
        m_MonsterGroupData.CleanUp();
        m_MonsterGroupData = null;
        m_DeadObjList.Clear();
        m_DeadObjList = null;
        _inst = null; 
    }

    // ============================= 公共接口 =================================
    public long WorldBossDamageSum
    {
        set { m_WorldBossDamageSum = value; }
        get { return m_WorldBossDamageSum; }
    }
	public ObjectCreature GetObjectHeroByGUID(X_GUID id)
	{
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
		{
            if (m_pObjectMonsterMgr[i].GetGuid().Equals(id) == true)
			{
                return (ObjectCreature)m_pObjectMonsterMgr[i];
			}			
		}
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            if (m_pObjcetHeroMgr[i].GetGuid().Equals(id) == true)
            {
                return (ObjectCreature)m_pObjcetHeroMgr[i];
            }
        }
		return null;
	}

    public ObjectCreature GetSceneObjectByGUID(X_GUID id)
    {
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            if (m_pObjectMonsterMgr[i].GetGuid().Equals(id) == true)
            {
                return (ObjectCreature)m_pObjectMonsterMgr[i];
            }
        }
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            if (m_pObjcetHeroMgr[i].GetGuid().Equals(id) == true)
            {
                return (ObjectCreature)m_pObjcetHeroMgr[i];
            }
        }

        return null;
    }

	public ObjectCreature GetObjectHeroByIndex(int index)
	{	
		if (index >= 0 && index < m_pObjcetHeroMgr.Count)
		{
			return m_pObjcetHeroMgr[index];
		}

		return null;
	}

    public void SceneObjectAddHero(GameObject obj, int TableID, ObjectCard _Hero)
	{
        ObjectHero pHero = new ObjectHero();
        InitTempHero(TableID, obj,_Hero, ref pHero);
        m_pObjcetHeroMgr.Add(pHero);
	}

    /// <summary>
    /// 添加怪物数据到管理器,根据参数在怪物组中找个怪物ID
    /// </summary>
    /// <param name="obj">实例化的显示对象</param>
    /// <param name="nRound">当前第几波怪</param>
    /// <param name="nNum">第几个怪物</param>
    public void SceneObjectAddMonster(GameObject obj, int nRound, int nNum)
    {
        ObjectMonster pMonster = new ObjectMonster();
        if (nRound > 0 && nRound <= m_MonsterGroupData.Count)
        {
            int nTableID = m_MonsterGroupData.IDs[nRound - 1, nNum];
            if (nTableID <= 0)
                return;
            m_MonsterGroupData.SetGUID(pMonster.GetGuid(), nRound - 1, nNum);

            InitTempMonsterData(nTableID, obj, ref pMonster);
        }
        m_pObjectMonsterMgr.Add(pMonster);
    }

    public CampaignMonsterGroupData GetCapaignMonsterGroupData() { return m_MonsterGroupData; }

    /// <summary>
    /// 返回场景内英雄数量
    /// </summary>
    /// <returns></returns>
	public int GetObjectHeroCount(){ return m_pObjcetHeroMgr.Count; }
    /// <summary>
    /// 返回当前场景死亡英雄数量
    /// </summary>
    /// <returns></returns>
    public int DieHeroCount { set { m_DieHeroCount = value; } get { return m_DieHeroCount; } }
    /// <summary>
    /// 返回当前场景内怪物数量
    /// </summary>
    /// <returns></returns>
	public int GetObjectMonsterCount(){ return m_pObjectMonsterMgr.Count; }

    /// <summary>
    /// 根据序列索引，返回英雄对象
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ObjectHero GetHeroObject(int index)
    {
        if (index >= 0 && index < m_pObjcetHeroMgr.Count)
        {
            return m_pObjcetHeroMgr[index];
        }
        return null;
    }
    /// <summary>
    /// 根据序列索引，返回怪物对象
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public ObjectMonster GetMonsterObject(int index)
    {
        if (index >= 0 && index < m_pObjectMonsterMgr.Count)
        {
            return m_pObjectMonsterMgr[index];
        }
        return null;
    }

    public List<ObjectHero> GetSceneHeroList() { return m_pObjcetHeroMgr; }
    public ObjectCreature GetSceneObjectByGameObject(GameObject obj)
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            if (m_pObjcetHeroMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
            {
                return m_pObjcetHeroMgr[i];
            }
        }
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            if (m_pObjectMonsterMgr[i].GetGameObject() != null)
            {
                if (m_pObjectMonsterMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    return m_pObjectMonsterMgr[i];
                }
            }
        }
        return null;
    }

    public List<ObjectMonster> GetSceneMonsterList() { return m_pObjectMonsterMgr; }
    public List<FightInfo> GetFightInfoList() { return m_CacheFightInfo; }
    /// <summary>
    /// 获取hero的实例化对象列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetGameObjectListForHero()
    {
        List<GameObject> _list = new List<GameObject>();
        for (int i = 0; i < GetObjectHeroCount(); i++ )
        {
            _list.Add(m_pObjcetHeroMgr[i].GetGameObject());
        }
        return _list;
    }

    /// <summary>
    /// 获取monster的实例化对象列表
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetGameObjectListForMonster()
    {
        List<GameObject> _list = new List<GameObject>();
        for (int i = 0; i < GetObjectMonsterCount(); i++)
        {
            _list.Add(m_pObjectMonsterMgr[i].GetGameObject());
        }
        return _list;
    }

	public void ScanByObject(ObjectCreature pObj, ref SCANOPERATOR_INIT pScanOperator,int tagnum=0,ObjectCreature obj=null)
	{
         if (pObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
         {
             m_Myself.Clear();
             m_Enemy.Clear();
             for(int i=0;i< m_pObjcetHeroMgr.Count;++i)
             {
                 if (m_pObjcetHeroMgr[i] != null && m_pObjcetHeroMgr[i].IsAlive())
                     m_Myself.Add(m_pObjcetHeroMgr[i]);
             }
             for (int i = 0; i < m_pObjectMonsterMgr.Count; ++i)
             {
                 if (m_pObjectMonsterMgr[i] != null && m_pObjectMonsterMgr[i].IsAlive())
                     m_Enemy.Add(m_pObjectMonsterMgr[i]);
             }
         }
         else if (pObj.GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
         {
             m_Myself.Clear();
             m_Enemy.Clear();
             for(int i=0;i<m_pObjectMonsterMgr.Count;++i)
             {
                 if (m_pObjectMonsterMgr[i] != null && m_pObjectMonsterMgr[i].IsAlive())
                     m_Myself.Add(m_pObjectMonsterMgr[i]);
             }
             for (int i = 0; i < m_pObjcetHeroMgr.Count; ++i)
             {
                 if (m_pObjcetHeroMgr[i] != null && m_pObjcetHeroMgr[i].IsAlive())
                     m_Enemy.Add(m_pObjcetHeroMgr[i]);
             }
         }
		switch (pScanOperator.m_Type)
		{
		case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND:
			{
                for (int i = 0; i < m_Myself.Count; i++)
				 {
                     if ((m_Myself[i] != null && m_Myself[i].IsAlive()) /*&& (pObj.IsAttacker() == pScanObject.IsAttacker())*/)
					 {
                         pScanOperator.m_ObjectList.Add(m_Myself[i]);
					 }
				 }
			}
			break;
		case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY:
			{
                for (int i = 0; i < m_Enemy.Count; i++)
				 {
                     if ((m_Enemy[i] != null && m_Enemy[i].IsAlive())/* && (pObj.IsAttacker() != pScanObject.IsAttacker())*/)
					 {
                         pScanOperator.m_ObjectList.Add(m_Enemy[i]);
					 }
				 }
			}
			break;
		case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
			{
                if (pObj != null && pObj.IsAlive())
				    pScanOperator.m_ObjectList.Add(pObj);
			}
			break;
        case (int)EM_TARGET_TYPE.EM_TARGET_ALL:
            {
                for (int i = 0; i < m_Myself.Count; i++)
               {
                   if (m_Myself[i] != null && m_Myself[i].IsAlive())
                       pScanOperator.m_ObjectList.Add(m_Myself[i]);
               }
                for (int i = 0; i < m_Enemy.Count; i++)
               {
                   if (m_Enemy[i] != null && m_Enemy[i].IsAlive())
                       pScanOperator.m_ObjectList.Add(m_Enemy[i]);
               }
            }
            break;
        case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_NO_SELF:
            {
                if (pObj != null && pObj.IsAlive())
                {
                    m_Myself.Remove(pObj);
                    for(int i=0;i<m_Myself.Count;++i)
                    {
                        if(m_Myself[i]!=null&&m_Myself[i].IsAlive())
                        {
                            pScanOperator.m_ObjectList.Add(m_Myself[i]);
                        }
                    }
                }
            }
            break;
		case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_MIN_HPPERCENT:
			{
				 ObjectCreature pDestObject = null;
                 for (int i = 0; i < m_Myself.Count; i++)
				 {
                     ObjectCreature pScanObject = m_Myself[i];
					 if ((pScanObject != null)/* && (pObj.IsAttacker() == pScanObject.IsAttacker())*/)
					 {
						 if (pDestObject == null )
						 {
							 pDestObject = pScanObject;
						 }
						 else if (pScanObject.GetHPPercent() < pDestObject.GetHPPercent())
						 {
							 pDestObject = pScanObject;
						 }
					 }
				 }
                 if (pDestObject != null && pDestObject.IsAlive())
				 {
					 pScanOperator.m_ObjectList.Add(pDestObject);
				 }
			}
			break;
		case (int)EM_TARGET_TYPE.EM_TARGET_ALL_NO_SELF:
			{
                for (int i = 0; i < m_Myself.Count; i++)
                {
                    if (m_Myself[i] != null && m_Myself[i].IsAlive())
                        pScanOperator.m_ObjectList.Add(m_Myself[i]);
                }
                for (int i = 0; i < m_Enemy.Count; i++)
                {
                    if (m_Enemy[i] != null && m_Enemy[i].IsAlive())
                        pScanOperator.m_ObjectList.Add(m_Enemy[i]);
                }
                pScanOperator.m_ObjectList.Remove(pObj);
			}
			break;
		case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_MIN_HPPERCENT:
			{
				ObjectCreature pDestObject = null;
                for (int i = 0; i < m_Enemy.Count; i++)
				 {
                     ObjectCreature pScanObject = m_Enemy[i];
					 if ((pScanObject != null) /*&& (pObj.IsAttacker() != pScanObject.IsAttacker())*/)
					 {
						 if (pDestObject == null )
						 {
							 pDestObject = pScanObject;
						 }
						 else if (pScanObject.GetHPPercent() < pDestObject.GetHPPercent())
						 {
							 pDestObject = pScanObject;
						 }
					 }
				 }
				 if (pDestObject != null&&pDestObject.IsAlive())
				 {
					 pScanOperator.m_ObjectList.Add(pDestObject);
				 }
			}
			break;
		case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RAND:
			{
                int _random = UnityEngine.Random.Range(0, m_Enemy.Count);
                if (m_Enemy[_random] != null && m_Enemy[_random].IsAlive())
                    pScanOperator.m_ObjectList.Add(m_Enemy[_random]);
			}
			break;
        case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RAND:
            {
                if (pObj != null && pObj.IsAlive())
                {
                    m_Myself.Remove(pObj);
                    for (int i = 0; i < m_Myself.Count; ++i)
                    {
                        if (m_Myself[i] != null && m_Myself[i].IsAlive())
                        {
                            pScanOperator.m_ObjectList.Add(m_Myself[i]);
                        }
                    }
                    int m_mynum = m_Myself.Count;
                    if (tagnum <= 0)
                        return;
                    if (tagnum >= m_mynum)
                    {
                        for (int i = 0; i < m_mynum; i++)
                        {
                            if (m_Myself[i] != null && m_Myself[i].IsAlive())
                                pScanOperator.m_ObjectList.Add(m_Myself[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < tagnum; ++i)
                        {
                            int _random = UnityEngine.Random.Range(0, m_Myself.Count);
                            if (m_Myself[_random] != null && m_Myself[_random].IsAlive())
                            {
                                pScanOperator.m_ObjectList.Add(m_Myself[_random]);
                                m_Myself.Remove(m_Myself[_random]);
                            }
                        }
                    }
                }
            }
            break;
        case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RANDOM:
            {
                if (obj != null && obj.IsAlive())
                    m_Enemy.Remove(obj);
                int m_enemynum = m_Enemy.Count;
                if(tagnum < 0)
                    return;
                if(tagnum >= m_enemynum)
                {
                    for (int i = 0; i < m_enemynum; i++)
                    {
                        if (m_Enemy[i] != null && m_Enemy[i].IsAlive())
                            pScanOperator.m_ObjectList.Add(m_Enemy[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < tagnum; ++i)
                    {
                        int _random = UnityEngine.Random.Range(0, m_Enemy.Count);
                        if (m_Enemy[_random] != null && m_Enemy[_random].IsAlive())
                        {
                            pScanOperator.m_ObjectList.Add(m_Enemy[_random]);
                            m_Enemy.Remove(m_Enemy[_random]);
                        } 
                    }
                }
            }
            break;
        case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RANDOM:
            {
                if (obj != null && obj.IsAlive())
                    m_Myself.Remove(obj);
                int m_mynum = m_Myself.Count;
                if (tagnum < 0)
                    return;
                if (tagnum >= m_mynum)
                {
                    for (int i = 0; i < m_mynum; i++)
                    {
                        if (m_Myself[i] != null && m_Myself[i].IsAlive())
                            pScanOperator.m_ObjectList.Add(m_Myself[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < tagnum; ++i)
                    {
                        int _random = UnityEngine.Random.Range(0, m_Myself.Count);
                        if (m_Myself[_random] != null && m_Myself[_random].IsAlive())
                        {
                            pScanOperator.m_ObjectList.Add(m_Myself[_random]);
                            m_Myself.Remove(m_Myself[_random]);
                        }
                    }
                }
            }
            break;
        case (int)EM_TARGET_TYPE.EM_TARGET_SELF_RANDOM:
            {
                if (obj != null && obj.IsAlive())
                {
                    m_Myself.Remove(obj);
                    m_Myself.Remove(pObj);
                }
                int m_mynum = m_Myself.Count;
                //Debug.Log(m_mynum);
                if (tagnum < 0)
                    return;
                if (tagnum >= m_mynum)
                {
                    for (int i = 0; i < m_mynum; i++)
                    {
                        if (m_Myself[i] != null && m_Myself[i].IsAlive())
                            pScanOperator.m_ObjectList.Add(m_Myself[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < tagnum; ++i)
                    {
                        int _random = UnityEngine.Random.Range(0, m_Myself.Count);
                        if (m_Myself[_random] != null && m_Myself[_random].IsAlive())
                        {
                            pScanOperator.m_ObjectList.Add(m_Myself[_random]);
                            m_Myself.Remove(m_Myself[_random]);
                        }
                    }
                }
            }
            break;
		default:
			{

			}
			break;
		}
	}


    /// <summary>
    /// 锁定一个距离参数目标最近的monster目标对象 [1/22/2015 Zmy]
    /// </summary>
    /// <param name="sourceObj">参考源</param>
    /// <param name="targetObj">输出目标</param>
    public void LockMonsterTarget(GameObject sourceObj, out ObjectMonster targetObj)
    {
        if (GetObjectMonsterCount() == 0)
        {
            targetObj = null;
            return;
        }
        List<_StrSort>   _Relist = new List<_StrSort>();
        List<GameObject> pList = GetGameObjectListForMonster();
        for (int i = 0; i < pList.Count;++i )
        {
            float _disTemp = Vector3.Distance(sourceObj.transform.position, pList[i].transform.position);
            if ( GetMonsterObject(i).GetHP() > 0 )
            {
                _StrSort _data = new _StrSort();
                _data.nIndex = i;
                _data.nDis = _disTemp;
                _Relist.Add(_data);
            }
        }

        if (_Relist.Count == 0)
        {
            targetObj = null;
            return;
        }

        _Relist.Sort(new CComparer());
        int nIndex = _Relist[0].nIndex;
        targetObj = m_pObjectMonsterMgr[nIndex];
    }
    /// <summary>
    /// 锁定一个距离参数目标最近的Hero目标对象 [1/22/2015 Zmy]
    /// </summary>
    /// <param name="sourceObj">参考源</param>
    /// <param name="targetObj">输出目标</param>
    public void LockHeroTarget(GameObject sourceObj,out ObjectHero targetObj)
    {
        if (GetObjectHeroCount() == 0)
        {
            targetObj = null;
            return;
        }
        List<_StrSort> _Relist = new List<_StrSort>();
        List<GameObject> pList = GetGameObjectListForHero();
        for (int i = 0; i < pList.Count; ++i)
        {
            float _disTemp = Vector3.Distance(sourceObj.transform.position, pList[i].transform.position);
            if (GetHeroObject(i).GetHP() > 0)
            {
                _StrSort _data = new _StrSort();
                _data.nIndex = i;
                _data.nDis = _disTemp;
                _Relist.Add(_data);
            }
        }
        if (_Relist.Count == 0)
        {
            targetObj = null;
            return;
        }
        _Relist.Sort(new CComparer());
        int nIndex = _Relist[0].nIndex;
        targetObj = m_pObjcetHeroMgr[nIndex];
    }

    /// <summary>
    /// 根据索引销毁怪物
    /// </summary>
    /// <param name="index"></param>
    public void OnDestroyMonsterByIndex(int index)
    {
        if (index >= 0 && index < m_pObjectMonsterMgr.Count)
        {
            GameObject obj = m_pObjectMonsterMgr[index].GetGameObject();
            Destroy(obj);
            obj = null;

            m_pObjectMonsterMgr.RemoveAt(index);
        }
    }
    /// <summary>
    /// 根据索引销毁玩家
    /// </summary>
    /// <param name="index"></param>
    public void OnDestroyHeroByIndex(int index)
    {
        if (index >= 0 && index < m_pObjcetHeroMgr.Count)
        {
            GameObject obj = m_pObjcetHeroMgr[index].GetGameObject();
            Destroy(obj);
            obj = null;

            m_pObjcetHeroMgr.RemoveAt(index);
        }
    }

    /// <summary>
    /// 临时接口 用于直接销毁所以怪物
    /// </summary>
    public void ClearUpAllMonster()
    {
        for (int i = 0; i < m_pObjectMonsterMgr.Count; ++i )
        {
            GameObject obj = m_pObjectMonsterMgr[i].GetGameObject();
            Destroy(obj);
            obj = null;
        }

        m_pObjectMonsterMgr.Clear();
    }
    public void OnMonsterAttack()
    {
        for (int i = 0; i < m_pObjectMonsterMgr.Count; ++i)
        {
            m_pObjectMonsterMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        }
    }

    /// <summary>
    /// 立即刷新英雄的目标
    /// </summary>
    /// <param name="id"></param>
    public void OnClearTargetForHero(X_GUID id)
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            m_pObjcetHeroMgr[i].OnClearUpLockTarget(id);
        }

        if (m_FireSignObj != null && m_FireSignObj.GetGuid().Equals(id))
        {
            m_bIsFireSignState = false;
            m_FireSignObj = null;
            Destroy(FireSignGameObj);
            FireSignGameObj = null;
        }
    }
    public void OnClearTargetForEnemy(X_GUID id)
    {
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            m_pObjectMonsterMgr[i].OnClearUpLockTarget(id);
        }
    }
    //刷新buff制作者死亡是否消失buff [3/13/2015 Zmy]
    public void OnUpdateImpactOfMakeDeath(X_GUID deathObj_GUID)
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            if (m_pObjcetHeroMgr[i].GetGuid().Equals(deathObj_GUID))
                continue;

            m_pObjcetHeroMgr[i].OnClearImpactOfMakerDeath(deathObj_GUID);
        }
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            if (m_pObjectMonsterMgr[i].GetGuid().Equals(deathObj_GUID))
                continue;

            m_pObjectMonsterMgr[i].OnClearImpactOfMakerDeath(deathObj_GUID);
        }
    }


    public void OnCacheDeadObj(ObjectCreature obj, EM_OBJECT_TYPE nType)
    {
        if (nType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
        {
            for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
            {
                if (m_pObjcetHeroMgr[i].GetGuid().Equals(obj.GetGuid()))
                {
                    m_pObjcetHeroMgr.RemoveAt(i);
                    break;
                }
            }
            if (m_pObjcetHeroMgr.Count <= 0)
            {
                // 英雄死亡，战斗失败 [4/1/2015 Zmy]
                FightControler.Inst.SetFightState(FightState.FightLose);
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_BattleFail);
            }


            bool frontDie = SceneObjectManager.GetInstance().IsFrontHeroDie();

            if (frontDie && !m_IsMonsterMonveTargetDone)
            {
                m_IsMonsterMonveTargetDone = true;
                SceneObjectManager.GetInstance().ObjectMonsterMoveTarget();
            }
        }
        else if (nType == EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER)
        {
            for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
            {
                if (m_pObjectMonsterMgr[i].GetGuid().Equals(obj.GetGuid()))
                {
                    m_pObjectMonsterMgr.RemoveAt(i);
                    break;
                }
            }
            if (ObjectSelf.GetInstance().isSkillShow)
            {
                Invoke("SkillShowEnd", 3);
                return;
            }
            if (FightEditorContrler.GetInstantiate() != null)
            {
                if (m_pObjectMonsterMgr.Count <= 0 && FightEditorContrler.GetInstantiate().IsFightOver())
                {
                    FightControler.Inst.SetFightState(FightState.FightOver);
                }
            }

            if (IsFrontMonsterDie() && !m_IsHeroMonveTargetDone)
            {
                m_IsHeroMonveTargetDone = true;
                SceneObjectManager.GetInstance().ObjectHeroMoveTarget();
            }
        }

        m_DeadObjList.Add(obj);
    }
    /// <summary>
    /// 技能展示完成 【Lyq】
    /// </summary>
    private void SkillShowEnd()
    {
        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        ObjectSelf.GetInstance().isSkillShow = false;
    }

    public void OnDeleteDeadObj(X_GUID guid)
    {
        for (int i = 0; i < m_DeadObjList.Count; i++)
        {
            if (m_DeadObjList[i].GetGuid().Equals(guid))
            {
                GameObject obj = m_DeadObjList[i].GetGameObject();
                Destroy(obj);
                obj = null;
                m_DeadObjList.RemoveAt(i);
                break;
            }
        }
    }
    /// <summary>
    /// 进入战斗后的逻辑模块更新
    /// </summary>
    public void OnUpdateFightingLogic()
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; ++i)
        {
            m_pObjcetHeroMgr[i].OnAttackStateLogicUpdate();
        }

        for (int i = 0; i < m_pObjectMonsterMgr.Count; ++i)
        {
            m_pObjectMonsterMgr[i].OnAttackStateLogicUpdate();
        }
    }
    //进入战斗后AI逻辑更新
    private void OnUpdateFightingAILogic()
    {
        AILogicHero.GetInstance().AIUpdate(m_pObjcetHeroMgr);
        AILogicMonster.GetInstance().AIUpdate(m_pObjectMonsterMgr);
    
    }
    /////////////////////////////////////////////////////////////////////////////////////
//     private X_GUID CreateGuid()
//     {
//         m_MaskGUID.GUID_value++;
//         m_MaskGUID.Copy(m_MaskGUID);
//         return m_MaskGUID;
//     }
    /// <summary>
    /// 本地临时模拟的一份英雄数据，后面更换成从指定位置获取
    /// </summary>
    /// <param name="obj"></param>
    private void InitTempHero(int nTableID, GameObject obj, ObjectCard _CardHero, ref ObjectHero pHero)
    {
        pHero.SetHeroObject(obj);
        //HeroData pData = new HeroData();
        pHero.GetHeroData().Copy(_CardHero.GetHeroData());
        pHero.UpdateItemEffectValue();
        pHero.UpdateTeamEffectValue();
        pHero.UpdateTrainEffectValue();
        
        pHero.InitEventData();
        pHero.SetSpellNormalData();
        pHero.InitBaseData();

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            int _pos = pHero.GetTeamPos();
            int _curHp = ObjectSelf.GetInstance().LimitFightMgr.m_HeroHp[_pos];
            if (_curHp != -1)
            {
                pHero.SetHP(_curHp);
            }
        }
    }

    public void UpdateMonsterGroupData()
    {
        m_MonsterGroupData.Copy(ObjectSelf.GetInstance().GetMonsterGroupData());
    }
    public int GetMosnterBundleRes(int nRound,int nNum)
    {
        int nTableID = 0;
        if (nRound > 0 && nRound <= m_MonsterGroupData.Count)
        {
            nTableID = m_MonsterGroupData.IDs[nRound - 1, nNum];
            MonsterTemplate pRow = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(nTableID);
            ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(pRow.getArtresources());
        }

        return nTableID;
    }

#region 新版战斗增加的接口（HRF）

    public void ObjectMonsterAllAttack()
    {
        int round = StoryAnimEditorContrler.GetInst().GetCurrentFightCount();
        for (int i = 0; i < 5; i++)
        {
            bool die = IsMonsterDie(round, i);

            if (die)
            {
                continue;
            }

            ObjectMonster monster = GetMonsterByGUID(m_MonsterGroupData.GUIDs[round - 1, i]);

            if (monster != null && monster.GetActionState() == ObjectCreature.ObjectActionState.none)
            {
                //monster.GetAnimation().Anim_Run();
                monster.SetObjectActionState(ObjectCreature.ObjectActionState.idle);
            }
        }
    }

    public void ObjectHeroMoveTarget()
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i ++ )
        {
            if (IsHeroDie(m_pObjcetHeroMgr[i].GetGuid()))
            {
                continue;
            }
            m_pObjcetHeroMgr[i].GetAnimation().Anim_Run();

            m_pObjcetHeroMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.moveTarget);
        }
    }

    public void ObjectMonsterMoveTarget()
    {
        int round = StoryAnimEditorContrler.GetInst().GetCurrentFightCount();
        for (int i = 0; i < 5; i++)
        {
            bool die = IsMonsterDie(round, i);

            if (die)
            {
                continue;
            }
            
            ObjectMonster monster = GetMonsterByGUID(m_MonsterGroupData.GUIDs[round - 1, i]);
            
            if (monster != null)
            {
                //monster.GetAnimation().Anim_Run();
                monster.SetObjectActionState(ObjectCreature.ObjectActionState.moveTarget);
            }
        }
    }

    public void LockMonsterTarget(ObjectHero sourceObj, out ObjectMonster targetObj)
    {
        if (GetObjectMonsterCount() == 0)
        {
            targetObj = null;
            return;
        }

        int idx = GetHeroIdxByGUID(sourceObj.GetGuid());

        if (idx == -1)
        {
            targetObj = null;
            return;
        }

        //按攻击顺序寻找攻击的对象;
        for (int i = 0; i < 5; i++)
        {
            int id = GlobalMembers.AttackSort[idx, i];
            int round = StoryAnimEditorContrler.GetInst().GetCurrentFightCount();
            bool die = IsMonsterDie(round, id - 1);

            if (!die)
            {
                targetObj = GetMonsterByGUID(m_MonsterGroupData.GUIDs[round - 1, id - 1]);
                return;
            }
        }

        targetObj = null;
        return;
    }

    public void LockHeroTarget(ObjectMonster sourceObj, out ObjectHero targetObj)
    {
        if (GetObjectHeroCount() == 0)
        {
            targetObj = null;
            return;
        }
        
        int idx = GetMonsterIdxByGUID(sourceObj.GetGuid());

        if (idx == -1)
        {
            targetObj = null;
            return;
        }

        //按攻击顺序寻找攻击的对象;
        for (int i = 0; i < 5; i++)
        {
            int id = GlobalMembers.AttackSort[idx, i];
            int group = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            bool die = IsHeroDie(group, id - 1);

            if (!die)
            {
                X_GUID guid = ObjectSelf.GetInstance().Teams.GetHeroGUID(group, id - 1);

                targetObj = GetHeroByGUID(guid);
                return;
            }
        }

        targetObj = null;
        return;
    }

    ObjectHero GetHeroByGUID(X_GUID guid)
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++ )
        {
            if (m_pObjcetHeroMgr[i].GetGuid().Equals(guid))
            {
                return m_pObjcetHeroMgr[i];
            }
        }

        return null;
    }

    ObjectMonster GetMonsterByGUID(X_GUID guid)
    {
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++ )
        {
            if (m_pObjectMonsterMgr[i].GUID.Equals(guid))
            {
                return m_pObjectMonsterMgr[i];
            }
        }

        return null;
    }

    /// <summary>
    /// [0,...]
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    int GetHeroIdxByGUID(X_GUID guid)
    {
        if (guid == null || !guid.IsValid())
        {
            return -1;
        }

        int groupId = ObjectSelf.GetInstance().Teams.GetDefaultGroup();

        int idx = ObjectSelf.GetInstance().Teams.FindTeamMemberIndex(groupId, guid);

        if (idx == int.MaxValue)
        {
            return -1;
        }

        return idx;
    }

    /// <summary>
    /// [0,...]
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public int GetMonsterIdxByGUID(X_GUID guid)
    {
        if (guid == null || !guid.IsValid())
        {
            return -1;
        }

        for (int m = 0; m < GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP; m++ )
        {
            for (int i = 0; i < GlobalMembers.MAX_MONSTER_GROUP_COUNT; i++ )
            {
                X_GUID g = m_MonsterGroupData.GUIDs[m, i];

                if (g.Equals(guid))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    public bool IsFrontHeroDie()
    {
        int groupId = ObjectSelf.GetInstance().Teams.GetDefaultGroup();

        bool isDie1 = IsHeroDie(groupId, 0);
        bool isDie2 = IsHeroDie(groupId, 1);
        
        return isDie1 && isDie2;
    }

    public bool IsFrontMonsterDie()
    {
        bool firstDie = SceneObjectManager.GetInstance().IsMonsterDie(StoryAnimEditorContrler.GetInst().GetCurrentFightCount(), 0);
        bool secDie = SceneObjectManager.GetInstance().IsMonsterDie(StoryAnimEditorContrler.GetInst().GetCurrentFightCount(), 1);

        return firstDie && secDie;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="idx">英雄索引[0,4]</param>
    /// <returns></returns>
    public bool IsHeroDie(int groupId, int idx)
    {
        X_GUID guid = ObjectSelf.GetInstance().Teams.GetHeroGUID(groupId, idx);

        return IsHeroDie(guid);
    }

    public bool IsHeroDie(X_GUID guid)
    {
        if (guid == null || !guid.IsValid())
        {
            return true;
        }

        bool isHave = false;
        bool isDead = false;

        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            if (m_pObjcetHeroMgr[i].GetGuid().Equals(guid) && m_pObjcetHeroMgr[i].GetHP() > 0)
                isHave = true;
        }

        for (int i = 0; i < m_DeadObjList.Count; i++)
        {
            if (m_DeadObjList[i].GetGuid().Equals(guid))
            {
                isDead = true;
            }
        }

        return isDead || !isHave;
    }

    public bool IsMonsterDie(int nRound, int nNum)
    {
        if (nRound > 0 && nRound <= m_MonsterGroupData.Count)
        {
            X_GUID guid = m_MonsterGroupData.GUIDs[nRound - 1, nNum];

            return IsMonsterDie(guid);
        }

        return false;
    }

    public bool IsMonsterDie(X_GUID guid)
    {
        if (guid == null || !guid.IsValid())
        {
            return true;
        }

        bool isHave = false;
        bool isDead = false;
        
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            if (m_pObjectMonsterMgr[i].GUID.Equals(guid) && m_pObjectMonsterMgr[i].GetHP() > 0)
                isHave = true;
        }

        for (int i = 0; i < m_DeadObjList.Count; i++)
        {
            if (m_DeadObjList[i].GetGuid().Equals(guid))
            {
                isDead = true;
            }
        }

        return isDead || !isHave;
    }
#endregion

    //本地设置怪物数据
    private void InitTempMonsterData(int nTableID, GameObject obj, ref ObjectMonster pMonster)
    {
        //pMonster.GUID.Copy(CreateGuid());
        pMonster.SetMonsterTableID(nTableID);
        pMonster.SetMonsterObject(obj);
        pMonster.SetSpellNormalData();
        pMonster.InitBaseData();
    }
    //索引当前list中的所有怪物，释放一次身上携带的被动技能 [7/31/2015 Zmy]
    public void OnMonsterFreePassiveSpell()
    {
         for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
         {
             if (m_pObjectMonsterMgr[i] != null && m_pObjectMonsterMgr[i].IsAlive())
             {
                 List<SpellInfo> _spellInfoArray = m_pObjectMonsterMgr[i].Getm_SpellInfo();
                 for (int n = 0; n < _spellInfoArray.Count; n++)
                 {
                     //只释放被动类型 或者是 光环类型的技能 [7/31/2015 Zmy]
                     if (_spellInfoArray[n] != null && _spellInfoArray[n].GetSpellID() != -1 && (_spellInfoArray[n].GetSpellType() == 2 || _spellInfoArray[n].GetSpellType() == 3))
                     {
                         Spell pSkill = new Spell();
                         pSkill.SetHolder(m_pObjectMonsterMgr[i]);
                         pSkill.Init(_spellInfoArray[n]);
                         pSkill.ImmActiveOnce();
                         pSkill = null;
                     }
                 }
             }
         }
    }
    public void OnChangeActionState(GameObject obj,int nOwnerType, ObjectCreature.ObjectActionState state)
    {
        // 优先处理死亡状态的改变 [4/3/2015 Zmy]
        if (state == ObjectCreature.ObjectActionState.destory)
        {
            for (int i = 0; i < m_DeadObjList.Count; i++)
            {
                if (m_DeadObjList[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    if (nOwnerType == 1)
                    {
                        ((ObjectHero)m_DeadObjList[i]).OnDestorySelf();
                    }
                    else
                    {
                        ((ObjectMonster)m_DeadObjList[i]).OnDestorySelf();
                    }
                    return;
                }
            }
            
        }

        if ( nOwnerType == 1)
        {
            for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
            {
                if (m_pObjcetHeroMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    if (state == ObjectCreature.ObjectActionState.normalAttack && m_pObjcetHeroMgr[i].IsAlive() == false)
                        return;
                    //fuck 滑步原来是这里导致的,在此状态下。由动作回调导致的状态切换一定要终止 [2/5/2015 Zmy]
                    if (m_pObjcetHeroMgr[i].GetActionState() == ObjectCreature.ObjectActionState.moveTarget ||
                        m_pObjcetHeroMgr[i].GetActionState() == ObjectCreature.ObjectActionState.scanning)
                        return;
                    m_pObjcetHeroMgr[i].SetObjectActionState(state);
                }
            }
        }
        else
        {
            for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
            {
                if (m_pObjectMonsterMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    if ((state == ObjectCreature.ObjectActionState.normalAttack || state == ObjectCreature.ObjectActionState.AttackIdle) && 
                         m_pObjectMonsterMgr[i].IsAlive() == false)
                        return;
                    //fuck 滑步原来是这里导致的,在此状态下。由动作回调导致的状态切换一定要终止  [2/5/2015 Zmy]
                    if (m_pObjectMonsterMgr[i].GetActionState() == ObjectCreature.ObjectActionState.moveTarget ||
                        m_pObjectMonsterMgr[i].GetActionState() == ObjectCreature.ObjectActionState.scanning)
                        return;
                    m_pObjectMonsterMgr[i].SetObjectActionState(state);
                }
            }
        }
    }
    public void NormalAttack_CallBack(GameObject obj, int nOwnerType)
    {
        if (nOwnerType == 1)
        {
            for (int i = 0; i < m_pObjcetHeroMgr.Count; i++ )
            {
                if (m_pObjcetHeroMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    m_pObjcetHeroMgr[i].OnNormalAttack();
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
            {
                if (m_pObjectMonsterMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    m_pObjectMonsterMgr[i].OnNormalAttack();
                    return;
                }
            }
        }
    }
    //PVE战斗英雄释放技能入口 [3/6/2015 Zmy]
    public bool OnFree_PveHeroSkill(ref EventRequestSkillPackage Temp)
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            //此处ID验证要改成GUID fix！ [3/27/2015 Zmy]
            if (m_pObjcetHeroMgr[i].GetHeroData().GUID.Equals(Temp.mOwner))
            {
                if (m_bIsFireSignState && m_FireSignObj != null)
                {
                    m_pObjcetHeroMgr[i].SetSkillLockTarget(m_FireSignObj);
                }
                else
                {
                    m_pObjcetHeroMgr[i].SetSkillLockTarget(Temp.mTarget);
                }
                bool bRet = m_pObjcetHeroMgr[i].OnPre_CheckUseSkillCondtion();
                if (bRet)
                {
//                     m_pObjcetHeroMgr[i].OnSkillConsume();
//                     m_pObjcetHeroMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.skillAttack);
//                     SkillTemplate info = m_pObjcetHeroMgr[i].Getm_SpellInfo()[0].GetSpellRow();
//                     SkillShowNamePackage package = new SkillShowNamePackage(Temp.mOwner, info.getSkillNameRes());
//                     GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_ShowSkillName, package);
// 
//                     if (info.getSkillhittype() == 1)// 治疗技能不加成怒气 [10/17/2015 Zmy]
//                         return true;
//                     // 英雄攻击怒气加成。治疗技能不加怒气 [10/17/2015 Zmy]
//                     AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(((ObjectHero)m_pObjcetHeroMgr[i]).GetHeroRow().getFuryId());
//                     int nValue = _data.getAttackFury();
//                     for (int n = 0; n < ((ObjectHero)m_pObjcetHeroMgr[i]).GetHeroData().HeroCabalaDB.CabalaList.Count; ++n)
//                     {
//                         int _tableID = ((ObjectHero)m_pObjcetHeroMgr[i]).GetHeroData().HeroCabalaDB.CabalaList[n].TableID;
//                         MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
//                         if (_row.getMstype() == 3)//攻击额外怒气增加
//                         {
//                             int nLev = ((ObjectHero)m_pObjcetHeroMgr[i]).GetHeroData().HeroCabalaDB.CabalaList[n].IntensifyLev;
//                             if (nLev <= 0)
//                                 continue;
//                             nValue += _row.getValue()[nLev - 1];
//                         }
//                     }
//                     FightControler.Inst.OnUpdatePowerValue(m_pObjcetHeroMgr[i].GetGroupType(), nValue);

                    m_pObjcetHeroMgr[i].LaunchFreeSpellLogic(EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_INITIATIVE);
                    return true;
                }
            }
        }
        return false;
    }

    // 技能命中回调 [3/4/2015 Zmy]
    // nFunctionType = 1 :飞行技能命中帧回调
    // nFunctionType = 2 :瞬发技能命中帧回调
    // nFunctionType = 3 :技能动作结束帧回调
    public void SkillAttack_CallBack(GameObject obj, int nOwnerType,int nFunctionType,int nHitCount = 1)
    {
        if (nOwnerType == 1)
        {
            for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
            {
                if (m_pObjcetHeroMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    switch (nFunctionType)
                    {
                        case 1: //飞行技能命中帧回调
                            m_pObjcetHeroMgr[i].OnSkillFly_Effect(nHitCount);
                            break;
                        case 2: //瞬发技能命中帧回调
                            m_pObjcetHeroMgr[i].OnSkillMoment_Effect();
                            break;
                        case 3://引导技能命中真回调
                            m_pObjcetHeroMgr[i].OnSkillGuidance_Effect();
                            break;
                        case 4: //技能动作结束帧回调
                            m_pObjcetHeroMgr[i].OnClearSpellState();
                            m_pObjcetHeroMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.AttackIdle);
                            break;
                        default:
                            break;
                    }
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
            {
                if (m_pObjectMonsterMgr[i].GetGameObject().GetHashCode() == obj.GetHashCode())
                {
                    switch (nFunctionType)
                    {
                        case 1: //飞行技能命中帧回调
                            m_pObjectMonsterMgr[i].OnSkillFly_Effect(nHitCount);
                            break;
                        case 2: //瞬发技能命中帧回调
                            m_pObjectMonsterMgr[i].OnSkillMoment_Effect();
                            break;
                        case 3://引导技能命中真回调
                            m_pObjectMonsterMgr[i].OnSkillGuidance_Effect();
                            break;
                        case 4: //技能动作结束帧回调
                            m_pObjectMonsterMgr[i].OnClearSpellState();
                            m_pObjectMonsterMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
                            break;
                        default:
                            break;
                    }
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 回合战斗结束刷新
    /// </summary>
    public void UpdateRoundOver()
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            m_pObjcetHeroMgr[i].OnClearUpdateImpact();
            m_pObjcetHeroMgr[i].ClearPassiveSpellLogic();
        }

        if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
        {
            ObjectSelf.GetInstance().WorldBossMgr.SendRoundOver();
        }

        m_CacheFightInfo.Clear();

        m_IsHeroMonveTargetDone = false;
        m_IsMonsterMonveTargetDone = false;
    }
    /// <summary>
    /// 缓存由技能产生的战斗信息，等待每回合结束后发送给服务器验证
    /// </summary>
    /// <param name="_info"></param>
    public void _CacheFightInfo(FightInfo _info)
    {
        m_CacheFightInfo.Add(_info);
    }
    /// <summary>
    /// 更新当前集火目标
    /// </summary>
    /// <param name="pTarget"></param>
    public void UpdateFireSignCreature(ObjectCreature pTarget)
    {
        if (m_FireSignObj != null && m_FireSignObj.GetGuid().Equals(pTarget.GetGuid()))
        {
            //已有集火目标 并且是同一个集火目标 那个取消当前的集火目标 [3/27/2015 Zmy]
            m_bIsFireSignState = false;
            m_FireSignObj = null;
        }
        else
        {
            m_bIsFireSignState = true;
            m_FireSignObj = pTarget;
        }
        
        
        if (m_bIsFireSignState && pTarget.IsAlive())
        {
            Destroy(FireSignGameObj);
            FireSignGameObj = null;


            FireSignGameObj = Instantiate(Resources.Load("UI/Prefabs/NewFireSign"), ((ObjectMonster)pTarget).GetAnimation().EventControl.Pre_Bottom_EffectPoint.transform.position,
                ((ObjectMonster)pTarget).GetAnimation().EventControl.Pre_Bottom_EffectPoint.transform.rotation) as GameObject;
            if (FireSignGameObj)
            {
                FireSignGameObj.SetActive(false);
                FireSignGameObj.transform.parent = pTarget.GetGameObject().transform;
                ArtresourceTemplate pRow = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(((ObjectMonster)pTarget).GetMonsterRow().getArtresources()); 

                ParticleSystem pSys = FireSignGameObj.GetComponent<ParticleSystem>();
                pSys.startSize = pRow.getFireSignSize();
                FireSignGameObj.SetActive(true);
            }
        }
        else
        {
            Destroy(FireSignGameObj);
            FireSignGameObj = null;
        }
        
    }

    public void OnHealAllHero()
    {
        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter == false)
            return;
        
        for (int i = 0; i < m_pObjcetHeroMgr.Count;i++)
        {
            if (m_pObjcetHeroMgr[i].IsAlive())
            {
                float nCurHP = m_pObjcetHeroMgr[i].GetHP();
                float nMaxHP = m_pObjcetHeroMgr[i].GetMaxHP();
                float _HealValue = nMaxHP * DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_honestdiploma_num1();

                UI_HurtInfo pData = new UI_HurtInfo();
                pData.pTarget = m_pObjcetHeroMgr[i];
                pData.bCritical = false;
                
                if (nCurHP + _HealValue >= nMaxHP)
                {
                    m_pObjcetHeroMgr[i].SetHP((int)nMaxHP);
                    pData.nHurt = (int)(nMaxHP - nCurHP);
                }
                else
                {
                    m_pObjcetHeroMgr[i].SetHP((int)(nCurHP + _HealValue));
                    pData.nHurt = (int)(_HealValue);
                }
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_ChangeHP, pData);

                string _effectName = DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_honestdiploma_effect1();
                if (string.IsNullOrEmpty(_effectName) == false)
                {
                    Transform paran = m_pObjcetHeroMgr[i].GetGameObject().GetComponent<AnimationEventControler>().GetTransform(m_pObjcetHeroMgr[i].GetGameObject().transform,
                                                                                                                               "Chest_EffectPoint");
                    EffectManager.GetInstance().InstanceEffect_Static(_effectName, m_pObjcetHeroMgr[i], paran);
                }
            }
        }
    }
    public bool GetIsFireSignState() { return m_bIsFireSignState; }
    public ObjectCreature GetFireSighCreatrue() { return m_FireSignObj; }

    // 战斗胜利 根据存活英雄数量 刷新战斗关卡数据 [4/8/2015 Zmy]
    public void UpdateBattleWinStageData()
    {
        int nCurStageID = 0;
        if (ObjectSelf.GetInstance().GetIsPrompt())
        {
            nCurStageID = ObjectSelf.GetInstance().GetPromptCurCampaignID();
        }
        else
	    {
            nCurStageID = ObjectSelf.GetInstance().GetCurCampaignID();
	    }
      
        int nChapterID = ObjectSelf.GetInstance().GetCurChapterID();
        int nStarNum = 0;
        if (DieHeroCount == 0)
        {
            nStarNum = 3;
        }
        else if (DieHeroCount == 1)
        {
            nStarNum = 2;
        }
        else
        {
            nStarNum = 1;
        }

        //ObjectSelf.GetInstance().BattleStageData.UpdateBattleStage(nChapterID, nCurStageID, nStarNum);

        if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
        {
            ObjectSelf.GetInstance().WorldBossMgr.SendRoundOver();
        }
        else
        {
            CEndBattle battle = new CEndBattle();
            battle.pass = nStarNum;
            IOControler.GetInstance().SendProtocol(battle);
        }
       
    }

    /// <summary>
    ///设置英雄和怪物初始状态为搜寻目标状态
    ///技能开始前和播放完成后使用
    ///只用于技能展示使用 【Lyq】
    /// </summary>
    public void InitSkillShowState()
    {
        for (int i = 0; i < m_pObjcetHeroMgr.Count; i++)
        {
            m_pObjcetHeroMgr[i].SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        }
        for (int j = 0; j < m_pObjectMonsterMgr.Count; j++)
        {
            m_pObjectMonsterMgr[j].SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        }   
    }

    public bool IsOnlyOneMonsterLeft(out ObjectCreature target)
    {
        int _count = 0;
        target = null;
        for (int i = 0; i < m_pObjectMonsterMgr.Count; i++)
        {
            if (m_pObjectMonsterMgr[i].IsAlive())
            {
                _count++;
            }
            if (_count == 1)
                target = m_pObjectMonsterMgr[i];
        }
        if (_count == 1)
        {
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

}
