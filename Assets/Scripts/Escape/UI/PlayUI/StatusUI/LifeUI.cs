using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class LifeUI : MonoBehaviour
    {
        Life[] _lifeList;
        int _hideRank = 0;          // ������ ó�� �� ����� ����
        int _maxCount = 0;

        public void Init()
        {
            _lifeList = GetComponentsInChildren<Life>();

            foreach(Life life in _lifeList)
            {
                // �ʱ� ������ ���� ǥ��
                life.ShowLife(true);
                _maxCount++;
            }
        }

        public void OnDamage()
        {
            if(_hideRank >= _maxCount - 1)
            {
                Debug.Log("Error : ���� Ƚ���� �ִ� ���������� ���� ����");

                GameManager._inst.Die = true;

                return;
            }

            // ���� ����� �� Life�� Ȱ��ȭ ������ ���
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
                Debug.Log("Error : �������� 0���� ���� �� ����.");
                _hideRank = 0;
                return;
            }

            int prevRank = _hideRank - 1;
            if (prevRank > -1)
            {
                // ���� Life�� ��Ȱ��ȭ ������ ���
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