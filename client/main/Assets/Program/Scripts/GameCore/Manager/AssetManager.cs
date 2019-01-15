using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.UI;
using Platform;
using DreamFaction.UI.Core;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// 资源管理器，供初始化启动时，对资源进行校验并下载，缓存公共资源部分的bundle信息，通过AssetLoader进行选择性加载缓存。
    /// </summary>
    public class AssetManager : BaseControler
    {
        // 单例
        private static AssetManager _inst;
        // 客户端资源是否准备就绪，需要先进行资源检查；
        private static bool m_clientAllAseetsOK;
        // 本地资MD5校验是否OK
        private bool isMD5OK;
        // 与服务器资源是否已经同步
        private bool isSyncServerResOK;
        //与服务器资源是否已经比较完毕
        private bool m_ComparisonVersionOver;

        //资源列表文件
        private string VERSION_FILE;
        //服务器下载地址
        private string SERVER_RES_URL;
        //服务器.bin文件下载地址
        private string SERVER_BIN_RES_URL;
        //本地资源路径
        private string LOCAL_RES_PATH;
        //本地资源列表
        private Dictionary<string, string> m_LocalResVersion;
        //服务器资源列表
        private Dictionary<string, string> m_ServerResVersion;
        //服务器资源大小列表
        private Dictionary<string, long> m_ServerResSize;
        //数值表列表
        private List<string> TableDataFiles;
        //读取的本地MD5中的FileName列表
        List<string> MD5NodeFile;


        private Dictionary<string, GameObject> m_AssetPlayerBundle;
        private Dictionary<string, GameObject> m_AssetMonsterBundle;
        private Dictionary<string, GameObject> m_AssetBossBundlu;
        public List<string> NeedDownFiles;
        private bool NeedUpdateLocalVersionFile = false;
        private bool bIsFirstEnter = false;
        private float delay = 0.0f;
        private float donwLoadSpeed = 0;//下载速度
        private int downbytes = 0;//下载的字节数
        private long NeedLoadFileSize;//需要下载文件大小

        XmlDocument m_XmlDoc = new XmlDocument();
        public int NeedDownFilesCount;
        public int CurrentDownFilesCount;
        
        // ==================================== 公共属性(限制外部修改) =====================================
        public static AssetManager Inst
        {
            get
            {
                return _inst;
            }
        }

        public static bool ClientAllAseetsOK
        {
            get
            {
                return m_clientAllAseetsOK;
            }
        }

        public bool ComparisonVersionOver
        {
            get
            {
                return m_ComparisonVersionOver;
            }
        }

        public float DonwLoadSpeed
        {
            get
            {
                return donwLoadSpeed;
            }
        }
        public Dictionary<string, string> LocalResVersion
        {
            get
            {
                return m_LocalResVersion;
            }
        }

        // ==================================== 继承接口 =====================================

        void OnResourcesAllOK()
        {
            UpdateLocalVersionFile();
        }

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

            VERSION_FILE = "VersionMD5.xml";
            string versionNum = ConfigsManager.Inst.GetClientConfig(ClientConfigs.Version);


// #if UNITY_ANDROID
//              SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
//                                      "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/Android/"
//                                     : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/Android/";
//                     SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
//                                      "file://" + AppManager.Inst.readOnlyPath + "clientxml2/"
//                                     : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
// #elif UNITY_IPHONE
//              SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
//                                      "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/IOS/"
//                                     : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/IOS/";
//                     SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
//                                      "file://" + AppManager.Inst.readOnlyPath +"clientxml2/"
//                                     : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
// #elif UNITY_STANDALONE_WIN
//                 SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
//                      "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/PC/"
//                     : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/PC/";
//                 SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
//                                  "file://" + AppManager.Inst.readOnlyPath + "clientxml2/"
//                                 : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
// #else 
                        // 根据不同平台进行初始化
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/Android/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/Android/";
                    SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "clientxml2/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/IOS/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/IOS/";
                    SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath +"clientxml2/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
                    break;
                default:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/PC/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/AssetBundle/PC/";
                    SERVER_BIN_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "clientxml2/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/clientxml/";
                    break;
            }
