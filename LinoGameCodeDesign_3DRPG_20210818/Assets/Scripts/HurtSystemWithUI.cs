using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LiangWei
{
    /// <summary>
    /// 包含介面的受傷系統
    /// 可以處理血條更新
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("要更新的血條")]
        public Image imgHp;

        /// <summary>
        /// 血條效果專用的扣血前血量
        /// </summary>
        private float hpEffectOriginal;

        //複寫父類別成員 override
        public override void Hurt(float damage)
        {
            hpEffectOriginal = hp;

            //該成員的父類別基底 父類別內的內容
            base.Hurt(damage);

            StartCoroutine(HpBarEffect());
        }

        /// <summary>
        /// 血條效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator HpBarEffect()
        {
            while (hpEffectOriginal != hp)                      //當 扣血前血量不等於血量
            {
                hpEffectOriginal --;                            //遞減
                imgHp.fillAmount = hpEffectOriginal / hpMax;    //更新血條
                yield return new WaitForSeconds(0.01f);         //等待
            }
        }
    }
}
