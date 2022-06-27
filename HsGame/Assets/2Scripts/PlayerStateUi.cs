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
        ManualMainText.text = "������";
        ManualText.text = "�ֺ� ���Ϳ��� ���ظ� �ش�!" + "\n"+ "�Ⱦ��� ���δٰ�" + "\n" + "������ ������ ū�� ��ĥ��";
        ManualManaText.text = "���ŷ� �Ҹ�:10";
        ManualDamageText.text = "10+" + "������ ������";
    }
}
