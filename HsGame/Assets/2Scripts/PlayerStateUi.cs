using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUi : MonoBehaviour
{
    public RectTransform StateUi;
    public RectTransform DamageScale;
    public RectTransform SpeedScale;
    public RectTransform HpScale;
    public RectTransform MpScale;
    public Player player;
    public Weapon weapon;
    public MagicArea magic1;
    public MagicArrow magic2;
    public MagicArrow magic3;
    public MagicArea magic4;
    public MagicArrow magic5;
    public Text LvPoint;
    public Text ManualMainText;
    public Text ManualText;
    public Text ManualManaText;
    public Text ManualDamageText;
    public Text DamagePonint;
    public Text SpeedPonint;
    public Text HpPonint;
    public Text MpPonint;
    public float DamageUp = 0;
    public float SpeedUp = 0;
    public float HpUp = 0;
    public float MpUp = 0;

    void Update()
    {
        PointText();
        StateScale();
        ExtraLvPoint();
    }
    void StateScale()
    {
        DamageScale.localScale = new Vector2 (DamageUp/10,1);
        SpeedScale.localScale = new Vector2(SpeedUp / 10, 1);
        HpScale.localScale = new Vector2(HpUp / 10, 1);
        MpScale.localScale = new Vector2(MpUp / 10, 1);
    }
    void ExtraLvPoint()
    {
        LvPoint.text = "남은 포인트:" + player.LvPoint;
    }
    void PointText()
    {
        DamagePonint.text = "" + DamageUp + "/10";
        SpeedPonint.text = "" + SpeedUp + "/10";
        HpPonint.text = "" + HpUp + "/10";
        MpPonint.text = "" + MpUp + "/10";
    }
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
    public void Item5()
    {
        ManualMainText.text = "맨손";
        ManualText.text = "아무것도 장비하지 않은 기본상태";
        ManualManaText.text = "무기를 들고 사람들이랑 대화하면"+"\n" + "받아주지 않을꺼야";
        ManualDamageText.text = "";
    }
    public void Item6()
    {
        ManualMainText.text = "한손검";
        ManualText.text = "슬라임에게서 받은 평범한 한손검";
        ManualManaText.text = "보기보다는 강력한 근접무기야";
        ManualDamageText.text = "100+레벨당 데미지";
    }
    public void Item7()
    {
        ManualMainText.text = "매직 완드";
        ManualText.text = "퀘스트를 완료하고 얻은 완드";
        ManualManaText.text = "스킬을 사용하기위해서는" +"\n" + "필수로 장비해야돼";
        ManualDamageText.text = "";
    }


    public void Point1Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            DamageUp += 1;
            weapon.Damage *= (float)1.3;
            magic1.Damage *= (float)1.2;
            magic2.Damage *= (float)1.2;
            magic3.Damage *= (float)1.2;
            magic4.Damage *= (float)1.2;
            magic5.Damage *= (float)1.2;
        }
    }
    public void Point2Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            SpeedUp += 1;
            player.moveSpeed +=2;
            player.firedelay /= (float)0.1;
        }
    }
    public void Point3Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            HpUp += 1;
            player.curhealth *= (float)1.2;
            player.maxhealth *= (float)1.2;
        }
    }
    public void Point4Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            MpUp += 1;
            player.curmana *= (float)1.2;
            player.maxmana *= (float)1.2;
        }
    }

}
