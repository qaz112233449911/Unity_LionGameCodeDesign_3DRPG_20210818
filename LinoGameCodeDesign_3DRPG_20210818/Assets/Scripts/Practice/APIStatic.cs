using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 認識API
/// </summary>
public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        #region 靜態屬性
        //與靜態差異
        //1. 不需要實體物件
        //2. 不用取得實體物件
        //取得
        //語法:
        //類別名稱.靜態屬性
        float r = Random.value;
        print("取得靜態屬性，隨機值 : " + r);

        //設定 Set
        //語法
        //類別名稱.靜態屬性 指定 值:
        //只要看到Read Only 就不能設定
        Cursor.visible = false;
        #endregion

        #region 靜態方法
        //呼叫，參數、傳回
        //簽章: 參數、傳回
        //語法:
        //類別名稱.靜態方法(對應引數)
        float range = Random.Range(10.5f, 20.9f);
        print("隨機範圍10.5 ~ 20.9 : " + range);

        int rangeInt = Random.Range(1, 3);
        print("整數隨機範圍 1 ~ 3 : " + rangeInt);
        #endregion
    }
    private void Update()
    {
        #region 靜態屬性
        //print("經過多久 : " + Time.timeSinceLevelLoad);
        #endregion

        #region 靜態方法
        float h = Input.GetAxis("Horizontal");
        print("水平值 : " + h);
        #endregion
    }
}
