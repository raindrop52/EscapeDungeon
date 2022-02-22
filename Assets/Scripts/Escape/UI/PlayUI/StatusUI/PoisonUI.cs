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
        public string name;
        public Sprite sprite;
    }

    public class PoisonUI : MonoBehaviour
    {
        public List<Poison_Info> _poisonInfo;
        PoisonImg[] _imgList;
        int _rank;                  // 표시 순서

        public void Init()
        {
            _imgList = GetComponentsInChildren<PoisonImg>(true);
            
            foreach(PoisonImg img in _imgList)
            {
                img.Init();
            }
        }

        
    }
}