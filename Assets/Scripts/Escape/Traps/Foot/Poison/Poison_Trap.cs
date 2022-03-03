using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Poison_Type
    {
        None = 0,               // 디폴트
        Blind,                  // 실명
        Paralysis,              // 마비
        Slow,                   // 감속
        Faint,                  // 기절
        Count                   // 개수
    }

    public class Poison_Trap : Trap_Foot
    {
        Poison _poison;
        Poison_Type _poisonType;

        void Start()
        {
            _poison = GetComponent<Poison>();

            _poisonType = FindPoisonType();
        }

        Poison_Type FindPoisonType()
        {
            Poison_Type type = Poison_Type.None;

            if (_poison != null)
            {
                // 출혈
                if (_poison is Blind)
                {
                    type = Poison_Type.Blind;
                }
                else if (_poison is Paralysis)
                {
                    type = Poison_Type.Paralysis;
                }
                else if (_poison is Slow)
                {
                    type = Poison_Type.Slow;
                }
                else if (_poison is Faint)
                {
                    type = Poison_Type.Faint;
                }
            }

            return type;
        }

        protected override void ExecuteTrap(GameObject playerObj)
        {
            Player player = playerObj.GetComponent<Player>();

            if(player != null)
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

                if(_poison != null)
                {
                    UIManager._inst._statusUI.OnPoisoningUI(_poisonType, _poison._poisonTime);

                    // 플레이어 중독
                    _poison.OnPoison(player, delegate ()
                    {
                        // 중독 해제 시 콜백 함수 호출
                        player.SetPoisonStatus(_poisonType, false);
                    });
                }
            }
            // 중독 상태인 경우
            else if(player.GetPoisonStatus(_poisonType) == true)
            {
                // UI 시간 갱신
                UIManager._inst._statusUI.OnPoisonUIResetTime();
                // 중독 시간 갱신
                if (_poison != null)
                    _poison.UsingTime = 0.0f;
            }
        }
    }
}