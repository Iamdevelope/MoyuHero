using UnityEngine;
using System.Collections;
using DreamFaction.LogSystem;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;

namespace DreamFaction.GameCore
{
    /// <summary>
    /// Loading场景的逻辑控制器
    /// </summary>
    public class LoadingControler : BaseControler
    {
        //单例
        private static LoadingControler _inst;
        //异步对象
        private AsyncOperation async;
        //加载进度
        private float progress;
        //设置进度条播放时间
        float progressTime = 1.0f;
        float time = 0.0f;
        private bool bStarLoading = false;

        // ============================= 公共属性(限制外部修改) ===================
        public static LoadingControler Inst
        {
            get
            {
                return _inst;
            }
        }

        // ============================= 继承接口 =================================
        protected override void InitData()
        {
            if (_inst == null)
            {
                _inst = this;
            }
            else
            {
                GameObject.Destroy(this);
            }

            SceneManager.Inst.EndChangeScene(SceneEntry.Loading.ToString());

            StartCoroutine(LoadNextScene());
        }

        protected override void UpdateData()
        {
            if (bStarLoading)
            {
                time += Time.deltaTime;
                if (time < progressTime)
                {
                    progress += (Time.deltaTime / progressTime);
                    
                    if (progress > 0.9)
                    {
                        progress = 1.0f;
                    }
                    UI_Loading.Inst.SetValue(progress);
                }
                else
                {
                    async.allowSceneActivation = true;
                }
            }
        }
        protected override void DestroyData()
        {

            _inst = null;

        }
        // ============================= 公共接口 =================================

		public void SetProgress(float _value)
		{
			progress = _value;
			LogManager.Log("加载进度:" + progress);
		}
        // ============================= 私有函数 =================================
        private IEnumerator LoadNextScene()
        {
            //if (AppManager.Inst.UseServerRes)
            //{
			AssetLoader.Inst.ReadyloadRes(SceneManager.Inst.NextloadScene.ToString());

            while (AssetLoader.Inst.IsReadyOver == false)
                yield return 1;
            //}

            //if (GameControler.Inst.nextLoadScene == GameState.Fight && AppManager.Inst.UseServerRes)
            //{
            //    StartCoroutine(LoadLevelCoroutine(GameControler.Inst.nextLoadScene.ToString()));
            //}
            //else
            {

                yield return new WaitForEndOfFrame();
                bStarLoading = true;

                async = Application.LoadLevelAsync(SceneManager.Inst.NextloadScene.ToString());
				//GameEventDispatcher.Inst.addEventListener(GameEventID.G_DestroyLoadingObj, DestroyLoadingObj);
                async.allowSceneActivation = false;
                yield return async;

            }
        }
    }

}
