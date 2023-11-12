using MV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerActiveWeapon : MonoBehaviour
{
    public bool isPerformingActive;
    EnemyManager enemyManager;
    public static ManagerActiveWeapon instanceManagerWeapon;
    public GameObject nose;

    private void Start() {
        enemyManager = GetComponent<EnemyManager>();
        isPerformingActive = false;
        instanceManagerWeapon = this;
    }

    private void Update() {
        isPerformingActive = enemyManager.isPerformingAction;
        

    }
}
