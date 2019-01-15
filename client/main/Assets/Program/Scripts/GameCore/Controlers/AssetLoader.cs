using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
namespace DreamFaction.GameCore
{
    /// <summary>
    /// [9/24/2014 Zmy]
    /// 资源下载器，负责游戏运行过程中，根据所需资源动态进行加载并缓存.后续根据需求扩展选择加载策略
    /// </summary>
    public class AssetLoader : BaseControler
    {
        //单例
        private static AssetLoader _inst;
        //标记资源是否下载完毕
        private bool m_bIsReadyOK = false;
        //标记资源是否下载完毕
        private bool m_bIsHeroReadyOK = true;
        //www本地下载资源路径
        private string WWW_LOCAL_PATH;

        private Dictionary<string, GameObject> m_AssetHeroRes;
        private Dictionary<string, List<AnimationClip>> m_AssetHeroAnimactionClip;
        //缓存当前已下载的资源bundle信息
        private Dictionary<string, GameObject> m_AssetDynamicBundle;
        private Dictionary<string, UnityEngine.Object> m_EditorAssetDynamicBundle;
        private Dictionary<string, Sprite> m_AssetDynamicSprite;
        private Dictionary<string, Sprite> m_AssetIconSprite;
        private Dictionary<string, Sprite> m_AssetEternalSprite;

        ///商城资源;
        private List<Sprite> m_ShopAssetList;
        //需要下载的资源列表
        private List<string> m_NeedDownHeroAnimationBundle;
        private List<string> m_NeedDownHeroBundle;
        private List<string> m_NeedDownBundle;
        private List<string> m_NeedDownEternalBundle;
        private List<string> m_NeedDownEditorBundle;
        //公用资源是否加载
        private bool isEternalBundleReady = false;
        //资源文件后缀
        private String _strRes = ".enc";
        //资源文件，动作标识
        private String _strAnimation = "_Anim";
        // ==================================== 公共属性(限制外部修改) =====================================
        public static AssetLoader Inst
        {
            get
            {
                return _inst;
            }
        }
        public bool IsReadyOver
        {
            get
            {
                return m_bIsReadyOK && m_bIsHeroReadyOK;
            }
        }
        // ==================================== 继承接口 =====================================
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

            m_AssetHeroRes = new Dictionary<string, GameObject>();
            m_AssetHeroAnimactionClip = new Dictionary<string, List<AnimationClip>>();
            m_AssetDynamicBundle = new Dictionary<string, GameObject>();
            m_EditorAssetDynamicBundle = new Dictionary<string, UnityEngine.Object>();
            m_AssetDynamicSprite = new Dictionary<string, Sprite>();
            m_AssetIconSprite = new Dictionary<string, Sprite>();
            m_AssetEternalSprite = new Dictionary<string, Sprite>();

