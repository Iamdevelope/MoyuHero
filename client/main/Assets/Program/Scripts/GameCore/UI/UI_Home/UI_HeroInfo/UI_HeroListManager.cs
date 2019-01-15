using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DG.Tweening;
using GNET;
namespace DreamFaction.UI
{
    /// <summary>
    /// 英雄item 辅助类
    /// </summary>
    public class HeroItemData
    {
        public ObjectCard objcet = null;  //当ObjectCard不为空时  就是以获取英雄
        public int heroTableID = -1;       //当heroTableID不为空时  就是未获取英雄
    }
    public class UI_HeroListManager : BaseUI
    {
        public static UI_HeroListManager _instance;

        private int m_MaxCount;//当前可容纳的英雄个数

        public List<UI_HeroListItem> heroList = new List<UI_HeroListItem>();
        private Transform _Grid;
        private LoopLayout _gridLayout;
        private ScrollRect _scrollRect;
        private Slider _slider;

        private Button SetHeroBagBtn;                                                        //显示英雄背包购买界面
        private Text SortHeroBagtext;                                                        //显示英雄背包排序
        private Text curCount;                                                               //当前英雄个数
        private Text maxCount;                                                               //最大英雄个数
        private Text MaxBagnum;                                                              //英雄背包数量扩充上线
        private Text ConNum;                                                                 //英雄背包扩充消耗
        private int CurtPageCount;                                                           //当前单页最多显示个数
        private int CurtRowCount;                                                            //当前单行最多显示个个数

        private List<ObjectCard> tempList;
        private int count;
        private int HandSortType;                                                            //1为品质排序2为等级排序
        private GameObject AddHeroObj;                                                       //扩充按钮
        private GameObject m_addBg;                                                          //增加按钮的背景

        private int mSelectHeroId = -1;                                                      //记录当前选中的英雄卡牌数组下标索引;

        private UI_SlideBtn SortSlidebtn;
        private Button QualitysortBtn;                                                       //品质排序按钮
        private Button LevelsortBtn;                                                         //等级排序按钮
        private GameObject Card3Dmodel;                                                      //当前实例化3D模型
        private Transform _Point;                                                            //3D模型实例化位置
        private GameObject ModelRotaeBtn;                                                    //3D模型旋转按钮
        public bool iSRotate;                                                                //3D模型旋转开关
        private float Card3DRoteh;                                                           //3D模型旋转参数
        private float Card3DRotev;                                                           //3D模型旋转参数
        private Vector3 Torque;                                                              //旋转力数值
        private UI_HeroListItem curHLItem;                                                   //记录当前选中的Item
        private Animation m_ModelAnim;
        public int m_SelectRecruitTableId = -1;                                             //点击招募 需要招募的英雄表id
        public bool isNewGuide = false;                                                      //是否是新手引导

        private List<HeroItemData> heroDatas = new List<HeroItemData>();
        List<HeroTemplate> noGetHeros = new List<HeroTemplate>();   //未获取的英雄


        public void SetCurHeroListItem(UI_HeroListItem item)
        {
            curHLItem = item;
        }

