using UnityEngine;
using UnityEngine.AI;
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
       
        [Header("等待隨機秒速")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("走路隨機秒數")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        #endregion

        #region 欄位 : 私人
        [SerializeField]  //序列化欄位 : 顯示私人欄位
        private StateEnemy state;

        /// <summary>
        /// 是否等待狀態
        /// </summary>
        private bool isIdle;
        /// <summary>
        /// 是否走路狀態
        /// </summary>
        private bool isWalk;

        /// <summary>
        /// 隨機行走座標 : 透過 API 取得網格內可走到的位置
        /// </summary>
        private Vector3 v3RandomWalkFinal;

        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "走路開關";
        #endregion

        [Header("攻擊區域位移與尺寸")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;

        #region 繪製圖形
        private void OnDrawGizmos()
        {
            #region 攻擊範圍﹑追蹤範圍與隨機行走座標
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);
            
            Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);
            
            if (state == StateEnemy.Walk)
            {
                Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion

            #region 攻擊碰撞判定區域
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);

            //繪製方形，需要跟者角色旋轉時請使用 matrix 指定座標角度與尺寸
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #endregion

        #region 事件
        private Transform traPlayer;
        private string namePlayer = "主角";
        /// <summary>
        /// 隨機行走座標
        /// </summary>
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * rangeTrack + transform.position;
        }

        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();

            traPlayer = GameObject.Find(namePlayer).transform;

            nma.SetDestination(transform.position);                      //導覽器 一開始就先啟動
        }
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
                    Walk();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
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
        /// 等待 : 隨機秒數後進入走路狀態
        /// </summary>
        private void Idle()
        {
            if (playerInTrackRange) state = StateEnemy.Track;           //如果 玩家進入 追蹤範圍 就切為追蹤狀態

            #region 進入條件
            if (isIdle) return;

            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(Idleffect());
        }
        /// <summary>
        /// 等待效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator Idleffect()
        {
            float randoWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randoWait);

            state = StateEnemy.Walk;
            isIdle = false;
        }

        /// <summary>
        /// 走路 : 隨機秒數後進入等待狀態
        /// </summary>
        private void Walk()
        {
            if (playerInTrackRange) state = StateEnemy.Track;           //如果 玩家進入 追蹤範圍 就切為追蹤狀態

            nma.SetDestination(v3RandomWalkFinal);                                          //代理器.設定目的地(座標)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.05f);                   //走路動畫 - 離目的地距離大約 0.1 時走路

            #region 進入條件
            if (isWalk) return;
            isWalk = true;
            #endregion

            print("隨機座標:" + v3RandomWalk);

            NavMeshHit hit;                                                                 //導覽網格碰撞 - 儲存網格碰撞資訊 
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);    //導覽網格.取得座標(隨機座標,碰撞資訊,半徑,區域) - 網格內可行走的座標
            v3RandomWalkFinal = hit.position;                                               //最終座標 = 碰撞資訊 的 座標
            
            StartCoroutine(WalkEffect());
        }
        /// <summary>
        /// 走路效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator WalkEffect()
        {
            float randoWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randoWalk);

            state = StateEnemy.Idle;

            #region
            isWalk = false;
            #endregion
        }

        /// <summary>
        /// 玩家是否在追蹤範圍內， true 是，false 否
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }

        private bool isTrack;

        /// <summary>
        /// 追蹤玩家
        /// </summary>
        private void Track()
        {
            #region
            if (!isTrack)
            {
                StopAllCoroutines();
            }

            isTrack = true;
            #endregion

            nma.isStopped = false;                         //導覽器啟動
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            //距離小於等於攻擊 就進 攻擊狀態
            if (nma.remainingDistance <= rangeAttack) state = StateEnemy.Attack;
        }

        [Header("攻擊時間"), Range(0, 5)]
        public float timeAttack = 2.5f;

        private string parameterAttack = "攻擊觸發";
        private bool isAttack;

        /// <summary>
        /// 攻擊玩家
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                         //導覽器停止
            ani.SetBool(parameterIdleWalk, false);        //停止走路
            nma.SetDestination(traPlayer.position);

            if (nma.remainingDistance > rangeAttack) state = StateEnemy.Track;
            
            if (isAttack) return;
            ani.SetTrigger(parameterAttack);

            //物理 盒形碰撞(中心點﹑一半尺寸﹑角度﹑圖層)
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            if (hits.Length > 0) print("攻擊到的物件 : " + hits[0].name);

            isAttack = true;
        }
        #endregion
    }
}
