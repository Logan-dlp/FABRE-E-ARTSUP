#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FABRE.Painting.Tools
{
    public class PaintingInstance
    {
        public static void Create(PaintingList list, string path, string name, Sprite sprite, string description, List<Vector2> keyPointsList)
        {
            PaintingItem asset = ScriptableObject.CreateInstance<PaintingItem>();
            asset.PaintingName = name;
            asset.PaintingSprite = sprite;
            asset.PaintingDescription = description;
            asset.PaintingKeyPointsList = keyPointsList;
            
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/{name}.asset");
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log($"{name} as been created !\n{assetPath}");
            
            list.AddPaintingItem(asset);
        }
        
        public static void Delete(PaintingList list, PaintingItem asset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Asset does not exist !");
            }
            
            if (EditorUtility.DisplayDialog(
                    "Delete asset",
                    $"Are you sure you want to delete this asset : {assetPath} ?",
                    "Yes",
                    "No"))
            {
                if (AssetDatabase.DeleteAsset(assetPath))
                {
                    Debug.Log($"Asset as been deleted !\n{assetPath}");
                }
                else
                {
                    Debug.LogError($"Unable to delete asset !\n{assetPath}");
                }
                
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log("Deletion canceled !");
            }
            
            list.RemovePaintingItem(asset);
        }
    }
}

#endif