        public UI_HeroListItem GetCurHeroListItem()
        {
            return curHLItem;
        }
        public override void InitUIData()
        {
            _instance = this;
            base.InitUIData();
            _Grid = selfTransform.FindChild("HeroList/ListLayOut");
            _gridLayout = selfTransform.FindChild("HeroList/ListLayOut").GetComponent<LoopLayout>();

            curCount = selfTransform.FindChild("SortObj/NumberBg/CurPlayers_txt").GetComponent<Text>();
            maxCount = selfTransform.FindChild("SortObj/NumberBg/MaxPlayers_txt").GetComponent<Text>();

            AddHeroObj = selfTransform.FindChild("SortObj/NumberBg").gameObject;
            m_addBg = selfTransform.FindChild("SortObj/NumberBg/Image").gameObject;


            SetHeroBagBtn = selfTransform.FindChild("SortObj/NumberBg/add").GetComponent<Button>();
            SetHeroBagBtn.onClick.AddListener(new UnityAction(OnSetHeroBagBtn));


            SortSlidebtn = selfTransform.FindChild("SortObj/SortHeroBagBtn").GetComponent<UI_SlideBtn>();
            SortHeroBagtext = selfTransform.FindChild("SortObj/SortHeroBagBtn/Text").GetComponent<Text>();
            selfTransform.FindChild("SortObj/LevelsortBtn").GetComponent<Button>().onClick.AddListener(OnLevelsortBtn);
            selfTransform.FindChild("SortObj/QualitysortBtn").GetComponent<Button>().onClick.AddListener(OnQualitysortBtn);
            ////点击响应 
            //selfTransform.FindChild("ModelRotaeBtn").GetComponent<Button>().onClick.AddListener(new UnityAction(OnclickModelBtn));

            GameEventDispatcher.Inst.addEventListener(GameEventID.KE_HeroBagItemSizeShow, OnKE_HeroBagItemSizeShow);
            GameEventDispatcher.Inst.addEventListener(GameEventID.UI_FragmentComposeHeroSuccess, OnRecruitSuccess);

            HeroListSort();
        }
        void TranHeroList(List<ObjectCard> list)
        {
            heroDatas.Clear();
            Dictionary<int, int> noGetIDs = new Dictionary<int, int>();
            noGetHeros.Clear();
            //先显示已拥有英雄
            foreach (ObjectCard oc in list)
            {
                HeroItemData data = new HeroItemData();
                data.objcet = oc;
                data.heroTableID = -1;
                heroDatas.Add(data);
                noGetIDs.Add(GameUtils.GetHeroIDNum(oc.GetHeroRow().GetID()), -1);
            }

			// 插入一条无用数据
			if (ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count % 2 != 0) 
			{
				heroDatas.Add(new HeroItemData());		
			}

            //再显示未获得英雄
            Dictionary<int, IExcelBean> table = DataTemplate.GetInstance().m_HeroTable.getData();
            int id = -1;
            foreach (var item in table)
            {
                string endChar = item.Key.ToString().Substring(item.Key.ToString().Length - 1, 1);
                string paixuId = ((HeroTemplate)item.Value).getPaxid().ToString();
                // Debug.LogError("substring:" + endChar + "---paixun:" + paixuId);
                if (endChar.Contains(paixuId) && !noGetIDs.TryGetValue(GameUtils.GetHeroIDNum(item.Key), out id))
                {
                    noGetHeros.Add((HeroTemplate)item.Value);
                    noGetIDs.Add(GameUtils.GetHeroIDNum(item.Key), -1);
                }
            }
            //排序
            noGetHeros.Sort(new NotHeroComparer());
            for (int i = 0; i < noGetHeros.Count; i++)
			{
			   HeroItemData data = new HeroItemData();
               data.objcet = null;
               data.heroTableID = noGetHeros[i].GetID();
               heroDatas.Add(data);
			}
          
        }

