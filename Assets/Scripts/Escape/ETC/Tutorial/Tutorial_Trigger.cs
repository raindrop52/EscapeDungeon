using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Tutorial_Type
    {
        MOVE,
        SPACE,
    }

    public class Tutorial_Trigger : MonoBehaviour
    {
        Tutorial_Type _type;
        public List<Tutorial_Key> _keys;
        bool _inArea = false;
        [SerializeField] float _pushTime = 0.2f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                if(GameManager._inst._mobileMode == true)
                {
                    PlayRoomUI uiRoom = UIManager._inst.GetUI(UI_ID.PLAYROOM) as PlayRoomUI;
                    if(uiRoom != null)
                    {
                        uiRoom._noticeUI._notice_Tutorial.OnShow(true);
                    }
                }

                foreach (Tutorial_Key key in _keys)
                {
                    key.OnShow(true);
                    StartCoroutine(_OnPush());
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (GameManager._inst._mobileMode == true)
            {
                return;
            }

            if (collision.gameObject.tag.Equals("Player"))
            {
                _inArea = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                if (GameManager._inst._mobileMode == true)
                {
                    return;
                }

                _inArea = false;

                foreach (Tutorial_Key key in _keys)
                {
                    key.OnShow(false);
                }
            }
        }

        IEnumerator _OnPush()
        {
            int imgIndex = 1;       // 이미지 변경 번호
            int keyIndex = 0;       // 키 배열 번호

            yield return new WaitUntil(() => _inArea == true);

            while (_inArea == true)
            {
                if(keyIndex < _keys.Count)
                {
                    _keys[keyIndex].ChangeImg(imgIndex);
                    
                    yield return new WaitForSeconds(_pushTime);

                    keyIndex++;
                }
                else
                {
                    keyIndex = 0;
                    imgIndex++;
                }
            }
        }
    }
}