using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntosdeExperiencia : MonoBehaviour
{
    public static PuntosdeExperiencia instancePuntosDeExperiencia;
    private int sumaExperiencia;
    public TextMeshProUGUI textMeshPro;
    void Start()
    {
        instancePuntosDeExperiencia = this;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = sumaExperiencia.ToString();
    }


    public void SumaDeExperiencia(int cantidad) {
        
        sumaExperiencia += cantidad;
    }
}
