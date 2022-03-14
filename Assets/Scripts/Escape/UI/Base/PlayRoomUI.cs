using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class PlayRoomUI : BaseUI
    {
        GameObject _textReadPanel;

        StatusUI _statusUI;
        ControlUI _controlUI;
        MessageBox _messageBox;

        bool _talking = false;
        public bool Talking
        { get { return _talking; } }

        protected override void OnEnable()
        {
            // ���� X
        }

        public override void Init()
        {
            base.Init();

            _textReadPanel = GetComponentInChildren<WriteTyping>(true).gameObject;
            if (_textReadPanel.activeSelf)
            {
                // �ʱ�ȭ �� Ȱ��ȭ ������ ��� ��Ȱ��ȭ ó��
                ShowTextPanel(false);
            }

            _messageBox = GetComponentInChildren<MessageBox>(true);
            if (_messageBox != null)
            {
                _messageBox.OnShow(false);
            }

            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();
            _controlUI = transform.Find("ControlUI").GetComponentInChildren<ControlUI>();
            _controlUI.Init();
        }

        public void StageInit()
        {
            // �÷��̾� ü�¹� �ʱ�ȭ

            // �÷��̾� �����(�ߵ�) ���� �ʱ�ȭ

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
            if (forceHide)
            {
                if (_talking == true)
                    _talking = false;

                ShowTextPanel(false);
            }
            else
            {
                if (_textReadPanel != null)
                {
                    // show = �ؽ�Ʈ â ǥ�� ����
                    bool isShow = _textReadPanel.activeSelf;

                    // UI �ؽ�Ʈ â Ȱ��ȭ(�����ִ� ����)
                    if (isShow == false)
                    {
                        _talking = true;

                        ShowTextPanel(true);

                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // �ؽ�Ʈ �޽��� â �ݱ�
                            _talking = false;
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }

        public MessageBox GetMessageBox()
        {
            return _messageBox;
        }

        public void OnHit()
        {
            _statusUI.OnHitUI();
        }

        public void OnHeal()
        {
            _statusUI.OnHealUI();
        }

        public void OnPoisoning(Poison_Type type, float holdingTime)
        {
            _statusUI.OnPoisoningUI(type, holdingTime);
        }

        public void OnPoisoningTimeReset()
        {
            _statusUI.OnPoisonUIResetTime();
        }

        public void OnClearPoison()
        {
            _statusUI.OnClearPoisonUI();
        }
    }
}