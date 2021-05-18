using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab=null, _tripleShotPrefab=null;
    private float _canFire = -1;
    [SerializeField]
    private float _fireRate =.5f;
    [SerializeField]
    private int _lives =3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isPowerBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _LeftPlayerDamage , _RightPlayerDamage ;


    // Start is called before the first frame update
    void Start()
    {
        // set position to (Y=0, X=0, Z=0)
        transform.position = new Vector3(0, 0, 0);
        _playerShield.SetActive(false);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("The _spawnManager = null!!!");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.LogError("The _uiManager = null!!!");

        

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
       

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire )
        {
            _canFire = Time.time + _fireRate;
           if (_isTripleShotActive ==true)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
               Instantiate(_laserPrefab, transform.position + new Vector3(0, .8f, 0), Quaternion.identity);

            }
        }


    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
       // Debug.Log("Triple Shot Aactive");
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
      //  Debug.Log("Triple Shot Inactive");
    }

    public void PowerBoostActive()
    {
        _isPowerBoostActive = true;
        _speed = 8.5f;
       // Debug.Log("Speed Boost Collected");
        StartCoroutine(PowerBoostEndRoutine());

    }

    IEnumerator PowerBoostEndRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 3.5f;
        _isPowerBoostActive = false;
        //Debug.Log("Speed Boost Over");
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        _playerShield.SetActive(true);
       // Debug.Log(" Shields Collected");
       // StartCoroutine(ShieldPowerDownRoutine());
    }

    /* IEnumerator ShieldPowerDownRoutine()
     {
         yield return new WaitForSeconds(5.0f);
         _isShieldActive = false;
         _playerShield.SetActive(false);
         Debug.Log("Shields are off");

     }

     */

    public void Damage()
    {
        if(_isShieldActive==true)
        {
            _isShieldActive = false;
            _playerShield.SetActive(false);
           // Debug.Log("Shields are off");
            return;
        }

         _lives--;
        if(_lives==2)
        {
            _LeftPlayerDamage.SetActive(true);
        }

        if (_lives == 1)
        {
            _RightPlayerDamage.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);
       
        if (_lives < 1)
        {
            
            
            Destroy(gameObject);
            _spawnManager.OnPlayerDeath();
            

        }
    }

   void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }


        if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
