using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using DreamFaction.UI.Core;
using DreamFaction.GameAudio;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;

namespace DreamFaction.UI
{

    public class UI_SkipAnimationPanel : BaseUI
    {

        private Button skipBtn;
        public override void InitUIData()
        {
            skipBtn = selfTransform.FindChild("Button(skipAnimation)").GetComponent<Button>();
            skipBtn.onClick.AddListener(OnClickSkipBtn);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 点击跳过
        /// </summary>
        private void OnClickSkipBtn()
        {
            if (FightControler.isOpeningAnimation)
            {
                FightControler.isOpeningAnimation = false;
                if (FightControler.Inst.GetFightState() != FightState.FightInstantiateHero)
                {
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnd);
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_FightEditorLoadDone);
                }
            }
            else
            {
                if (StoryAnimEditorContrler.GetInst().GetStoryAnimID() != 1)
                {
                    FightEditorContrler.GetInstantiate().HeroPathFightEnterEnd();                    
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnd);
                    //GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_EnterFightState, StoryAnimEditorContrler.GetInst().GetCurrentFightCount());
                }
            }
        }
    }
}
