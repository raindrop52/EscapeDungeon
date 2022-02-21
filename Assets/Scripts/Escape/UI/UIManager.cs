using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EscapeGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        public Joystick _joystick;
        GameObject _textReadPanel;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _textReadPanel = GetComponentInChildren<WriteTyping>().gameObject;
            if(_textReadPanel.activeSelf)
            {
                // �ʱ�ȭ �� Ȱ��ȭ ������ ��� ��Ȱ��ȭ ó��
                ShowTextPanel(false);
            }

            // �˸� UI �ʱ� ��Ȱ��ȭ
            //ShowTextMessage("", true);
        }

        public void OnPointerDown()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnPointerDown(data);
        }

        public void OnPointerUp()
        {
            _joystick.OnPointerUp(null);
        }

        public void OnDrag()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnDrag(data);
        }

        public bool IsTextPanel()
        {
            return _textReadPanel.activeSelf;
        }

        void ShowTextPanel(bool show)
        {
            _textReadPanel.SetActive(show);
        }

        // UI�� ���� �ؽ�Ʈ ǥ��
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // ���� ����
            if(forceHide)
            {
                ShowTextPanel(false);
            }
            else
            {
                // show = �ؽ�Ʈ â ǥ�� ����
                bool isShow = _textReadPanel.activeSelf;

                if (_textReadPanel != null)
                {
                    // UI �ؽ�Ʈ â Ȱ��ȭ(�����ִ� ���), ��Ȱ��ȭ(�����ִ� ���)
                    if (isShow == false)
                        ShowTextPanel(true);
                    else
                        ShowTextPanel(false);

                    // Ȱ��ȭ �Ǿ����� �ؽ�Ʈ Ÿ���� ����
                    if (isShow == false)
                    {
                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // �ؽ�Ʈ �޽��� â �ݱ�
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }
    }
}