using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == false)
            return;

        GameManager.instance.GameOver();
    }
}
