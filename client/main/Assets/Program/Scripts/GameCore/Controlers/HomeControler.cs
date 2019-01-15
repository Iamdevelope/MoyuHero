using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using GNET;
using DreamFaction.UI;
using UnityEngine.EventSystems;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameAudio;

namespace DreamFaction.GameCore
{
    public class HomeControler : BaseControler
    {
        // 单例
        private static HomeControler _inst;
        public static bool isMainHomeWindow = true;  
        private GameObject Herogroup1;                           //3D场景显示用英雄组
        private GameObject Herogroup2;
        private GameObject TeamViewRoomTeam1;                   //2D场景映射显示用Team1
        private GameObject TeamViewRoomTeam2;                   //2D场景映射显示用Team2
        private Transform CurrentViewRoomTeam;                  //2D场景当前显示Team
        private List<GameObject> HeroObjs2D;                    //2D显示用OBJ
        private List<GameObject> HeroObjs3D;                    //3D显示用OBJ
        private List<int> tipsImdex = new List<int>();
        private X_GUID[] CurrenViewGuid = new X_GUID[5];        //当前显示的英雄的GUid
        public AudioClip m_MianClip = null;                     //主界面以及其他非战斗界面背景音乐
        private bool isGradulMin = true;
        private List<int> funly = new List<int>();
        private float m_MusicVolumeTemp;

        // ============================= 公共属性(限制外部修改) ===================
        public static HomeControler Inst
        {
            get
            {
                return _inst;
            }
        }

        public Transform GetHoergroup1()
        {
            return Herogroup1.transform;
        }

        public Transform GetHoergroup2()
        {
            return Herogroup2.transform;
        }
        // ============================= 继承接口 =================================
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
            SceneManager.Inst.EndChangeScene(SceneEntry.Home.ToString());
            Herogroup1 = GameObject.Find("HeroGroup1");
            Herogroup2 = GameObject.Find("HeroGroup2");
            TeamViewRoomTeam1 = GameObject.Find("TeamViewRoom").transform.FindChild("Team1").gameObject;
            TeamViewRoomTeam2 = GameObject.Find("TeamViewRoom").transform.FindChild("Team2").gameObject;
            HeroObjs2D = new List<GameObject>();
            HeroObjs3D = new List<GameObject>();
            InitTipsIndex();
            //Util.Add<GameLuaManager>(gameObject);

            //监听初始化编辑器完成事件 [1/20/2015 Zmy]
            GameEventDispatcher.Inst.addEventListener(GameEventID.Net_MCDetailCampaignPacket_OK, CallBack_DetailCampaignPacket_OK);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Lith_Hero_Update, UpdateFormationGameObject);
            GameEventDispatcher.Inst.addEventListener(GameEventID.HE_HeroSkin, UpdateFormationGameObject);
            GameEventDispatcher.Inst.addEventListener(GameEventID.G_Soul_Hero_Update,UpdateFormationGameObject);
            GameEventDispatcher.Inst.addEventListener(GameEventID.Net_RefreshHero, UpdateFormationGameObject);

			// 测试代码 [1/19/2015 Zmy]
			//if (!IsInvoking("OnStartLoad"))
			//	Invoke("OnStartLoad", 0.1f);

