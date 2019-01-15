using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using GNET;
using DreamFaction.UI;


public class ModelViewRoom : BaseUI
{
    private GameObject curModel; // 创建的人物模型
    private Dictionary<int,GameObject> HeroMoldList = new Dictionary<int,GameObject>(); // 已经创建的模型列表
    private int HeroID;
    private GameObject mTray;   // 托盘
    private GameObject pos;//3D场景显示用英雄组
    int a;
    GameObject _obj;
    void Awake()
    {
        mTray = transform.FindChild("Quad").gameObject;
        //StartCoroutine(Init());
    }

    //IEnumerator Init()
    //{
    //    yield return new WaitForEndOfFrame();
    //    //var matrix = ObjectSelf.GetInstance().Teams.m_Matrix;
    //    //int nGroup = ObjectSelf.GetInstance().Teams.m_DefaultGroup;
    //    //int HeroCounts = matrix.GetLength(1);
    //    //ObjectHero hero = ObjectSelf.GetInstance().HeroContainerBag.FindHero(matrix[nGroup, 0]);
    //    //ObjectHero hero1 = ObjectSelf.GetInstance().HeroContainerBag.FindHero(matrix[nGroup, 1]);
    //    //ObjectHero hero2 = ObjectSelf.GetInstance().HeroContainerBag.FindHero(matrix[nGroup, 2]);
    //    //ObjectHero hero3 = ObjectSelf.GetInstance().HeroContainerBag.FindHero(matrix[nGroup, 3]);
    //    //ObjectHero hero4 = ObjectSelf.GetInstance().HeroContainerBag.FindHero(matrix[nGroup, 4]);
    //    //HeroTemplate ht_0 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[hero.GetHeroData().TableID];
    //    //HeroTemplate ht_1 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[hero1.GetHeroData().TableID];
    //    //HeroTemplate ht_2 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[hero2.GetHeroData().TableID];
    //    //HeroTemplate ht_3 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[hero3.GetHeroData().TableID];
    //    //HeroTemplate ht_4 = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[hero4.GetHeroData().TableID];
    //    //ArtresourceTemplate at_0 = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[ht_0.m_artresources];
    //    //ArtresourceTemplate at_1 = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[ht_1.m_artresources];
    //    //ArtresourceTemplate at_2 = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[ht_2.m_artresources];
    //    //ArtresourceTemplate at_3 = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[ht_3.m_artresources];
    //    //ArtresourceTemplate at_4 = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[ht_4.m_artresources];
    //    //GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(at_0.m_artresources);
    //    //curModel = Instantiate(curModel,transform.position, Quaternion.identity) as GameObject;
    //    Init3DHeros();
    //}



    public void UpdateRenderTextureWhitOutTaizi(int heroID)
    {
        UpdateRenderTexture(heroID);
        mTray.SetActive(false);
    }
    public void Update()
    {
//         List<GameObject> PointList = new List<GameObject>();
//         for (int i = 0; i < mTray.transform.childCount; ++i)
//         {
//             PointList.Add(mTray.transform.GetChild(i).gameObject);
//         }
//         int GroupCount = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
//         int HeroCount = ObjectSelf.GetInstance().Teams.m_Matrix.GetLength(1);
// 
// 
//         ObjectHero temp = ObjectSelf.GetInstance().HeroContainerBag.FindHero(ObjectSelf.GetInstance().Teams.m_Matrix[GroupCount, HeroList_Item.i]);
//         Debug.Log(HeroList_Item.i);
//         //if (temp == null)
//         //continue;
//         //_HeroData = new HeroTemplate();
//         HeroTemplate _HeroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.m_Data[temp.GetHeroRow().GetID()];
//         ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate();
//         _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.m_Data[_HeroData.m_artresources];
//         GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(_Artresourcedata.m_artresources);
//         _obj = Instantiate(_AssetRes, transform.position, Quaternion.identity) as GameObject;
        
        //_obj.gameObject.
        
        //_obj.GetComponent<Animation>().Play("Nidle1");
        //_obj.GetComponent<Animation>().wrapMode = WrapMode.Loop;
    }

    // 更新渲染纹理
    public  void UpdateRenderTexture(int heroID)
    {
        mTray.SetActive(true);
        if (HeroID == heroID)
            return;
        HeroID = heroID;
        //更新纹理RanderTexture
        if (curModel != null)
            GameObject.Destroy(curModel);
        Vector3 posMove = Vector3.zero;
        Vector3 endScaleNum = Vector3.one;
        Quaternion endrot = new Quaternion();
        endrot.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        switch (heroID)
        {
            case 10004:
                // 普罗米修斯 位置偏移量
                posMove = new Vector3(0.1910019f, 0.01699996f, 0.118f);
                endScaleNum = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 10003:
                // 艾露恩 位置偏移量
                endScaleNum = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 10001:
                // 安妮
                endScaleNum = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 10005:
                // 梅林
                endScaleNum = new Vector3(1.1f, 1.1f, 1.1f);
                break;
            case 10002:
                // 亚瑟
                endScaleNum = new Vector3(1.2f, 1.2f, 1.2f);
                break;
        }
        curModel = Instantiate(curModel, (transform.position + posMove), endrot) as GameObject;
        curModel.transform.parent = transform;
        curModel.transform.localScale = endScaleNum;
        //curModel.GetComponent<ShowHero>().enabled = false; // 这ShowHero类报错，所以= false；
        
    }

}
