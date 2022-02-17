using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public enum STATUS
    {
        NONE,
        WAIT,
        READY,
        START,
        END,
    }

    public class AIManager : MonoBehaviour
    {
        public static AIManager _inst;

        [Header("��������1 ����")]
        [SerializeField] GameObject _tileHidden;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            ShowHiddenTile(false);
        }

        // ���� Ÿ�� ����
        public void ShowHiddenTile(bool show)
        {
            Tilemap[] tileHidden = _tileHidden.GetComponentsInChildren<Tilemap>(true);
            foreach (Tilemap tile in tileHidden)
            {
                TilemapCollider2D collider = tile.GetComponent<TilemapCollider2D>();

                // �ݶ��̴��� �ִ� �� 
                if (collider != null)
                {
                    tile.gameObject.SetActive(!show);
                }
                else
                {
                    tile.gameObject.SetActive(show);
                }
            }
        }
    }
}