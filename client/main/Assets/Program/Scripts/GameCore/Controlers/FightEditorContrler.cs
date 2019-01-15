using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
using DreamFaction.GameSceneEditorText;
using DG.Tweening;
using System.IO;
using System.Text;

namespace DreamFaction.GameSceneEditor
{
    /// <summary>
    /// 场景编辑器控制器
    /// </summary>
    public class FightEditorContrler : BaseControler
    {
        private static FightEditorContrler inst;              //单例
        private GameObject PathEvent;                         //场景回调事件
        private GameObject HeroPath;                          //场景路线控制器
        private GameObject Cameracontrler;                    //摄像机控制器
        private GameObject StoryAnimContrler;                 //过场动画控制器
        private WWW CamAssetbundle;                           //摄像机场景Assetbundle
        private WWW HeroPathAssetbundle;                      //英雄路径场景Assetbundle
        private WWW MonstersAssetbundle;                      //怪物路径场景Assetbundle
        private WWW StoryAnimAssetbundle;                     //怪物路径场景Assetbundle
        private CameraDataObj Camdata;                        //摄像机场景数据
        private HeroPathDataObj HeroPathdata;                 //英雄路径场景数据
        //private MonsterGroupDataObj Monstersdata;             //所有场景怪物编辑器数据
        private MonsterGroupDataObjMgr Monstersdata;             //所有场景怪物编辑器数据
        private StoryAnimDataObj StoryAnimdata;               //场景动画数据
        private StageTemplate CurStage;                       //当前场景表数据

        private GameObject HeroCenterObj = null;                       //跟踪英雄中心点的go;

        // 人物阴影相关;
        private int m_ShadowLayer = -1;

        protected override void InitData()
        {
            inst = this;
            if (this.GetComponent<FightTempContrler>() == null)
                LoadAssetbundle();
              // StartCoroutine("LoadAssetbundle");
        }
        private void LoadAssetbundle()
        {
            int CurStageID = 0;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                CurStageID = ObjectSelf.GetInstance().GetPromptCurCampaignID();
            }
            else
            {
                CurStageID = ObjectSelf.GetInstance().GetCurCampaignID();
            }
            CurStage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(CurStageID);
            FightControler.isOpeningAnimation = CurStage.m_iskcdh != -1;

            if (CurStage.m_stageevent[0]!="-1")
                Monstersdata = new MonsterGroupDataObjMgr(AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[0]) as MonsterGroupDataObj);
            if (CurStage.m_stageevent[1] != "-1")
            {
                HeroPathdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[1]) as HeroPathDataObj;
                //测试用的;
                HeroPathdata.MoveDistance -= 4f;
            }
            if (CurStage.m_stageevent[2] != "-1")
                Camdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[2]) as CameraDataObj;
            if (CurStage.m_stageevent[3] != "-1")
                StoryAnimdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[3]) as StoryAnimDataObj;
            LoadFightEditor();
        }
//        private IEnumerator LoadAssetbundle()
//        {
            
