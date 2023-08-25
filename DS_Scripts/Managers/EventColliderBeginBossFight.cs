using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;
        public GameObject bossbackGroundSound;
        public GameObject gamebackGroundSound;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                worldEventManager.ActivateBossFight();
                bossbackGroundSound.gameObject.SetActive(true);
                gamebackGroundSound.gameObject.SetActive(false);
            }
        }
    }

}