        public override void InitUIView()
        {
            CurtPageCount = 9;
            CurtRowCount = 3;

            InitHeroBagCount();
			int getcount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
			int nogetcount = noGetHeros.Count;
			
			if (getcount % _gridLayout.columns != 0) 
			{
				getcount = (getcount / _gridLayout.columns + 1) * _gridLayout.columns;
			}
					
			_gridLayout.cellCount = getcount + nogetcount;
			_gridLayout.SeparatorIndex = getcount;
			_gridLayout.updateCellEvent += UpdateHeroItem;
			_gridLayout.Reload();
            //GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_HeroChangeTarget, m_SelectCard);
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        void Reload()
        {
            HeroListSort();

			int getcount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
			int nogetcount = noGetHeros.Count;

			if (getcount % _gridLayout.columns != 0) 
			{

				getcount = (getcount / _gridLayout.columns + 1) * _gridLayout.columns;
			}


            _gridLayout.cellCount = getcount + nogetcount;
			_gridLayout.SeparatorIndex = getcount;
            _gridLayout.updateCellEvent += UpdateHeroItem;
            _gridLayout.Reload();
        }

        /// <summary>
        /// 英雄列表排序
        /// </summary>
        private void HeroListSort()
        {
            tempList = new List<ObjectCard>();
            List<ObjectCard> temp = new List<ObjectCard>();
            List<ObjectCard> objCardList = ObjectSelf.GetInstance().HeroContainerBag.GetYetFormList(ref temp);
            objCardList.Sort(new HeroComparer());
            for (int i = 0; i < objCardList.Count; i++)
            {
                tempList.Add(objCardList[i]);
            }

            temp.Sort(new HeroComparer());
            for (int i = 0; i < temp.Count; i++)
            {
                tempList.Add(temp[i]);
            }

            TranHeroList(tempList);
        }

        void UpdateHeroItem(int index, RectTransform cell)
        {
            HeroItemData objHero = heroDatas[index];
            UI_HeroListItem uiIt = cell.gameObject.GetComponent<UI_HeroListItem>();
            if (uiIt == null)
            {
                uiIt = cell.gameObject.AddComponent<UI_HeroListItem>();
                heroList.Add(uiIt);
            }

			// 有一个空
			if (ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count % 2 != 0 && index == ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count) 
			{
				uiIt.index = index;
				uiIt.m_id = index;
				uiIt.Initialize (objHero, ItemType.Empty);
			} 
			else 
			{
				uiIt.index = index;
				uiIt.m_id = index;
				uiIt.Initialize (objHero);
			}
//
//			if (index < ObjectSelf.GetInstance ().HeroContainerBag.GetHeroList ().Count) {
//								uiIt.index = index;
//								uiIt.m_id = index;
//								uiIt.Initialize (objHero);
//						} 
//			//else if(index == 
//			else
//			{
//				uiIt.index = index;
//				uiIt.m_id = index;
//				uiIt.Initialize(objHero);
//			}

//            if (index == noGetHeros.Count && (noGetHeros.Count % 2 != 0))   //当显示未获取的英雄时  需要空出一个空格
//            {
//                uiIt.index = index;
//                uiIt.m_id = index;
//                uiIt.Initialize(objHero, ItemType.Empty);
//            }
//            else
//            {
//                uiIt.index = index;
//                uiIt.m_id = index;
//                uiIt.Initialize(objHero);
//            }

        }
        public GameObject GetCard3Dmodel() { return Card3Dmodel; }

        public void SetGridActive(bool active)
        {
            //_Grid.gameObject.SetActive(active);
        }
        //当前选择的英雄
        public void SelectedHero(ObjectCard _card)
        {

        }
        private void ModelCear()
        {
            //if (Card3Dmodel != null)
            //    Destroy(Card3Dmodel);
        }
        //显示英雄背包扩充
        private void OnSetHeroBagBtn()
        {
            //int MaxCount = m_MaxCount + DataTemplate.GetInstance().m_GameConfig.getHero_packset_per_expand();
            //string _counttext = GameUtils.getString("hero_info_expandform_content") +"<color=#00e6f2>"+ MaxCount.ToString()+"</color>";
            //int _conum = DataTemplate.GetInstance().m_GameConfig.getHero_packset_expand_cost()[ObjectSelf.GetInstance().HeroBuyCount];
            //string _kc = GameUtils.getString("heromelt_button4");
            //string _conNum;
            //if (ObjectSelf.GetInstance().Gold < _conum)
            //{
            //    _conNum = "<color=#ff0000ff>" + _conum.ToString() + "</color>";
            //}
            //else
            //{
            //    _conNum = _conum.ToString();
            //}
            //UI_RechargeBox _box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            //_box.SetIsNeedDescription(true);
            //_box.SetDescription_text(_counttext);
            //_box.SetConNum(_conNum);
            //_box.SetLeftBtn_text(_kc);
            //_box.SetLeftClick(OnAddHeroBagBtn);
            //_box.SetMoneyInfo((int)EM_RESOURCE_TYPE.Gold, (int)ObjectSelf.GetInstance().Gold);
            //_box.SetMoneyInfoActive(true);
        }
        //英雄背包扩充
        private void OnAddHeroBagBtn()
        {
            //int _conum = DataTemplate.GetInstance().m_GameConfig.getHero_packset_expand_cost()[ObjectSelf.GetInstance().HeroBuyCount];
            //if (ObjectSelf.GetInstance().Gold < _conum)
            //{
            //    UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
            //    UI_RechargeBox _box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();
            //    string _counttext = GameUtils.getString("common_diamondenough_content");
            //    string _kc = GameUtils.getString("common_button_recharge");
            //    _box.SetIsNeedDescription(true);
            //    _box.SetDescription_text(_counttext);
            //    _box.SetLeftBtn_text(_kc);

            //}
            //else
            //{
            //    CBagExpansion addbag = new CBagExpansion();
            //    addbag.bagtype = 3;
            //    IOControler.GetInstance().SendProtocol(addbag);
            //}

        }
        //初始化英雄背包上线
        private void InitHeroBagCount()
        {
            //if (ObjectSelf.GetInstance().HeroBuyCount >= DataTemplate.GetInstance().m_GameConfig.getHero_packset_max_purchase())
            //{
            //    m_addBg.SetActive(false);
            //    SetHeroBagBtn.gameObject.SetActive(false);
            //}
            //count = tempList.Count;
            //m_MaxCount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();

            //curCount.text = count.ToString();
            //maxCount.text = "/" + m_MaxCount;
        }
        //背包更新
        private void OnKE_HeroBagItemSizeShow()
        {
            //UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
            //count = tempList.Count;
            //m_MaxCount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax();

            //curCount.text = count.ToString();
            //maxCount.text = "/" + m_MaxCount;
            //string msgtext = string.Empty;
            //if (ObjectSelf.GetInstance().HeroBuyCount >= DataTemplate.GetInstance().m_GameConfig.getHero_packset_max_purchase())
            //{
            //    msgtext = GameUtils.getString("hero_info_expand_tip2");
            //    InterfaceControler.GetInst().AddMsgBox(msgtext, this.transform.parent);
            //    m_addBg.SetActive(false);
            //    SetHeroBagBtn.gameObject.SetActive(false);
            //}
            //else
            //{
            //    msgtext = GameUtils.getString("hero_info_expand_tip1") + m_MaxCount.ToString();
            //    InterfaceControler.GetInst().AddMsgBox(msgtext, this.transform.parent);
            //}

        }
        //充值钻石
        private void OnDiamondBuyObjBtn()
        {

        }
        //等级排序
        private void OnLevelsortBtn()
        {
            //    SetSortType(2);
            //    SortSlidebtn.OnClose();
            //    UpdateHeroCardData();
        }
        //品质排序
        private void OnQualitysortBtn()
        {
            //SetSortType(1);
            //SortSlidebtn.OnClose();
            //UpdateHeroCardData();
        }
        private void OnRotateDown(GameObject a)
        {
            //iSRotate = true;
            //UI_HeroListManager._instance.GetCard3Dmodel().rigidbody.isKinematic = false;
        }
        private void OnRotatUp(GameObject a)
        {
            //iSRotate = false;
        }
        //设置排序方式
        private void SetSortType(int type)
        {
            //HandSortType = type;
            //if (HandSortType == 1)
            //{
            //    string _text = GameUtils.getString("hero_info_sort_quality");
            //    SortHeroBagtext.text = _text;
            //}
            //else
            //{
            //    string _text = GameUtils.getString("hero_info_sort_level");
            //    SortHeroBagtext.text = _text;
            //}
        }


        private int SortHeroHandler(ObjectCard leftCard, ObjectCard rightCard)
        {
            bool _accordingToLv = HandSortType == 2;
            int key = GetHeroCardSortKey(rightCard, _accordingToLv) - GetHeroCardSortKey(leftCard, _accordingToLv);
            if (key == 0)
            {
                key = leftCard.GetHeroRow().getId() - rightCard.GetHeroRow().getId();
            }
            return key;
        }
        private int GetHeroCardSortKey(ObjectCard hero, bool accordingToLv = false)
        {
            int _weightFactorTeam = 1000000;
            //            int _weightFactorExploration = 100000;
            int _weightFactoQuality;
            int _weightFactorLevel;
            if (accordingToLv)
            {
                _weightFactoQuality = 1;
                _weightFactorLevel = 100;
            }
            else
            {
                _weightFactoQuality = 1000;
                _weightFactorLevel = 1;
            }

            var _objSelf = ObjectSelf.GetInstance();

            int _sortKey = 0;

            bool[] _teamArray = _objSelf.Teams.GetHeroInTeamList(hero.GetGuid());

            for (int i = 0; i < _teamArray.Length; i++)
            {
                if (_teamArray[i])
                {
                    _sortKey += (_teamArray.Length - i) * 2 - 1;
                }
            }
            _sortKey *= _weightFactorTeam;
            _sortKey += _weightFactoQuality * (hero.GetHeroRow().getQuality());
            _sortKey += _weightFactorLevel * (hero.GetHeroData().Level);

            return _sortKey;
        }

        private void OnDestroy()
        {
            //_instance = null;
            //if (Card3Dmodel != null)
            //    Destroy(Card3Dmodel);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.KE_HeroBagItemSizeShow, OnKE_HeroBagItemSizeShow);
            GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_FragmentComposeHeroSuccess, OnRecruitSuccess);

        }

