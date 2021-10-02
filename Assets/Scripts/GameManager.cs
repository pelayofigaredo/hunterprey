using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Accion que hace a la unidad avanzar hacia su objetivo
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool spawn = true;
    public Action<bool> TogglePathRendering;
    public Action<bool> ToggleStatsRendering;
    public Action<bool> ToggleSensesRendering;

    public int preyNumber = 3;
    public int hunterNumber = 1;
    public Transform[] startPositions;
    public GameObject prey;
    public GameObject hunter;

    private List<Transform> preys = new List<Transform>();
    private List<Transform> hunters = new List<Transform>();


    private float fixedDeltaTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        this.fixedDeltaTime = Time.fixedDeltaTime;
        if (spawn)
        {
            Initialice();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetTime(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    private void Initialice()
    {
        ReadPreferences();
        int total = preyNumber + hunterNumber;
        if (startPositions.Length < total)
        {
            Debug.LogError("Insuficient startPositions to fit all units");
            return;
        }
        List<Transform> positionList = new List<Transform>(startPositions);
        for (int i = 0; i < hunterNumber; i++)
        {
            Transform position = positionList[UnityEngine.Random.Range(0, positionList.Count)];
            positionList.Remove(position);
            Transform h = Instantiate(hunter, position.position, position.rotation).transform;
            hunters.Add(h);
        }
        for (int i = 0; i < preyNumber; i++)
        {
            Transform position = positionList[UnityEngine.Random.Range(0, positionList.Count)];
            positionList.Remove(position);
            Transform p = Instantiate(prey, position.position, position.rotation).transform;
            preys.Add(p);
        }
    }

    private void ReadPreferences()
    {
        int nPrey = PlayerPrefs.GetInt("nPrey");
        if (nPrey > 0) { preyNumber = nPrey; }
        int nHunter = PlayerPrefs.GetInt("nHunter");
        if (nHunter > 0) { hunterNumber = nHunter; }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PreyKilled(Transform prey)
    {
        foreach (Transform h in hunters)
        {
            h.GetComponent<Unit>().TargetKilled(prey);
        }
    }
}
