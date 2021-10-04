using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 靜態屬性與方法 API課堂練習
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
   
    private void Start()
    {
        int count = Camera.allCameras.Length;
        print("目前擁有" + count + "cameras");

        Vector2 Gravity = Physics2D.gravity;
        print("2D 重力 : " + Physics2D.gravity);

        
        print("圓周率 : " + Mathf.PI);
        
        Physics2D.gravity = new Vector2(0, -20);

        Time.timeScale = 0.5f;

        print("9.999 去除小數點結果 : " + Mathf.Round(9.999f));

        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b=new Vector3(22, 22, 22);
        print("a b 兩點的距離 : " + Vector3.Distance(a, b));

        Application.OpenURL("https://unity.com/");
    }


    private void Update()
    {
        
        print("是否輸入任意鍵 : " + Input.anyKey);
        
        print("時間 : " + Time.time);

        print("是否按下空白鍵 : " + Input.GetKeyDown(KeyCode.Space));
    }
}
