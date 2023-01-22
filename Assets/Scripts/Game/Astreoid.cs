using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Astreoid : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;

    public GameObject explosionAnim;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
            Destroy(other.gameObject);
            spawnManager.Spawnings();
        }
    }
}
