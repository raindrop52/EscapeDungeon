using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [Serializable]
    public class PlayerItemInfo
    {
        public PlayerItemInfo(int itemIndex)
        {
            _itemIndex = itemIndex;
            _itemLevel = 1;
        }

        public int _itemIndex;
        public int _itemLevel;
    }

    public class Inventory : MonoBehaviour
    {
        public static Inventory _inst;

        public List<PlayerItemInfo> _playerItemList;

        void Awake()
        {
            _inst = this;
        }

        public void StoreItem(TutorialItemInfo itemInfo)
        {
            PlayerItemInfo playerItem = new PlayerItemInfo(itemInfo.index);
            
            _playerItemList.Add(playerItem);
        }

        public bool HasItem(int itemIndex)
        {
            // TODO : 추후 구현

            return false;
        }

        public void LevelUpItem(TutorialItemInfo itemInfo)
        {
            if(_playerItemList.Count > 0)
            {
                foreach(PlayerItemInfo info in _playerItemList)
                {
                    if(itemInfo.index == info._itemIndex)
                    {
                        info._itemLevel++;
                    }
                }
            }
        }
    }
}