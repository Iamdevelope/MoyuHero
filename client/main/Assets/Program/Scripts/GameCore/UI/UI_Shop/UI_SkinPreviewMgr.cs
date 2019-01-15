using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using GNET;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;

public class UI_SkinPreviewMgr :  BaseUI
{
    public static readonly string UI_ResPath = "UI_Shop/UI_SkinPreview_1_2";
    public static UI_SkinPreviewMgr inst;
    private static ArtresourceTemplate mArtTemplate = null;

    Text titleTxt;
    Text nameTxt;
    Text previewTxt;
    GameObject model;
    Button closeBtn;
    Text closeBtnTxt;
    GameObject attriObj;
    GameObject attriItem;

    private GameObject Card3Dmodel;                                                      //当前实例化3D模型
    private Transform Point = null;                                                      //3D模型实例化位置
    private GameObject ModelRotaeBtn;                                                    //3D模型旋转按钮
    public bool iSRotate;                                                                //3D模型旋转开关
    private float Card3DRoteh;                                                           //3D模型旋转参数
    private float Card3DRotev;                                                           //3D模型旋转参数
    private Vector3 Torque;                                                              //旋转力数值


    public override void InitUIData()
    {
        inst = this;
        titleTxt = transform.FindChild("Image/Text").GetComponent<Text>();
        nameTxt = transform.FindChild("SkinDetail/skinName").GetComponent<Text>();
        previewTxt = transform.FindChild("SkinDetail/Text").GetComponent<Text>();
        model = transform.FindChild("ModelImg").gameObject;
        Point = GameObject.Find("pos").transform;
        closeBtn = transform.FindChild("CloseBtn").GetComponent<Button>();
        closeBtnTxt = transform.FindChild("CloseBtn/Text").GetComponent<Text>();
        attriObj = transform.FindChild("SkinDetail/Attris").gameObject;
        attriItem = transform.FindChild("Items/AttriPair").gameObject;

        ModelRotaeBtn = transform.FindChild("ModelRotaeBtn").gameObject;
        EventTriggerListener.Get(ModelRotaeBtn).onDown = OnRotateDown;
        EventTriggerListener.Get(ModelRotaeBtn).onUp = OnRotatUp;

        closeBtn.onClick.AddListener(CloseUI);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        titleTxt.text = GameUtils.getString("shop_content13");
        closeBtnTxt.text = GameUtils.getString("common_button_close");
        previewTxt.text = GameUtils.getString("shop_content14");

        if (mArtTemplate != null)
            SetShowData(mArtTemplate);
    }

    /// <summary>
    /// 修改标题
    /// </summary>
    /// <param name="titleText"></param>
    public void SetTitleText(string titleText)
    {
        titleTxt.text = titleText;
    }

    private void OnRotateDown(GameObject a)
    {
        iSRotate = true;
        //UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
    }

    public override void UpdateUIView()
    {
        base.UpdateUIView();
        //int count = heroList.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    heroList[i].UpdateView(-12.0f, -4.0f);
        //}
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

    private void FixedUpdate()
    {
        if (Card3Dmodel != null)
        {
            Card3Dmodel.rigidbody.AddTorque(Torque * 10);
        }
    }
    public Transform GetPoint()
    {
        return Point;
    }

    private void ModelCear()
    {
        if (Card3Dmodel != null)
            Destroy(Card3Dmodel);
    }

    void Show3DModel(ArtresourceTemplate artT)
    {
        ModelCear();

        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(artT.getArtresources());
        //实例化该对象
        Card3Dmodel = Instantiate(_AssetRes, Point.position, Point.rotation) as GameObject;
        float _zoom = artT.getArtresources_zoom();
        Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
        Card3Dmodel.transform.parent = Point;
        //设置3D模型摩擦力
        Card3Dmodel.rigidbody.angularDrag = 1;
        Card3Dmodel.rigidbody.mass = 1;
        //_obj.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
        Animation anim = Card3Dmodel.GetComponent<Animation>();
        if (anim == null)
            return;
        Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
        Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
    }

    private void OnRotatUp(GameObject a)
    {
        iSRotate = false;
    }

    public static void SetShowArtTemplate(ArtresourceTemplate artT)
    {
        mArtTemplate = artT;
    }

    public void SetShowData(ArtresourceTemplate artT)
    {
        if(artT == null)
        {
            LogManager.LogError("皮肤预览传入的HeroTemplate is null");
            return;
        }

        //int count = DataTemplate.GetInstance().GetArtResourceAtrriCount(artT);

        //if (count > 0)
        //{
        //    for(int i = 0; i < count; i++)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(artT.getSymbol()[i]);
            
        //        if (artT.getIspercentage()[i] == 1)
        //        {
        //            float val = (float)(artT.getAttriValue()[i]) / 10f;
        //            sb.Append(val);
        //            sb.Append("%");
        //        }
        //        else
        //        {
        //            sb.Append(artT.getAttriValue()[i]);
        //        }

        //        CreateAttriItem(artT.getAttriDes()[i],sb.ToString());
        //    }
        //}

        //nameTxt.text = GameUtils.getString(artT.getNameID());

        Show3DModel(artT);
    }

    public override void OnReadyForClose()
    {
        closeBtn.onClick.RemoveAllListeners();
        ModelCear();
        base.OnReadyForClose();
    }

    void OnDisable()
    {
        mArtTemplate = null;
    }

    void OnDestroy()
    {

        OnReadyForClose();
    }
	
    void CloseUI()
    {
        OnReadyForClose();
        UI_HomeControler.Inst.ReMoveUI(UI_ResPath);
    }

    void CreateAttriItem(string name, string val)
    {
        GameObject go = GameObject.Instantiate(attriItem) as GameObject;
        if(go == null)
        {
            LogManager.LogError("皮肤预览属性obj创建失败");
            return;
        }

        Transform trans = go.transform;

        Text left = trans.FindChild("Left_txt").GetComponent<Text>();
        left.text = name;
        Text right = trans.FindChild("Right_txt").GetComponent<Text>();
        right.text = val;

        trans.parent = attriObj.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, 0f);
    }
}
