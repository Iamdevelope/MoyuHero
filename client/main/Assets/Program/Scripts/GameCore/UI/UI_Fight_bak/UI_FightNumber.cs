using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DreamFaction.GameCore;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class Number : MonoBehaviour
    {
        private Image image = null;
        private void Awake()
        {
            image = gameObject.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
        }

        public void onStart(Sprite sprite)
        {
            image.color = new Color(1, 1, 1, 1);
            image.overrideSprite = sprite;
            image.SetNativeSize();
        }
    }
    public class FightNumber : BaseUI
    {
        public int iBitNumber = -1;
        public bool isShowing = false;
        [HideInInspector]
        public Sprite num_flag1 = null;
        [HideInInspector]
        public List<Sprite> mNums = new List<Sprite>(10);
        protected List<Number> mNumbers = new List<Number>();

        private CanvasGroup mGroup;
        protected GridLayoutGroup mGridGroup;

        private int offset = 0;
        private RectTransform rectTran = null;
        public override void InitUIData()
        {
            // 初始化
            mGroup = GetComponent<CanvasGroup>();
            mGridGroup = GetComponent<GridLayoutGroup>();
            rectTran = GetComponent<RectTransform>();
        }

        // 数字开始激活
        public virtual void onStart(string num, Vector3 v)
        {
            onReset(v);

            int size = num.Length;

            // 重置
            if (iBitNumber == -1)
            {
                iBitNumber = size;
            }


            Number[] nums = mGridGroup.transform.GetComponentsInChildren<Number>();
            int count = nums.Length;
            for (int i = 0; i < size; ++i)
            {
                char c = num[size - i - 1];
                int idx = int.Parse(c.ToString());

                if (mNumbers.Count <= i)
                {
                    GameObject obj = new GameObject("Number");
                    Number sN = obj.AddComponent<Number>();
                    sN.transform.SetParent(mGridGroup.transform, false);
                    mNumbers.Add(sN);
                }

                mNumbers[i].onStart(Instantiate(mNums[idx]) as Sprite);
            }
            onStartAni();
        }

        protected void onReset(Vector3 v)
        {
            isShowing = true;
            mGroup.alpha = 1;
            mGroup.transform.localScale = new Vector3(3, 3, 1);
            gameObject.SetActive(true);
            //transform.position = new Vector3(v.x, v.y, 0);
            SetPosition(v);
        }
        protected virtual void onStartAni()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(mGroup.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutCubic));
            mySequence.Append(mGroup.transform.DOLocalMoveY(mGroup.transform.localPosition.y + 100, 0.7f).SetEase(Ease.OutCubic));
            mySequence.AppendCallback(onPlayEnd);
            mGroup.DOFade(0, 1.0f);
        }

        // 播放完成
        protected virtual void onPlayEnd()
        {
            isShowing = false;
            gameObject.SetActive(false);
        }

        private Vector3 WorldToUIPoint(Camera camera, Vector3 worldPos)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);
            //pos.z = 0f;   //z一定要为0.
            pos = camera.ScreenToWorldPoint(pos);
            return pos;
        }

        public void SetPosition(Vector3 vPos)
        {
            Vector3 v = WorldToUIPoint(UI_FightControler.Inst.GetCanvasCamera(BaseUIControler.UICanvasFlag.Canvas3), vPos);

            if (v.y > 5 - 0.9)
            {
                v = new Vector3(v.x, 5 - 0.9f, 0);
                offset = 0;
            }

            transform.position = new Vector3(v.x, v.y, 0);


            int x = (int)rectTran.anchoredPosition.x;
            int y = (int)rectTran.anchoredPosition.y;

            rectTran.anchoredPosition3D = new Vector3(x, y + offset, 0);
            //rectTran.anchoredPosition = new Vector2(x, y + offset);
        }
    }

    // 英雄伤害数字
    public class HeroNumber : FightNumber
    {
        public override void InitUIData()
        {
            base.InitUIData();
            num_flag1 = UIResourceMgr.LoadSprite("UI/Number/hurt_hero/-");
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/0"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/1"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/2"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/3"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/4"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/5"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/6"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/7"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/8"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_hero/9"));
        }
        public override void onStart(string num, Vector3 v)
        {
            base.onStart(num, v);

            //添加加血符号
            int size = num.Length;

            // 重置
            iBitNumber = size + 1;

            Number[] nums = mGridGroup.transform.GetComponentsInChildren<Number>();
            int count = nums.Length;

            if (count == size)
            {
                // 未添加（+）
                GameObject obj = new GameObject("Number");
                Number sN = obj.AddComponent<Number>();
                sN.transform.SetParent(mGridGroup.transform, false);
                mNumbers.Add(sN);
                mNumbers[count].onStart(Instantiate(num_flag1) as Sprite);
            }

        }
    }

    // 怪物伤害数字
    public class MonsterNumber : FightNumber
    {
        public override void InitUIData()
        {
            base.InitUIData();
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/0"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/1"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/2"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/3"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/4"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/5"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/6"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/7"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/8"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/hurt_monster/9"));
        }

    }

    // 治疗数字
    public class HealNumber : FightNumber
    {
        public override void InitUIData()
        {
            base.InitUIData();
            num_flag1 = UIResourceMgr.LoadSprite("UI/Number/heal/+");
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/0"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/1"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/2"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/3"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/4"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/5"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/6"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/7"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/8"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heal/9"));
        }

        public override void onStart(string num, Vector3 v)
        {
            base.onStart(num, v);

            //添加加血符号
            int size = num.Length;

            // 重置
            iBitNumber = size + 1;

            Number[] nums = mGridGroup.transform.GetComponentsInChildren<Number>();
            int count = nums.Length;

            if (count == size)
            {
                // 未添加（+）
                GameObject obj = new GameObject("Number");
                Number sN = obj.AddComponent<Number>();
                sN.transform.SetParent(mGridGroup.transform, false);
                mNumbers.Add(sN);
                mNumbers[count].onStart(Instantiate(num_flag1) as Sprite);
            }

        }
    }

    // 暴击数字
    public class HeavyNumber : FightNumber
    {
        public override void InitUIData()
        {
            base.InitUIData();
            num_flag1 = UIResourceMgr.LoadSprite("UI/Number/heavy/flag");
            //num_flag1 = UIResourceMgr.LoadSprite("UI/Number/heavy/flag");
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/0"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/1"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/2"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/3"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/4"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/5"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/6"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/7"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/8"));
            mNums.Add(UIResourceMgr.LoadSprite("UI/Number/heavy/9"));

        }

        public override void onStart(string num, Vector3 v)
        {
            base.onStart(num, v);

            //添加加血符号
            int size = num.Length;

            // 重置
            iBitNumber = size + 1;

            Number[] nums = mGridGroup.transform.GetComponentsInChildren<Number>();
            int count = nums.Length;

            if (count == size)
            {
                // 未添加（暴击）

                GameObject obj = new GameObject("Number");
                Number sN = obj.AddComponent<Number>();
                sN.transform.SetParent(mGridGroup.transform, false);
                GameObject imageObj = new GameObject("baoji");
                Image image = imageObj.AddComponent<Image>();
                image.sprite = Instantiate(num_flag1) as Sprite;
                image.SetNativeSize();
                RectTransform rect = imageObj.GetComponent<RectTransform>();
                rect.pivot = new Vector2(1.0f, 0.5f);
                rect.anchorMin = new Vector2(1, 0.5f);
                rect.anchorMax = new Vector2(1, 0.5f);
                rect.localPosition = new Vector3(0, 0, 0);
                //rect.sizeDelta = new Vector2(116, 64);
                imageObj.transform.SetParent(obj.transform, false);

                mNumbers.Add(sN);
                //mNumbers[count].onStart(Instantiate(num_flag1) as Sprite);
            }

        }

    }

    // 未命中
    public class MissNumber : FightNumber
    {
        public override void InitUIData()
        {
            base.InitUIData();
            num_flag1 = UIResourceMgr.LoadSprite(common.defaultPath + "Battle_Miss");
        }

        public override void onStart(string num, Vector3 v)
        {
            onReset(v);

            // 未添加（闪避）
            if (iBitNumber == -1)
            {
                GameObject obj = new GameObject("Miss");
                Number sN = obj.AddComponent<Number>();
                sN.transform.SetParent(mGridGroup.transform, false);
                GameObject imageObj = new GameObject("Miss");
                Image image = imageObj.AddComponent<Image>();
                image.sprite = Instantiate(num_flag1) as Sprite;
                image.SetNativeSize();
                RectTransform rect = imageObj.GetComponent<RectTransform>();
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchorMin = new Vector2(1, 0.5f);
                rect.anchorMax = new Vector2(1, 0.5f);
                rect.localPosition = new Vector3(0, 0, 0);
                //rect.sizeDelta = new Vector2(116, 64);
                imageObj.transform.SetParent(obj.transform, false);

                mNumbers.Add(sN);
                iBitNumber = 0;
            }
            onStartAni();
        }
    }
}
