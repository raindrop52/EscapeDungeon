using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class StatusUI : BaseUI
    {
        // ����� ������ UI
        LifeUI _lifeUI;
        // �ߵ� ������ UI
        PoisonUI _poisonUI;

        public void Init()
        {
            _lifeUI = GetComponentInChildren<LifeUI>();
            _lifeUI.Init();

            _poisonUI = GetComponentInChildren<PoisonUI>();
            _poisonUI.Init();
        }

        public void OnHitUI()
        {
            _lifeUI.OnDamage();
        }

        public void OnHealUI()
        {
            _lifeUI.OnHeal();
        }
    }
}