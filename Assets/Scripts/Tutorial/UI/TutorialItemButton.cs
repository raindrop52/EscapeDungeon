using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialItemButton : MonoBehaviour
    {
        public Text _nameTxt;
        public Text _descTxt;
        public Image _itemImg;

        public void Init()
        {
            _nameTxt = transform.Find("name").GetComponent<Text>();
            _descTxt = transform.Find("desc").GetComponent<Text>();
            _itemImg = transform.Find("item").GetComponent<Image>();
        }

        public void SetData(TutorialItemInfo info)
        {
            _nameTxt.text = info.name;
            _descTxt.text = info.desc;
            //_itemImg.sprite = ;
        }
    }
}