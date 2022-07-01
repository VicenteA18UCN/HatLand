using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject sli;
    private bool isDead;
    // Start is called before the first frame update

    void Start()
    {
        if(!isDead)
        {
            sli.SetActive(true);
        }
    }
    void Update()
    {
        slider.value = BossScript.hp;

        if (slider.value == 0)
        {
            isDead = true;
        }

        if(isDead)
        {
            sli.SetActive(false);
            isDead = false;
        }
    }

}
