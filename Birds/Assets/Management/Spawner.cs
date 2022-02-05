using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spawner")]
public class Spawner : ScriptableObject
{
    [SerializeField] private float scoreOne;
    [SerializeField] private float scoreMultiplier;
    [SerializeField] private GameObject[] birdPrefabs;
    public List<GameObject> GetBirds(float lastScore, float currentScore)
    {
        if(lastScore < 0) { 
            return new List<GameObject>() { GetRandBird(), GetRandBird(), GetRandBird() };
        }

        if(lastScore == 0)
        {
            if (currentScore < scoreOne)
                return new List<GameObject>();

            return new List<GameObject>() { GetRandBird() };
        }

        if(currentScore >= lastScore * scoreMultiplier * 2)
        {
            return new List<GameObject>() { GetRandBird() };
        }

        return new List<GameObject>();
    }

    private GameObject GetRandBird()
    {
        return birdPrefabs[Random.Range(0, birdPrefabs.Length)];
    }
}
