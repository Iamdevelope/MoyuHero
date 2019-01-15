using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DreamFaction.UI
{
    public class UI_StageMapScroll : ScrollRect
    {
        public enum ScrollDirection
        {
            kNone,  // 未设置
            kFront, // 向前
            kBack,  // 向后
            kOver,  // 不需设置
        };
        public float disBase = 0.3f;
        private int iCurIndex = 0;
        private float mBeginX = 0.0f;
        private int iMaxNum = 0;

        private Tweener mAction;

        private ScrollDirection mBeginDirection = ScrollDirection.kNone;   // 初始的方向
        private bool isHadChangeDirection;  // 是否已经更改过方向

        public delegate void onBeginCall();
        public delegate void onEndCall(int idx);

        public onBeginCall beginDelegate;
        public onEndCall endDelegate;

        void Start()
        {
            iMaxNum = content.childCount;
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (mAction!=null) mAction.Kill();
            base.OnBeginDrag(eventData);
            mBeginX = content.anchoredPosition3D.x;

            //Debug.Log("OnDrag .. " + eventData.ToString());
            do
            {
                if (eventData.delta.x > 0)
                {
                    // 向前
                    mBeginDirection = ScrollDirection.kFront;
                    if (iCurIndex == 0)
                    {
                        // 不能滑动
                        break;
                    }
                }
                else
                {
                    // 向后
                    mBeginDirection = ScrollDirection.kBack;
                    if (iCurIndex == iMaxNum - 1)
                    {
                        // 不能滑动
                        break;
                    }
                }
                mBeginDirection = ScrollDirection.kOver;
                if (beginDelegate != null)
                {
                    beginDelegate();
                }
            } while (false);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            if (iMaxNum <= 1)
            {
                return;
            }

            if (mBeginDirection != ScrollDirection.kOver)
            {
                if (eventData.delta.x > 0 && mBeginDirection == ScrollDirection.kBack)
                {
                    mBeginDirection = ScrollDirection.kOver;
                    if (beginDelegate != null)
                    {
                        beginDelegate();
                    }
                }
                else if (eventData.delta.x < 0 && mBeginDirection == ScrollDirection.kFront)
                {
                    mBeginDirection = ScrollDirection.kOver;
                    if (beginDelegate != null)
                    {
                        beginDelegate();
                    }
                }
            }
            //Debug.Log("OnDrag .. " + eventData.ToString());
        }

        public override void OnScroll(PointerEventData data)
        {
            base.OnScroll(data);
            //Debug.Log("OnScroll .. ");
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            if (iMaxNum <= 1)
            {
                return;
            }

            var distance = content.anchoredPosition3D.x - mBeginX;
            if (Mathf.Abs(distance) < 1f)
            {
                SetContentAnchoredPosition(new Vector3(iCurIndex * -2208, 0, 0));
                if (endDelegate != null) endDelegate(iCurIndex + 1);
                return;
            }
            bool isChange = Mathf.Abs(distance) - 2208 * disBase > 0;
            if (!isChange)
            {
                onMoveTo(iCurIndex);
                return;
            }
            if (distance > 0)
            {
                // 向后拉
                onMoveTo(iCurIndex - 1);
            }
            else if (distance < 0)
            {
                // 向前拉
                onMoveTo(iCurIndex + 1);
            }
        }

        public void onMoveTo(int idx)
        {
            iMaxNum = content.childCount;
            iCurIndex = idx;
            if (iCurIndex >= iMaxNum)
            {
                iCurIndex = iMaxNum-1;
            }
            else if (iCurIndex < 0)
            {
                iCurIndex = 0;
            }
            //Debug.Log(iCurIndex);
            mAction = content.DOAnchorPos3D(new Vector3(iCurIndex * -2208, 0, 0), 0.5f).OnComplete(() => {
                if (endDelegate != null) endDelegate(iCurIndex+1);
            });
        }

        public void setIdx(int idx)
        {
            iCurIndex = idx;
            content.anchoredPosition3D = new Vector3(iCurIndex * -2208, 0, 0);
        }
    }
}
