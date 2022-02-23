using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    [Serializable]
    public class Poison_Info
    {
        public Poison_Type type;
        public Sprite sprite;
    }

    public class PoisonUI : MonoBehaviour
    {
        public List<Poison_Info> _poisonInfo;
        [SerializeField] GameObject _prefab;

        public void Poisoning(Poison_Type type, float holdingTime)
        {
            GameObject go = Instantiate(_prefab);
            go.transform.parent = transform;
            go.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            PoisonImg poImg = go.GetComponent<PoisonImg>();
            poImg.Init();

            foreach(Poison_Info info in _poisonInfo)
            {
                // 동일한 타입의 경우
                if(type == info.type)
                {
                    // 이미지값 전달
                    poImg.SetImage(info.sprite);
                    poImg.OnTimer(holdingTime);
                }
            }
        }
    }
}