        /// <summary>
        /// 判断该英雄是否有为获得的时装  [根据策划需求改变，现在没用，防止策划反水 Lyq]
        /// </summary>
        /// <param name="_card"></param>
        public void JudgeAdd(ObjectCard _card)
        {
            //List<int> playerHeroSkinList = ObjectSelf.GetInstance().GetHeroSkinList();
            //int num = 0;
            //int[] HeroSkinList = _card.GetHeroRow().getUseableArtresource();
            //for (int i = 0; i < HeroSkinList.Length; i++)
            //{
            //    if (playerHeroSkinList.Contains(HeroSkinList[i]))
            //    {
            //        num++;
            //    }
            //}
            //for (int i = 0; i < heroList.Count; i++)
            //{
            //    heroList[i].SetActiveSkinImg(false);
            //    if (heroList[i].tableId == _card.GetHeroData().TableID)
            //    {
            //        if (num < HeroSkinList.Length)
            //        {
            //            heroList[i].SetActiveSkinImg(true);
            //        }
            //        else
            //        {
            //            heroList[i].SetActiveSkinImg(false);
            //        }
            //    }
            //}
        }


        void OnclickModelBtn()
        {
            //m_ModelAnim = Card3Dmodel.GetComponent<Animation>();
            //if (m_ModelAnim != null && m_ModelAnim.IsPlaying("Nidle1"))
            //{
            //    m_ModelAnim.CrossFade("Fidle0", 0.3f);
            //    StartCoroutine(OnWaitAnimEnd());
            //}
        }

