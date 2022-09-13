using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang_Test : MonoBehaviour
{
    public float speed = 1f; // default speed 1 unit / second
    public float distance = 5f; // default distance 5 units
    public Transform boomerang; // the object you want to throw (assign from the scene)
    private float _distance; // the distance it moves
    private bool _back; // is it coming back

    public void Shoot()
    {
        _distance = 0; // resetting the distance
        _back = false; // resetting direction
        enabled = true; // enabling the component, so turning the Update call on
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        float travel = Time.deltaTime * speed;
        if (!_back)
        {
            boomerang.Translate(Vector2.right * travel); // moves object
            _distance += travel; // update distance
            _back = _distance >= distance; // goes back if distance reached
        }
        else
        {
            boomerang.Translate(Vector2.right * -travel); // moves object
            _distance -= travel; // update distance;
            enabled = _distance > 0; // turning off when done
        }
    }

    private void OnEnable()
    {
        boomerang.gameObject.SetActive(true); // activating the object
    }
    private void OnDisable()
    {
        boomerang.gameObject.SetActive(false);
    }
}
