using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using DreamFaction.Utils;

public enum RuneDetailObjType
{
    ScrollRect,
    Title,
    Content,
    TwoAttri,
    ThreAttri,
}

public interface IRuneDetailObj
{
    void SetParent(Transform parent);

    void Destroy();

    void SetActive(bool active);
}

public class RuneDetailScrollRect : IRuneDetailObj
{
    protected GameObject mGo = null;
    protected GameObject mVerticalLayoutObj = null;
    protected RectTransform mScrollRectTrans = null;

    public RuneDetailScrollRect(GameObject go)
    {
        mGo = go;
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);

        Transform trans = mGo.transform;
        mVerticalLayoutObj = trans.FindChild("ItemsLayout").gameObject;
        mScrollRectTrans = trans.GetComponent<RectTransform>();
    }

    /// <summary>
    /// 返回verticalLayout 的根节点;
    /// </summary>
    /// <returns></returns>
    public GameObject GetLayoutObj()
    {
        return mVerticalLayoutObj;
    }

    /// <summary>
    /// 组件ScrollRect所在的gameobject;
    /// </summary>
    /// <returns></returns>
    public void SetScrollRectWidth(float width)
    {
        //mScrollRectTrans.rect.width = width;
        mScrollRectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width);
    }

    public void Destroy()
    {
        GameObject.Destroy(mGo);

        mGo = null;
        mVerticalLayoutObj = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }
}

public class RuneDetailTitle : IRuneDetailObj
{
    protected Text txt = null;
    protected GameObject mGo = null;

    public RuneDetailTitle(GameObject go)
    {
        mGo = go;
        Transform trans = go.transform;

        txt = trans.FindChild("AttriTitle").GetComponent<Text>();
    }

    public void SetTxt(string str)
    {
        txt.text = str;
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
    }

    public void Destroy()
    {
        GameObject.Destroy(mGo);

        mGo = null;
        txt = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }
}

public class RuneDetailContent : IRuneDetailObj
{
    protected Text txt = null;
    protected GameObject mGo = null;

    public RuneDetailContent(GameObject go)
    {
        mGo = go;
        Transform trans = go.transform;

        txt = trans.FindChild("Text").GetComponent<Text>();
    }

    public void SetTxt(string str)
    {
        txt.text = str;
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
    }

    public void Destroy()
    {
        GameObject.Destroy(mGo);

        txt = null;
        mGo = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }
}

public class RuneDetailTwoAttri : IRuneDetailObj
{
    protected Text leftTxt = null;
    protected Text rightTxt = null;
    protected GameObject mGo = null;

    public RuneDetailTwoAttri(GameObject go)
    {
        mGo = go;
        Transform trans = go.transform;

        leftTxt = trans.FindChild("AttriPair/Left_txt").GetComponent<Text>();
        rightTxt = trans.FindChild("AttriPair/Right_txt").GetComponent<Text>();
    }

    public void SetLeftTxt(string str)
    {
        leftTxt.text = str;
    }

    public void SetRightTxt(string str)
    {
        rightTxt.text = str;
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
    }

    public void Destroy()
    {
        GameObject.Destroy(mGo);

        mGo = null;
        leftTxt = null;
        rightTxt = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }
}

public class RuneDetailThreeAtrri : IRuneDetailObj
{
    protected Text leftTxt = null;
    protected Text midTxt = null;
    protected Text rightTxt = null;
    protected GameObject mGo = null;

    public RuneDetailThreeAtrri(GameObject go)
    {
        mGo = go;
        Transform trans = go.transform;

        leftTxt = trans.FindChild("AddAttriPair/Left_txt").GetComponent<Text>();
        midTxt = trans.FindChild("AddAttriPair/Mid_txt").GetComponent<Text>();
        rightTxt = trans.FindChild("AddAttriPair/Right_txt").GetComponent<Text>();
    }

    public void SetLeftTxt(string str, bool isGray)
    {
        leftTxt.text = isGray ? GameUtils.StringWithGrayColor(str) : str;
    }

    public void SetMidTxt(string str, bool isGray)
    {
        midTxt.text = isGray ? GameUtils.StringWithGrayColor(str) : str;
    }

    public void SetRightTxt(string str, bool isGray)
    {
        rightTxt.text = isGray ? GameUtils.StringWithGrayColor(str) : str;
    }

    public void SetParent(Transform parent)
    {
        mGo.transform.SetParent(parent, false);
    }

    public void Destroy()
    {
        GameObject.Destroy(mGo);

        mGo = null;
        leftTxt = null;
        midTxt = null;
        rightTxt = null;
    }

    public void SetActive(bool active)
    {
        mGo.SetActive(active);
    }
}

public class RuneFactory
{
    public static IRuneDetailObj Create(RuneDetailObjType type, Transform parent)
    {
        switch (type)
        {
            case RuneDetailObjType.ScrollRect:
                RuneDetailScrollRect lahyout = new RuneDetailScrollRect(RuneManager.Inst.RuneDetail.GetObjClone(RuneDetailObjType.ScrollRect));
                lahyout.SetParent(parent);
                return lahyout;
            case RuneDetailObjType.Title:
                RuneDetailTitle title = new RuneDetailTitle(RuneManager.Inst.RuneDetail.GetObjClone(RuneDetailObjType.Title));
                title.SetParent(parent);
                return title;
            case RuneDetailObjType.Content:
                RuneDetailContent content = new RuneDetailContent(RuneManager.Inst.RuneDetail.GetObjClone(RuneDetailObjType.Content));
                content.SetParent(parent);
                return content;
            case RuneDetailObjType.TwoAttri:
                RuneDetailTwoAttri twoAttri = new RuneDetailTwoAttri(RuneManager.Inst.RuneDetail.GetObjClone(RuneDetailObjType.TwoAttri));
                twoAttri.SetParent(parent);
                return twoAttri;
            case RuneDetailObjType.ThreAttri:
                RuneDetailThreeAtrri threeAttri = new RuneDetailThreeAtrri(RuneManager.Inst.RuneDetail.GetObjClone(RuneDetailObjType.ThreAttri));
                threeAttri.SetParent(parent);
                return threeAttri;
            default:
                return null;
        }
    }

    public static RuneItemCommon CreateRuneItemCommom(Transform parent)
    {
        GameObject go = RuneManager.Inst.GetRuneObjClone();
        if (go == null)
        {
            Debug.LogError("公用符文ItemCommon的gameobject创建失败！");
            return null;
        }
        RuneItemCommon m_Obj = go.AddComponent<RuneItemCommon>();
        if (m_Obj == null)
        {
            Debug.LogError("Add RuneItemCommon component 失败！");
            return null;
        }
        m_Obj.transform.SetParent(parent, false);

        return m_Obj;
    }
}
