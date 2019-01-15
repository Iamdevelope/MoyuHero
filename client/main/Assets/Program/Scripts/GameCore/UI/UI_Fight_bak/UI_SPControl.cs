using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.SkillCore;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;


namespace DreamFaction.UI
{

    public class UI_SPControl : BaseUI
    {
        public Transform mSelfAnger;
        public Transform mEnemyAnger;

        private Material mSelfMaterial;
        private Material mEnemyMaterial;

        private float mReal2UnrealDis = 0.2f;

        public bool isFightEnd = false;    // 是否战斗结束

        public override void InitUIData()
        {
            mSelfMaterial = (mSelfAnger.GetComponent<MeshRenderer>() as Renderer).material;
            mEnemyMaterial = (mEnemyAnger.GetComponent<MeshRenderer>() as Renderer).material;
            SetAngerReal(mSelfMaterial, 0);
            SetAngerUnreal(mSelfMaterial, 0);
            SetAngerReal(mEnemyMaterial, 0);
            SetAngerUnreal(mEnemyMaterial, 0);
        }

        public override void InitUIView()
        {
            //Debug.Log("UI_SPControl InitUIView ...");
        }

        void OnDestroy()
        {
            StopCoroutine("onUpdateAnger");
        }

        /// <summary>
        /// 更新怒气事件响应
        /// </summary>
        public void onUpdateAngerCall(ref EM_OBJECT_TYPE type)
        {
            if (isFightEnd) return;

            StopCoroutine("onUpdateAnger");
            bool isSelf = false;
            bool isAdd = false;

            GameConfig config = DataTemplate.GetInstance().m_GameConfig;

            if (type == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
            {
                isSelf = true;
            }

            float value = FightControler.Inst.GetPowerValue(type);
            value = value >= ObjectSelf.GetInstance().GetMaxPowerValue() ? 1 : value / config.getMax_rage_point();
//            value /= config.getMax_rage_point();
            //Debug.Log("into .................");
           // StartCoroutine(onUpdateAnger(value, isSelf, isAdd));
        }

        /// <summary>
        /// 更新怒气显示
        /// </summary>
        /// <param name="value">怒气百分比</param>
        /// <param name="isSelf">是不是我方</param>
        /// <param name="isAdd">值是增加的吗</param>
        /// <returns></returns>
        private IEnumerator onUpdateAnger(float value, bool isSelf, bool isAdd)
        {
            Material material = isSelf ? mSelfMaterial : mEnemyMaterial ;
            SetAngerReal(material, value);
            if (!isAdd) 
            { 
                yield return new WaitForSeconds(mReal2UnrealDis);
            }
            SetAngerUnreal(material, value);
        }

        private void SetAngerUnreal(Material material, float value)
        {
            material.SetFloat("_valueunreal", value);
        }
        private void SetAngerReal(Material material, float value)
        {
            material.SetFloat("_valuereal", value);
        }

    }
}
