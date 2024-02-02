using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arriver : Kinematic
{
    public enum SteeringType
    {
        Face,
        Arrive,
        Wander,
        Separation,
        CollisionAvoidance
    }

    public SteeringType steeringBehavior;
    Face myFaceMoveType;
    Arrive myArriveMoveType;
    Wander myWanderMoveType;
    Separation mySeparationMoveType;
    Align myRotateType;
    CollisionAvoidance myCollisionAvoidanceMoveType;

    GameObject wanderTarget;

    void Start()
    {
        myFaceMoveType = new Face();
        myFaceMoveType.character = this;
        myFaceMoveType.target = myTarget;

        myArriveMoveType = new Arrive();
        myArriveMoveType.character = this;
        myArriveMoveType.target = myTarget;

        myWanderMoveType = new Wander();
        myWanderMoveType.character = this;

        mySeparationMoveType = new Separation();
        mySeparationMoveType.character = this;
        mySeparationMoveType.targets = GetSeparationTargets();

        myRotateType = new Align();
        myRotateType.character = this;
        myRotateType.target = myTarget;

        myCollisionAvoidanceMoveType = new CollisionAvoidance();
        myCollisionAvoidanceMoveType.character = this;

        //setting the initial wander target
        myWanderMoveType.character.myTarget = GetRandomWanderTarget();
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        switch (steeringBehavior)
        {
            case SteeringType.Face:
                steeringUpdate.linear = myFaceMoveType.getSteering().linear;
                break;

            case SteeringType.Arrive:
                steeringUpdate.linear = myArriveMoveType.getSteering().linear;
                break;

            case SteeringType.Wander:
                steeringUpdate.linear = myWanderMoveType.getSteering().linear;
                break;

            case SteeringType.Separation:
                steeringUpdate.linear = mySeparationMoveType.getSteering().linear;
                break;

            case SteeringType.CollisionAvoidance:
                steeringUpdate.linear = myCollisionAvoidanceMoveType.getSteering().linear;
                break;

            default:
                Debug.LogError("NOOOOOOOOO");
                break;
        }

        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }

    private Kinematic[] GetSeparationTargets()
    {
        return GameObject.FindObjectsOfType<Kinematic>();
    }

    private GameObject GetRandomWanderTarget()
    {
        GameObject[] wanderTargets = GameObject.FindGameObjectsWithTag("WanderTarget");

        if (wanderTargets.Length > 0)
        {
            return wanderTargets[Random.Range(0, wanderTargets.Length)];
        }

        return null;
    }
}
