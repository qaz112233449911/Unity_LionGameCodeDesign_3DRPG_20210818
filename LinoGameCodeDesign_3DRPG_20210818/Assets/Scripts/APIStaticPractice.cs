using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R�A�ݩʻP��k API�Ұ�m��
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
   
    private void Start()
    {
        int count = Camera.allCameras.Length;
        print("�ثe�֦�" + count + "cameras");

        Vector2 Gravity = Physics2D.gravity;
        print("2D ���O : " + Physics2D.gravity);

        
        print("��P�v : " + Mathf.PI);
        
        Physics2D.gravity = new Vector2(0, -20);

        Time.timeScale = 0.5f;

        print("9.999 �h���p���I���G : " + Mathf.Round(9.999f));

        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b=new Vector3(22, 22, 22);
        print("a b ���I���Z�� : " + Vector3.Distance(a, b));

        Application.OpenURL("https://unity.com/");
    }


    private void Update()
    {
        
        print("�O�_��J���N�� : " + Input.anyKey);
        
        print("�ɶ� : " + Time.time);

        print("�O�_���U�ť��� : " + Input.GetKeyDown(KeyCode.Space));
    }
}
