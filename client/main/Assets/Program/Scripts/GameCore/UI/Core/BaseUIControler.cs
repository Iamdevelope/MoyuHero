using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using GNET;
using DreamFaction.LogSystem;

namespace DreamFaction.UI.Core
{
    /// <summary>
    /// UI控制器基类，提供了基础的初始化更新操作！以及添加/删除UI的操作等！
    /// </summary>
    public class BaseUIControler : BaseControler
    {
        private string m_Str = "";

        /// <summary>
        /// UICanvas的标记，用来标记具体操作针对于那个Canvas，例如隐藏操作，和屏蔽事件操作！
        /// </summary>
        public enum UICanvasFlag
        {
            All_Canvas = -1,// 所有UI层，包括系统层
            Canvas0 = 0,
            Canvas1 = 1,
            Canvas2 = 2 ,
            Canvas3 = 3 ,
            Max = 4,  // 上限标记，以后有扩展的canvas这个需要后移
        }
        /// <summary>
        /// 已添加的UI列表
        /// </summary>
        protected Dictionary<string, GameObject> AddedUIList = new Dictionary<string, GameObject>();
        /// <summary>
        /// 缓存transform的引用
        /// </summary>
        protected Transform selfTransform;
        /// <summary>
        /// UI输入事件响应系统 
        /// </summary>
        protected EventSystem UIEventSystem;
        /// <summary>
        /// UI的Canvas列表：canvas0表示最顶层 ，canvas3表示最底层
        /// </summary>
        protected List<Transform> canvasList = new List<Transform>();
        // 屏蔽UICanvas的Raycasts操作数组，用来记录每个canvas被关闭BlockCanvasRaycasts的次数
        private int [] UnBlockCanvasRaycastsCountList;



