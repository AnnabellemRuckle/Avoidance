using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : SteeringBehavior
{
    public Kinematic character;
    public Transform[] waypoints;
    float maxAcceleration = 100f;

    int currentWaypointIndex = 0;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        if (waypoints.Length == 0)
        {
            return result;
        }

        Vector3 direction = waypoints[currentWaypointIndex].position - character.transform.position;
        float distance = direction.magnitude;

        if (distance < 1.0f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        Seek seek = new Seek();
        seek.character = character;
        seek.target = waypoints[currentWaypointIndex].gameObject;
        seek.flee = false;

        result.linear = seek.getSteering().linear;
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0f;

        return result;
    }
}
