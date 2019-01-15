using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
namespace DreamFaction.GameSceneEditorText
{
    public class ForllowPoint : MonoBehaviour
    {
        public Transform Tag;
        public NavMeshAgent nav;
        public bool isStop=false;
        public bool isMonmentMove=false;
        private void Start()
        {
			GameEventDispatcher.Inst.addEventListener(GameEventID.SE_EnterFightState, FightEnterEnd);
        }
        private void Update()
        {
            if (nav.enabled)
               setPoition();
        }
        private void setPoition()
        {
            nav.SetDestination(Tag.position);
        }
        public void Init(Transform tag)
        {
            nav = this.GetComponent<NavMeshAgent>();
            Tag = tag;
        }
        public void MomentMove()
        {
            this.transform.position = Tag.position;
        }
        public void MomentMoveStop()
        {
            nav.Stop();
            nav.enabled = false;
        }
        public void SetGo()
        {
            nav.enabled = true;
        }
        private void FightEnterEnd()
        {
            nav.Stop();
            nav.enabled = false;
        }
    }
}

