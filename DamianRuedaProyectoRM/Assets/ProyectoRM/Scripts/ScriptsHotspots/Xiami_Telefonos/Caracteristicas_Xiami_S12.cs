using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caracteristicas_Xiami_S12 : MonoBehaviour
{
    public GameObject panelCaracteristicas;



    void Start()
    {
        panelCaracteristicas.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelCaracteristicas != null)
        {
            bool IsActive = panelCaracteristicas.activeSelf;
            panelCaracteristicas.SetActive(!IsActive);


        }
    }

}
