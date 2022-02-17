using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SpriteInfo
{
    public string name;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "SpriteAssetManager", menuName = "Common/SpriteAssetManager", order = 3)]
public class SpriteAssetManager : ScriptableObject
{
    public List<SpriteInfo> _spriteList;

    public Sprite GetSprite(string spriteName)
    {
        foreach (SpriteInfo info in _spriteList)
        {
            if(info.name == spriteName)
            {
                return info.sprite;
            }
        }

        return null;
    }
}
