using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;
    float gravityDivider;

    Vector3 gravityAcceleration;
    CharacterController controller;

    public void AssignBullet(float speed, float gravityDivider)
    {
        this.speed = speed;
        this.gravityDivider = gravityDivider;
        StartCoroutine(SelfDestruct());
    }

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        gravityAcceleration += Physics.gravity * Time.deltaTime;
        controller.Move((this.transform.forward * speed) * Time.deltaTime);
        controller.Move(gravityAcceleration * Time.deltaTime / gravityDivider);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Debug.Log("Do something");
            Destroy(this.gameObject);
        }
    }
}
