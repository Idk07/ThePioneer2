using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV {
    //Por si teniamos mas acciones
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    public class EnemyAttackAction : EnemyAction {
        public int attackScore = 3;
        public float recoveryTime = 0f;

        public float maximunAttackAngle = 270;
        public float minimunAttackAngle = -270;

        public float minimunDistanceNeededToAttack = 0.1f;
        public float maximunDistanceNeededToAttack = 20f;
    }
}