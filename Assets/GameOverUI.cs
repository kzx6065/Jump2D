using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI NowScore;
    public TextMeshProUGUI BestScore;

    public void ShowScore(int currentScore, int bestScore)
    {
        gameObject.SetActive(true);
        NowScore.text = currentScore.ToString();
        BestScore.text = bestScore.ToString();
    }

    public void Retry()
    {
        // 처음부터 다시 시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
