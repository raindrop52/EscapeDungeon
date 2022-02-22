using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class CollectionUI : MonoBehaviour
    {
        [SerializeField] List<Button> _collectionList;

        public void Init()
        {
            // 컬렉션 오브젝트의 하위 버튼들을 리스트에 할당
            Button[] btnList = transform.Find("Collection").GetComponentsInChildren<Button>(true);
            _collectionList = btnList.ToList<Button>();

            if(_collectionList != null)
            {
                SetButtonImage();
            }
        }

        void SetButtonImage()
        {

        }
    }
}