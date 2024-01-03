using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMaterial : MonoBehaviour
{
    public Material material;



    public void RandomColorMaterial()
    {
        material.color = Random.ColorHSV();
    }



   
}
