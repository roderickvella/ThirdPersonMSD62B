using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float initialForce;
    public float lifeTime;
    private float lifeTimer = 0f;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //add the initial force to the rigidbody (attached to this grenade)
        GetComponent<Rigidbody>().AddRelativeForce(0, 0, initialForce);
    }

    // Update is called once per frame
    void Update()
    {
        //update the timer
        lifeTimer += Time.deltaTime;

        if(lifeTimer >= lifeTime)
        {
            Explode(); //destroy projectile if time is up
        }
    }

    private void Explode()
    {
        print("Explode");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); //spawn the explosion where the grenade is
        Destroy(this.gameObject); //destroy the grenade

    }
}
