using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei
{
    public class ThirdPersonController : MonoBehaviour
    {
        #region ��� Field
        //�x�s�C����ơA�Ҧp : ���ʳt�סB���D���׵���...
        //�`�Υ|�j���� : ��� int�B�B�I�� float�B�r�� string�B���L�� bool
        //���y�k : �׹��� ������� ���W�� (���w �w�]��) ����
        //�׹���:
        //1. ���} public   -  ���\��L���O�s�� - ��ܦb�ݩʭ��O - �ݭn�վ㪺��Ƴ]�w�����}
        //2. �p�H private  -  �T���L���O�s�� - ���æb�ݩʭ��O - �w�]��
        // Unuty�H�ݩʭ��O���D
        // ��_�w�]�ȫ�... > Reset
        //����ݩ� Attribute : ���U�����
        //����ݩʻy�k : [�ݩʦW��(�ݩʭ�)]
        //Header ���D �B Tooltip ���� : �ƹ����d�b���W�٤W�|��ܼu�X���� �B Range �d�� : �i�ϥΦb�ƭ�������ƤW, �Ҧp : int, float
        [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(1, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
        public float jump = 100f;

        [Header("�ˬd�a�����"), Tooltip("�ˬd����O�_�b�a�O�W")]
        public bool isGrounded;
        public Vector3 v3CheckGroundoffset;
        public float CheckGroundRadius = 0.2f;

        [Header("���D����")]
        public AudioClip jumpsound;
        [Header("���a����")]
        public AudioClip Landingsound;

        [Header("�ʵe�Ѽ�")]
        public string animatorParWalk = "�����}��";
        public string animatorParRun = "�]�B�}��";
        public string animatorParHurt = "���˶}��";
        public string animatorParDead = "���`�}��";
        public string animatorParJump = "���DĲ�o";
        public string animatorParIsGrounded = "�O�_�b�a�O�W";

        [Header("���V�t��"), Range(0, 50)]
        public float speedLookAt = 2;

        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;
        #region Unity �������
        /** �m��Unity �������
        //�C�� Color
        public Color color;
        public Color white = Color.white;       //�����C��
        public Color yellow = Color.yellow;
        public Color color1 = new Color(0.5f, 0.5f, 0);               //�ۭq�C�� RGB
        public Color color2 = new Color(0, 0.5f, 0.5f, 0.5f);         //�ۭq�C�� RGBA

        // �y�� Vector 2 - 4
        public Vector2 v2;
        public Vector2 v2Right = Vector2.right;
        public Vector2 v2Up = Vector2.up;
        public Vector2 v2One = Vector2.one;
        public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
        public Vector3 v3 = new Vector3(1, 2, 3);
        public Vector4 v4 = new Vector4(1, 2, 3, 4);

        // ���� �C�|��� enum
        public KeyCode key;
        public KeyCode move = KeyCode.W;
        public KeyCode jump = KeyCode.Space;

        //�C����������G������w�w�]��
        public AudioClip sound;      //���� MP3�Aogg�Awav
        public VideoClip video;      //�v�� MP4
        public Sprite sprite;        //�Ϥ� png�Ajpeg - ���䴩 gif
        public Texture2D texture2D;  //2D �Ϥ� png�A jpeg
        public Material material;    //����y

        //���� Component : �ݩʭ��O�W�i���|��
        public Transform tra;
        public Animation aniOld;
        public Animator aniNew;
        public Light lig;
        public Camera cam;

        //���L�C
        //1. ��ĳ���n�ϥΦ��W�� 2.�ϥιL�ɪ�API
        /**/
        #endregion

        #endregion

        #region �ݩ� Property
        /**�ݩʽm��
        // �x�s��ơA�P���ۦP
        // �t���b�� : �i�H�]�w�s���v�� Get Set
        // �ݩʻy�k : �׹��� ������� �ݩʦW�� { ��; �s; }
        public int readAndWrite { get; set; }
        // �߿W�ݩ� : �u����o get
        public int read { get; }
        // �߿W�ݩ� : �z�L get �]�w�w�]�ȡA����r return ���Ǧ^��
        public int readValue 
        {
            get
            {
                return 77;
            }
        }
        // �߼g�ݩ� : �T��A�����n�� get
        // public int write { set; }
        // value �����O���w����
        private int _hp;
        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }
        */
        //C# 7.0 �s���l �i�H�ϥ� Lambda =>�B��l
        //�y�k get => { �{���϶� } - ���i�ٲ��j�A��
        
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        /// <summary>
        /// �H�����q
        /// </summary>
        private float volumeRandom { get => Random.Range(0.7f, 1.5f); }
        #endregion

        #region ��k Method
        //�w�q�P��@�������{�����϶��B�\��
        //��k�y�k : �׹��� �Ǧ^������� ��k�W�� (�Ѽ�1�A ...�Ѽ�N) { �{���϶� }
        //�`�ζǦ^���� : �L�Ǧ^ void - ����k�S���Ǧ^���
        //�榡�� : Ctrl + K D
        //�ۭq��k :
        //�ۭq��k�ݭn�Q�I�s�~�|�����k�����{��
        //�W���C�⬰�H���� - �S���Q�I�s
        //�W���C�⬰�G���� - ���Q�I�s
        /*�m�ߤ�k
        private void Test()
        {
            print("�ڬO�ۭq��k~");
            print("�m�߽m��~");
        }

        private int ReturnJump() 
        {
            return 999;
        }

        //�Ѽƻy�k : ������� �ѼƦW��
        //���w�]�Ȫ��Ѽƥi�H����J�޼ơA��񦡰Ѽ�
        private void Skill(int damage,string effect = "�ǹЯS��",string sound = "�ǹǹ�")
        {
            print("�Ѽƪ��� - �ˮ`�� : " + damage);
            print("�Ѽƪ��� - �ޯ�S�� : "+ effect);
            print("�Ѽƪ��� - ���� : " + sound);
        }
        /*���~ : ��񦡰ѼƨS���b()�k��
        private void ErrorSkill(string effect = "�ǹЯS��",int damage)
        {

        }

        //��Ӳ� : ���ϥΰѼ�
        //���C���@�P�X�R��
        private void Skill100()
        {
            print("�ˮ`�� : " + 100);
            print("�ޯ�S��");
        }
        private void Skill150()
        {
            print("�ˮ`�� : " + 150);
            print("�ޯ�S��");
        }
        private void Skill200()
        {
            print("�ˮ`�� : " + 200);
            print("�ޯ�S��");
        }

        //BMI = �魫 / ���� * ���� (����)
        //�D���n���ܭ��n
        /// <summary>
        /// �p�� BMI ��k
        /// </summary>
        /// <param name="weight">�魫�A��쬰����</param>
        /// <param name="height">�����A��쬰����</param>
        /// <param name="name">�W�١A���q�̦W��</param>
        /// <returns></returns>
        private float BMI(float weight, float height, string name = "����")
        {
            print(name + " �� BMI ");

            return weight / (height * height);
        }
        */
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="speedMove">���ʳt��</param>
        private void Move(float speedMove)
        {
            //�Ш��� Animator �ݩ� Apply Root Motion : �Ŀ�ɨϥΰʵe�첾��T
            //����.�[�t�� = �T���V�q - �[�t�ץΨӱ������T�Ӷb�V���B�ʳt��
            //�e��*��J��*���ʳt��
            //�ϥΫe�ᥪ�k�b�V�B�ʨåB�O���쥻���a�ߤޤO
            //Vector3.forward �@�ɮy�� �� �e�� (����)
            //transform.forward ������ �� �e�� (�ϰ�)
            rig.velocity =
                transform.forward * MoveInput("Vertical") * speedMove +
                transform.right * MoveInput("Horizontal") * speedMove +
                Vector3.up * rig.velocity.y;
        }

        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="axisName">�n���o���b�V�W��</param>
        /// <returns>���ʫ����</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }

        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        /// <returns>�O�_�I��a�O</returns>
        private bool CheckGround()
        {
            //���z.�л\�y��(�����I�A�b�|�A�ϼh)
            Collider[] hits = Physics.OverlapSphere(transform.position
                + transform.right * v3CheckGroundoffset.x
                + transform.up * v3CheckGroundoffset.y
                + transform.forward * v3CheckGroundoffset.z
                , CheckGroundRadius, 1 << 3);

            //print("�y��I�쪺�Ĥ@�Ӫ��� : " + hits[0].name);

            if (!isGrounded && hits.Length > 0) aud.PlayOneShot(Landingsound, volumeRandom);            
            isGrounded = hits.Length > 0;

            //�Ǧ^ �I���}�C�ƶq > 0 - �u�n�I����w�ϼh����N�N��b�a���W
            return hits.Length > 0;
        }

        private void Jump()
        {
            //print("�O�_�b�a���W : " + CheckGround());

            //�åB &&
            //�p�G �b�a���W �åB ���U�ť��� �N ���D

            if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            {
                //����.�K�[���O(�����󪺤W�� * ���D)
                rig.AddForce(transform.up * jump);

                aud.PlayOneShot(jumpsound, volumeRandom);
            }
        }

        private void UpdateAnimation()
        {

            /** �m�ߩI�s��k
            //�w�����G :
            //���U�e�Ϋ�� �N���L�ȳ]�� true
            //�S������ �N���L�ȳ]�� false
            //Input
            //if (��ܱ���)
            //!=�B== ����B��l (��ܱ���)
            //�m��
            //if (Input.GetAxis("Vertical") != 0)
            //ani.SetBool(animatorParWalk, true);
            //else
            //ani.SetBool(animatorParWalk, false);
            */

            ani.SetBool(animatorParWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            //�]�w�O�_�b�a�O�W  �ʵe�Ѽ�
            ani.SetBool(animatorParIsGrounded, isGrounded);
            //�p�G ���U ���D�� �N �]�w���DĲ�o�Ѽ�
            //�P�_�� �u���@��ԭz(�u���@�Ӥ���) �i�H�ٲ� �j�A��
            if (keyJump) ani.SetTrigger(animatorParJump);
        }


        /// <summary>
        /// ���V�e�� : ���V��v���e���m
        /// </summary>
        private void LookAtForward()
        {
            //�����b�V ������� �� �j�� 0.1 �N�B�z ���V
            if (Mathf.Abs(MoveInput("Vertical")) > 0.1f)
            {
                //���o�e�訤�� = �|��.���V����(�e��y�� - �����y��)
                Quaternion angle = Quaternion.LookRotation(thirdPersonGamera.posForward - transform.position);
                //�����󪺨��� = �|��.�t��
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            }
        }
        #endregion

        public GameObject playerObject;

        /// <summary>
        /// ��v�����O
        /// </summary>
        private ThirdPersonGamera thirdPersonGamera;

        #region �ƥ� Event
        // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start ���� Console Main
        // �}�l�ƥ� : �C���}�l�ɰ���@�� - �B�z��l�ơB���o��Ƶ���
        private void Start()
        {
            #endregion

            //���o���󪺤覡
            //1. �������W��.���o����(����(��������))��@ ��������
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2. ���}���C������.���o����<�x��>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3. ���o����<�x��>();
            //���O�i�H�ϥ��~�����O(�����O)�������A���}�ΫO�@ ���B�ݩʻP��k
            ani = GetComponent<Animator>();

            //��v�����O = �z�L�����M�䪫��<�x��>();
            //FindObjectOfType ���n��bUpdate ���ϥη|�y���j�q�į�t��
            thirdPersonGamera = FindObjectOfType<ThirdPersonGamera>();
        }

        private void Update()
        #region
        {
            Jump();
            UpdateAnimation();
            LookAtForward();
        }

        //�T�w��s�ƥ�: �T�w0.02�����@�� - 50FPS
        //�B�z���z�欰�A�Ҧp:Rigidbody API
        private void FixedUpdate()
        {
            Move(speed);
        }

        //ø�s�ϥܨƥ�
        //�bUnity Editor ��ø�s�ϥܻ��U�}�o�A�o����|�۰�����
        private void OnDrawGizmos()
        {
            // 1 . ���w�C��
            // 2 . ø�s�ϧ�
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            
            //transform �P���}���b�P���h�� Transform����
            Gizmos.DrawSphere(
                transform.position
                + transform.right * v3CheckGroundoffset.x
                + transform.up * v3CheckGroundoffset.y
                + transform.forward * v3CheckGroundoffset.z
                , CheckGroundRadius);
        }
        #endregion
    }
}


