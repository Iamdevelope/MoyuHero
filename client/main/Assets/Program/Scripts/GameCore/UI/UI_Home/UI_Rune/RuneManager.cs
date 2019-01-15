using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI;
using DreamFaction.LogSystem;

public class RuneDetailObj
{
    protected GameObject mScrollRectObj = null;        //verticalLayout obj；
    protected GameObject mTitleObj = null;         //标题;
    protected GameObject mContentObj = null;       //特殊符文文字描述;
    protected GameObject mTwoAttriObj = null;      //两个属性文字说明;
    protected GameObject mThreeAttriObj = null;    //三个属性文字说明;

    protected Transform m_Trans = null;

    public RuneDetailObj(Transform trans)
    {
        if (trans == null)
        {
            return;
        }

        m_Trans = trans;

        mScrollRectObj = trans.FindChild("ScrollRectObj").gameObject;
        mTitleObj = trans.FindChild("Title").gameObject;
        mContentObj = trans.FindChild("Content").gameObject;
        mTwoAttriObj = trans.FindChild("TwoAttri").gameObject;
        mThreeAttriObj = trans.FindChild("ThreeAttri").gameObject;
    }

    public GameObject GetObjClone(RuneDetailObjType objType)
    {
        switch (objType)
        {
            case RuneDetailObjType.ScrollRect:
                return UIResourceMgr.Clone(mScrollRectObj) as GameObject;
            case RuneDetailObjType.Title:
                return UIResourceMgr.Clone(mTitleObj) as GameObject;
            case RuneDetailObjType.Content:
                return UIResourceMgr.Clone(mContentObj) as GameObject;
            case RuneDetailObjType.TwoAttri:
                return UIResourceMgr.Clone(mTwoAttriObj) as GameObject;
            case RuneDetailObjType.ThreAttri:
                return UIResourceMgr.Clone(mThreeAttriObj) as GameObject;
            default:
                Debug.LogError("不存在的类型：" + objType);
                return null;
        }
    }
}

public class RuneManager
{
    //----符文图标----
    private static GameObject m_RuneObj = null;  //符文prefab的引用，用于动态创建;
    public static readonly string RuneCommonObjPath = "UI/Prefabs/UI_Rune/UI_Rune_Common";

    //----符文属性信息展示----
    private GameObject m_RuneDetailObj = null;
    public static readonly string RuneDetailObjPath = "UI/Prefabs/UI_Rune/UI_Rune_Detail";
    private RuneDetailObj m_RuneDetail = null;

    private static RuneManager m_inst = null;

    //private Queue<GameObject> m_RuneObjQueue = new Queue<GameObject>();  //符文Obj缓冲池子;

    /// <summary>
    /// 蓝、紫色、绿色、红色、特殊色;
    /// </summary>
    private Color[] m_runeColors = new Color[5]{
        new Color(0.137f, 0.3765f, 0.94f, 1f),
        new Color(0.51f, 0.035f, 0.88f, 1f),
        new Color(0.275f, 0.94f, 0.137f, 1f),
        new Color(0.94f, 0.137f, 0.15f, 1f),
        new Color(0.733f, 0.137f, 0.94f, 1f),
    };

    public static RuneManager Inst
    {
        get{
            if (m_inst == null)
            {
                m_inst = new RuneManager();

            }

            return m_inst;
        }
    }

    GameObject RuneDetailGo
    {
        get
        {
            if (m_RuneDetailObj == null)
            {
                m_RuneDetailObj = UIResourceMgr.LoadPrefab(RuneDetailObjPath) as GameObject;
            }

            return m_RuneDetailObj;
        }
    }

    public RuneDetailObj RuneDetail
    {
        get
        {
            if (m_RuneDetail == null)
            {
                m_RuneDetail = new RuneDetailObj(RuneDetailGo.transform);
            }

            return m_RuneDetail;
        }
    }

    private RuneManager()
    {

    }

    public GameObject GetRuneObjClone()
    {
        if (m_RuneObj == null)
        {
            m_RuneObj = UIResourceMgr.LoadPrefab(RuneCommonObjPath) as GameObject;
        }

        if (m_RuneObj == null)
        {
            LogManager.LogError("错误的公用符文Prefab路径：" + RuneCommonObjPath);
        }

        GameObject go = GameObject.Instantiate(m_RuneObj) as GameObject;

        return go;
    }

    public string GetEffColorStr(EM_RUNE_TYPE runeType)
    {
        switch (runeType)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                return "fa2326";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                return "2360fa";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                return "8209e0";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                return "46fa23";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                return "fa2326";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL:
                return "bb23fa";
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE:
                return "bb23fa";
            default:
                return "fa2326";
        }
    }

    public Color GetEffColor(EM_RUNE_TYPE runeType)
    {
        switch (runeType)
        {
            case EM_RUNE_TYPE.EM_RUNE_TYPE_INVALID:
                return m_runeColors[3];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_BLUE:
                return m_runeColors[0];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_PURPLE:
                return m_runeColors[1];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_GREEN:
                return m_runeColors[2];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_RED:
                return m_runeColors[3];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL:
                return m_runeColors[4];
            case EM_RUNE_TYPE.EM_RUNE_TYPE_SPECIAL_UNIQUE:
                return m_runeColors[4];
            default:
                return m_runeColors[3];
        }
    }
}
