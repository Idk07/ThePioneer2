using MV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoosEntrada : MonoBehaviour
{
    public GameObject puerta;
    public Transform target;
    Collider colliderVentana;

 

    private void Start() {
        colliderVentana = GetComponent<Collider>();
        colliderVentana.isTrigger = true;
        
    }

    private void Update() {
      
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
           // print("Hola");
           
            puerta.transform.position = Vector3.Lerp(puerta.transform.position, target.position, 0.80f );
            

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
         //   print("Hola2");

            StartCoroutine("wait");

        }
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(2f);
        colliderVentana.isTrigger = false;
      

    }
}
