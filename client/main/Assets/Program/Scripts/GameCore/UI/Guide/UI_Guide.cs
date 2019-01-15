using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.UI;
using UnityEngine.Events;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using System.Collections.Generic;

public class UI_Guide : UI_GuideBase
{

    public static UI_Guide inst = null;
    public static string UI_ResPath = "Guide/UI_Guide_0_5";

    NewbieguideTemplate m_NewTemp;

    int canvasWidth = 2208;
    int canvasHeight = 1242;

    GameObject m_BtnGuide;
    public Canvas canvas;
    Canvas curCanvas;

    public bool m_Continue = false;  // 点击任意位置继续

    private string m_GotoPath = string.Empty;   //需要跳转的路径

    public int interruptID = GuideManager.GetInstance().interruptID;
    public List<int> guideidList = new List<int>();

    public override void InitUIData()
    {
        inst = this;
        base.InitUIData();
        m_BtnGuide = selfTransform.FindChild("TipsImage/BtnGuide").gameObject; 
        //curCanvas = UI_HomeControler.Inst.GetCanvasByIdx(1);
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    void OnDestroy()
    {
        inst = null;
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
    }

    /// <summary>
    /// 引导对话框
    /// 在配置表里面填写相应的屏幕坐标
    /// </summary>
    public void GuideWithInfo(NewbieguideTemplate temp)
    {
        m_NewTemp = temp;
        guideidList.Add(m_NewTemp.GetID());
        GetCurCanvas();

        // 女孩设置位置
        //string url = "UI/Sprites/";
        //m_Girl.sprite = UIResourceMgr.LoadSprite(url + m_NewTemp.getGuide());
        if (m_NewTemp.getNpc_x() != -1)
        {
            m_Girl.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getNpc_x(), m_NewTemp.getNpc_y());
            m_Girl.gameObject.SetActive(true);
        }
        else
        {
            m_Girl.gameObject.SetActive(false);
        }

        // 对话框 位置
        m_TipsImage.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getTalk_box_re_npc_x(), m_NewTemp.getTalk_box_re_npc_y());
        m_TipsImage.rectTransform.sizeDelta = new Vector2(m_NewTemp.getTalk_box_w(), m_NewTemp.getTalk_box_h());

        // 背景遮罩
        //if (m_NewTemp.getBackground_shade() == -1)
        //{

        //}

        // 对话框提示内容
        string str = "";
        if (temp.GetID() == 100201 || temp.GetID() == 100501)
        {
            str = string.Format(GameUtils.getString(m_NewTemp.getGuide_word()), ObjectSelf.GetInstance().Name);
        }
        else
        {
            str = GameUtils.getString(m_NewTemp.getGuide_word());
        }
        m_TipsText.text = str;

        // 立即前往
        if (m_NewTemp.getGo_button() != -1)
        {
            m_LeaveBtn.gameObject.SetActive(true);
            m_LeaveBtn.onClick.RemoveAllListeners();

            // 设置回调 有点麻烦
            // TODO...
        }
        else
        {
            m_LeaveBtn.gameObject.SetActive(false);
        }
        // 跳过
        if (m_NewTemp.getSkip_button() != -1)
        {
            m_SkipBtn.gameObject.SetActive(true);
            //m_SkipBtn.onClick.RemoveAllListeners();
            m_SkipBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_NewTemp.getSkip_x(), m_NewTemp.getSkip_y());

