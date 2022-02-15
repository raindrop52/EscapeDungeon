using System;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;

[Serializable]
public class TutorialItemInfo
{
    public int index;
    public string name;
    public string name_en;
    public int type;
    public string content;
}

[CreateAssetMenu(fileName = "GameData_Tutorial", menuName = "GameData/Tutorial", order = 1)]
public class GameData_TutorialItem : GameData
{
    List<TutorialItemInfo> _itemDataList;

    // ����Ƽ ������ �Լ��� #if UNITY_EDITOR #endif �� ������� ��
#if UNITY_EDITOR
    public override void Parse(System.Object[] objList)
    {
        _itemDataList = new List<TutorialItemInfo>();

        foreach (System.Object csvObj in objList)
        {
            //// C# Object�� UnitInfo ��ü�� ��ȯ
            //UnitInfo info = new UnitInfo();

            //ParseObject(info, csvObj);

            //_itemDataList.Add(info);
        }
    }
#endif


}
