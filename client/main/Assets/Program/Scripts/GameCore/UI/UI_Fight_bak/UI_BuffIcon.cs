using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.SkillCore;

namespace DreamFaction.UI
{
    public class UI_BuffIcon : BaseUI
    {
        private int m_iBuffID = 1;
        public int GetBuffID() { return m_iBuffID; }

        private Text mText = null; // 数量显示
        public void onImageInit(Sprite sprite)
        {
            gameObject.AddComponent<Image>().overrideSprite = sprite;
        }

        public void onUpdateText(int count)
        {
            if (count <2)
            {
                // 累加大于2时显示数字
                if (mText) mText.gameObject.SetActive(false);
                return;
            }
            if (mText == null)
            {
                GameObject obj = new GameObject("Number");
                mText = obj.AddComponent<Text>();
                mText.rectTransform.pivot = new Vector2(1, 0);
                obj.transform.SetParent(transform, false);
            }
            mText.gameObject.SetActive(true);
            mText.text = count.ToString();
        }
    }
}