//            int CurStageID = ObjectSelf.GetInstance().GetCurCampaignID();
//            CurStage = (StageTemplate)DataTemplate.GetInstance().m_StageTable.m_Data[CurStageID];
//            Monstersdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[0]) as MonsterGroupDataObj;
//            HeroPathdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[1]) as HeroPathDataObj;
//            Camdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[2]) as CameraDataObj;
//            StoryAnimdata = AssetLoader.Inst.GetEditorAssetRes(CurStage.m_stageevent[3]) as StoryAnimDataObj;
//            MonstersAssetbundle = new WWW("file:///" + AppManager.Inst.readAndWritePath + CurStage.m_stageevent[0] + ".enc");
//            HeroPathAssetbundle = new WWW("file:///" + AppManager.Inst.readAndWritePath + CurStage.m_stageevent[1] + ".enc");
//            CamAssetbundle = new WWW("file:///" + AppManager.Inst.readAndWritePath + CurStage.m_stageevent[2] + ".enc");
//            StoryAnimAssetbundle = new WWW("file:///" + AppManager.Inst.readAndWritePath + CurStage.m_stageevent[3] + ".enc");
//            yield return CamAssetbundle;
//            if (CamAssetbundle.error != null || HeroPathAssetbundle.error != null || MonstersAssetbundle.error != null || StoryAnimAssetbundle.error!=null)
//            {
//                Debug.Log(CamAssetbundle.error);
//                Debug.Log(HeroPathAssetbundle.error);
//                Debug.Log(MonstersAssetbundle.error);
//                Debug.Log(StoryAnimAssetbundle.error);
//            }
//            if (CamAssetbundle.isDone && HeroPathAssetbundle.isDone && MonstersAssetbundle.isDone&&StoryAnimAssetbundle.isDone)
//            {
////                LogManager.Log(CamAssetbundle);
//                //解密
//                byte[] decryptedData1 = AssetManager.Inst.ExecuteDecrypt(name, CamAssetbundle.bytes);
//                AssetBundleCreateRequest acr1 = AssetBundle.CreateFromMemory(decryptedData1);
//                yield return acr1;
//                AssetBundle Cambundle = acr1.assetBundle;

//                byte[] decryptedData2 = AssetManager.Inst.ExecuteDecrypt(name, HeroPathAssetbundle.bytes);
//                AssetBundleCreateRequest acr2 = AssetBundle.CreateFromMemory(decryptedData2);
//                yield return acr2;
//                AssetBundle HeroPathbundle = acr2.assetBundle;

//                byte[] decryptedData3 = AssetManager.Inst.ExecuteDecrypt(name, MonstersAssetbundle.bytes);
//                AssetBundleCreateRequest acr3 = AssetBundle.CreateFromMemory(decryptedData3);
//                yield return acr3;
//                AssetBundle MonsterGroupbundle = acr3.assetBundle;

//                byte[] decryptedData4 = AssetManager.Inst.ExecuteDecrypt(name, StoryAnimAssetbundle.bytes);
//                AssetBundleCreateRequest acr4 = AssetBundle.CreateFromMemory(decryptedData4);
//                yield return acr4;
//                AssetBundle StoryAnimbundle = acr4.assetBundle;

//                //转换资源
//                Camdata = Cambundle.mainAsset as CameraDataObj;
//               // HeroPathdata = HeroPathbundle.mainAsset as HeroPathDataObj;
//                Monstersdata = MonsterGroupbundle.mainAsset as MonsterGroupDataObj;
//                StoryAnimdata = StoryAnimbundle.mainAsset as StoryAnimDataObj;
//                LoadFightEditor();

