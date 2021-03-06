using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class StatusUI : MonoBehaviour
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

            OnShow(false);
        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }

        public void OnHitUI()
        {
            if(_lifeUI != null)
                _lifeUI.OnDamage();
        }

        public void OnHealUI(bool all = true)
        {
            if (_lifeUI != null)
            {
                // 전체 힐
                if (all == true)
                    _lifeUI.OnMaxHeal();
                else
                    _lifeUI.OnHeal();
            }
        }

        public void OnPoisoningUI(Poison_Type type, float holdingTime)
        {
            if (_poisonUI != null)
                _poisonUI.Poisoning(type, holdingTime);
        }

        public void OnPoisonUIResetTime()
        {
            if (_poisonUI != null)
                _poisonUI.PoisoningReset();
        }

        public void OnClearPoisonUI()
        {
            if (_poisonUI != null)
                _poisonUI.ClearPoison();
        }
    }
}