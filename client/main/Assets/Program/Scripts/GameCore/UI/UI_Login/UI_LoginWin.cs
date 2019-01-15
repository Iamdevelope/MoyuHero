using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using System.Collections.Generic;

namespace DreamFaction.UI
{
    /// <summary>
    /// 登陆界面
    /// </summary>
    public class UI_LoginWin : BaseUI
    {
        public const float AlphaDelta = 0.5f;

        public static string UI_ResPath = "UI_Login/UI_LoginWin_2_0";
        public static string curServerName;
        private Button LoginBtn; // 登陆按钮 
        private Button ServerBtn; // 服务器按钮
        private Text serverName; // 服务器名称文本框
        private Image serverStatus; // 服务器当前状态 顺畅 拥堵
        private bool isEmptyServer; // 是否无上次登录服务器
        public static bool m_StarShow = false;
        private Component[] _comps;
        private float AlphaValue = 0;

        private Dictionary<string, ServerListConfig> serverListData; //服务器列表数据
        // ===================== 继承 ==================
        // 1: 初始化UI数据操作
        public override void InitUIData()
        {
            base.InitUIData();
            serverListData = ConfigsManager.Inst.GetAllServerConfig();
            LoginBtn = selfTransform.FindChild("LoginBtn").GetComponent<Button>();
            ServerBtn = selfTransform.FindChild("ServerBtn").GetComponent<Button>();
            serverName = selfTransform.FindChild("ServerBtn/Text").GetComponent<Text>();
            serverStatus = selfTransform.FindChild("ServerBtn/image").GetComponent<Image>();
            LoginBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLoginBtn));
            ServerBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickServerBtn));

            //(LoginBtn.GetComponent<Component>() as Graphic).CrossFadeAlpha(0, 0,true);
            //(ServerBtn.GetComponent<Component>() as Graphic).CrossFadeAlpha(0, 0, true);
            //(serverName.GetComponent<Component>() as Graphic).CrossFadeAlpha(0, 0, true);
            // 监听事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_SelectedServer, OnSelectedServer);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_ReActive, SetBtnShowState);
        }

        // 2：初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();


            string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
            if (ServerID != string.Empty)
            {
                ServerListConfig nServerList = ConfigsManager.Inst.GetServerList(ServerID);
                if (nServerList != null)
                {
                    string[] _str = nServerList.ServerName.Split('#');
                    if (_str.Length >= 2)
                    {
                        serverName.text = "  " + _str[0] + "   " + _str[1];
                        foreach (var listData in serverListData)
                        {
                            if (byte.Parse(ServerID) == byte.Parse(listData.Value.GsID))
                            {
                                if (listData.Value.ServerStatus != 4)
                                    serverStatus.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0208");
                                else
                                    serverStatus.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0209");
                            }
                        }
                    }
                    isEmptyServer = false;
                }
            }
            else
            {
                serverName.text = GameUtils.getString("login_content1");//"请选择服务器"
                isEmptyServer = true;
            }

            _comps = transform.GetComponentsInChildren<Component>();
            //全透明 [6/19/2015 Zmy]
            foreach (Component item in _comps)
            {
                if (item is Graphic)
                {
                    Graphic _aphic = item as Graphic;
                    if (_aphic != null)
                    {
                        _aphic.color = new Color(_aphic.color.r, _aphic.color.g, _aphic.color.b, 0f);
                    }
                }
            }
            
            //ShowAnnounceUI();
        }

        
        public override void UpdateUIView()
        {
            if (m_StarShow)
            {
                AlphaValue += Time.deltaTime;
                if (AlphaValue < AlphaDelta)
                {
                    foreach (Component item in _comps)
                    {
                        if (item is Graphic)
                        {
                            Graphic _aphic = item as Graphic;
                            if (_aphic != null)
                            {
                                _aphic.color = new Color(_aphic.color.r, _aphic.color.g, _aphic.color.b, AlphaValue / AlphaDelta);
                            }
                        }
                    }
                }
                else
                {
                    m_StarShow = false;

                    OnUIShowCallBack();
                }
            }
        }
        // 记得删除监听的事件
        void OnDestroy()
        {
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_SelectedServer, OnSelectedServer);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_ReActive, SetBtnShowState);
        }

        /// <summary>
        /// 界面展示回调;
        /// </summary>
        void OnUIShowCallBack()
        {
            ShowWelcome();
            //ShowAnnounceUI();
        }

        //打开公告界面;
        void ShowAnnounceUI()
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_AnnounceMgr.UI_ResPath);
            //UI_HomeControler.Inst.AddUI(UI_AnnounceMgr.UI_ResPath);
        }

        // 欢迎用户回来
        void ShowWelcome()
        {
            InterfaceControler.GetInst().AddMsgBox (GameUtils.getString("login_welcomeback1"), UI_LoginControler.Inst.GetTopTransform());
        }

        private void SetBtnShowState()
        {
            isEmptyServer = false;
            GameUtils.SetBtnSpriteGrayState(LoginBtn, false);
            LoginBtn.interactable = true;
        }

        // ===================== 按钮回调 =================
        // 1: 登陆按钮
        private void OnClickLoginBtn()
        {
            // 第一次登陆没有选择过登陆服务器，强制让用户选择一个
            if (isEmptyServer)
            {
                OnClickServerBtn();
                return;
            }
       
            string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
            if( ServerID != string.Empty )
            {
                ServerListConfig serverconfig = ConfigsManager.Inst.GetServerList(ServerID);
                if(serverconfig !=null)
                {
                    curServerName = serverconfig.ServerName;

                    IOControler.Connect(serverconfig.ServerIP, (ushort)serverconfig.ServerPort);

                    GameUtils.SetBtnSpriteGrayState(LoginBtn, true);
                    LoginBtn.interactable = false;
                }
                else
                {
                    // 加代码
                    UI_LoginControler.Inst.AddUI(UI_ServerList.UI_ResPath);
                }
            }
            else
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("error #100052"));//serverid 错误
            }

        }

        // 2: 服务器按钮
        private void OnClickServerBtn()
        {
            UI_LoginControler.Inst.AddUI(UI_ServerList.UI_ResPath);
        }

        // 3: 选中某个服务器列表项
        private void OnSelectedServer(GameEvent e)
        {
            ServerListConfig config = (ServerListConfig)e.data;
            if (config.GsID != string.Empty)
            {
                ServerListConfig nServerList = ConfigsManager.Inst.GetServerList(config.GsID);
                if(nServerList != null)
                {
                    string[] _str = nServerList.ServerName.Split('#');
                    if (_str.Length >= 2)
                    {
                        serverName.text = "  " + _str[0] + "   " + _str[1];
                        if (config.ServerStatus != 4)
                            serverStatus.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0208");
                        else
                            serverStatus.sprite = UIResourceMgr.LoadSprite(common.defaultPath + "img_TY_0209");
                    }
                }
            }
            isEmptyServer = false;
        }

    }
}
