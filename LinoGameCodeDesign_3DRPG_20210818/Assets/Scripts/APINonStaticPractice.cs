using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APINonStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer sprSquare;
    public Camera camMain;
    public SpriteRenderer sprHum;
    public Transform hum1;
    public Rigidbody2D hum2;

    private void Start()
    {
        print("��v���`��" + cam.depth);
        print("��ιϤ����C��" + sprSquare.color);

        camMain.backgroundColor = Random.ColorHSV();
        sprHum.flipY = true;
    }

    private void Update()
    {
        hum1.Rotate(0, 0, 3);

        hum2.AddForce(new Vector2(0, 12));
    }
}