        // ======================== 继承 ===============================
        /// <summary>
        /// 1: 初始化
        /// </summary>
        protected override void InitData()
        {
            selfTransform = transform;
            UIEventSystem = selfTransform.FindChild("EventSystem").GetComponent<EventSystem>();
            canvasList.Add(selfTransform.FindChild("Canvas0").transform);
            canvasList.Add(selfTransform.FindChild("Canvas1").transform);
            canvasList.Add(selfTransform.FindChild("Canvas2").transform);
            canvasList.Add(selfTransform.FindChild("Canvas3").transform);
            UnBlockCanvasRaycastsCountList = new int[canvasList.Count];//通过图层数量来创建

            

            // 注册UICanvas的监听器
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_HidUICanvas, OnHidUICanvas);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_ShowUICanvas, OnShowUICanvas);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_BlockCanvasRaycasts, OnBlockCanvasRaycasts);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_UnBlockCanvasRaycasts, OnUnBlockCanvasRaycasts);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_NetTips, OnNetTips);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_GameTips, OnGameTips);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_OpenUI, OnU_OpenUI);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_CloseUI, OnU_CloseUI);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_OpenOrCloseUI, OnU_OpenOrCloseUI);
            GameEventDispatcher.Inst.addEventListener(GameEventID.U_MsgNotify, OnMsgNotify);
            GameEventDispatcher.Inst.addEventListener(GameEventID.HE_GetHeroHp, OnGetHeroHp);
            
        }

        // 退出操作
        protected override void DestroyData()
        {
            base.DestroyData();
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HidUICanvas, OnHidUICanvas);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_ShowUICanvas, OnShowUICanvas);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_BlockCanvasRaycasts, OnBlockCanvasRaycasts);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_UnBlockCanvasRaycasts, OnUnBlockCanvasRaycasts);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_NetTips, OnNetTips);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_GameTips, OnGameTips);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_OpenUI, OnU_OpenUI);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_CloseUI, OnU_CloseUI);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_OpenOrCloseUI, OnU_OpenOrCloseUI);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.U_MsgNotify, OnMsgNotify);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.HE_GetHeroHp, OnGetHeroHp);
        }

        // ======================== 公共接口 ==========================
       /// <summary>
        /// 1: 添加一个UI
       /// </summary>
       /// <param name="UIName">UI名称</param>
        public virtual GameObject AddUI(string UIName)
        {
            if(AddedUIList.ContainsKey(UIName))   
                return AddedUIList[UIName];
            
            Resources.UnloadUnusedAssets();

            int canvasIndex; // 图层索引
            int siblingIndex; // 排序索引
            GetCanvasIndex(UIName, out canvasIndex, out siblingIndex);
            Object UIAsset = Resources.Load("UI/Prefabs/" + UIName);
            GameObject UIInst = (GameObject)Instantiate(UIAsset);
            UIInst.name = UIName;
            UIInst.transform.SetParent(canvasList[canvasIndex], false);
            UIInst.transform.SetSiblingIndex(siblingIndex);
            AddedUIList.Add(UIName, UIInst);
            return UIInst;
        }

        

        /// <summary>
        /// 获取UI
        /// </summary>
        /// <param name="UIName">UI名称</param>
        /// <returns></returns>
        public GameObject GetUI(string UIName)
        {
            if (AddedUIList.ContainsKey(UIName))
            {
                return AddedUIList[UIName];
            }
            else
            {
                return null;
            }
        }

        public T GetUI<T>(string UIName) where T : BaseUI
        {
            if (AddedUIList.ContainsKey(UIName))
            {
                GameObject go = AddedUIList[UIName];

                return go == null ? null : go.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 2: 删除一个UI       通过名称查找删除
        /// </summary>
        /// <param name="UIName">UI名称</param>
        public virtual void ReMoveUI(string UIName)
        {
            if (!AddedUIList.ContainsKey(UIName))
            {
                return;
            }
            int canvasIndex; // 图层索引
            int siblingIndex; // 排序索引
            GetCanvasIndex(UIName, out canvasIndex, out siblingIndex);
            GameObject.Destroy(AddedUIList[UIName]);
            Resources.UnloadUnusedAssets();
            AddedUIList.Remove(UIName);
        }

        /// <summary>
        /// 3: 删除一个UI        直接删除对象
        /// </summary>
        /// <param name="UIobj">UI名称</param>
        public virtual void ReMoveUI(GameObject UIobj) 
        {
            GameObject.Destroy(UIobj);
            Resources.UnloadUnusedAssets();
            AddedUIList.Remove(UIobj.name);
        }

        public void RemoveAllUIButThis(string[] ignorUiNames)
        {
            List<string> destroyUINames = new List<string>();

            Dictionary<string, GameObject>.Enumerator enumerator = AddedUIList.GetEnumerator();
            while(enumerator.MoveNext())
            {
                bool isContais = false;
                string uiname = enumerator.Current.Key;

                for (int m = 0, n = ignorUiNames.Length; m < n; m++)
                {
                    if (uiname.Equals(ignorUiNames[m]))
                    {
                        isContais = true;
                        break;
                    }
                }

                if (!isContais)
                {
                    destroyUINames.Add(uiname);
                }
            }

            for (int i = 0; i < destroyUINames.Count; i++ )
            {
                ReMoveUI(destroyUINames[i]);
            }
        }

        /// <summary>
        ///  4: 隐藏/显示 某个UICanvas下的所有UI，通过参数UICanvasFlag确定，isHid控制显示/隐藏
        /// </summary>
        /// <param name="canvasFlag">UI图层</param>
        /// <param name="isHid">控制显示/隐藏</param>
        public void HidUICanvas(UICanvasFlag canvasFlag,bool isHid)
        {
            if(canvasFlag == UICanvasFlag.All_Canvas)
            {
                for(int i = 0; i < canvasList.Count; ++i)
                {
                    canvasList[i].GetComponent<CanvasGroup>().alpha = isHid ? 0 : 1;
                }
            }
            else 
            {
                int startIndex = (int)canvasFlag + 1;
                int maxIndex = (int)UICanvasFlag.Max;
                for (int i = startIndex; i < maxIndex; ++i)
                {
                    canvasList[i].GetComponent<CanvasGroup>().alpha = isHid ? 0 : 1;
                }
            }
        }

        /// <summary>
        ///  开启/关闭 UICanvas的 点击检测 ，关闭后不在拦截点击事件
        /// </summary>
        /// <param name="canvasFlag"></param>
        public void BlockCanvasRaycasts(UICanvasFlag canvasFlag, bool isBlock)
        {
            if (canvasFlag == UICanvasFlag.All_Canvas)
            {
                for (int i = 0; i < canvasList.Count; ++i)
                {
                    CheckOutCanvasRaycasts(i, isBlock);
                }
            }
            else 
            {
                int startIndex = (int)canvasFlag + 1;
                int maxIndex = (int)UICanvasFlag.Max;
                for (int i = startIndex; i < maxIndex; ++i )
                {
                    CheckOutCanvasRaycasts(i, isBlock);
                }
            }
            // 最后 更新下各个Canvas的显示状态
            DoBlockCanvasRaycasts();
        }

        // ========================= 私有接口 ==========================
        /// <summary>
        /// 1: 解析UI所需要添加的canvas,返回图层索引，以及排序索引
        /// </summary>
        /// <param name="UIName">UI名称</param>
        /// <param name="canvasIndex">UI的canvas索引</param>
        /// <param name="siblingIndex">渲染顺序索引</param>
        private void GetCanvasIndex(string UIName, out int canvasIndex, out int siblingIndex)
        {
            string [] splitList = UIName.Split('_');
            canvasIndex = int.Parse(splitList[splitList.Length-2]);
            siblingIndex = int.Parse(splitList[splitList.Length - 1]);
        }

        // 内部执行BlockCanvasRaycasts(操作
        private void CheckOutCanvasRaycasts(int index, bool isBlock)
        {
            // UICanvas的 点击检测的时候需要进行关闭操作计数，以便在Update的时候恢复点击测试！
            int curUnBlock = UnBlockCanvasRaycastsCountList[index];
            UnBlockCanvasRaycastsCountList[index] = isBlock ? Mathf.Max(0, curUnBlock - 1) : curUnBlock + 1;
        }

        // 设置最后BlockCanvasRaycasts的状态
        private void DoBlockCanvasRaycasts()
        {
            for(int i = 0 ; i < UnBlockCanvasRaycastsCountList.Length; ++i)
            {
                bool x = canvasList[i].GetComponent<CanvasGroup>().blocksRaycasts = !(UnBlockCanvasRaycastsCountList[i] > 0);
            }
        }
        
        // =============================== 事件回调 =======================
        // 隐藏UICanvas 的回调
        private void OnHidUICanvas(GameEvent e)
        {
            UICanvasFlag canvasFlag = (UICanvasFlag)e.data;
            HidUICanvas(canvasFlag,true);
        }

        // 显示UICanvas 的回调
        private void OnShowUICanvas(GameEvent e)
        {
            UICanvasFlag canvasFlag = (UICanvasFlag)e.data;
            HidUICanvas(canvasFlag, false);
        }

        // 开启UICanvas捕获点击操作 的事件
        private void OnBlockCanvasRaycasts(GameEvent e)
        {
            UICanvasFlag canvasFlag = (UICanvasFlag)e.data;
            BlockCanvasRaycasts(canvasFlag, true);
        }

        // 关闭UICanvas捕获点击操作 的事件
        private void OnUnBlockCanvasRaycasts(GameEvent e)
        {
            UICanvasFlag canvasFlag = (UICanvasFlag)e.data;
            BlockCanvasRaycasts(canvasFlag, false);
        }

        // 加载UI
        private void OnU_OpenUI(GameEvent e)
        {
            AddUI((string)e.data);
        }
        // 卸载UI
        private void OnU_CloseUI(GameEvent e)
        {
            if(AddedUIList.ContainsKey((string)e.data))
                ReMoveUI(AddedUIList[(string)e.data]);
        }

        // UI互斥操作，打开或者关闭UI
        private void OnU_OpenOrCloseUI(GameEvent e)
        {
            string uiName = (string)e.data;
            if (AddedUIList.ContainsKey(uiName))
            {
                ReMoveUI(uiName);
            }
            else
            {
                AddUI(uiName);
            }
        }

        /// <summary>
        ///  处理分发服务器下发的信息提示，提示类型可能有很多种，走马灯，气泡，提示框，等等！ 
        /// </summary>
        /// <param name="e">消息ID</param>[Lyq]
        private void OnMsgNotify(GameEvent e)   
        {
            try
            {
                SSendMsgNotify _smn = (SSendMsgNotify)e.data;
                string _msg = MsgIdManager.getMsgStr(_smn.msgid);
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString(_msg), canvasList[0]);
            }
            catch (System.Exception ex)
            {
                LogManager.Log(ex.ToString());
            }

        }
        /// <summary>
        /// 处理获得英雄之血
        /// </summary>
        private void OnGetHeroHp(GameEvent e)
        {
            string _str = GameUtils.getString(e.data.ToString());
            m_Str = _str.Substring(0, _str.Length - 2);
            string _text = string.Format(GameUtils.getString("common_bloodget_tip"), _str);           
            //InterfaceControler.GetInst().AddMsgBox(_text, canvasList[0]);
            Invoke("ShowHeroHpOpenText", 0.5f);
        }

        /// <summary>
        /// 显示获得英雄之血开启克隆文本
        /// </summary>
        private void ShowHeroHpOpenText()
        {
            string _text = string.Format(GameUtils.getString("heroclone_content5"), m_Str);
            //InterfaceControler.GetInst().AddMsgBox(_text, canvasList[0]);            
        }

        // 网络提示
        private void OnNetTips(GameEvent e)
        {
            string state = (string)e.data;
            UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
            ui.type = UI_GameTips.TipsType.SocketTips;
            ui.AddMsg(state);
        }

        // 通用提示
        private void OnGameTips(GameEvent e)
        {
            string state = (string)e.data;
            UI_GameTips ui = AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
            ui.type = UI_GameTips.TipsType.GameTips;
            ui.SetErrorTips();
            ui.AddMsg(state);
        }

        public Transform GetTopTransform()
        {
            return canvasList[0];
        }

    }
}