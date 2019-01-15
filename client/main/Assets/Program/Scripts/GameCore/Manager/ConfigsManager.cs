using System.Xml;
using System;
using System.Collections.Generic;
using DreamFaction.Utils;
using System;
using UnityEngine;

namespace DreamFaction.GameCore
{
    /// 服务器配置文件的数据结构
    public class ServerListConfig
    {
        // 服务器ID
        public string GsID;

        /// 服务器的名称
        public string ServerName;

        /// 服务器IP地址
        public string ServerIP;

        /// 服务器端口
        public int ServerPort;

        /// 服务器状态：0：维护中,1：流畅,2：正常,3：饱和，4：爆满 
        public int ServerStatus;
    }

    public class ConfigsManager : BaseControler
    {
  
        public static ConfigsManager Inst;

        private string m_configRootName = "ClientConfigs";                                                          // 客户端配置表中Root的名字
        private string m_configFileName = "Config.xml";                                                        // 客户端配置文件名称
        private string m_serverListFileName = "ServerList.xml";                                                // 服务器列表配置文件
        private bool m_isConfigFileOK = false;                                                                      // 配置文件是否可用
        private XmlDocument m_xmlDoc = new XmlDocument();                                                           // 客户端配置文件的文本内容
        private Dictionary<string, string> m_ClientConfigInfoMap = new Dictionary<string, string>();                // 客户端配置文件中的数据信息
        private Dictionary<string,ServerListConfig> m_ServerListInfo = new Dictionary<string,ServerListConfig>();   // 服务器列表数据

