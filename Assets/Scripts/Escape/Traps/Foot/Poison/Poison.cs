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

            // 중독 지속 시간
            while (time < _poisonTime)
            {
                time += Time.fixedDeltaTime;

                yield return null;
            }

            ClosePoison();
            // 중독 시간 종료 시 콜백 함수 호출
            if (_cb != null)
                _cb();
        }
    }
}