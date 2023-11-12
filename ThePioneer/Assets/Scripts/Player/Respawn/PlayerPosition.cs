using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MV {
    public class PlayerPosition : MonoBehaviour {
        private GameRespawn gr;
        TestPlayerStats stats;

        private void Start() {
            gr = GameObject.FindGameObjectWithTag("GR").GetComponent<GameRespawn>();
            transform.position = gr.lastCheckPointPos;
            stats = gameObject.GetComponent<TestPlayerStats>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               
            }
            if(stats.currentHealth == 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }
}
