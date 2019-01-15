using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using DreamFaction.UI.Core;
using DreamFaction.GameAudio;
using DreamFaction.GameCore;

namespace DreamFaction.UI {

    public class UI_AutoPanel : BaseUI
    {
        public Toggle mFreevBattle;
        public Toggle mFirstBattle;
        public Toggle mFirstHeal;

        public GameObject mFreevBattleEffect;
        public GameObject mFirstBattleEffect;
        public GameObject mFirstHealEffect;

        private EM_SPELL_AI_TYPE mStatus;
        void Awake()
        {
            mFreevBattle.isOn = true;
            mFirstBattle.isOn = false;
            mFirstHeal.isOn = false;
            mFreevBattleEffect.SetActive(true);
            mFirstBattleEffect.SetActive(false);
            mFirstHealEffect.SetActive(false);
            mStatus = EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL;
        }

        void Start()
        {
            FightControler.Inst.SetFightAIState(mStatus);
        }
        public void onToggleValueChange(Toggle item)
        {
            bool isOn = item.isOn;
            GameObject curObj = null;
            EM_SPELL_AI_TYPE curStatue = mStatus;
            if (item == mFreevBattle)
            {
                curObj = mFreevBattleEffect;
                curStatue = EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_NORMAL;
            }
            else if (item == mFirstBattle)
            {
                curObj = mFirstBattleEffect;
                curStatue = EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_ATTACK;
            }
            else if (item == mFirstHeal)
            {
                curObj = mFirstHealEffect;
                curStatue = EM_SPELL_AI_TYPE.EM_SPELL_AI_TYPE_CURE;
            }

            if (curObj)
            {
                curObj.SetActive(isOn);
                if (isOn)
                {
                    mStatus = curStatue;
                    FightControler.Inst.SetFightAIState(mStatus);
                }
            }
        }

        public void onReset()
        {
            FightControler.Inst.SetFightAIState(mStatus);
        }
    }
}
