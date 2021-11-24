using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LiangWei
{
    /// <summary>
    /// �]�t���������˨t��
    /// �i�H�B�z�����s
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("�n��s�����")]
        public Image imgHp;

        /// <summary>
        /// ����ĪG�M�Ϊ�����e��q
        /// </summary>
        private float hpEffectOriginal;

        //�Ƽg�����O���� override
        public override void Hurt(float damage)
        {
            hpEffectOriginal = hp;

            //�Ӧ����������O�� �����O�������e
            base.Hurt(damage);

            StartCoroutine(HpBarEffect());
        }

        /// <summary>
        /// ����ĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator HpBarEffect()
        {
            while (hpEffectOriginal != hp)                      //�� ����e��q�������q
            {
                hpEffectOriginal --;                            //����
                imgHp.fillAmount = hpEffectOriginal / hpMax;    //��s���
                yield return new WaitForSeconds(0.01f);         //����
            }
        }
    }
}
