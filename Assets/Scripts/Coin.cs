using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum CoinType
    {
        Normal,
        Red,
        Blue
    }

    // Choose the coin type in the Inspector
    [SerializeField] CoinType coinType = CoinType.Normal;

    [SerializeField] int value = 1; // points for collecting this coin
    [SerializeField] float spinSpeed = 90f;   // coin rotation speed

    // reference used by the red coin to spawn blue coins
    Spawner spawner;

    bool collected;  // stop the same coin being collected twice

    public void SetSpawner(Spawner coinSpawner)
    {
        spawner = coinSpawner;
    }
    void Update()
    {   // spin the coin around the vertical axis
        // deltaTime keeps the speed consistent across frame rates
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only react to the player and only once
        if (collected || !other.CompareTag("Player")) return;

        // Check the red coin has a Spawner before collecting it
        if (coinType == CoinType.Red && spawner == null)
        {
            Debug.LogError("Red coin is missing its Spawner reference.");
            return;
        }

        collected = true;

        switch (coinType)
        {
            case CoinType.Normal:
                // Normal coins increase the main score
                GameManager.Instance.AddScore(value);
                break;

            case CoinType.Red:
                // Red coin creates the blue coins around the player
                spawner.SpawnBlueCoins();
                break;

            case CoinType.Blue:
                // Add one to the bonus counter
                GameManager.Instance.AddBonusCoin();
                break;
        }

        // Remove the collected coin
        Destroy(gameObject);
    }
}