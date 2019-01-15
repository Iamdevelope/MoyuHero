using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.UI;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class AutoBatchCode : EditorWindow
{
	bool _isActiveObj = false;    // 是否生成未激活的对象

	string _namespace;  // 需要引用的命令空间
	string _className;  // 生成的类名
	string _baseClass;  // 基类的名称
	string _fullCode;   // 完整的代码

	// 
	string _variable;  // 变量声明
	string _gainCode;     // 获取的代码
	string _function;    // 按钮回调代码
	string _tempParent; // 临时的父节点关系

	string _path = "";       // 生成代码的路径
	string _tempPath = "";   // 零时路径
	bool _isFirst = true;   // 开始标记位
	List<string> _widgetList = new List<string>();
	List<string> _objNameList = new List<string>();
    List<string> _objNameListStr = new List<string>();

	GameObject _target;  // 目标对象

	[MenuItem("GameTool/自动生成代码 %g")]
	static void ShowWindow()
	{
		AutoBatchCode win = EditorWindow.GetWindow<AutoBatchCode>(true);
		win.title = "AutoBatchCode";
		win.minSize = new Vector2(800, 480);
		win.Show();
	}

	void Init()
	{
		if (_isFirst)
		{
			_namespace += "using UnityEngine;\nusing UnityEngine.UI;\nusing DreamFaction.UI.Core;";

			_isFirst = false;

			_widgetList.Add("Text");
			_widgetList.Add("Button");
            _widgetList.Add("Toggle");
			//_widgetList.Add("RichText");
			//_widgetList.Add("Image");

            //_path = @"Assets\Program\Scripts\GameCore\UI\Guide\";
			_baseClass = "BaseUI";
		}
	}

	void OnGUI()
	{
		Init();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("生成的路径", GUILayout.Width(120));
		_path = EditorGUILayout.TextArea(_path);
        if (GUILayout.Button("浏览"))
        {
            _path = EditorUtility.OpenFolderPanel("选择存储路径：", @"Assets\Program\Scripts\GameCore\UI", "");
        }
		EditorGUILayout.EndHorizontal();

		// 选择的目标
		_target = Selection.activeGameObject;
		EditorGUILayout.ObjectField(_target, typeof(GameObject), true, GUILayout.Width(180), GUILayout.Height(20));

		// 命名空间
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("需要引用的命令空间", GUILayout.Width(120));
		_namespace = EditorGUILayout.TextField(_namespace, GUILayout.Height(40));
		EditorGUILayout.EndHorizontal();

		// 类名
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("类名", GUILayout.Width(120));
		_className = EditorGUILayout.TextField(_className, GUILayout.Height(40));
		EditorGUILayout.EndHorizontal();

		// 基类的名字
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("基类的名字", GUILayout.Width(120));
		_baseClass = EditorGUILayout.TextField(_baseClass, GUILayout.Height(40));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("是否生成未激活的对象", GUILayout.Width(120));
		_isActiveObj = EditorGUILayout.Toggle(_isActiveObj, GUILayout.Height(40));
		EditorGUILayout.EndHorizontal();

		// 生成代码
		if (GUILayout.Button("生成代码", GUILayout.Height(30)))
		{
			// 清理数据
			_fullCode = "";
			_variable = "";
			_function = "";
			_tempParent = "";
			_gainCode = "";

            _objNameList.Clear();
            _objNameListStr.Clear();
			HandleCode();

            AssetDatabase.Refresh();
		}

		if (GUILayout.Button("删除上次生成的文件", GUILayout.Height(30)))
		{
			// 指定的路径
			if (IOHelper.Exists(_tempPath))
			{
				IOHelper.DeleteFile(_tempPath);

				if (EditorUtility.DisplayDialog("删除上次生成的文件", "删除成功", "确定", "取消"))
				{
					
				}
			}
		}
	}

	string HandleCode()
	{
		//
		_fullCode += _namespace;
		_fullCode += "\n\n";

		// 类声明
		if (_baseClass != "")
			_fullCode += "public class " + _className + " : " + _baseClass + "\n";
		else
			_fullCode += "public class " + _className + "\n";
		_fullCode += "{\n";

		// 生成所有的代码
		FullCode(_target);

		// 所有变量声明
		_fullCode += _variable + "\n";

		// 初始化方法
		_fullCode += "	public override void InitUIData()\n";
		_fullCode += "	{\n";
		_fullCode += "		base.InitUIData();\n";
		_fullCode += _gainCode + "\n";
		_fullCode += "	}\n\n";

		_fullCode += "	public override void InitUIView()\n";
		_fullCode += "	{\n";
		_fullCode += "		base.InitUIView();\n";
		_fullCode += "	}\n\n";

		// 按钮回调函数
		_fullCode += _function;

        _fullCode += AddDestroy();

		_fullCode += "}\n";

		// 处理路径
		if (!_path.Contains(".cs"))
		{
            //加上文件名;
            _path += ("/" + _className);
			_path += ".cs";
		}

		// 指定的路径
		if (IOHelper.Exists(_path))
		{
			IOHelper.DeleteFile(_path);
		}
		_tempPath = _path;
		IOHelper.CreateFile(_path);
		IOHelper.Write(_path, _fullCode);

		return _fullCode;
	}

    string AddDestroy()
    {
        string str = "";
        str += "    public virtual void OnDestroy()\n";
        str += "    {\n";
        for (int i = 0; i < _objNameListStr.Count; ++i)
        {
            str += "        Destroy(m_" + _objNameListStr[i] + ");\n";
        }

        str += "    }\n";
        return str;
    }

	void FullCode(GameObject parent)
	{
		if (parent == null)
		{
			return;
		}

		for (int i = 0; i < parent.transform.childCount; i++)
		{
			Transform obj = parent.transform.GetChild(i);
			if (!_isActiveObj)
			{
				if (obj.gameObject.activeSelf == false)
					continue;
			}
			
			for (int j = 0; j < _widgetList.Count; ++j)
			{
				if (obj.GetComponent(_widgetList[j]))
				{
					int count = 0;
					for (int k = 0; k < _objNameList.Count; ++k)
					{
						if (obj.name == _objNameList[k])
						{
							count++;
						}
					}

					string objName = "";
					if (count == 0)
					{
						objName = obj.name;
					}
					else
					{
						objName = obj.name + "_" + count.ToString();
					}

					_variable += "	protected " + _widgetList[j] + " m_" + objName + ";\n";
					_gainCode += "		m_" + objName + " = " + "selfTransform.FindChild(\"" + _tempParent + obj.name + "\")." + "GetComponent<" + _widgetList[j] + ">();\n";

                    _objNameList.Add(obj.name);
                    _objNameListStr.Add(objName);

					if (_widgetList[j] == "Button")
					{
						_gainCode += "		m_" + objName + ".onClick.AddListener(OnClick" + objName + ");\n";

						_function += "	protected virtual void OnClick" + objName + "()\n";
						_function += "	{\n";
						_function += "	}\n\n";
					}

                    if (_widgetList[j] == "Toggle")
                    {
                        _gainCode += "		m_" + objName + ".onValueChanged.AddListener(OnToggle" + objName + ");\n";

                        _function += "	protected virtual void OnToggle" + objName + "(bool isSelected)\n";
                        _function += "	{\n";
                        _function += "	}\n\n";
                    }
				}
			}
			_tempParent += obj.name + "/";
			FullCode(obj.gameObject);
			string[] buffer = _tempParent.Split('/');
			_tempParent = "";
			for (int j = 0; j < buffer.Length - 2; j++)
			{
				_tempParent += buffer[j] + "/";
			}
		}
	}
}
