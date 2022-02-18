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
                _textReadPanel.SetActive(false);
            }

            // �˸� UI �ʱ� ��Ȱ��ȭ
            ShowTextMessage("", true);
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

        // UI�� ���� �ؽ�Ʈ ǥ��
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // ���� ����
            if(forceHide)
            {
                _textReadPanel.SetActive(false);
            }
            else
            {
                // show = �ؽ�Ʈ â ǥ�� ����
                bool isShow = _textReadPanel.activeSelf;

                if (_textReadPanel != null)
                {
                    // UI �ؽ�Ʈ â Ȱ��ȭ(�����ִ� ���), ��Ȱ��ȭ(�����ִ� ���)
                    if (isShow == false)
                        _textReadPanel.SetActive(true);
                    else
                        _textReadPanel.SetActive(false);

                    // Ȱ��ȭ �Ǿ����� �ؽ�Ʈ Ÿ���� ����
                    if (isShow == false)
                    {
                        // �ؽ�Ʈ �ֱ�
                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init();
                    }
                }
            }
        }
    }
}