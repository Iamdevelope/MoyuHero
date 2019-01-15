using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using UnityEngine.EventSystems;
using DreamFaction.Utils;

public class LoadPictureRound : BaseUI 
{
        //[Range(0f, 5f)]
       // public  float DelayTime = 2f;
        private Image aroundImag; // 转圈的图片

        // ===================== 继承 ==================
        // 1: 初始化UI数据操作
        public override void InitUIData()
        {
            base.InitUIData();
        
            aroundImag = selfTransform.GetComponent<Image>();
           
        }


        // 3：更新UI显示
        public override void UpdateUIView()
        {
            base.UpdateUIView();   
            aroundImag.transform.Rotate(0.0f, 0.0f, -2f);
        }

}
