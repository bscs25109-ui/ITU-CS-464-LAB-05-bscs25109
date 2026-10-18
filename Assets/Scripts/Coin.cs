using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1; // points for collecting this coin
    [SerializeField] float spinSpeed = 90f;   // coin rotation speed


    void Update()
    {   // spin the coin around the vertical axis
        // deltaTime keeps the speed consistent across frame rates
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Coin touched by {other.name}");
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);  // ddd the coin's points
            Destroy(gameObject);    // remove the whole coin
        }
    }
}