        //private IEnumerator OnWaitAnimEnd()
        //{
        //    //yield return new WaitForSeconds(m_ModelAnim.clip.length);
        //    //if (m_ModelAnim != null)
        //    //{
        //    //    m_ModelAnim.CrossFade("Nidle1", 0.3f);
        //    //}
        //    //anim.wrapMode = WrapMode.Loop;
        //}
        void OnRecruitSuccess()
        {
            if (m_SelectRecruitTableId != -1)
            {
                Reload();
                List<ObjectCard> list= ObjectSelf.GetInstance().HeroContainerBag.GetHeroList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].GetHeroRow().GetID() == m_SelectRecruitTableId)
                    {
                        UICommonManager.Inst.ShowHeroGet(list[i]);
                        break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 未获得英雄排序 可以合成碎片比例大的 --碎片数量多的--品质--》资质---》ID
    /// </summary>
    public class NotHeroComparer : IComparer<HeroTemplate>
    {
        public int Compare(HeroTemplate left, HeroTemplate right)
        {
           int leftCount= ObjectSelf.GetInstance().CommonItemContainer.GetFragmentCount(left.GetID());
           int rightCount=ObjectSelf.GetInstance().CommonItemContainer.GetFragmentCount(right.GetID());
           int leftNeedCount=left.getSyntheticCount();
           int rightNeedCount=right.getSyntheticCount();
           if((float)leftCount/leftNeedCount<(float)rightCount/rightNeedCount)
           {
              return 1;
           }
           else if((float)leftCount/leftNeedCount==(float)rightCount/rightNeedCount)
           {
               if(leftCount<rightCount)
               {
                   return 1;
               }
               else if(leftCount==rightCount)
               {
                   if (left.getQuality() < right.getQuality())
                   {
                       return 1;
                   }
                   else if (left.getQuality() == right.getQuality())
                   {
                       if (left.getBorn() < right.getBorn())
                       {
                           return 1;
                       }
                       else if (left.getBorn() == right.getBorn())
                       {
                           if (left.GetID() > right.GetID())
                           {
                               return 1;
                           }
                           else if (left.GetID() == right.GetID())
                           {
                               return 0;
                           }
                           else
                           {
                               return -1;
                           }
                       }
                       else
                       {
                           return -1;
                       }
                   }
                   else
                   {
                       return -1;
                   }
               }
               else 
               {
                   return -1;
               }
           }
            else
           {
               return -1;
           }
        }
    }

}

