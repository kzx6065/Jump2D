using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Direction {Right, Left}
public class GameManager : MonoBehaviour
{
    static public GameManager instance;  //싱글톤 패턴, 타 클래스에서 접근 가능
    private void Awake() //생성되었을 때 가장 먼저 실행.
    {
        instance = this; //인스턴스 값에 자신 할당.
    }

    private void OnDestroy() //오브젝트가 파괴되었을 때 실행.
    {
        instance = null; //파괴되었을 때 null값 전환
    }
    // 블럭 왼쪽에서 스폰
    // 오른쪽에서 스폰
    // 반복

    public Block block;
    public Direction moveDirection = Direction.Right; //오른쪽으로 움직임 (왼쪽 생성)
    public bool waitNextBlock = true;

    public int score;
    public TextMeshProUGUI scoreText;
    internal void SetNextLevel()
    {
        waitNextBlock = false;
        score += 100;
        //ui에 score 표시하기.
        scoreText.text = score.ToString();
    }
    internal void GameOver()
    {
        throw new NotImplementedException();
    }

    private float initY = -3;
    private float level = 0;
    private float blockHeight = 1;
    public float offsetX = 5; //블럭이 생성되는 위치
    public float blockSpeed = 3;

    private IEnumerator Start()
    {
        scoreText.text = "";
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
        //레벨 증가할 때마다 카메라 올리기

        //플레이어 죽음(GameOver)

        //타이틀씬 Touch
        //3,2,1 게임 시작

        //최고 점수 기록 (앱 재시작에도 유지)
    }
}
