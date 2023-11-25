using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position), 1f);
        }     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
