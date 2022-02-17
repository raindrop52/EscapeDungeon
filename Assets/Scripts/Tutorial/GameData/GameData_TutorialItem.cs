using System;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;

[Serializable]
public class TutorialItemInfo
{
    public int index;
    public string name;
    public int type;
    public string name_en;
    public string sprite_path;
    public string desc;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "GameData_TutorialItem", menuName = "GameData/TutorialItem", order = 1)]
public class GameData_TutorialItem : GameData
{
    public List<TutorialItemInfo> _dataList;
    public SpriteAssetManager _spriteAssetMgr;

    public TutorialItemInfo GetData(int index)
    {
        TutorialItemInfo info = null;

        foreach (TutorialItemInfo i in _dataList)
        {
            if (i.index == (int)index)
            {
                info = i;
                break;
            }
        }

        return info;
    }

    public TutorialItemInfo GetData(string itemName)
    {
        TutorialItemInfo info = null;

        foreach (TutorialItemInfo i in _dataList)
        {
            if (i.name == itemName)
            {
                info = i;
                break;
            }
        }

        return info;
    }


    // 유니티 에디터 함수는 #if UNITY_EDITOR #endif 로 감싸줘야 함
#if UNITY_EDITOR
    public override void Parse(System.Object[] objList)
    {
        _dataList = new List<TutorialItemInfo>();

        foreach (System.Object csvObj in objList)
        {
            // C# Object를 TutorialItemInfo 객체로 변환
            TutorialItemInfo info = new TutorialItemInfo();

            ParseObject(info, csvObj);

            if(info.sprite == null && _spriteAssetMgr != null)
            {
                info.sprite = _spriteAssetMgr.GetSprite(info.sprite_path);
            }

            _dataList.Add(info);
        }
    }
#endif


}
