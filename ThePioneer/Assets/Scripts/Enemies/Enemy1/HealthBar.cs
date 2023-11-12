using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; 
    public Camera cam;

    private void Start() {
        slider = GetComponent<Slider>();
        cam = Camera.main;
        
    }

    private void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void SetMaxHealth(int maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void SetCurrentHealth(int currentHealth) {
        slider.value = currentHealth;
    }

   

}
