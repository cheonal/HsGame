using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUi : MonoBehaviour
{
    public RectTransform StateUi;
    public Text ManualMainText;
    public Text ManualText;
    public Text ManualManaText;
    public Text ManualDamageText;
    public void StateCall()
    {
        StateUi.anchoredPosition = Vector2.zero;
    }
    public void StateExit()
    {
        StateUi.anchoredPosition = Vector2.down * 2000;
    }
    public void ManualSkill1()
    {
        ManualMainText.text = "눈보라";
        ManualText.text = "주변 몬스터에게 피해를 준다!" + "\n"+ "안아파 보인다고" + "\n" + "가만히 있으면 큰코 다칠껄";
        ManualManaText.text = "정신력 소모:10";
        ManualDamageText.text = "10+" + "레벨당 데미지";
    }
}
