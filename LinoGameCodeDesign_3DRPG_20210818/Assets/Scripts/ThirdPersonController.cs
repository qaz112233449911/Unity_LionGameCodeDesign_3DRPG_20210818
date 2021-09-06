using System.Collections;
using System.Collections.Generic;
using UnityEngine;       //�ޥ� Unity API(�ܮw-��ƻP�\��)
using UnityEngine.Video;  //�ޥ� �v�� API

//�׹���  ���O             ���O�W��  :  �~�����O
//MonoBehaviour : Unity �����O �A �n���b����W�@�w�n�~��
//�~�ӫ�|�ɦ������O������
//�b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
//�`�Φ��� : ��� Field�B�ݩ� Property(�ܼ�)�B��k Method�B�ƥ� Event
/// <summary>
/// LiangWei 2021.09.06
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>
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
    #region Unity �������
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
    #endregion

    #endregion

    #region �ݩ� Property

    #endregion

    #region ��k Method

    #endregion

    #region �ƥ� Event

    #endregion
}
