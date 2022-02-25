using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public class TileAnimation : MonoBehaviour
    {
        public SpriteRenderer _renderer;
        public Sprite _sp1;
        public Sprite _sp2;
        Tilemap _tilemap;
        

        float _delta;
        public float _span = 0.5f;

        void Start()
        {
            _tilemap = GetComponent<Tilemap>();
            Vector3Int vectorTest;
            bool testB;

            for (int i = _tilemap.cellBounds.x; i < _tilemap.cellBounds.size.x; i++)
            {
                vectorTest = new Vector3Int(i, 0, 0);
                testB = _tilemap.HasTile(vectorTest);
                Debug.Log(i + ",0의 위치 타일 존재 유무 : " + testB);
            }


            //Sprite sprite = _tilemap.GetSprite(Vector3Int.zero);
            //_renderer.sprite = sprite;
            //_renderer = GetComponent<SpriteRenderer>();
        }

        // 타일맵 위치 체크용
        #region TEST
        private void OnMouseOver()
        {
            try
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);



                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);



                if (_tilemap = hit.transform.GetComponent<Tilemap>())
                {
                    _tilemap.RefreshAllTiles();

                    int x, y;
                    x = _tilemap.WorldToCell(ray.origin).x;
                    y = _tilemap.WorldToCell(ray.origin).y;

                    Vector3Int v3Int = new Vector3Int(x, y, 0);

                    Debug.Log(v3Int);

                    //타일 색 바꿀 때 이게 있어야 하더군요
                    //_tilemap.SetTileFlags(v3Int, TileFlags.None);

                    //타일 색 바꾸기
                    //_tilemap.SetColor(v3Int, (Color.white));

                }
            }
            catch (NullReferenceException)
            {

            }
        }

        private void onMouseExit()
        {
            _tilemap.RefreshAllTiles();

        }
        #endregion

        void Update()
        {
            /*
            _delta += Time.deltaTime;
            if (_delta > _span)
            {
                _renderer.sprite = _sp1;
            }
            if (_delta > _span * 2.0f)
            {
                _renderer.sprite = _sp2;

                _delta = 0.0f;
            }
            */
        }
    }
}