        /// <summary>
        /// 初始化接口
        /// </summary>
        protected override void InitData()
        {
            Inst = this;

            // 加载客户端配置文件
            LoadConfigXML(ref m_configFileName,false);

            // 读取客户端配置文件
            ReadClientConfig();

            // 加载服务器列表配置文件
            LoadConfigXML(ref m_serverListFileName,true);

            // 读取服务器列表配置文件
            ReadServerListConfig();

            // 从配置项中获取当前游戏使用语言
            AppManager.Inst.GameLanguage = GetClientConfig(ClientConfigs.GameLanguage);
            
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        protected override void DestroyData()
        {
            Inst = null;
        }

        /// <summary>
        ///  获取当前配置是否可用的标记
        /// </summary>
        public bool isConfigFileOK
        {
            get { return m_isConfigFileOK; }
        }

        /// <summary>
        /// 获取客户端 配置项
        /// </summary>
        /// <param name="config">客户端配置项</param>
        /// <returns></returns>
        public string GetClientConfig(ClientConfigs config)
        {
            string configKey = config.ToString();
            if (!m_ClientConfigInfoMap.ContainsKey(configKey))
            {
                //// 先清理客户端缓存信息！
                //m_ClientConfigInfoMap.Clear();
                //// 加载客户端配置文件
                //LoadConfigXML(ref m_configFileName, true);
                //// 读取客户端配置文件
                //ReadClientConfig();
                //if (GetClientConfig(config) == string.Empty)
                    return string.Empty;
            }
            if (!isConfigFileOK)
            {
                return string.Empty;
            }
            return m_ClientConfigInfoMap[configKey];
        }

        /// <summary>
        /// 设置客户端配置表数据！
        /// </summary>
        /// <param name="config">需要设置的客户端配置项</param>
        /// <param name="value">设置的最终值</param>
        public void SetClientConfig(ClientConfigs config, string value)
        {
            string key = config.ToString();
            
            if (!m_ClientConfigInfoMap.ContainsKey(key))
            {
                Debug.Log("配置项中不包含" + key);
                return;
            }
            //1：更新xml缓冲区
            if (m_ClientConfigInfoMap[key] == value)
            {
                return;
            }
            m_ClientConfigInfoMap[key] = value;

            //2：更新客户端xml文件中的内容
            LoadConfigXML(ref m_configFileName,false); // 先加载xml文件
            XmlNodeList configs = m_xmlDoc.SelectSingleNode(m_configRootName).ChildNodes;
            foreach (XmlElement element in configs)
            {
                if (element.Name == key)
                {
                    element.InnerText = value;
                    break;
                }
            }
            string rw_Path = AppManager.Inst.readAndWritePath + "Data/" + m_configFileName;
            //3: 保存到磁盘文件，以后这里需要定时保存，而不是来一条修改信息就保存一次！
            m_xmlDoc.Save(rw_Path);
        }

        //服务器配置信息
        public ServerListConfig GetServerList(string nServerId )
        {
            if (!m_ServerListInfo.ContainsKey(nServerId))
            {
                return null;
            }
            return m_ServerListInfo[nServerId];
        }

        /// 获取所有服务器配置项列表！
        public Dictionary<string, ServerListConfig> GetAllServerConfig()
        {
            return m_ServerListInfo;
        }
        
        // 加载客户端配置文件
        private void LoadConfigXML(ref string fileName,bool needReload) 
        {
            string r_Path;
           // if (fileName.Contains("Config.xml"))
            if (AppManager.Inst.IsLocalVersion)
            {
                r_Path = AppManager.Inst.readOnlyPath + "Data/" + fileName;
            }
            else
            {
                if (fileName.Contains("Config.xml"))
                {
                    r_Path = AppManager.Inst.readOnlyPath + "Data/" + fileName;
                }
                else
                {
                    r_Path = m_ClientConfigInfoMap["ResoursePlatformIP"] + "data/" + m_ClientConfigInfoMap["Version"] + "/ServerInfo/" + fileName;
                }
                
            }

            string rw_Path = AppManager.Inst.readAndWritePath + "Data/" + fileName;
            if (needReload)
            {
                XMLHelper.RemoveFile_RW(rw_Path);
            }
            // 1：检查可读写目录下是否有配置文件
            bool isHaveXMLFile = XMLHelper.CheckFileAndDirExists_RW(rw_Path);
            if (!isHaveXMLFile)
            {
                // 2: 如果没有则从Streaming目录下复制一个过去
                XMLHelper.LoadXML(r_Path, ref m_xmlDoc);
                XMLHelper.CreateXML(rw_Path, m_xmlDoc);
            }

            // 3: 读取xml，并获取配置项
            XMLHelper.LoadXML(rw_Path, ref m_xmlDoc);
        }

        // 从客户端配置XML文本中获取配置信息    
        private void ReadClientConfig()
        {
            XmlNodeList configs = m_xmlDoc.SelectSingleNode(m_configRootName).ChildNodes;
            foreach (XmlElement element in configs)
            {
                m_ClientConfigInfoMap.Add(element.Name, element.InnerText);
            }
            m_isConfigFileOK = true;
        }

        // 加载服务器列表配置文件
        private void ReadServerListConfig()
        {
            XmlNodeList configs = m_xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement element in configs)
            {
                ServerListConfig temp = new ServerListConfig();
                temp.GsID        = element.GetAttribute("GsId");
                temp.ServerName  = element.GetAttribute("Name");
                temp.ServerIP    = element.GetAttribute("IP");
                temp.ServerPort  = int.Parse(element.GetAttribute("Port"));
                temp.ServerStatus = int.Parse(element.GetAttribute("Status"));
                if (!m_ServerListInfo.ContainsKey(temp.GsID))
                {
                    m_ServerListInfo[temp.GsID] = temp;
                }   
            }
        }

        /// 获取新关卡数据
        /// <param name="data"> 输出数据 </param>
        public void getNewStages(ref Dictionary<int, bool> data)
        {
            string str = GetClientConfig(ClientConfigs.DreamFactionNewStages);
            if (!string.IsNullOrEmpty(str))
            {
                data.Clear();
                string[] stringArray = str.Split("|"[0]);
                for (int i = 0; i < stringArray.Length; i++)
                {
                    int id = Convert.ToInt32(stringArray[i]);
                    data.Add(id, true);
                }
            }
        }

        public void updateNewStages(Dictionary<int, bool> data)
        {
            if (data == null || data.Count <= 0)
            {
                SetClientConfig(ClientConfigs.DreamFactionNewStages, string.Empty);
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var idd in data)
                {
                    sb.Append(idd.Key).Append("|");
                }
                if (sb.Length > 1)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                SetClientConfig(ClientConfigs.DreamFactionNewStages, sb.ToString());
            }
        }
    }
}
