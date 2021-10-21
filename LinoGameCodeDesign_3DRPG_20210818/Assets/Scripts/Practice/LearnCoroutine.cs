using System.Collections;  // 引用 系統.集合 命名空間 協同程序 API
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Practice
{
    /// <summary>
    /// 認識協同程序 Coroutine
    /// </summary>
    public class LearnCoroutine : MonoBehaviour
    {
        //定義協同程序方法
        //IEnumerator 為協同程序傳回值﹑可傳回時間
        //yield 讓步
        //new WaitForSeconds(浮點數) - 等待時間
        private IEnumerator TestCoroutine()
        {
            print("協同程序開始執行");
            yield return new WaitForSeconds(2);
            print("協同程序等待兩秒後執行此行");
        }

        public Transform sphere;

        private IEnumerator SphereScale()
        {
            for(int i = 0; i < 10; i++)
            {
                sphere.localScale += Vector3.one;
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void Start()
        {
            //啟動協同程序
            StartCoroutine(TestCoroutine());
            StartCoroutine(SphereScale());
        }
    }
}