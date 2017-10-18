using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public Vector3 direction;  

    public float speed;

    public void Fire(Transform enemy)
    {
        target = enemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Vector3 direction = target.position - transform.position;
        //float distanceThisFrame = speed * Time.deltaTime;
        //if (direction.magnitude <= distanceThisFrame)
        //{
        //    TargetHit();
        //    return;
        //}
        //transform.Translate(direction.normalized * distanceThisFrame,Space.World);
        Vector3 velocity = direction.normalized * speed;
        transform.position += velocity * Time.deltaTime;
    }
    void TargetHit()
    {
        Debug.Log("Hit!");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ground" || col.tag == "Platform")
        {
            Debug.Log("Hit Ground");
            Destroy(gameObject);
        }
    }
}
