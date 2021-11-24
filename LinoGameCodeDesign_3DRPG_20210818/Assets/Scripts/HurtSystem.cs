using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LiangWei
{
    /// <summary>
    /// ���˨t��
    /// �B�z��q�N���˻P���`
    /// </summary>
    public class HurtSystem : MonoBehaviour
    {
        #region ��� : ���}
        [Header("��q"), Range(0, 5000)]
        public float hp = 100;
        [Header("���˨ƥ�")]
        public UnityEvent onHurt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�Ѽ� : ���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string parameterDead = "���`�}��";

        #endregion

        #region ��� : �p�H�P�O�@
        //private     �p�H �����\�b�l���O�s��
        //public      ���} ���\�Ҧ����O�s��
        //protected   �O�@ �ȭ��l���O�s��
        protected float hpMax;
        private Animator ani;

        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }

        #endregion

        #region ��k : ���}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">�����쪺�ˮ`</param>
        /// �����n�Q�l���O�Ƽg�����[�W virtual ����
        public virtual void Hurt (float damage)
        {
            if (ani.GetBool(parameterDead)) return;       //�p�G ���`�ѼƤw�g�Ŀ� �N���X
            hp -= damage;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0) Dead();
        }

        #endregion

        #region ��k : �p�H
        /// <summary>
        /// ���`
        /// </summary>
        private void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }

        #endregion
    }
}
