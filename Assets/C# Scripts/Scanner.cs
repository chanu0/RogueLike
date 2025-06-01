using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float ScanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform NearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, targetLayer);
        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diffX = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 Mypos = transform.position;
            Vector3 targetpos = target.transform.position;
            float curDiff = Vector3.Distance(Mypos, targetpos);

            if(curDiff < diffX)
            {
                diffX = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
