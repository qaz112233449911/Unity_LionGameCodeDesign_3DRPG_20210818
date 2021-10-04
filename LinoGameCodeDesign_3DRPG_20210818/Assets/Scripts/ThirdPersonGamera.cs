using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiangWei
{
    public class ThirdPersonGamera : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;
        [Header("追蹤速度"), Range(0, 100)]
        public float speedTrack = 1.5f;
        [Header("旋轉左右速度"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("旋轉上下速度"), Range(0, 100)]
        public float speedTurnVertical = 5;
        #endregion

        #region 屬性
        /// <summary>
        /// 取得滑鼠水平座標
        /// </summary>
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        /// <summary>
        /// 取得滑鼠垂直座標
        /// </summary>
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region 事件
        private void Update()
        {
            TurnCamera();
        }
        //在 Update 後執行，處理攝影機追蹤行為
        private void LateUpdate()
        {
            TrackTarget();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 追蹤目標
        /// </summary>
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;                         //取得 目標 座標
            Vector3 posCamera = transform.position;                      //取得 攝影機 座標

            //攝影機座標 = 差值 (速度 * 一偵的時間)
            posCamera = Vector3.Lerp(posTarget, posCamera, speedTrack * Time.deltaTime);  //攝影機座標 = 差值

            transform.position = posCamera;                              //此物件的座標 = 攝影機座標
        }

        /// <summary>
        /// 旋轉攝影機
        /// </summary>
        private void TurnCamera()
        {
            transform.Rotate(
                inputMouseY * Time.deltaTime * speedTurnVertical,
                inputMouseX * Time.deltaTime * speedTurnHorizontal, 0);
        }
        #endregion

    }
}