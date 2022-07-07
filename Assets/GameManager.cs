using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {Right, Left}
public class GameManager : MonoBehaviour
{
    // 블럭 왼쪽에서 스폰
    // 오른쪽에서 스폰
    // 반복

    public Block block;
    public Direction moveDirection = Direction.Right; //오른쪽으로 움직임 (왼쪽 생성)
    public bool waitNextBlock = true;
    private float initY = -3;
    private float level = 0;
    private float blockHeight = 1;
    public float offsetX = 5; //블럭이 생성되는 위치
    public float blockSpeed = 3;

    private IEnumerator Start()
    {
        while (true)
        {
            waitNextBlock = true;

            var newBlock = Instantiate(block);

            float regenX;
            if (moveDirection == Direction.Right)
            {
                regenX = -offsetX;
                newBlock.speed = blockSpeed;
            }
            else
            {
                regenX = offsetX;
                newBlock.speed = -blockSpeed;
            }

            float regenY = initY + level * blockHeight;
            newBlock.transform.position = new Vector2(regenX, regenY);

            while (waitNextBlock)
            {
                yield return null;
            }
            level++;
            moveDirection = moveDirection == Direction.Right ? Direction.Left : Direction.Right;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) waitNextBlock = false;   
    }
}
