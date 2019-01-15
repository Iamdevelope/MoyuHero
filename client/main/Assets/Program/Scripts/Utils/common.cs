using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
///  动态精灵资源加载路径集合。凡是和路径相关的静态对象统一生成在此处。全局静态变量请放在GlobalMembers类中[8/10/2015 Zmy]
/// </summary>
namespace DreamFaction.Utils
{
    class common
    {
        //所有动态加载的图片精灵全部放在一个目录下
        public static string uiPath = "UI/";

        public static string defaultPath = "UI/Sprites/";
        //数字图片资源
        public static string numberPath = "UI/Number";
        //默认Pref路径
        public static string prefabPath = "UI/Prefabs/";


        //默认特效路径
        public static string EffectPath = "UI/Prefabs/Effects/";
        public static string EleBody = "ElementBody/";

        //默认音效路径
        public static string AudioPath = "Audio/";
    }
}
