using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;

/***********************
   1、	资源控制（加载和释放）
        （1）	因为要用WWW下载所以只能打包成assetbundle（不支持mp3格式下载）。而且要在切换场景时预加载（同美术资源的加载方式）。用即时加载的话WWW没有返回值而且会有延迟。
        （2）	主要是战斗场景音效，通用的UI音效不需要释放资源。
   2、	播放
        （1）播放有两个接口。播放音效和背景音乐。并有参数需要设置。 
             第一个参数为声音文件，第二个参数为是否循环，音效默认不循环，背景音乐默认循环。第三个参数为是否可调音高（也就是加速和减速播放）。
        （2）音效控制器中有三个AudioSource，分别播放背景音乐、可加速音效（战斗时的音效）、不可加速音效（UI音效）。设置音量接口。
**********************/

namespace DreamFaction.GameAudio
{
    /// <summary>
    /// 音频控制器，用来控制游戏内的音乐，音效播放/停止！
    /// 音乐资源控制，需要在切换场景时预加载
    /// </summary>
    public class AudioControler : BaseControler
    {
        // 单例
        public static AudioControler Inst;
        // 音频资源组件
        //播放背景音乐
        private AudioSource musicSource = null;
        //播放音效
        private AudioSource soundSource = null;
        private string WWW_LOCAL_PATH;
        //播放不加速音效，比如按钮音效（不受加速影响）
        private AudioSource mSoundSource = null;
        //静态音乐文件，切场景时不释放
        private Dictionary<string, AudioClip> audioStaticStorage = null;
        //动态音乐文件，切场景时释放重新加载
        private Dictionary<string, AudioClip> audioDynamicStorage = null;

        /// <summary>
        /// 初始化数据
        /// </summary>
        protected override void InitData()
        {
            Inst = this;
            audioStaticStorage = new Dictionary<string, AudioClip>();
            audioDynamicStorage = new Dictionary<string, AudioClip>();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        protected override void InitView()
        {
            if (musicSource == null)
                musicSource = gameObject.AddComponent<AudioSource>();
            if (soundSource == null)
                soundSource = gameObject.AddComponent<AudioSource>();
            if (mSoundSource == null)
                mSoundSource = gameObject.AddComponent<AudioSource>();

            musicSource.playOnAwake = false;
            soundSource.playOnAwake = false;

            //读取配置 开启/关闭 音乐/音效 yao_15_6_29
            if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Music)) == 0)
            {
                StartMusic();
            } 
            else
            {
                StopMusic();       
            }

            if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Sound)) == 0)
            {               
                StartSound();
            } 
            else
            {
                StopSound();
            }
            string versionNum = ConfigsManager.Inst.GetClientConfig(ClientConfigs.Version);

//#if UNITY_ANDROID
//              WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
//                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/Android/"
//                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/Android/";
//#elif UNITY_IPHONE
//             WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
//                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/IOS/"
//                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/IOS/";
//#elif UNITY_STANDALONE_WIN
//               WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
//                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/PC/"
//                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/PC/";
//#else 
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/Android/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/Android/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/IOS/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/IOS/";
                    break;
                default:
                    WWW_LOCAL_PATH = AppManager.Inst.IsLocalVersion ?
                                     "file://" + AppManager.Inst.readOnlyPath + "Audio/PC/"
                                    : ConfigsManager.Inst.GetClientConfig(ClientConfigs.ResoursePlatformIP) + "data/" + versionNum + "/Audio/PC/";
                    break;
            }
