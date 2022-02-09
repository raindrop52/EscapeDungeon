using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace EscapeGame
{
    public class PlayerLight : MonoBehaviour
    {
        Light2D _light;

        void Start()
        {
            _light = GetComponent<Light2D>();
        }

        public float GetLightOuterRadius()
        {
            return _light.pointLightOuterRadius;
        }

        public void SetLightOuterRadius(float radius)
        {
            _light.pointLightOuterRadius = radius;
        }
    }
}