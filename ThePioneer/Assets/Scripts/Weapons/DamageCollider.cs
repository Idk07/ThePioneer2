using MV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV { 
public class DamageCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 5;
    Collider damageCollider;

    private void Awake() {

        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        //EN TEORIA ESOS DE ABAJO DEBERIAN DE IR PERO SI LOS PONGO NO CAPTA LA COLISION, creo que es porque falta tambien
        //puso codigo en algo de las armas peeeero puede que no tenga mucho que ver pero de momento funciona :D
        //damageCollider.isTrigger = true;
        //damageCollider.enabled = false;

            print("Awake");
    }

    public void EnableDamageCollider() {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider() {
        damageCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider collision) {

            print("Weapon:TocoAlgo");
                if (collision.CompareTag("Enemy")) {
                    EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

                    print("Weapon:TocoAlEnemigo");

                    if (enemyStats != null) {
                     //el valor de damage se debe de sustituir por un script de CurrentWeaponDamage
                    enemyStats.Damage(damage);
                    print("Weapon:HizoDañoAlEnemigo");
                   

                    }
            
                }          
     }
}

}