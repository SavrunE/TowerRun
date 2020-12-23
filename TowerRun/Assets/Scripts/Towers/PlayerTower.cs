using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human startHuman;
    [SerializeField] private Transform distanceChecker;
    [SerializeField] private float fixationMaxDistance;
    [SerializeField] private BoxCollider checkCollider;

    private Stack<Human> humans;
    private float displaceScale = 1.5f;
    private BoxCollider mainBox;

    private void Awake()
    {
        mainBox = GetComponent<BoxCollider>();
        humans = new Stack<Human>();
        Vector3 spawnPoint = transform.position;
        humans.Push(Instantiate(startHuman, spawnPoint, Quaternion.identity, transform));
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
                    }

                    DisplaceCheckers(humans.Count - 1);
                    collisionTower.Break();
                }
            }
        }
    }
    private void InsertHuman(Human collectedHuman)
    {
        SetHumanPosition(collectedHuman);
        humans.Push(collectedHuman);
    }
    private void SetHumanPosition(Human collectedHuman)
    {
        collectedHuman.transform.parent = transform;
        collectedHuman.transform.position = new Vector3(humans.Peek().transform.position.x, 
            humans.Peek().transform.position.y - displaceScale,
            humans.Peek().transform.position.z);
        collectedHuman.transform.localRotation = Quaternion.identity;
    }
    private void DisplaceCheckers(int towerCountCollected)
    {
        distanceChecker.position = new Vector3(humans.Peek().transform.position.x,
            humans.Peek().transform.position.y - displaceScale,
            humans.Peek().transform.position.z);

        checkCollider.center = distanceChecker.position;
    }
    public void OnSizeChange()
    {
        mainBox.transform.position = new Vector3(humans.Peek().transform.position.x,
            humans.Peek().transform.position.y - displaceScale,
            humans.Peek().transform.position.z);
    }
}
