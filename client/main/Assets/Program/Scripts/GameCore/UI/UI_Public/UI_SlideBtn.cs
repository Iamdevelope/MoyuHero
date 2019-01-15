using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DG.Tweening;
namespace DreamFaction.UI
{ 
    public delegate void Handle(); //无参回调
    /// <summary>
    /// 按钮拉伸向上移动功能类共有变量需要手动拖拽上去 注：按钮的锚点需要设置为0.5,1 背景图片的锚点需要设置为0.5,0 子节点的顺序需要按照显示顺序添加
    /// </summary>
    public class UI_SlideBtn : BaseUI
    {
        public enum MoveState
        {
            Null,
            Play,
            Pause,
        }
        public List<Transform> ItemList;                                                        //需要滑动的子节点
        public GameObject DirectionImageDown;                                                            //方向图片
        public GameObject DirectionImageUp;                                                            //方向图片
        public RectTransform BgRect;                                                            //背景框
        public float Slidtime;                                                                  //滑动所需要的时间
        public float PaddingY;

        private RectTransform DirectionImageRect;                                               //方向图片缩放信息
        public RectTransform MaxRect;                                                          //最上边按钮Rect
        private Vector3 DirectionImageRectScale;                                                //转向用图片
        public MoveState Movestate;                                                            //移动状态
        private float StartPos;                                                                 //记录初始位置Y坐标
        private int ItemCount;                                                                  //子节点个数
        public bool isUp = false;
        public event Handle closeFinish;// 当关闭时 回调
        public event Handle openFinish;// 当展开时 回调
        
        public override void InitUIData()
        {
            if (ItemList.Count > 0)
            {
                ItemCount = ItemList.Count;
                StartPos = ItemList[0].GetComponent<RectTransform>().anchoredPosition3D.y;
                for (int i = 0; i < ItemCount; ++i)
                {
                    ItemList[i].gameObject.SetActive(false);
                }
                if (isUp)
                    MaxRect = ItemList[0].GetComponent<RectTransform>();
                else
                    MaxRect = ItemList[0].GetComponent<RectTransform>();
            }
            //if (DirectionImageDown != null)
            //    DirectionImageRect = DirectionImageDown.GetComponent<RectTransform>();
            //DirectionImageRectScale = new Vector3(1, -1, 1);
            SetMoveState(MoveState.Pause);
            SetBgActive(false);
        }

        public override void UpdateUIView()
        {
            if (isUp)
            {
                if (BgRect != null && Movestate != MoveState.Null)
                {
                    BgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(BgRect.anchoredPosition3D.y - MaxRect.anchoredPosition3D.y + MaxRect.sizeDelta.y));
                }
            }
            else
            {
                if (BgRect != null && Movestate != MoveState.Null)
                {
                    BgRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(MaxRect.anchoredPosition3D.y - BgRect.anchoredPosition3D.y));
                }
            }

            if (Movestate == MoveState.Play)
            {
                if (Input.GetMouseButton(0))
                {
                    OnCloseing();
                }
            }
        }

        //按钮触发函数
        public void OnStart()
        {
            switch (Movestate)
            {
                case MoveState.Pause:
                case MoveState.Null:
                    OnOpen();
                    break;
                case MoveState.Play:
                    OnClose();
                    break;
            }
        }

        public void OnOpen()
        {
            SetMoveState(MoveState.Play);
            SetBgActive(true);
            //if (DirectionImageRect != null)
            //    DirectionImageRect.localScale = DirectionImageRectScale;

            for (int i = 0; i < ItemCount; ++i)
            {
                ItemList[i].gameObject.SetActive(true);
                Transform temp = ItemList[i];
                ItemList[i].DOPause();
                if (isUp)
                    ItemList[i].DOLocalMoveY(StartPos - GetMaxHeight(i), Slidtime);
                else
                    ItemList[i].DOLocalMoveY(StartPos - GetMaxHeight(i), Slidtime);
            }
            DirectionImageUp.SetActive(true);
            DirectionImageDown.SetActive(false);
            if (openFinish != null)
                openFinish();
        }

        public void OnClose()
        {
            SetMoveState(MoveState.Pause);
            //if (DirectionImageRect != null)
            //    DirectionImageRect.localScale = Vector3.one;

            for (int i = 0; i < ItemCount; ++i)
            {
                Transform temp = ItemList[i];
                ItemList[i].DOPause();
                if (isUp)
                    ItemList[i].DOLocalMoveY(StartPos, Slidtime).OnComplete(() => OnPlayEnd(temp));
                else
                    ItemList[i].DOLocalMoveY(StartPos, Slidtime).OnComplete(() => OnPlayEnd(temp));
            }
            //if (UI_HeroListManager._instance!=null)
            //{
            //    for (int i = 0; i < UI_HeroListManager._instance.heroList.Count; i++)
            //    {
            //        UI_HeroListManager._instance.heroList[i].m_NewHeroTag.SetActive(false);
            //    }
            //}

        }

        void OnCloseing()
        {
            if (DirectionImageRect != null)
                DirectionImageRect.localScale = Vector3.one;
            for (int i = 0; i < ItemCount; ++i)
            {
                Transform temp = ItemList[i];
                ItemList[i].DOPause();
                if (isUp)
                    ItemList[i].DOLocalMoveY(StartPos, Slidtime).OnComplete(() => OnPlayEnd(temp));
                else
                    ItemList[i].DOLocalMoveY(StartPos, Slidtime).OnComplete(() => OnPlayEnd(temp));
            }
            DirectionImageUp.SetActive(false);
            DirectionImageDown.SetActive(true);
            if (closeFinish != null)
                closeFinish();
        }
        private void SetMoveState(MoveState value)
        {
            Movestate = value;
        }
        private float GetMaxHeight(int count)
        {
            float _count = 0;
            for (int i = count; i < ItemCount; ++i)
            {
                _count += ItemList[i].GetComponent<RectTransform>().rect.height + PaddingY;
            }
            return _count;
        }
        private void OnPlayEnd(Transform temp)
        {
            if (temp == ItemList[0])
                SetMoveState(MoveState.Null);
            SetBgActive(false);
            temp.gameObject.SetActive(false);
        }
        private void SetBgActive(bool isActive)
        {
            if (BgRect != null)
                BgRect.gameObject.SetActive(isActive);
        }
    }
}

