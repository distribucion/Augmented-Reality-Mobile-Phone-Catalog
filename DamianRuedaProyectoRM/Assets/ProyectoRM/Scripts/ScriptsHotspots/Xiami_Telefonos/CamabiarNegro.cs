using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamabiarNegro : MonoBehaviour
{
    public Material materialNegro;

    public void CambiarColorMaterialNegro()
    {


        materialNegro.color = new Color32(0, 0, 0, 255);
    }
}
