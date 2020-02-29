using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    public void StartSpawn()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        { 
            Instantiate(enemyShipPrefab, new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, -90f));
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 2);
            Instantiate(powerups[randomPowerup], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
