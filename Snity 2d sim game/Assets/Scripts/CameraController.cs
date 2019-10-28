using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;
    private float zoomNum =10f;


    // Update is called once per frame
    void Update()//camera controlling 
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

         if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1) * speed * Time.deltaTime;
        }

         if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position +=  new Vector3(1,0) * speed * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zoomNum -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomNum += 1;
        }
        GetComponent<Camera>().orthographicSize = zoomNum;
    }
}
