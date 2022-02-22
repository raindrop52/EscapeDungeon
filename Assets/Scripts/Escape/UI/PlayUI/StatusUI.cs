using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class StatusUI : BaseUI
    {
        // 생명력 아이콘 UI
        LifeUI _lifeUI;
        // 중독 아이콘 UI
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