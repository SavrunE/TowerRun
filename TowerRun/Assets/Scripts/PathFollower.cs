using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PathCreator pathCreator;

    private Rigidbody body;
    private float distanceTravelled;
    private void Start()
    {
        body = GetComponent<Rigidbody>();

        body.MovePosition(pathCreator.path.GetPointAtDistance(distanceTravelled));
    }
    private void Update()
    {
        distanceTravelled += Time.deltaTime * speed;

        Vector3 nextPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Loop);
        nextPoint.y = transform.position.y;

        transform.LookAt(nextPoint);

        body.MovePosition(nextPoint);
    }
}
