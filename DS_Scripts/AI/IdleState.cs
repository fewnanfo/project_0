using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class IdleState : State
    {
        public PursueTargetState PursueTargetState;

        public LayerMask detectionLayer;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position,
                enemyManager.detectionRadius, detectionLayer);

                for (int i = 0; i < colliders.Length; i++)
                {
                    CharacterStatsManager characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                    if (characterStats != null)
                    {
                        //check for team id

                        Vector3 targetDirection = characterStats.transform.position - transform.position;
                        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viewableAngle > enemyManager.minimumDetectionAngle
                        && viewableAngle < enemyManager.maximumDetectionAngle)
                        {
                            enemyManager.currentTarget = characterStats;
                            return PursueTargetState;
                        }
                    }
                }
            #endregion

            #region Handle Switching To Next Target
            if (enemyManager.currentTarget != null)
            {

            }
            else
            {
                return this;
            }
            
            // look for a potential target
            //switch
            return this;
            #endregion
        }
    }
}


