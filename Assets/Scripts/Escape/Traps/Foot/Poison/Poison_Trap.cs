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
        Poison_Type _type;

        void Start()
        {
            _poison = GetComponent<Poison>();

            _type = FindPoisonType(_poison);
        }

        protected override void ExecuteTrap(GameObject playerObj)
        {
            // �� ���� ����
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
            // �ش� �ߵ� ���°� false�� ���
            if(player.GetPoisonStatus(_type) == false)
            {
                // �ش� �ߵ� ���� true ��ȯ
                player.SetPoisonStatus(_type, true);

                if(_poison != null)
                {
                    UIManager._inst.SetPoisonUI(_type, _poison._poisonTime);

                    // �÷��̾� �ߵ�
                    _poison.OnPoison(player, delegate()
                    {
                        player.SetPoisonStatus(_type, false);
                    });
                }
            }
            // �ߵ� ������ ���
            else if(player.GetPoisonStatus(_type) == true)
            {
                // �ߵ� �ð� ����
                if (_poison != null)
                    _poison.UsingTime = 0.0f;
                // UI �ð� ����
                UIManager._inst.ResetPoisonTimeUI();
            }
        }
    }
}