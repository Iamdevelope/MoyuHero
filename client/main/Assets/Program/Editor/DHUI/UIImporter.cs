using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

public class UIImporter : EditorWindow
{
    public GameObject root;
    public List<GameObject> parentList = new List<GameObject> ();
    public int resolutionX = 2208;
    public int resolutionY = 1242;

    static UIImporter win = null;
    [MenuItem ( "GameTools/Psd 2 UGUI" )]
    static public void ImportUISceneMenuItem ()
    {
        win = ( UIImporter ) EditorWindow.GetWindow ( typeof ( UIImporter ), true, "UIImporter" );
    }

    void OnGUI ()
    {
        root = Selection.activeGameObject;
        EditorGUILayout.BeginVertical ();
        EditorGUILayout.ObjectField ( "请选择根节点", root, typeof ( GameObject ), GUILayout.Height ( 35 ) );

        //if(root != null)
        //{
        //    EditorGUILayout.Vector3Field("position:", root.GetComponent<RectTransform>().position);
        //    EditorGUILayout.Vector3Field("position:", root.GetComponent<RectTransform>().localPosition);
        //    EditorGUILayout.Vector3Field("position:", root.GetComponent<RectTransform>().anchoredPosition3D);
        //    EditorGUILayout.Vector2Field("position:", root.GetComponent<RectTransform>().anchoredPosition);
        //    EditorGUILayout.Vector2Field("anchorMin:", root.GetComponent<RectTransform>().anchorMin);
        //    EditorGUILayout.Vector2Field("anchorMax:", root.GetComponent<RectTransform>().anchorMax);
        //    EditorGUILayout.Vector2Field("pivot:", root.GetComponent<RectTransform>().pivot);
        //}

        if ( GUILayout.Button ( "Importer UIScene with xml", GUILayout.Height ( 35 ) ) )
        {
            if ( root == null )
            {
                if ( EditorUtility.DisplayDialog ( "请选择根节点", "必须选择一个根节点", "确定" ) )
                    ;
                {
                    return;
                }
            }

            //root.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            //root.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            parentList.Add ( root );
            ImporterUIScene ();
        }

        if ( GUILayout.Button ( "Update UIScene with xml", GUILayout.Height ( 35 ) ) )
        {
            if ( root == null )
            {
                Debug.LogError ( "root is null" );
                if ( EditorUtility.DisplayDialog ( "请选择根节点", "必须选择一个根节点", "确定" ) )
                    ;
                {
                    return;
                }
            }

            //root.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            parentList.Add ( root );
            ImporterUIScene ();
        }

        EditorGUILayout.EndVertical ();
    }

    void ImporterUIScene ()
    {
        // 选择 psd 导出的文件信息
        string inputFile = EditorUtility.OpenFilePanel ( "Choose photoshop file info  to Import", Application.dataPath + "/Art/UI_Psd_Xml/", "xml" );
        if ( ( inputFile != null ) && ( inputFile != "" ) && ( inputFile.StartsWith ( Application.dataPath ) ) )
        {
            XmlDocument xml = LoadXml ( inputFile );
            XmlNode scene = xml.SelectSingleNode ( "UIScene" );
            XmlElement resolution = scene [ "Resolution" ];
            resolutionX = Int32.Parse ( resolution.GetAttribute ( "width" ) );
            resolutionY = Int32.Parse ( resolution.GetAttribute ( "height" ) );

            XmlNodeList xmlNodeList = xml.SelectSingleNode ( "UIScene" ).ChildNodes;
            foreach ( XmlElement element in xmlNodeList )
            {
                LoadXmlNode ( element );
            }
        }
    }

    // 加载 psd 文件信息的 xml
    XmlDocument LoadXml ( string filePath )
    {
        Debug.Log ( "LoadXml path:" + filePath );

        XmlDocument xml = new XmlDocument ();
        XmlReaderSettings set = new XmlReaderSettings ();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        xml.Load ( XmlReader.Create ( filePath, set ) );
        return xml;
    }

    void LoadXmlNode ( XmlElement element )
    {
        if ( element.GetAttribute ( "type" ) == "image" )
        {
            GenerateImage ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "text" )
        {
            GenerateText ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "btn" || element.GetAttribute ( "type" ) == "button" )
        {
            GenerateButton ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "rect" )
        {
            GenerateRectPanel ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "scroll" )
        {
            GenerateRectPanel ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "grid" )
        {
            GenerateRectPanel ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "loop" )
        {
            GenerateRectPanel ( element );
        }
        else if ( element.GetAttribute ( "type" ) == "slider" )
        {
            GenerateRectPanel ( element );
        }
        else
        {
            Debug.Log ( element.GetAttribute ( "type" ) + "TODO..." );
        }
    }

