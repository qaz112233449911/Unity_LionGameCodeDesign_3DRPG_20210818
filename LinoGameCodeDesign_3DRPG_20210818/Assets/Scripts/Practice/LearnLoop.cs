using UnityEngine;

namespace LiangWei.Practice
{
    /// <summary>
    /// �{�Ѱj��
    /// while�Ndo while�Nfor�Nforeach
    /// </summary>
    public class LearnLoop : MonoBehaviour
    {
        private void Start()
        {
            // �j�� Loop
            // ���ư���{�����e
            // �ݨD : ��X�Ʀr 1 - 5
            print(1);
            print(2);
            print(3);
            print(4);
            print(5);

            // while �j��
            // �y�k : if (���L��) { �{�����e }     -���L�Ȭ� true ����@��
            // �y�k : while (���L��) { �{�����e }  -���L�Ȭ� true �������
            int a = 1;

            while (a < 6)
            {
                print("�j�� while : " + a);
                a++;
            }

            // for �j��
            // �y�k : for(��l��, ����(���L��); �j�鵲������{��) { �{�����e }
            for (int i = 1; i < 6; i++)
            {
                print("�j�� for : " + i);
            }
        }
    }
}
