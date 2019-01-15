using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.UI;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using UnityEngine.UI;

public class UICommon_HeroGet : BaseUI {

    public  enum PanelType
    {
        FragmentRecruit,  //碎片合成招募
        None,             //默认类型
    }
    private GameObject m_Card3Dmodel = null;      //当前实例化3D模型
    private Transform m_Point = null;              //3D模型实例化位置
    private ObjectCard m_CurCard; //当前角色卡
    private Text m_HeroName; //英雄名称
    private Image m_HeroBorn;//英雄资质
    private Image m_SkillIcon; //技能图标
    private Text m_SkillDesc; //技能描述
    private Text m_SkillName; //技能名称
    private Text m_HeroTedian; //英雄特点
    private Button m_Box;      //点击任意处触发点击事件 关闭界面
    private Transform m_Left;
    private Transform m_Right;
    private Transform m_Top;
    private Transform m_Tween;
    private Transform m_Effect;
    public override void InitUIData()
    {
        base.InitUIData();
        m_Tween = transform.FindChild("tween");
        m_HeroName = m_Tween.FindChild("top/HeroName").GetComponent<Text>();
        m_HeroBorn = m_Tween.FindChild("top/Image(zizhi)").GetComponent<Image>();
        m_SkillIcon = m_Tween.FindChild("left/Image(skillicon)").GetComponent<Image>();
        m_SkillDesc = m_Tween.FindChild("left/Text(skilldec)").GetComponent<Text>();
        m_SkillName = m_Tween.FindChild("left/Text(skillName)").GetComponent<Text>();
        m_HeroTedian = m_Tween.FindChild("right/Text(tedian)").GetComponent<Text>();
        m_Box = m_Tween.FindChild("box").GetComponent<Button>();
        m_Box.onClick.AddListener(OnBox);
        m_Left = m_Tween.FindChild("left");
        m_Right = m_Tween.FindChild("right");
        m_Top = m_Tween.FindChild("top");
        m_Top.gameObject.SetActive(false);
        m_Right.gameObject.SetActive(false);
        m_Left.gameObject.SetActive(false);
        m_Effect = transform.FindChild("Ui_Effect_Zhaomu01");
        m_Effect.gameObject.SetActive(false);
    }
    /// <summary>
    /// 设置数据 
    /// </summary>
    /// <param name="oc">英雄卡牌</param>
    /// <param name="type">当前界面类型 默认为正常招募英雄界面</param>
    public void SetData(ObjectCard oc, PanelType type =PanelType.None)
    {
        if (oc == null) return;
        m_CurCard = oc;
        if (type == PanelType.FragmentRecruit)
        {
            HeroTemplate _heroTemp = oc.GetHeroRow();
            m_HeroName.text = GameUtils.getString(_heroTemp.getTitleID());
            m_HeroBorn.sprite = InterfaceControler.GetInst().GetHeroAptImg(_heroTemp);
            m_HeroBorn.SetNativeSize();
            SkillTemplate _skillTemp = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(_heroTemp.getSkill1ID());
            m_SkillIcon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _skillTemp.getSkillIcon());
            m_SkillDesc.text = GameUtils.getHeroString(_skillTemp.getSkillDes(), oc).Replace("\\n", "\n");
            m_SkillName.text = GameUtils.getString(_skillTemp.getSkillName());
            m_HeroTedian.text = GameUtils.getString(_heroTemp.getTedian());
            StartCoroutine(DeySetData());
            StartCoroutine(InitHeroModel());
        }
    }
    IEnumerator DeySetData()
    {
        yield  return new WaitForSeconds(0.5f);
        m_Effect.gameObject.SetActive(true);
        if (m_Effect.GetComponentInChildren<ParticleSystem>() != null)
        {
            m_Effect.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    IEnumerator InitHeroModel()
    {
        yield return new WaitForSeconds(0.5f);
        m_Top.gameObject.SetActive(true);
        m_Right.gameObject.SetActive(true);
        m_Left.gameObject.SetActive(true);
        while (AssetLoader.Inst.IsReadyOver==false)
        yield return 1;
        Show3DModel(m_CurCard);
    }
   /// <summary>
    /// 显示3D模型
    /// </summary>
    /// <param name="card"></param>
    private void Show3DModel(ObjectCard m_Card)
    {
        ClearModel();

        m_Point = GameObject.Find("pos").transform;

        ArtresourceTemplate m_Artresourcedata = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(m_Card.GetHeroData().GetHeroViewID());
        GameObject _AssetRes = AssetLoader.Inst.GetAssetRes(m_Artresourcedata.getArtresources());
        if (_AssetRes != null)
        {
            if (_AssetRes.GetComponent<NavMeshAgent>() != null)
                _AssetRes.GetComponent<NavMeshAgent>().enabled = false;

            m_Card3Dmodel = Instantiate(_AssetRes, m_Point.position, m_Point.rotation) as GameObject;
            float _zoom = m_Artresourcedata.getArtresources_zoom();
            m_Card3Dmodel.transform.localScale = new UnityEngine.Vector3(_zoom, _zoom, _zoom);
            m_Card3Dmodel.transform.parent = m_Point;

            ////设置3D模型摩擦力
            //m_Card3Dmodel.rigidbody.angularDrag = 2.8f;
            //m_Card3Dmodel.rigidbody.mass = 1.5f;

            Animation anim = m_Card3Dmodel.GetComponent<Animation>();
            if (anim == null)
                return;
            m_Card3Dmodel.GetComponent<Animation>().Play("Nidle1");
            m_Card3Dmodel.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }
    }
    
    /// <summary>
    /// 清除模型
    /// </summary>
    private void ClearModel()
    {
        if (m_Card3Dmodel != null)
            Destroy(m_Card3Dmodel);
    }
    void OnBox()
    {
        ClearModel();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}
