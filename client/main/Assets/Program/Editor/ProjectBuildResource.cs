using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

class ProjectBuildResource : Editor{
	
	//得到项目的名称
	public static string projectName
	{
		get
		{ 
			//在这里分析shell传入的参数， 还记得上面我们说的哪个 project-$1 这个参数吗？
			//这里遍历所有参数，找到 project开头的参数， 然后把-符号 后面的字符串返回，
			foreach(string arg in System.Environment.GetCommandLineArgs()) {
				if(arg.StartsWith("project"))
				{
					return arg.Split("-"[0])[1];
				}
			}
			return "test";
		}
	}


	static void ExecuteCreateResource()
	{
        //删除所有动作组件
        VisualAutuPack.DelAnimationComponent();
        //一键所有prefab散包
        VisualAutuPack.Execute(BuildTarget.iPhone);
        //一键所有动作
        VisualAutuPack.ExecuteAnim(BuildTarget.iPhone);
        //资源加密
        CreateAssetBundle.ExecuteEncryption(BuildTarget.iPhone);
        //MD5 生成OK
        CreateMD5List.ExecuteMd5(BuildTarget.iPhone, projectName);
        EditorUtility.DisplayDialog("", "MD5 生成OK", "OK");
	}

}
