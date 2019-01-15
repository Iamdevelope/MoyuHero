using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameSceneEditorText;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
namespace DreamFaction.GameSceneEditor
{
    public class PosInfo
    {
        private GameObject mCurPosObj;           //默认位置;
        private Vector3 mNextPos;          //如果地方阵型前排死亡后需要移动到的位置;

        public PosInfo(GameObject curPosObj, Vector3 nextPos)
        {
            mCurPosObj = curPosObj;
            mNextPos = nextPos;
        }

        public Vector3 CurPos
        {
            get
            {
                return mCurPosObj.transform.position;
            }
        }

        public Quaternion Rotate
        {
            get
            {
                return mCurPosObj.transform.rotation;
            }
        }

        public Vector3 NextPos
        {
            get
            {
                return mNextPos;
            }
        }

        public string Tag
        {
            get
            {
                return mCurPosObj.tag;
            }
        }
    }

    /// <summary>
    /// 英雄阵型控制器
    /// </summary>
    public class HeroPathtContrler : BaseControler
    {
        private static HeroPathtContrler inst;                           //单例
        private CameraPathAnimator HeroPathAnim;                         //阵型轨迹动作组件
        private CameraPath HeroPath;                                     //阵型轨迹基础组件
        private GameObject TagFormation;                                 //更换用阵型
        private GameObject FormationCenterObj;                           //阵型
        private HeroPathMoveType Heropathtype;                           //阵型状态
        private HeroPathHoldType HeroHoldtype;                           //阵型开关
        private List<GameObject> FormationList;                          //阵型子节点数组
        private Dictionary<ObjectCreature, GameObject> FormationDic;        //阵型字典
        private Vector3 MonstersCenter;                                  //怪物中心点
        private float FormationSpacing;                                  //前后排距离
        private GameObject FightFollowCam;                               //战斗摄像机 
        private GameObject FightDefaultCam;                              //战斗固定摄像机
        private GameObject LineUpFollowCam;                              //整队摄像机
        private HeroPathDataObj HeroPathdata;                            //英雄路径信息
        private HeroFormationType HeroFormationtype;                     //阵型状态
        private GameObject FightCenter;                                  //战斗中心点
        private GameObject HerosCenter;                                  //英雄中心点
        private float MoveSpeed;                                         //英雄阵型移动速度
        private float XHcenter = 0;                                          
        private float ZHcenter = 0;
        private float YHcenter = 0;
        private Vector3 HCenter = new Vector3();
        private float XMcenter = 0;
        private float ZMcenter = 0;
        private float YMcenter = 0;
        private Vector3 MCenter = new Vector3();

        private Vector3 mPrevMonsterPos = new Vector3();

        private bool mInitFightEulerAngleDone = false;
        private Vector3 mInitFightCamEulerAngle = Vector3.zero;

        private int mMomentMoveIdx = -1;

        private Vector3 mMoveTargePosOffset = new Vector3();

