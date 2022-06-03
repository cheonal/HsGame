using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public RectTransform skillR;
    public RectTransform skillRGuageFront;
    public RectTransform skillRGuagesucces;
    float Guage;


    public void Awake()
    {

    }
    void Update()
    {
        SkillR();
    }
    void SkillR()
    {
       // if (GameObject.Find("MagicRstart").GetComponent<Weapon>().cast == true)
        //{
            if (Input.GetKeyDown("r"))
            {
                skillR.anchoredPosition = Vector3.up * 350;
            }
            if (Input.GetKey("r") && Guage <= 1f)
            {
                Guage += Time.deltaTime;
            }
            if (Guage > 1f)
            {
                skillRGuagesucces.anchoredPosition = Vector3.up * 350;
            }
            if (Input.GetKeyUp("r"))
            {
                skillR.anchoredPosition = Vector3.down * 1000;
                skillRGuagesucces.anchoredPosition = Vector3.down * 1000;
                Guage = 0;
            }

            skillRGuageFront.localScale = new Vector3(Guage * 100 / 100, 1, 1);
      //  }
       
    }
}


