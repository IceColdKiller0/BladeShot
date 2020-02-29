using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
   private void Start()
    {
        transform.position = new Vector3(-10.0f, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.updateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawn();
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(1.3f, 0, 0), transform.rotation);
            }
            _canFire = Time.time + _fireRate;
        }
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.up * _speed * 2f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.left * _speed * 2f  * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.left * _speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.x < -11.0f)
        {
            transform.position = new Vector3(-11.0f, transform.position.y, 0);
        }

        if (transform.position.y > 5.7f)
        {
            transform.position = new Vector3(transform.position.x, -5.7f, 0);
        }

        if (transform.position.y < -5.7f)
        {
            transform.position = new Vector3(transform.position.x, 5.7f, 0);
        }
    }

    public void Damage()
    {
       // lives = lives - 1;
        lives--;
        // lives -= 1;

        _uiManager.updateLives(lives);

        if (lives < 1)
        {
            _gameManager.gameOver = true;
            _uiManager.showTitle();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        isSpeedBoostActive = false;
    }
}
