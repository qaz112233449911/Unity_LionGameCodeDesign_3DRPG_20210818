using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace LiangWei.Enemy
{
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A : ���ݡN�����N�l�ܡN���ˡN���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ��� : ���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d�� : �l�ܻP����")]
        [Range(0, 7)]
        public float rangeAttack = 5;
        [Range(7, 20)]
        public float rangeTrack = 15;
       
        [Header("�����H����t")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3, 7);
        #endregion

        #region ��� : �p�H
        [SerializeField]  //�ǦC����� : ��ܨp�H���
        private StateEnemy state;

        /// <summary>
        /// �O�_���ݪ��A
        /// </summary>
        private bool isIdle;
        /// <summary>
        /// �O�_�������A
        /// </summary>
        private bool isWalk;

        /// <summary>
        /// �H���樫�y�� : �z�L API ���o���椺�i���쪺��m
        /// </summary>
        private Vector3 v3RandomWalkFinal;

        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "�����}��";
        #endregion

        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;

        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            #region �����d��N�l�ܽd��P�H���樫�y��
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

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);

            //ø�s��ΡA�ݭn��̨������ɽШϥ� matrix ���w�y�Ш��׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.localScale);

            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #endregion

        #region �ƥ�
        private Transform traPlayer;
        private string namePlayer = "�D��";
        /// <summary>
        /// �H���樫�y��
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

            nma.SetDestination(transform.position);                      //������ �@�}�l�N���Ұ�
        }
        private void Update()
        {
            StateManager();
        }
        #endregion

        #region ��k : �p�H
        /// <summary>
        /// ���A�޲z
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
        /// ���� : �H����ƫ�i�J�������A
        /// </summary>
        private void Idle()
        {
            if (playerInTrackRange) state = StateEnemy.Track;           //�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            #region �i�J����
            if (isIdle) return;

            isIdle = true;
            #endregion

            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(Idleffect());
        }
        /// <summary>
        /// ���ݮĪG
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
        /// ���� : �H����ƫ�i�J���ݪ��A
        /// </summary>
        private void Walk()
        {
            if (playerInTrackRange) state = StateEnemy.Track;           //�p�G ���a�i�J �l�ܽd�� �N�����l�ܪ��A

            nma.SetDestination(v3RandomWalkFinal);                                          //�N�z��.�]�w�ت��a(�y��)
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.05f);                   //�����ʵe - ���ت��a�Z���j�� 0.1 �ɨ���

            #region �i�J����
            if (isWalk) return;
            isWalk = true;
            #endregion

            print("�H���y��:" + v3RandomWalk);

            NavMeshHit hit;                                                                 //��������I�� - �x�s����I����T 
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);    //��������.���o�y��(�H���y��,�I����T,�b�|,�ϰ�) - ���椺�i�樫���y��
            v3RandomWalkFinal = hit.position;                                               //�̲׮y�� = �I����T �� �y��
            
            StartCoroutine(WalkEffect());
        }
        /// <summary>
        /// �����ĪG
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
        /// ���a�O�_�b�l�ܽd�򤺡A true �O�Afalse �_
        /// </summary>
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }

        private bool isTrack;

        /// <summary>
        /// �l�ܪ��a
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

            nma.isStopped = false;                         //�������Ұ�
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);

            //�Z���p�󵥩���� �N�i �������A
            if (nma.remainingDistance <= rangeAttack) state = StateEnemy.Attack;
        }

        [Header("�����ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;

        private string parameterAttack = "����Ĳ�o";
        private bool isAttack;

        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                         //����������
            ani.SetBool(parameterIdleWalk, false);        //�����
            nma.SetDestination(traPlayer.position);

            if (nma.remainingDistance > rangeAttack) state = StateEnemy.Track;
            
            if (isAttack) return;
            ani.SetTrigger(parameterAttack);

            //���z ���θI��(�����I�N�@�b�ؤo�N���סN�ϼh)
            Collider[] hits = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            if (hits.Length > 0) print("�����쪺���� : " + hits[0].name);

            isAttack = true;
        }
        #endregion
    }
}
