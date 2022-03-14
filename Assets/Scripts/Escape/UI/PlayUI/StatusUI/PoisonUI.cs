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
        PoisonImg _poImg;

        public void Poisoning(Poison_Type type, float holdingTime)
        {
            GameObject go = Instantiate(_prefab);
            go.transform.parent = transform;
            go.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            _poImg = go.GetComponent<PoisonImg>();
            if(_poImg != null)
                _poImg.Init();

            foreach(Poison_Info info in _poisonInfo)
            {
                // 동일한 타입의 경우
                if(type == info.type)
                {
                    // 이미지값 전달
                    _poImg.SetImage(info.sprite);
                    _poImg.OnTimer(holdingTime);
                }
            }
        }

        public void PoisoningReset()
        {
            if (_poImg != null)
                _poImg.Reset = true;
        }

        public void ClearPoison()
        {
            if (_poImg != null)
            {
                _poImg.Disappear();
            }
        }
    }
}