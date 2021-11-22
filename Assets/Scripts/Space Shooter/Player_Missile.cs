using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player_Missile : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private GameObject _noTarget;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _angleChangingSpeed;
    [SerializeField]
    private float _moveSpeed;




    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

       // transform.Translate((Vector3.up) * _speed * Time.deltaTime);

        if (transform.position.x >= 10.5 || transform.position.x < -11.5|| transform.position.y >= 8 || transform.position.y <= -5.5)
        {
            DestroyMissilePrefab();
        }

        if(FindClosestByTag("Enemy")==null)
        {
            _target = _noTarget.transform;
        }

        else
        {
            _target = FindClosestByTag("Enemy").transform;
        }
    }


    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)_target.position - _rigidbody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rigidbody.angularVelocity = -_angleChangingSpeed * rotateAmount;
        _rigidbody.velocity = transform.up * _moveSpeed;
    }

    GameObject FindClosestByTag(string Tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(Tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance<distance)
            {
                closest = go;
                distance = curDistance;
            }
            
        }
        return closest;
    }

    private void DestroyMissilePrefab()
    {
        Destroy(gameObject);
    }
}
