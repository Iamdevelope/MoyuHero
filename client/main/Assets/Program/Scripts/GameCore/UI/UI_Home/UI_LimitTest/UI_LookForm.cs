using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;

public class UI_LookForm : UI_LookFormBase 
{
    public static string UI_ResPath = "UI_Home/UI_LookForm_1_2";
    public static UI_LookForm Inst;
    private int m_TroopType = 0;                                                                           //战队类型
    private Dictionary<int, OtherHeroInfo> m_HeroAttribute = null;                                         //战队成员（key 位置， value 信息）
    private Transform m_Team1ListPos;                                                                      //阵型1的位置
    private Transform m_Team2ListPos;                                                                      //阵型2的位置
    private List<UI_LookHeroClick> m_Team1BtnList = new List<UI_LookHeroClick>();                          //阵型1按钮组
    private List<UI_LookHeroClick> m_Team2BtnList = new List<UI_LookHeroClick>();                          //阵型2按钮组
    private Transform m_Team1Btns;                                                                         //阵型1按钮组父节点
    private Transform m_Team2Btns;                                                                         //阵型2按钮组父节点
    private List<GameObject> m_GoList = new List<GameObject>();                                            //模型组
    /// <summary>
    /// 设置战队类型
    /// </summary>
    /// <param name="type"></param>
    public void SetTroopType(int type)
    {
        m_TroopType = type;
    }
    /// <summary>
    /// 设置队伍
    /// </summary>
    /// <param name="heroinfo"></param>
    public void SetHeroAttribute(Dictionary<int,OtherHeroInfo> heroinfo)
    {
        if (heroinfo != null)
        {
            m_HeroAttribute = heroinfo;
        }
    }


    public override void InitUIData()
    {
        base.InitUIData();
        Inst = this;
        m_Team1ListPos = GameObject.Find("LookTeamViewRoom/Team1").transform;
        m_Team2ListPos = GameObject.Find("LookTeamViewRoom/Team2").transform;
        m_Team1Btns = selfTransform.FindChild("Team1Buttons");
        m_Team2Btns = selfTransform.FindChild("Team2Buttons");
        
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    /// <summary>
    /// 初始化队伍显示
    /// </summary>
    public void InitTeamShow()
    {
        m_Team1Btns.gameObject.SetActive(false);
        m_Team2Btns.gameObject.SetActive(false);
        if (m_TroopType == 0)
            return;

        if (m_TroopType == 1)
        {
            SetShowTram(m_Team1ListPos);
            InitBtnShow(m_Team1Btns, m_Team1BtnList);
            m_Team1Btns.gameObject.SetActive(true);
        }
        else if (m_TroopType == 2)
        {
            SetShowTram(m_Team2ListPos);
            InitBtnShow(m_Team2Btns, m_Team2BtnList);
            m_Team2Btns.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 初始化队伍Btn组
    /// </summary>
    /// <param name="teamBtns"></param>
    /// <param name="m_TeamBtnList"></param>
    private void InitBtnShow(Transform teamBtns,List<UI_LookHeroClick> m_TeamBtnList)
    {
        m_TeamBtnList.Clear();
        for (int i = 0; i < teamBtns.childCount; i++)
        {
            OtherHeroInfo _info = null;
            m_HeroAttribute.TryGetValue((i + 1), out _info);

            UI_LookHeroClick _lookItem = teamBtns.GetChild(i).GetComponent<UI_LookHeroClick>();

            if (_info == null)
                continue;

            _lookItem.m_HeroId = _info.m_HeroId;
            _lookItem.m_Exp = _info.m_Exp;
            _lookItem.m_HeroLevel = _info.m_HeroLevel;
            _lookItem.m_Hp = _info.m_Hp;
            _lookItem.m_PysicalAttack = _info.m_PysicalAttack;
            _lookItem.m_Physicaldefence = _info.m_Physicaldefence;
            _lookItem.m_MagicAttack = _info.m_MagicAttack;
            _lookItem.m_MagicDefence = _info.m_MagicDefence;
            _lookItem.m_Skill1 = _info.m_Skill1;
            _lookItem.m_Skill2 = _info.m_Skill2;
            _lookItem.m_Skill3 = _info.m_Skill3;
            _lookItem.m_HeroViewId = _info.m_HeroViewId;

            _lookItem.InitShowUI();
            m_TeamBtnList.Add(_lookItem);
        }

    }
    /// <summary>
    /// 设置显示队伍
    /// </summary>
    /// <param name="teamPos"></param>
    private void SetShowTram(Transform teamPos)
    {
        ClearTramModel();
        for (int i = 0; i < teamPos.childCount; i++)
        {
            OtherHeroInfo _info = null;
            bool isNull = m_HeroAttribute.TryGetValue((i+1), out _info);
            if (isNull == false) continue;
            int _artResId = _info.m_HeroViewId;
            ArtresourceTemplate _artRes = (ArtresourceTemplate)DataTemplate.GetInstance().m_ArtresourceTable.getTableData(_artResId);
            GameObject _assetRes = AssetLoader.Inst.GetAssetRes(_artRes.getArtresources());
            GameObject _go = Instantiate(_assetRes, teamPos.GetChild(i).position, teamPos.GetChild(i).rotation) as GameObject;
            if (_go != null)
            {
                float _zoom = _artRes.getArtresources_zoom();
                _go.transform.localScale = new Vector3(_zoom, _zoom, _zoom);
                Animation _anim = _go.GetComponent<Animation>();
                if (_anim != null)
                {
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


            m_GoList.Add(_go);
        }
    }
    /// <summary>
    /// 清空模型
    /// </summary>
    private void ClearTramModel()
    {
        for (int i = 0; i < m_GoList.Count; i++)
        {
			Destroy(m_GoList[i].gameObject);
        }
        m_GoList.Clear();
    }


    /// <summary>
    /// 关闭按钮
    /// </summary>
    protected override void OnClickCloseBtn()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        if (m_GoList.Count > 0)
        {
            ClearTramModel();
        }
    }

    protected override void OnClickButton1()
    {
    }

    protected override void OnClickButton2()
    {
    }

    protected override void OnClickButton3()
    {
    }

    protected override void OnClickButton4()
    {
    }

    protected override void OnClickButton5()
    {
    }
}
