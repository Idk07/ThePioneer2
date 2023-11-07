using MV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 20;
    Collider damageCollider;

    private void Awake() {

        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);

        damageCollider.isTrigger = true;
        //EN TEORIA ESOS DE ABAJO DEBERIAN DE IR PERO SI LOS PONGO NO CAPTA LA COLISION, creo que es porque falta tambien
        //puso codigo en algo de las armas peeeero puede que no tenga mucho que ver pero de momento funciona :D
        //damageCollider.isTrigger = true;
        //damageCollider.enabled = false;

        //print("Awake");
    }

   
    

    public void EnableDamageTrigger() {
        damageCollider.isTrigger = true;
    }

    public void DisableDamageTrigger() {
        damageCollider.isTrigger = false;
    }

    private void Update() {
      
        if (EnemyManager.instanceEnemyManager.isPerformingAction == false) {
            DisableDamageTrigger();
            
        }
        if (EnemyManager.instanceEnemyManager.isPerformingAction == true) {
            EnableDamageTrigger();
           
           
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if(EnemyManager.instanceEnemyManager.isPerformingAction == false) {
            return;
        }

        print("WeaponEnemy:TocoAlgo");
        if (collision.CompareTag("Player")) {
            TestPlayerStats playerStats = collision.GetComponent<TestPlayerStats>();
         
            

            //print("Weapon:TocoAlEnemigo");

            if (playerStats != null) {
                //el valor de damage se debe de sustituir por un script de CurrentWeaponDamage
                playerStats.Damage(damage);
                print("Weapon:HizoDañoAlPlayer");


            }

        }
    }
}