            m_NeedDownHeroBundle = new List<string>();
            m_NeedDownHeroAnimationBundle = new List<string>();
            m_NeedDownBundle = new List<string>();
            m_NeedDownEternalBundle = new List<string>();
            m_NeedDownEditorBundle = new List<string>();
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    WWW_LOCAL_PATH = "file://" + AppManager.Inst.readAndWritePath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    WWW_LOCAL_PATH = "file://" + AppManager.Inst.readAndWritePath;
                    break;
                default:
                    WWW_LOCAL_PATH = "file:///" + AppManager.Inst.readAndWritePath + "/";
                    break;
            }

        }
        protected override void DestroyData()
        {
            _inst = null;
        }

        // ==================================== 公共接口 =====================================

        public List<Sprite> GetShopSpriteList()
        {
            if (m_ShopAssetList == null)
            {

                DownloadShopBundle(ShopModule.ShopAssetBundleName);
            }

            return m_ShopAssetList;
        }

        /// <summary>
        /// 获取资源对象
        /// </summary>
        /// <param name="name">资源对象名称，即prefab</param>
        /// <returns></returns>
        public GameObject GetAssetRes(string name)
        {
            GameObject go = null;
            if (m_AssetDynamicBundle.TryGetValue(name, out go) == true)
            {
                return go;
            }
            if (m_AssetHeroRes.TryGetValue(name, out go) == true)
            {
                return go;
            }
            if (go == null)
            {
                LogManager.LogError("!!!!!!Error : AssetRes Failed to get! GetAssetRes() is failed Name:" + name);
            }
            return go;
        }
        public UnityEngine.Object GetEditorAssetRes(string name)
        {
            UnityEngine.Object go = null;
            if (m_EditorAssetDynamicBundle.TryGetValue(name, out go) == false)
            {
                // LogManager.Log("!!!!!!Error : AssetRes Failed to get! GetAssetRes() is failed Name:" + name);
            }
            return go;
        }

        /// <summary>
        /// 准备加载目标场景资源
        /// </summary>
        public void ReadyloadRes(string _strNextScene, bool isSkipDown = false)
        {
            OnClearUp();
            //TODO:按需添加需要下载的资源名称到m_NeedDownBundle，按策划需求写加载策略
            //.......
            if (isSkipDown == false)
            {
                //PushNeedResList(_strNextScene);
                if (SceneManager.Inst.NextloadScene.ToString().Contains(SceneEntry.SkillShow.ToString()))
                    IndexSkillShowSceneHeroModel();
                else
                    IndexHeromodel();
                IndexMonsterModel();

                IndexSceneEditor();
            }

            //按照顺序迭代下载资源。保证所有资源下载完成后发送事件  [9/7/2015 Zmy]
            DownLoadHeroAnimationBundle();
            DownLoadEditorBundle();
            DownLoadEternalBundle();
        }
        //动态加载极限试炼怪物资源 [6/17/2015 Zmy]
        public void DynamicLoadLimitMonsterRes(int nTableID)
        {
            MonsterTemplate _rowData = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(nTableID);
            IndexModel(_rowData);
            IndexSkillmodel(_rowData);
            if (!IsInvoking("DownLoadHeroAnimationBundle"))
                Invoke("DownLoadHeroAnimationBundle", 0.5f);

        }
        public void DynamicLoadHeroCardRes(int nTableID)
        {
            HeroTemplate _rowData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(nTableID);
            if (_rowData != null)
            {
                m_bIsReadyOK = false;
                IndexModel(_rowData, true);
                IndexSkillmodel(_rowData);

                if (!IsInvoking("DownLoadHeroAnimationBundle"))
                    Invoke("DownLoadHeroAnimationBundle", 0.1f);

            }
        }
        // ==================================== 私有接口 =====================================
        /// <summary>
        /// 传入英雄ID加载英雄资源 【Lyq】
        /// </summary>
        /// <param name="heroID">英雄ID</param>
        private void IndexHeroAsset(int heroID)
        {
            HeroTemplate _hero = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(heroID);
            ArtresourceTemplate _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_hero.getArtresources());
            ArtresourceTemplate[] _SkinArtresourcedata = new ArtresourceTemplate[_hero.getUseableArtresource().Length];
            PushNeedDownBundle(_Artresourcedata.getArtresources() + _strRes);
            for (int i = 0; i < _hero.getUseableArtresource().Length; ++i)
            {
                _SkinArtresourcedata[i] = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_hero.getUseableArtresource()[i]);
                if (_SkinArtresourcedata[i].getArtresources() == null)
                    continue;
                PushNeedDownBundle(_SkinArtresourcedata[i].getArtresources() + _strRes);
            }
            string[] ActArtresourceData = _Artresourcedata.getActionresource();
            for (int i = 0; i < ActArtresourceData.Length; ++i)
            {
                PushNeedDownBundle(ActArtresourceData[i] + _strRes);
            }

            SkillTemplate normalskill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(_hero.getNormalskill());
            for (int i = 0; i < normalskill.getBallIsticEffID().Length; i++)
            {
                PushNeedDownBundle(normalskill.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(normalskill.getUnderAttackEffID() + _strRes);

            SkillTemplate m_skill1 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(_hero.getSkill1ID());
            for (int i = 0; i < m_skill1.getBallIsticEffID().Length; i++)
            {
                PushNeedDownBundle(m_skill1.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(m_skill1.getUnderAttackEffID() + _strRes);
            SkillTemplate m_skill2 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(_hero.getSkill2ID());
            for (int i = 0; i < m_skill2.getBallIsticEffID().Length; i++)
            {
                Debug.Log(m_skill2.getBallIsticEffID()[i]);
                PushNeedDownBundle(m_skill2.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(m_skill2.getUnderAttackEffID() + _strRes);
        }
        private void IndexSkillShowSceneHeroModel()
        {
            IndexHeroAsset(GlobalMembers.SPELL_SHPW_TEAMMATE_ID);
            Dictionary<int, IExcelBean> _handBookjXmlData = DataTemplate.GetInstance().m_IllustratehandbookTable.getData();
            foreach (var item in _handBookjXmlData)
            {
                IllustratehandbookTemplate _handBookData = (IllustratehandbookTemplate)DataTemplate.GetInstance().m_IllustratehandbookTable.getTableData(item.Key);
                if (_handBookData.getType() == 1)
                {
                    IndexHeroAsset(_handBookData.getContentId());
                }
            }
        }

        //索引英雄模型资源
        private void IndexHeromodel()
        {
            if (SceneManager.Inst.NextloadScene.ToString().Contains("Battle"))//战斗场景加载阵型内英雄资源
            {
                for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT; ++i)
                {
                    int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
                    if (nGroup < 0 && nGroup >= GlobalMembers.MAX_MATRIX_COUNT)
                        continue;

                    X_GUID pMemberGuiD = ObjectSelf.GetInstance().Teams.m_Matrix[nGroup, i];
                    if (!pMemberGuiD.IsValid())
                        continue;
                    ObjectCard pHero = ObjectSelf.GetInstance().HeroContainerBag.FindHero(pMemberGuiD);
                    if (pHero == null)
                        continue;
                    int nTableID = pHero.GetHeroData().TableID;
                    HeroTemplate _rowData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(nTableID);
                    IndexModel(_rowData, true);
                    IndexSkillmodel(_rowData);
                    //IndexModel(_rowData);
                    //PushNeedDownBundle("HeroPlayer" + _strRes);
                    //IndexSkillmodel(_rowData);
                }
            }
            else if (SceneManager.Inst.NextloadScene.ToString().Equals("Login") == false)
            {
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
                //            IndexModel(_rowData, true);
                //            IndexSkillmodel(_rowData);
                //        }
                //    }
                //}
                //加载英雄背包中的英雄
                List<ObjectCard> _heroBagCardList = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
                int _heroBagCount = _heroBagCardList.Count;
                for (int i = 0; i < _heroBagCount; ++i)
                {
                    ObjectCard pHero = _heroBagCardList[i];
                    int nTableID = pHero.GetHeroData().TableID;
                    HeroTemplate _rowData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(nTableID);
                    IndexModel(_rowData, true);
                    IndexSkillmodel(_rowData);
                }
                PushNeedDownBundle("ElementBody02" + _strRes);
            }
            PushNeedDownBundle("Cloud" + _strRes);
        }
        //索引怪物模型资源
        private void IndexMonsterModel()
        {
            if (!SceneManager.Inst.NextloadScene.ToString().Contains("Battle"))
                return;

            CampaignMonsterGroupData pData = ObjectSelf.GetInstance().GetMonsterGroupData();
            if (pData != null)
            {
                for (int i = 0; i < GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP; i++)
                {
                    for (int j = 0; j < GlobalMembers.MAX_MONSTER_GROUP_COUNT; ++j)
                    {
                        if (DataTemplate.GetInstance().m_MonsterTable.tableContainsKey(pData.IDs[i, j]))
                        {
                            int nTableID = pData.IDs[i, j];
                            MonsterTemplate _rowData = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(nTableID);
                            IndexModel(_rowData);
                            IndexSkillmodel(_rowData);
                        }
                    }
                }
            }
        }
        //索引场景编辑器资源
        private void IndexSceneEditor()
        {
            int SceneID = 0;
            if (!SceneManager.Inst.NextloadScene.ToString().Contains("Battle"))
                return;
            if (ObjectSelf.GetInstance().GetIsPrompt())
            {
                SceneID = ObjectSelf.GetInstance().GetPromptCurCampaignID();
            }
            else
            {
                SceneID = ObjectSelf.GetInstance().GetCurCampaignID();
            }
            // Debug.Log(SceneManager.Inst.NextloadScene);
            StageTemplate _StageData = (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(SceneID);

#if UNITY_IPHONE
            string Scenedata = "AllFormation";
            PushNeedDownBundle(Scenedata + _strRes);
#else
            string Scenedata = "Formation100,Formation101,Formation102,Formation103,Formation110,Formation111,Formation112,Formation113,Formation120,Formation121,Formation122,Formation123,Formation130,Formation131,Formation132,Formation133,";
            string[] SceneData = Scenedata.Split(',');
            for (int i = 0; i < SceneData.Length; i++)
            {
                PushNeedDownBundle(SceneData[i] + _strRes);
            }
#endif
            for (int i = 0; i < _StageData.m_stageevent.Length; ++i)
            {
                if (_StageData.m_stageevent[i] != "-1")
                    m_NeedDownEditorBundle.Add(_StageData.m_stageevent[i] + _strRes);
            }
            for (int i = 0; i < _StageData.m_extraloadresource.Length; ++i)
            {
                if (_StageData.m_extraloadresource[i] != "-1")
                    PushNeedDownBundle(_StageData.m_extraloadresource[i] + _strRes);
            }
        }
        private void IndexModel(HeroTemplate hero, bool isHero = false)
        {
            ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate();
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getArtresources());
            PushNeedDownBundle(_Artresourcedata.getArtresources() + _strRes, isHero);
            ArtresourceTemplate[] _SkinArtresourcedata = new ArtresourceTemplate[hero.getUseableArtresource().Length];
            for (int i = 0; i < hero.getUseableArtresource().Length; ++i)
            {
                _SkinArtresourcedata[i] = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(hero.getUseableArtresource()[i]);
                if (_SkinArtresourcedata[i].getArtresources() == null)
                    continue;
                PushNeedDownBundle(_SkinArtresourcedata[i].getArtresources() + _strRes, isHero);
            }
            if (!SceneManager.Inst.NextloadScene.ToString().Contains("Battle"))
                return;
            string[] ActArtresourceData = _Artresourcedata.getActionresource();
            for (int i = 0; i < ActArtresourceData.Length; ++i)
            {
                PushNeedDownBundle(ActArtresourceData[i] + _strRes);
            }
        }
        private void IndexModel(MonsterTemplate monster)
        {
            ArtresourceTemplate _Artresourcedata = new ArtresourceTemplate();
            _Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(monster.getArtresources());
            PushNeedDownBundle(_Artresourcedata.getArtresources() + _strRes);
            string[] ActArtresourceData = _Artresourcedata.getActionresource();
            for (int i = 0; i < ActArtresourceData.Length; ++i)
            {
                PushNeedDownBundle(ActArtresourceData[i] + _strRes);
            }
        }
        private void IndexSkillmodel(HeroTemplate hero)
        {
            if (!SceneManager.Inst.NextloadScene.ToString().Contains("Battle"))
                return;
            SkillTemplate normalskill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(hero.getNormalskill());
            for (int i = 0; i < normalskill.getBallIsticEffID().Length; i++)
            {
                PushNeedDownBundle(normalskill.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(normalskill.getUnderAttackEffID() + _strRes);
            SkillTemplate m_skill1 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(hero.getSkill1ID());
            for (int i = 0; i < m_skill1.getBallIsticEffID().Length; i++)
            {
                PushNeedDownBundle(m_skill1.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(m_skill1.getUnderAttackEffID() + _strRes);
            SkillTemplate m_skill2 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(hero.getSkill2ID());
            for (int i = 0; i < m_skill2.getBallIsticEffID().Length; i++)
            {
                PushNeedDownBundle(m_skill2.getBallIsticEffID()[i] + _strRes);
            }
            PushNeedDownBundle(m_skill2.getUnderAttackEffID() + _strRes);
            // 被动技能暂未开放 [5/14/2015 Zmy]
            //             SkillTemplate m_skill3 = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.m_Data[hero.getSkill3ID()];
            //             for (int i = 0; i < m_skill3.getBallIsticEffID().Length; i++)
            //             {
            //                 PushNeedDownBundle(m_skill3.getBallIsticEffID()[i] + _strRes);
            //             } 
            //             PushNeedDownBundle(m_skill3.getUnderAttackEffID() + _strRes);
        }
        private void IndexSkillmodel(MonsterTemplate monster)
        {
            SkillTemplate normalskill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(monster.getNormalattack());
            PushNeedDownBundle(normalskill.getBallIsticEffID() + _strRes);
            PushNeedDownBundle(normalskill.getUnderAttackEffID() + _strRes);
            SkillTemplate[] m_skill = new SkillTemplate[monster.getMonsterskill().Length];
            for (int i = 0; i < monster.getMonsterskill().Length; ++i)
            {
                if (monster.getMonsterskill()[i] == -1)
                    return;
                m_skill[i] = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(monster.getMonsterskill()[i]);
                PushNeedDownBundle(m_skill[i].getBallIsticEffID() + _strRes);
                PushNeedDownBundle(m_skill[i].getUnderAttackEffID() + _strRes);
            }
        }

        //push 动画assets
        private void PushNeedDownAnimationBundle(string strRes)
        {
            string _tmpRes = strRes.Substring(0, strRes.LastIndexOf('.'));
            // Debug.Log("_tmpRes " + _tmpRes);
            if (_tmpRes == string.Empty)
                return;
            string _tempAniRes = _tmpRes.Substring(0, _tmpRes.Length - 2);
            string _aniRes = _tempAniRes + _strAnimation + _strRes;
            if (m_NeedDownHeroAnimationBundle.Contains(_aniRes) == false)
            {
                if (m_AssetHeroAnimactionClip.ContainsKey(_aniRes) == false)
                {
                    m_NeedDownHeroAnimationBundle.Add(_aniRes);
                }
            }
        }

        private void PushNeedDownBundle(string strRes, bool isHero = false)
        {
            if (isHero)
            {
                PushNeedDownAnimationBundle(strRes);
                if (m_NeedDownHeroBundle.Contains(strRes) == false)
                {
                    string _tmpRes = strRes.Substring(0, strRes.LastIndexOf('.'));
                    if (m_AssetHeroRes.ContainsKey(_tmpRes) == false)
                    {
                        m_NeedDownHeroBundle.Add(strRes);
                    }
                }
            }
            else
            {
                PushNeedDownAnimationBundle(strRes);
                if (m_NeedDownBundle.Contains(strRes) == false)
                {
                    string _tmpRes = strRes.Substring(0, strRes.LastIndexOf('.'));
                    if (m_AssetDynamicBundle.ContainsKey(_tmpRes) == false && m_AssetHeroRes.ContainsKey(_tmpRes) == false)
                    {
                        m_NeedDownBundle.Add(strRes);
                    }
                }
            }

        }


        /// </summary>
        private void OnClearUp()
        {
            foreach (KeyValuePair<string, GameObject> item in m_AssetDynamicBundle)
            {
                DestroyImmediate(item.Value, true);
            }
            m_AssetDynamicBundle.Clear();

            foreach (KeyValuePair<string, UnityEngine.Object> item in m_EditorAssetDynamicBundle)
            {
                DestroyImmediate(item.Value, true);
            }
            m_EditorAssetDynamicBundle.Clear();

            foreach (KeyValuePair<string, Sprite> item in m_AssetDynamicSprite)
            {
                DestroyImmediate(item.Value, true);
            }
            m_AssetDynamicSprite.Clear();

            foreach (KeyValuePair<string, Sprite> item in m_AssetIconSprite)
            {
                DestroyImmediate(item.Value, true);
            }
            m_AssetIconSprite.Clear();
            m_NeedDownBundle.Clear();
            m_NeedDownHeroBundle.Clear();
            m_NeedDownHeroAnimationBundle.Clear();
            m_bIsReadyOK = false;

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        private void DownloadShopBundle(string name)
        {
            StartCoroutine(DownloadShopAssedBundle(name + _strRes));
        }

        IEnumerator DownloadShopAssedBundle(string name)
        {
            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);

            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }

            byte[] decryptedData = AssetManager.Inst.ExecuteDecrypt(name, www.bytes);

            AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
            yield return acr;

            m_ShopAssetList = new List<Sprite>();

            AssetBundle bundle = acr.assetBundle;
            object[] ListSprite = bundle.LoadAll(typeof(UnityEngine.Sprite));
            Sprite sprite = null;
            int size = ListSprite.Length;
            for (int i = 0; i < size; i++)
            {
                sprite = ListSprite[i] as Sprite;
                if (sprite != null && !m_ShopAssetList.Contains(sprite))
                {
                    m_ShopAssetList.Add(sprite);
                }
            }
            bundle.Unload(false);
            www.Dispose();

            GameEventDispatcher.Inst.dispatchEvent(GameEventID.UI_ShopAdAssetDownload);
        }

        void CheckSendResOkEvent()
        {
            if (m_bIsHeroReadyOK && m_bIsReadyOK)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Scene_ResOK);
            }
        }

        private void DownLoadHeroAnimationBundle()
        {
            //if (m_NeedDownHeroAnimationBundle.Count == 0 && m_NeedDownBundle.Count == 0 && m_NeedDownEditorBundle.Count == 0 && m_NeedDownHeroBundle.Count == 0)
            //{
            //    m_bIsReadyOK = true;
            //    GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Scene_ResOK);
            //    return;
            //}
            if (m_NeedDownHeroAnimationBundle.Count == 0)
            {
                //下载完人物动画才开始下载人物模型
                DownLoadAssetBundle();
                DownLoadHeroBundle();
                return;
            }

            string bundleName = m_NeedDownHeroAnimationBundle[0];
            // Debug.Log("m_NeedDownHeroAnimationBundle bundleName" + bundleName);
            m_NeedDownHeroAnimationBundle.RemoveAt(0);

            //本地资源列表如果没有此资源。重启递归往下加载
            if (AssetManager.Inst.LocalResVersion.ContainsKey(bundleName) == false)
            {
                DownLoadHeroAnimationBundle();
            }
            else
            {
                StartCoroutine(this.DowningAnimationClipBundle(bundleName, delegate(WWW w)
                {
                    DownLoadHeroAnimationBundle();
                }, true));
            }
        }

        private void DownLoadHeroBundle()
        {
            if (m_NeedDownBundle.Count == 0 && m_NeedDownEditorBundle.Count == 0 && m_NeedDownHeroBundle.Count == 0)
            {
                m_bIsReadyOK = true;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Scene_ResOK);
                return;
            }
            if (m_NeedDownHeroBundle.Count == 0)
                return;

            string bundleName = m_NeedDownHeroBundle[0];
            m_NeedDownHeroBundle.RemoveAt(0);

            //本地资源列表如果没有此资源。重启递归往下加载
            if (AssetManager.Inst.LocalResVersion.ContainsKey(bundleName) == false)
            {
                DownLoadHeroBundle();
            }
            else
            {
                StartCoroutine(this.DowningBundle(bundleName, delegate(WWW w)
                {
                    DownLoadHeroBundle();
                }, true));
            }
        }
        private void DownLoadAssetBundle()
        {
            if (m_NeedDownHeroAnimationBundle.Count == 0 && m_NeedDownBundle.Count == 0 && m_NeedDownEditorBundle.Count == 0 && m_NeedDownHeroBundle.Count == 0)
            {
                m_bIsReadyOK = true;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Scene_ResOK);
                return;
            }
            if (m_NeedDownBundle.Count == 0)
                return;

            string bundleName = m_NeedDownBundle[0];
            m_NeedDownBundle.RemoveAt(0);

            //本地资源列表如果没有此资源。重启递归往下加载
            if (AssetManager.Inst.LocalResVersion.ContainsKey(bundleName) == false)
            {
                DownLoadAssetBundle();
            }
            else
            {
                StartCoroutine(this.DowningBundle(bundleName, delegate(WWW w)
                {
                    DownLoadAssetBundle();
                }));
            }
        }
        IEnumerator DowningEditorBundle(string name, HandleFinishDownLoad finishFun)
        {

            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);

            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }
            byte[] decryptedData = AssetManager.Inst.ExecuteDecrypt(name, www.bytes);

            AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
            yield return acr;
            AssetBundle bundle = acr.assetBundle;
            if (bundle == null)
            {
                Debug.LogError("!!!Error: DowningEditorBundle() bundle is null:" + name);
                yield return 1;
            }

            object[] EditorList = bundle.LoadAll(typeof(UnityEngine.Object));
            UnityEngine.Object go = null;
            for (int i = 0; i < EditorList.Length; i++)
            {
                go = EditorList[i] as UnityEngine.Object;
                if (go != null && m_EditorAssetDynamicBundle.ContainsKey(go.name) == false)
                {
                    m_EditorAssetDynamicBundle.Add(go.name, go);
                }
            }
            bundle.Unload(false);
            www.Dispose();
            if (finishFun != null)
            {
                LogManager.LogToFile("下载完成！！！！！！！！Path：" + localPath.ToString());
                finishFun(www);
            }
        }
        IEnumerator DowningBundle(string name, HandleFinishDownLoad finishFun, bool isHero = false)
        {
            //Debug.Log("DowningBundle name " + name);
            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);

            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }




            byte[] decryptedData = AssetManager.Inst.ExecuteDecrypt(name, www.bytes);

            AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
            yield return acr;

            AssetBundle bundle = acr.assetBundle;
            if (bundle == null)
            {
                Debug.LogError("!!!Error: DowningBundle() bundle is null:" + name);
                yield return 1;
            }

            object[] List = new object[] { };
            try
            {
                List = bundle.LoadAll(typeof(UnityEngine.GameObject));
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError("!!!Error: DowningBundle() bundle loadall faild:" + name);
            }
            GameObject go = null;
            int _leng = List.Length;
            for (int i = 0; i < _leng; i++)
            {
                go = List[i] as GameObject;
                if (isHero)
                {
                    if (go != null && m_AssetHeroRes.ContainsKey(go.name) == false)
                    {
                        if (go.name.Contains("_FBX"))
                        {
                            continue;
                        }
                        //增加动作判断
                        string _tempHeroName = go.name.Substring(0, go.name.Length - 2);
                        List<AnimationClip> clipList = getHeroAnimationClipListByName(_tempHeroName + _strAnimation);
                        if (clipList != null)
                        {
                            SetAnimation(go, clipList, false);
                        }
                        m_AssetHeroRes.Add(go.name, go);
                        LogManager.Log(go.name + "下载完成！！！！！！！！Path：" + localPath.ToString());
                    }
                }
                else
                {
                    if (go != null && m_AssetDynamicBundle.ContainsKey(go.name) == false)
                    {
                        if (go.name.Contains("_FBX"))
                        {
                            continue;
                        }
                        string _tempHeroName = go.name.Substring(0, go.name.Length - 2);
                        List<AnimationClip> clipList = getHeroAnimationClipListByName(_tempHeroName + _strAnimation);
                        if (clipList != null)
                        {
                            SetAnimation(go , clipList , false);
                        }
                        m_AssetDynamicBundle.Add(go.name, go);
                        LogManager.Log(go.name + "下载完成！！！！！！！！Path：" + localPath.ToString());
                    }
                }
            }
            bundle.Unload(false);
            www.Dispose();
            if (finishFun != null)
            {
                finishFun(www);
            }
        }

        private delegate void HandleFinishDownLoad(WWW www);

        //加载编辑器资源
        private void DownLoadEditorBundle()
        {
            if (m_NeedDownHeroAnimationBundle.Count == 0 && m_NeedDownBundle.Count == 0 && m_NeedDownEditorBundle.Count == 0 && m_NeedDownHeroBundle.Count == 0)
            {
                m_bIsReadyOK = true;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Scene_ResOK);
                return;
            }
            if (m_NeedDownEditorBundle.Count == 0)
                return;

            string bundleName = m_NeedDownEditorBundle[0];
            m_NeedDownEditorBundle.RemoveAt(0);

            //Debug.Log("bundleName" + bundleName);
            //本地资源列表如果没有此资源。重启递归往下加载
            if (AssetManager.Inst.LocalResVersion.ContainsKey(bundleName) == false)
            {
                DownLoadEditorBundle();
            }
            else
            {
                StartCoroutine(this.DowningEditorBundle(bundleName, delegate(WWW w)
                {
                    DownLoadEditorBundle();
                }));
            }
        }
        private void DownLoadEternalBundle()
        {
            if (isEternalBundleReady)
                return;

            int size = m_NeedDownEternalBundle.Count;
            for (int i = 0; i < size; ++i)
            {
                string name = m_NeedDownEternalBundle[i];
                if (AssetManager.Inst.LocalResVersion.ContainsKey(name) == false)
                {
                    continue;
                }
                else
                {
                    StartCoroutine(DowningEternalBundle(name, null));
                }
            }
        }

        IEnumerator DowningEternalBundle(string name, HandleFinishDownLoad finishFun)
        {
            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);

            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }

            byte[] decryptedData = AssetManager.Inst.ExecuteDecrypt(name, www.bytes);

            AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
            yield return acr;

            AssetBundle bundle = acr.assetBundle;

            object[] ListSprite = bundle.LoadAll(typeof(UnityEngine.Sprite));
            Sprite sprite = null;
            int size = ListSprite.Length;
            for (int i = 0; i < size; i++)
            {
                sprite = ListSprite[i] as Sprite;
                if (sprite != null && m_AssetEternalSprite.ContainsKey(sprite.name) == false)
                {
                    m_AssetEternalSprite.Add(sprite.name, sprite);
                    if (m_AssetEternalSprite.Count == m_NeedDownEternalBundle.Count)
                    {
                        isEternalBundleReady = true;
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_EternalSpriteLoaded);
                    }
                }
            }

            bundle.Unload(false);
            www.Dispose();
            if (finishFun != null)
            {
                LogManager.LogToFile("下载完成！！！！！！！！Path：" + localPath.ToString());
                finishFun(www);
            }
            LogManager.LogToFile("下载完成！！！！！！！！Path：" + localPath.ToString());
        }

        IEnumerator DowningAnimationClipBundle(string name, HandleFinishDownLoad finishFun, bool isHero = false)
        {
            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);

            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }

            byte[] decryptedData = AssetManager.Inst.ExecuteDecrypt(name, www.bytes);

            AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
            yield return acr;

            AssetBundle bundle = acr.assetBundle;
            if (bundle == null)
            {
                Debug.LogError("!!!Error: DowningBundle() bundle is null:" + name);
                yield return 1;
            }

            object[] List = new object[] { };

            try
            {
                List = bundle.LoadAll(typeof(UnityEngine.AnimationClip));
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError("!!!Error: DowningBundle() bundle loadall faild:" + name);
            }

            List<AnimationClip> clipList = new List<AnimationClip>();

            int _leng = List.Length;
            for (int i = 0; i < _leng; i++)
            {
                AnimationClip clip = List[i] as AnimationClip;
                clipList.Add(clip);
            }
            //Debug.Log("nameClipList = " + name);
            m_AssetHeroAnimactionClip.Add(name, clipList);

            //增加判断是否有该动画bundle未设置动画的英雄模型
            string[] _tempHeroName = name.Split('_');
            List<GameObject> _heroResList = getHeroResListByName(_tempHeroName[0]);
            for (int i = 0; i < _heroResList.Count; i++)
            {
                SetAnimation(_heroResList[i], clipList, true);
            }

            LogManager.Log(name + "下载完成！！！！！！！！Path：" + localPath.ToString());
            bundle.Unload(false);
            www.Dispose();
            if (finishFun != null)
            {
                finishFun(www);
            }
        }


        //获取英雄动画列表
        private List<AnimationClip> getHeroAnimationClipListByName(string name)
        {
            List<AnimationClip> listClip = null;
            foreach (KeyValuePair<string, List<AnimationClip>> aniClip in m_AssetHeroAnimactionClip)
            {
                if (aniClip.Key.Contains(name))
                    return aniClip.Value;
            }

            return listClip;
        }



        //获取已下载的英雄模型
        private List<GameObject> getHeroResListByName(string name)
        {
            List<GameObject> goList = new List<GameObject>();
            foreach (KeyValuePair<string, GameObject> _heroRes in m_AssetHeroRes)
            {
                if (_heroRes.Key.Contains(name))
                {
                    goList.Add(_heroRes.Value);
                }
            }
            return goList;
        }

        /// <summary>
        /// 下载好bundle设置动画 
        /// </summary>
        /// <param name="go">需要添加动画的gameobject</param>
        /// <param name="clipList">需要添加的动画列表</param>
        /// <param name="isUpdateHeroResDic">是否更新HeroRes资源，只有在下载动画的时候判断，是否有未设置动画的hero</param>
        public void SetAnimation(GameObject go , List<AnimationClip> clipList , bool isUpdateHeroResDic)
        {
            Animation ani = go.GetComponent<Animation>();
            if (ani == null)
            {
                ani = go.AddComponent<Animation>();
                for (int k = 0; k < clipList.Count; k++)
                {
                    ani.AddClip(clipList[k], clipList[k].name);
                    if (clipList[k].name == "Nidle1")
                    {
                        ani.clip = clipList[k];
                    }
                }

                if (ani.clip == null)
                {
                    Debug.LogError("Error: " + go.name + " not Nidle1 Animation");
                }
                ani.playAutomatically = true;
                ani.animatePhysics = false;
                ani.cullingType = AnimationCullingType.BasedOnRenderers;

                if (clipList.Count != ani.GetClipCount())
                {
                    Debug.LogError("Animation is not all setting : " + go);
                }
                if (isUpdateHeroResDic)
                {
                    m_AssetHeroRes[go.name] = go;
                }
            }
        }
    }
}
