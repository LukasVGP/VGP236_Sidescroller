using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private Vector3 nextPosition;

    void Start()
    {
        // Check if start and end points are assigned.
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start Point or End Point is not assigned for the Moving Platform!");
            return;
        }

        // Set the initial position and target.
        transform.position = startPoint.position;
        nextPosition = endPoint.position;
    }

    void Update()
    {
        // Move the platform towards the next target position.
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        // Check if the platform has reached the target position.
        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        {
            // Switch the target position.
            if (nextPosition == endPoint.position)
            {
                nextPosition = startPoint.position;
            }
            else
            {
                nextPosition = endPoint.position;
            }
        }
    }
}