using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //for other scripts access this GameManager
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bonusText;
    [SerializeField] GameObject winPanel;
    [SerializeField] Spawner spawner;

    int score;
    int redCoinScore;
    int bonusCoins;

    const int bonusTarget = 50;

    bool redCoinTriggered;
    bool bonusStarted;
    bool hasWon;
    void Awake() => Instance = this;
    void Start()
    {
        // Make sure the game starts normally
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        // Start with the bonus counter hidden
        bonusText.text = $"BonusCoins: 0/{bonusTarget}";
        bonusText.gameObject.SetActive(false);
        // Pick one random number from 1 to 10
        redCoinScore = Random.Range(1, 11);

        // Show the chosen number so it is easy to test
        Debug.Log($"Red coin will spawn at score: {redCoinScore}");

        UpdateUI();
    }

    public void AddScore(int amount)
    {

        if (hasWon) return;

        // Keep the normal score from going above the target
        score = Mathf.Min(score + amount, targetScore);
        UpdateUI();
        //if (score >= targetScore) Win();
        // Winning is temporarily disabled while adding the bonus tasks

        if (!redCoinTriggered && score >= redCoinScore)
        {
            redCoinTriggered = true;
            spawner.SpawnRedCoin();
        }
        CheckWin();
    }
    public void ShowBonusCounter()
    {
        if (bonusStarted) return;

        bonusStarted = true;

        // Show the counter when the blue coins spawn
        bonusText.text = $"BonusCoins: {bonusCoins}/{bonusTarget}";
        bonusText.gameObject.SetActive(true);
    }

    public void AddBonusCoin()
    {
        // Only count coins while the bonus is active
        if (!bonusStarted || hasWon || bonusCoins >= bonusTarget) return;

        bonusCoins++;

        // Update the bonus text after each blue coin
        bonusText.text = $"BonusCoins: {bonusCoins}/{bonusTarget}";

        CheckWin();
    }
    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

   
    void CheckWin()
    {
        // Both targets must be completed before winning
        if (!hasWon && score >= targetScore && bonusCoins >= bonusTarget)
        {
            hasWon = true;
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}