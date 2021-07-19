using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=4f;
    private Player _player;
    [SerializeField]
    private GameObject enemyLaserPrefab;
    private float _canFire = -1;
    private float _fireRate = 3f;

    private bool _enemyIsDead = false;

    private Collider _collider;
    private AudioSource _audioSource;

    private Animator _anim;
    
        // Start is called before the first frame update
    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player==null)
        {
            Debug.Log("_player is NULL!!!");

        }
        _anim = GetComponent<Animator>();
        if(_anim==null)
        {
            Debug.LogError("_anim is NULL!!!");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Enemy _audioSource is NULL!!!");
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f && _player != null && _enemyIsDead == false)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 7f, 0);
        }

        if (transform.position.y < -10f )// clean up after player death
        {
            Destroy(gameObject);
           
        }


        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(1f, 3f);

            _canFire = Time.time + _fireRate;

            Instantiate(enemyLaserPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
          
        if (other.tag== "Player")
        {                   
            other.transform.GetComponent<Player>().Damage();
            //_speed = 1;
            _anim.SetTrigger("Enemy_Destroyed");
            
            DestroyEnemy();
        }
        
        if (other.tag == "Laser")
        {
            
            Destroy(other.gameObject);
           
            if (_player !=null)
            {
               _player.AddScore(10);
            
                DestroyEnemy();
            }
        }

       if (other.tag == "Enemy_Laser")
        {
            Destroy(other.gameObject);
            DestroyEnemy();
        }
    }

   
    private void DestroyEnemy()
    {
        _anim.SetTrigger("Enemy_Destroyed");
        _audioSource.Play(); // the selected "Explosion" sound clip
        this.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(DecelSpeed());
        Destroy(this.gameObject, 2.4f);
        _enemyIsDead = true;
       
    }

    IEnumerator DecelSpeed()
    {
        while (_speed > 0)
        {
            _speed = _speed-1f;
            yield return new WaitForSeconds(.5f);
        }
    }

}
