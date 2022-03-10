using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Stage_Base : MonoBehaviour
    {
        public Transform _savePoint;

        public virtual void Init()
        {
            OnShow(true);
        }

        public virtual void StageStart()
        {

        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}