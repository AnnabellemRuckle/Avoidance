using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior
{
    public Kinematic character;
    public GameObject target;
    public bool flee;

    public abstract SteeringOutput getSteering();
}