//#endif

             ClearAudio();

            OnLoad("AllAudio", false);


        }

        //======================================== 公共接口 ========================================

        /// <summary>
        /// 清空缓存中的音效
        /// </summary>
        public void ClearAudio()
        {
            if (audioDynamicStorage.Count > 0)
                audioDynamicStorage.Clear();
        }

        /// <summary>
        /// 在缓存中查找到要播放的音效
        /// </summary>
        /// <param name="audioName">音效名称</param>
        /// <returns>返回AudioClip</returns>
        private AudioClip LoadAudio(string audioName)
        {
            if (audioStaticStorage.ContainsKey(audioName))
            {
                return audioStaticStorage[audioName];
            }
            if (audioDynamicStorage.ContainsKey(audioName))
            {
                return audioDynamicStorage[audioName];
            }
           // LogManager.Log("播放的音乐未预加载！");
            return null;
        }

        /// <summary>
        /// 预加载场景中用到的音效
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <param name="name">是否是静态文件，不需要在切场景时释放</param>
        public void OnLoad(string name, bool isStatic)
        {
            StartCoroutine(StartLoadAudio(name, isStatic));
        }

        /// <summary>
        /// 用协程去加载音乐文件,保存到缓存中
        /// </summary>
        /// <param name="name">音乐文件名称</param>
        /// <param name="name">是否是静态文件，不需要在切场景时释放</param>
        /// <returns>协程返回的指针</returns>
        private IEnumerator StartLoadAudio(string name, bool isStatic)
        {
            StringBuilder localPath = new StringBuilder();
            localPath.Append(WWW_LOCAL_PATH);
            localPath.Append(name);
            localPath.Append(".assetbundle");

            WWW www = new WWW(localPath.ToString());
            yield return www;
            if (www.error != null)
            {
                LogManager.Log(www.error);
            }
            if (www.isDone)
            {
                Object[] bundles = www.assetBundle.LoadAll(typeof(UnityEngine.AudioClip));
                int size = bundles.Length;
                for (int i = 0; i < size; ++i)
                {
                    AudioClip audio = bundles[i] as AudioClip;
                    var audioName = audio.name;
                    if (isStatic)
                    {
                        if (!audioStaticStorage.ContainsKey(audioName))
                        {
                            audioStaticStorage.Add(audioName, audio);
                        }
                    }
                    else
                    {
                        if (!audioDynamicStorage.ContainsKey(audioName))
                        {
                            audioDynamicStorage.Add(audioName, audio);
                        }
                    }
                }

                www.assetBundle.Unload(false);
                www.Dispose();
            }
        }

        /// <summary>
        /// 音乐加速，音高
        /// </summary>
        /// <param name="f">音高值</param>
        public void SoundPitch(float f)
        {
            soundSource.pitch = f;
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">音乐名称</param>
        /// <param name="isLoop">是否循环</param>
        public void PlaySound(string name, bool isLoop)
        {
            PlaySound(name, isLoop, true);
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">音乐名称</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="isFixable">是否可调音高</param>
        public void PlaySound(string name, bool isLoop, bool isFixable)
        {
            if (string.IsNullOrEmpty(name))
            {
                //LogManager.Log("Error: PlaySound Argument is null");
                return;
            }
            AudioClip audio = LoadAudio(name);
            if (audio == null)
            {
                LogManager.LogToFile("Error: LoadAudio is NULL :" + name);
                return;
            }
            PlaySound(audio, isLoop);
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="audio">音乐文件</param>
        /// <param name="isLoop">是否循环</param>
        public void PlaySound(AudioClip audio, bool isLoop)
        {
            PlaySound(audio, isLoop, true);
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">音乐文件</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="isFixable">是否可调音高</param>
        public void PlaySound(AudioClip audio, bool isLoop, bool isFixable)
        {
            if (isLoop)
            {
                if (isFixable)
                {
                    soundSource.clip = audio;
                    soundSource.loop = isLoop;
                    if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Sound)) == 1)
                        return;
                    soundSource.Play();
                }
                else
                {
                    mSoundSource.clip = audio;
                    mSoundSource.loop = isLoop;
                    if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Sound)) == 1)
                        return;
                    mSoundSource.Play();
                }
            }
             else
             {
                 if (isFixable)
                 {
                     soundSource.PlayOneShot(audio,0.9f);
                 }
                 else
                 {
                     mSoundSource.PlayOneShot(audio,0.9f);
                 }
             }
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="audio">音乐文件</param>
        public void PlaySound(AudioClip audio)
        {
            PlaySound(audio, false);
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="name">音乐名称</param>
        public void PlaySound(string name)
        {
            if (name == "null")
            {
                return;
            }
            else
            {
                PlaySound(name, false);
            }
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名称</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayMusic(string name, bool isLoop)
        {
            if (string.IsNullOrEmpty(name))
            {
                LogManager.Log("Error: PlayMusic Argument is null");
                return;
            }
            AudioClip audio = LoadAudio(name);
            if (audio == null)
            {
                LogManager.LogAssert(false);
                LogManager.LogToFile("Error: PlayMusic function for LoadAudio is NULL:" + name);
                return;
            }
            PlayMusic(audio, isLoop);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名称</param>
        public void PlayMusic(string name)
        {
            PlayMusic(name, true);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audio">音乐文件</param>
        /// <param name="isLoop">是否循环</param>
        public void PlayMusic(AudioClip audio, bool isLoop)
        {
            if (isLoop)
            {
                musicSource.clip = audio;
                musicSource.loop = isLoop;
                if (int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Music)) == 1)
                    return;
                musicSource.Play();
            }
            else
            {
                musicSource.clip = null;
                musicSource.PlayOneShot(audio);
            }
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audio">音乐文件</param>
        public void PlayMusic(AudioClip audio)
        {
            PlayMusic(audio, true);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audio">音乐文件</param>
        /// <param name="volum">音量</param>
        public void PlayMusic(AudioClip audio, float volum)
        {
            musicVolume = volum;
            PlayMusic(audio, true);
        }

        /// <summary>
        /// 背景音乐音量
        /// </summary>
        public float musicVolume
        {
            get
            {
                return musicSource==null?0f:musicSource.volume;
            }
            set
            {
                if (musicSource != null )  musicSource.volume = value;
            }
        }

        /// <summary>
        /// 音效音量
        /// </summary>
        public float soundVolume
        {
            get
            {
                return soundSource == null ? 0f : soundSource.volume;
            }
            set
            {
                if (musicSource != null)    mSoundSource.volume = value;
                if (soundSource != null)    soundSource.volume = value;
            }
        }

        /// <summary>
        /// 背景音乐是否在播放中
        /// </summary>
        public bool isMusicPlaying
        {
            get
            {
                return musicSource.isPlaying;
            }
        }

        /// <summary>
        /// 音效是否在播放中
        /// </summary>
        public bool isSoundPlaying
        {
            get
            {
                if (soundSource.isPlaying || mSoundSource.isPlaying)
                    return true;
                else if (!soundSource.isPlaying && !mSoundSource.isPlaying)
                    return false;
                else
                    return false;
            }
        }

        /// <summary>
        /// 停止播放背景音乐
        /// </summary>
        public void StopMusic()
        {
            musicSource.Stop();
        }

        /// <summary>
        /// 开始播放背景音乐
        /// </summary>
        public void StartMusic()
        {
            musicSource.Play();
        }

        /// <summary>
        /// 停止播放音效
        /// </summary>
        public void StopSound()
        {
            soundSource.Stop();
            mSoundSource.Stop();
        }

        /// <summary>
        /// 打开播放音效
        /// </summary>
        public void StartSound()
        {
            soundSource.Play();
            mSoundSource.Play();
        }

        /// <summary>
        /// 退出音乐管理器
        /// </summary>
        protected override void DestroyData()
        {
            Inst = null; 
        }

    }
}

