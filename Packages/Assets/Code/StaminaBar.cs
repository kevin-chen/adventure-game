using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
   public void SetMaxStamina(float stamina) {
       slider.maxValue = stamina;
       slider.value = stamina;
       fill.color = gradient.Evaluate(1f);
   }
   
    public void SetStamina(float stamina) {
        slider.value = stamina;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
