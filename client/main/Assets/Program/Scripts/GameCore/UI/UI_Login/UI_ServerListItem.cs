using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using System;

namespace DreamFaction.UI
{
    /// <summary>
    /// 服务器列表中的 Item
    /// </summary>
    public class UI_ServerListItem : BaseUI
    {
        // 显示资源绑定
        private Text serverName; // 服务器名称
        private GameObject SelectedArrow; // 选中箭头，选中标记
        private Button clickBtn; // 点击响应的按钮
        private ServerListConfig serverData; // 服务器相关数据
        private bool m_isSelected; // 是否是当前选中的服务器
        private Image[] m_Image = new Image[4];//服务器 忙碌/空闲 的状态图标

        public ServerListConfig GetServerData()
        {
            return serverData;
        }
        // ===================== 继承 ========================
        // 0: 初始化数据
        public override void InitUIData()
        {
            base.InitUIData();
            serverName = selfTransform.FindChild("Text").GetComponent<Text>();
            SelectedArrow = selfTransform.FindChild("SelectedArrow").gameObject;
            clickBtn = selfTransform.GetComponent<Button>();
            clickBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickItem));
            m_Image[0] = selfTransform.FindChild("Image0").GetComponent<Image>();
            m_Image[1] = selfTransform.FindChild("Image1").GetComponent<Image>();
            m_Image[2] = selfTransform.FindChild("Image2").GetComponent<Image>();
            m_Image[3] = selfTransform.FindChild("Image3").GetComponent<Image>();
        }

        // 1：初始化UI显示
        public override void InitUIView()
        {
            base.InitUIView();
            if (!string.IsNullOrEmpty(serverData.ServerName))
            {
                string[] _str = serverData.ServerName.Split('#');
                if (_str.Length >= 2)
                {
                    serverName.text = "  " + _str[0] + "       " + _str[1];
                }
            }

            //if (serverData.ServerStatus <= 4)
            //{
            //    if (serverData.ServerStatus == 1 )
            //    {
            //        for (int i = 0; i < serverData.ServerStatus; i++)
            //        {
            //            m_Image[i].color = new Color(0.44f, 1f, 0.14f);
            //        }
            //    }
            //    if (serverData.ServerStatus == 2)
            //    {
            //        for (int i = 0; i < serverData.ServerStatus; i++)
            //        {
            //            m_Image[i].color = new Color(1f, 0.89f, 0.14f);
            //        }
            //    }
            //    if (serverData.ServerStatus == 3)
            //    {
            //        for (int i = 0; i < serverData.ServerStatus; i++)
            //        {
            //            m_Image[i].color = new Color(1f, 0.44f, 0.14f);
            //        }
            //    }
            //    if (serverData.ServerStatus == 4)
            //    {
            //        for (int i = 0; i < serverData.ServerStatus; i++)
            //        {
            //            m_Image[i].color = new Color(1f, 0.18f, 0.14f);
            //        }
            //    }
            //}
            SelectedArrow.SetActive(m_isSelected);
        }

       // ==================== 公共对外接口 =================
        // 1: 设置服务器选项数据
        public void SetServerDate(ServerListConfig data,bool isSelected)
        {
            serverData = data;
            m_isSelected = isSelected;
        }

        public void SetHaveSelect(bool isSelected)
        {
            SelectedArrow.SetActive(isSelected);
        }


        // ================= 回调事件 ================
        // 点击Item后回调
        private void OnClickItem()
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_SelectedServer, serverData);
        }
    }
}

