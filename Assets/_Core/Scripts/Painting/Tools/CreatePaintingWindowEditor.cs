#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FABRE.Painting.Tools
{
    public class CreatePaintingWindowEditor : EditorWindow
    {
        private PaintingList _paintingList;
        private Sprite _currentSpritePainting;
        private List<Vector2> _currentKeyPointsList;
        
        private Vector2 _newKeyPoint;
        
        private Vector2 _scrollPositionPaintingList;
        private Vector2 _scrollPositionKeyPointList;
        
        private string _currentNamePainting;
        private string _currentDescriptionPainting;
        
        private int _selectedKeyPointIndex = -1;
        
        private bool isButtonDisabled;
    
        private const string _itemPath = "Assets/_Core/Painting/ScriptableObject";
        
        [MenuItem("Tools/Create Painting")]
        public static void ShowWindow()
        {
            GetWindow<CreatePaintingWindowEditor>("Create Painting");
        }

        private void PaintingBasicInfoLayout()
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
        }

        private void KeyPointLayout()
        {
            EditorGUILayout.BeginHorizontal();
            _newKeyPoint = EditorGUILayout.Vector2Field("New KeyPoint :", _newKeyPoint);
            if (GUILayout.Button("+"))
            {
                _currentKeyPointsList.Add(_newKeyPoint);
                _newKeyPoint = Vector2.zero;
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            _scrollPositionKeyPointList = EditorGUILayout.BeginScrollView(_scrollPositionKeyPointList, GUILayout.Height(_currentKeyPointsList.Count * 50));
            for (int i = 0; i < _currentKeyPointsList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
            
                _currentKeyPointsList[i] = EditorGUILayout.Vector3Field($"Élément {i + 1}", _currentKeyPointsList[i]);
            
                if (GUILayout.Toggle(_selectedKeyPointIndex == i, "Sélectionner", "Button", GUILayout.Width(100)))
                {
                    _selectedKeyPointIndex = i;
                }
            
                if (GUILayout.Button("X", GUILayout.Width(30)))
                {
                    _currentKeyPointsList.RemoveAt(i);
                    if (_selectedKeyPointIndex == i)
                    {
                        _selectedKeyPointIndex = -1;
                    }
                    else if (_selectedKeyPointIndex > i)
                    {
                        _selectedKeyPointIndex--;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
        
            if (_selectedKeyPointIndex != -1 && _selectedKeyPointIndex < _currentKeyPointsList.Count)
            {
                GUILayout.Label($"Élément sélectionné : {_currentKeyPointsList[_selectedKeyPointIndex]}");
            }
            else
            {
                GUILayout.Label("Aucun élément sélectionné");
            }
        }

        private void CreatePaintingLayout()
        {
            GUI.enabled = !string.IsNullOrEmpty(_currentNamePainting)
                          && _currentSpritePainting != null 
                          && _paintingList != null 
                          && !string.IsNullOrEmpty(_currentDescriptionPainting);
            if (GUILayout.Button("Create Paiting !"))
            {
                PaintingInstance.Create(_paintingList, _itemPath,  _currentNamePainting, _currentSpritePainting, _currentDescriptionPainting);
                
                _currentNamePainting = string.Empty;
                _currentSpritePainting = null;
                _currentDescriptionPainting = string.Empty;
            }
            GUI.enabled = true;
        }
    
        private void OnGUI()
        {
            PaintingBasicInfoLayout();
    
            KeyPointLayout();
            
            CreatePaintingLayout();
    
            if (_paintingList != null && _paintingList.paintingItemList.Count > 0)
            {
                DrawPaintingList();
            }
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
    
            float boxPositionY = 500;
    
            Rect boxRect = new Rect(10, boxPositionY, windowWidth - 20, boxHeight);
            DrawPaintingGrid(boxRect, itemWidth, itemsPerRow);
        }
    
        private void DrawPaintingGrid(Rect boxRect, float itemWidth, int itemsPerRow)
        {
            GUILayout.BeginArea(boxRect);
    
            int currentItem = 0;
            List<PaintingItem> paintingToRemoveList = new List<PaintingItem>();
    
            _scrollPositionPaintingList = EditorGUILayout.BeginScrollView(_scrollPositionPaintingList, GUILayout.Height(200));
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
            EditorGUILayout.EndScrollView();
    
            if (currentItem > 0)
            {
                GUILayout.EndHorizontal();
            }
    
            GUILayout.EndArea();
    
            foreach (PaintingItem prefab in paintingToRemoveList)
            {
                PaintingInstance.Delete(_paintingList, prefab);
            }
    
            Repaint();
        }
    }
}

#endif