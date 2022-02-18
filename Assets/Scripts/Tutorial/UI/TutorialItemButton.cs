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
        TutorialItemInfo _itemInfo;

        LevelUpUI _ownerUI;

        public void Init(LevelUpUI ownerUI)
        {
            _ownerUI = ownerUI;

            _nameTxt = transform.Find("name").GetComponent<Text>();
            _descTxt = transform.Find("desc").GetComponent<Text>();
            _itemImg = transform.Find("item").GetComponent<Image>();

            Button b = GetComponent<Button>();
            if(b != null)
            {
                b.onClick.AddListener(delegate ()
                {
                    _ownerUI.OnSelectItem();

                    Inventory._inst.StoreItem(_itemInfo);
                });
            }
        }

        public void SetData(TutorialItemInfo info)
        {
            _itemInfo = info;
            _nameTxt.text = info.name;
            _descTxt.text = info.desc;
            _itemImg.sprite = info.sprite;
        }

    }
}