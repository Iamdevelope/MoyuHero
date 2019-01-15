using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DG.Tweening;
using DreamFaction.UI;


public class UI_VipPrivilege : UI_BaseVipPrivilege 
{
    private class PrivilegeData 
    {
        public List<Text> m_TipsTextList;           //预制件上的Text控件
        public RectTransform m_LayoutObject;            //预制件作为子物体挂在该点下
        public GameObject m_OriginalTipsText;       //需要实例化预制件的引用，该预制件用于显示特权字段。
        public byte m_VipLv;                        //VIP等级
        public TableReader m_DataReader;

        public PrivilegeData(RectTransform layoutObject, GameObject originalTipsText,TableReader dataReader)
        {
            m_TipsTextList = new List<Text>();
            m_LayoutObject = layoutObject;
            m_OriginalTipsText = originalTipsText;
            m_DataReader = dataReader;
        }

        public void LoadPrivilegeData(byte vipLv)
        {
            m_VipLv = vipLv;

            if(m_TipsTextList.Count>0)
            {
                for (int i = 0; i < m_TipsTextList.Count;i++ )
                {
                    GameObject.Destroy(m_TipsTextList[i].gameObject);
                }
                m_TipsTextList.Clear();

            }

            var vipTemplate = (VipTemplate)m_DataReader.getTableData(vipLv);
            string[] dataArray = vipTemplate.getPrivilegedDes();

            for(int i= 0;i<dataArray.Length;i++)
            {
                GameObject temp = GameObject.Instantiate(m_OriginalTipsText, m_LayoutObject.position, m_LayoutObject.rotation) as GameObject;
                temp.transform.SetParent(m_LayoutObject);
                temp.transform.localScale = Vector3.one;
                Text tempText = temp.GetComponent<Text>();
                tempText.text = GameUtils.getString(dataArray[i]);
                m_TipsTextList.Add(tempText);

            }

        }
    }

    private static readonly string Path = "UI_Shop/UI_VipPrivilege_2_8";
    /// <summary>
    /// 该布尔值影响充值按钮的行为，true时填出快速充值，false关闭自身窗体
    /// </summary>
    private static bool QuikCharge;

    private ObjectSelf objectSelf;
    private string m_TopDescriptionTextTempString;
    private Slider m_ExpSlider;
    private ScrollRect m_ScrollRect;
    private GameObject m_OriginalUnitImage;
    private GameObject m_OriginalTipsObject;
    private Sprite[] m_NumArray = new Sprite[10];    //VIP等级数字图片引用

    private PrivilegeData[] m_DataArray = new PrivilegeData[3];
    private sbyte m_CurDataArrayIdx;                //m_DataArray中，当前显示给用户的Layout
    private int m_CurBrowseLv;                      //当前用户正在浏览的特权等级
    private int m_MaxVipLv;                         //最大VIP等级

    private Transform m_VipLvPanel;
    private Transform m_CenterPoint;
    private Transform m_PrevPoint;
    private Transform m_NextPoint;

    private TableReader m_VipReader;

    public static string GetPath(bool quikCharge)
    {
        QuikCharge = quikCharge;
        return Path;
    }