    /*
    <Layer type="text" content="服务器" colorr="255" colorg="255" colorb="255" 
    font="STYuanti-SC-Regular" fontsize="40" code="server" opacity="255" 
    bounds1="1041" bounds2="863" bounds3="1168" bounds4="902" />
    */
    void GenerateText ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Text" ) ) as GameObject;
        }
        Text text = obj.GetComponent<Text> ();
        // content
        text.text = element.GetAttribute ( "content" );
        // font todo...

        string [] fontasset = AssetDatabase.FindAssets ( "t:font" );
        List<string> fontPath = new List<string> ();
        foreach( var item in fontasset )
        {
            fontPath.Add ( AssetDatabase.GUIDToAssetPath ( item ) );
        }
        
        // font
        string font = element.GetAttribute ( "font" );
        if ( font == "AdobeHeitiStd-Regular" || font == "HYe3gj")
        {
            text.font = ( Font ) AssetDatabase.LoadAssetAtPath ( fontPath [ 0], typeof ( Font ) );
        }
        else
        {
            text.font = ( Font ) AssetDatabase.LoadAssetAtPath ( fontPath [ 1 ], typeof ( Font ) );
        }


        // font size
        //text.fontSize = ( int ) ( float.Parse ( element.GetAttribute ( "fontsize" ) )  * rate);

        text.fontSize = ( int ) ( float.Parse ( element.GetAttribute ( "fonttext" ) ));

        // color todo...
        text.color = new Color ( float.Parse ( element.GetAttribute ( "colorr" ) ) / 255.0f,
            float.Parse ( element.GetAttribute ( "colorg" ) ) / 255.0f,
            float.Parse ( element.GetAttribute ( "colorb" ) ) / 255.0f,
            float.Parse ( element.GetAttribute ( "opacity" ) ) / 255.0f );

        GenerateRectTransform ( element, obj );
    }

    /*
    <Layer type="image" code="topbg" res="Ui_ziseanniu"  opacity="255" 
    bounds1="933" bounds2="836" bounds3="1277" bounds4="945" 
    s9="1"/>
    */
    // image 图片 
    void GenerateImage ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Image" ) ) as GameObject;
        }

        Image image = obj.GetComponent<Image> ();

        // res name
        string [] asset = AssetDatabase.FindAssets ( element.GetAttribute ( "res" ) );
        string path = "";

        foreach ( var item in asset )
        {
            string str = AssetDatabase.GUIDToAssetPath ( item );
            string [] pathname = str.Split ( '/' );
            string name = pathname [ pathname.Length - 1 ].Substring ( 0, pathname [ pathname.Length - 1 ].Length - 4 );
            if ( element.GetAttribute ( "res" ) == name )
            {
                path = str;
                break;
            }
        }

        //string path = AssetDatabase.GUIDToAssetPath(asset[0]);
        image.sprite = ( Sprite ) AssetDatabase.LoadAssetAtPath ( path, typeof ( Sprite ) );
        Debug.Log ( path );
        image.SetNativeSize ();

        // 九宫格
        if ( element.HasAttribute ( "s9" ) )
        {
            if ( element.GetAttribute ( "s9" ) == "1" )
            {
                image.type = Image.Type.Sliced;
                image.preserveAspect = true;
            }
        }
        else
        {
            image.type = Image.Type.Simple;
        }

        // 不透明度
        if ( element.HasAttribute ( "opacity" ) )
        {
            int opacity = Int32.Parse ( element.GetAttribute ( "opacity" ) );
            //image.color = new Color ( image.color.r, image.color.g, image.color.b, opacity / 255 );
        }

        GenerateRectTransform ( element, obj );
    }

    // button  按钮
    void GenerateButton ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Button" ) ) as GameObject;
        }
        Image image = obj.GetComponent<Image> ();

        if ( element.HasAttribute ( "res" ) )
        {
            string [] asset = AssetDatabase.FindAssets ( element.GetAttribute ( "res" ) );
            string path = "";

            foreach ( var item in asset )
            {
                string str = AssetDatabase.GUIDToAssetPath ( item );
                string [] pathname = str.Split ( '/' );
                string name = pathname [ pathname.Length - 1 ].Substring ( 0, pathname [ pathname.Length - 1 ].Length - 4 );
                if ( element.GetAttribute ( "res" ) == name )
                {
                    path = str;
                    break;
                }
            }

            image.sprite = ( Sprite ) AssetDatabase.LoadAssetAtPath ( path, typeof ( Sprite ) );
            image.SetNativeSize ();

            // 九宫格
            if ( element.HasAttribute ( "s9" ) )
            {
                if ( element.GetAttribute ( "s9" ) == "1" )
                {
                    image.type = Image.Type.Sliced;
                    image.preserveAspect = true;
                }
            }
            else
            {
                image.type = Image.Type.Simple;
            }

            // 不透明度
            if ( element.HasAttribute ( "opacity" ) )
            {
                int opacity = Int32.Parse ( element.GetAttribute ( "opacity" ) );
                image.color = new Color ( image.color.r, image.color.g, image.color.b, opacity / 255 );
            }
        }
        else
        {
            image.enabled = false;
        }

        GenerateRectTransform ( element, obj );
    }

    // 面板
    void GenerateRectPanel ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/RectPanel" ) ) as GameObject;
        }
        GenerateRectTransform ( element, obj );
    }

    // scroll
    void GenerateScroll ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Scroll" ) ) as GameObject;
        }
        GenerateRectTransform ( element, obj );
    }

    // grid
    void GenerateGrid ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Grid" ) ) as GameObject;
        }
        GenerateRectTransform ( element, obj );
    }

    // loop
    void GenerateLoop ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Loop" ) ) as GameObject;
        }
        GenerateRectTransform ( element, obj );
    }

    // slider
    void GenerateSlider ( XmlElement element )
    {
        GameObject obj = null;
        try
        {
            obj = parentList [ parentList.Count - 1 ].transform.FindChild ( element.GetAttribute ( "code" ) ).gameObject;
        }
        catch
        {
            obj = Instantiate ( Resources.Load ( "Prefabs/Slider" ) ) as GameObject;
        }
        GenerateRectTransform ( element, obj );
    }

    // 生成 object 的 Rect 属性
    void GenerateRectTransform ( XmlElement element, GameObject obj )
    {
        // code
        obj.name = element.GetAttribute ( "code" );
        obj.transform.SetParent ( root.transform );
        obj.transform.SetSiblingIndex ( Int32.Parse ( element.GetAttribute ( "index" ) ) );
        obj.transform.localScale = new Vector3 ( 1, 1, 1 );

        // visible
        obj.SetActive ( bool.Parse ( element.GetAttribute ( "visible" ) ) );

        Vector2 anchormin = obj.transform.GetComponent<RectTransform> ().anchorMin;
        Vector2 anchormax = obj.transform.GetComponent<RectTransform> ().anchorMax;
        Vector2 pivot = obj.transform.GetComponent<RectTransform> ().pivot;

        //obj.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        //obj.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        //obj.transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

        //Debug.Log("code   " + element.GetAttribute("code") + "   " + obj.transform.GetComponent<RectTransform>().anchoredPosition.x + "   " + obj.transform.GetComponent<RectTransform>().anchoredPosition.y);

        // size 
        float width = float.Parse ( element.GetAttribute ( "width" ) );
        float height = float.Parse ( element.GetAttribute ( "height" ) );
        obj.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 ( width, height );

        float posx = float.Parse ( element.GetAttribute ( "bounds1" ) );
        float posy = -float.Parse ( element.GetAttribute ( "bounds2" ) );

        posx -= resolutionX * anchormin.x;
        posy += resolutionY * anchormin.y;

        posx += width * pivot.x;
        posy -= height * ( 1 - pivot.y );

        // 这里有点硬编码。在坐标转换中需要上下颠倒一下
        if ( anchormin.y == 1.0f )
        {
            posy -= resolutionY;
        }
        if ( anchormin.y == 0.0f )
        {
            posy += resolutionY;
        }
        // pos
        // posx = psx + image.width/2 - screen.width/2
        //float posx = float.Parse(element.GetAttribute("bounds1")) + width / 2 - resolutionX / 2;
        // posy = -(psy+image.height/2-screen.height/2)
        // float posy = -(float.Parse(element.GetAttribute("bounds2")) + height / 2 - resolutionY / 2);
        //Debug.Log("code   " + element.GetAttribute("code") + "   " + posx + "   " + posy);

        if ( element.GetAttribute ( "type" ) == "image" || element.GetAttribute ( "type" ) == "btn" )
        {
            if ( !element.HasAttribute ( "s9" ) )
                obj.transform.GetComponent<Image> ().SetNativeSize ();
        }

        obj.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ( posx, posy );
        //Debug.Log("anchor pos" + obj.transform.GetComponent<RectTransform>().anchoredPosition); 
        obj.transform.SetParent ( parentList [ parentList.Count - 1 ].transform);
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0f);
        #region anchor and pivot

        // anchor x
        //if(anchormin.x == anchormax.x && anchormin.x == 0.0f)
        //{
        //    posx += parentList[parentList.Count - 1].transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        //}
        //else if(anchormin.x == anchormax.x && anchormin.x == 1.0f)
        //{
        //    posx -= parentList[parentList.Count - 1].transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        //}
        //else if (anchormin.x == anchormax.x && anchormin.x == 0.5f)
        //{

        //}
        //else
        //{
        //    if (EditorUtility.DisplayDialog("锚点不支持", "这个对象" + GetParentList(obj.transform) + "的锚点设置不支持", "关闭"))
        //    {
        //        this.Close();
        //    }
        //    else
        //    {
        //        this.Close();
        //    }
        //}

        //// anchor y
        //if (anchormin.y == anchormax.y && anchormin.y == 0.0f)
        //{
        //    posy += parentList[parentList.Count - 1].transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        //}
        //else if (anchormin.y == anchormax.y && anchormin.y == 1.0f)
        //{
        //    posy -= parentList[parentList.Count - 1].transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        //}
        //else if(anchormin.y == anchormax.y && anchormin.y == 0.5f)
        //{

        //}
        //else
        //{
        //    if (EditorUtility.DisplayDialog("锚点不支持", "这个对象" + GetParentList(obj.transform) + "的锚点设置不支持", "关闭"))
        //    {
        //        this.Close();
        //    }
        //    else
        //    {
        //        this.Close();
        //    }
        //}

        //// pivot x
        //if (obj.transform.GetComponent<RectTransform>().pivot.x == 0.0f)
        //{
        //    posx -= obj.transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        //}
        //else if (obj.transform.GetComponent<RectTransform>().pivot.x == 1.0f)
        //{
        //    posx += obj.transform.GetComponent<RectTransform>().sizeDelta.x / 2;
        //}

        //// pivot y
        //if (obj.transform.GetComponent<RectTransform>().pivot.y == 0.0f)
        //{
        //    posy -= obj.transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        //}
        //else if (obj.transform.GetComponent<RectTransform>().pivot.y == 1.0f)
        //{
        //    posy += obj.transform.GetComponent<RectTransform>().sizeDelta.y / 2;
        //}

        //obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, posy);
        #endregion

        // 子节点
        parentList.Add ( obj );
        foreach ( XmlElement item in element.ChildNodes )
        {
            LoadXmlNode ( item );
        }
        parentList.Remove ( obj );
    }

    public string GetParentList ( Transform obj )
    {
        string str = "";
        for ( int i = 0; i < parentList.Count; ++i )
        {
            str += parentList [ i ].name + "/";
        }

        str += obj.name;
        return str;
    }

    /*
    static private object DeserializeXml(string filePath, System.Type type)
    {
        object instance = null;
        StreamReader xmlFile = File.OpenText(filePath);
        if (xmlFile != null)
        {
            string xml = xmlFile.ReadToEnd();
            if ((xml != null) && (xml.ToString() != ""))
            {
                XmlSerializer xs = new XmlSerializer(type);
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] byteArray = encoding.GetBytes(xml);
                MemoryStream memoryStream = new MemoryStream(byteArray);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                if (xmlTextWriter != null)
                {
                    instance = xs.Deserialize(memoryStream);
                }
            }
        }
        xmlFile.Close();
        return instance;
    }
    
    private void ImportUIScene(string assetPath)
    {
        Debug.Log("ImportUIScene");
        // before we do anything else, try to deserialize the input file and be sure it's actually the right kind of file
        UIScene uiScene = (UIScene)DeserializeXml(assetPath, typeof(UIScene));
        if (uiScene == null)
        {
            Debug.Log("The file " + assetPath + " wasn't able to generate a UI scene.");
            return;
        }
        Debug.Log("ImportUIScene   uiscene images  " + uiScene.layers.Length.ToString());
        for (int i = 0; i < uiScene.layers.Length; ++i)
        {
            UIScene.Layer layer = uiScene.layers[i];
            Debug.Log("type :" + layer.type);
            Debug.Log("name :" + layer.name);
            Debug.Log("x :" + layer.x);
            Debug.Log("y :" + layer.y);
        }

        GenerateUI(uiScene);
    }

    private void GenerateUI(UIScene scene)
    {
        for (int i = 0; i < scene.layers.Length; ++i)
        {
            UIScene.Layer layer = scene.layers[i];
            // image
            if (layer.type == "Image")
            {
                GameObject obj = Instantiate(image) as GameObject;
                obj.name = layer.name;
                obj.transform.SetParent(root.transform);
                Image img = obj.GetComponent<Image>();
                //img.sprite = Resources.Load("Png" + layer.name) as Sprite;
                //img.sprite = Resources.Load<Sprite>("Png" + layer.name + ".png");

                img.sprite = Resources.Load<Sprite>("Png/" + layer.name);
                img.SetNativeSize();
                img.GetComponent<RectTransform>().anchoredPosition = new Vector2(layer.x, layer.y);
            }
        }
    }
    */
}
