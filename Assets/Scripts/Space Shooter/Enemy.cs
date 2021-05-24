using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=4f;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private bool TestDestroy=false;
    private bool _enemyIsDead = false;

    private Collider _collider;


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

       


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <-5.5f && _player !=null && _enemyIsDead==false)
        {
            transform.position = new Vector3(Random.Range(-10f,10f), 7f, 0);
        }

        if(TestDestroy == true)
        {
            DestroyEnemy();// used to setup IEnumerator DecelSpeed() decel rates
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
            }
            DestroyEnemy();
        }
    
    }

    private void DestroyEnemy()
    {
        _anim.SetTrigger("Enemy_Destroyed");
        this.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(DecelSpeed());
        Destroy(this.gameObject, 2.4f);
        _enemyIsDead = true;
       
    }

    IEnumerator DecelSpeed()
    {
        while (_speed > 0)
        {
            _speed = _speed-.01f;
            yield return new WaitForSeconds(.5f);
        }
    }

}
