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

            float exp = player.Exp;             // ���� exp
            float needExp = nextLvExp - curLvExp;

            // ����ġ �ؽ�Ʈ
            _expText.text = string.Format("{0} / {1}", exp, needExp);

            // ���� �ؽ�Ʈ
            string level = string.Format("LV. {0}", curLevel);
            if(_lvText.text.Equals(level) == false)
                _lvText.text = level;

            // ������
            _expGagueImg.fillAmount = exp / needExp;
        }
    }
}