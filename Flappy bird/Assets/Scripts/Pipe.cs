using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;

    void Start()
    {
             
    }

    void Update()
    {
       transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
