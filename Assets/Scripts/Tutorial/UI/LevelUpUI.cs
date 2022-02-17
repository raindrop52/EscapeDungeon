using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class LevelUpUI : MonoBehaviour
    {
        CallBack _cb;
        Button[] _btnList;

        public void Init()
        {
            int i = 0;
            // ��ư �̺�Ʈ ����
            _btnList = transform.Find("ListView").GetComponentsInChildren<Button>(true);
            foreach (Button b in _btnList)
            {
                TutorialItemButton itemBtn = b.GetComponent<TutorialItemButton>();
                itemBtn.Init();

                b.onClick.AddListener(delegate ()
                {
                    OnClickItem(itemBtn);
                    });

                i++;
            }
        }

        public void Show(bool show, CallBack cb = null)
        {
            _cb = cb;

            if(gameObject.activeSelf != show)
                gameObject.SetActive(show);

            if(show == true)
            {
                // �������� ���̺��� ������ 3���� ��� �����ֱ�
                SetItemInfo();
            }
        }

        void SetItemInfo()
        {
            // ���̺��� ������ �������� ����
            GameData_TutorialItem itemTable = GameManager._inst._itemTable;

            int itemCount = itemTable._dataList.Count;

            int index1 = Random.Range(0, itemCount);
            int index2 = 0;
            int index3 = 0;
            while(true)
            {
                index2 = Random.Range(0, itemCount);
                if (index1 != index2)
                    break;
            }

            while (true)
            {
                index3 = Random.Range(0, itemCount);

                if (index1 != index3 && index2 != index3)
                    break;
            }

            // ������ �ε����� ��� �ٸ� ������ üũ
            Debug.Assert(index1 != index2 && index1 != index3 && index2 != index3);

            TutorialItemInfo itemInfo = itemTable._dataList[index1];
            TutorialItemButton itemBtn1 = _btnList[0].GetComponent<TutorialItemButton>();
            itemBtn1.SetData(itemInfo);

            itemInfo = itemTable._dataList[index2];
            TutorialItemButton itemBtn2 = _btnList[1].GetComponent<TutorialItemButton>();
            itemBtn2.SetData(itemInfo);

            itemInfo = itemTable._dataList[index3];
            TutorialItemButton itemBtn3 = _btnList[2].GetComponent<TutorialItemButton>();
            itemBtn3.SetData(itemInfo);
        }

        void OnClickItem(TutorialItemButton selectButton)
        {
            if (_cb != null)
                _cb();

            gameObject.SetActive(false);

            selectButton.SetItemUI();
        }
    }
}