//                Cambundle.Unload(false);
//                HeroPathbundle.Unload(false);
//                MonsterGroupbundle.Unload(false);
//                StoryAnimbundle.Unload(false);
//            }
//            else
//            {
//                LogManager.Log("加载失败");
//            }
//        }
    
        //加载场景编辑器数据
        private void LoadFightEditor()
        { 
            //加载场景回调事件
            PathEvent = new GameObject("PathEvent");
            PathEvent.transform.parent = this.transform;
            PathEvent.AddComponent<PathEvent>();
            //加载场景路线
            HeroPath = new GameObject("HeroPathtContrler");
            HeroPath.transform.parent = this.transform;
            //XmlDocument xml = new XmlDocument();
           // XMLHelper.LoadXML(AppManager.Inst.readOnlyPath + "Data/HeroPathXml/" + CurStage.m_stageevent[1] + ".xml", ref xml);
            string str = File.ReadAllText(@AppManager.Inst.readOnlyPath + "Data/HeroPathXml/" + CurStage.m_stageevent[1] + ".xml", Encoding.UTF8);
            //XMLHelper.LoadXML(AppManager.Inst.readOnlyPath + "Data/HeroPathXml/" + CurStage.m_stageevent[1] + ".xml", ref xml);
            HeroFormationType type = ObjectSelf.GetInstance().Teams.GetFormation(); ;
            //HeroPath.AddComponent<HeroPathtContrler>().Init(HeroPathdata, type, xml);
            HeroPath.AddComponent<HeroPathtContrler>().Init(HeroPathdata, type, str);
            //加载过场动画组件
            StoryAnimContrler = new GameObject("StoryAnimEditorContrler");
            StoryAnimContrler.transform.parent = this.transform;
            StoryAnimEditorContrler storyControler = StoryAnimContrler.AddComponent<StoryAnimEditorContrler>();
            EM_SCENE_TYPE sceneType = ObjectSelf.GetInstance().IsLimitFight ? EM_SCENE_TYPE.JIXIANSHILIAN : EM_SCENE_TYPE.NORMAL;
            int beginRound = sceneType == EM_SCENE_TYPE.JIXIANSHILIAN ? ObjectSelf.GetInstance().LimitFightMgr.m_BeginRoundNum : 1;
            storyControler.Init(StoryAnimdata, false, Monstersdata, sceneType, beginRound);
            //加载摄像机组件
            Cameracontrler = new GameObject("CameraContrler");
            Cameracontrler.transform.parent = this.transform;
            Cameracontrler.AddComponent<CameraContrler>().Init(Camdata, StoryAnimdata);

            //创建一个跟踪HeroCenter的物体;
            HeroCenterObj = new GameObject("HeroCenterObj");
            HeroCenterObj.transform.SetParent(this.transform);

            //初始化人物灯光;
            InitSpotLight();

            //初始化人物阴影;
            InitHeroShadow();
        }

        private void InitSpotLight()
        {
            GameObject go = GameObject.Find("SpotLight");
            if (go != null)
            {
                AddObjToHeroCenter(go, Vector3.zero);
            }
        }

        private void InitHeroShadow()
        {
            GameObject go = GameObject.Find("ShadowProject");
            if (go != null)
            {
                //删除相机上多余的AudioListener;
                Transform trans = go.transform;
                for (int i = 0, j = trans.childCount; i < j; i++)
                {
                    Camera c = trans.GetChild(i).GetComponent<Camera>();
                    if (c == null)
                        continue;

                    //相机不能是主相机(和英雄一样的Tag);
                    c.tag = "Untagged";

                    //注，下面这句很可能有问题--这里是写死的，怎家层级可能会导致问题;
                    //m_ShadowLayer = c.cullingMask - 1;
                    m_ShadowLayer = LayerMask.NameToLayer("TransparentFX");

                    //Debug.Log(go.layer + LayerMask.LayerToName(go.layer) + "-----------" + c.cullingMask + LayerMask.LayerToName(c.cullingMask));

                    AudioListener al = c.GetComponent<AudioListener>();
                    if (al != null)
                    {
                        UnityEngine.Object.Destroy(al);
                        break;
                    }
                }

                AddObjToHeroCenter(go, Vector3.zero);
            }
            else
            {
                m_ShadowLayer = LayerMask.NameToLayer("Default");
            }
        }

        protected override void UpdateData()
        {
            base.UpdateData();

            if (HeroCenterObj != null)
            {
                HeroCenterObj.transform.position = GetHerosCenter();
            }
        }

        //############################################摄像机调用接口############################################
        public static FightEditorContrler GetInstantiate()
        {
            return inst;
        }
        /// <summary>
        /// 摄像机播放
        /// </summary>
        public void CamPlay()
        {
            CameraContrler.GetInstantiate().Play();
        }
        /// <summary>
        /// 摄像机停止
        /// </summary>
        public void CamPause()
        {
            CameraContrler.GetInstantiate().Pause();
        }
        /// <summary>
        /// 发送摄像机信息
        /// </summary>
        /// <param name="id">ID</param>
        public void SetCamInfo(string id)
        {
            CameraContrler.GetInstantiate().SetCamInfo(id);
        }
        /// <summary>
        /// 发送摄像机信息
        /// </summary>
        /// <param name="info">信息结构</param>
        public void SetCamInfo(Caminfo info)
        {
            CameraContrler.GetInstantiate().SetCamInfo(info);
        }
        /// <summary>
        /// 获取战斗摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetFightFollowCam()
        {
            return HeroPathtContrler.GetInstantiate().GetFightFollowCam();
        }
        /// <summary>
        /// 获取默认视角战斗摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetFightDefaultCam()
        {
            return HeroPathtContrler.GetInstantiate().GetFightDefaultCam();
        }
        /// <summary>
        /// 获取整队摄像机
        /// </summary>
        /// <returns></returns>
        public GameObject GetLineUpFollowCam()
        {
            return HeroPathtContrler.GetInstantiate().GetLineUpFollowCam();
        }
        /// <summary>
        /// 摄像机震屏
        /// </summary>
        public void ShakeCamera()
        {
            CameraContrler.GetInstantiate().ShakeCamera();
        }
        /// <summary>
        /// 技能震屏
        /// </summary>
        /// <param name="value">技能震屏类型</param>
        public void SkillShake(int value,EM_SPELL_SHAKE_TYPE shaketype)
        {
            CameraContrler.GetInstantiate().SkillShake(value, shaketype);
        }
        //############################################英雄阵型调用接口############################################
        /// <summary>
        /// 获取英雄路径信息
        /// </summary>
        /// <returns></returns>
        public HeroPathDataObj GetHeroPathData()
        {
            return HeroPathtContrler.GetInstantiate().GetHeroPathData();
        }



        /// <summary>
        /// 英雄阵型开
        /// </summary>
        public void HeroPathPlay()
        {
            HeroPathtContrler.GetInstantiate().HeroPathPlay();
        }
        /// <summary>
        /// 英雄阵型关
        /// </summary>
        public void HeroPathPause()
        {
            HeroPathtContrler.GetInstantiate().HeroPathPause();
        }
        /// <summary>
        /// 改变英雄阵型移动速度
        /// </summary>
        /// <param name="speed">速度值</param>
        public void HeroPathSetSpeed(float speed)
        {
            HeroPathtContrler.GetInstantiate().SetFormationSpeed(speed);
        }
        /// <summary>
        /// 初始化阵型速度
        /// </summary>
        public void HeroPathInitSpeed()
        {
            HeroPathtContrler.GetInstantiate().HeroPathInitSpeed();
        }
        /// <summary>
        /// 英雄阵型进入待机
        /// </summary>
        public void HeroPathIdle()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathIdle();
        }
        /// <summary>
        /// 英雄阵型进入正常移动
        /// </summary>
        public void HeroPathNormalMove()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathNormalMove();
        }
        /// <summary>
        /// 英雄阵型进入瞬间移动
        /// </summary>
        public void HeroPathMomentMoveEnter(int i)
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathMomentMoveEnter(i);
        }
        /// <summary>
        /// 英雄阵型瞬间移动中
        /// </summary>
        public void HeroPathMomentMoveIng()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathMomentMoveIng();
        }
        /// <summary>
        /// 英雄阵型退出瞬间移动
        /// </summary>
        public void HeroPathMomentMoveExit()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathMomentMoveExit();
        }
        /// <summary>
        /// 英雄阵型进入战斗状态
        /// </summary>
        /// <param name="i">第几波怪物</param>
        public void HeroPathFightEnter(int i)
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathFightEnter(i);
        }
        /// <summary>
        /// 英雄阵型战斗进入状态结束
        /// </summary>
        public void HeroPathFightEnterEnd()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathFightEnterEnd();
        }
        /// <summary>
        /// 英雄阵型状态进入准备整队状态
        /// </summary>
        public void HeroPathLineUpReady()
        {
            HeroPathtContrler.GetInstantiate().SetHeroPathLineUpReady();
        }
        /// <summary>
        /// 改变阵型
        /// </summary>
        /// <param name="type">阵型类型</param>
        public void HeroPathSetFormation()
        {
            HeroPathtContrler.GetInstantiate().SetFormation();
        }
        /// <summary>
        ///  获取阵型GameObjPos
        /// </summary>
        /// <param name="i">索引</param>
        /// <returns></returns>
        public Vector3 GetFormationCenterPos(int i)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationCenterPos(i);
        }
        /// <summary>
        /// 获取GameObjAngle
        /// </summary>
        /// <param name="i">索引</param>
        /// <returns></returns>
        public Quaternion GetFormationCenterAngle(int i)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationCenterAngle(i);
        }
        /// <summary>
        /// 获取阵型前后排距离
        /// </summary>
        /// <returns></returns>
        public float GetFormationSpacing()
        {
            return HeroPathtContrler.GetInstantiate().GetFormationSpacing();
        }
        /// <summary>
        /// 初始化阵型坐标给Hero
        /// </summary>
        /// <param name="HeroObj">HeroObj</param>
        /// <param name="i">索引值</param>
        /// <returns></returns>
        public void InitFormationPos(ObjectCreature HeroObj, int i)
        {
            HeroPathtContrler.GetInstantiate().InitFormationPos(HeroObj, i);
        }
        /// <summary>
        /// 获取英雄对应阵型追踪点
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <returns></returns>
        public Vector3 GetFormationPos(ObjectCreature HeroObj)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationPos(HeroObj);
        }

        /// <summary>
        /// 获取英雄整形向前移动后的阵型位置;
        /// </summary>
        /// <param name="HeroObj"></param>
        /// <returns></returns>
        public Vector3 GetFormationMovePos(ObjectCreature HeroObj)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationMovePos(HeroObj);
        }

        public Quaternion GetFormationAngle(ObjectCreature HeroObj)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationAngle(HeroObj);
        }
        /// <summary>
        /// 获取英雄对应的阵型前后排信息(临时接口)
        /// </summary>
        /// <param name="HeroObj">英雄Obj</param>
        /// <returns></returns>
        public string GetFormationTag(ObjectCreature HeroObj)
        {
            return HeroPathtContrler.GetInstantiate().GetFormationTag(HeroObj);
        }
        /// <summary>
        /// 英雄中心点
        /// </summary>
        /// <returns></returns>
        public Vector3 GetHerosCenter()
        {
            return HeroPathtContrler.GetInstantiate().GetHerosCenter();
        }


        /// <summary>
        /// 停止移动;
        /// </summary>
        /// <param name="data"></param>
        public void HeroOnObject(string data)
        {
            GameObject go = GameObject.Find(data);
            if (go != null)
            {
                HeroPathtContrler.GetInstantiate().SetHeroPathIdle();
                StartCoroutine("HeroPathOnObjectMove", go as object);
            }
            else
            {
                Debug.LogError("不存在指定名字的GameObject，name=" + data);
            }
        }

        IEnumerator HeroPathOnObjectMove(GameObject go)
        {
            yield return new WaitForSeconds(1f);

            HeroPathtContrler.GetInstantiate().SetAniObject(go);
            HeroPathtContrler.GetInstantiate().SetHerosParent(go);
            HeroPathNormalMove();
        }

        public void HeroOffObject()
        {
            HeroPathtContrler.GetInstantiate().SetAniObject(null);
            HeroPathtContrler.GetInstantiate().SetHerosParent(null);
        }

        //############################################怪物接口############################################
        /// <summary>
        /// 获取怪物组数据
        /// </summary>
        /// <returns></returns>
        public MonsterGroupDataObj GetMonsterGroupEditorData()
        {
            return Monstersdata.GroupData; 
        }
        /// <summary>
        /// 获取当前战斗的总波数
        /// </summary>
        /// <returns></returns>
        public int GetMonsterGroupCount()
        {
            return StoryAnimEditorContrler.GetInst().GetMonsterGroupCount();
        }
        /// <summary>
        /// 获取当前波数是否有支援的怪物
        /// </summary>
        /// <returns></returns>
        public bool GetisSupport()
        {
            return StoryAnimEditorContrler.GetInst().GetisSupport();
        }
        /// <summary>
        /// 当前波数怪物是否有支援怪物
        /// </summary>
        /// <returns></returns>
        public bool IsFightOver()
        {
            return StoryAnimEditorContrler.GetInst().IsFightOver();
        }

        /// <summary>
        /// 设置怪物阵型状态1-前2后3；2-前3后2;
        /// </summary>
        /// <param name="type"></param>
        public void SetMonsterTroopType(int type)
        {
            StoryAnimEditorContrler.GetInst().SetMonsterTroopType(1);
        }

        public List<Monsterdata> GetMonsterGroupEditorData(int i)
        {
            return StoryAnimEditorContrler.GetInst().GetMonsterGroupEditorData(i);
        }
        public Vector3 GetMonstersCenter()
        {
            return HeroPathtContrler.GetInstantiate().GetMonstersCenter();
        }

        public Vector3 GetMonsterMovePos(int roundIdx, ObjectMonster monster)
        {
            int idx = SceneObjectManager.GetInstance().GetMonsterIdxByGUID(monster.GUID);

            return GetMonsterMovePos(roundIdx, idx);
        }

        /// <summary>
        /// 获得怪物向前移动指定距离后的位置;
        /// </summary>
        /// <param name="waveIdx">第几波数</param>
        /// <param name="monsterIdx">怪物索引</param>
        /// <returns></returns>
        public Vector3 GetMonsterMovePos(int waveIdx, int monsterIdx)
        {
            Vector3 orgiPos = GetMonsterGroupEditorData(waveIdx)[monsterIdx].MyPos;
            //Vector3 direction = GetHerosCenter() - GetMonstersCenter();
            //Vector3 result = orgiPos + HeroPathdata.MoveDistance * direction.normalized;
            Vector3 result = orgiPos - HeroPathtContrler.GetInstantiate().GetMoveTargetOffset();

            return result;
        }

        /// <summary>
        /// 开启出生怪物行为
        /// </summary>
        /// <param name="obj">怪物Obj</param>
        /// <param name="data">怪物数据</param>
        /// <param name="isLocal">是否本地加载</param>
        public void SetMonsterBirthState(GameObject obj, Monsterdata data, bool isLocal, int DynamicID=0)
        {
            StoryAnimEditorContrler.GetInst().SetMonsterBirthState(obj, data, isLocal, DynamicID);
            StoryAnimEditorContrler.GetInst().GetMonsterObjList().Add(obj);
        }

        /// <summary>
        /// 设置初始当前战斗回合数;
        /// </summary>
        /// <param name="count"></param>
        public void SetBeginFightCount(int count)
        {
            StoryAnimEditorContrler.GetInst().SetBeginFightCount(count);
        }

        //############################################过场动画接口############################################
        public GameObject GetStoryCamAnimsObj()
        {
            return StoryAnimEditorContrler.GetInst().GetStoryCamAnimsObj();
        }
        private void OnDestroy()
        { 
            inst = this;
        }


        //###################################################################
        /// <summary>
        /// 添加go到英雄中心点上;
        /// </summary>
        /// <param name="go"></param>
        private void AddObjToHeroCenter(GameObject go, Vector3 initPos)
        {
            if (go == null)
            {
                return;
            }

            if (HeroCenterObj != null)
            {
                Transform goTrans = go.transform;
                goTrans.SetParent(HeroCenterObj.transform, false);
                goTrans.localPosition = initPos;
            }
        }

        /// <summary>
        /// 获取人物阴影的Layer;
        /// </summary>
        /// <returns></returns>
        public int GetShadowCullMaskLayer()
        {
            return m_ShadowLayer;
        }
    }
}


