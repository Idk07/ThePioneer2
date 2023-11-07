using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameRespawn gr;
    string player = "Player";
    string tagGameRespawn = "GR";

    private void Start() {
        gr = GameObject.FindGameObjectWithTag(tagGameRespawn).GetComponent<GameRespawn>();
        
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(player)) {
            gr.lastCheckPointPos = transform.position;
        }
        
    }
}
