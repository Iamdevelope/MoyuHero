using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;
using DreamFaction.LogSystem;

namespace DreamFaction.GameSceneEditorText
{
    /// <summary>
    /// 临时怪物控制器
    /// </summary>
    public class MonsterGroupContrler : MonoBehaviour
    {
        private WWW MonsterGroupAssetbundle;
        public MonsterGroupDataObj MonsterGroupData;
        private float Xcenter = 0;
        private float Zcenter = 0;
        private float Ycenter = 0;
        private Vector3 Centercount = new Vector3();
        /// <summary>
        /// 单例
        /// </summary>
        public static MonsterGroupContrler inst;
        /// <summary>
        /// 怪物数组
        /// </summary>
        public List<GameObject> MonstersList;
        /// <summary>
        /// 怪物组中心点
        /// </summary>
        public Vector3 MonsterCenterPos;
        private void Awake()
        {
            inst = this; 
        }
       
        private void Start()
        {
            StartCoroutine("LoadAssetbundle");
            MonstersList = new List<GameObject>();
			GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareEnemy, CreatMonsters);
        }
        private void Update()
        {
            MonsterCenterPosUpdate();
        }
        private void MonsterCenterPosUpdate()
        {
            MonsterCenterPos = GetCenterCount(MonstersList);
        }
        private Vector3 GetCenterCount(List<GameObject> tag)
        {
            Xcenter = 0;
            Zcenter = 0;
            Ycenter = 0;
            Centercount = Vector3.zero;
            if (tag.Count != 0 && tag != null)
            {
                for (int i = 0; i < tag.Count; i++)
                {
                    Xcenter += tag[i].transform.position.x;
                    Zcenter += tag[i].transform.position.z;
                    Ycenter += tag[i].transform.position.y;
                }
                Centercount = new Vector3(Xcenter / tag.Count, Ycenter / tag.Count, Zcenter / tag.Count);
            }
            return Centercount;
        }
        private IEnumerator LoadAssetbundle()
        {
            MonsterGroupAssetbundle = new WWW("file://" + Application.streamingAssetsPath + "/ScenceEditor/WindowsEditor/" + Application.loadedLevelName + "/Monster/" + Application.loadedLevelName + "Monster" + ".assetbundle");
            yield return MonsterGroupAssetbundle;
            if (MonsterGroupAssetbundle.error != null)
            {
                Debug.Log(MonsterGroupAssetbundle.error);
            }
            if (MonsterGroupAssetbundle.isDone)
            {
                //转换资源
                //AssetBundle bundle = MonsterGroupAssetbundle.assetBundle;
              //  object[] List = bundle.LoadAll(typeof(UnityEngine.Object));
                MonsterGroupData = MonsterGroupAssetbundle.assetBundle.mainAsset as MonsterGroupDataObj;
            }
            else
            {
                LogManager.Log("加载失败");
            }
        }
        private void CreatMonsters(GameEvent temp)
        {
            LogManager.Log(temp.data);
            for (int i = 0; i < MonsterGroupData.MonsterGroupDataList.Count;i++ )
            {
                if(MonsterGroupData.MonsterGroupDataList[i].MonstersGroupID==(int)temp.data)
                {
                    for(int j=0;j<MonsterGroupData.MonsterGroupDataList[i].MonsterGroupdata.Count;j++)
                    {
                        GameObject Monster = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Monster.GetComponent<BoxCollider>().enabled = false;
                        Monster.transform.parent = this.transform;
                        Monster.transform.position = MonsterGroupData.MonsterGroupDataList[i].MonsterGroupdata[j].MyPos;
                        MonstersList.Add(Monster);
                    }
                }
            }
                //for(int i=0;i<MonsterGroupData.MonsterGroupDataList[(int)temp.data-1].MonsterGroupdata.Count;i++)
                //{
                //    GameObject Monster = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //    Monster.GetComponent<BoxCollider>().enabled = false;
                //    Monster.transform.parent = this.transform;
                //    Monster.transform.position = MonsterGroupData.MonsterGroupDataList[(int)temp.data - 1].MonsterGroupdata[i].MyPos;
                //    MonstersList.Add(Monster);
                //}
               // Invoke("RemoveHero", 4.5f);
                Invoke("FightEnd", 5.0f);
        }
        private void FightEnd()
        {
            FightTempContrler.inst.Sencetype = FightTempContrler.SenceType.LineUp;
            for (int i = 0; i < HeroGroupContrler.inst.HerosList.Count; i++)
            {
                HeroGroupContrler.inst.HerosList[i].GetComponent<ForllowPoint>().SetGo();
            }
            FightEditorContrler.GetInstantiate().CamPause();
            MonstersList.Clear();
            FightEditorContrler.GetInstantiate().HeroPathNormalMove();
        }
        private void RemoveHero()
        {
            Destroy(HeroGroupContrler.inst.HerosList[0].GetComponent<ForllowPoint>().Tag.gameObject);
            Destroy(HeroGroupContrler.inst.HerosList[0]);
            HeroGroupContrler.inst.HerosList.Remove(HeroGroupContrler.inst.HerosList[0]);
            Invoke("ChangeFormation", 0.1f);
        }
        private void ChangeFormation()
        {
           // FightEditorContrler.GetInstantiate().HeroPathSetFormation(HeroFormationType.Formation123);
        }
        private void OnDestroy()
        {
            inst = null;
        }
    }
}