        protected override void InitData()
        {
            inst = this;
        }
        protected override void InitView()
        {
            if (FightTempContrler.inst != null)
                init();
        }
        protected override void UpdateData()
        {
            HeroPathHoldUpdate();
        }
        //本地初始化
        private void init()
        {
            HeroPathAnim = this.transform.FindChild("HerosPath").GetComponent<CameraPathAnimator>();
            HeroPath = this.transform.FindChild("HerosPath").GetComponent<CameraPath>();
            //HeroPath.FromXML("Assets/CameraPath3/HerosPath.xml");
            FightCenter = this.transform.FindChild("FightCenter").gameObject;
            HerosCenter = this.transform.FindChild("HerosCenter").gameObject;
            FormationCenterObj = this.transform.FindChild("FormationCenter").gameObject;
            FormationList = new List<GameObject>();
            for (int i = 0; i < FormationCenterObj.transform.childCount; i++)
            {
                FormationList.Add(FormationCenterObj.transform.GetChild(i).gameObject);
            }
            Heropathtype = HeroPathMoveType.Null;
            HeroFormationtype = HeroFormationType.Formation133;
            HeroPathAnim.animationObject = FormationCenterObj.transform;
            HeroPathAnim.pathSpeed = MoveSpeed=this.GetComponent<HeroPathData>().MoveSpeed;
            HeroPathPause();
        }
        //阵型更新
        private void HeroPathHoldUpdate()
        {
            switch (HeroHoldtype)
            {
                case HeroPathHoldType.Play:
                    HeroMoveUpdate();
                    if (FightTempContrler.inst != null)
                    {
                        HeroCenterTempPosUpdate();
                    }
                    else
                    {
                        HerosCenterPosUpdate();
                    }
                    break;
                case HeroPathHoldType.Pause:
                    break;
            }
        }
        private void HeroMoveUpdate()
        {
            switch (Heropathtype)
            {
                case HeroPathMoveType.NormalMove:
                    if (Vector3.Distance(FormationCenterObj.transform.position, HerosCenter.transform.position)<5.0f)
                    {
                        HeroPathAnim.pathSpeed = MoveSpeed;
                    }
                    else
                    {
                        HeroPathAnim.pathSpeed = 1;
                    }
                    break;
                case HeroPathMoveType.FightEnter:
                    break;
                case HeroPathMoveType.MomentMoveEnter:
                    break;
                case HeroPathMoveType.Idle:
                    break;
            }
        }
        private void HerosCenterPosUpdate()
        {
            HerosCenter.transform.position = GetHeroCenterPos(FightControler.Inst.GetHeroGameObjectList());
            HerosCenter.transform.eulerAngles = GetCenterAngle();
            if ((int)FightControler.Inst.GetFightState() >= (int)FightState.prepareEnemy && (int)FightControler.Inst.GetFightState() <= (int)FightState.FightOver)
            {
                MonstersCenter = GetMonsterCenterPos(FightControler.Inst.GetEnemyGameObjectList());

                if (MonstersCenter != Vector3.zero)
                {
                    FightCenter.transform.position = GetFightCenterPos();
                }
            }
            else
            {
                FightCenter.transform.position = HerosCenter.transform.position;
            }
        }
        private float GetFormationSpcing(int near, int far)
        {
            if (near == 0 || far == 0)
            {
                return -1;
            }
            else
            {
                float NearXMcenter = 0;
                float NearZMcenter = 0;
                float NearYMcenter = 0;
                Vector3 NearMCenter = Vector3.zero;
                for (int i = 0; i < near; i++)
                {
                    NearXMcenter += FormationCenterObj.transform.GetChild(i).position.x;
                    NearZMcenter += FormationCenterObj.transform.GetChild(i).position.z;
                    NearYMcenter += FormationCenterObj.transform.GetChild(i).position.y;
                }
                NearMCenter = new Vector3(NearXMcenter / near, NearYMcenter / near, NearZMcenter / near);
                float FarXMcenter = 0;
                float FarZMcenter = 0;
                float FarYMcenter = 0;
                Vector3 FarMCenter = Vector3.zero;
                for (int i = near; i < (far + near); i++)
                {
                    FarXMcenter += FormationCenterObj.transform.GetChild(i).position.x;
                    FarZMcenter += FormationCenterObj.transform.GetChild(i).position.z;
                    FarYMcenter += FormationCenterObj.transform.GetChild(i).position.y;
                }
                FarMCenter = new Vector3(FarXMcenter / far, FarYMcenter / far, FarZMcenter / far);
                return Vector3.Distance(NearMCenter, FarMCenter);
            }
        }
        //本地测试用战斗 英雄中心点更新
        private void HeroCenterTempPosUpdate()
        {
            HerosCenter.transform.position = HeroGroupContrler.inst.HeroCenterPos;
            FightCenter.transform.position = FightTempContrler.inst.FightCenterPos;
            HerosCenter.transform.eulerAngles = GetCenterAngle();
        }
        private Vector3 GetHeroCenterPos(List<GameObject> tag)
        {
            XHcenter = 0;
            ZHcenter = 0;
            YHcenter = 0;
            HCenter = Vector3.zero;
            if (tag.Count != 0 && tag != null)
            {
                for (int i = 0; i < tag.Count; i++)
                {
                    XHcenter += tag[i].transform.position.x;
                    ZHcenter += tag[i].transform.position.z;
                    YHcenter += tag[i].transform.position.y;
                }
                HCenter = new Vector3(XHcenter / tag.Count, YHcenter / tag.Count, ZHcenter / tag.Count);
            }
            return HCenter;
        }
        private Vector3 GetMonsterCenterPos(List<GameObject> tag)
        {
            XMcenter = 0;
            ZMcenter = 0;
            YMcenter = 0;
            //MCenter = Vector3.zero;
            //防止因为怪物全部死亡，导致相机照向的位置错误;
            MCenter = mPrevMonsterPos;
            if (tag.Count != 0 && tag != null)
            {
                for (int i = 0; i < tag.Count; i++)
                {
                    XMcenter += tag[i].transform.position.x;
                    ZMcenter += tag[i].transform.position.z;
                    YMcenter += tag[i].transform.position.y;
                }
                MCenter = new Vector3(XMcenter / tag.Count, YMcenter / tag.Count, ZMcenter / tag.Count);
                mPrevMonsterPos = MCenter;
            }
            return MCenter;
        }
        private Vector3 GetFightCenterPos()
        {
            return (MonstersCenter + HerosCenter.transform.position) / 2;
        }
        private void FightCenterAngleUpdate(int i)
        {
            if (FightTempContrler.inst != null)
            {
                FightCenter.transform.eulerAngles = MonsterGroupContrler.inst.MonsterGroupData.MonsterGroupDataList[i - 1].MonsterGroupAngle + new Vector3(0, 180, 0);
            }
            else
            {
                FightCenter.transform.eulerAngles = FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData().MonsterGroupDataList[i - 1].MonsterGroupAngle + new Vector3(0, 180, 0);
            }
        }
        private void FormationUpdate()
        {
            if (TagFormation != null)
            {
                for (int i = 0; i < TagFormation.transform.childCount; i++)
                {
                    FormationCenterObj.transform.GetChild(i).position = Vector3.Lerp(FormationCenterObj.transform.GetChild(i).position, TagFormation.transform.GetChild(i).position, Time.deltaTime * 2);
                }
            }
        }
        private Vector3 GetCenterAngle()
        {
            return new Vector3(0, FormationCenterObj.transform.eulerAngles.y, 0);
        }
        private void CreatFormation()
        {
            FormationDic.Clear();
            int count = SceneObjectManager.GetInstance().GetSceneHeroList().Count;
            int nearCount = 0;
            int farCount = 0;
            int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            for (int i = 0; i < count; ++i)
            {
                if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].IsAlive())
                {
                    if (type==1)
                    {
                        if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetTeamPos() < 2)
                        {
                            nearCount++;
                        }
                        else
                        {
                            farCount++;
                        }
                    }
                    else
                    {
                        if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetTeamPos() < 3)
                        {
                            nearCount++;
                        }
                        else
                        {
                            farCount++;
                        }
                    }
                    FormationDic.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i], FormationCenterObj.transform.GetChild(i).gameObject);
                    // 增加战斗中位置移动 [9/25/2015 HRF]
                    // 当前位置相对于阵型角度的向前偏移;
                    //Vector3 curPos = FormationCenterObj.transform.GetChild(i).position;
                    //Vector3 direction = GetMonsterInitCenter() - GetHerosCenter();
                    //Vector3 nextPos = curPos + (HeroPathdata.MoveDistance * direction.normalized);
                    //PosInfo pi = new PosInfo(FormationCenterObj.transform.GetChild(i).gameObject, nextPos);
                    //FormationDic.Add(SceneObjectManager.GetInstance().GetSceneHeroList()[i], pi);
                }
            }
            string FormType = "Formation1" + nearCount.ToString() + farCount.ToString();
