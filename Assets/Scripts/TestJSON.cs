using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyPlayerInfo
{
    public MyPlayerInfo(int lv, float time, string name)
    {
        itemIndexList = new List<int>();

        level = lv;
        timeElasped = time;
        playerName = name;

        for (int i = 0; i < 5; i++)
        {
            int randomInt = UnityEngine.Random.Range(0, 10);
            itemIndexList.Add(randomInt);
        }
    }

    public int level;
    public float timeElasped;
    public string playerName;
    public List<int> itemIndexList;
}

[Serializable]
public class MyList
{
    public List<MyPlayerInfo> infoList = new List<MyPlayerInfo>();
}

public class TestJSON : MonoBehaviour
{
    void Start()
    {
        MyList myList = new MyList();

        for (int i = 1; i <= 5; i++)
        {
            MyPlayerInfo info = new MyPlayerInfo(i, i + 5.0f, "tester " + i);

            myList.infoList.Add(info);
        }

        // json string �������� ����
        string jsonStr = JsonUtility.ToJson(myList);
        // json string ������ �����͸� MyList�� ��ȯ
        MyList myList2 = JsonUtility.FromJson<MyList>(jsonStr);
    }
}
