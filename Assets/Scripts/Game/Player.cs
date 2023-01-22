using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Player attributes
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private int score;

    [SerializeField]
    private GameObject rightWing;

    [SerializeField]
    private GameObject leftWing;

    [SerializeField]
    private AudioClip laserSound;
    
    private AudioSource audioSource;

    // Lazer attributes
    public GameObject laserPrefab;
    private float fireRate = 0.15f;
    private float nextFire = 0f;

    private SpawnManager spawnManager;

    // Power-ups
    private bool isTripleShotActive = false;
    public GameObject tripleshotPrefab;

    private bool isSpeedActive = false;
    private int speedMultiplier = 2;

    [SerializeField]
    private bool isShieldActive = false;
    [SerializeField]
    private GameObject shieldVisualizer;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2, 0);

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //Finding the spawn manager
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = laserSound;

        if(spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }


        if (uiManager== null)
        {
            Debug.LogError("UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        shootLaser();

    }

    // Method for Player Movement
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
       
        transform.Translate(direction * _speed * Time.deltaTime);

        // To limit the movement
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f,0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    // Method for to Spawn (shoot) the lasers.
    void shootLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            if (isTripleShotActive == true)
            {
                Instantiate(tripleshotPrefab, transform.position, Quaternion.identity);
            }
            else{
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }

            audioSource.Play();
        }

        
    }

    public void Damage()
    {

        if (isShieldActive == true)
        {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }

        lives -= 1;

        if (lives == 2)
        {
            rightWing.SetActive(true);
        }else if(lives == 1)
        {
            leftWing.SetActive(true);
        }


        uiManager.UpdateLives(lives);

        if (lives == 0)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    // Tripleshot Power-Up Methods
    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());    
    }

    public void SpeedActive()
    {
        isSpeedActive = true;
        _speed *= speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        isTripleShotActive = false; 
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(3);
        isSpeedActive = false;
        _speed /= speedMultiplier;
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }
}   
