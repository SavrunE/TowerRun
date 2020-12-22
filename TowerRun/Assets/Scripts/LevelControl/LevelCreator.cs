using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private Tower towerTemplate;
    [SerializeField] private int towersCount;

    private void Start()
    {
        GenerateLevel();
    }
    private void GenerateLevel()
    {
        float roadLenght = pathCreator.path.length;
        float distanceBetweenTowers = roadLenght / towersCount;

        float distanceTravelled = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < towersCount; i++)
        {
            distanceTravelled += distanceBetweenTowers;
            spawnPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

            Instantiate(towerTemplate, spawnPoint, Quaternion.identity);
        }
    }
}