//#endif


            LOCAL_RES_PATH = AppManager.Inst.readAndWritePath;
            m_clientAllAseetsOK = false;
            isMD5OK = false;
            isSyncServerResOK = false;
            m_LocalResVersion = new Dictionary<string, string>();
            m_ServerResVersion = new Dictionary<string, string>();
            m_AssetPlayerBundle = new Dictionary<string, GameObject>();
            m_AssetMonsterBundle = new Dictionary<string, GameObject>();
            m_AssetBossBundlu = new Dictionary<string, GameObject>();
            NeedDownFiles = new List<string>();
            m_ServerResSize = new Dictionary<string, long>();
            TableDataFiles = new List<string>();
            MD5NodeFile = new List<string>();
        }
        
        protected override void UpdateData()
        {
            if (isMD5OK && isSyncServerResOK)
                m_clientAllAseetsOK = true;

        }

        protected override void DestroyData()
        {
            _inst = null;
        }

        // ==================================== 公共接口 =====================================

        public GameObject GetResPlayer(string name)
        {
            GameObject go = null;
            if (m_AssetPlayerBundle.TryGetValue(name, out go) == false)
            {
                LogManager.Log("!!!!!!error : ResPlayer does not exist ! GetResPlayer is failed Name:" + name);
            }

            return go;
        }

        public GameObject GetResMonster(string name)
        {
            GameObject go = null;
            if (m_AssetMonsterBundle.TryGetValue(name, out go) == false)
            {
                LogManager.Log("!!!!!!error : ResMonster does not exist ! GetResMonster is failed Name:" + name);
            }

            return go;
        }

        public GameObject GetResBoss(string name)
        {
            GameObject go = null;
            if (m_AssetBossBundlu.TryGetValue(name, out go) == false)
            {
                LogManager.Log("!!!!!!error : ResBoss does not exist ! GetResBoss is failed Name:" + name);
            }

            return go;
        }
        /// <summary>
        /// 资源bundle解密
        /// </summary>
        /// <param name="filename">资源名称</param>
        /// <param name="fileData">bundle读取后二进制信息</param>
        /// <returns></returns>
        public byte[] ExecuteDecrypt(string filename, byte[] fileData)
        {
            byte[] buff = new byte[(int)fileData.Length - 16 - 3];//不可修改
            for (int i = 16; i < fileData.Length - 3; i++)
            {
                //与加密算法相逆的运算~ 修改要同步！
                buff[i - 16] = fileData[i];
                buff[i - 16] -= 1;
            }

            //ReplaceLocalRes(filename, buff);
            return buff;
        }

        /// <summary>
        /// 初始化资源检查
        /// </summary>
        public void CheckAssetVer()
        {
            if (Application.genuineCheckAvailable)
            {
                // 这个属性暂时不确定触发的系统环境以及触发条件，暂时先打印一条LOG
                if (!Application.genuine)
                    LogManager.LogError("游戏客户端发布后被修改过！");
            }
            // 1: 本地资源检查,MD5检查
            isMD5OK = CheckClientMD5();
            if (!isMD5OK)
            {
                QuiteGameByAssetProblem("客户端资源MD5码校验有问题,终止游戏！");
            }
            // 2: 检查服务器资源是否有更新，是否需要下载,如果需要则通过www下载资源。
            CheckServerRes();

            // 3: 读取加载CSV表数据
            //DataTemplate.GetInstance().Init();
        }
        /// <summary>
        /// 加载需要跟新的资源
        /// </summary>
        public void StartLoadAsset()
        {
            IterativLocalRes();
        }

        public string GetNeedLoadFileSize()
        {
            if (NeedLoadFileSize > 1024 * 1024)
            {
                long _size = NeedLoadFileSize / 1024 / 1024;
                return _size.ToString("0.0") + "MB";
            }
            else
            {
                if (NeedLoadFileSize > 1024)
                {
                    long _size = NeedLoadFileSize / 1024;
                    return _size.ToString("0.0") + "KB";
                }
                else
                {
                    long _size = NeedLoadFileSize;
                    return _size.ToString("0.0") + "B";
                }             
            }
        }
        //委托加载完成回调事件
        public delegate void HandleFinishDownLoad(WWW www, AssetBundle bundle);


        // ==================================== 私有接口 =====================================

        // 终止游戏，某部分资源有问题
        private void QuiteGameByAssetProblem(string erro)
        {
            LogManager.LogError(erro);
            Application.Quit();
        }
        // 检查服务器资源是否有更新，是否需要下载
        private void CheckServerRes()
        {
            // 1: 从服务器下载资源更新文件，与本地资源进行比较
            IterativServerLoad();
        }

        // 本地资源检查,MD5检查
        private bool CheckClientMD5()
        {
            //首先查看本地是否存在version.xml文件。如果没有的话，创建一个空的文件。
            if (File.Exists(LOCAL_RES_PATH + VERSION_FILE) == false)
            {
                FileStream verFile = new FileStream(LOCAL_RES_PATH + VERSION_FILE, FileMode.Create);
                verFile.Flush();
                verFile.Close();
                LogManager.Log("Version.txt file Success Created！Path：" + LOCAL_RES_PATH + VERSION_FILE);

                //标记如果是第一次创建空文件的把。在下面就无需多一次IO删除文件的操作
                bIsFirstEnter = true;
            }
            else
            {
                ReadLocalVersionXML(m_LocalResVersion);
            }
            return true;
        }

        //迭代下载服务器资源[9/29/2014 Zmy]
        private void IterativServerLoad()
        {
            //確保本地资源列表文件存在的情况,执行以下顺序：
            //加载本地verisnon.txt并读取存储内容 -> 
            //下载服务器version.txt并读取存储内容 -> 
            //与本地的version进行比较，筛选需要更新和新增的资源 ->
            //依次下载更新资源到本地路径，如果本地已有旧资源，则替换只，否则新建保存文件-> 
            //下载完成后更新本地的VersionMD5.xml，确保下次启动不会重复下载更新 over[9/29/2014 Zmy]

            StartCoroutine(this.DownLoad(SERVER_RES_URL , VERSION_FILE, delegate(WWW serverVersion, AssetBundle bundle)
            {
                //读取服务器并保存version
                ParseVersionFile(serverVersion.text, m_ServerResVersion);

                //计算差异，得到需要重新加载的资源
                CompareVersion();

                //加载需要更新的资源[先注释下8/18 yao]
                //IterativLocalRes();

            }));
        }

        //读取本地资源列表文件
        private void ReadLocalVersionXML(Dictionary<string, string> dict)
        {
            XmlDocument XmlDoc = new XmlDocument();

            //如果是空的XML说明加载有问题，或者本地资源遭到破坏

            FileInfo fi = new FileInfo(@LOCAL_RES_PATH + VERSION_FILE);
            if (fi.Length == 0)
            {
                File.Delete(LOCAL_RES_PATH + VERSION_FILE);
                CheckClientMD5();
                return;
            }
 
            XmlDoc.Load(LOCAL_RES_PATH + VERSION_FILE);
            XmlElement XmlRoot = XmlDoc.DocumentElement;

            foreach (XmlNode node in XmlRoot.ChildNodes)
            {
                if ((node is XmlElement) == false)
                    continue;

                string file = (node as XmlElement).GetAttribute("FileName");
                string md5 = (node as XmlElement).GetAttribute("MD5");

                if (dict.ContainsKey(file) == false)
                {
                    dict.Add(file, md5);
                }
            }

            XmlRoot = null;
            XmlDoc = null;
        }

        //解析本地资源文件列表 [9/29/2014 Zmy]
        private void ParseVersionFile(string content, Dictionary<string, string> dict)
        {
            if (content == null || content.Length == 0)
            {
                return;
            }

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(content);
            XmlElement XmlRoot = XmlDoc.DocumentElement;

            foreach (XmlNode node in XmlRoot.ChildNodes)
            {
                if ((node is XmlElement) == false)
                    continue;

                string file = (node as XmlElement).GetAttribute("FileName");
                string md5 = (node as XmlElement).GetAttribute("MD5");
                long size = long.Parse((node as XmlElement).GetAttribute("FileSize"));

                if (dict.ContainsKey(file) == false)
                {
                    dict.Add(file, md5);
                    m_ServerResSize.Add(file, size);
                }
            }

            XmlRoot = null;
            XmlDoc = null;
        }

        //计算本地资源文件与服务器资源文件的差异 [9/29/2014 Zmy]
        private void CompareVersion()
        {
            foreach (var version in m_ServerResVersion)
            {
                string fileName = version.Key;
                string serverMd5 = version.Value;

                if (m_LocalResVersion.ContainsKey(fileName) == false)
                {
                    NeedDownFiles.Add(fileName);
                    NeedLoadFileSize += m_ServerResSize[fileName];
                }
                else
                {
                    //需要替换的资源
                    //本地已有的资源，对比md5值来确定是否需要重新下载
                    string localMd5;
                    m_LocalResVersion.TryGetValue(fileName, out localMd5);
                    if (!serverMd5.Equals(localMd5))
                    {
                        NeedDownFiles.Add(fileName);
                        NeedLoadFileSize += m_ServerResSize[fileName];
                    }
                }
            }

            //校验公共资源包，初始化时及保存好全部的GameObject
            //CheckPublicBundleRes();

            NeedUpdateLocalVersionFile = NeedDownFiles.Count > 0;
            NeedDownFilesCount = NeedDownFiles.Count;
            m_ComparisonVersionOver = true;

            //UI_LoginControler.Inst.ComparisonVersionNum();
        }

        //递归依次加载本地需要更新的资源 [9/29/2014 Zmy]
        private void IterativLocalRes()
        {
            CurrentDownFilesCount = NeedDownFiles.Count;
            if (NeedDownFiles.Count == 0)
            {
                ReadLocalBinData();//资源加载Ok 加载表数据
                //资源已全部下载到本地就绪
                //UpdateLocalVersionFile();

                OnResourcesAllOK();              
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Clent_ResOK);

                return;
            }

            string file = NeedDownFiles[0];
            NeedDownFiles.RemoveAt(0);

            StartCoroutine(this.DownLoad(SERVER_RES_URL , file, delegate(WWW w, AssetBundle bundle)
            {
                if (file.Equals("Player.enc") ||
                    file.Equals("Monster.enc") ||
                    file.Equals("Boss.enc"))
                {
                    //InitPublicAssetInfo(w, bundle, file);
                }
                else if (file.Contains("_csv.assetbundle"))
                {
                    //DataTemplate.Inst.InitCSV(w);//解析csv表，执行多次
                }
                else
                {
                    //将下载的资源替换本地就的资源  
                    //因为上面的bundle 每次启动游戏的时候都需要从服务器获取一次。和本地是否有文件已经没关联，所以无需再进行生成文件的操作
                    ReplaceLocalRes(file, w.bytes);
                }
                IterativLocalRes();
            }));
        }

        //对下载的ab进行一次解析分类，初始化一下那些公共资源包
        private void InitPublicAssetInfo(WWW www, AssetBundle bundle, string fileName)
        {
            if (!www.isDone || www.error != null)
                return;

            //object[] objList = bundle.LoadAll(typeof(UnityEngine.GameObject));
            //后续按需求重新对公共包进行初始化分类

            //因为在计算需要更新的文件差异的时候，已经将public包强制加入进去了。这里直接区分不同公共包存储即可
            //        foreach (UnityEngine.GameObject item in objList)
            //        {
            //             if (fileName.Equals(GamePlayerType.Player.ToString() + ".enc"))
            //             {
            //                 if (dicAssetPlayer.ContainsKey(item.name) == false)
            //                 {
            //                     dicAssetPlayer.Add(item.name, item);
            //                 }
            //             }
            //             else if (fileName.Equals(GamePlayerType.Monster.ToString() + ".enc"))
            //             {
            //                 if (dicAssetMonster.ContainsKey(item.name) == false)
            //                 {
            //                     dicAssetMonster.Add(item.name, item);
            //                 }
            //             }
            //             else if (fileName.Equals(GamePlayerType.Boss.ToString() + ".enc"))
            //             {
            //                 if (dicAssetBoss.ContainsKey(item.name) == false)
            //                 {
            //                     dicAssetBoss.Add(item.name, item);
            //                 }
            //             }
            //        }

            bundle.Unload(false);
        }


        //在启动初始化资源对比的时候，对几个特定公共bundle包进行校验
        //在此次更新文件中如果不存在，强制从服务器下载，后面只通过一次www下载资源
        //获取并存储bundle内的GameObject，生命周期为整个游戏过程，方便不同场景内可以快速随时实例化
        //PS:公共包按需求规划，后续有新的包手动添加公共包的名称
        private void CheckPublicBundleRes()
        {
            //         if (LocalResVersion.ContainsKey(GamePlayerType.Player.ToString() + AppManager.Inst.extensionName) == false ||
            //             NeedDownFiles.Contains(GamePlayerType.Player.ToString() + AppManager.Inst.extensionName) == false)
            //         {
            //             NeedDownFiles.Add(GamePlayerType.Player.ToString() + AppManager.Inst.extensionName);
            //         }
            // 
            //         if (LocalResVersion.ContainsKey(GamePlayerType.Monster.ToString() + AppManager.Inst.extensionName) == false ||
            //             NeedDownFiles.Contains(GamePlayerType.Monster.ToString() + AppManager.Inst.extensionName) == false)
            //         {
            //             NeedDownFiles.Add(GamePlayerType.Monster.ToString() + AppManager.Inst.extensionName);
            //         }
            // 
            //         if (LocalResVersion.ContainsKey(GamePlayerType.Boss.ToString() + AppManager.Inst.extensionName) == false ||
            //             NeedDownFiles.Contains(GamePlayerType.Boss.ToString() + AppManager.Inst.extensionName) == false)
            //         {
            //             NeedDownFiles.Add(GamePlayerType.Boss.ToString() + AppManager.Inst.extensionName);
            //         }

            //CheckCSV();
        }
        
        //更新本地资源列表文件 [9/29/2014 Zmy]
        private void UpdateLocalVersionFile()
        {
            if (NeedUpdateLocalVersionFile)
            {
                //如果有文件更新并且本地不是空文件，删除本地的旧资源列表文件。重新生成一份新的服务器资料列表到本地
                if (File.Exists(LOCAL_RES_PATH + VERSION_FILE) && !bIsFirstEnter)
                {
                    System.IO.File.Delete(LOCAL_RES_PATH + VERSION_FILE);
                }
                m_LocalResVersion.Clear();

                XmlDocument XmlDoc = new XmlDocument();
                XmlElement XmlRoot = XmlDoc.CreateElement("Files");
                XmlDoc.AppendChild(XmlRoot);
                foreach (KeyValuePair<string, string> pair in m_ServerResVersion)
                {
                    XmlElement xmlElem = XmlDoc.CreateElement("File");
                    XmlRoot.AppendChild(xmlElem);

                    xmlElem.SetAttribute("FileName", pair.Key);
                    xmlElem.SetAttribute("MD5", pair.Value);

                    //更新本地存储的资源列表信息
                    m_LocalResVersion.Add(pair.Key.ToString(), pair.Value.ToString());
                }
                XmlDoc.Save(LOCAL_RES_PATH + VERSION_FILE);
                XmlDoc = null;
            }
        }

        //更新本地资源列表文件 [6/11/2015 Yao]
        private void UpdateLocalVersionFile(string name, string key)
        {

            if (File.Exists(LOCAL_RES_PATH + VERSION_FILE))
            {

                FileStream fs = new FileStream(@LOCAL_RES_PATH + VERSION_FILE, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                if (sr.ReadToEnd() == string.Empty)
                {
                    sr.Close();

                    XmlElement XmlRoot = m_XmlDoc.CreateElement("Files");
                    m_XmlDoc.AppendChild(XmlRoot);
                    XmlElement xmlElem = m_XmlDoc.CreateElement("File");
                    XmlRoot.AppendChild(xmlElem);

                    xmlElem.SetAttribute("FileName", name);
                    xmlElem.SetAttribute("MD5", key);

                    //更新本地存储的资源列表信息
                    if (m_LocalResVersion.ContainsKey(name) == false)
                    {
                        m_LocalResVersion.Add(name, key);
                    }
                    else
                    {
                        m_LocalResVersion[name] = key;
                    }

                    m_XmlDoc.Save(LOCAL_RES_PATH + VERSION_FILE);
                }
                else
                {
                    sr.Close();
                    m_XmlDoc.Load(LOCAL_RES_PATH + VERSION_FILE);
                    XmlNode XmlRoot = m_XmlDoc.SelectSingleNode("Files");

                    if (MD5NodeFile.Count == 0)
                    {
                        XmlNodeList childlist = XmlRoot.ChildNodes;
                        foreach (XmlNode node in childlist)
                        {
                            MD5NodeFile.Add(node.Attributes["FileName"].Value);
                        }
                    }

                    if (MD5NodeFile.Contains(name))
                    {
                        XmlNodeList childlist = XmlRoot.ChildNodes;
                        foreach (XmlNode node in childlist)
                        {
                            if (node.Attributes["FileName"].Value.Equals(name))
                            {
                                node.Attributes["MD5"].Value = key;
                            }
                        }
                    }
                    else
                    {
                        XmlElement xmlElem = m_XmlDoc.CreateElement("File");
                        XmlRoot.AppendChild(xmlElem);

                        xmlElem.SetAttribute("FileName", name);
                        xmlElem.SetAttribute("MD5", key);
                    }
                    //更新本地存储的资源列表信息
                    if (m_LocalResVersion.ContainsKey(name) == false)
                    {
                        m_LocalResVersion.Add(name, key);
                    }
                    else
                    {
                        m_LocalResVersion[name] = key;
                    }

                    m_XmlDoc.Save(LOCAL_RES_PATH + VERSION_FILE);
                }
            }

        }

        //替换本地资源 [9/29/2014 Zmy]
        private void ReplaceLocalRes(string fileName, byte[] data)
        {
            string filePath = LOCAL_RES_PATH + fileName;
            FileStream stream = new FileStream(LOCAL_RES_PATH + fileName, FileMode.Create);
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();

            //LogManager.Log("生成文件：" + filePath);
            if (fileName != null)
            {
                UpdateLocalVersionFile(fileName, m_ServerResVersion[fileName]);
            }
        }

        //协同下载的过程，在完成下载资源后。执行委托函数 [9/29/2014 Zmy]
        IEnumerator DownLoad(string url,string name, HandleFinishDownLoad finishFun)
        {
            string path = url + name;
            if (name.Contains("client") == true)
            {
                path = SERVER_BIN_RES_URL + name;
            }

            WWW www = new WWW(path);
            float jianGe = 1.0f;
               
            while (www.isDone == false)
                yield return www;

            if (www.error == null)
            {
                downbytes += www.bytes.Length;
                if (Time.time > delay)
                {
                    delay = Time.time + jianGe;
                    donwLoadSpeed = (float)(downbytes / 1024 / jianGe);
                    downbytes = 0;
                }

                AssetBundle bundle = null;
                //对除了这两种文件以外的资源进行解密load
                if (path.Contains("VersionMD5") == false && path.Contains(".lua") == false && name.Contains("client") == false)
                {
                    string key = path.Substring(SERVER_RES_URL.Length, path.Length - SERVER_RES_URL.Length - 1);
                    key = key.Substring(0, key.LastIndexOf('.'));
                    key += ".dec";

                    byte[] decryptedData = ExecuteDecrypt(key, www.bytes);
                    AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
                    yield return acr;

                    bundle = acr.assetBundle;
                }
                else
                {
                    yield return new WaitForSeconds(0.01f);
                }

                if (finishFun != null && www.error == null)
                {
                    LogManager.LogToFile("下载完成: " + path + " 耗时：" + delay.ToString() + "毫秒！");
                    Debug.Log("下载完成: " + path);
                    finishFun(www, bundle);

                    if (bundle != null)
                    {
                        bundle.Unload(false);
                    }
                    www.Dispose();
                    www = null;
                }
            }
            else
            {              
                yield return new WaitForSeconds(0.5f);
                WWWError();
                LogManager.LogError("!!!error = " + www.error);
                //LogManager.LogError("原始路径 = " + path);
                //StopAllCoroutines();
            }
        }
        private void WWWError()
        {
            if (UI_LoginControler.Inst != null)
            {
                UI_LoginControler.Inst.ResourceDownloadError();
            }
        }

        //读取加载.bin表数据
        private void ReadLocalBinData()
        {
            m_clientAllAseetsOK = false;
            ReadLocalTableData();
            ReadTableData();
        }

        //读取本地数值表名字
        private void ReadLocalTableData()
        {
            try
            {
                DataTemplate.GetInstance().Init();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
            Dictionary<String, TableReader> _List = DataTemplate.GetInstance().m_TableList;
            foreach (KeyValuePair<string, TableReader> item in _List)
            {
                TableDataFiles.Add(item.Key);
            }
        }

        //递归依次读取本地需要的表数据 [8/21/2014 Yao]
        private void ReadTableData()
        {
            if (TableDataFiles.Count == 0)
            {
                m_clientAllAseetsOK = true;
                NeedDownFilesCount = 0;
                return;
            }
            string file = TableDataFiles[0];
            TableDataFiles.RemoveAt(0);

            StartCoroutine(DownloadTableDataAssedBundle(file));
        }

        IEnumerator DownloadTableDataAssedBundle(string name)
        {
            StringBuilder localPath = new StringBuilder();
            localPath.Append("file:///" + AppManager.Inst.readAndWritePath);
            localPath.Append(name + ".bin");
            WWW www = new WWW(localPath.ToString());
            while (www.isDone == false)
                yield return www;

            if (www.error != null)
            {
                LogManager.Log("error = " + www.error);
                LogManager.Log("error when downloading: " + name);
            }

            DataTemplate.GetInstance().InitData(name, www.bytes);
            www.Dispose();
            ReadTableData();
        }
    }
}