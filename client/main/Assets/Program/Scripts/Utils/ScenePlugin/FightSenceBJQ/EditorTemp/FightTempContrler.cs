using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
namespace DreamFaction.GameSceneEditorText
{
    /// <summary>
    /// 测试战斗管理器
    /// </summary>
    public class FightTempContrler : MonoBehaviour
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static FightTempContrler inst;
        private float hSliderValue = 1.0F;
        /// <summary>
        /// 场景状态
        /// </summary>
        public enum SenceType
        {
            /// <summary>
            /// 整队
            /// </summary>
            LineUp,
            /// <summary>
            /// 战斗
            /// </summary>
            Fight
        }
        /// <summary>
        /// 场景状态
        /// </summary>
        public SenceType Sencetype;
        /// <summary>
        /// 战斗中心点
        /// </summary>
        public Vector3 FightCenterPos;
        private void Awake()
        {
            inst = this;
        }
        private void Start()
        {
			GameEventDispatcher.Inst.addEventListener(GameEventID.SE_PrepareEnemy, FightEnter);
        }
        private void Update()
        {
            FightCenterPosUpdate();
        }
        private void FightEnter()
        {
            Sencetype = SenceType.Fight;
        }
        private void FightCenterPosUpdate()
        {
            switch(Sencetype)
            {
                case SenceType.LineUp:
                    FightCenterPos = HeroGroupContrler.inst.HeroCenterPos;
                    break;
                case SenceType.Fight:
                    FightCenterPos= GetFightCenterPos();
                    break;
            }
        }
        private Vector3 GetFightCenterPos()
        {
            if(MonsterGroupContrler.inst.MonstersList.Count>0)
               return (HeroGroupContrler.inst.HeroCenterPos + MonsterGroupContrler.inst.MonsterCenterPos) / 2;
            else
               return HeroGroupContrler.inst.HeroCenterPos;
        }
   
        void OnGUI()
        {
            hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue, 1.0F, 10.0F);
            Time.timeScale = hSliderValue;
        }


        private void OnDestroy()
        {
            inst = this;
        }
    }
}

