using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
namespace DreamFaction.GameCore
{
    /// <summary>
    /// Home场景英雄数据类
    /// </summary>
    public class UI_HomeHero
    {
        private static UI_HomeHero _inst;
        private List<ObjectHero> ObjectHeroList=new List<ObjectHero>();
        private List<GameObject> Object2DHeroList = new List<GameObject>();
        private List<GameObject> HeroGameObjectList=new List<GameObject>();
        private List<int> HerocountList = new List<int>();
        private int ObjectCount=0;
        private float CD = 0;
        private int HeroID = 0;//记录已播放英雄索引
        private int TempHeroID = -1;
        private UI_HomeHero() { }
        public static UI_HomeHero GetInstantiate()
        {
            if (_inst == null)
                _inst = new UI_HomeHero();
            return _inst;
        }
        public void AddObjHero(ObjectHero obj)
        {
            if (ObjectHeroList == null)
                ObjectHeroList = new List<ObjectHero>();
            ObjectHeroList.Add(obj);
            ObjectCount = ObjectHeroList.Count;
        }
        public void Add2DObjHero(GameObject obj)
        {
            if (Object2DHeroList == null)
                Object2DHeroList = new List<GameObject>();
            Object2DHeroList.Add(obj);
        }
        public void AddGameObjHero(GameObject obj)
        {
            if (HeroGameObjectList == null)
                HeroGameObjectList = new List<GameObject>();
            HeroGameObjectList.Add(obj);
        }
        
        public List<GameObject> GetHeroGameObjectList()
        {
            return HeroGameObjectList;
        }
        public List<GameObject> GetObject2DHeroList()
        {
            return Object2DHeroList;
        }
        public List<ObjectHero> GetObjectHeroList()
        {
            return ObjectHeroList;
        }
        public void SetHeroAnimNidle1()
        {
            for (int i = 0; i < ObjectCount; ++i)
            {
                HeroGameObjectList[i].GetComponent<Animation>().Play("Nidle1");
                HeroGameObjectList[i].GetComponent<Animation>().wrapMode = WrapMode.Loop;
            }
        }
        public void Set2DHeroAnimNidle1()
        {            for (int i = 0; i < ObjectCount; ++i)
            {
                Object2DHeroList[i].GetComponent<Animation>().Play("Nidle1");
                Object2DHeroList[i].GetComponent<Animation>().wrapMode = WrapMode.Loop;
            }
        }
        public int GetObjectCount()
        {
            return ObjectCount;
        }
        public void OnClear()
        {
            ObjectCount = 0;
            ObjectHeroList.Clear();
            HeroGameObjectList.Clear();
            Object2DHeroList.Clear();
        }
        public void HeroAnimInit()
        {
            CD = Random.Range(5, 7);
        }
        public void HeroAnimUpdate()
        {
            CD -= Time.deltaTime;
            if (HeroGameObjectList.Count > 1 && CD <= 0)
            {
                HeroID = Random.Range(0, 5);
                if (HeroID == TempHeroID)
                    return;
                for (int i = 0; i < HerocountList.Count; i++)
                {
                    if (HerocountList[i] == HeroID)
                        return;
                }
                HeroGameObjectList[HeroID].GetComponent<Animation>().CrossFade("Nidle2");
                HeroGameObjectList[HeroID].GetComponent<Animation>().wrapMode = WrapMode.Loop;
                HerocountList.Add(HeroID);
                if (HerocountList.Count == ObjectCount)
                {
                    HerocountList.Clear();
                    TempHeroID = HeroID;
                }
                else
                {
                    TempHeroID = -1;
                }
                CD = Random.Range(4.5f, 6);
            }
        }
    }
}

