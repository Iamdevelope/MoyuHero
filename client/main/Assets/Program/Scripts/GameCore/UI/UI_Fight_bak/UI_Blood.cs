using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DG.Tweening;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_Blood : BaseUI
    {

        const int nLimit = 20;

        public Image levelNum_1 = null;
        public Image levelNum_2 = null;

        //角色id
        [HideInInspector]
        public X_GUID uid = new X_GUID();
        public bool isHero = false;
        //当前值
        private float curValue = 0f;
        //未更新前的值
        private float preValue = 0f;
        //减小量
        private float change = 0f;
        //是否更新血条
        private bool isUpdate = false;

        private int offset = 0;

        private Image forImage = null;
        private Image behImage = null;
        private Text mLevel = null;
        //private Text mSkillName = null; // 技能名称
        private Image mSkillImage = null;   // 技能名称

        private RectTransform rectTran = null;
        private GameObject targetFlag = null;

        private Transform mHeadPosition;
        private UI_BuffAllControl mBuffControl;
        private Vector3 mInitSkillNamePosition; // 技能名称初始位置

        void Awake()
        {
            forImage = transform.FindChild("ForBar").GetComponent<Image>();
            behImage = transform.FindChild("BehBar").GetComponent<Image>();
            mLevel = transform.FindChild("RankBg/Level").GetComponent<Text>();
            mBuffControl = transform.FindChild("BuffGroup").gameObject.AddComponent<UI_BuffAllControl>();
            mSkillImage = transform.FindChild("Skill").GetComponent<Image>();
            //mSkillName.text = "";
            mSkillImage.gameObject.SetActive(false);
            Vector3 localSkill = mSkillImage.transform.localPosition;
            mInitSkillNamePosition = new Vector3(localSkill.x, localSkill.y, localSkill.z);

            rectTran = GetComponent<RectTransform>();
            SetValue(1);
        }

        void Start()
        {

            //UIFightManager.Inst.numControlers.Add(numberControl);
        }

        private Vector3 WorldToUIPoint(Camera camera, Vector3 worldPos)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);
            //pos.z = 0f;   //z一定要为0.
            pos = camera.ScreenToWorldPoint(pos);
            return pos;
        }

        public void SetPosition(Vector3 vPos)
        {
            Vector3 v = WorldToUIPoint(UI_FightControler.Inst.GetCanvasCamera(BaseUIControler.UICanvasFlag.Canvas3), vPos);

            if (v.y > 5 - 0.9)
            {
                v = new Vector3(v.x, 5 - 0.9f, 0);
                offset = 0;
            }

            transform.position = new Vector3(v.x, v.y, 0);


            int x = (int)rectTran.anchoredPosition.x;
            int y = (int)rectTran.anchoredPosition.y;
            rectTran.anchoredPosition3D = new Vector3(x, y + offset, 0);
        }

        //设置新的血量值
        public void SetValue(float v)
        {
            forImage.fillAmount = v;
            curValue = v;
            if (preValue == 0)
                preValue = v;
            else
            {
                change = (preValue - curValue) / nLimit;
                isUpdate = true;
            }
        }

        void Update()
        {
            if (isUpdate)
            {
                if (preValue > curValue)
                {
                    preValue -= change;
                }
                else
                {
                    preValue = curValue;
                    isUpdate = false;
                }
                behImage.fillAmount = preValue;
            }
            //BattleHeadEffect.Inst.UpdateHeadEffect(heroId, HeadEffectType.HeadEffectType_Scared, transform.position);
        }


        public void CreatBattleScare()
        {
            //BattleHeadEffect.Inst.OnFightHeadEffect(heroId, HeadEffectType.HeadEffectType_Scared, transform.position);
        }

        public void AddTargetFlag()
        {
            if (targetFlag != null)
            {
                //RemoveTargetFlag();
                targetFlag.gameObject.SetActive(true);
                return;
            }

            targetFlag = Instantiate(UI_FightControler.Inst.mFlagPre) as GameObject;
            targetFlag.transform.SetParent(transform);
            targetFlag.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rectTran = targetFlag.GetComponent<RectTransform>();
            rectTran.anchoredPosition = new Vector2(0, 100);
        }

        public void RemoveTargetFlag()
        {
            if (targetFlag != null)
            {
                //Destroy(targetFlag);
                //targetFlag = null;
                targetFlag.gameObject.SetActive(false);
            }
        }

        public void Destroy()
        {

        }

        public void setHeadPosition(Transform tans)
        {
            mHeadPosition = tans;
        }
        protected void LateUpdate()
        {
            if (mHeadPosition)
            {
                SetPosition(mHeadPosition.position);
            }
        }

        public void setHeroLevel(int level)
        {
            mLevel.text = level.ToString();
        }

        /// <summary>
        /// 处理buff
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="info"></param>
        public void onSingleBuffRemove(BuffTemplate info)
        {
            mBuffControl.RemoveBuff(info);
        }

        public void onSingleBuffAdd(BuffTemplate info)
        {
            mBuffControl.AddBuff(info);
        }

        public void onShowSkillName(string name)
        {
            //Debug.Log("Skill name is " + name);
            RectTransform skillTrans = mSkillImage.gameObject.GetComponent<RectTransform>();
            Object resobj = UIResourceMgr.LoadSprite(common.defaultPath + name);
            if(resobj!=null)
                mSkillImage.overrideSprite = Instantiate(resobj) as Sprite;
            mSkillImage.SetNativeSize();
            mSkillImage.gameObject.SetActive(true);
            mSkillImage.transform.localScale = new Vector3(1, 0, 1);
            mSkillImage.transform.localPosition = new Vector3(mInitSkillNamePosition.x, mInitSkillNamePosition.y, mInitSkillNamePosition.z);

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(mSkillImage.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutElastic));
            //mySequence.AppendInterval(0.1f);
            mySequence.Append(mSkillImage.transform.DOScale(new Vector3(1.5f, 0, 0), 0.1f).SetEase(Ease.OutQuad));
            mySequence.AppendCallback(onShowNameEnd);

            float y = mSkillImage.transform.localPosition.y + 10;
            mSkillImage.transform.DOLocalMoveY(y, 0.5f);
        }
        private void onShowNameEnd()
        {
            mSkillImage.gameObject.SetActive(false);
        }
    }
}