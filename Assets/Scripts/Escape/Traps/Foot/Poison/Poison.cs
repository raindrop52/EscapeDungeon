using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class Poison : MonoBehaviour
    {
        float _time = 0.0f;
        public float UsingTime
        { get { return _time; } set { _time = value; } }
        public float _poisonTime = 3.0f;
        Callback _cb = null;

        public void OnPoison(Player player, Callback cb)
        {
            _cb = cb;

            ExecutePoison(player);
        }

        protected virtual void ExecutePoison(Player player)
        {
            // 중독 코루틴 시작
            StartCoroutine(_Poisoning());
        }

        protected virtual void ClosePoison()
        {
            // 중독 상태 해제 후 상태 원상 복구
        }

        IEnumerator _Poisoning()
        {
            _time = 0.0f;

            // 중독 지속 시간
            while (_time < _poisonTime)
            {
                _time += Time.fixedDeltaTime;

                yield return null;
            }

            ClosePoison();

            // 중독 시간 종료 시 콜백 함수 호출
            if (_cb != null)
                _cb();
        }
    }
}