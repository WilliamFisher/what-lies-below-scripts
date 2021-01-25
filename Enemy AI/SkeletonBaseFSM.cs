using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeletonBaseFSM
{
    public abstract void OnStateEnter(Skeleton skeleton);
    public abstract void OnStateUpdate(Skeleton skeleton);
    public abstract void OnStateExit(Skeleton skeleton);
}
