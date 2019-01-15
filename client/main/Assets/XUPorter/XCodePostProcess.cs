using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.Xml;
#endif
using System.IO;

public static class XCodePostProcess
{
#if UNITY_EDITOR
    [PostProcessBuild(100)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iPhone)
        {
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        //得到xcode工程的路径
        string path = Path.GetFullPath(pathToBuiltProject);

        // Create a new project object from build target
        XCProject project = new XCProject(pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        //在这里面把frameworks添加在你的xcode工程里面
        string[] files = Directory.GetFiles(Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            project.ApplyMod(file);
        }

        //增加一个编译标记。。没有的话sharesdk会报错。。
        project.AddOtherLinkerFlags("-licucore");

        //设置签名的证书， 第二个参数 你可以设置成你的证书
        project.overwriteBuildSetting("CODE_SIGN_IDENTITY", "iPhone Distribution: Beijing Dream Faction Network Technology Co., Ltd. (TF95Q2D6XF)", "Release");
        //project.overwriteBuildSetting ("CODE_SIGN_IDENTITY", "iPhone Distribution: Beijing Dream Faction Network Technology Co., Ltd. (TF95Q2D6XF)", "Debug");
        project.overwriteBuildSetting("ENABLE_BITCODE", "NO", "Release");
        project.overwriteBuildSetting("ENABLE_BITCODE", "NO", "Debug");

        //project.overwriteBuildSetting ("CODE_SIGN_RESOURCE_RULES_PATH", "$(SDKROOT)/ResourceRules.plist", "Debug");
        //project.overwriteBuildSetting ("CODE_SIGN_RESOURCE_RULES_PATH", "$(SDKROOT)/ResourceRules.plist", "Release");       


        // 编辑plist 文件
        EditorPlist(path);

        //编辑代码文件
        EditorCode(path);

        // Finally save the xcode project
        project.Save();


        //上面的介绍 我已经在上一篇文章里面讲过， 这里就不赘述 。。
        //那么当我们打完包以后 如果需要根据不同平台 替换 不同的framework plist oc 包名 等等。。。

        //这里输出的projectName 就是 91  
        Debug.Log(projectName);

        if (projectName == "91")
        {
            //当我们在打91包的时候 这里面做一些 操作。

        }


    }

    //参数传进来做处理
    public static string projectName
    {
        get
        {
            foreach (string arg in System.Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("project"))
                {
                    return arg.Split("-"[0])[1];
                }
            }
            return "test";
        }
    }

    private static void EditorPlist(string filePath)
    {

        XCPlist list = new XCPlist(filePath);
        string bundle = "com.DreamFactionGame.DreamHeros";

        string PlistAdd = @"            
            <key>NSAppTransportSecurity</key>
            <dict>
            <key>NSAllowsArbitraryLoads</key>
            <true/>
            </dict>";

        //在plist里面增加一行
        list.AddKey(PlistAdd);
        //在plist里面替换一行
        //list.ReplaceKey("<string>com.koramgame.${PRODUCT_NAME}</string>","<string>"+bundle+"</string>");
        //保存
        list.Save();

    }

    private static void EditorCode(string filePath)
    {
        //读取UnityAppController.mm文件
        //XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        //在指定代码后面增加一行代码
        //UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"","#import <ShareSDK/ShareSDK.h>");

        //在指定代码中替换一行
        //UnityAppController.Replace("return YES;","return [ShareSDK handleOpenURL:url sourceApplication:sourceApplication annotation:annotation wxDelegate:nil];");

        //在指定代码后面增加一行
        //UnityAppController.WriteBelow("UnityCleanup();\n}","- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url\r{\r    return [ShareSDK handleOpenURL:url wxDelegate:nil];\r}");


    }

#endif
}
