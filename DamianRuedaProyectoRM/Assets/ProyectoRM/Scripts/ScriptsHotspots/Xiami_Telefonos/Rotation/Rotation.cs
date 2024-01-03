using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 10;
    private string sumbu;
    private bool putar;
  

    // Update is called once per frame
    void Update()
    {
        if (putar) 
        {
            if (sumbu.ToUpper().Equals("X")) 
            {
                transform.Rotate(Vector3.right * speed);

            }

            
            if (sumbu.ToUpper().Equals("Y"))
                transform.Rotate(Vector3.up * speed);
            if (sumbu.ToUpper().Equals("Z"))
                transform.Rotate(Vector3.forward * speed);
        }
        
    }
    public void RotateCube(string sumbu) 
    {
        this.sumbu = sumbu;
        putar = true; 

    }

    public void StopRotatio() 
    {
        putar = false;
        
    }
}
