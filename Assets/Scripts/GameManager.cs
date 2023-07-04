using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private List<GameObject> m_EnemyPrefabs;
    [SerializeField] private float m_XRange = 10.0f;
    [SerializeField] private float m_YRange = 20.0f;
    [SerializeField] private float m_MinSpawnTime = 1.0f;
    [SerializeField] private float m_MaxSpawnTime = 5.0f;
    [SerializeField] private bool m_Paused = false;
    [SerializeField] private bool m_GameOver = false;

    public bool Paused
    {
        get { return m_Paused; }
        set { m_Paused = value; }
    }

    public bool GameOver
    {
        get { return m_GameOver; }
        set { m_GameOver = value; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    public void Play()
    {
        m_GameOver = false;
        SpawnPlayer();
        StartCoroutine(SpawnEnemy());
    }

    public void SpawnPlayer()
    {
        m_Player = Instantiate(m_PlayerPrefab, new Vector2(0, -15), m_PlayerPrefab.transform.rotation);
        m_Player.name = "Player";
    }

    private IEnumerator SpawnEnemy()
    {
        while (m_GameOver != true && m_Player != null)
        {
            int index = Random.Range(0, m_EnemyPrefabs.Count);
            Vector2 spawnPosition = new Vector2(Random.Range(m_Player.transform.position.x - m_XRange, m_Player.transform.position.x + m_XRange), m_YRange);
            Instantiate(m_EnemyPrefabs[index], spawnPosition, Quaternion.Euler(0, 0, 180));
            yield return new WaitForSeconds(Random.Range(m_MinSpawnTime, m_MaxSpawnTime));
        }
    }
}
