using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    private List<GameObject> birds = new List<GameObject>();
    private float lastBirdScore;
    private float score;
    [SerializeField] private Transform spawnAreaBot;
    [SerializeField] private Transform spawnAreaTop;


    void Start()
    {
        lastBirdScore = 0;
        score = 0;
        List<GameObject> toSpawn = spawner.GetBirds(-1, 0);
        foreach (GameObject prefab in toSpawn)
        {
            Spawn(prefab);
        }
    }

    void Update()
    {
        // TODO: end when no birds
        GameObject frontBird = birds[0];
        foreach(GameObject bird in birds)
        {
            if(frontBird.transform.position.x < bird.transform.position.x)
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
            Destroy(bird);
        }


        score += Time.deltaTime * speed;


        List<GameObject> toSpawn = spawner.GetBirds(lastBirdScore, score);
        if(toSpawn.Count > 0)
        {
            lastBirdScore = score;
            foreach (GameObject prefab in toSpawn)
                Spawn(prefab);
        }
        
    }

    private void Spawn(GameObject bird)
    {
        Vector2 position = new Vector2(Random.Range(spawnAreaBot.position.x, spawnAreaTop.position.x), Random.Range(spawnAreaBot.position.y, spawnAreaTop.position.y));
        GameObject spawned = Instantiate(bird, position, Quaternion.identity);
        birds.Add(spawned);
    }
}
