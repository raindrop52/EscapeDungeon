using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Poison : MonoBehaviour
    {
        [SerializeField] float _poisonTime = 3.0f;
        Callback _cb = null;
        
        public void OnPoison(Player player, Callback cb)
        {
            _cb = cb;
            ExecutePoison(player);
        }

        protected virtual void ExecutePoison(Player player)
        {
            StartCoroutine(_PoisonCheck());
        }

        protected virtual void ClosePoison()
        {

        }

        IEnumerator _PoisonCheck()
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