            m_MusicVolumeTemp = AudioControler.Inst.musicVolume;
            InvokeRepeating("AudioGradul", 0, 0.15f);
        }

        public void PushFunly(int index, int data)
        {
            if (index == 0)
            {
                funly.Clear();
                Invoke("ClearFunly", 60.0f);
            }

            if(funly.Count == index)
                funly.Add(data);
            else
            {
                funly.Clear();
            }
        }

        void ClearFunly()
        {
            funly.Clear();
        }

        /// <summary>
        /// 控制音乐淡入淡出
        /// </summary>
        private void AudioGradul()
        {
            if (isGradulMin)
            {

                if (AudioControler.Inst.musicVolume <= 0f)
                {
                    AudioControler.Inst.PlayMusic(m_MianClip);
                    isGradulMin = false;
                }
                else
                    AudioControler.Inst.musicVolume -= 0.05f;
            }
            else
            {

                if (AudioControler.Inst.musicVolume >= m_MusicVolumeTemp)
                {
                    CancelInvoke();
                }
                else
                    AudioControler.Inst.musicVolume += 0.05f;
            }
        }

        public void PlayTipsIndex()
        {

        }

        public void PlayFunly()
        {
            if(funly.Count == 13)
            {
                string str = "";
                for(int i =0; i < funly.Count; ++i)
                {
                    str += (char)(funly[i]);
                }

                InterfaceControler.GetInst().AddMsgBox(str);
                funly.Clear();
            }
        }

		private void OnStartLoad()
		{
			//SceneManager.Inst.StartChangeScene(SceneEntry.Battle01_00);
            //加载图鉴表中的所有英雄模型资源 [7/29/2015 Zmy]
            //int num = DataTemplate.GetInstance().m_IllustratehandbookTable.getDataCount();
            //for (int i = 0; i < num; ++i)
            //{
            //    if (DataTemplate.GetInstance().m_IllustratehandbookTable.tableContainsKey(i) == false)
            //        continue;

            //    IllustratehandbookTemplate row = (IllustratehandbookTemplate)DataTemplate.GetInstance().m_IllustratehandbookTable.getTableData(i);
            //    if (row != null && row.getType() == 1)
            //    {
            //        int nTableID = row.getContentId();
            //        HeroTemplate _rowData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(nTableID);
            //        if (_rowData != null)
            //        {
            //            AssetLoader.Inst.DynamicLoadHeroCardRes(nTableID);
            //        }
            //    }

            //}
		}

        protected override void InitView()
        {
            //UI初始化
            //人物实例化
            InstantiateView();
            IsContinueLimitFight();
        }

        /// <summary>
        /// 是否有未完成的极限试炼
        /// </summary>
        private void IsContinueLimitFight()
        {
            if (!ObjectSelf.GetInstance().LimitFightMgr.m_bRuntime)
                return;

            
            //弹出继续极限试炼
            string _leftBtnTxt = GameUtils.getString("ultimatetrial_form1_button1");
            string _rightBtnTxt = GameUtils.getString("ultimatetrial_form1_button2");
            string _desTxt = GameUtils.getString("ultimatetrial_form1_content2");
            string _descriptionTxt = GameUtils.getString("ultimatetrial_form1_content1");
            UI_RechargeBox _box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            _box.SetIsNeedDescription(false);
            _box.SetDescription_text(_descriptionTxt);
            _box.SetLeftBtn_text(_leftBtnTxt);
            _box.SetRightBtn_text(_rightBtnTxt);
            _box.SetDesTxt(_desTxt);
            _box.SetLeftClick(SendFightOnMeg);
            _box.SetRightClick(SendFightGiveUpMeg);
        }
        /// <summary>
        /// 发送继续战斗消息
        /// </summary>
        private void SendFightOnMeg()
        {
            CBeginEndless _cbe = new CBeginEndless();
            _cbe.troopid = 1;
            IOControler.GetInstance().SendProtocol(_cbe);
        }
        /// <summary>
        /// 发送放弃消息
        /// </summary>
        private void SendFightGiveUpMeg()
        {
            CEndlessEnd _cee = new CEndlessEnd();
            IOControler.GetInstance().SendProtocol(_cee);
        }

        protected override void UpdateView()
        {
            if (Time.frameCount % 60 == 0)
            {
                System.GC.Collect();
            }
            base.UpdateView();
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();
                    //LayerMask mask = 1 << LayerMask.NameToLayer("Default");
                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        Debug.DrawLine(ray.origin, hit.point);
                        Animation anim = hit.transform.gameObject.GetComponent<Animation>();
                        if (anim != null)
                        {
                            if (anim.IsPlaying("Nidle1"))
                            {
                                anim.CrossFade("Fidle0", 0.3f);
                                StartCoroutine(OnWaitAnimEnd(anim));
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator OnWaitAnimEnd(Animation anim)
        {
            yield return new WaitForSeconds(anim["Fidle0"].length);
            if (anim != null)
            {
                //anim.Play("Nidle1");
                anim.CrossFade("Nidle1", 0.3f);
            }           
            //anim.wrapMode = WrapMode.Loop;
        }
        protected override void DestroyData()
        {
            _inst = null;
        }

        public void UpdateFormationGameObject()
        {
            Des3DGameObject();
            Init2DHeros();
            Init3DHeros();
            UI_MainHome.GetInst().ShowMianHeroData();
        }
        private void CallBack_DetailCampaignPacket_OK()
        {
            SceneManager.Inst.StartChangeScene(SceneEntry.Battle01_00.ToString());
        }

        // ============================= 私有函数 =================================
        //实例化显示模型
        private void InstantiateView()
        {
            Init3DHeros();
            //Init2DHeros();
        }
        //创建特效
        private void InstantiateEffs()
        {
            GameObject m_Cloud = AssetLoader.Inst.GetAssetRes("Cloud");
            GameObject Cloud = Instantiate(m_Cloud, Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
        }
        private void Des3DGameObject()
        {
            for (int i= 0 ; i < HeroObjs2D.Count; ++i)
            {
                Destroy(HeroObjs2D[i]);
            }
            for (int i = 0; i < HeroObjs3D.Count; ++i)
            {
                Destroy(HeroObjs3D[i]);
            }
            HeroObjs2D.Clear();
            HeroObjs3D.Clear();
        }

        public void DestroyFromModel()
        {
            for (int i = 0; i < HeroObjs2D.Count; ++i)
            {
                Destroy(HeroObjs2D[i]);
            }
            HeroObjs2D.Clear();


//             for(int i = 0;  i < CurrentViewRoomTeam.childCount; ++i)
//             {
//                 CurrentViewRoomTeam.GetChild(i).gameObject.SetActive(false);
//             }
        }

        //刷新2D显示人物
        public void Init2DHeros()
        {
            //int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            //if (type == 1)
            //{
                TeamViewRoomTeam1.SetActive(true);
                TeamViewRoomTeam2.SetActive(false);
                CurrentViewRoomTeam = TeamViewRoomTeam1.transform;
            //}
            //else
            //{
            //    TeamViewRoomTeam1.SetActive(false);
            //    TeamViewRoomTeam2.SetActive(true);
            //    CurrentViewRoomTeam = TeamViewRoomTeam2.transform;
            //}
            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            for (int i = 0; i < HeroCount; ++i)
            {                
                X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i];

                GameObject _obj = null;
                if (pMemberGuiD.IsValid())
                {
                    ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);

                    if (temp == null) continue;
                        
                    HeroTemplate _HeroData = temp.GetHeroRow();
                    ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(temp.GetHeroData().GetHeroViewID());
                    GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
                    if (_AssetRes != null)
                    {
                        if(_AssetRes.GetComponent<NavMeshAgent>() != null)
                            _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

                        _obj = Instantiate(_AssetRes, CurrentViewRoomTeam.GetChild(i).position, CurrentViewRoomTeam.GetChild(i).rotation) as GameObject;
                        float _zoom = _Artresourcedata.getArtresources_zoom();
                        _obj.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
                    }
                }
//                 else
//                 {
//                     //GameObject _DefRes = AssetLoader.Inst.GetAssetRes("ElementBody02");
//                     GameObject _DefRes = Resources.Load(common.EleBody + "ElementBody02") as GameObject;
//                     if (_DefRes != null)
//                         _obj = Instantiate(_DefRes, CurrentViewRoomTeam.GetChild(i).position, CurrentViewRoomTeam.GetChild(i).rotation) as GameObject;
//                 }

                if (_obj != null)
                {
                    HeroObjs2D.Add(_obj);
                    Animation anim = _obj.GetComponent<Animation>();
                    if (anim != null)
                    {
                        if (anim["Nidle1"] != null)
                        {
                            anim.CrossFade("Nidle1",0.5f);
                        }
                        else if (anim["Idle1"] != null)
                        {
                            anim.CrossFade("Idle1",0.5f);
                        }
                        anim.wrapMode = WrapMode.Loop;
                    }
                }
            }
        }
      
        //刷新3D显示英雄位置
        private void Init3DHeros()
        {
            List<GameObject> PointList = new List<GameObject>();
            //int type = ObjectSelf.GetInstance().Teams.GetFormationType();
            //if (type == 1)
            //{
                for (int i = 0; i < Herogroup1.transform.childCount; ++i)
                {
                    PointList.Add(Herogroup1.transform.GetChild(i).gameObject);
                }
            //}
            //if (type == 2)
            //{
            //    for (int i = 0; i < Herogroup2.transform.childCount; ++i)
            //    {
            //        PointList.Add(Herogroup2.transform.GetChild(i).gameObject);
            //    }
            //}

            int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
            int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
            for (int i = 0; i < HeroCount; ++i)
            {
                ObjectCard temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, i]);
                if (temp == null)
                    continue;
                    
                ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(temp.GetHeroData().GetHeroViewID());
                GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_Artresourcedata.getArtresources());
                if (_AssetRes != null)
                {
                    if (_AssetRes.GetComponent<NavMeshAgent>() != null)
                        _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

                    int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                    X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                    GameObject _obj = Instantiate(_AssetRes, PointList[i].transform.position, PointList[i].transform.rotation) as GameObject;
                    float _zoom = _Artresourcedata.getArtresources_zoom();
                    _obj.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);

                    HeroObjs3D.Add(_obj);
                    Animation anim = _obj.GetComponent<Animation>();
                    if (anim != null)
                    {
                        if (anim["Nidle1"] != null)
                        {
                            anim.CrossFade("Nidle1");
                        }
                        else if (anim["Idle1"] != null)
                        {
                            anim.CrossFade("Idle1");
                        }
                        anim.wrapMode = WrapMode.Loop;
                    }
                }
            }
        }

        public void SetMainHomeWindow(bool isOff)
        {
            isMainHomeWindow = isOff;
            
        }

        void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Lith_Hero_Update, UpdateFormationGameObject);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.G_Soul_Hero_Update, UpdateFormationGameObject);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.Net_RefreshHero, UpdateFormationGameObject);
        }

        void InitTipsIndex()
        {
            tipsImdex.Add(68);
            tipsImdex.Add(114);
            tipsImdex.Add(101);
            tipsImdex.Add(97);
            tipsImdex.Add(109);
            tipsImdex.Add(101);
            tipsImdex.Add(97);
            tipsImdex.Add(114);
            tipsImdex.Add(32);
            tipsImdex.Add(71);
            tipsImdex.Add(97);
            tipsImdex.Add(109);
            tipsImdex.Add(101);
        }
    }

}

