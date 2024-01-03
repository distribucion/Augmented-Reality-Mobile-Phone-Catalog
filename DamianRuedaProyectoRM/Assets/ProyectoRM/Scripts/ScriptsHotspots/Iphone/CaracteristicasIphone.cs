using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracteristicasIphone : MonoBehaviour
{
    public GameObject panelCaracteristicasIphone;



    void Start()
    {
        panelCaracteristicasIphone.SetActive(false);
    }

    public void panelActivar()
    {
        if (panelCaracteristicasIphone != null)
        {
            bool IsActive = panelCaracteristicasIphone.activeSelf;
            panelCaracteristicasIphone.SetActive(!IsActive);


        }
    }
}
