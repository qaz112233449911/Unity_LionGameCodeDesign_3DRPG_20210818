using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LiangWei
{
    /// <summary>
    /// 攻擊系統
    /// 玩家攻擊按鍵監聽
    /// 攻擊區域、攻擊力與造成傷害
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        #region 欄位 : 公開
        [Header("攻擊力"), Range(0, 500)]
        public float attack = 20;
        [Header("攻擊冷卻時間"), Range(0, 5)]
        public float timeAttack = 2.3f;
        [Header("延遲傳送傷害時間"), Range(0, 3)]
        public float delaySendDamage = 0.2f;
        [Header("攻擊區域尺寸與位移")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("攻擊動畫參數")]
        public string parameterAttack = "攻擊圖層觸發";
        public string parameterWalk = "走路開關";        
        [Header("攻擊事件")]
        public UnityEvent onAttack;
        [Header("攻擊圖層遮色片")]
        public AvatarMask maskAttack;
        #endregion

        #region 欄位 : 私人
        private Animator ani;
        private bool isAttack;
        #endregion

        #region 屬性 : 私人
        private bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }
        #endregion

        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            Attack();
        }
        #endregion

        #region 繪製圖形
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.2f, 0.3f);
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
        }
        #endregion

        #region 方法 : 私人
        /// <summary>
        /// 攻擊
        /// </summary>
        private void Attack()
        {
            #region 攻擊圖層遮色片處理
            bool isWalk = ani.GetBool(parameterWalk);

            //左腳、右腳、左右腳 IK 與根部
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, !isWalk);
            maskAttack.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, !isWalk);

            #endregion

            if (keyAttack && !isAttack)
            {
                
                onAttack.Invoke();
                isAttack = true;
                ani.SetTrigger(parameterAttack);
                StartCoroutine(DelayHit());
            }
        }

        private IEnumerator DelayHit()
        {
            yield return new WaitForSeconds(delaySendDamage);

            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 7);

            if (hits.Length > 0) hits[0].GetComponent<HurtSystem>().Hurt(attack);

            float waitToNextAttack = timeAttack - delaySendDamage;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }
        #endregion
    }
}
