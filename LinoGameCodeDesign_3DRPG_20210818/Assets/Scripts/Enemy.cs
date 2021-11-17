using UnityEngine;
using System.Collections;

namespace LiangWei.Enemy
{
    /// <summary>
    /// 敵人行為
    /// 敵人狀態 : 等待﹑走路﹑追蹤﹑受傷﹑死亡
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region 欄位 : 公開
        [Header("移動速度"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("攻擊力"), Range(0, 200)]
        public float attack = 35;
        [Header("範圍 : 追蹤與攻擊")]
        [Range(0, 7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
        #endregion

        #region 欄位 : 私人
        [SerializeField]  //序列化欄位 : 顯示私人欄位
        private StateEnemy state;
        #endregion

        #region 繪製圖形
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

        }
        #endregion

        #region 事件
        private void Update()
        {
            StateManager();
        }
        #endregion

        #region 方法 : 私人
        /// <summary>
        /// 狀態管理
        /// </summary>
        private void StateManager()
        {
            switch (state)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Walk:
                    break;
                case StateEnemy.Track:
                    break;
                case StateEnemy.Attack:
                    break;
                case StateEnemy.Hurt:
                    break;
                case StateEnemy.Dead:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 是否等待狀態
        /// </summary>
        private bool isIdle;

        [Header("等待隨機秒速")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);

        /// <summary>
        /// 等待 : 隨機秒數後進走路狀態
        /// </summary>
        private void Idle()
        {
            #region 進入條件
            if (isIdle) return;

            isIdle = true;
            #endregion

            print("等待");
            StartCoroutine(Idleffect());
        }
        private IEnumerator Idleffect()
        {
            float randoWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randoWait);

            state = StateEnemy.Walk;
        }
        #endregion
    }
}
