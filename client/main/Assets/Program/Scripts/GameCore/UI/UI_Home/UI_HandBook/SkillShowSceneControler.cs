using UnityEngine;
using System.Collections;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameNetWork;
using GNET;
using DreamFaction.GameNetWork.Data;
using DreamFaction.UI.Core;

public class SkillShowSceneControler : BaseControler 
{
    public static SkillShowSceneControler Inst;
    private static ArtresourceTemplate m_ArtResData;
    private static ObjectCard m_Card;
    private static int m_HeroTableId = 0;
    private static int m_AttackType = 0;//0近战 1远程
    private Transform m_HeroNearPos;//近程英雄位置
    private Transform m_HeroFarPos;//远程英雄位置
    private Transform m_BossPos;//敌人位置
    private Transform m_TeamMatePos;//队友位置（非为队友增幅的不实例化）
    protected override void InitData()
    {
        base.InitData();
        Inst = this;
        gameObject.AddComponent<SceneObjectManager>();
        gameObject.AddComponent<EffectManager>();
        m_HeroNearPos = transform.FindChild("NearHero");
        m_HeroFarPos = transform.FindChild("FarHero");
        m_BossPos = transform.FindChild("Boss");
        m_TeamMatePos = transform.FindChild("TeamMate");
    }

    protected override void InitView()
    {
        base.InitView();
        SceneObjectManager.GetInstance().InitSkillShowState();
        CreateMonterObject();
        CreateHeroObject();
        CreateTeamMate();
        
    }
    /// <summary>
    /// 设置英雄的数据
    /// </summary>
    /// <param name="artRes">资源表</param>
    /// <param name="card">当前卡牌</param>
    /// <param name="heroTabId">英雄表ID</param>
    /// <param name="attackType">攻击类型（远近）</param>
    public static void SetHeroData(ArtresourceTemplate artRes,ObjectCard card,int heroTabId,int attackType)
    {
        m_ArtResData = artRes;
        m_Card = card;
        m_HeroTableId = heroTabId;
        m_AttackType = attackType;
    }
    /// <summary>
    /// 创建英雄
    /// </summary>
    private void CreateHeroObject()
    {
        GameObject _heroObject = AssetLoader.Inst.GetAssetRes(m_ArtResData.getArtresources());
        if (_heroObject != null)
        {
            Transform _pos = null;
            if (m_AttackType == 0)
                _pos = m_HeroNearPos;
            else
                _pos = m_HeroFarPos;
            GameObject _obj = Instantiate(_heroObject, _pos.position, _pos.rotation) as GameObject;
            _obj.transform.localScale = new Vector3(2,2,2);
            Animation _anim = _obj.GetComponent<Animation>();
            if (_anim == null)
                return;
            if (_obj.GetComponent<Animation>()["Idle1"] != null)
            {
                _obj.GetComponent<Animation>().CrossFade("Idle1");
            }
            else if (_obj.GetComponent<Animation>()["Nidle1"] != null)
            {
                _obj.GetComponent<Animation>().CrossFade("Nidle1");
            }
            _obj.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            SceneObjectManager.GetInstance().SceneObjectAddHero(_obj, m_HeroTableId, m_Card);
        }
    }
    /// <summary>
    /// 创建怪物
    /// </summary>
    private void CreateMonterObject()
    {
        MonsterTemplate _monsterData = (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(GlobalMembers.SPELL_SHOW_MONTER_ID);
        ArtresourceTemplate _atrRes = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_monsterData.getArtresources());
        GameObject _assetRes = AssetLoader.Inst.GetAssetRes(_atrRes.getArtresources());
        if (_assetRes != null)
        {
            GameObject _go = Instantiate(_assetRes, m_BossPos.position, m_BossPos.rotation) as GameObject;
            SceneObjectManager.GetInstance().SceneObjectAddMonster(_go, 1, 0);
            //_go.transform.rotation = Quaternion.Euler(_go.transform.rotation.x, 180, _go.transform.rotation.z);
            _go.transform.localScale = new Vector3(3, 3, 3);
            Animation _anim = _go.GetComponent<Animation>();
            if (_anim == null)
                return;
            if (_go.GetComponent<Animation>()["Idle1"] != null)
            {
                _go.GetComponent<Animation>().CrossFade("Idle1");
            }
            else if (_go.GetComponent<Animation>()["Nidle1"] != null)
            {
                _go.GetComponent<Animation>().CrossFade("Nidle1");
            }
            _go.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }

    }
    /// <summary>
    /// 创建队友
    /// </summary>
    /// 如果释放目标为敌人就不创建该对象
    private void CreateTeamMate()
    {
        SkillTemplate _skill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(m_Card.GetHeroData().SpellDataList[0].SpellID);
        if (_skill.getTarget() == 2)
            return;

        ObjectCard _teamMateCard = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0];
        HeroTemplate _heroData = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(_teamMateCard.GetHeroData().TableID);
        //ObjectCard _teamMateCard = new ObjectCard();
        //Hero hero = new Hero();
        //hero.heroid = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList()[0].GetHeroData().TableID;
        //_teamMateCard.GetHeroData().Init(hero);
        ArtresourceTemplate _artResData = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_heroData.getArtresources());
        GameObject _heroObject = AssetLoader.Inst.GetAssetRes(_artResData.getArtresources());
        if (_heroObject != null)
        {
            GameObject _obj = Instantiate(_heroObject, m_TeamMatePos.position, m_TeamMatePos.rotation) as GameObject;
            _obj.transform.localScale = new Vector3(2, 2, 2);
            SceneObjectManager.GetInstance().SceneObjectAddHero(_obj, _heroData.getId(), _teamMateCard);
            Animation _anim = _obj.GetComponent<Animation>();
            if (_anim == null)
                return;
            if (_obj.GetComponent<Animation>()["Idle1"] != null)
            {
                _obj.GetComponent<Animation>().CrossFade("Idle1");
            }
            else if (_obj.GetComponent<Animation>()["Nidle1"] != null)
            {
                _obj.GetComponent<Animation>().CrossFade("Nidle1");
            }
            _obj.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        }
    }

}
