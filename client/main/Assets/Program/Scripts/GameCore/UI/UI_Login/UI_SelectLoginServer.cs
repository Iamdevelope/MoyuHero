using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using System.Collections.Generic;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using Platform;

namespace DreamFaction.UI
{
    public class UI_SelectLoginServer : BaseUI
    {
        //隐藏到显示所需时间;
        public const float AlphaDelta = 0.5f;

        public static string UI_ResPath = "UI_Login/UI_SelectLoginServer_2_1";
        private Button QuickPlay_Btn; // 快速游戏 按钮
        private Button Regist_Btn; // 注册 按钮
        public static bool m_StarShow = false;
        private Component[] _comps;
        private float AlphaValue = 0;
        // ===================== 继承 ==================
        public override void InitUIData()
        {
            base.InitUIData();
            QuickPlay_Btn = selfTransform.FindChild("QuickPlay_Btn").GetComponent<Button>();
            Regist_Btn = selfTransform.FindChild("Regist_Btn").GetComponent<Button>();
            QuickPlay_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickQuickPlayBtn));
            Regist_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRegistBtn));

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
                }
            }

        }
        // ===================== 按钮回调 =================
        // 快速游戏 按钮回调
        private void OnClickQuickPlayBtn()
        {
            CGuest msgLogin = new CGuest();
            msgLogin.device_key = AppManager.Inst.DeviceUniqueIdentifier;
            IOControler.GetInstance().SendPlatform(msgLogin);
        }


        // 注册 按钮回调
        private void OnClickRegistBtn() 
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_SelectLoginServer.UI_ResPath);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, AccountBinding.UI_ResPath);
        }
    }


}
