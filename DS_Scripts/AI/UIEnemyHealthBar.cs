using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        public Slider slider;
        float timeUntilBarIsHidden = 0;

        private void wake()
        {
            slider = GetComponentInChildren<Slider>();
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            timeUntilBarIsHidden = 3;
        }

        public void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update()
        {
            if(slider != null)
            {
                timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

                if (timeUntilBarIsHidden <= 0)
                {
                    timeUntilBarIsHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else
                {
                    if (!slider.gameObject.activeInHierarchy)
                    {
                        slider.gameObject.SetActive(true);
                    }
                }

                if (slider.value <= 0)
                {
                    Destroy(slider.gameObject);
                }
            }
            
        }
    }
}

