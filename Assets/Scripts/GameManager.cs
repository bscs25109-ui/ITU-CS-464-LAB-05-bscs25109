using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //for other scripts access this GameManager
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;
    int score;
    [SerializeField] Spawner spawner;
    int redCoinScore;
    bool redCoinTriggered;
    void Awake() => Instance = this;
    void Start()
    {
        // Make sure the game starts normally
        Time.timeScale = 1f;
        winPanel.SetActive(false);

        // Pick one random number from 1 to 10
        redCoinScore = Random.Range(1, 11);

        // Show the chosen number so it is easy to test
        Debug.Log($"Red coin will spawn at score: {redCoinScore}");

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        //if (score >= targetScore) Win();
        // Winning is temporarily disabled while adding the bonus tasks

        if (!redCoinTriggered && score >= redCoinScore)
        {
            redCoinTriggered = true;
            spawner.SpawnRedCoin();
        }
    }

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

    //void Win() { winPanel.SetActive(true); Time.timeScale = 0f; }

    public void Restart()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}