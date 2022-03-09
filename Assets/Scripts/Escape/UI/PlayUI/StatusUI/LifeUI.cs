using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class LifeUI : MonoBehaviour
    {
        Life[] _lifeList;
        int _hideRank = 0;          // 데미지 처리 시 숨기는 순서
        int _maxCount = 0;

        public void Init()
        {
            _lifeList = GetComponentsInChildren<Life>();

            foreach(Life life in _lifeList)
            {
                // 초기 라이프 전부 표시
                life.ShowLife(true);
                _maxCount++;
            }
        }

        public void OnDamage()
        {
            if(_hideRank >= _maxCount - 1)
            {
                Debug.Log("Error : 맞은 횟수가 최대 라이프보다 많은 상태");

                GameManager._inst.Die = true;

                return;
            }

            // 현재 숨기기 할 Life가 활성화 상태인 경우
            if(_lifeList[_hideRank].gameObject.activeSelf == true)
            {
                _lifeList[_hideRank].ShowLife(false);
                _hideRank++;
            }
        }

        public void OnHeal()
        {
            if(_hideRank < 0)
            {
                Debug.Log("Error : 라이프가 0보다 작을 순 없다.");
                _hideRank = 0;
                return;
            }

            int prevRank = _hideRank - 1;
            if (prevRank > -1)
            {
                // 이전 Life가 비활성화 상태인 경우
                if (_lifeList[prevRank].gameObject.activeSelf == false)
                {
                    _lifeList[prevRank].ShowLife(true);
                    _hideRank--;
                }
            }
        }

        public void OnMaxHeal()
        {
            for (int i = _hideRank; i > 0; i--)
            {
                OnHeal();
            }
        }
    }
}