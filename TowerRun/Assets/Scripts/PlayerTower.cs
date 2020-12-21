using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human startHuman;
    [SerializeField] private Transform distanceChecker;
    [SerializeField] private float fixationMaxDistance;
    [SerializeField] private BoxCollider checkCollider;

    private List<Human> humans;

    private void Awake()
    {
        humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        humans.Add(Instantiate(startHuman, spawnPoint, Quaternion.identity, transform));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            if (collisionTower != null)
            {
                List<Human> collectedHumans = collisionTower.CollectHuman(distanceChecker, fixationMaxDistance);

                if (collectedHumans != null)
                {
                    for (int i = collectedHumans.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHumans[i];
                        InsertHuman(insertHuman);
                        DisplaceCheckers(insertHuman);
                    }

                    collisionTower.Break();
                }
            }
        }
    }
    private void InsertHuman(Human collectedHuman)
    {
        humans.Insert(0, collectedHuman);
        SetHumanPosition(collectedHuman);
    }
    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }
    private void DisplaceCheckers(Human human)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = distanceChecker.position;
        distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
        distanceChecker.position = distanceCheckerNewPosition;
        checkCollider.center = distanceChecker.localPosition;

        Debug.Log(displaceScale);
        Debug.Log(distanceCheckerNewPosition);
        Debug.Log(distanceCheckerNewPosition.y);
        Debug.Log(distanceChecker.position);
        Debug.Log(checkCollider.center);
    }
}
