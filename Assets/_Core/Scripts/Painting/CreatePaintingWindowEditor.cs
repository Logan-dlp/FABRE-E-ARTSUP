using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace FABRE.Painting.Tools
{
    public class CreatePaintingWindowEditor : EditorWindow
    {
        private PaintingList _paintingList;
        private Sprite _currentSpritePainting;
        private string _currentNamePainting;
        private string _currentDescriptionPainting;
        private bool isButtonDisabled;
    
        private const string _itemPath = "Assets/_Core/Painting/ScriptableObject"; 
        
        [MenuItem("Tools/Create Painting")]
        public static void ShowWindow()
        {
            GetWindow<CreatePaintingWindowEditor>("Create Painting");
        }
    
        private void OnGUI()
        {
            _paintingList = (PaintingList)EditorGUILayout.ObjectField("Painting List :", _paintingList, 
                                                            typeof(PaintingList), 
                                                            false) as PaintingList;
            
            _currentNamePainting = EditorGUILayout.TextField("Name : ", _currentNamePainting);
            
            EditorGUILayout.BeginHorizontal();
            _currentSpritePainting = (Sprite)EditorGUILayout.ObjectField("Sprite :", _currentSpritePainting, 
                                                                    typeof(Sprite), 
                                                                    false) as Sprite;
            EditorGUILayout.EndHorizontal();
            
            _currentDescriptionPainting = EditorGUILayout.TextField("Description : ", _currentDescriptionPainting, GUILayout.Height(50));
    
            
            GUI.enabled = !string.IsNullOrEmpty(_currentNamePainting) && _currentSpritePainting != null && _paintingList != null && !string.IsNullOrEmpty(_currentDescriptionPainting);
            if (GUILayout.Button("Create Paiting !"))
            {
                CreateCustomPainting();
            }
            GUI.enabled = true;
    
            if (_paintingList != null && _paintingList.paintingItemList.Count > 0)
            {
                DrawPaintingList();
            }
        }
    
        private void CreateCustomPainting()
        {
            PaintingItem asset = ScriptableObject.CreateInstance<PaintingItem>();
            asset.PaintingName = _currentNamePainting;
            asset.PaintingSprite = _currentSpritePainting;
            asset.PaintingDescription = _currentDescriptionPainting;
            
            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{_itemPath}/{_currentNamePainting}.asset");
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log($"{_currentNamePainting} as been created !\n{assetPath}");
            
            _paintingList.AddPaintingItem(asset);
        }
    
        private void DeleteCustomPainting(PaintingItem asset)
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
            
            _paintingList.RemovePaintingItem(asset);
        }
        
        private void DrawPaintingList()
        {
            float windowWidth = position.width;
            float itemWidth = 80f;
            float spaceBetweenItems = 10f;
    
            int itemsPerRow = Mathf.FloorToInt((windowWidth - spaceBetweenItems) / (itemWidth + spaceBetweenItems));
    
            float defaultHeight = itemWidth + 30f;
            int totalRows = Mathf.CeilToInt(_paintingList.paintingItemList.Count / (float)itemsPerRow);
            float calculatedHeight = totalRows * (itemWidth + spaceBetweenItems) + 10f;
            float boxHeight = Mathf.Max(defaultHeight, calculatedHeight);
    
            float boxPositionY = 200f;
    
            Rect boxRect = new Rect(10, boxPositionY, windowWidth - 20, boxHeight);
            GUI.Box(boxRect, string.Empty);
            DrawPaintingGrid(boxRect, itemWidth, itemsPerRow);
        }
    
        private void DrawPaintingGrid(Rect boxRect, float itemWidth, int itemsPerRow)
        {
            GUILayout.BeginArea(boxRect);
    
            int currentItem = 0;
            List<PaintingItem> paintingToRemoveList = new List<PaintingItem>();
    
            for (int i = 0; i < _paintingList.paintingItemList.Count; i++)
            {
                if (currentItem == 0)
                {
                    GUILayout.BeginHorizontal();
                }
    
                PaintingItem prefab = _paintingList.paintingItemList[i];
                Texture2D prefabTexture = AssetPreview.GetAssetPreview(prefab.PaintingSprite);
    
                using (new GUILayout.VerticalScope(GUILayout.Width(itemWidth)))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button(prefabTexture, GUILayout.Width(itemWidth), GUILayout.Height(itemWidth)))
                        {
                            EditorGUIUtility.PingObject(prefab);
                        }
    
                        if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
                        {
                            paintingToRemoveList.Add(prefab);
                        }
                    }
                }
    
                currentItem++;
    
                if (currentItem >= itemsPerRow)
                {
                    GUILayout.EndHorizontal();
                    currentItem = 0;
                }
            }
    
            if (currentItem > 0)
            {
                GUILayout.EndHorizontal();
            }
    
            GUILayout.EndArea();
    
            foreach (PaintingItem prefab in paintingToRemoveList)
            {
                DeleteCustomPainting(prefab);
            }
    
            Repaint();
        }
    }
}
#endif