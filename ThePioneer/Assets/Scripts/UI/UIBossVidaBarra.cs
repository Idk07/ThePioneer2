using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossVidaBarra : MonoBehaviour
{
    public GameObject vidaBarraBoss;
    void Start()
    {   
        
        vidaBarraBoss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(BoosEntrada.instanceBossEntrada.muestraBarra == true) {
            ShowBarra();
        }
    }

    private void ShowBarra() {
        vidaBarraBoss.SetActive(true);
    }
}
