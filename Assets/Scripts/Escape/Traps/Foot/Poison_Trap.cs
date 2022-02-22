using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Poison_Type
    {
        None = 0,               // ����Ʈ
        Bleeding,               // ����
        Paralysis,              // ����
        Slow,                   // ����
        Faint,                  // ����
        Count                   // ����
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
            // �ش� �ߵ� ���°� false�� ���
            if(player.GetPoisonStatus(_poisonType) == false)
            {
                // �ش� �ߵ� ���� true ��ȯ
                player.SetPoisonStatus(_poisonType, true);

                // �÷��̾� �ߵ�
                _poison.OnPoison(player, delegate ()
                {
                    // �ߵ� ���� �� �ݹ� �Լ� ȣ��
                    player.SetPoisonStatus(_poisonType, false);
                });
            }
        }
    }
}