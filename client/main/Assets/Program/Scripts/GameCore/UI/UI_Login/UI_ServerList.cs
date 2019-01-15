using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_ServerList : BaseUI
    {
        public static string UI_ResPath = "UI_Login/UI_ServerList_2_2";
        private UI_ServerListItem ServerListItem;                    // 列表Item
        private RectTransform serverList;                            //列表控件 的显示对象
        private List<UI_ServerListItem> itemList;                    //服务器列表Item的显示对象数组
        private Dictionary<string, ServerListConfig> serverListData; //服务器列表数据
        private Text BeforServerTxt;                                 // 之前登陆的服务器 
        private Text NewServerTxt;                                   // 新服推荐
        private Button ReturnButton;                                   // 新服推荐


        // 1: 初始化UI数据操作
        public override void InitUIData()
        {
            base.InitUIData();
            itemList = new List<UI_ServerListItem>();
            serverListData = ConfigsManager.Inst.GetAllServerConfig();
            serverList = selfTransform.FindChild("ServerList/ListLayout").GetComponent<RectTransform>();
            ServerListItem = selfTransform.FindChild("ServerList/ListLayout/ServerListItem").GetComponent<UI_ServerListItem>();
            BeforServerTxt = selfTransform.FindChild("LeftImg1/Text").GetComponent<Text>();
            NewServerTxt =  selfTransform.FindChild("LeftImg0/Text").GetComponent<Text>();
            ReturnButton = selfTransform.FindChild("UI_BG_Top/BackBtn").GetComponent<Button>();
            ReturnButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReturn));

            // 创建服务器列表Item
            CreatServerListItem();
            // 监听事件
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_SelectedServer, OnSelectedServer);
        }
        // 2：初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();
      
            string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
            if (ServerID != string.Empty)
            {
                ServerListConfig nServerList = ConfigsManager.Inst.GetServerList(ServerID);
                if(nServerList != null )
                {
                    string[] _str = nServerList.ServerName.Split('#');
                    if (_str.Length >= 2)
                    {
                        BeforServerTxt.text = "  " + _str[0] + "   " + _str[1];
                        NewServerTxt.text = BeforServerTxt.text;
                    }
                }
            }
            else
            {
                NewServerTxt.text = BeforServerTxt.text = GameUtils.getString("create_role_tip8"); //"无"
            }
        }

        // 2：关闭UI操作
        public override void OnReadyForClose()
        {
            base.OnReadyForClose();
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_SelectedServer, OnSelectedServer);
            UI_LoginControler.Inst.ReMoveUI(gameObject);
        }


        //  ===================== 私有接口 ===================
        // 1：创建服务器列表中的Item
        private void CreatServerListItem()
        {
            UI_ServerListItem newItem;
            bool hasSelectedItem = false;

            foreach (var listData in serverListData)
            {
                newItem = Instantiate(ServerListItem) as UI_ServerListItem;
                itemList.Add(newItem);
                newItem.transform.SetParent(serverList, false);
                newItem.gameObject.SetActive(true);

                bool selected = false;
                string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
                if (ServerID != string.Empty)
                {
                    if (!hasSelectedItem)
                    {
                        if (byte.Parse(ServerID) == byte.Parse(listData.Value.GsID))
                        {
                            hasSelectedItem = true;
                            selected = true;
                        }
                    }
                }
                newItem.SetServerDate(listData.Value, selected); // 填充初始化数据
            }
        }

        private void OnClickReturn()
        {
            UIState = UIStateEnum.ReadyForClose;
        }

        private void UpdateSelectState()
        {
            string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
            for (int i = 0; i < itemList.Count;i++ )
            {
                if (byte.Parse(ServerID) == byte.Parse(itemList[i].GetServerData().GsID))
                {
                    itemList[i].SetHaveSelect(true);
                }
                else
                {
                    itemList[i].SetHaveSelect(false);
                }
            }
        }

        // ===================== 回调 =================
        // 选中某个服务器列表项
        private void OnSelectedServer(GameEvent e)
        {
            ServerListConfig config = (ServerListConfig)e.data;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.ServerID,config.GsID.ToString());
            UpdateSelectState();
            UIState = UIStateEnum.ReadyForClose;
        }
    }
}
