using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int humanInTowerRange;
    [SerializeField] private Human[] humansTemplates;

    private List<Human> humansInTower;

    private void Start()
    {
        humansInTower = new List<Human>();
        int humanInTowerCount = Random.Range(humanInTowerRange.x, humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human SpawnedHuman = humansTemplates[Random.Range(0, humansTemplates.Length)];

            humansInTower.Add(Instantiate(SpawnedHuman, spawnPoint, Quaternion.identity));

            //humansInTower[i].transform.position = new Vector3(0, humansInTower[i].transform.localPosition.y, 0);

            spawnPoint = humansInTower[i].FixationPoint.position;
        }
    }
}
