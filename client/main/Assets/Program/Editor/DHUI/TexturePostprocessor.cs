using UnityEngine;
using System.Collections;
using UnityEditor;

public class TexturePostprocessor : AssetPostprocessor
{

    void OnPostprocessTexture(Texture2D texture)
    {
        if(assetPath.Contains("UI_Public_New") || assetPath.Contains("Sprites") || assetPath.Contains("UI_Temp"))
        {
            Texture2D tex = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D)) as Texture2D;
            // 重新导入这个贴图才能读取到
            if (tex == null)
            {
                AssetDatabase.ImportAsset(assetPath);
                AssetDatabase.Refresh();
                return;
            }

            TextureImporter importer = assetImporter as TextureImporter;
            if (importer != null)
            {
                Debug.Log(importer.assetPath);

                int maxSize = tex.width;
                if (texture.height > maxSize)
                {
                    maxSize = tex.height;
                }

                if (importer.textureType == TextureImporterType.Sprite)
                    return;

                importer.textureType = TextureImporterType.Sprite;
                importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;

                if (maxSize <= 32)
                {
                    importer.maxTextureSize = 32;
                }
                else if (maxSize <= 64)
                {
                    importer.maxTextureSize = 64;
                }
                else if (maxSize <= 128)
                {
                    importer.maxTextureSize = 128;
                }
                else if (maxSize <= 256)
                {
                    importer.maxTextureSize = 256;
                }
                else if (maxSize <= 512)
                {
                    importer.maxTextureSize = 512;
                }
                else if (maxSize <= 1024)
                {
                    importer.maxTextureSize = 1024;
                }
                else if (maxSize <= 2048)
                {
                    importer.maxTextureSize = 1024;
                }
                else
                {
                    importer.maxTextureSize = 1024;
                }

                AssetDatabase.ImportAsset(assetPath);
                AssetDatabase.Refresh();
            }
        }        
    }
}