            // 设置回调 有点麻烦
            // TODO...
        }
        else
        {
            m_SkipBtn.gameObject.SetActive(false);
        }

        // 点击继续
        if (m_NewTemp.getClick_continue() == -1)
        {
            m_Continue = false;
            m_ContuineBtn.enabled = false;
            m_ContuineText.gameObject.SetActive(false);
        }
        else
        {
            m_Continue = true;
            m_ContuineBtn.enabled = true;
            m_ContuineText.gameObject.SetActive(true);
        }


        // 高亮区
        // 这里如果出现间隙或者重复，那就麻烦了。
        ShowMask();

        // 小手
        ShowFinger();
    }

    // 显示遮罩的位置
    void ShowMask()
    {
        m_Transparent.gameObject.SetActive(false);
        m_Mask.gameObject.SetActive(false);

        // 背景遮罩
        if (m_NewTemp.getBackground_shade() == -1)
        {
            // 没有背景遮罩但是有高亮区的情况
            if (m_NewTemp.getHighlight_area() == 1)
            {
                m_Mask.gameObject.SetActive(true);
                Image mask = m_Mask.transform.FindChild("Target").GetComponent<Image>();
                // 目标点转到现在的 Canvas 下面的坐标
                //Vector2 screenPos = new Vector2(m_NewTemp.getArea_x(), m_NewTemp.getArea_y());
                //mask.transform.position = curCanvas.worldCamera.ScreenToWorldPoint(screenPos);

                mask.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArea_x(), m_NewTemp.getArea_y());
                // 高亮区域的大小
                mask.rectTransform.sizeDelta = new Vector2(m_NewTemp.getArea_w(), m_NewTemp.getArea_h());

                // up
                RectTransform bgUp = m_Mask.transform.FindChild("BgUp") as RectTransform;
                bgUp.sizeDelta = new Vector2(bgUp.sizeDelta.x, canvasHeight / 2 - mask.rectTransform.anchoredPosition.y - mask.rectTransform.sizeDelta.y / 2);
                Color upColor = bgUp.GetComponent<Image>().color;
                bgUp.GetComponent<Image>().color = new Color(upColor.r, upColor.g, upColor.b, 0);

                // lower
                RectTransform bgLower = m_Mask.transform.FindChild("BgLower") as RectTransform;
                bgLower.sizeDelta = new Vector2(mask.rectTransform.sizeDelta.x, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y - mask.rectTransform.sizeDelta.y / 2);
                bgLower.anchoredPosition = new Vector2(mask.rectTransform.anchoredPosition.x, bgLower.anchoredPosition.y);
                Color lowerColor = bgLower.GetComponent<Image>().color;
                bgLower.GetComponent<Image>().color = new Color(lowerColor.r, lowerColor.g, lowerColor.b, 0);

                // left
                RectTransform bgLeft = m_Mask.transform.FindChild("BgLeft") as RectTransform;
                bgLeft.sizeDelta = new Vector2(canvasWidth / 2 + mask.rectTransform.anchoredPosition.x - mask.rectTransform.sizeDelta.x / 2, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y + mask.rectTransform.sizeDelta.y / 2);
                Color leftColor = bgLeft.GetComponent<Image>().color;
                bgLeft.GetComponent<Image>().color = new Color(leftColor.r, leftColor.g, leftColor.b, 0);

                // right
                RectTransform bgRight = m_Mask.transform.FindChild("BgRight") as RectTransform;
                bgRight.sizeDelta = new Vector2(canvasWidth / 2 - mask.rectTransform.anchoredPosition.x - mask.rectTransform.sizeDelta.x / 2, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y + mask.rectTransform.sizeDelta.y / 2);
                Color rightColor = bgRight.GetComponent<Image>().color;
                bgRight.GetComponent<Image>().color = new Color(rightColor.r, rightColor.g, rightColor.b, 0);
                mask.gameObject.SetActive(false);
            }
            else
            {
                m_Mask.gameObject.SetActive(false);
                m_Transparent.gameObject.SetActive(false);
            }

            return;
        }

        // 高亮区域是否可以点击
        // 全屏高亮，但是不可点击
        if (m_NewTemp.getArea_t() == -1)
        {
            m_Transparent.gameObject.SetActive(true);
            m_Transparent.color = new Color(m_Transparent.color.r, m_Transparent.color.g, m_Transparent.color.b, m_Transparent.color.a);
            m_Mask.gameObject.SetActive(false);
        }  // 有高亮区域
        else if (m_NewTemp.getArea_t() == 1)
        {
            m_Transparent.gameObject.SetActive(false);

            m_Mask.gameObject.SetActive(true);

            Image mask = m_Mask.transform.FindChild("Target").GetComponent<Image>();
            // 目标点转到现在的 Canvas 下面的坐标
            mask.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArea_x(), m_NewTemp.getArea_y());
            // 高亮区域的大小
            mask.rectTransform.sizeDelta = new Vector2(m_NewTemp.getArea_w(), m_NewTemp.getArea_h());

            // up
            RectTransform bgUp = m_Mask.transform.FindChild("BgUp") as RectTransform;
            bgUp.sizeDelta = new Vector2(bgUp.sizeDelta.x, canvasHeight / 2 - mask.rectTransform.anchoredPosition.y - mask.rectTransform.sizeDelta.y / 2);
            Color upColor = bgUp.GetComponent<Image>().color;
            bgUp.GetComponent<Image>().color = new Color(upColor.r, upColor.g, upColor.b, 0.5f);

            // lower
            RectTransform bgLower = m_Mask.transform.FindChild("BgLower") as RectTransform;
            bgLower.sizeDelta = new Vector2(mask.rectTransform.sizeDelta.x, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y - mask.rectTransform.sizeDelta.y / 2);
            bgLower.anchoredPosition = new Vector2(mask.rectTransform.anchoredPosition.x, bgLower.anchoredPosition.y);
            Color lowerColor = bgLower.GetComponent<Image>().color;
            bgLower.GetComponent<Image>().color = new Color(lowerColor.r, lowerColor.g, lowerColor.b, 0.5f);

            // left
            RectTransform bgLeft = m_Mask.transform.FindChild("BgLeft") as RectTransform;
            bgLeft.sizeDelta = new Vector2(canvasWidth / 2 + mask.rectTransform.anchoredPosition.x - mask.rectTransform.sizeDelta.x / 2, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y + mask.rectTransform.sizeDelta.y / 2);
            Color leftColor = bgLeft.GetComponent<Image>().color;
            bgLeft.GetComponent<Image>().color = new Color(leftColor.r, leftColor.g, leftColor.b, 0.5f);

            // right
            RectTransform bgRight = m_Mask.transform.FindChild("BgRight") as RectTransform;
            bgRight.sizeDelta = new Vector2(canvasWidth / 2 - mask.rectTransform.anchoredPosition.x - mask.rectTransform.sizeDelta.x / 2, canvasHeight / 2 + mask.rectTransform.anchoredPosition.y + mask.rectTransform.sizeDelta.y / 2);
            Color rightColor = bgRight.GetComponent<Image>().color;
            bgRight.GetComponent<Image>().color = new Color(rightColor.r, rightColor.g, rightColor.b, 0.5f);
            mask.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("填表错误，请修改表");
        }
    }

    // 指示小手的位置
    void ShowFinger()
    {
        Image mask = m_Mask.transform.FindChild("Target").GetComponent<Image>();

        // 上
        if (m_NewTemp.getDirection() == 1)
        {
            m_Finger.gameObject.SetActive(true);
            //m_Finger.rectTransform.anchoredPosition = mask.rectTransform.anchoredPosition + new Vector2(m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
        } // 下
        else if (m_NewTemp.getDirection() == 2)
        {
            m_Finger.gameObject.SetActive(true);
            m_Finger.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
            //m_Finger.rectTransform.anchoredPosition = mask.rectTransform.anchoredPosition + new Vector2(m_NewTemp.getArrow_x(), -m_NewTemp.getArrow_y());
        } // 左
        else if (m_NewTemp.getDirection() == 3)
        {
            m_Finger.gameObject.SetActive(true);
            //m_Finger.rectTransform.anchoredPosition = new Vector2(-m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.rotation = Quaternion.Euler(0, 0, 270);
        } // 右
        else if (m_NewTemp.getDirection() == 4)
        {
            m_Finger.gameObject.SetActive(true);
            //m_Finger.rectTransform.anchoredPosition = mask.rectTransform.anchoredPosition + new Vector2(m_NewTemp.getArrow_x(), -m_NewTemp.getArrow_y());
            m_Finger.rectTransform.anchoredPosition = new Vector2(m_NewTemp.getArrow_x(), m_NewTemp.getArrow_y());
            m_Finger.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
        } // 不显示小手  0
        else
        {
            m_Finger.gameObject.SetActive(false);
        }
    }


    // 点击继续
    protected override void OnClickContuineBtn()
    {
        if (m_Continue)
        {
            // 当点击继续的时候调用，每次点击都进行到下一步引导 ID
            // TODO...
            //GuideManager.GetInstance().ShowNextGuide();
            // 将下一个的指引 ID 给抛出来
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Guide_Continue, GuideManager.GetInstance().GetNextGuideID());
        }
    }

    // 跳过   (跳过向服务器发送该引导步骤的最后一步骤ID [Lyq])
    protected override void OnClickSkipBtn()
    {
        // 移除 UI
        GuideManager.GetInstance().StopGuide();

        // 给服务器发送消息，标志这个引导完毕
        GuideManager.GetInstance().SendMessage(m_NewTemp.getSkip_end());
    }


    void GetCurCanvas()
    {
        // 获取指引所在的 canvas
        Transform t = transform;
        while (true)
        {
            if (t.parent.parent != null)
            {
                t = t.parent;
            }
            else
            {
                break;
            }
        }

        try
        {
            curCanvas = t.GetComponent<Canvas>();
        }
        catch (System.Exception ex)
        {
            Debug.Log("UI_Guide 获取指引所在的 canvas 失败");
        }
    }
}
