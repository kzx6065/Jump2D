using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 3;
 
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //블럭 이동 멈추기.
        if (collision.collider.CompareTag("Player") == false)
            return;

        enabled = false;

        //다음 블럭 나오게 하기.
        //점수 추가하기
    }
}
