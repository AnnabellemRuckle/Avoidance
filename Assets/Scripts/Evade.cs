using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 100f;
    float maxPredictionTime = 1f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        Vector3 directionToTarget = target.transform.position - character.transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float targetSpeed = target.GetComponent<Kinematic>().linearVelocity.magnitude;
        float predictionTime;
        if (targetSpeed <= distanceToTarget / maxPredictionTime)
        {
            predictionTime = maxPredictionTime;
        }
        else
        {
            predictionTime = distanceToTarget / targetSpeed;
        }

        Vector3 targetPosition = target.transform.position + target.GetComponent<Kinematic>().linearVelocity * predictionTime;
        result.linear = character.transform.position - targetPosition;
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
