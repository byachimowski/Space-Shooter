using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
   
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
    private int _score =0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _LeftPlayerDamage , _RightPlayerDamage ;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioClip _powerUpSoundClip;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosionPrefab;
    
    private float _horizontalInput = 0;
    private float _verticalInput = 0;
    
    private Animator _anim;

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

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Player C# _audioSource is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("_anim is NULL!!!");
        }

       

    }

    // Update is called once per frame
    void Update()
    {
       

        CalculateMovement();
       
        ///////////////////////////////////// FIRE LASERS ////////////////////////////////////////////////////
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

            _audioSource.clip = _laserSoundClip;//  laserSoundClip will be played when we call _audioSource.Play()
            _audioSource.Play(); // the selected sound clip
        }


    }
   
   
    
    
    
    
    /// ////////////////////////////////////// TRIPLE SHOT POWERUP ////////////////////////////////////////////
    
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        _audioSource.clip = _powerUpSoundClip;//  PowerUp Sound Clip will be played when we call _audioSource.Play()
        _audioSource.Play(); // the selected sound clip
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
      //  Debug.Log("Triple Shot Inactive");
    }



   
   ///////////////////////////////////////// POWER BOOST POWERUP /////////////////////////////////////////////////
   
    public void PowerBoostActive()
    {
        _isPowerBoostActive = true;
        _audioSource.clip = _powerUpSoundClip;//  PowerUp Sound Clip will be played when we call _audioSource.Play()
        _audioSource.Play(); // the selected sound clip
        _speed = 8.0f;
      
        StartCoroutine(PowerBoostEndRoutine());

    }

    IEnumerator PowerBoostEndRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 4f;
        _isPowerBoostActive = false;
        //Debug.Log("Speed Boost Over");
    }


    
     /// ///////////////////////////////////  SHIELDS POWERUP /////////////////////////////////////////////////////
    
    public void ShieldsActive()
    {
        _isShieldActive = true;
        _audioSource.clip = _powerUpSoundClip;//  PowerUp Sound Clip will be played when we call _audioSource.Play()
        _audioSource.Play(); // the selected sound clip
        _playerShield.SetActive(true);
      
       StartCoroutine(ShieldPowerDownRoutine());
    }

     IEnumerator ShieldPowerDownRoutine()
     {
         yield return new WaitForSeconds(5.0f);
         _isShieldActive = false;
         _playerShield.SetActive(false);


    }

   
    
    /// //////////////////////////////////// DAMAGE UPDATE /////////////////////////////////////////////////////
    
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
            _uiManager.UpdateLives(_lives);
        }

        if (_lives == 1)
        {
            _RightPlayerDamage.SetActive(true);
            _uiManager.UpdateLives(_lives);
        }

       
       
        if ((_lives < 1) && ( _lives > -2))/// -2 ACCOUNT FOR DOUBLE LASER HIT
        {
            _uiManager.UpdateLives(_lives);
            Instantiate(_explosionPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            _spawnManager.OnPlayerDeath();
             Destroy(gameObject);

        }
    }

    
    /////////////////////////////////////// CALULATE MOVEMENT /////////////////////////////////////////////////
    void CalculateMovement()
    {

       
           
        

        ////////////////////////////////////// PLAYER MOVEMENT ///////////////////////////////////////////////////
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * _horizontalInput * _speed  * Time.deltaTime);
        transform.Translate(Vector3.up * _verticalInput * _speed * Time.deltaTime);

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

    
    /// ////////////////////////////////// UPDATE SCORE ////////////////////////////////////////////////////////
      public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
