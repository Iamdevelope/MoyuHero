using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HandleImage : EditorWindow
{
	[MenuItem("GameTool/UGUI资源检查工具/功能入口")]
    public static void ChangeTextureType()
	{
        HandleImage win = (HandleImage)EditorWindow.GetWindow(typeof(HandleImage), true, "ChangeTextureType");
		win.ShowNotification(new GUIContent("请先选中需要操作的文件或者文件夹"));
	}

//     [MenuItem("GameTool/UGUI资源检查工具/CheckSpriteSize")]
//     public static void CheckSpriteSize()
//     {
//         HandleImage win = (HandleImage)EditorWindow.GetWindow(typeof(HandleImage), true, "CheckSpriteSize");
//         win.ShowNotification(new GUIContent("替换操作无法撤销，请谨慎操作"));
//     }
	void OnGUI()
	{
        if (GUILayout.Button("改变Texture格式到Sprite2D"))
		{
			Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
			foreach (Object o in objs)
			{
				string path = AssetDatabase.GetAssetPath(o);
				if (path.Contains(".png"))
				{
					if (o is Texture2D)
					{
						Texture2D tex = o as Texture2D;
						SetTextureImporterFormat(tex, true);
						Debug.Log(path + "  Texture   " + tex.width + "  " + tex.height);
					}
				}
			}
		}
        if ( GUILayout.Button ( "改变Audio格式到 将 3D 音源改为 2D" ) )
        {
            Object [] objs = Selection.GetFiltered ( typeof ( Object ), SelectionMode.DeepAssets );
            foreach ( Object o in objs )
            {
                string path = AssetDatabase.GetAssetPath ( o );
                if ( path.Contains ( ".wav" ) )
                {
                    if ( o is AudioClip )
                    {
                        AudioClip audio = o as AudioClip;
                        SetAudioImporterFormat ( audio );
                        Debug.Log ( path + "  audio   " + audio.name );
                    }
                }
            }
        }
        if (GUILayout.Button("检查九宫格图片尺寸是否满足2的n次幂需求"))
        {
            OnExportCheckText();
        }
        if (GUILayout.Button("批量处理UIPrefabs中Text控件的Font属性"))
        {
            //OnExportCheckText();
            OnResetMissingFont();
        }

        if (GUILayout.Button("批量处理button下text加shadow脚本"))
        {
            OnAddButtonAnimation();
        }

        if (GUILayout.Button("批量处理UIPrefabs添加Button点击动画"))
        {
            //OnExportCheckText();
            OnAddButtonClickAnimation();
        }

	}

    //返回一个数是否是2的n次幂 [8/25/2015 Zmy]
    public static bool getResult(int num)
    {
        return ((num & (num - 1)) == 0) ? true : false;
    }
    public static void OnExportCheckText()
    {
        if (File.Exists("Assets/CheckSpriteLog.txt"))
        {
            System.IO.File.Delete("Assets/CheckSpriteLog.txt");
        }

        FileStream verFile = new FileStream("Assets/CheckSpriteLog.txt", FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(verFile);
        sw.WriteLine("以下图片使用了九宫格切割，并且图片尺寸不符合规格：");

        Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object o in objs)
        {
            string path = AssetDatabase.GetAssetPath(o);
            if (path.Contains(".png"))
            {
                if (o is Texture2D)
                {
                    Texture2D tex = o as Texture2D;
                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
		            if (importer != null)
                    {
                        Vector4 _Border = importer.spriteBorder;
                        if (_Border.w != 0 || _Border.x != 0 || _Border.y != 0 || _Border.z != 0 )
                        {
                            int _w = tex.width;
                            int _h = tex.height;

                            if (getResult(_w) == false || getResult(_h) == false)
                            {
                                RecommendSize(ref _w);
                                RecommendSize(ref _h);
                                sw.WriteLine(o.name + "  推荐尺寸为:" + _w + "x" + _h);
                            }
                        }
                    }
                }
            }
        }

        sw.Flush();
        sw.Close();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("", "已生成到统计文本信息到Assets/CheckSpriteLog.txt下", "OK");
    }
    public static void RecommendSize(ref int nSize)
    {
        if (nSize < 32)
        {
            nSize = 32;
        }
        else if (nSize < 64)
        {
            nSize = 32;
        }
        else if (nSize < 128)
        {
            nSize = 64;
        }
        else if (nSize < 256)
        {
            nSize = 128;
        }
        else if (nSize < 512)
        {
            nSize = 256;
        }
        else if (nSize < 1024)
        {
            nSize = 512;
        }
        else if (nSize < 2048)
        {
            nSize = 1024;
        }
        else
        {
            nSize = 1024;
        }
    }
	public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
	{
		if (null == texture) return;

		int maxSize = texture.width;
		if (texture.height > maxSize)
		{
			maxSize = texture.height;
		}

		string assetPath = AssetDatabase.GetAssetPath(texture);
		TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
		if (importer != null)
		{
			//importer.textureType = TextureImporterType.Advanced;
			//importer.isReadable = isReadable;
            if (importer.textureType == TextureImporterType.Sprite)
                return;

			importer.textureType = TextureImporterType.Sprite;
			importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;

			if (maxSize < 32)
			{
				importer.maxTextureSize = 32;
			}
			else if (maxSize < 64)
			{
				importer.maxTextureSize = 64;
			}
			else if (maxSize < 128)
			{
				importer.maxTextureSize = 128;
			}
			else if (maxSize < 256)
			{
				importer.maxTextureSize = 256;
			}
			else if (maxSize < 512)
			{
				importer.maxTextureSize = 512;
			}
			else if (maxSize < 1024)
			{
				importer.maxTextureSize = 1024;
			}
			else if (maxSize < 2048)
			{
				importer.maxTextureSize = 2048;
			}
			else
			{
				importer.maxTextureSize = 2048;
			}

			AssetDatabase.ImportAsset(assetPath);
			AssetDatabase.Refresh();
		}
	}

    void SetAudioImporterFormat ( AudioClip audio )
    {
        if ( audio == null )
        {
            return; 
        }

        string assetPath = AssetDatabase.GetAssetPath ( audio );
        AudioImporter importer = AudioImporter.GetAtPath ( assetPath ) as AudioImporter;
        if ( importer != null )
        {
            importer.threeD = false;
            importer.loadType = AudioImporterLoadType.StreamFromDisc;

            AssetDatabase.ImportAsset ( assetPath );
            AssetDatabase.Refresh ();
        }
    }

    void OnResetMissingFont()
    {
        string[] allPrefabList = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Resources/UI/Prefabs" });
        //批量强制设置为Font2的字体。个别需要Font字体的控件 手动重新指定一下 [10/21/2015 Zmy]
        Font font = AssetDatabase.LoadAssetAtPath("Assets/Art/UI/Fonts/Font2.ttf", typeof(Font)) as Font;
        for (int i = 0; i < allPrefabList.Length; ++i)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(allPrefabList[i]);
            GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;

            GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;

            foreach (Transform t in instObj.GetComponentsInChildren<Transform>(true))
            {
                Text _txt = t.GetComponent<Text>();
                if (_txt)
                {
                    if (_txt.font == null)
                    {
                        _txt.font = font;

                        PrefabUtility.ReplacePrefab(instObj,obj);
                        Debug.Log(obj.gameObject.name + "/" + _txt.name);
                    }
                }
            }
            Object.DestroyImmediate(instObj);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void OnAddButtonAnimation()
    {
        string[] allPrefabList = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Resources/UI/Prefabs" });
        for (int i = 0; i < allPrefabList.Length; ++i)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(allPrefabList[i]);
            GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;
            GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;

            foreach (Transform t in instObj.GetComponentsInChildren<Transform>(true))
            {
                Text _text = t.GetComponent<Text>();
                if (_text != null)
                {
                    Outline _outline = _text.GetComponent<Outline>();
                    if (_outline != null)
                    {
                        DestroyImmediate(_outline);
                        Shadow shadow = _text.GetComponent<Shadow>();
                        if (shadow != null)
                            continue;
                        Shadow _Shadow = _text.gameObject.AddComponent<Shadow>();
                        _Shadow.effectDistance = new UnityEngine.Vector2(3, -3);
                    }
                    OutLineGlow _OutLineGlow = _text.GetComponent<OutLineGlow>();
                    if (_OutLineGlow != null)
                    {
                        DestroyImmediate(_OutLineGlow);
                        Shadow shadow = _text.GetComponent<Shadow>();
                        if (shadow != null)
                            continue;
                        Shadow _Shadow = _text.gameObject.AddComponent<Shadow>();
                        _Shadow.effectDistance = new UnityEngine.Vector2(3, -3);
                    }
                }
            }
            PrefabUtility.ReplacePrefab(instObj, obj);
            Object.DestroyImmediate(instObj);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    void OnAddButtonClickAnimation()
    {
        string[] allPrefabList = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/Resources/UI/Prefabs" });
        RuntimeAnimatorController _Controller = AssetDatabase.LoadAssetAtPath("Assets/Art/UI/Animations/ui_bt_onclick.controller", typeof(UnityEngine.RuntimeAnimatorController)) as RuntimeAnimatorController;
        for (int i = 0; i < allPrefabList.Length; ++i)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(allPrefabList[i]);
            GameObject obj = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;

            GameObject instObj = PrefabUtility.InstantiatePrefab(obj) as GameObject;

            for (int q = 0; q < instObj.GetComponentsInChildren<Transform>().Length; q++)
            {
                if (instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponent<Button>() != null
                    && instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponent<ButtonClickEffect>()==null)
                {  
                  ButtonClickEffect _click=  instObj.GetComponentsInChildren<Transform>()[q].gameObject.AddComponent<ButtonClickEffect>();
                  if (instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponent<Animator>() == null)
                  {
                      Animator _annimator = instObj.GetComponentsInChildren<Transform>()[q].gameObject.AddComponent<Animator>();
                      _annimator.runtimeAnimatorController = _Controller;
                  }
                  //_click.SetAnimator();
                }
                else if (instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponent<ButtonClickEffect>() != null)
                {
                    for (int n = 0; n < instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponents<ButtonClickEffect>().Length; n++)
                    {
                        DestroyImmediate(instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponents<ButtonClickEffect>()[n]);
                    }
                    ButtonClickEffect _click=  instObj.GetComponentsInChildren<Transform>()[q].gameObject.AddComponent<ButtonClickEffect>();
                    if (instObj.GetComponentsInChildren<Transform>()[q].gameObject.GetComponent<Animator>() == null)
                    {
                        Animator _annimator = instObj.GetComponentsInChildren<Transform>()[q].gameObject.AddComponent<Animator>();
                        _annimator.runtimeAnimatorController = _Controller;
                    }
                    //_click.SetAnimator();
                }
            }
            PrefabUtility.ReplacePrefab(instObj, obj);
            Object.DestroyImmediate(instObj);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
}
