using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class Poison : MonoBehaviour
    {
        public float _poisonTime = 3.0f;
        Callback _cb = null;

        public void OnPoison(Player player, Callback cb)
        {
            _cb = cb;

            ExecutePoison(player);
        }

        protected virtual void ExecutePoison(Player player)
        {
            // �ߵ� �ڷ�ƾ ����
            StartCoroutine(_Poisoning());
        }

        protected virtual void ClosePoison()
        {
            // �ߵ� ���� ���� �� ���� ���� ����
        }

        IEnumerator _Poisoning()
        {
            float time = 0.0f;

            // �ߵ� ���� �ð�
            while (time < _poisonTime)
            {
                time += Time.fixedDeltaTime;

                yield return null;
            }

            ClosePoison();

            // �ߵ� �ð� ���� �� �ݹ� �Լ� ȣ��
            if (_cb != null)
                _cb();
        }
    }
}