using UnityEngine;
using System.Xml;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditorText;
 namespace DreamFaction.GameSceneEditor
 {
     /// <summary>
     /// 场景摄像机控制器
     /// </summary>
     public class CameraContrler : BaseControler
     {
         class MaterialData : System.Object
         {
             public GameObject go = null;
             public Material material = null;

             public override bool Equals(object obj)
             {
                 if (ReferenceEquals(this, obj))
                 {
                     return true;
                 }

                 MaterialData md = obj as MaterialData;

                 if ((md != null) && (go.GetInstanceID() == md.go.GetInstanceID()))
                 {
                     return true;
                 }

                 return false;
             }

             public override int GetHashCode()
             {
                 return go.GetHashCode();
             }
         }

         private static CameraContrler inst;                                        //单例
         private Camera MainCam;                                                    //主摄像机
         private Transform LineUpFollowCam;                                         //非战斗跟随摄像机
         private Transform FightFollowCam;                                          //战斗跟随摄像机
         private Transform FightDefaultCam;                                         //战斗默认跟随摄像机
         private Transform CamTriggerPointGroup;                                    //摄像机触发事件点组
         private GameObject AnimationFollowCam;                                     //动画跟随摄像机
         private CamType Camtype;                                                   //摄像机场景状态
         private CamTagType Camtagtype;                                             //摄像机看向目标O
         private CamMoveType CamPosmoveType;                                        //摄像机坐标移动类型
         private CamMoveType CamAnglesMovetype;                                     //摄像机角度移动类型
         private CamHoldType CamholdType;                                           //摄像机开关
         private CamLookType Camlooktype;                                           //攝像機是否歡動移動開關
         private float CamAnglesMoveSpeed;                                          //摄像机改变目标朝向的速度
         private float CamPosMoveSpeed;                                             //摄像机移动的速度  
         private float CamPosMoveTime;                                              //摄像机坐标平滑阻尼时间
         private float CamAnglesMoveTime;                                           //摄像角度机平滑阻尼时间
         private float CamLookSpeed;                                                //摄像机看向速度
         private Vector3 CamEnterPos;                                               //摄像机进场时移动到的位置点
         private Vector3 CamCenter;                                                 //摄像机目标中心点
         private Vector3 CamStaticPos;                                              //触发事件移动到的静态点坐标
         private Vector3 CamCenterDeviant = Vector3.zero;                           //摄像机视角偏移值
         private Vector3 CamPosDeviant = Vector3.zero;                              //摄像机坐标偏移值
         private Vector3 CamPosvelocity = Vector3.zero;                             //摄像机坐标平滑阻尼参数
         private Vector3 CamAnglesvelocity = Vector3.zero;                          //摄像机角度平滑阻尼参数
         private Quaternion rotation;                                               //摄像机看向目标点的Rotaion
         private Quaternion CamStaticAngles;                                        //触发事件移动到的静态点角度
         private int CaminfoCount;                                                  //摄像机事件个数
         private List<Caminfo> CaminfoList;                                         //场景中所有摄像机事件信息数组
         private StoryAnimDataObj StoryData;                                        //场景中过场动画数据信息
         private float Duration = 0.3f;                                             //震屏持续时间
         private Vector3 Strength = new Vector3(0.3f, 0.3f, 0.0f);                  //震屏各个方向的强度
         private int Vibrato = 30;                                                  //震屏颤动值
         private float Randomness = 90f;                                            //震屏随机范围
         private bool IsShackeCamera = false;                                       //震屏开关
         private float CamEnterTime;                                                //摄像机入场时间

         //private List<GameObject> AlphaMaterialObjs = new List<GameObject>();
         //private readonly string AlphaShaderName = "DreamFaction/Effects/CameraTransparency";
         //private List<MaterialData> FadeAlphaTmpTrans = new List<MaterialData>();
         //private Material AlphaMaterial = null;

         private float RaycastMaxDistance = 100f;                                    //射线最大距离;
         private float collisionFadeSpeed = 10;                                     //渐变透明的速率;
         private int mRayCastTransparentLayer = 0;
         private List<Material> _faded_mats = new List<Material>();
         private List<Material> _current_faded_mats = new List<Material>();
         protected override void InitData()
         {
             inst = this;
             //监听过场动画事件
             GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryEnter, StoryStart);
             GameEventDispatcher.Inst.addEventListener(GameEventID.SE_StoryCameraEnter, StoryStart);
             GameEventDispatcher.Inst.addEventListener(GameEventID.F_BattleFail, GameOver);
             GameEventDispatcher.Inst.addEventListener(GameEventID.F_CountDownOver, GameOver);
             GameEventDispatcher.Inst.addEventListener(GameEventID.F_BattleOver, GameOver);

             //InitAlphaMaterial();

             mRayCastTransparentLayer = LayerMask.NameToLayer("RayCastTransparent");
         }
         protected override void InitView()
         {
             if (FightTempContrler.inst != null)
                 init();
             else
             {
                 if (FightControler.isOpeningAnimation)
                 {
                     GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryCameraEnter, 1);
                 }
                 else
                 {
                     GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightEditorLoadDone);
                 }
             }
         }

         //void InitAlphaMaterial()
         //{
         //    Shader s = Shader.Find(AlphaShaderName);
         //    if (s == null)
         //    {
         //        Debug.LogError("Shader Not Found");
         //        return;
         //    }

         //    Material mat = new Material(s);
         //    //mat.SetFloat("_Gray", 1f);

         //    AlphaMaterial = mat;
         //}

         //剧情开始
         private void StoryStart(GameEvent ID)
         {
             int id = (int)ID.data-1;
             if (StoryData == null)
                 return;
             FightEditorContrler.GetInstantiate().GetStoryCamAnimsObj().transform.position = StoryData.StoryAnimGroupList[id].CamTagPos;
             FightEditorContrler.GetInstantiate().GetStoryCamAnimsObj().transform.rotation = StoryData.StoryAnimGroupList[id].CamTagAngle;
             SetCamtype(CamType.Null);
             SetCamTagType(CamTagType.Null);
             MainCam.transform.DORotate(StoryData.StoryAnimGroupList[id].CamTagAngle.eulerAngles, StoryData.StoryAnimGroupList[id].CamToTagTime);
             MainCam.transform.DOMove(StoryData.StoryAnimGroupList[id].CamTagPos, StoryData.StoryAnimGroupList[id].CamToTagTime).OnComplete(StoryCamToEnd);
         }
         //战斗失败摄像停止更新
         private void GameOver()
         {
             SetCamholdType(CamHoldType.Pause);
         }
         private void StoryCamToEnd()
         {
             SetCamtype(CamType.Animation);
             SetCamTagType(CamTagType.AnimationCenter);
         }
        
         //本地初始化
         private void init()
         {
             MainCam = Camera.main;
             CamEnterTime = this.GetComponent<CameraData>().CamEnterTime;
             AnimationFollowCam = this.transform.FindChild("AnimationFollowCam").gameObject;
             CamTriggerPointGroup = this.transform.FindChild("CamTriggerPointGroup");
             Transform CamAnimationGroup=this.transform.FindChild("CamAnimationGroup");
             CamEnterPos = this.transform.FindChild("CamEnterPos").position;
             LineUpFollowCam = GameObject.Find("LineUpFollowCam").transform;
             FightFollowCam = GameObject.Find("FightFollowCam").transform;
             //FightDefaultCam = GameObject.Find("FightDefaultCam").transform;
             CamCenter = this.transform.FindChild("CamCenter").position;
             CaminfoList = new List<Caminfo>();
             for (int i = 0; i < CamTriggerPointGroup.childCount; i++)
             {
                 CaminfoList.Add(CamTriggerPointGroup.GetChild(i).GetComponent<CamTriggerEvent>().info);
             }
             CaminfoCount = CaminfoList.Count;
             SetCamtype(CamType.Enter);
             SetCamTagType(CamTagType.Null);
             SetCamholdType(CamHoldType.Pause);
             GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightEditorLoadDone);             
             MainCam.GetComponent<Camera>().enabled = true;
             Debug.Log("加载完毕");
         }
         //改变摄像机数据
         private void SetCaminfo(Caminfo info)
         {
             CamholdType = info.CamHoldtype;
             CamStaticPos = info.CamStaticPos;
             CamStaticAngles = info.CamStaticAngles;
             CamAnglesMoveSpeed = info.CamAnglesMoveSpeed;
             CamPosMoveSpeed = info.CamPosMoveSpeed;
             CamPosMoveTime = info.CamPosMoveTime;
             CamAnglesMoveTime = info.CamAnglesMoveTime;
             CamCenterDeviant = info.CamCenterDeviant;
             CamPosDeviant = info.CamPosDeviant;
             Camtype = info.CamType;
             Camtagtype = info.CamTagType;
             CamAnglesMovetype = info.CamAnglesMovetype;
             CamPosmoveType = info.CamPosMovetype;
             Camlooktype = info.Camlooktype;
             CamLookSpeed = info.CamLookSpeed;
         }

         void FixedUpdate()
         {
             Ray ray = new Ray(MainCam.transform.position, CamCenter - MainCam.transform.position);
             //Ray ray = new Ray(MainCam.transform.position, HeroPathtContrler.GetInstantiate().GetFormationCenterPos(1) - MainCam.transform.position);
             RaycastHit[] hits = Physics.RaycastAll(ray, RaycastMaxDistance);

             _current_faded_mats = new List<Material>();

             for (int i = 0, j = hits.Length; i < j; i++)
             {
                 RaycastHit hit = hits[i];

                 Transform trans = hit.transform;

                 //Debug.Log(CamCenter.ToString() + trans.name);

                 if (!trans.renderer || !trans.gameObject.layer.Equals(mRayCastTransparentLayer))
                     continue;

                 Material[] mats = trans.renderer.materials;

                 if (mats == null)
                     continue;

                 foreach (Material mat in mats)
                 {
                     mat.SetFloat("_Fresnel", 0.2f);
                     mat.SetFloat("_Opacity", Mathf.Lerp(mat.GetFloat("_Opacity"), 0.3f, Time.deltaTime * collisionFadeSpeed));
                     mat.SetFloat("_DiffusePower", 0.85f);

                     _current_faded_mats.Add(mat);

                     bool add = true;

                     foreach (Material fmat in _faded_mats)
                     {
                         if (fmat == mat)
                         {
                             add = false;
                             break;
                         }
                     }

                     if (add)
                     {
                         _faded_mats.Add(mat);
                     }
                 }
             }
         }

         /// <summary>
         /// 创建透明的材质;
         /// </summary>
         /// <param name="texture"></param>
         /// <returns></returns>
         //Material CreateAlphaMaterial(Texture texture)
         //{
         //    Material mat = new Material(AlphaMaterial);
             
         //    mat.mainTexture = texture;
         //    mat.SetFloat("_Fresnel", 0.2f);
         //    mat.SetFloat("_Opacity", 0.3f);
         //    mat.SetFloat("_DiffusePower", 0.85f);

         //    return mat;
         //}

         /// <summary>
         /// 刷新透明物体显示;
         /// </summary>
         void UpdateTransObjects()
         {
             foreach (Material mat in _faded_mats)
             {
                 bool skip = false;

                 foreach (Material cmat in _current_faded_mats)
                 {
                     if (mat == cmat)
                     {
                         skip = true;
                         break;
                     }
                 }

                 if (!skip)
                 {

                     if (mat.GetFloat("_Opacity") >= 1.0f)
                     {
                         _faded_mats.Remove(mat);
                         break;
                     }
                     else
                     {
                         mat.SetFloat("_Fresnel", 0f);
                         mat.SetFloat("_Opacity", Mathf.Lerp(mat.GetFloat("_Opacity"), 1.0f, Time.deltaTime * collisionFadeSpeed));
                         mat.SetFloat("_DiffusePower", 0.85f);
                     }
                 }
             }
         }

         protected override void UpdateData()
         {
             if (IsShackeCamera)
                 return;
             CamholdTypeUpdate();

             UpdateTransObjects();
         }
         //摄像机开关更新
         private void CamholdTypeUpdate()
         {
             switch (CamholdType)
             {
                 case CamHoldType.Play:
                     CamTagTypeUpdate();
                     CamAnglesUpdate();
                     CamTypeUpdate();
                   
                     break;
                 case CamHoldType.Pause:
                     break;
             }
         }
         //改变摄像机开关
         private void SetCamholdType(CamHoldType type)
         {
             if (CamholdType != type)
                 CamholdType = type;
         }
         //摄像机朝向更新
         private void CamAnglesUpdate()
         {
             // MainCam.transform.LookAt(CamCenter);
             //CamAnglesLerp = Mathf.PingPong(Time.time, 2) / 2;
              switch (Camtagtype)
              {
                  case CamTagType.Null:
                      switch (Camlooktype)
                      {
                          case CamLookType.NoSlow:
                              MainCam.transform.LookAt(CamCenter);
                              break;
                          default:
                              {
                                  rotation = Quaternion.LookRotation(CamCenter - MainCam.transform.position);
                                  MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, rotation, Time.deltaTime*CamLookSpeed);
                              }
                              break;
                      }
                      break;
                  case CamTagType.HeroCenter:
                      switch (Camlooktype)
                      {
                          case CamLookType.NoSlow:
                              MainCam.transform.LookAt(CamCenter);
                              break;
                          default:
                              {
                                  rotation = Quaternion.LookRotation(CamCenter - MainCam.transform.position);
                                  MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, rotation, Time.deltaTime * CamLookSpeed);
                              }
                              break;
                      }
                      break;
                  case CamTagType.FightCenter:
                      switch (Camlooktype)
                      {
                          case CamLookType.NoSlow:
                              MainCam.transform.LookAt(CamCenter);
                              break;
                          default:
                              {
                                  rotation = Quaternion.LookRotation(CamCenter - MainCam.transform.position);
                                  MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, rotation, Time.deltaTime* CamLookSpeed );
                              }
                              break;
                      }
                      break;
                  case CamTagType.AnimationCenter:
                      //..
                      break;
                  case CamTagType.StaticMoveCenter:
                      //..
                      break;
              }
         }
         //===================================================================摄像机场景状态更新=============================================================================
         private void CamTypeUpdate()
         {
             switch (Camtype)
             {
                 case CamType.Null:
                     NullUpdate();
                     break;
                 case CamType.Enter:
                     EnterUpdate();
                     break;
                 case CamType.LineUp:
                     LineUpUpdate();
                     break;
                 case CamType.Fight:
                     FightUpdate();
                     break;
                 case CamType.FightFixedAOV:
                     if (HeroPathtContrler.GetInstantiate() != null)
                     {
                         HeroPathtContrler.GetInstantiate().SetFightFollowCamFixEulerAngle();
                     }
                     FightFixedAOVUpdate();
                     break;
                 case CamType.DefaultFight:
                     DefaultFightUpdate();
                     break;
                 case CamType.StaticMove:
                     MoveUpdate();
                     break;
                 case CamType.Animation:
                     AnimationUpdate();
                     break;
             }
         }
         private void EnterUpdate()
         {
             // MainCam.transform.position = Vector3.Lerp(MainCam.transform.position,CamEnterPos.position,Time.deltaTime*CamEnterSpeed);
             //MainCam.transform.position = Vector3.MoveTowards(MainCam.transform.position, CamEnterPos, CamEnterSpeed);
             MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, CamEnterPos, ref CamPosvelocity, CamEnterTime);
         }
         private void LineUpUpdate()
         {
             switch (CamPosmoveType)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.position = LineUpFollowCam.position + CamPosDeviant;
                     break;
                 case CamMoveType.NormalMove:
                    // CamPosMoveLerp = Mathf.PingPong(Time.time, CamPosMoveSpeed * 100) / (CamPosMoveSpeed * 100);
                     //MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, LineUpFollowCam.position, CamPosMoveLerp);
                     MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, LineUpFollowCam.position + CamPosDeviant, CamPosMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, LineUpFollowCam.position + CamPosDeviant, ref CamPosvelocity, CamPosMoveTime);
                     break;
             }
         }
         private void MoveUpdate()
         {
             switch (CamPosmoveType)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.position = CamStaticPos + CamPosDeviant;
                     break;
                 case CamMoveType.NormalMove:
                     //CamPosMoveLerp = Mathf.PingPong(Time.time, CamPosMoveSpeed * 100) / (CamPosMoveSpeed * 100);
                     MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, CamStaticPos + CamPosDeviant, CamPosMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, CamStaticPos + CamPosDeviant, ref CamPosvelocity, CamPosMoveTime);
                     break;
             }
         }
         private void FightUpdate()
         {
             switch (CamPosmoveType)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.position = FightFollowCam.position + CamPosDeviant;
                     break;
                 case CamMoveType.NormalMove:
                     //CamPosMoveLerp = Mathf.PingPong(Time.time, CamPosMoveSpeed * 100) / (CamPosMoveSpeed * 100);
                     MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, FightFollowCam.position + CamPosDeviant, CamPosMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, FightFollowCam.position + CamPosDeviant, ref CamPosvelocity, CamPosMoveTime);
                     break;
             }
         }

         private void FightFixedAOVUpdate()
         {
             switch (CamPosmoveType)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.position = FightFollowCam.position + CamPosDeviant;
                     break;
                 case CamMoveType.NormalMove:
                     //CamPosMoveLerp = Mathf.PingPong(Time.time, CamPosMoveSpeed * 100) / (CamPosMoveSpeed * 100);
                     MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, FightFollowCam.position + CamPosDeviant, CamPosMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, FightFollowCam.position + CamPosDeviant, ref CamPosvelocity, CamPosMoveTime);
                     break;
             }
         }

         private void DefaultFightUpdate()
         {
             switch (CamPosmoveType)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.position = FightDefaultCam.position + CamPosDeviant;
                     break;
                 case CamMoveType.NormalMove:
                     //CamPosMoveLerp = Mathf.PingPong(Time.time, CamPosMoveSpeed * 100) / (CamPosMoveSpeed * 100);
                     MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, FightDefaultCam.position + CamPosDeviant, CamPosMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, FightDefaultCam.position + CamPosDeviant, ref CamPosvelocity, CamPosMoveTime);
                     break;
             }
         }
         private void AnimationUpdate()
         {
             MainCam.transform.position = AnimationFollowCam.transform.position;
            // MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, AnimationFollowCam.transform.position, CamPosMoveSpeed * Time.deltaTime);
             
             //switch (CamPosmoveType)
             //{
             //    case CamMoveType.MomentMove:
             //        MainCam.transform.position = AnimationFollowCam.transform.position;
             //        break;
             //    case CamMoveType.NormalMove:
             //        MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, AnimationFollowCam.transform.position, CamPosMoveSpeed * Time.deltaTime);
             //        break;
             //    case CamMoveType.NormalMoveTime:
             //        MainCam.transform.position = Vector3.SmoothDamp(MainCam.transform.position, AnimationFollowCam.transform.position, ref CamPosvelocity, CamPosMoveTime);
             //        break;
             //}
         }
         private void NullUpdate()
         {
             //...
         }
         //改变摄像机场景状态
         private void SetCamtype(CamType type)
         {
             if (Camtype != type)
                 Camtype = type;
         }
         //============================================================================End=====================================================================================
         //===================================================================摄像机目标状态更新===============================================================================
         private void CamTagTypeUpdate()
         {
             switch (Camtagtype)
             {
                 case CamTagType.Null:
                     break;
                 case CamTagType.HeroCenter:
                     HeroCenterUpdate();
                     break;
                 case CamTagType.FightCenter:
                     FightCenterUpdate();
                     break;
                 case CamTagType.AnimationCenter:
                     AnimationCenterUpdate();
                     break;
                 case CamTagType.StaticMoveCenter:
                     StaticMoveCenterUpdate();
                     break;
             }
         }
         private void HeroCenterUpdate()
         {
             switch (CamAnglesMovetype)
             {
                 case CamMoveType.MomentMove:
                     CamCenter = HeroPathtContrler.GetInstantiate().GetHerosCenter() + CamCenterDeviant;
                     break;
                 case CamMoveType.NormalMove:
                    // CamAnglesMoveLerp = Mathf.PingPong(Time.time, CamAnglesMoveSpeed * 100) / (CamAnglesMoveSpeed * 100);
                     //   CamCenter.position = Vector3.Lerp(CamCenter.position, HeroPathtContrler.inst.HerosCenter.position, CamAnglesMoveLerp);
                     CamCenter = Vector3.Lerp(CamCenter, HeroPathtContrler.GetInstantiate().GetHerosCenter() + CamCenterDeviant, CamAnglesMoveSpeed * Time.deltaTime);
                     //CamAnglesMoveSpeedUpdate(HeroPathtContrler.inst.HerosCenter);
                     break;
                 case CamMoveType.NormalMoveTime:
                     CamCenter = Vector3.SmoothDamp(CamCenter, HeroPathtContrler.GetInstantiate().GetHerosCenter() + CamCenterDeviant, ref CamAnglesvelocity, CamAnglesMoveTime);
                     break;
             }
         }
         private void FightCenterUpdate()
         {
             switch (CamAnglesMovetype)
             {
                 case CamMoveType.MomentMove:
                     CamCenter = HeroPathtContrler.GetInstantiate().GetFightCenter() + CamCenterDeviant;
                     break;
                 case CamMoveType.NormalMove:
                     //CamAnglesMoveLerp = Mathf.PingPong(Time.time, CamAnglesMoveSpeed * 100) / (CamAnglesMoveSpeed * 100);
                     CamCenter = Vector3.Lerp(CamCenter, HeroPathtContrler.GetInstantiate().GetFightCenter()+ CamCenterDeviant, CamAnglesMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     CamCenter = Vector3.SmoothDamp(CamCenter, HeroPathtContrler.GetInstantiate().GetFightCenter()+ CamCenterDeviant, ref CamAnglesvelocity, CamAnglesMoveTime);
                     break;
             }
         }
         private void StaticMoveCenterUpdate()
         {
             switch (CamAnglesMovetype)
             {
                 case CamMoveType.MomentMove:
                     MainCam.transform.rotation = CamStaticAngles ;
                     break;
                 case CamMoveType.NormalMove:
                     MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, CamStaticAngles, CamAnglesMoveSpeed * Time.deltaTime);
                     break;
                 case CamMoveType.NormalMoveTime:
                     MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, CamStaticAngles, CamAnglesMoveSpeed * Time.deltaTime);
                     break;
             }
         }
         private void AnimationCenterUpdate()
         {
             MainCam.transform.rotation = AnimationFollowCam.transform.rotation;
             //switch(CamAnglesMovetype)
             //{
             //    case CamMoveType.MomentMove:
             //        MainCam.transform.rotation = AnimationFollowCam.transform.rotation;
             //        break;
             //    case CamMoveType.NormalMove:
             //        MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, AnimationFollowCam.transform.rotation, CamAnglesMoveSpeed * Time.deltaTime);
             //        break;
             //    case CamMoveType.NormalMoveTime:
             //        MainCam.transform.rotation = Quaternion.Slerp(MainCam.transform.rotation, AnimationFollowCam.transform.rotation, CamAnglesMoveSpeed * Time.deltaTime);
             //        break;
             //}
         }
         //改变摄像机朝向目标
         private void SetCamTagType(CamTagType Type)
         {
             if (Camtagtype != Type)
                 Camtagtype = Type;
         }
         //震屏结束
         private void OnShakeCameraOver()
         {
             IsShackeCamera = false;
         }
         //============================================================================End=====================================================================================
         public static CameraContrler GetInstantiate()
         {
             return inst;
         }
         /// <summary>
         /// 发送摄像机信息
         /// </summary>
         /// <param name="id">ID</param>
         public void SetCamInfo(string id)
         {
             for (int i = 0; i < CaminfoCount; i++)
             {
                 if (CaminfoList[i].EventID == id)
                 {
                     SetCaminfo(CaminfoList[i]);
                 }
             }
         }
         /// <summary>
         /// 发送摄像机信息
         /// </summary>
         /// <param name="info">info</param>
         public void SetCamInfo(Caminfo info)
         {
             SetCaminfo(info);
         }
         /// <summary>
         /// 动态初始化完成后发送加载完事件给战斗
         /// </summary>
         /// <param name="Camdata">摄像机序列化数据</param>
         public void Init(CameraDataObj Camdata,StoryAnimDataObj Storydata)
         {
             MainCam = Camera.main;
             StoryData = Storydata;
             AnimationFollowCam = new GameObject("AnimationFollowCam");
             if (StoryData!=null)
                 AnimationFollowCam.transform.parent = FightEditorContrler.GetInstantiate().GetStoryCamAnimsObj().transform;
             LineUpFollowCam = FightEditorContrler.GetInstantiate().GetLineUpFollowCam().transform;
             FightFollowCam = FightEditorContrler.GetInstantiate().GetFightFollowCam().transform;
             FightDefaultCam = FightEditorContrler.GetInstantiate().GetFightDefaultCam().transform;
             MainCam.transform.position = Camdata.CameraPos;
             MainCam.transform.eulerAngles = Camdata.CameraAngles;
             CamEnterPos = Camdata.CamEnterPos;
             CamCenter = Camdata.CamCenter;
             CamEnterTime = Camdata.CamEnterTime;
             CaminfoList = new List<Caminfo>();
             CaminfoList = Camdata.CamPointdataList;
             CaminfoCount = CaminfoList.Count;
             SetCamtype(CamType.Enter);
             SetCamTagType(CamTagType.Null);
             SetCamholdType(CamHoldType.Pause);
         }
         //===========================================================战斗调用接口============================================================
         /// <summary>
         /// 摄像机开始
         /// </summary>
         public void Play()
         {
             SetCamholdType(CamHoldType.Play);
         }
         /// <summary>
         /// 摄像机暂停
         /// </summary>
         public void Pause()
         {
             SetCamholdType(CamHoldType.Pause);
         }
         //单一震屏
         public void ShakeCamera()
         {
             IsShackeCamera = true;
             MainCam.transform.DOShakePosition(Duration, Strength, Vibrato, Randomness).OnComplete(OnShakeCameraOver);
         }
         //技能震屏
         public void SkillShake(int value, EM_SPELL_SHAKE_TYPE shaketype)
         {
             if (value == (int)shaketype)
                 ShakeCamera();
         }
         private void OnDestroy()
         {
             _faded_mats = null;
             _current_faded_mats = null;

             inst = null;
         }
     }
 }

