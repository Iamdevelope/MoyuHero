using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using System.IO;
using System.Xml;
using DreamFaction.GameEventSystem;
using Platform;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_AssetsLoading : BaseUI
    {
        public static UI_AssetsLoading Inst;
        public static string UI_ResPath = "UI_Loding/UI_AssetsLoading_2_0";
        private Image BackImg; // 背景图
        private Image FillImage;// 填充用的图
        private Text PercentageText; // 百分比Text
        private Text ReleaseText; // 版本Text
        private Text DownSpeed;  //下载速度

        public string LocalVersionNum;
        private string ServerVersionNum;
        private int WaitTime = 0;

        // ===================== 继承 ==================
        public override void InitUIData()
        {
            base.InitUIData();
            Inst = this;
            BackImg = selfTransform.FindChild("BackImg").GetComponent<Image>();
            FillImage = selfTransform.FindChild("Fill").GetComponent<Image>();
            FillImage.fillAmount = 0;
            PercentageText = selfTransform.FindChild("PercentageText").GetComponent<Text>();
            ReleaseText = selfTransform.FindChild("ReleaseText").GetComponent<Text>();
            DownSpeed = selfTransform.FindChild("downSpeedText").GetComponent<Text>();
        }

        public override void InitUIView()
        {
            base.InitUIView();
            if (UI_LoginControler.Inst != null)
            {
                ReleaseText.text = "V " + ConfigsManager.Inst.GetClientConfig(ClientConfigs.Version);
            }
            
        }

        // 更新UI显示进度
        public override void UpdateUIView()
        {
            base.UpdateUIView();
            if (AssetManager.Inst.NeedDownFilesCount != 0)
            {
                FillImage.fillAmount = (float)(AssetManager.Inst.NeedDownFilesCount - (float)AssetManager.Inst.CurrentDownFilesCount) / (float)(AssetManager.Inst.NeedDownFilesCount);
                PercentageText.text = Mathf.FloorToInt((float)(AssetManager.Inst.NeedDownFilesCount - AssetManager.Inst.CurrentDownFilesCount) / (float)(AssetManager.Inst.NeedDownFilesCount) * 100).ToString() + "%";
            }
            if (AssetManager.Inst.CurrentDownFilesCount == 0)
            {
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_ResPath);
                //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_LoginWin.UI_ResPath);
            }
           if (AssetManager.Inst.DonwLoadSpeed > 1024)
           {
               DownSpeed.text = (AssetManager.Inst.DonwLoadSpeed / 1024).ToString("0.0") + "MB/S";
           }
           else if (AssetManager.Inst.DonwLoadSpeed < 1024 && AssetManager.Inst.DonwLoadSpeed > 1)
           {
               DownSpeed.text = AssetManager.Inst.DonwLoadSpeed.ToString("0.0") + "KB/S";
           }
           else if (AssetManager.Inst.DonwLoadSpeed < 1)
           {
               DownSpeed.text = (AssetManager.Inst.DonwLoadSpeed * 1024).ToString("0.0") + "B/S";
           } 
        }

        void OnDestroy()
        {
            Inst = null;
        }

    }
}
