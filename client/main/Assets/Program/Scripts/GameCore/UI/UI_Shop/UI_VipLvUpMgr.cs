using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.UI;

public class UI_VipLvUpMgr : UI_BaseVipLvUpMgr 
{
    public static readonly string Path = "UI_Shop/UI_VipLvUp_2_9";
    public static Queue<int> PopupQueue = new Queue<int>();

    private Sprite[] m_NumArray = new Sprite[10];    //VIP等级数字图片引用

    private GameObject m_OriginalNumImage;         
    private GameObject m_OriginalTipsObject;

    private Transform m_VipLvPanel;
    private Transform m_Layout;
    private Vector3 m_LayoutPositionBackUp;

    private List<GameObject> m_TipsList = new List<GameObject>();
    private List<GameObject> m_NumImageList = new List<GameObject>();
    public override void InitUIData()
    {
        base.InitUIData();
        LoadNumSprite();
        m_OriginalNumImage = selfTransform.FindChild("OriginalTipsPanel/OriginalUnitImage").gameObject;
        m_OriginalTipsObject = selfTransform.FindChild("OriginalTipsPanel/OriginalTips").gameObject;
        m_VipLvPanel = selfTransform.FindChild("ImagePanel/VipLvPanel");
        m_Layout = selfTransform.FindChild("MessagePanel/SubPanel/Layout");

//        objectSelf = ObjectSelf.GetInstance();
        
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_CloseButtonText.text = GameUtils.getString("common_button_close");
        m_LayoutPositionBackUp = m_Layout.position;
        Popup();
    }

    public void Popup()
    {
        if (PopupQueue.Count > 0)
        {
            int lv = PopupQueue.Dequeue();
            LoadPrivilegeData(lv);
            UpdateVipPanelSprite(lv);
        }
    }
    public static void AddPopupQueue(int lvValue)
    {
        PopupQueue.Enqueue(lvValue);
    }

    private void LoadPrivilegeData(int vipLv)
    {
        
        if (m_TipsList.Count > 0)
        {
            for (int i = 0; i < m_TipsList.Count; i++)
            {
                GameObject.Destroy(m_TipsList[i]);
            }
            m_TipsList.Clear();

        }
        m_Layout.position = m_LayoutPositionBackUp;

        var vipTemplate = (VipTemplate)DataTemplate.GetInstance().m_VipTable.getTableData(vipLv);
        string[] dataArray = vipTemplate.getPrivilegedDes();
        int[] newTipsArray = vipTemplate.getIsNew();
        for (int i = 0; i < dataArray.Length; i++)
        {
            GameObject temp = GameObject.Instantiate(m_OriginalTipsObject, m_Layout.position, m_Layout.rotation) as GameObject;
            temp.transform.SetParent(m_Layout);
            temp.transform.localScale = Vector3.one;
            m_TipsList.Add(temp);

            Text tempText = temp.transform.FindChild("TipsImage/OriginalTipsText").GetComponent<Text>();
            tempText.text = GameUtils.getString(dataArray[i]);
            temp.transform.FindChild("TipsImage/OriginalTipsText/newImage").gameObject.SetActive(newTipsArray[i] == 1);

        }

    }
    private void UpdateVipPanelSprite(int vipLv)
    {
        int count = vipLv % 10;           //十位数
        int dex = vipLv / 10;           //个位数


//        var delList = m_VipLvPanel.GetComponentsInChildren<Image>();
        if (m_NumImageList.Count > 0)
        {
            for (int i = 0; i < m_NumImageList.Count; i++)
            {
                Destroy(m_NumImageList[i]);
            }
            m_NumImageList.Clear();
        }

        GameObject temp;

        if (dex > 0)
        {
            temp = GameObject.Instantiate(m_OriginalNumImage, m_VipLvPanel.position, m_VipLvPanel.rotation) as GameObject;
            temp.transform.SetParent(m_VipLvPanel);
            temp.transform.localScale = Vector3.one;
            temp.GetComponent<Image>().sprite = m_NumArray[dex];
            m_NumImageList.Add(temp);
        }


        temp = GameObject.Instantiate(m_OriginalNumImage, m_VipLvPanel.position, m_VipLvPanel.rotation) as GameObject;
        temp.transform.SetParent(m_VipLvPanel);
        temp.transform.localScale = Vector3.one;
        temp.GetComponent<Image>().sprite = m_NumArray[count];
        m_NumImageList.Add(temp);
    }

    private void AwakeUp()
    {
        Popup();
        gameObject.SetActive(true);
    }

    protected override void OnClickCloseButton()
    {
        if (PopupQueue.Count>0)
        {
            Invoke("AwakeUp", 0.1f);
            gameObject.SetActive(false);
        }
        else
            UI_HomeControler.Inst.ReMoveUI(Path);
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

}
