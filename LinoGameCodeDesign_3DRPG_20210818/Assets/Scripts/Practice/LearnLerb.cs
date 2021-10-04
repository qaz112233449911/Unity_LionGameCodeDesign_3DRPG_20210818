using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Practice
{
    /// <summary>
    /// 學習差值 Lerp
    /// </summary>
    public class LearnLerb : MonoBehaviour
    {
        public float a = 0, b = 100;
        public float c = 0, d = 100;
        public Color colorA = Color.blue, colorB = Color.yellow;
        public Vector3 v3A = Vector3.zero, v3B = Vector3.one * 100;

        private void Start()
        {
            print("a b 兩點差值0.5 : " + Mathf.Lerp(a, b, 0.5f));
            print("a b 兩點差值0.5 : " + Mathf.Lerp(a, b, 0.1f));
        }

        private void Update()
        {
            d = Mathf.Lerp(c, d, 0.5f);
            colorB = Color.Lerp(colorA, colorB, 0.8f);
            v3B = Vector3.Lerp(v3A, v3B, 0.5f);
        }
    }
}