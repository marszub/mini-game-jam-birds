using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private BGController bgController;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private Text_control scoreDisplay;
    private List<GameObject> birds = new List<GameObject>();
    private float lastBirdScore;
    private static float score;
    [SerializeField] private Transform spawnAreaBot;
    [SerializeField] private Transform spawnAreaTop;
    [SerializeField] private Transform joinAreaBot;
    [SerializeField] private Transform joinAreaTop;
    [SerializeField] private float joinSpeed;
    private bool playing = true;

    public static float Score { get => score; }

    void Start()
    {
        lastBirdScore = -1;
        score = 0;
    }

    void Update()
    {
        if (!playing)
            return;

        List<GameObject> toSpawn = spawner.GetBirds(lastBirdScore, score);
        if (toSpawn.Count > 0)
        {
            lastBirdScore = score;
            foreach (GameObject prefab in toSpawn)
                Spawn(prefab);
        }

        if(birds.Count == 0)
        {
            levelChanger.FadeToScene("EndScreen");
            playing = false;
            return;
        }
        GameObject frontBird = birds[0];
        foreach(GameObject bird in birds)
        {
            if(bird.GetComponent<BirdBehaviour>().state == BirdBehaviour.State.Fly && frontBird.transform.position.x < bird.transform.position.x)
                frontBird = bird;
        }
        float speed = frontBird.GetComponent<BirdBehaviour>().OptimalSpeed;
        List<GameObject> toDestroy = new List<GameObject>();
        foreach (GameObject bird in birds)
        {
            BirdBehaviour birdBehaviour = bird.GetComponent<BirdBehaviour>();
            birdBehaviour.Speed = speed;
            if (birdBehaviour.IsDead())
            {
                toDestroy.Add(bird);
            }
        }

        foreach (GameObject bird in toDestroy)
        {
            birds.Remove(bird);
            StartCoroutine(bird.GetComponent<BirdBehaviour>().Die());
        }

        bgController.Set_Velocity(speed/4);
        score += Time.deltaTime * speed;
        scoreDisplay.Set_Distance(score);
    }

    private void Spawn(GameObject bird)
    {
        Vector2 position = new Vector3(spawnAreaTop.position.x, Random.Range(spawnAreaBot.position.y, spawnAreaTop.position.y), 0);
        GameObject spawned = Instantiate(bird, position, Quaternion.identity);
        float joinPlace = Random.Range(joinAreaBot.position.x, joinAreaTop.position.x);
        StartCoroutine(spawned.GetComponent<BirdBehaviour>().Join(-joinSpeed, joinPlace));
        birds.Add(spawned);
    }
}
