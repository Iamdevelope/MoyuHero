using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;

namespace DreamFaction.UI
{

    /// <summary>
    /// 系统提示框1，继承自BaseUI
    /// </summary>
    public class UI_GameTips : BaseUI
    {
        //提示板显示内容的类型
        public enum TipsType
        {
            Nomle = 0,
            SocketTips = 1,            //网络异常提示
            Recruit = 2,           //玩家无英雄，提示招募
            NotEnoughFightCount = 3,            //剩余挑战次数不足提示
            AccountBindingOk = 4,            //绑定账号Ok
            GameTips = 5,                // 通用提示
            GoPlatform,                  //跳转到平台
            StartLoadUpdataAsset,        //开始下载更新资源
            CancelOrGoPlatform,          //取消更新资源或去平台更新
            ResourceDownloadUnOk,       //资源下载异常
        }

        public static string UI_ResPath = "UI_MsgBox/UI_MsgBox_0_1";
        public static UI_GameTips Inst;
        public TipsType type = TipsType.Nomle;
        private Button YesBtn = null; //“是”按钮(TipsType.Recruit)
        private Button CancelBtn = null;//“否”按钮(TipsType.Recruit)
        private GameObject background = null;
        private Text BtnTxt = null; // 按钮文本(TipsType.Award)
        private Text YesBtnText = null;//“是”按钮文本(TipsType.Recruit)
        private Text CancelBtnText = null;//“否”按钮文本(TipsType.Recruit)
        private Queue<string> msgQueue; // 消息队列
        private Text msgTxt = null; // 消息文本框

        private Button payBtn = null;
        private Text payText = null;