    public override void InitUIData()
    {
        base.InitUIData();

        LoadNumSprite();

        m_TopDescriptionTextTempString = GameUtils.getString("shop_content16");
        objectSelf = ObjectSelf.GetInstance();
        m_VipReader = DataTemplate.GetInstance().m_VipTable;

        m_ExpSlider = selfTransform.FindChild("LeftPanel/VipExpProgressBar").gameObject.GetComponent<Slider>();
        m_ScrollRect = selfTransform.FindChild("RightPanel").gameObject.GetComponent<ScrollRect>();
        m_OriginalUnitImage = selfTransform.FindChild("OriginalTipsPanel/OriginalUnitImage").gameObject;
        m_OriginalTipsObject = selfTransform.FindChild("OriginalTipsPanel/OriginalTipsText").gameObject;

        m_VipLvPanel = selfTransform.FindChild("LeftPanel/VipLvPanel");
        m_CenterPoint = selfTransform.FindChild("RightPanel/SubPanel/CenterPoint");
        m_PrevPoint = selfTransform.FindChild("RightPanel/SubPanel/PrevPoint");
        m_NextPoint = selfTransform.FindChild("RightPanel/SubPanel/NextPoint");



        GameEventDispatcher.Inst.addEventListener(GameEventID.G_VipLevel_Update, RefreshPanel);

    }
    private void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.G_VipLevel_Update, RefreshPanel);
    
    }

    public override void InitUIView()
    {
        base.InitUIView();

        m_CurBrowseLv = objectSelf.VipLevel;
        m_MaxVipLv = m_VipReader.getDataCount();
        InitLayout();
        m_RightPanelBottomTipsText.text = GameUtils.getString("shop_content23");
        m_PayButtonText.text = GameUtils.getString("shop_content4");
        m_RightPanelTittleText.text = GameUtils.getString(string.Format("VIP_lv{0}", m_CurBrowseLv));

        UpdateVipExp();
        UpdateVipPanelSprite();
        TweenOverCallBack();
    }

    private void InitLayout()
    {
        RectTransform tempTransform;
        PrivilegeData tempData;

        tempTransform = selfTransform.FindChild("RightPanel/SubPanel/LayoutP").GetComponent<RectTransform>();
        tempTransform.position = m_PrevPoint.transform.position;
        tempData = new PrivilegeData(tempTransform, m_OriginalTipsObject, m_VipReader);
        m_DataArray[0] = tempData;

        tempTransform = selfTransform.FindChild("RightPanel/SubPanel/LayoutC").GetComponent<RectTransform>();
        tempTransform.position = m_CenterPoint.transform.position;
        tempData = new PrivilegeData(tempTransform, m_OriginalTipsObject, m_VipReader);
        m_DataArray[1] = tempData;

        tempTransform = selfTransform.FindChild("RightPanel/SubPanel/LayoutN").GetComponent<RectTransform>();
        tempTransform.position = m_NextPoint.transform.position;
        tempData = new PrivilegeData(tempTransform, m_OriginalTipsObject, m_VipReader);
        m_DataArray[2] = tempData;




        if (m_CurBrowseLv > 1)
        {
            m_DataArray[0].LoadPrivilegeData((byte)(m_CurBrowseLv - 1));
        }
        m_DataArray[1].LoadPrivilegeData((byte)(m_CurBrowseLv));
        if (m_CurBrowseLv < m_MaxVipLv)
        {
            m_DataArray[2].LoadPrivilegeData((byte)(m_CurBrowseLv + 1));
        }

        m_CurDataArrayIdx = 1;
        m_ScrollRect.content = m_DataArray[m_CurDataArrayIdx].m_LayoutObject;
//        m_ScrollRect.velocity = new Vector2(0, -25);
    
    }


    private sbyte GetNextDataIdx()
    {
        sbyte index = m_CurDataArrayIdx;
        index++;
        return (sbyte)(index % 3);
    }
    private sbyte GetPrevDataIdx()
    {
        sbyte index = m_CurDataArrayIdx;
        index--;
        return index < 0 ? (sbyte)2 : index;
    }

    private void MoveToNextLevel()
    {
        if (m_CurBrowseLv >= m_MaxVipLv)
            return;

        m_ScrollRect.content = null;
        m_ScrollRect.enabled = false;
        sbyte next = GetNextDataIdx();
        sbyte prev = GetPrevDataIdx();


        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(m_DataArray[next].m_LayoutObject.DOMoveX(m_CenterPoint.position.x, 0.5f));
        mySequence.Insert(0,m_DataArray[m_CurDataArrayIdx].m_LayoutObject.DOMoveX(m_PrevPoint.position.x, 0.5f));
        mySequence.AppendCallback(TweenOverCallBack);
        mySequence.SetUpdate(true);


        m_DataArray[prev].m_LayoutObject.position = m_NextPoint.position;
        m_CurDataArrayIdx = next;
        m_CurBrowseLv++;
        if (m_CurBrowseLv < m_MaxVipLv)
        {
            m_DataArray[prev].LoadPrivilegeData((byte)(m_CurBrowseLv + 1));
        }
        LockTurnBotton(true);

    }
    private void MoveToPrevLevel()
    {
        if (m_CurBrowseLv <= 1)
            return;

        m_ScrollRect.content = null;
        m_ScrollRect.enabled = false;

        sbyte next = GetNextDataIdx();
        sbyte prev = GetPrevDataIdx();


        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(m_DataArray[prev].m_LayoutObject.DOMoveX(m_CenterPoint.position.x, 0.5f));
        mySequence.Insert(0, m_DataArray[m_CurDataArrayIdx].m_LayoutObject.DOMoveX(m_NextPoint.position.x, 0.5f));
        mySequence.AppendCallback(TweenOverCallBack);
        mySequence.SetUpdate(true);


        m_DataArray[next].m_LayoutObject.position = m_PrevPoint.position;
        m_CurDataArrayIdx = prev;
        m_CurBrowseLv--;

        if(m_CurBrowseLv > 1)
        {
            m_DataArray[next].LoadPrivilegeData((byte)(m_CurBrowseLv - 1));
        }
        LockTurnBotton(true);
    }



    private void LockTurnBotton(bool value)
    {
        m_TurnLeftButton.enabled = !value;
        m_TurnRightButton.enabled = !value;
    }

   

    private void LoadNumSprite()
    {
        //此处暂时从预制件中读取数据
        m_NumArray[0] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip0");
        m_NumArray[1] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip1");
        m_NumArray[2] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip2");
        m_NumArray[3] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip3");
        m_NumArray[4] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip4");
        m_NumArray[5] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip5");
        m_NumArray[6] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip6");
        m_NumArray[7] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip7");
        m_NumArray[8] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip8");
        m_NumArray[9] = UIResourceMgr.LoadSprite(common.numberPath + "/vip_number/vip9");
       
    }

    private void UpdateVipExp()
    {
        int exp = objectSelf.VipExp;
        var vipTemplate = (VipTemplate)m_VipReader.getTableData(objectSelf.VipLevel);   
        int maxExp = vipTemplate.getVipExp();

        if (maxExp > 0)
        {
            m_ExpSlider.value = (float)exp / maxExp;
            int money = ShopModule.GetMoneyCountToNextVipLv(objectSelf.VipLevel, exp);
            System.Text.StringBuilder builder = new System.Text.StringBuilder(string.Format(m_TopDescriptionTextTempString, money));
            builder.AppendFormat("<color=#ECAF48>VIP{0}</color>", (objectSelf.VipLevel + 1));
//            builder.Append((objectSelf.VipLevel+1).ToString());
            m_TopDescriptionText.text = builder.ToString();
            m_VipExpText.text = string.Format("/{0}",maxExp);
            m_CurrentExpText.text = exp.ToString();
        }
        else
        {
            m_TopDescriptionText.text = GameUtils.getString("VIP_tips1"); ;
            m_ExpSlider.gameObject.SetActive(false);
            m_VipExpText.gameObject.SetActive(false);
        }
    }


    private void UpdateVipPanelSprite()
    {
        int lv = objectSelf.VipLevel;
        int count = lv % 10;           //十位数
        int dex = lv / 10;           //个位数


        var delList = m_VipLvPanel.GetComponentsInChildren<Image>();
        for(int i = 0;i<delList.Length;i++)
        {
            Destroy(delList[i].gameObject);

        }

        GameObject temp;

        if (dex > 0)
        {
            temp = GameObject.Instantiate(m_OriginalUnitImage, m_VipLvPanel.position, m_VipLvPanel.rotation) as GameObject;
            temp.transform.SetParent(m_VipLvPanel);
            temp.transform.localScale = Vector3.one;
            temp.GetComponent<Image>().sprite = m_NumArray[dex];
        }


        temp = GameObject.Instantiate(m_OriginalUnitImage, m_VipLvPanel.position, m_VipLvPanel.rotation) as GameObject;
        temp.transform.SetParent(m_VipLvPanel);
        temp.transform.localScale = Vector3.one;
        temp.GetComponent<Image>().sprite = m_NumArray[count];

    }

    /**************CallBack*********************/
    protected override void OnClickCloseBtn()
    {


        UI_HomeControler.Inst.ReMoveUI(Path);
    }


    protected override void OnClickTurnRightButton()
    {
        MoveToNextLevel();
    }
    protected override void OnClickTurnLeftButton()
    {
        MoveToPrevLevel();
    }

    protected override void OnClickPayButton()
    {
        //var temp = DreamFaction.GameCore.InterfaceControler.GetInst();
        //if (temp != null)
        //{
        //    temp.AddMsgBox("暂时无法充值", transform);
        //}
        if(QuikCharge)
            UI_HomeControler.Inst.AddUI(UI_QuikChargeMgr.UI_ResPath);
        else
            UI_HomeControler.Inst.ReMoveUI(Path);

    }


    protected void TweenOverCallBack()
    {
        m_ScrollRect.content = m_DataArray[m_CurDataArrayIdx].m_LayoutObject;
        m_ScrollRect.enabled = true;
        LockTurnBotton(false);

        m_RightPanelTittleText.text = GameUtils.getString(string.Format("VIP_lv{0}", m_CurBrowseLv));
        //置灰左按钮
        GameUtils.SetBtnSpriteGrayState(m_TurnLeftButton, (m_CurBrowseLv<=1));
        //置灰右按钮
        GameUtils.SetBtnSpriteGrayState(m_TurnRightButton, m_CurBrowseLv >= m_MaxVipLv);

        

    }

    protected void RefreshPanel()
    {
        InitUIView();
    }

}
