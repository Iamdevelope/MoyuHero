using UnityEngine;
using System.Collections;
namespace DreamFaction.UI
{
    /// <summary>
    /// Tips基类
    /// </summary>
    public class BaseUITips
    {
        protected string Title;                                //提示标题
        protected string Describe;                             //提示描述
        //组合字符串
        public virtual string SetShow()
        {
            return string.Empty;
        }

    }
}