        // ================ 继承 =======================
        // 初始化UI数据和绑定关系等
        public override void InitUIData()
        {
            base.InitUIData();
            Inst = this;
            msgQueue = new Queue<string>();

            YesBtn = selfTransform.FindChild("ButtonOK").GetComponent<Button>();
            CancelBtn = selfTransform.FindChild("ButtonCancel").GetComponent<Button>();
            YesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickYesBtn));
            CancelBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCancelBtn));
            background = selfTransform.FindChild("Background").gameObject;
            YesBtnText = selfTransform.FindChild("ButtonOK/Text").GetComponent<Text>();
            CancelBtnText = selfTransform.FindChild("ButtonCancel/Text").GetComponent<Text>();
            payBtn = selfTransform.FindChild("ButtonPay").GetComponent<Button>();

            payBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPayBtn));
            payText = selfTransform.FindChild("ButtonPay/Text").GetComponent<Text>();
            msgTxt = selfTransform.FindChild("Image/msg_txt").GetComponent<Text>();

            // 屏蔽除了canvas0层之外图层的UI点击事件！
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_UnBlockCanvasRaycasts, BaseUIControler.UICanvasFlag.Canvas0);
        }

        // 初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();
            switch (type)
            {
                case TipsType.SocketTips:
                    SetFightCountTip(false);
                    SetRecruitTip(true);
                    gameObject.SetActive(true);
                    break;
                case TipsType.Recruit:
                    SetFightCountTip(false);
                    SetRecruitTip(true);
                    break;
                case TipsType.NotEnoughFightCount:
                    SetRecruitTip(false);
                    SetFightCountTip(true);
                    break;
                case TipsType.AccountBindingOk:
                    CancelBtn.gameObject.SetActive(false);
                    YesBtn.gameObject.SetActive(true);
                    payBtn.gameObject.SetActive(false);
                     YesBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-13, -115);
                    break;
                case TipsType.GameTips:
                    {
                        CancelBtn.gameObject.SetActive(false);
                        YesBtn.gameObject.SetActive(true);
                        payBtn.gameObject.SetActive(false);
                        YesBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-13, -115);
                    }
                    break;
                case TipsType.StartLoadUpdataAsset:
                    {
                        CancelBtn.gameObject.SetActive(false);
                        YesBtn.gameObject.SetActive(true);
                        payBtn.gameObject.SetActive(false);
                        YesBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-13, -115);
                    }
                    break;
                case TipsType.GoPlatform:
                    {
                        SetFightCountTip(false);
                        SetRecruitTip(true);
                        gameObject.SetActive(true);
                    }
                    break;
                case TipsType.CancelOrGoPlatform:
                    {
                        SetFightCountTip(false);
                        SetRecruitTip(true);
                        gameObject.SetActive(true);
                    }
                    break;
                case TipsType.ResourceDownloadUnOk:
                    {
                        CancelBtn.gameObject.SetActive(false);
                        YesBtn.gameObject.SetActive(true);
                        payBtn.gameObject.SetActive(false);
                        YesBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-13, -115);
                    }
                    break;
                default:
                    SetRecruitTip(false);
                    SetFightCountTip(false);
                    break;
            }


            ResetMsgTxt();
        }

        public void SetErrorTips()
        {
            CancelBtn.gameObject.SetActive(false);
            YesBtn.gameObject.SetActive(true);
            payBtn.gameObject.SetActive(false);
            YesBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-13, -115);
        }


        private void SetRecruitTip(bool isActive)
        {
            YesBtn.gameObject.SetActive(isActive);
            CancelBtn.gameObject.SetActive(isActive);
            //background.SetActive(isActive);
        }
        private void SetFightCountTip(bool isActive)
        {
            payBtn.gameObject.SetActive(isActive);
            CancelBtn.gameObject.SetActive(isActive);
        }

        // 2: 界面关闭后显示UICanvas
        void OnDestroy()
        {
            // 开启除了canvas0层之外图层的UI点击事件！
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_BlockCanvasRaycasts, BaseUIControler.UICanvasFlag.Canvas0);
            Inst = null;
        }

        // 准备关闭UI
        public override void OnReadyForClose()
        {
            base.OnReadyForClose();
            if (msgQueue.Count > 0)
            {
                ResetMsgTxt();
                UIState = UIStateEnum.StandBy; // 重置UI状态到StandBy
                return;
            }
            selfTransform.parent.parent.GetComponent<BaseUIControler>().ReMoveUI(gameObject);
        }

        // 设置信息内容和操作方式
        public void AddMsg(string msg)
        {
            msgQueue.Enqueue(msg);
        }

        // ================== 私有接口 ==================
        // 1：更新Msg文本内容
        private void ResetMsgTxt()
        {
            switch (type)
            {
                case TipsType.SocketTips:
                    if (msgQueue.Count > 0)
                    {
                        string msg = msgQueue.Dequeue();
                        msgTxt.text = msg;
                        YesBtnText.text = GameUtils.getString("common_button_ok1"); //"确  定";
                        CancelBtnText.text = GameUtils.getString("common_button_cancel1");//取消
                    }
                    break;
                case TipsType.Recruit:
                    msgTxt.text = GameUtils.getString("hero_info_null");
                    YesBtnText.text = GameUtils.getString("heromelt_button8");
                    CancelBtnText.text = GameUtils.getString("common_button_cancel");
                    break;
                case TipsType.NotEnoughFightCount:
                    int vipLevel = ObjectSelf.GetInstance().VipLevel;
                    VipTemplate pRow = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLevel);
                    payText.text = "99";
                    CancelBtnText.text = GameUtils.getString("common_button_cancel");

                    msgTxt.text = GameUtils.getString("fight_stagepurchase_form_content")
                                    + string.Format("<size=40><color=#F7F709> {0}</color></size>", pRow.getRapidClearBuyTimes());
                    break;
                case TipsType.AccountBindingOk:
                    msgTxt.text = "绑定成功!" + "<color=#1E90FF>" +  ConfigsManager.Inst.GetClientConfig(ClientConfigs.State) + "</color>" + "的信息已转移至注册账号";
                    YesBtnText.text = GameUtils.getString("common_button_ok1");
                    break;
                case TipsType.GameTips:
                    {
                        msgTxt.text = msgQueue.Dequeue();
                    }
                    break;
                case TipsType.GoPlatform:
                    {
                        msgTxt.text = "当前客户端版本需要更新，点击确定进入更新界面";
                        YesBtnText.text = "确  定";//GameUtils.getString("common_button_ok1"); //"确  定";
                        CancelBtnText.text = "取 消";//GameUtils.getString("common_button_cancel1");//取消
                    }
                    break;
                case TipsType.CancelOrGoPlatform:
                    {
                        msgTxt.text = "当前客户端版本需要更新，点击确定进入更新界面";
                        YesBtnText.text = "确  定";// GameUtils.getString("common_button_ok1"); //"确  定";
                        CancelBtnText.text = "取 消";// GameUtils.getString("common_button_cancel1");//取消
                    }
                    break;
                case TipsType.StartLoadUpdataAsset:
                    {
                        string _fileSize = AssetManager.Inst.GetNeedLoadFileSize();
                        msgTxt.text = "有" + "<color=#FF0000>" + _fileSize + "</color>" + "资源需要更新，点击确定开始加载";
                        YesBtnText.text = "确  定";//GameUtils.getString("common_button_ok1"); //"确  定";
                    }
                    break;
                case TipsType.ResourceDownloadUnOk:
                    {
                        msgTxt.text = "下载异常，请重新登录开始下载";
                        YesBtnText.text = "确  定";//GameUtils.getString("common_button_ok1"); //"确  定";
                    }
                    break;
                default:
                    break;
            }
        }

        // ===================== 按钮回调 =================
        private void OnClickYesBtn()
        {
            if (type == TipsType.GameTips)
            {
                if (!SceneManager.Inst.CurScene.Equals(SceneEntry.Login.ToString()))
                {
                    GameObject.Destroy(MainGameControler.Inst.gameObject.GetComponent<ObjectSelf>());
                    SceneManager.Inst.StartChangeScene(SceneEntry.Login.ToString());
                }
                else
                {
                    HideUI();
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_ReActive);
                }

                SocketManager.GetInstance().Uninit();

                return;
            }

            if (type == TipsType.SocketTips)
            {
                string ServerID = ConfigsManager.Inst.GetClientConfig(ClientConfigs.ServerID);
                if(ServerID != string.Empty)
                {
                    ServerListConfig serverconfig = ConfigsManager.Inst.GetServerList(ServerID);
                    if(serverconfig!=null)
                        IOControler.Connect(serverconfig.ServerIP, (ushort)serverconfig.ServerPort);
                }
                else
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, GameUtils.getString("error #100053"));//serverid 错误
                }
            }

            if (type == TipsType.Recruit)
            {
                //招募界面
                UI_HomeControler.Inst.AddUI(UI_Recruit.UI_ResPath);
            }

            if (type == TipsType.GoPlatform || type == TipsType.CancelOrGoPlatform)
            {
                //Debug.Log("GoPlatform");
                Application.OpenURL("www.baidu.com");
            }

            if (type == TipsType.StartLoadUpdataAsset)
            {
                AssetManager.Inst.StartLoadAsset();
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_AssetsLoading.UI_ResPath);     
            }

            if (type == TipsType.ResourceDownloadUnOk)
            {
                Debug.Log("Quit");
                Application.Quit();
            }

            if ( UI_HomeControler.Inst != null )
            {
                UI_HomeControler.Inst.ReMoveUI ( gameObject );
            }
            else if (UI_LoginControler.Inst != null)
            {
                UI_LoginControler.Inst.ReMoveUI(gameObject);
            }
            else if ( UI_FightControler.Inst != null )
            {
                UI_FightControler.Inst.ReMoveUI ( gameObject );
            }
            else if ( UI_LodingControler.Inst != null )
            {
                UI_LodingControler.Inst.ReMoveUI ( gameObject );
            }
            else
            {
                Debug.LogError ( "destory game tips error" );
            }
        }
        private void OnClickCancelBtn()
        {
            if (type == TipsType.SocketTips)
            {
                if (!SceneManager.Inst.CurScene.Equals(SceneEntry.Login.ToString()))
                {
                    GameObject.Destroy(MainGameControler.Inst.gameObject.GetComponent<ObjectSelf>());
                    SceneManager.Inst.StartChangeScene(SceneEntry.Login.ToString());
                }
                else
                {
                    HideUI();
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_ReActive);
                }
            }
            if (type == TipsType.GoPlatform)
            {
                Debug.Log("Quit");
                Application.Quit();
            }

            if (type == TipsType.CancelOrGoPlatform)
            {
                Debug.Log("CancelOrGoPlatform");
                AssetManager.Inst.NeedDownFiles.Clear();
                AssetManager.Inst.StartLoadAsset();
            }

            if (UI_HomeControler.Inst != null)
            {
                UI_HomeControler.Inst.ReMoveUI(gameObject);
            }
            else if (UI_LoginControler.Inst != null)
            {
                UI_LoginControler.Inst.ReMoveUI(gameObject);
            }
            else
            {
                UI_FightControler.Inst.ReMoveUI(gameObject);
            }
        }
        private void OnClickPayBtn()
        {
            //print("点击购买按钮");
            OnClickCancelBtn();
        }

        void HideUI()
        {
            gameObject.SetActive(false);
        }
    }
}
