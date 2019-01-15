using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DreamFaction.UI.Core
{
    /// <summary>
    /// Loading场景的UI控制器继承自BaseUIControler，用来控制主场景的UI加载，删除！
    /// </summary>
    public class UI_LodingControler : BaseUIControler
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static UI_LodingControler Inst; 

        // =====================  重载 ============================
        /// <summary>
        /// 1: 初始化
        /// </summary>
        protected override void InitData()
        {
 	         base.InitData();
             Inst = this;
             AddUI(UI_Loading.UI_ResPath);
            
        }

        // ====================== 公共接口 =========================


    }
}
