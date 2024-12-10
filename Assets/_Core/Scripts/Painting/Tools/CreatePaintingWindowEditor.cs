#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FABRE.Painting.Tools
{
    public class CreatePaintingWindowEditor : EditorWindow
    {
        private const string _itemPath = "Assets/_Core/Painting/ScriptableObject";
        
        private static List<Vector2> _currentKeyPointsList = new();
        private static int _selectedKeyPointIndex = -1;
        
        [MenuItem("Tools/Create Painting")]
        public static void ShowWindow()
        {
            GetWindow<CreatePaintingWindowEditor>("Create Painting");
        }
        
        private static void OnSceneGUI(SceneView sceneView)
        {
            if (_selectedKeyPointIndex > -1)
            {
                Color defaultHandlesColor = Handles.color;
                Handles.color = new Color(.1875f, .828125f, .5390625f);
                
                Handles.CubeHandleCap(0, (Vector3)_currentKeyPointsList[_selectedKeyPointIndex], Quaternion.Euler(0, 0, 45), .1f, EventType.Repaint);
                
                Handles.color = defaultHandlesColor;
                
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    BoxCollider[] colliderArray = FindObjectsByType<BoxCollider>(FindObjectsSortMode.None);
                    foreach (BoxCollider collider in colliderArray)
                    {
                        if (collider.TryGetComponent<SpriteRenderer>(out SpriteRenderer colliderSpriteRenderer))
                        {
                            collider.size = colliderSpriteRenderer.sprite.bounds.size;
                            break;
                        }
                    }

                    if (Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin, HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).direction, out RaycastHit hit, Mathf.Infinity) 
                        && hit.transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer hitSpriteRenderer))
                    {
                        _currentKeyPointsList[_selectedKeyPointIndex] = (Vector2)hit.point;
                    }
                    
                    Event.current.Use();
                }
            }
        }
        
        private PaintingList _paintingList;
        private Sprite _currentSpritePainting;
        
        private Vector2 _newKeyPoint;
        private Vector2 _scrollPositionPaintingList = Vector2.zero;
        private Vector2 _scrollPositionKeyPointList = Vector2.zero;
        
        private string _currentNamePainting;
        private string _currentDescriptionPainting;
        
        private bool isButtonDisabled;

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
        
        private void OnGUI()
        {
            PaintingBasicInfoLayout();
            KeyPointLayout();
            CreatePaintingLayout();
            
    
            // if (_paintingList != null && _paintingList.paintingItemList.Count > 0)
            // {
            //     DrawPaintingList();
            // }
            
            Repaint();
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
            EditorGUILayout.LabelField($"Key Point ({_currentKeyPointsList.Count}) :");
            
            if (_currentKeyPointsList.Count > 0)
            {
                if (_selectedKeyPointIndex > -1 && GUILayout.Button("Clear"))
                {
                    _currentKeyPointsList.RemoveAt(_selectedKeyPointIndex);
                    _selectedKeyPointIndex = -1;
                }
                
                if (_selectedKeyPointIndex <= -1 && GUILayout.Button("Clear all"))
                {
                    _currentKeyPointsList.Clear();
                }
            }
            
            if (GUILayout.Button("Add KeyPoint"))
            {
                _currentKeyPointsList.Add(_newKeyPoint);
                _newKeyPoint = Vector2.zero;
            }
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            
            int contentNumber = _currentKeyPointsList.Count <= 3 ? _currentKeyPointsList.Count : 3;
            _scrollPositionKeyPointList = EditorGUILayout.BeginScrollView(_scrollPositionKeyPointList, GUI.skin.box, GUILayout.Height(contentNumber * 45));
            
            for (int i = 0; i < _currentKeyPointsList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
            
                _currentKeyPointsList[i] = EditorGUILayout.Vector2Field($"Element {i}", _currentKeyPointsList[i]);

                Color originalBackground = GUI.backgroundColor;
                GUI.backgroundColor = new Color(.1875f, .828125f, .5390625f);
                
                if (_selectedKeyPointIndex == i && GUILayout.Button("Unselect", GUILayout.Width(70)))
                {
                    _selectedKeyPointIndex = -1;
                }
                
                GUI.backgroundColor = originalBackground;
                
                if (_selectedKeyPointIndex != i && GUILayout.Button("Select", GUILayout.Width(70)))
                {
                    _selectedKeyPointIndex = i;
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUILayout.Space();
        
            if (_selectedKeyPointIndex != -1 && _selectedKeyPointIndex < _currentKeyPointsList.Count)
            {
                GUILayout.Label($"Item selected : Element {_selectedKeyPointIndex}, {_currentKeyPointsList[_selectedKeyPointIndex]}.");
            }
            else
            {
                GUILayout.Label("Nothing selected.");
            }
        }

        private void CreatePaintingLayout()
        {
            GUI.enabled = !string.IsNullOrEmpty(_currentNamePainting)
                          && _currentSpritePainting != null
                          && _paintingList != null
                          && !string.IsNullOrEmpty(_currentDescriptionPainting)
                          && _currentKeyPointsList != null
                          && _currentKeyPointsList.Count > 0;
            
            if (GUILayout.Button("Create Paiting !"))
            {
                PaintingInstance.Create(_paintingList, _itemPath,  _currentNamePainting, _currentSpritePainting, _currentDescriptionPainting, _currentKeyPointsList);
                
                _currentNamePainting = string.Empty;
                _currentSpritePainting = null;
                _currentDescriptionPainting = string.Empty;
                _currentKeyPointsList.Clear();
            }
            
            GUI.enabled = true;
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