using System.Collections;  // �ޥ� �t��.���X �R�W�Ŷ� ��P�{�� API
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Practice
{
    /// <summary>
    /// �{�Ѩ�P�{�� Coroutine
    /// </summary>
    public class LearnCoroutine : MonoBehaviour
    {
        //�w�q��P�{�Ǥ�k
        //IEnumerator ����P�{�ǶǦ^�ȡN�i�Ǧ^�ɶ�
        //yield ���B
        //new WaitForSeconds(�B�I��) - ���ݮɶ�
        private IEnumerator TestCoroutine()
        {
            print("��P�{�Ƕ}�l����");
            yield return new WaitForSeconds(2);
            print("��P�{�ǵ��ݨ�����榹��");
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
            //�Ұʨ�P�{��
            StartCoroutine(TestCoroutine());
            StartCoroutine(SphereScale());
        }
    }
}