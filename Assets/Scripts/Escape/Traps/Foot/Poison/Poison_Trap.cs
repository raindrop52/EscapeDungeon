using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Poison_Type
    {
        None = 0,               // ����Ʈ
        Blind,                  // �Ǹ�
        Paralysis,              // ����
        Slow,                   // ����
        Faint,                  // ����
        Count                   // ����
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
                // ����
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
            // �ش� �ߵ� ���°� false�� ���
            if(player.GetPoisonStatus(_poisonType) == false)
            {
                // �ش� �ߵ� ���� true ��ȯ
                player.SetPoisonStatus(_poisonType, true);

                if(_poison != null)
                {
                    UIManager._inst._statusUI.OnPoisoningUI(_poisonType, _poison._poisonTime);

                    // �÷��̾� �ߵ�
                    _poison.OnPoison(player, delegate ()
                    {
                        // �ߵ� ���� �� �ݹ� �Լ� ȣ��
                        player.SetPoisonStatus(_poisonType, false);
                    });
                }
            }
            // �ߵ� ������ ���
            else if(player.GetPoisonStatus(_poisonType) == true)
            {
                // UI �ð� ����
                UIManager._inst._statusUI.OnPoisonUIResetTime();
                // �ߵ� �ð� ����
                if (_poison != null)
                    _poison.UsingTime = 0.0f;
            }
        }
    }
}