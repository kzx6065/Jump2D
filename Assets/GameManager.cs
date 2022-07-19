using EasyButtons;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Direction {Right, Left}
public class GameManager : MonoBehaviour
{
    //Coroutine myCoHandle; //코루틴 샘플
    //[Button]
    //void CoStart()
    //{
    //    print("CoStart");
    //    myCoHandle = StartCoroutine(MyCo());
    //}
    //[Button]
    //void CoStop()
    //{
    //    StopCoroutine(myCoHandle);
    //}

    //IEnumerator MyCo()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        print(Time.time);
    //    }
    //}

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
    public int bestScore;
    public TextMeshProUGUI scoreText;
    internal void SetNextLevel()
    {
        waitNextBlock = false;
        score += 100;
        //ui에 score 표시하기.
        scoreText.text = score.ToString();
    }
    public GameOverUI gameOverUI;
    internal void GameOver()
    {
        if(score > bestScore)
        {
            bestScore = score;
        }
        gameOverUI.ShowScore(score, bestScore);

        StopCo();
        gameState = GameStateType.GameOver;
    }
    public enum GameStateType {BeforePlay ,Play, GameOver}

    public GameStateType gameState = GameStateType.BeforePlay;

    private float initY = -3;
    private float level = 0;
    private float blockHeight = 1;
    public float offsetX = 5; //블럭이 생성되는 위치
    public float blockSpeed = 3;

    Coroutine spawnBlockCoHandle;
    
    private void Start()
    {
        spawnBlockCoHandle = StartCoroutine(SpawnBlockCo());
    }
    void StopCo()
    {
        StopCoroutine(spawnBlockCoHandle);
    }

    public TextMeshProUGUI countdownText;
    private IEnumerator SpawnBlockCo()
    {
        gameState = GameStateType.BeforePlay;
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false);

        scoreText.text = "";
        gameState = GameStateType.Play;
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
