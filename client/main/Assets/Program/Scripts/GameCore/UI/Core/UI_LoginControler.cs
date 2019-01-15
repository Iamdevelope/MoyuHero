using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.UI;
using DreamFaction.GameEventSystem;
using Platform;
using System.Xml;
using System.IO;
using System;
using DreamFaction.Utils;

namespace DreamFaction.UI.Core
{
    /// <summary>
    /// 登陆场景的UI控制器继承自BaseUIControler，用来控制主场景的UI加载，删除！
    /// </summary>
    public class UI_LoginControler : BaseUIControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static UI_LoginControler Inst;

        private bool IsOpenUI_AssetsLoading = false;
        private bool isGoPlat = false;
        private bool isComparisonVersion = false;
        private string LocalVersionNum;//本地的版本号
        private string ServerVersionNum;//服务器获得的版本号

        // =====================  重载 ============================
        /// <summary>
        /// 1: 初始化
        /// </summary>
        protected override void InitData()
        {
 	         base.InitData();
             Inst = this;
             GetLocalVersionNum();
             GetServerVersionNum();
            
             // 如果没有缓存账号信息，显示账号注册界面 UI_SelectLoginServer，否则直接显示登陆界面 UI_LoginWin
            //string nAccount = ConfigsManager.Inst.GetClientConfig(ClientConfigs.Account);
            //if (nAccount== string.Empty)
            //{
            //    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_SelectLoginServer.UI_ResPath);
            //}
            //else
            //{
            //    // 验证是否有账号
            //    CPlatformExists exists = new CPlatformExists();
            //    exists.username = nAccount;
            //    IOControler.GetInstance().SendPlatform(exists);
            //}
            //StageTemplate ii = new StageTemplate();
            //Dictionary<string, int> d = new Dictionary<string, int>();
            //string str = "{h1:32,h2:14}";
            //ii.parserXmlDict(str, out d);
             //StartCoroutine(Test());
            // InvokeRepeating("serverError", 1.0f, 2.0f);
        }
        // ====================== 公共接口 =========================
        
        protected override void UpdateView()
        {
            base.UpdateView();

            if (AssetManager.Inst.ComparisonVersionOver)
            {
                if (isComparisonVersion == false)
                {
                    ComparisonVersionNum();
                }
                isComparisonVersion = true;
            }

            if (AssetManager.ClientAllAseetsOK)
            {
                if (isGoPlat == false)
                {
    
                    if (LoginControler.StartShow)
                    {
                        GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_AnnounceMgr.UI_ResPath);
                    }

                    isGoPlat = true;
                }
                return;
            }

        }

        protected override void InitView()
        {
           base.InitView();       
        }
        /// <summary>
        /// 比对服务器版本号和本地版本号
        /// </summary>
        public void ComparisonVersionNum()
        {
            Debug.Log(LocalVersionNum + "---" + ServerVersionNum);
            string[] _Local = LocalVersionNum.Split('.');
            string[] _Server = ServerVersionNum.Split('.');
            if (_Local.Length != 3 || _Server.Length != 3)
            {
                Debug.Log("版本号错误--- Application.Quit()");
                return;
            }
            if (float.Parse(_Local[0]) < float.Parse(_Server[0]))
            {
                UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
                ui.type = UI_GameTips.TipsType.GoPlatform;
                return;
            }
            else if (float.Parse(_Local[1]) < float.Parse(_Server[1]))
            {
                UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
                ui.type = UI_GameTips.TipsType.GoPlatform;
                return;
            }
            else if (float.Parse(_Local[1]) >= float.Parse(_Server[1]) && float.Parse(_Local[1]) < float.Parse(_Server[2]))
            {
                UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
                ui.type = UI_GameTips.TipsType.CancelOrGoPlatform;
                return;
            }
            else
            {
                if (AssetManager.Inst.NeedDownFilesCount > 0)
                {
                    UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
                    ui.type = UI_GameTips.TipsType.StartLoadUpdataAsset;
                    return;
                }
                else
                {
                    AssetManager.Inst.StartLoadAsset();
                }
            }

            
        }

        private void GetLocalVersionNum()
        {
            LocalVersionNum = ConfigsManager.Inst.GetClientConfig(ClientConfigs.Version);
            //XmlDocument m_xmlDoc = new XmlDocument();
            //try
            //{ 
            //     m_xmlDoc.Load(@AppManager.Inst.readAndWritePath + "/Data/Config.xml");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error!!!!!!!!!!!! Local ConfigXML is Empty! \nErrorMessage:====>" + ex.Message);
            //}
               
            ////m_xmlDoc.Load(@"C:\Users\Administrator\AppData\LocalLow\DreamFactionGame\Dream Heros\Data\Config.xml");
            //XmlNodeList nodeList = m_xmlDoc.SelectSingleNode("/ClientConfigs").ChildNodes;

            //if (nodeList != null)
            //{
            //    foreach (XmlNode node in nodeList)
            //    {
            //        XmlElement xe = (XmlElement)node;

            //        if (xe.Name == "Version")
            //        {
            //            LocalVersionNum = xe.InnerText;
            //            //Debug.Log(LocalVersionNum);
            //            break;
            //        }
            //    }
            //}
        }

        private void GetServerVersionNum()
        {
            string SERVER_RES_URL;
            string SERVER_VERSION_URL;
            SERVER_VERSION_URL = "file://" + AppManager.Inst.readOnlyPath + "Data/";


            // 根据不同平台进行初始化
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/Android/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/IOS/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP);
                    break;
                default:
                    SERVER_RES_URL = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "AssetBundle/PC/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP);
                    break;
            }
            XmlDocument m_xmlDoc = new XmlDocument();
            if (AppManager.Inst.IsLocalVersion)
            {
                m_xmlDoc.Load(@SERVER_VERSION_URL + "/ServerVersion.xml");
            }
            else
            {
                m_xmlDoc.Load(SERVER_RES_URL + "data/" + ConfigsManager.Inst.GetClientConfig(ClientConfigs.Version) + "/ServerInfo/ServerVersion.xml");
            }
            XmlNodeList nodeList = m_xmlDoc.SelectSingleNode("/ClientConfigs").ChildNodes;

            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    XmlElement xe = (XmlElement)node;

                    if (xe.Name == "Version")
                    {
                        ServerVersionNum = xe.InnerText;
                        // Debug.Log(ServerVersionNum);
                        break;
                    }
                }
            }
        }

        public void ResourceDownloadError()
        {
            UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
            ui.type = UI_GameTips.TipsType.ResourceDownloadUnOk;
            return;
        }

    }
}
