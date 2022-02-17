using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ns;

public class GameDataEditor : EditorWindow
{
    GameData _gameData;
    SpriteAssetManager _spriteAssetMgr;

    [MenuItem("Window/GameDataEditor")]
    static public void OpenGameDataEditor()
    {
        EditorWindow.GetWindow<GameDataEditor>(false, "GameData Editor", true);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("== GAME DATA EDITOR ==");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        _gameData = EditorGUILayout.ObjectField(_gameData, typeof(GameData), false) as GameData;

        if(GUILayout.Button("Import from csv"))
        {
            string gameDataType = _gameData.GetType().ToString();
            string filePath = "GameData/CSV/" + gameDataType + ".csv";
            bool hasFieldName = true;
            char seperator = ',';
            System.Object[] objList = CsvLoader.LoadCsvToObjectList(filePath, hasFieldName, seperator);

            _gameData.Parse(objList);

            EditorUtility.SetDirty(_gameData);
        }

        _spriteAssetMgr = EditorGUILayout.ObjectField(_spriteAssetMgr, typeof(SpriteAssetManager), false) as SpriteAssetManager;

        if (GUILayout.Button("Load Sprite"))
        {
            _spriteAssetMgr._spriteList = new List<SpriteInfo>();
            
            string filepath = "Assets/images/Tutorial/Transparent_Icons.png";

            var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(filepath);
            if (sprites != null)
            {
                foreach (var s in sprites)
                {
                    if (s is Sprite && s.name.Contains("#1") == false)
                    {
                        SpriteInfo info = new SpriteInfo();
                        info.name = s.name;
                        info.sprite = s as Sprite;

                        _spriteAssetMgr._spriteList.Add(info);
                    }
                }
            }

            EditorUtility.SetDirty(_gameData);
        }
    }
}
