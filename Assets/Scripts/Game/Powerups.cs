using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(clip,transform.position);

            if(player != null)
            {

                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedActive();
                        break;

                    case 2:
                        player.ShieldActive();
                        break;
                }
            }
            
            Destroy(this.gameObject);  
            
        }else if(other.tag == "Laser")
        {
            Destroy(this.gameObject);
        }
    }
}
