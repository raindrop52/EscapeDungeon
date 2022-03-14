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
        Poison_Type _type;

        void Start()
        {
            _poison = GetComponent<Poison>();

            _type = FindPoisonType(_poison);
        }

        protected override void ExecuteTrap(GameObject playerObj)
        {
            // 독 함정 동작
            Player player = playerObj.GetComponent<Player>();

            if(player != null)
            {
                SetPoisonStatus(player);
            }
        }

        Poison_Type FindPoisonType(Poison poison)
        {
            Poison_Type type = Poison_Type.None;

            if (poison is Blind)
            {
                type = Poison_Type.Blind;
            }
            else if (poison is Paralysis)
            {
                type = Poison_Type.Paralysis;
            }
            else if (poison is Slow)
            {
                type = Poison_Type.Slow;
            }
            else if (poison is Faint)
            {
                type = Poison_Type.Faint;
            }

            return type;
        }

        void SetPoisonStatus(Player player)
        {
            // 해당 중독 상태가 false인 경우
            if(player.GetPoisonStatus(_type) == false)
            {
                // 해당 중독 상태 true 전환
                player.SetPoisonStatus(_type, true);

                if(_poison != null)
                {
                    UIManager._inst.SetPoisonUI(_type, _poison._poisonTime);

                    // 플레이어 중독
                    _poison.OnPoison(player, delegate()
                    {
                        player.SetPoisonStatus(_type, false);
                    });
                }
            }
            // 중독 상태인 경우
            else if(player.GetPoisonStatus(_type) == true)
            {
                // 중독 시간 갱신
                if (_poison != null)
                    _poison.UsingTime = 0.0f;
                // UI 시간 갱신
                UIManager._inst.ResetPoisonTimeUI();
            }
        }
    }
}