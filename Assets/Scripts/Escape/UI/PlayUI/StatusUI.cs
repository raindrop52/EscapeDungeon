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
        }

        public void OnHitUI()
        {
            if(_lifeUI != null)
                _lifeUI.OnDamage();
        }

        public void OnHealUI()
        {
            if (_lifeUI != null)
                _lifeUI.OnHeal();
        }

        public void OnPoisoningUI(Poison_Type type, float holdingTime)
        {
            if (_poisonUI != null)
                _poisonUI.Poisoning(type, holdingTime);
        }
    }
}