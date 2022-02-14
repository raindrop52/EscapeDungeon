using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class ExpBar : MonoBehaviour
    {
        Image _expGagueImg;

        public void Init()
        {
            _expGagueImg = transform.Find("ExpGague").GetComponent<Image>();

            RefreshUI();
        }

        public void RefreshUI()
        {
            Player player = GameManager._inst._playerTrans.GetComponent<Player>();

            float curLevel = player.Level;
            float nextLevel = curLevel + 1;

            int curLevelExp = (int)((float)player._maxExp * player._expCurve.Evaluate(curLevel/(float)player._maxLevel));
            int NextLevelExp = (int)((float)player._maxExp * player._expCurve.Evaluate(nextLevel / (float)player._maxLevel));

            float exp = player.Exp;     // ±¸°£ exp
            float needExp = NextLevelExp - curLevelExp;

            _expGagueImg.fillAmount = exp / needExp;
        }
    }
}