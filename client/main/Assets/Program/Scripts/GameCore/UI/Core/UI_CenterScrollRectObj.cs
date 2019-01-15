using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_CenterScrollRectObj : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Scrollbar scrollBar;
    public RectTransform rectTransform;

    private Transform m_CenterTrans = null;

    private float m_HeightDelta = 0f;
	// Use this for initialization
	void Start () {
	    if (scrollRect == null)
	    {
            scrollRect = transform.GetComponent<ScrollRect>();
	    }

        if (scrollBar == null)
        {
            Debug.LogError("ScrollBar 不能为Null");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 设置当前scrollRect快速滚动并显示到trans对象;
    /// 当前只支持从上到下，且anchor 为TopLeft;
    /// </summary>
    /// <param name="showCount"></param>
    /// <param name="trans"></param>
    public void SetCenterObj(int showCount, Transform trans)
    {
        if (!trans.IsChildOf(scrollRect.content))
        {
            Debug.LogError("该transform不存在于ScrollRect的列表中");
            return;
        }
        int i = 0;
        int j = scrollRect.content.childCount;
        for (; i < j;  i++)
        {
            Transform t = scrollRect.content.GetChild(i);
            if (t.Equals(trans))
            {
                break;
            }
        }

        if (i >= showCount)
        {
            //scrollBar.value = Mathf.Clamp01((float)(i + 1 - showCount) / (float)(j - showCount));

            CalcHeightDelta();
            float seprator = Mathf.Clamp01((float)(i + 1 - showCount) / (float)(rectTransform.childCount - showCount));
            float delta = Mathf.Round(Mathf.Lerp(0f, m_HeightDelta, seprator));
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, delta, 0f);
        }
        else
        {
            //scrollBar.value = 0f;
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, 0f, 0f);
        }
    }

    void CalcHeightDelta()
    {
        m_HeightDelta = rectTransform.rect.height - scrollRect.GetComponent<RectTransform>().rect.height;
    }
}
