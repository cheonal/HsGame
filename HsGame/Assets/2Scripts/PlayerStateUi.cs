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
    public static PlayerStateUi PlayerUI;
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
    public Text Helath;
    public Text Mana;
    public Text Exp;
    public Text Posion1Count;
    public Text Posion2Count;
    public Text Posion3Count;
    public float Item1Count;
    public float Item2Count;
    public float Item3Count;
   // public float Item4Count;
    public float DamageUp;
    public float SpeedUp;
    public float HpUp;
    public float MpUp;
    public bool Item3PowerUp;
    public bool Item1CoodDown;
    public bool Item2CoodDown;
    public bool Item3CoodDown;
    public bool Item4CoodDown;

    public void Start()
    {
        PlayerUI = this.GetComponent<PlayerStateUi>();
    }

    void Update()
    {
        PointText();
        StateScale();
        ExtraLvPoint();
        StateText();
        ItemText();
    }
    void ItemText()
    {
        Posion1Count.text = "" + Item1Count;
        Posion2Count.text = "" + Item2Count;
        Posion3Count.text = "" + Item3Count;
    }
    void StateText()
    {
        Helath.text = player.curhealth + "/" + player.maxhealth;
        Mana.text = player.curmana + "/" + player.maxmana;
        Exp.text = player.curexp + "/" + player.maxexp;
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
        ManualDamageText.text = "30x(0.1공격력)";
    }
    public void ManualSkill2()
    {
        ManualMainText.text = "파이어 볼";
        ManualText.text = "화염을 발사하는 마법!" + "\n" + "처음 적중한 적한테서 폭발하는 스킬";
        ManualManaText.text = "정신력 소모:5";
        ManualDamageText.text = "50x(0.1공격력)";
    }
    public void ManualSkill3()
    {
        ManualMainText.text = "에너지 볼트";
        ManualText.text = "에너지를 발사하는 마법!" + "\n" + "적을 관통해서 날아가는 스킬";
        ManualManaText.text = "정신력 소모:10";
        ManualDamageText.text = "30x(0.1공격력)";
    }
    public void ManualSkill4()
    {
        ManualMainText.text = "썬더 에리어";
        ManualText.text = "주변에 번개를 일으키는 마법!" + "\n" + "주변에 피해를 주고 날려버리는 스킬";
        ManualManaText.text = "정신력 소모:5";
        ManualDamageText.text = "30x(0.1공격력)";
    }
    public void ManualSkill5()
    {
        ManualMainText.text = "엘리멘탈 에로우";
        ManualText.text = "충전이 완료되면 강력한 화살 발사!" + "\n" + "2초이상 차징해서 발사하는 스킬 " + "\n" + "5초가 지나면 자동으로 발사";
        ManualManaText.text = "정신력 소모:20";
        ManualDamageText.text = "200x(0.1공격력)"; ;
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
        ManualText.text = "마시면 10초동안"+"\n"+ "근접 공격력과 이동속도가 상승한다";
        ManualManaText.text = "공격력 20%" + "이동속도 30%"+"\n" + "재사용 대기시간:30초";
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
        ManualDamageText.text = "100x(0.1공격력)x(0.2엘릭서)";
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
            weapon.Damage = 100 + (100 * (DamageUp * (float)0.1));
            //magic 관련 함수는 인스턴스 되는 오브젝트이기 때문에 담당하는 스크립트에서 직접 데미지 지정
        }
    }
    public void Point2Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            SpeedUp += 1;
            player.moveSpeed = 15 + (15*(SpeedUp *(float)0.1));
        }
    }
    public void Point3Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            HpUp += 1;
            player.curhealth = 100 + (100 * (HpUp * (float)0.1));
            player.maxhealth = 100 + (100 * (HpUp * (float)0.1));
        }
    }
    public void Point4Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            MpUp += 1;
            player.curmana = 100 + (100 * (MpUp * (float)0.1));
            player.maxmana = 100 + (100 * (MpUp * (float)0.1));
        }
    }
    public void Item1Use()
    {  
        if (Item1Count > 0 && !Item1CoodDown)
        {
            Item1Count -= 1;
            player.curhealth += player.maxhealth * (float)0.5;
            Item1CoodDown = true;
            Invoke("Item1UseOut", 5f);
            if (player.curhealth> player.maxhealth)
            {
                player.curhealth = player.maxhealth;
            }

        }
    }
    void Item1UseOut()
    {
        Item1CoodDown=false;
    }
    public void Item2Use()
    {
        if (Item2Count > 0 && !Item2CoodDown)
        {
            Item2Count -= 1;
            player.curmana += player.maxmana * (float)0.3;
            Item2CoodDown = true;
            Invoke("Item2UseOut", 5f);
            if (player.curmana > player.maxmana)
            {
                player.curmana = player.maxmana;
            }

        }
    }
    void Item2UseOut()
    {
        Item2CoodDown = false;
    }
    public void Item3Use()
    {
        if (Item3Count > 0 && !Item3CoodDown)
        {
            Item3Count -= 1;
            weapon.Damage += weapon.Damage * (float)0.2;
            player.moveSpeed += player.moveSpeed * (float)0.3;
            player.item3On.SetActive(true);
            Item3CoodDown = true;
            Item3PowerUp = true;
            Invoke("Item3UpOut", 10f);
            Invoke("Item3UseOut", 30f);
        }

    }
    void Item3UseOut()
    {
        player.item3On.SetActive(false);
        Item3CoodDown = false;
    }
    void Item3UpOut()
    {
        Item3PowerUp = false;
        weapon.Damage *= ((float)5/6);
        player.moveSpeed *= ((float)10/13);
    }
    public void Item4Use()
    {
        if (!Item4CoodDown)
        {
            Item4CoodDown = true;
            Invoke("Item4UseOut", 60f);
        }

    }
    void Item4UseOut()
    {
        Item4CoodDown = false;
    }
}
