using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class ExpBar : MonoBehaviour
    {
        Image _expGagueImg;
        Text _expText;
        Text _lvText;

        public void Init()
        {
            _expGagueImg = transform.Find("ExpGague").GetComponent<Image>();
            _expText = transform.Find("Exp").GetComponent<Text>();
            _lvText = transform.Find("Level").GetComponent<Text>();

            RefreshUI();
        }

        public void RefreshUI()
        {
            Player player = GameManager._inst._playerTrans.GetComponent<Player>();

            float curLevel = player.Level;

            int curLvExp, nextLvExp;
            player.CalcLevelExp(out curLvExp, out nextLvExp);

            float exp = player.Exp;             // 구간 exp
            float needExp = nextLvExp - curLvExp;

            // 경험치 텍스트
            _expText.text = string.Format("{0} / {1}", exp, needExp);

            // 레벨 텍스트
            string level = string.Format("LV. {0}", curLevel);
            if(_lvText.text.Equals(level) == false)
                _lvText.text = level;

            // 게이지
            _expGagueImg.fillAmount = exp / needExp;
        }
    }
}