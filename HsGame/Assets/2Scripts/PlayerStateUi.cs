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
        ManualMainText.text = "스노우 크리스탈";
        ManualText.text = "주변 몬스터에게 피해를 준다!" + "\n"+ "눈보라 주변 적에게 지속 피해" + "\n" + "여기로 유인해";
        ManualManaText.text = "정신력 소모:20";
        ManualDamageText.text = "20+" + "레벨당 데미지";
    }
    public void ManualSkill2()
    {
        ManualMainText.text = "파이어 볼";
        ManualText.text = "화염을 발사하는 마법!" + "\n" + "처음 적중한 적한테서 폭발하는 스킬";
        ManualManaText.text = "정신력 소모:5";
        ManualDamageText.text = "50+" + "레벨당 데미지";
    }
    public void ManualSkill3()
    {
        ManualMainText.text = "에너지 볼트";
        ManualText.text = "에너지를 발사하는 마법!" + "\n" + "적을 관통해서 날아가는 스킬";
        ManualManaText.text = "정신력 소모:10";
        ManualDamageText.text = "30+" + "레벨당 데미지";
    }
    public void ManualSkill4()
    {
        ManualMainText.text = "썬더 에리어";
        ManualText.text = "주변에 번개를 일으키는 마법!" + "\n" + "주변에 피해를 주고 날려버리는 스킬";
        ManualManaText.text = "정신력 소모:5";
        ManualDamageText.text = "30+" + "레벨당 데미지";
    }
    public void ManualSkill5()
    {
        ManualMainText.text = "엘리멘탈 에로우";
        ManualText.text = "충전이 완료되면 강력한 화살 발사!" + "\n" + "2초이상 차징해서 발사하는 스킬 " + "\n" + "5초가 지나면 자동으로 발사";
        ManualManaText.text = "정신력 소모:20";
        ManualDamageText.text = "200+" + "레벨당 데미지";
    }
    public void Item1()
    {
        ManualMainText.text = "체력 포션";
        ManualText.text = "마시면 일정비율 체력이 회복된다";
        ManualManaText.text = "체력회복 50 %"+ "\n"+"재사용 대기시간:5초";
        ManualDamageText.text = "";
    }
    public void Item2()
    {
        ManualMainText.text = "마나 포션";
        ManualText.text = "마시면 일정비율 정신력이 회복된다";
        ManualManaText.text = "마나회복 30 %"+ "\n" + "재사용 대기시간:5초";
        ManualDamageText.text = "";
    }
    public void Item3()
    {
        ManualMainText.text = "엘릭서";
        ManualText.text = "마시면 일정시간동안"+"\n"+ "공격력과 이동속도가 상승한다";
        ManualManaText.text = "공격력 20%" + "이동속도 30%"+"\n" + "재사용 대기시간:1분";
        ManualDamageText.text = "";
    }
    public void Item4()
    {
        ManualMainText.text = "순간이동 구슬";
        ManualText.text = "사용하면 지정한 위치로 이동한다";
        ManualManaText.text = "재사용 대기시간:1분";
        ManualDamageText.text = "";
    }
}
