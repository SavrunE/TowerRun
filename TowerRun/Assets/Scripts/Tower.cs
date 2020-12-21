using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int humanInTowerRange;
    [SerializeField] private Human[] humansTemplates;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;

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

            humansInTower.Add(Instantiate(SpawnedHuman, spawnPoint, Quaternion.identity, transform));

            //humansInTower[i].transform.position = new Vector3(0, humansInTower[i].transform.localPosition.y, 0);

            spawnPoint = humansInTower[i].FixationPoint.position;
        }
    }
    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < humansInTower.Count; i++)
        {
            float distanceBetweenPoints = CheckDistanceY(distanceChecker, humansInTower[i].FixationPoint.transform);

            if (distanceBetweenPoints < fixationMaxDistance)
            {
                List<Human> collectedHumans = humansInTower.GetRange(0, i + 1);
                humansInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }
        return null;
    }
    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }

    public void Break()
    {
        foreach (var human in humansTemplates)
        {
            Destroy(gameObject);
            //if (human.TryGetComponent(out Rigidbody rigidbody))
            //{
            //    rigidbody.isKinematic = false;
            //    //rigidbody.useGravity = true;

            //    rigidbody.AddExplosionForce(explosionForce, human.transform.position, explosionRadius);
            //}
        }
    }
}
