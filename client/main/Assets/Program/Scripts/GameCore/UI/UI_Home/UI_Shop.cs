using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;

namespace DreamFaction.UI
{
    /// <summary>
    /// 商店界面，继承自BaseUI
    /// </summary>
    public class UI_Shop : BaseUI
    {
        private Button backBtn;
        public static string UI_ResPath = "UI_Home/UI_Shop_2_0";

        public override void InitUIData()
        {
            base.InitUIData();
            backBtn = selfTransform.FindChild("Back_Btn").GetComponent<Button>();
            backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
        }
        // 1：准备播放进场动画
        public override void OnPlayingEnterAnimation()
        {
            //transform.localScale = new Vector3(0, 0, 0);
        }

        // 2: 准备删除UI
        public override void OnReadyForClose()
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }


        // 3: 更新UI显示
        public override void UpdateUIView()
        {
            //if (UIState == UIStateEnum.PlayingEnterAnimation)
            //{
            //    transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            //    if (transform.localScale.x >= 1.0f)
            //    {
            //        UIState = UIStateEnum.PlayingEnterAnimationOver;
            //    }
            //}
            if (UIState == UIStateEnum.PlayingExitAnimation)
            {
                transform.position += new Vector3(0.1f, 0.00f, 0.00f);
                if (transform.position.x > -20)
                {
                    UIState = UIStateEnum.PlayingExitAnimationOver;
                }
            }
        }
        private void OnClickBackBtn()
        {
            UIState = UIStateEnum.PlayingExitAnimation;
        }
    }

}