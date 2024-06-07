using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;

public class GameManager : MonoBehaviour
{
    public GameObject Object;

    public float spawnRate = 2f; // 스폰
    private float nextSpawnTime = 0f;

    public GameObject OverSet; // UI
    public int firstSceneIndex = 0;

    public static GameManager manager;

    Text timeText; // 시간
    float time = 0;
    bool isGameOver = false;

    void Start()
    {
        OverSet.SetActive(false);
        manager = this;
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
    }
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        if (isGameOver) return;

        time += Time.deltaTime;
        timeText.text = SetTime((int)time);
    }

    void SpawnObject()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 5f);
        Instantiate(Object, spawnPosition, Quaternion.identity);
    }
    public void GameOver()
    {
        OverSet.SetActive(true);
        isGameOver = true;
    }
    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene(firstSceneIndex);
    }

    string SetTime(int time)
    {
        string min = (time / 60).ToString();

        if (int.Parse(min) < 10)
            min = "0" + min;

        string sec = (time % 60).ToString();

        if(int.Parse(sec) < 10)
            sec = "0" + sec;

        return min + ":" + sec;
        
    }
}
