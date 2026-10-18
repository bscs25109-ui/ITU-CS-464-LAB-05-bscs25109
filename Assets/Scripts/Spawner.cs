using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;  // coin prefab to spawn
    [SerializeField] GameObject redCoinPrefab; // red coin prefab
    [SerializeField] Transform player;      // player position used to avoid spawning too close
    [SerializeField] float interval = 1.5f;  // seconds between spawn attempts
    [SerializeField] int maxCoins = 5;      // maximum coins present at once
    [SerializeField] float area = 7f;       // spawn range on the X and Z axes
    bool redCoinSpawned;
    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);
    // wait one second, then keep trying to spawn coins
    void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;

        // spawn the coin upright as a child of the Spawner
        Instantiate(coinPrefab, pos, Quaternion.Euler(90f, 0f, 0f), transform);
    }
    public void SpawnRedCoin()
    {
        // Stop if a red coin has already been spawned
        if (redCoinSpawned) return;

        // Mark it as spawned so this cannot happen again
        redCoinSpawned = true;

        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);

        Vector3 pos = new Vector3(x, 0.5f, z);

        // Choose the opposite corner if it is too close
        if (Vector3.Distance(pos, player.position) < 2f)
        {
            pos.x = player.position.x >= 0f ? -area : area;
            pos.z = player.position.z >= 0f ? -area : area;
        }

        // Spawn the red coin separately from normal coins
        Instantiate(
            redCoinPrefab,
            pos,
            Quaternion.Euler(90f, 0f, 0f)
        );

        Debug.Log("Red coin spawned!");
    }
}
