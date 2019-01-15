using UnityEngine;

using DreamFaction.LogSystem;

namespace DreamFaction.UI
{
    /// <summary>
    /// UI资源加载器
    /// </summary>
    class UIResourceMgr
    {
        /// <summary>
        /// 加载UI图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Sprite LoadSprite(string path)
        {
            try
            {
                Sprite Sp= Resources.Load<Sprite>(path);
                if (Sp == null)
                {
                    Debug.LogError("找不到资源：" + path);
                }
                return Sp;
            }
            catch(System.Exception e)
            {
                LogManager.LogError("精灵图片资源加载失败！！！路径：" + path);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Object LoadPrefab(string path)
        {
            return Resources.Load(path);
        }

        public static Object Clone(Object origi)
        {
            return GameObject.Instantiate(origi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Material LoadMaterial(string path)
        {
            return Resources.Load<Material>(path);
        }
    }
}

