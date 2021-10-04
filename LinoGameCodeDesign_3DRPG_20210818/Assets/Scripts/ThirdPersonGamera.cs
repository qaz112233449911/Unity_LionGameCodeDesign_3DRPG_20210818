using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiangWei
{
    public class ThirdPersonGamera : MonoBehaviour
    {
        #region ���
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܳt��"), Range(0, 100)]
        public float speedTrack = 1.5f;
        [Header("���४�k�t��"), Range(0, 100)]
        public float speedTurnHorizontal = 5;
        [Header("����W�U�t��"), Range(0, 100)]
        public float speedTurnVertical = 5;
        #endregion

        #region �ݩ�
        /// <summary>
        /// ���o�ƹ������y��
        /// </summary>
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        /// <summary>
        /// ���o�ƹ������y��
        /// </summary>
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region �ƥ�
        private void Update()
        {
            TurnCamera();
        }
        //�b Update �����A�B�z��v���l�ܦ欰
        private void LateUpdate()
        {
            TrackTarget();
        }
        #endregion

        #region ��k
        /// <summary>
        /// �l�ܥؼ�
        /// </summary>
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;                         //���o �ؼ� �y��
            Vector3 posCamera = transform.position;                      //���o ��v�� �y��

            //��v���y�� = �t�� (�t�� * �@�����ɶ�)
            posCamera = Vector3.Lerp(posTarget, posCamera, speedTrack * Time.deltaTime);  //��v���y�� = �t��

            transform.position = posCamera;                              //�����󪺮y�� = ��v���y��
        }

        /// <summary>
        /// ������v��
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