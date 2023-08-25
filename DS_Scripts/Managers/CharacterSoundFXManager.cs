using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        CharacterManager characterManager;
        PlayerInventory playerInventory;
        AudioSource audioSource;
        InputHandler inputHandler;
        Rigidbody rigid;

      
      

        [Header("Taking Damage Sound")]
        public AudioClip[] takingDamageSounds;
        private List<AudioClip> potentialDamageSounds;
        private AudioClip lastDamageSoundPlayed;

        [Header("Weapon Slashes")]
        private List<AudioClip> potentialWeaponSlashes;
        private AudioClip lastWeaponSlashes;

      

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            characterManager = GetComponent<CharacterManager>();
            playerInventory = GetComponent<PlayerInventory>();
            
            
        }
     

        public virtual void PlayRandomDamageSoundFX()
        {
            potentialDamageSounds = new List<AudioClip>();

            foreach(var damageSound in takingDamageSounds)
            {
                if(damageSound != lastDamageSoundPlayed)
                {
                    potentialDamageSounds.Add(damageSound);
                }
            }

            int randomValue = Random.Range(0, potentialDamageSounds.Count);
            lastDamageSoundPlayed = takingDamageSounds[randomValue];
            audioSource.PlayOneShot(takingDamageSounds[randomValue], 0.4f);
        }

        public virtual void PlayRandomWeaponSlashes()
        {
            potentialWeaponSlashes = new List<AudioClip>();

            if (characterManager.isUsingRightHand)
            {
                foreach (var slashSound in playerInventory.rightWeapon.weaponSlashes)
                {
                    if(slashSound != lastWeaponSlashes)
                    {
                        potentialWeaponSlashes.Add(slashSound);
                    }
                }
                int randomValue = Random.Range(0, potentialWeaponSlashes.Count);
                lastWeaponSlashes = playerInventory.rightWeapon.weaponSlashes[randomValue];
                audioSource.PlayOneShot(playerInventory.rightWeapon.weaponSlashes[randomValue], 0.4f);
            }
            else if (characterManager.isUsingRightHand)
            {
                foreach (var slashSound in playerInventory.leftWeapon.weaponSlashes)
                {
                    if (slashSound != lastWeaponSlashes)
                    {
                        potentialWeaponSlashes.Add(slashSound);
                    }
                }
                int randomValue = Random.Range(0, potentialWeaponSlashes.Count);
                lastWeaponSlashes = playerInventory.leftWeapon.weaponSlashes[randomValue];
                audioSource.PlayOneShot(playerInventory.leftWeapon.weaponSlashes[randomValue], 0.4f);
            }
        }
      
        

    }

}

