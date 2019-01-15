using UnityEngine;
using System.Collections;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;
namespace DreamFaction.GameSceneEditorText
{
    /// <summary>
    /// 本地测试用动画编辑器脚本
    /// </summary>
    public class StoryAnimTempContrler : MonoBehaviour
    {
        /// <summary>
        /// 保存场景名称
        /// </summary>
        public string SceneName;
        private WWW StoryAnim;
        private StoryAnimDataObj StoryObj;
        private void Start()
        {
            StartCoroutine("LoadAssetbundle");
        }
        private IEnumerator LoadAssetbundle()
        {
			string path = "IPhonePlayer";
			if (Application.platform == RuntimePlatform.IPhonePlayer) path = "IPhonePlayer";
			StoryAnim = new WWW("file:///" + Application.dataPath + "/StreamingAssets/ScenceEditor/"+path+"/" + SceneName + "/StoryAnim/" + SceneName + "StoryAnim" + ".assetbundle");
            yield return StoryAnim;
            if (StoryAnim.error != null)
            {
                Debug.Log(StoryAnim.error);
            }
            if(StoryAnim.isDone)
            {
                StoryObj = StoryAnim.assetBundle.mainAsset as StoryAnimDataObj;
                StoryAnimEditorContrler.GetInst().Init(StoryObj,true);
                Camera.main.transform.parent = GameObject.Find("StoryTempContrler").transform.GetChild(0);
                Camera.main.transform.localPosition = Vector3.zero;
                Camera.main.transform.localEulerAngles = Vector3.zero;
                Invoke("hehe", 0.2f);
            }
        }
        //触发动画
        private void hehe()
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_StoryEnter, 1);
        }
    }
}

