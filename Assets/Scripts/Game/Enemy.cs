using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float speed = 6;

    private Player player;

    [SerializeField]
    private Animator animator;

    private AudioSource audioSource;

    

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("Player is NULL");
        }

        if(animator == null)
        {
            Debug.LogError("Animator is NULL");
        }

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        float randomX = Random.Range(-8, 8);

        if (transform.position.y < -4)
        {
            transform.position = new Vector3(randomX, 5, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if(player != null)
            {
                player.Damage();
            }

            animator.SetTrigger("OnEnemyDeath");
            speed = 0;
            Destroy(this.gameObject, 2.8f);
            audioSource.Play();
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AddScore(10);
            }
            animator.SetTrigger("OnEnemyDeath");
            speed = 0;
            Destroy(this.gameObject, 2.8f);
            audioSource.Play();
        }
    }
}
