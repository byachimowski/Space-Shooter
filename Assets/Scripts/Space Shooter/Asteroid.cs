using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed =-10f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager==null)
        {
            Debug.LogError("_spawnManager is NULL!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        

        if (other.tag == "Laser")
        {
            Instantiate(_explosion, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
                   
            Destroy(this.gameObject, .5f);

        }


    }



}
