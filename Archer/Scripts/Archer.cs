using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Transform target;
    public GameObject missile;

    public float projectileSpeed = 1;
    public float reloadDuration = 1;
    public float rotationSpeed = 3;

    bool isReloaded = true;

    void Update()
    {
        LookAtTarget();

        if (isReloaded)
            Shoot();
    }

    IEnumerator Reload()
    {
        isReloaded = false;
        yield return new WaitForSeconds(reloadDuration);
        isReloaded = true;
    }

    void Shoot()
    {
        StartCoroutine(Reload());

        //Creation and assigning of a missile
        ProjectileInfo info = CalculateTrajectory(this.transform.position, target.transform.position);
        GameObject projectile = GameObject.Instantiate(missile, this.transform.position, new Quaternion());
        projectile.transform.rotation = Quaternion.Euler(info.angle, this.transform.localEulerAngles.y, 0);
        projectile.AddComponent<Projectile>().AssignBullet(info.speed * projectileSpeed, 0.041f / (projectileSpeed * projectileSpeed));
    }

    //Main formula, which calculates speed and angle of a projectory based on positions of target and the archer
    ProjectileInfo CalculateTrajectory(Vector3 archerPos, Vector3 targetPos)
    {
        float g = Physics.gravity.magnitude;
        Vector3 distance = (archerPos - targetPos);
        float heightDifference = distance.y;
        float heightUp = 10;
        distance.y = 0;

        float s = distance.magnitude;
        float h = (heightDifference + heightUp);
        float t = Mathf.Sqrt(2 * h / g);

        float angle = Mathf.Atan((2 * h - g * g * t) / (2 * s));
        float speed = (s / Mathf.Cos(angle) * t);

        ProjectileInfo info = new ProjectileInfo(speed, angle * Mathf.Rad2Deg);
        return info;
    }


    //Look at target with only one dimension
    void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}

public class ProjectileInfo
{
    public float speed;
    public float angle;

    public ProjectileInfo(float speed, float angle)
    {
        this.speed = speed;
        this.angle = angle;
    }
}