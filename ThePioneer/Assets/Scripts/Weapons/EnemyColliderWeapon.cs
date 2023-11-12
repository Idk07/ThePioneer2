using MV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderWeapon : MonoBehaviour {
    // Start is called before the first frame update
    public int damage = 20;
    Collider damageCollider;

    public EnemyManager script;

    private void Awake() {

        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);

        damageCollider.isTrigger = true;

    }




    public void EnableDamageTrigger() {
        damageCollider.isTrigger = true;
    }

    public void DisableDamageTrigger() {
        damageCollider.isTrigger = false;
    }

    private void Update() {
     
        if (script.isPerformingAction == false) {
            DisableDamageTrigger();

        }
        if (script.isPerformingAction == true) {
            EnableDamageTrigger();


        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (script.isPerformingAction == false) {
            return;
        }
      

     
        if (collision.CompareTag("Player")) {
            TestPlayerStats playerStats = collision.GetComponent<TestPlayerStats>();



            //print("Weapon:TocoAlEnemigo");

            if (playerStats != null) {
                //el valor de damage se debe de sustituir por un script de CurrentWeaponDamage
                playerStats.Damage(damage);
                //  print("Weapon:HizoDañoAlPlayer");


            }

        }
    }
}