//            Debug.Log(FormType);
            if (FormType == "Formation100")
                return;
            GameObject objType = AssetLoader.Inst.GetAssetRes(FormType);
            TagFormation = Instantiate(objType) as GameObject;
            //TagFormation.transform.parent = FormationCenterObj.transform;
            TagFormation.transform.position = FormationCenterObj.transform.position;
            TagFormation.transform.localEulerAngles = FormationCenterObj.transform.localEulerAngles;
            //if (FormationCenterObj.transform.childCount!= TagFormation.transform.childCount)
            //{
            //    Debug.Log("FormationCenter" + FormationCenterObj.transform.childCount);
            //    Debug.Log("TagFormation" + TagFormation.transform.childCount);
            //    Debug.Log("目标阵型点和当前阵型点个数不同");
            //    return;
            //}
            for (int i = 0; i < TagFormation.transform.childCount; ++i)
            {
                FormationCenterObj.transform.GetChild(i).position = TagFormation.transform.GetChild(i).position;
            }
            Destroy(TagFormation);
        }
        //阵型开关
        private void SetHeroPathHoldType(HeroPathHoldType type)
        {
            if (HeroHoldtype != type)
                HeroHoldtype = type;
        }
        //阵型瞬间移动
        private void SetFormationMonmetMove()
        {
            HeroPathAnim.pathSpeed = 50;
        }
        //=================================================================外部接口=================================================================
        public static HeroPathtContrler GetInstantiate()
        {
                return inst;
        }
        /// <summary>
        /// 动态初始化
        /// </summary>
        /// <param name="data">英雄阵型序列化数据</param>
        /// <param name="XmlName">Xml地址</param>
        /// <param name="type">英雄阵型</param>
        /// <param name="speed">初始移动速度</param>
        public void Init(HeroPathDataObj data, HeroFormationType type,string PathXml)
        {
            //添加轨迹组件 动画组件 读取轨迹数据
            HeroPathdata = data;
            HeroPath = this.gameObject.AddComponent<CameraPath>();
            HeroPathAnim = this.gameObject.AddComponent<CameraPathAnimator>();
            HeroPath.FromXML(PathXml);
            HeroPath.hermiteTension = data.Tension;
            //初始话战斗中心点和英雄中心点
            FightCenter = new GameObject("FightCenter");
            FightCenter.transform.parent = this.transform;
            FightCenter.transform.position = data.InitPos;
            HerosCenter = new GameObject("HerosCenter");
            HerosCenter.transform.parent = this.transform;
            HerosCenter.transform.position = data.InitPos;
            //初始化战斗摄像机 整队摄像机
            FightFollowCam = new GameObject("FightFollowCam");
            FightFollowCam.transform.parent = FightCenter.transform;
            FightFollowCam.transform.localPosition = data.FightFollowCamPos;
            FightDefaultCam = new GameObject("FightDefaultCam");
            FightDefaultCam.transform.SetParent(FightCenter.transform);
            FightDefaultCam.transform.localPosition = data.FightDefaultCamPos;
            LineUpFollowCam = new GameObject("LineUpFollowCam");
            LineUpFollowCam.transform.parent = HerosCenter.transform;
            LineUpFollowCam.transform.localPosition = data.LineUpFollowCamPos;
            //初始化队伍阵型
            GameObject objType = AssetLoader.Inst.GetAssetRes(type.ToString());
            FormationCenterObj = Instantiate(objType, data.InitPos, data.InitAngles) as GameObject;
            HeroFormationtype = type;
           // FormationCenterObj.transform.parent = this.transform;
            FormationList = new List<GameObject>();
            for (int i = 0; i < FormationCenterObj.transform.childCount;i++ )
            {
                FormationList.Add(FormationCenterObj.transform.GetChild(i).gameObject);
                //Debug.Log(FormationCenterObj.transform.GetChild(i).position);
            }
            FormationDic = new Dictionary<ObjectCreature, GameObject>();
            //初始化轨迹组件信息
            HeroPathAnim.animationObject = FormationCenterObj.transform;
            HeroPathAnim.pathSpeed = 0;
            Heropathtype = HeroPathMoveType.Null;
            HeroPathPause();
            SetHeroPathIdle();
        }

        /// <summary>
        /// 开启阵型逻辑
        /// </summary>
        public void HeroPathPlay()
        {
            SetHeroPathHoldType(HeroPathHoldType.Play);
        }
        /// <summary>
        /// 暂停阵型逻辑
        /// </summary>
        public void HeroPathPause()
        {
            SetHeroPathHoldType(HeroPathHoldType.Pause);
        }
        /// <summary>
        /// 改变英雄阵型移动速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetFormationSpeed(float speed)
        {
           // MoveSpeed = speed;
            HeroPathAnim.pathSpeed = speed;
        }
        public void HeroPathInitSpeed()
        {
            float Maxspeed=-1;
            for (int i = 0; i < SceneObjectManager.GetInstance().GetSceneHeroList().Count;++i )
            {
                if (SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getMovespeed() > Maxspeed)
                {
                    Maxspeed = SceneObjectManager.GetInstance().GetSceneHeroList()[i].GetHeroRow().getMovespeed();
                }
            }
            MoveSpeed = Maxspeed;
            HeroPathAnim.pathSpeed = MoveSpeed;
        }
        /// <summary>
        /// 英雄阵型进入待机
        /// </summary>
        public void SetHeroPathIdle()
        {
            if (Heropathtype != HeroPathMoveType.Idle)
            {
                Heropathtype = HeroPathMoveType.Idle;
                HeroPathAnim.Pause();
            }
        }
        /// <summary>
        /// 英雄阵型进入正常移动
        /// </summary>
        public void SetHeroPathNormalMove()
        {
            if (Heropathtype != HeroPathMoveType.NormalMove)
            {
                Heropathtype = HeroPathMoveType.NormalMove;
                HeroPathAnim.Play();
                SetFormationSpeed(MoveSpeed);
            }
        }
        /// <summary>
        /// 英雄阵型进入瞬间移动
        /// </summary>
        public void SetHeroPathMomentMoveEnter(int i)
        {
            if (Heropathtype != HeroPathMoveType.MomentMoveEnter)
            {
                Heropathtype = HeroPathMoveType.MomentMoveEnter;
                HeroPath.IsMonmentMove = true;
                HeroPathAnim.animationObject.transform.position = HeroPath.GetPathPosition(HeroPath.GetPathPercentage(i), true);
                HeroPathAnim.animationObject.transform.rotation = HeroPathAnim.GetAnimatedOrientation(HeroPath.GetPathPercentage(i), true);
                HeroPathAnim.percentage = HeroPath.GetPathPercentage(i);
                HeroPathAnim.Pause();
                mMomentMoveIdx = i;
            }
        }
        /// <summary>
        /// 英雄阵型瞬间移动中
        /// </summary>
        public void SetHeroPathMomentMoveIng()
        {
            if (Heropathtype != HeroPathMoveType.MonentMoveIng)
            {
                Heropathtype = HeroPathMoveType.MonentMoveIng;
                HeroPathAnim.Play();
                SetFormationMonmetMove();
            }
        }
        /// <summary>
        /// 英雄阵型退出瞬间移动
        /// </summary>
        public void SetHeroPathMomentMoveExit()
        {
            if (Heropathtype != HeroPathMoveType.MomentMoveExit)
            {
                HeroPath.IsMonmentMove = false;
                if (mMomentMoveIdx == -1)
                {
                    //Debug.LogError("地编事件中MomentMove开始和MomentMove结束不匹配");
                }
                else
                {
                    //HeroPath.GotoPoint(mMomentMoveIdx);
                    //HeroPathAnim.percentage = 0f;
                    HeroPathAnim.animationObject.transform.position = HeroPath.GetPathPosition(HeroPath.GetPathPercentage(mMomentMoveIdx), true);
                    HeroPathAnim.animationObject.transform.rotation = HeroPathAnim.GetAnimatedOrientation(HeroPath.GetPathPercentage(mMomentMoveIdx), true);
                    HeroPathAnim.percentage = HeroPath.GetPercentage(mMomentMoveIdx);

                    Heropathtype = HeroPathMoveType.MomentMoveExit;
                    HeroPathAnim.Pause();
                    mMomentMoveIdx = -1;
                }
            }
        }
        /// <summary>
        /// 英雄阵型进入战斗状态
        /// </summary>
        /// <param name="i">第几波怪物</param>
        public void SetHeroPathFightEnter(int i)
        {
            if (Heropathtype != HeroPathMoveType.FightEnter)
            {
                Heropathtype = HeroPathMoveType.FightEnter;
                FightCenterAngleUpdate(i);
                HeroPathAnim.Play();
                SetFormationSpeed(MoveSpeed);

                if (!mInitFightEulerAngleDone)
                {
                    mInitFightEulerAngleDone = true;
                    mInitFightCamEulerAngle = FightCenter.transform.eulerAngles - HerosCenter.transform.eulerAngles;
                }
            }
        }

        /// <summary>
        /// 设置战斗看点相机固定视角;
        /// </summary>
        public void SetFightFollowCamFixEulerAngle()
        {
            FightCenter.transform.eulerAngles = mInitFightCamEulerAngle + HerosCenter.transform.eulerAngles;
        }

        /// <summary>
        /// 英雄阵型战斗进入状态结束
        /// </summary>
        public void SetHeroPathFightEnterEnd()
        {
            if (Heropathtype != HeroPathMoveType.FightEnterEnd)
            {
                Heropathtype = HeroPathMoveType.FightEnterEnd;
                //SetFormationMonmetMove();
                //HeroPathAnim.Pause();
            }
        }
        /// <summary>
        /// 英雄阵型状态进入准备整队状态
        /// </summary>
        public void SetHeroPathLineUpReady()
        {
            if (Heropathtype == HeroPathMoveType.NormalMove)
            {
                return;
            }
            else if (Heropathtype != HeroPathMoveType.LineUpReady)
            {
                Heropathtype = HeroPathMoveType.LineUpReady;
                SetFormationSpeed(0);
                HeroPathAnim.Pause();
                //HeroPathAnim.isPlaying = false;
            }
        }
        public Vector3 GetHerosCenter()
        {
            return HerosCenter.transform.position;
        }
        public Vector3 GetFightCenter()
        {
            return FightCenter.transform.position;
        }

        public void SetAniObject(GameObject go)
        {
            if (go != null)
            {
                HeroPathAnim.animationObject = go.transform;
            }
            else
            {
                HeroPathAnim.animationObject = FormationCenterObj.transform;
            }
        }

        public void SetHerosParent(GameObject go)
        {
            if (go != null)
            {
                FormationCenterObj.transform.SetParent(go.transform, true);
                //HeroGroupContrler.inst.transform.SetParent(go.transform, true);
            }
            else
            {
                FormationCenterObj.transform.SetParent(transform, true);

            }
        }

        //=================================================================阵型接口===================================================================
        /// <summary>
        /// 获取英雄路径信息
        /// </summary>
        /// <returns></returns>
        public HeroPathDataObj GetHeroPathData()
        {
            return HeroPathdata;
        }
        public GameObject GetFormationObj()
        {
            return FormationCenterObj;
        }
        /// <summary>
        /// 改变阵型
        /// </summary>
        /// <param name="type">阵型类型</param>
        public void SetFormation()
        {
            CreatFormation();
        }
        /// <summary>
        /// 获取阵型前后排距离
        /// </summary>
        /// <returns></returns>
        public float GetFormationSpacing()
        {
            FormationSpacing=GetFormationSpcing(int.Parse(HeroFormationtype.ToString().Substring(10, 1)),int.Parse(HeroFormationtype.ToString().Substring(11, 1)));
            return FormationSpacing;
        }
        /// <summary>
        /// 添加英雄Obj
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <param name="PosObj">阵型目标点Obj</param>
        public void AddObjToFormation(ObjectCreature HeroObj, GameObject pi)
        {
            FormationDic.Add(HeroObj, pi);
        }
        /// <summary>
        /// 获取初始阵型GameObj坐标
        /// </summary>
        /// <returns></returns>
        public Vector3 GetFormationCenterPos(int i)
        {
            return FormationList[i].transform.position;
        }
        /// <summary>
        /// 获取初始阵型GameObj角度
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Quaternion GetFormationCenterAngle(int i)
        {
            return FormationList[i].transform.rotation;
        }
        /// <summary>
        /// 初始化阵型坐标给Hero
        /// </summary>
        /// <param name="HeroObj">HeroObj</param>
        /// <param name="i">索引值</param>
        /// <returns></returns>
        public void InitFormationPos(ObjectCreature HeroObj, int i)
        {
            AddObjToFormation(HeroObj, FormationList[i]);
            //PosInfo pi = new PosInfo(FormationList[i], Vector3.zero);
            //AddObjToFormation(HeroObj, pi);
        }
        /// <summary>
        /// 获取英雄对应阵型追踪点
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <returns></returns>
        public Vector3 GetFormationPos(ObjectCreature HeroObj)
        {
            return FormationDic[HeroObj].transform.position;
            //return FormationDic[HeroObj].CurPos;
        }

        /// <summary>
        /// 获得当前回合时候英雄中心点;
        /// </summary>
        /// <returns></returns>
        Vector3 GetHeroInitCenter()
        {
            
            float x = 0f;
            float y = 0f;
            float z = 0f;

            int heroCount = GlobalMembers.MAX_TEAM_CELL_COUNT;

            for (int i = 0; i < heroCount; i++)
            {
                Vector3 pos = GetFormationCenterPos(i);

                x += pos.x;
                y += pos.y;
                z += pos.z;
            }

            return new Vector3(x / heroCount, y / heroCount, z / heroCount);
        }

        /// <summary>
        /// 重算一下，位置偏移;
        /// </summary>
        public void ResetMoveTargetOffset()
        {
            Vector3 direction = GetMonsterInitCenter() - GetHeroInitCenter();

            mMoveTargePosOffset = HeroPathdata.MoveDistance * direction.normalized;
        }

        /// <summary>
        /// 获取移动位置偏移量;
        /// </summary>
        /// <returns></returns>
        public Vector3 GetMoveTargetOffset()
        {
            return mMoveTargePosOffset;
        }

        /// <summary>
        /// 获取英雄对应阵型调整阵型，向前移动一小段距离后的位置;
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <returns></returns>
        public Vector3 GetFormationMovePos(ObjectCreature HeroObj)
        {
            //return FormationDic[HeroObj].NextPos;
            return GetFormationPos(HeroObj) + mMoveTargePosOffset;
        }

        public Quaternion GetFormationAngle(ObjectCreature HeroObj)
        {
            return FormationDic[HeroObj].transform.rotation;
            //return FormationDic[HeroObj].Rotate;
        }
        /// <summary>
        /// 获取英雄对应的阵型前后排信息(临时接口)
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <returns></returns>
        public string GetFormationTag(ObjectCreature HeroObj)
        {
            return FormationDic[HeroObj].tag;
            //return FormationDic[HeroObj].Tag;
        }
        //=================================================================摄像机接口=================================================================
        /// <summary>
        /// 获取战斗摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetFightFollowCam()
        {
            return FightFollowCam;
        }
        /// <summary>
        /// 获取默认视角战斗摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetFightDefaultCam()
        {
            return FightDefaultCam;
        }
        /// <summary>
        /// 获取整队摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetLineUpFollowCam()
        {
            return LineUpFollowCam;
        }

        /// <summary>
        /// 获取相机路径上指定点的法线向量;
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector3 GetCurHeroCenterEulerAngles()
        {
            return HerosCenter.transform.eulerAngles;
        }

        //=================================================================怪物接口//=================================================================
        public Vector3 GetMonstersCenter()
        {
            return MonstersCenter;
        }

        /// <summary>
        /// 获得当前回合时候怪物中心点;
        /// </summary>
        /// <returns></returns>
        Vector3 GetMonsterInitCenter()
        {
            int m_RoundInScene = FightControler.Inst.CurRound;
            int MonsterNum = FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene).Count;

            float x = 0f;
            float y = 0f;
            float z = 0f;

            for (int i = 0; i < MonsterNum; i++)
            {
                Vector3 pos = FightEditorContrler.GetInstantiate().GetMonsterGroupEditorData(m_RoundInScene)[i].MyPos;

                x += pos.x;
                y += pos.y;
                z += pos.z;
            }

            return new Vector3(x / MonsterNum, y / MonsterNum, z / MonsterNum);
        }

        private void OnDestroy()
        {
            inst = this;
        }
    }
}

