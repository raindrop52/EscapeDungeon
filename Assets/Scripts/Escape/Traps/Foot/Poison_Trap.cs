using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Poison_Type
    {
        None = 0,               // 디폴트
        Bleeding,               // 출혈
        Paralysis,              // 마비
        Slow,                   // 감속
        Faint,                  // 기절
        Count                   // 개수
    }

    public class Poison_Trap : Trap_Foot
    {
        [SerializeField] Poison_Type _poisonType;
        [SerializeField] Poison _poison;

        void Start()
        {
            _poison = GetComponent<Poison>();
        }

        protected override void ExecuteTrap(GameObject playerObj)
        {
            Player_Control pc = playerObj.GetComponent<Player_Control>();
            Player player = playerObj.GetComponent<Player>();

            if(pc != null && player != null)
            {
                SetPoisonStatus(player);
            }
        }

        void SetPoisonStatus(Player player)
        {
            // 해당 중독 상태가 false인 경우
            if(player.GetPoisonStatus(_poisonType) == false)
            {
                // 해당 중독 상태 true 전환
                player.SetPoisonStatus(_poisonType, true);

                // 플레이어 중독
                _poison.OnPoison(player, delegate ()
                {
                    // 중독 해제 시 콜백 함수 호출
                    player.SetPoisonStatus(_poisonType, false);
                });
            }
        }
    }
}