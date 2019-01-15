打包编辑器主要分三大功能。针对不同平台的
Andriod，IOS，Web 

我们平时主要在Web平台打包即可。

所要打包的资源需要先制作成prefab，并且上面不能挂接脚本。然后选中所要打包成bundle的prefab，选择unity3D编辑器上方的【Custom Editor】下的【Create AssetBunldes All : Web】选项即可，此时会弹出 Web Save Bundle的保存文件窗口，对我们所要打包的bundle修改起文件名，然后放入对应的文件夹目录即可。

下面是我们AssetBundle的命名规则，请大家都遵循此命名规则进行资源打包，否则程序无法正常加载到资源

命名规则的模板是Scene_Type
1.Scene命名规范如下：
我们现在主要有如下几个场景规划：
Fight：战斗场景
Home：主场景
Init：初始化场景
Loading：加载资源过场动画场景
Login：登陆场景
Logo： 
World：世界地图场景

2.Type命名规范如下：
我们的资源主要有如下几个分类：
 Animation,  // 动作
 Camera,     // 摄像机
 Effect,     // 特效
 Light,      // 灯光
 Terrin,     // 地形
 Building,   // 建筑 即模型
 Texture,    // 纹理
 Grass,      // 草

以上是我们可以制作成bundle的资源名称和类型，上述都是不可绑定脚本的。当对一个资源进行打包的时候，首先要明确知道这些资源是用于哪个场景下的，例如我们新做了N个特效，这些特效一部分是用于战斗场景中的，需要先把每个特效独立放在一个prefab里。然后对用在战斗场景下的prefab进行CreateAssetBundle，命名的前缀就必是"Fight_"；最后完整的名称就是Fight_Effect.assetbundle，那么这些特效就会打包成一个assetbundle保存到对应的路径下。
本地的话一般都存储在工程目录的StreamingAsset/Web目录下。

如果这个特效可能在其他场景也会用到，那么它就是一个公用的特效。这些公用的资源的话 前缀统一使用Public_Effect;

对怪物，BOSS，玩家的模型资源。统一使用成
Boss.assetbundle
Player.assetbundle
Monster.assetbundle
命名。 当更新其中一个模型的时候，需要选中其他所有的模型重新打成唯一包

