using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public enum HE_SKIN_STATE
{ 
    SKIN_JINGDIAN,//经典
    SKIN_DEF,     //默认穿戴
    SKIN_HAVE,    //拥有
    SKIN_NOT_HAVE //未拥有
}
public class UI_HeroSkin : BaseUI 
{
    public static UI_HeroSkin _inst = null;
    private ObjectCard m_CurCard = null;

    private List<GameObject> goList = new List<GameObject>();
    private List<int> playerHeroSkinList;
    private List<UI_HeroListItem> heroList;
    private Button m_BackBtn = null;
    private GameObject m_Grid = null;
    private GameObject m_Prefab = null;                                                  //时装Item
    private Transform m_Point = null;
    private GameObject ModelRotaeBtn = null;
    public bool iSRotate;                                                                //3D模型旋转开关
    private float Card3DRoteh;                                                           //3D模型旋转参数
    private float Card3DRotev;                                                           //3D模型旋转参数
    private Vector3 Torque;                                                              //旋转力数值
    private Text m_NameText;
    private Text m_NiNameText;
    public override void InitUIData()
    {
        base.InitUIData();
        _inst = this;
        playerHeroSkinList = ObjectSelf.GetInstance().GetHeroSkinList();//获得当前角色的所有时装
        heroList = UI_HeroListManager._instance.heroList;
        m_Prefab = (GameObject)UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/HeroSkinItem");
        m_BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
        m_Grid = selfTransform.FindChild("HeroSkinList/ListLayOut").gameObject;

        m_Point = GameObject.Find("pos").transform;
        ModelRotaeBtn = selfTransform.FindChild("ModelRotaeBtn").gameObject;
        EventTriggerListener.Get(ModelRotaeBtn).onDown = OnRotateDown;
        EventTriggerListener.Get(ModelRotaeBtn).onUp = OnRotatUp;

        m_NameText = selfTransform.FindChild("NameText").GetComponent<Text>();
        m_NiNameText = selfTransform.FindChild("NameText/Text").GetComponent<Text>();

        m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnBackClick));
        GameEventDispatcher.Inst.addEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        ShowHeroSkin(UI_HeroInfoManager._instance.GetCurCard());
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
        if (iSRotate)
        {
            Card3DRoteh = Input.GetAxis("Mouse X");//有正左负
            Card3DRotev = Input.GetAxis("Mouse Y");//上正下负
        }
        else
        {
            Card3DRoteh = 0;
            Card3DRotev = 0;
        }
        Torque = new Vector3(Card3DRotev, -Card3DRoteh, 0);
    }
    //刷新3D模型旋转
    private void FixedUpdate()
    {
        if (UI_HeroListManager._instance.GetCard3Dmodel() != null)
        {
            UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.AddTorque(Torque * 10);
        }
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.U_HeroChangeTarget, OnSelectCardHeroChanged);
    }


    void ShowHeroSkin(ObjectCard card)
    {
        m_CurCard = card;
        ClearSkinItem();
        CreateHeroSkin();
        UpdateSkin();
        ShowModel();
        m_NameText.text = GameUtils.getString(m_CurCard.GetHeroRow().getNameID());
        m_NiNameText.text = GameUtils.getString(m_CurCard.GetHeroRow().getTitleID());
    }

    /// <summary>
    /// 创建时装Item
    /// </summary>
    void CreateHeroSkin()
    {
        goList.Clear();
        //当前的卡牌的所有时装
        int[] HeroSkinList = m_CurCard.GetHeroRow().getUseableArtresource();
        int length = HeroSkinList.Length;
        //2.遍历
        for (int i = 0; i < length; i++)
        {
            GameObject go = Instantiate(m_Prefab) as GameObject;
            //GameObject go = (GameObject)Instantiate(UIResourceMgr.LoadPrefab("UI/Prefabs/UI_Home/UI_HeroInfo/HeroSkinItem"));
            go.transform.parent = m_Grid.transform;
            go.transform.localScale = Vector3.one;
            Vector3 goPos = go.transform.localPosition;
            goPos.z = 0;
            go.transform.localPosition = goPos;
            goList.Add(go);
        }
    }

    /// <summary>
    /// 清理时装Item
    /// </summary>
    void ClearSkinItem()
    {
        for (int i = 0; i < m_Grid.transform.childCount; i++)
        {
            Destroy(m_Grid.transform.GetChild(i).gameObject);
        }      
    }

    /// <summary>
    /// 用于排序Item
    /// </summary>
    /// <returns></returns>
    private List<int> SkinItemRaek()
    {
        List<int> allSkinLsit = new List<int>();
        int one = 0;
        List<int> haveList = new List<int>();
        List<int> nothaveList = new List<int>();
        int[] HeroSkinList = m_CurCard.GetHeroRow().getUseableArtresource();
        one = HeroSkinList[0];
        for (int i = 1; i < goList.Count; i++)
        {
            if (playerHeroSkinList.Contains(HeroSkinList[i]))
            {
                haveList.Add(HeroSkinList[i]);
            }
            else
            {
                nothaveList.Add(HeroSkinList[i]);
            }
        }

        allSkinLsit.Add(one);
        for (int i = 0; i < haveList.Count; i++)
        {
            allSkinLsit.Add(haveList[i]);
        }
        for (int i = 0; i < nothaveList.Count; i++)
        {
            allSkinLsit.Add(nothaveList[i]);
        }

        return allSkinLsit;
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    void UpdateSkin()
    {
        List<int> SkinList = SkinItemRaek();
        for (int i = 0; i < SkinList.Count; i++)
        {
            UI_HeroSkinItem SkinItem = goList[i].GetComponent<UI_HeroSkinItem>();
            //if (i == 0)
            //{
            //    //经典时装
            //    SkinItem.HeroSkinState = HE_SKIN_STATE.SKIN_HAVE;
            //}
            if (m_CurCard.GetHeroData().GetHeroViewID() == SkinList[i])
            {
                //默认选择的时装
                SkinItem.HeroSkinState = HE_SKIN_STATE.SKIN_DEF;
            }
            else
            {
                //判断当前角色有没有获得的英雄时装
                SkinItem.HeroSkinState = playerHeroSkinList.Contains(SkinList[i]) ? HE_SKIN_STATE.SKIN_HAVE : HE_SKIN_STATE.SKIN_NOT_HAVE;
            }

            //处理时装Item逻辑
            SkinItem.ShowHeroSkinItem(SkinList[i], (int)m_CurCard.GetGuid().GUID_value);
        }
    }

    //显示模型
    void ShowModel()
    {
        ArtresourceTemplate _ArtesourData = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_CurCard.GetHeroData().GetHeroViewID());
    }


    /// <summary>
    /// 返回结果
    /// </summary>
    /// <param name="result">结果</param>
    public void GetResult(int result)
    {
        if (result == 1)
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.HE_HeroSkin);
            UpdateSkin();
            ResultOKShowMes();
            ShowModel();
            UI_HeroListManager._instance.GetCurHeroListItem().UpdateShow();
        }
    }
    /// <summary>
    ///换装成功弹窗
    /// </summary>
    void ResultOKShowMes()
    {
        string text = GameUtils.getString("hero_info_change_tip2");
        InterfaceControler.GetInst().AddMsgBox(text, transform);
    }

    void OnSelectCardHeroChanged(GameEvent ev)
    {
        if (ev == null || ev.data == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ObjectCard card = ev.data as ObjectCard;

        if (card == null)
        {
            LogManager.LogError("当前选中的英雄为空");
            return;
        }

        ShowHeroSkin(card);
    }

    private void OnRotateDown(GameObject a)
    {
        iSRotate = true;
        UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
    }
    private void OnRotatUp(GameObject a)
    {
        iSRotate = false;
    }

    void  OnBackClick()
    {
        HideUI();
        UI_HeroListManager._instance.GetCard3Dmodel().transform.rotation = new Quaternion(0, 0, 0, 0);
        UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = true;
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);

    }

    public void HideUI()
    {
        if (this.gameObject != null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
