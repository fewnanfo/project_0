using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterManager : MonoBehaviour
    {

        CharacterSoundFXManager characterSoundFXManager;
        [Header("Lock on transforms")]
        public Transform lockOnTransform;

        [Header("combat colliders")]
        public CriticalDamageCollider backStabCollider;
        public CriticalDamageCollider riposteCollider;

        [Header("Interaction")]
        

        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool canBeParried; 
        public bool isParrying;
        public bool isBlocking;
        public bool isInvulnerable;
        

        [Header("Movement Flags")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;
        [Header("Player Flag")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvulerable;

        //Damage will be inflicted during an ani event
        //used in backstab or riposte ani
        public int pendingCriticalDamage;

        private void Awake()
        {
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        }
    }

    
}


