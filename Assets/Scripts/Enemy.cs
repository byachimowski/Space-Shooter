using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed=4;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(Random.Range(-10f,10f), 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
          
        if (other.tag== "Player")
        {                   
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }


    }

}
