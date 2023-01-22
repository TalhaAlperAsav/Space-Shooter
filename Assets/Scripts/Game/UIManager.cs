using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Sprite[] livesArray;

    [SerializeField]
    private Image livesSprite;

    [SerializeField]
    private TextMeshProUGUI gameoverText;

    [SerializeField]
    private TextMeshProUGUI restartText;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score :" + 0;
        gameoverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score"+ playerScore.ToString();
    }


    public void UpdateLives(int currentLives)
    {
        livesSprite.sprite = livesArray[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }    
    }

    void GameOverSequence()
    {
        gameManager.GameOver();
        gameoverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        StartCoroutine(RestartTextFlicker());
    }


    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RestartTextFlicker()
    {
        while (true)
        {
            restartText.text = "Put a coin to try again !";
            yield return new WaitForSeconds(0.5f);
            restartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
