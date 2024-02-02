using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Kinematic
{
    public enum SteeringType
    {
        Seek,
        Pursue,
        Evade,
        PathFollowing
    }

    public SteeringType steeringBehavior;
    Seek mySeekMoveType;
    Face mySeekRotateType;
    LookWhereGoing myFleeRotateType;
    Evade myEvadeMoveType;
    PathFollowing myPathFollowMoveType;

    public bool flee = false;

    //array of waypoints for the inspector
    public Transform[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        mySeekMoveType = new Seek();
        mySeekMoveType.character = this;
        mySeekMoveType.target = myTarget;
        mySeekMoveType.flee = flee;

        mySeekRotateType = new Face();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;

        myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
        myFleeRotateType.target = myTarget;

        myEvadeMoveType = new Evade();
        myEvadeMoveType.character = this;
        myEvadeMoveType.target = myTarget;

        myPathFollowMoveType = new PathFollowing();
        myPathFollowMoveType.character = this;
        myPathFollowMoveType.waypoints = waypoints; 
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        switch (steeringBehavior)
        {
            case SteeringType.Seek:
                steeringUpdate.linear = mySeekMoveType.getSteering().linear;
                steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : mySeekRotateType.getSteering().angular;
                break;

            case SteeringType.Pursue:
                if (mySeekMoveType != null)
                {
                    mySeekMoveType = new Pursue();
                    mySeekMoveType.character = this;
                    mySeekMoveType.target = myTarget;
                    mySeekMoveType.flee = flee;
                }

                steeringUpdate.linear = mySeekMoveType.getSteering().linear;
                steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : mySeekRotateType.getSteering().angular;
                break;

            case SteeringType.Evade:
                steeringUpdate.linear = myEvadeMoveType.getSteering().linear;
                steeringUpdate.angular = mySeekRotateType.getSteering().angular; 
                break;

            case SteeringType.PathFollowing:
                steeringUpdate.linear = myPathFollowMoveType.getSteering().linear;
                steeringUpdate.angular = mySeekRotateType.getSteering().angular; 
                break;

            default:
                Debug.LogError("NOOOOO");
                break;
        }

        base.Update();
    }
}
