using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public Transform SpawnZone;
    public GameObject[] ObjToSpawn;
    public GameObject GameOverObj;
    public GameObject ScoreObj;
    public static bool gameOver = false;
    private bool delay = true; // Заглушка

    float maxX;
    float minX;
    float maxY;
    float minY;

    void Start()
    {
        maxX = SpawnZone.position.x + 9;
        minX = SpawnZone.position.x - 9;
        maxY = SpawnZone.position.y + 4f;
        minY = SpawnZone.position.y - 4f;

        gameOver = false;
    }

    void FixedUpdate()
    {
        if (delay)
        {
            if (!gameOver) // Если игрок проиграл, то спавн прекращается
            {
                StartCoroutine(SpawnObjects());
                delay = false;

            }
        }
    }

    IEnumerator SpawnObjects() // Задержка
    {
        yield return new WaitForSeconds(0.4f);
        Vector2 spawnPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(ObjToSpawn[Random.Range(0, 3)], spawnPos, Quaternion.identity);
        delay = true;
    }
}