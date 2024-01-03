using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMAterialRandom : MonoBehaviour
{
    public Material material;



    public void RandomColorMaterial()
    {
        material.color = Random.ColorHSV();
    }

}
