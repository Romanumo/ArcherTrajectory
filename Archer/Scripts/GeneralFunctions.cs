using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralFunctions
{
    /*        //float hDown = 10;
        float tTarget = Mathf.Sqrt(2 * heightDifference / g);
        float tRemain = t - tTarget;
        //s += heightDifference * (2 * (s / 2 * t) / (g * tRemain)) * multiplier;
    
        float tan = (s / heightUp);


    float sRemain = (heightDifference / s) * multiplier;
        s -= sRemain;
     */

    public static ProjectileInfo CalculateTrajectory(Vector3 archerPos, Vector3 targetPos, float multiplier, float multiplier2)
    {
        float g = Physics.gravity.magnitude;
        float heightDifference = (targetPos.y - archerPos.y);
        Vector3 distance = (archerPos - targetPos);
        distance.y = 0;
        float heightUp = 10;
        float s = distance.magnitude;
        float h = (heightDifference + heightUp);
        float t = Mathf.Sqrt(2 * h / g);
        s += (s / 2) * (heightDifference / h);
        float angle = Mathf.Atan((2 * h - g * g * t) / (2 * s));
        float speed = (s / Mathf.Cos(angle) * t);
        Debug.Log(g + " " + angle * Mathf.Rad2Deg);
        ProjectileInfo info = new ProjectileInfo(speed, angle * Mathf.Rad2Deg);
        return info;
    }
}
