using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using DG.Tweening;
using DreamFaction.GameAudio;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;

namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 动画控制器,用于执行怪物出场动画，场景动画编辑逻辑
    /// </summary>
    public class StoryAnimEditorContrler : BaseControler
    {
        private static StoryAnimEditorContrler Inst;                        //单例
        private Dictionary<int, StoryAnimGroup> Dic_StoryAnim;              //动画组字典，初始化赋值一次
        private Dictionary<int, StoryAnimdate> Dic_StoryAnimDate;           //当前动画动画事件字典
        private List<GameObject> Dic_GameobjList;                           //实例化，删除GameObj数组
        private List<GameObject> MonsterObjList;                            //出生怪物List
        private List<StoryAnimdate> StoryAnimDateList;                      //当前动画事件数组
        private List<Monsterdata> CurrentMomentMonsterdataList;             //当前立刻出现怪物编辑器数据
        private List<Monsterdata> CurrentAllMonsterdataList;                //当前所有出场怪物编辑器数据
        private GameObject StoryCamAnimsObj;                                //摄像机动画组件Obj
        private Animation StoryCamAnim;                                     //摄像机动画组件
        private int StoryAnimID;                                            //动画索引
        private int CurrentMonsterDieCount;                                 //记录当前波数怪物死亡个数
        private int CurrentFightCount;                                      //当前战斗波数
        private float CamToTagTime;                                         //当前动画摄像机移动到起始位置的时间
        private bool IsLocal = false;                                       //是否本地加载
        //private MonsterGroupDataObj Monstersdata;                           //所有场景怪物编辑器数据
        private MonsterGroupDataObjMgr Monstersdata;                           //所有场景怪物编辑器数据
        private int CurrentMonsterGroupCount;                               //当前场景怪物总波数

        private int mMonsterTroopType = 1;                                  //默认怪物阵型;
        private int mBeginFightCount = 1;                                   //默认战斗回合数;
        private EM_SCENE_TYPE mSceneType = EM_SCENE_TYPE.NORMAL;            //怪物组基数;
        protected override void InitData()
        {
            Inst = this;
            //监听场景动画事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryEnter, OnStoryStart);
            //监听开场动画事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryCameraEnter, OnStoryStart);
            //监听创建怪物事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareEnemy, OnPrepareEnemy);
            //监听战斗开始事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StopMonsterBirth, OnStopMonsterBirth);
            //监听战斗结束事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_BattleRoundOver, OnRoundOver);
            //监听怪物死亡事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.F_EnemyOnDie, OnEnemyOnDie);
            //初始化怪物编辑器数组
            MonsterObjList = new List<GameObject>();
            Dic_GameobjList = new List<GameObject>();
            CurrentMomentMonsterdataList = new List<Monsterdata>();
            CurrentAllMonsterdataList = new List<Monsterdata>();
            CurrentMonsterDieCount = 0;
        }
        //########################################################监听回调事件################################################
        private void OnStopMonsterBirth()
        {
           // LogManager.Log("战斗进入停止所有怪物出生行为");
            for (int i = 0; i < MonsterObjList.Count; ++i)
            {
                MonsterObjList[i].transform.DOPause();
                MonsterObjList[i].GetComponent<NavMeshAgent>().enabled = true;
            }
            foreach (GameObject temp in Dic_GameobjList)
            {
                Destroy(temp);
            }
            Dic_GameobjList.Clear();
            StopAllCoroutines();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnd);
        }
        private void OnEnemyOnDie(GameEvent ge)
        {
            CurrentMonsterDieCount++;
            for (int i = 0; i < CurrentAllMonsterdataList.Count; ++i)
            {
                if (CurrentAllMonsterdataList[i].MonsterPointdataList.Count <= 0)
                {
                    continue;
                }
                if (CurrentAllMonsterdataList[i].MonsterPointdataList[0].Entertype == MonsterEnterType.Bench && CurrentAllMonsterdataList[i].MonsterPointdataList[0].BenchCount == CurrentMonsterDieCount)
                {
                    int fightCount = ObjectSelf.GetInstance().IsLimitFight ? ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum : CurrentFightCount;
                    int id = SceneObjectManager.GetInstance().GetMosnterBundleRes(fightCount, i);
                    MonsterTemplate pRow = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(id);
                    ArtresourceTemplate art = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(pRow.getArtresources());
                    string _res = art.getArtresources();
                    GameObject _Monster = AssetLoader.Inst.GetAssetRes(_res);
                    GameObject obj = Instantiate(_Monster, CurrentAllMonsterdataList[i].MyPos, CurrentAllMonsterdataList[i].MyAngle) as GameObject;
                    float _zoom = art.getArtresources_zoom() * pRow.getMonsterEnlarge();
                    obj.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
                    MonsterObjList.Add(obj);
                    SetMonsterBirthState(obj, CurrentAllMonsterdataList[i], false, i);
                }
            }

            //X_GUID monsterGuid = ge.data as X_GUID;
            //if (monsterGuid != null)
            //{
            //    //判断前排英雄是否消失;
            //    //TODO::这里应该考虑到是否有支援怪物（和策划确认了不要支援怪物了2015/10/30/11.20）;
            //    //List<ObjectMonster> monsters = SceneObjectManager.GetInstance().GetSceneMonsterList();
            //    //if (monsters != null && monsters.Count > 0)
            //    //{
            //    //    //写死的阵型，前2后3;
            //    //    for (int i = 0; i < 2; i++)
            //    //    {

            //    //    }
            //    //}

            //    if (IsFrontRowDie())
            //    {
            //        SceneObjectManager.GetInstance().ObjectHeroMoveTarget();
            //    }
            //}
        }

        bool IsFrontRowDie()
        {
            bool firstDie = SceneObjectManager.GetInstance().IsMonsterDie(CurrentFightCount, 0);
            bool secDie = SceneObjectManager.GetInstance().IsMonsterDie(CurrentFightCount, 1);

            return firstDie && secDie;
        }

        /// <summary>
        /// 设置怪物阵型状态;
        /// </summary>
        /// <param name="type"></param>
        public void SetMonsterTroopType(int type)
        {
            mMonsterTroopType = type;
        }

        /// <summary>
        /// 设置初始当前战斗回合数;
        /// </summary>
        /// <param name="count"></param>
        public void SetBeginFightCount(int count)
        {
            mBeginFightCount = count;
        }

        private void OnPrepareEnemy(GameEvent o)
        {
            CurrentFightCount = (int)o.data;
           // LogManager.Log("第" + o.data + "怪物数据添加");

            for (int k = 0; k < Monstersdata[CurrentFightCount].MonsterGroupdata.Count; ++k)
            {
                CurrentAllMonsterdataList.Add(Monstersdata[CurrentFightCount].MonsterGroupdata[k]);
                if (Monstersdata[CurrentFightCount].MonsterGroupdata[k].MonsterPointdataList.Count <= 0)
                {
                    CurrentMomentMonsterdataList.Add(Monstersdata[CurrentFightCount].MonsterGroupdata[k]);
                    continue;
                }
                if (Monstersdata[CurrentFightCount].MonsterGroupdata[k].MonsterPointdataList[0].Entertype == MonsterEnterType.Bench)
                {
                    //....
                    LogManager.Log("延迟出场");
                }
                else
                {
                    CurrentMomentMonsterdataList.Add(Monstersdata[CurrentFightCount].MonsterGroupdata[k]);
                }
            }
        }
        //战斗结束清除所有数据
        private void OnRoundOver()
        {
            MonsterObjList.Clear();
            CurrentAllMonsterdataList.Clear();
            CurrentMomentMonsterdataList.Clear();
            CurrentMonsterDieCount = 0;
        }
        //剧情开启
        private void OnStoryStart(GameEvent ID)
        {
            if (Dic_StoryAnim.ContainsKey((int)ID.data))
            {
                StoryAnimID = (int)ID.data;
                CamToTagTime = Dic_StoryAnim[StoryAnimID].CamToTagTime;
                StoryAnimDateList = Dic_StoryAnim[StoryAnimID].StoryAnimDateList;
                if (Dic_StoryAnimDate==null)
                {
                    Dic_StoryAnimDate = new Dictionary<int, StoryAnimdate>();
                }
                else
                {
                    Dic_StoryAnimDate.Clear();
                }
                for (int i = 0; i < StoryAnimDateList.Count;++i )
                {
                    Dic_StoryAnimDate.Add(StoryAnimDateList[i].EventID, StoryAnimDateList[i]);
                }
                for (int i = 0; i < StoryAnimDateList.Count; ++i)
                {
                    switch (StoryAnimDateList[i].StoryAnimaHoldtype)
                    {
                        case StoryAnimaHoldType.Parallel:
                            StartCoroutine(StartEvent(StoryAnimDateList[i]));
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("Fuck!!!!没有找到ID索引");
            }
        }
        //##############################################################################################################################
        //怪物出生模式
        private IEnumerator StartMonsterBirth(GameObject obj, Monsterdata data, bool isLocal, int DynamicID = 0)
        {
            yield return new WaitForSeconds(data.EnterWaitTime);

            //TODO::怪物出场特效，这里临时这么写;
            isLocal = true;

            Animation anim = obj.GetComponent<Animation>();
            for (int i = 0; i < data.MonsterPointdataList.Count; ++i)
            {
                //选择动作播放循环播放动作
                switch (data.MonsterPointdataList[i].MonsterActiontype)
                {
                    case MonsterActionType.Loop:
                        anim.wrapMode = WrapMode.Loop;
                        break;
                    case MonsterActionType.Once:
                        anim.wrapMode = WrapMode.Once;
                        break;
                }
                if (data.MonsterPointdataList[i].ActionName != string.Empty)
                    anim.CrossFade(data.MonsterPointdataList[i].ActionName,0.2f);
                //加载特效
                if (data.MonsterPointdataList[i].Effname != string.Empty)
                {
                    //不填的默认为特效位置为当前object的位置;
                    Vector3 effPos = data.MonsterPointdataList[i].Effpos.Equals(Vector3.zero) ? obj.transform.position : data.MonsterPointdataList[i].Effpos;

                    if (!isLocal)
                    {
                        //正常加载特效
                        GameObject objType = AssetLoader.Inst.GetAssetRes(data.MonsterPointdataList[i].Effname);
                        //GameObject eff = Instantiate(objType, data.MonsterPointdataList[i].Effpos, Quaternion.identity) as GameObject;
                        GameObject eff = Instantiate(objType, effPos, Quaternion.identity) as GameObject;
                        Destroy(eff, data.MonsterPointdataList[i].EffTime);
                    }
                    else
                    {
                        //本地加载特效
                        //GameObject eff = Instantiate(Resources.Load(data.MonsterPointdataList[i].Effname), data.MonsterPointdataList[i].Effpos, Quaternion.identity) as GameObject;
                        GameObject eff = Instantiate(Resources.Load(data.MonsterPointdataList[i].Effname), effPos, Quaternion.identity) as GameObject;
                        Destroy(eff, data.MonsterPointdataList[i].EffTime);
                    }
                }
                //播放音效
                if (data.MonsterPointdataList[i].Soundname != null||data.MonsterPointdataList[i].Soundname !=string.Empty)
                {
                    string _audio = data.MonsterPointdataList[i].Soundname;
                    if (!isLocal)
                    {
                        //正常播放音效
                        AudioControler.Inst.PlaySound(_audio);
                    }
                    else
                    {
                        //...
                    }
                }
                //选择行为模式
                switch (data.MonsterPointdataList[i].Entertype)
                {
                    case MonsterEnterType.StayIdle:
                        obj.transform.DOPause();
                        break;
                    case MonsterEnterType.RunAround:
                        if (data.MonsterPointdataList[i].IsMovetoFirstPoint == true)
                        {
                            obj.transform.position = data.MonsterPointdataList[i].RunAroundPoints[0];
                        }
                        RunAroundUpdate(obj, data.MonsterPointdataList[i], data.MonsterPointdataList[i].RunAroundPoints);
                        break;
                    case MonsterEnterType.Bench:
                        SceneObjectManager.GetInstance().SceneObjectAddMonster(obj, CurrentFightCount, DynamicID);
                        // For Blood Create(by zcd)
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_OnSupportMonstorBlood);
                        break;
                    case MonsterEnterType.StayRun:
                        //..
                        break;
                    case MonsterEnterType.DesMySelf:
                        Dic_GameobjList.Remove(obj);
                        Destroy(obj);
                        break;
                    default:
                        Debug.Log("无行为");
                        break;
                }
                //等待当前行为时间
                yield return new WaitForSeconds(data.MonsterPointdataList[i].ActionTime);
            }
        }
        //巡逻逻辑
        private void RunAroundUpdate(GameObject obj, MonsterPointData data, List<Vector3> RunLine, int i = -1, bool isRunde = false)
        {
            if (RunLine.Count <= 1)
            {
                LogManager.Log("路径点不足2无法巡逻" + RunLine.Count);
                return;
            }
            switch (data.MonsterRuntype)
            {
                case MonsterRunType.Loop:
                    if (i == (RunLine.Count - 1))
                    {
                        isRunde = true;
                    }
                    if (i == 0)
                    {
                        isRunde = false;
                    }
                    if (isRunde)
                    {
                        i--;
                    }
                    else
                    {
                        i++;
                    }
                    break;
                case MonsterRunType.Back:
                    if (i == (RunLine.Count - 1))
                    {
                        i = 0;
                    }
                    else
                        i++;
                    break;
                case MonsterRunType.Stop:
                    if (i == (RunLine.Count - 1))
                    {
                        return;
                    }
                    else
                        i++;
                    break;
            }
            switch (data.MonsterLooktype)
            {
                case MonsterLookType.Look:
                    obj.transform.DOLookAt(RunLine[i], 0.5f);
                    break;
                case MonsterLookType.Nolook:
                    break;
            }
            float time = Vector3.Distance(obj.transform.position, RunLine[i]) / data.ActionSpeed;
            obj.transform.DOMove(RunLine[i],time).OnComplete(() => RunAroundUpdate(obj, data, RunLine, i, isRunde));
        }
        //开启事件
        private IEnumerator StartEvent(StoryAnimdate data)
        {
            yield return new WaitForSeconds(data.EventTime);
            switch(data.StoryAnimEventtype)
            {
                case StoryAnimEventType.CreatStaticObj:
                    OnCreatStaticObj(data);
                    break;
                case StoryAnimEventType.GetDynamicObj:
                    OnGetDynamicObj(data);
                    break;
                case StoryAnimEventType.PlayCamAnim:
                    OnPlayCamAnim(data);
                    break;
                case StoryAnimEventType.PlayCamAnimWait:
                    StartCoroutine(OnPlayCamAnimWait(data));
                    break;
                case StoryAnimEventType.SetEnterFightState:
                    FightEditorContrler.GetInstantiate().HeroPathFightEnterEnd();
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_EnterFightState, CurrentFightCount);
                    break;
                case StoryAnimEventType.SetEnterFightInstantiateHero:
                    FightControler.Inst.SetFightState(FightState.FightInstantiateHero);
                    break;
            }
        }
        //开启串联事件
        private void StartCascadeEvent(StoryAnimdate data)
        {
            if (data.CascadeEventID == string.Empty)
                return;
            string[] IDs = data.CascadeEventID.Split('#');
            for (int i = 0; i < IDs.Length; ++i)
            {
                StartCoroutine(StartEvent(Dic_StoryAnimDate[int.Parse(IDs[i])]));
            }
        }
        //创建物体
        private void OnCreatStaticObj(StoryAnimdate data)
        {
            if (data.ObjName == string.Empty)
                return;
            GameObject temp;
            if(IsLocal)
            {
                temp = Instantiate(Resources.Load(data.ObjName), data.ObjPos, Quaternion.Euler(data.ObjAngle)) as GameObject;
                temp.AddComponent<AnimationEventControler>();
 //               temp.transform.position = data.ObjPos;
 //               temp.transform.eulerAngles = data.ObjAngle;
                Dic_GameobjList.Add(temp);
                SetMonsterBirthState(temp, data.MonsterData, true);
                StartCascadeEvent(data);
            }
            else
            {
                GameObject objType = AssetLoader.Inst.GetAssetRes(data.ObjName);
                temp = Instantiate(objType, data.ObjPos, Quaternion.Euler(data.ObjAngle)) as GameObject;
                temp.AddComponent<AnimationEventControler>();
                Dic_GameobjList.Add(temp);
                SetMonsterBirthState(temp, data.MonsterData, false);
                StartCascadeEvent(data);
            }  
        }
        //获取当前战斗怪物信息(比较特殊只限BOSS)
        private void OnGetDynamicObj(StoryAnimdate data)
        {
            GameObject temp;
            if(SceneObjectManager.GetInstance()==null)
            {
                temp = Instantiate(Resources.Load(data.ObjDynamicName), data.ObjDynamicPos, Quaternion.Euler(data.ObjDynamicAngle)) as GameObject;
//                temp.transform.position = data.ObjDynamicPos;
//                temp.transform.eulerAngles = data.ObjDynamicAngle;
            }
            else
            {
                temp = MonsterObjList[data.ObjDynamicID-1];
            }
            SetMonsterBirthState(temp, data.MonsterData, false);
            StartCascadeEvent(data);
        }
    
        //播放摄像机轨迹动画
        private void OnPlayCamAnim(StoryAnimdate data)
        {

            StoryCamAnim.Play(data.CamAnimName);
            //StoryCamAnim[data.CamAnimName].speed = 1f;
            StartCascadeEvent(data);
        }
        //播放摄像机轨迹动画(DEMO)
        private IEnumerator OnPlayCamAnimWait(StoryAnimdate data)
        {
            yield return new WaitForSeconds(CamToTagTime);
            StoryCamAnim.Play(data.CamAnimName);
            //StoryCamAnim[data.CamAnimName].speed = 0.5f;
            StartCascadeEvent(data);
        }
        protected override void DestroyData()
        {
            Inst = null;
            GameEventDispatcher.Inst.clearEvents();
        }
        //###################################################公共接口###################################################
        public static StoryAnimEditorContrler GetInst()
        {
            return Inst;
        }
        /// <summary>
        /// 开启出生怪物行为
        /// </summary>
        /// <param name="obj">怪物Obj</param>
        /// <param name="data">怪物数据</param>
        /// <param name="isLocal">是否本地加载</param>
        public void SetMonsterBirthState(GameObject obj, Monsterdata data, bool isLocal, int DynamicID = 0)
        {
            StartCoroutine(StartMonsterBirth(obj, data, isLocal, DynamicID));
        }
        /// <summary>
        /// 获取当前波数是否有支援的怪物
        /// </summary>
        /// <returns></returns>
        public bool GetisSupport()
        {
            return Monstersdata[CurrentFightCount].isSupport;
        }
        public List<Monsterdata> GetMonsterGroupEditorData(int i)
        {
            if (CurrentMomentMonsterdataList.Count == 0)
            {
                LogManager.Log("怪物数据为空");
                // LogManager.Log("第" + o.data + "怪物数据添加");

                for (int k = 0; k < Monstersdata[i].MonsterGroupdata.Count; ++k)
                {
                    CurrentAllMonsterdataList.Add(Monstersdata[i].MonsterGroupdata[k]);
                    if (Monstersdata[i].MonsterGroupdata[k].MonsterPointdataList.Count <= 0)
                    {
                        CurrentMomentMonsterdataList.Add(Monstersdata[i].MonsterGroupdata[k]);
                        continue;
                    }
                    if (Monstersdata[i].MonsterGroupdata[k].MonsterPointdataList[0].Entertype == MonsterEnterType.Bench)
                    {
                        //....
                        LogManager.Log("延迟出场");
                    }
                    else
                    {
                        CurrentMomentMonsterdataList.Add(Monstersdata[i].MonsterGroupdata[k]);
                    }
                }
            }
            return CurrentMomentMonsterdataList;
        }
        /// <summary>
        /// 初始化关卡动画数据
        /// 是否
        /// </summary>
        /// <param name="Storydata">关卡动画数据组</param>
        public void Init(StoryAnimDataObj Storydata, bool isLocal, MonsterGroupDataObjMgr monsterdata = null, EM_SCENE_TYPE sceneType = EM_SCENE_TYPE.NORMAL, int beginFightCount = 1)
        {
            IsLocal = isLocal;
            Monstersdata = monsterdata;
            mSceneType = sceneType;
            mBeginFightCount = beginFightCount;
            if (Monstersdata!=null)
                CurrentMonsterGroupCount = monsterdata.Count;
            if (Storydata == null)
                return;
            if(isLocal)
            {
                StoryCamAnimsObj = GameObject.Find(Storydata.StoryCamAnimsName);
                StoryCamAnim = StoryCamAnimsObj.GetComponent<Animation>();
            }
            else
            {
                Object objType = AssetLoader.Inst.GetEditorAssetRes(Storydata.StoryCamAnimsName);
                StoryCamAnimsObj = Instantiate(objType) as GameObject;
                StoryCamAnimsObj.transform.parent = this.transform;
                StoryCamAnim = StoryCamAnimsObj.GetComponent<Animation>();
            }
            Dic_StoryAnim = new Dictionary<int, StoryAnimGroup>();
            for(int i=0;i<Storydata.StoryAnimGroupList.Count;++i)
            {
                Dic_StoryAnim.Add(Storydata.StoryAnimGroupList[i].ID, Storydata.StoryAnimGroupList[i]);
            }
        }
        /// <summary>
        /// 当前波数怪物是否有支援怪物
        /// </summary>
        /// <returns></returns>
        public bool IsFightOver()
        {
            return CurrentMonsterDieCount >= Monstersdata[CurrentFightCount].MonsterGroupdata.Count ? true : false;
        }
        public List<GameObject> GetMonsterObjList()
        {
            return MonsterObjList;
        }
        public GameObject GetStoryCamAnimsObj()
        {
            return StoryCamAnimsObj;
        }
        public int GetMonsterGroupCount()
        {
            return CurrentMonsterGroupCount;
        }

        public int GetCurrentFightCount()
        {
            return CurrentFightCount;
        }

        public int GetStoryAnimID()
        {
            return StoryAnimID;
        }
    }
}

