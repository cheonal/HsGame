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
        LvPoint.text = "���� ����Ʈ:" + player.LvPoint;
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
        ManualMainText.text = "����� ũ����Ż";
        ManualText.text = "�ֺ� ���Ϳ��� ���ظ� �ش�!" + "\n"+ "������ �ֺ� ������ ���� ����" + "\n" + "����� ������";
        ManualManaText.text = "���ŷ� �Ҹ�:20";
        ManualDamageText.text = "30x(0.1���ݷ�)";
    }
    public void ManualSkill2()
    {
        ManualMainText.text = "���̾� ��";
        ManualText.text = "ȭ���� �߻��ϴ� ����!" + "\n" + "ó�� ������ �����׼� �����ϴ� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:5";
        ManualDamageText.text = "50x(0.1���ݷ�)";
    }
    public void ManualSkill3()
    {
        ManualMainText.text = "������ ��Ʈ";
        ManualText.text = "�������� �߻��ϴ� ����!" + "\n" + "���� �����ؼ� ���ư��� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:10";
        ManualDamageText.text = "30x(0.1���ݷ�)";
    }
    public void ManualSkill4()
    {
        ManualMainText.text = "��� ������";
        ManualText.text = "�ֺ��� ������ ����Ű�� ����!" + "\n" + "�ֺ��� ���ظ� �ְ� ���������� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:5";
        ManualDamageText.text = "30x(0.1���ݷ�)";
    }
    public void ManualSkill5()
    {
        ManualMainText.text = "������Ż ���ο�";
        ManualText.text = "������ �Ϸ�Ǹ� ������ ȭ�� �߻�!" + "\n" + "2���̻� ��¡�ؼ� �߻��ϴ� ��ų " + "\n" + "5�ʰ� ������ �ڵ����� �߻�";
        ManualManaText.text = "���ŷ� �Ҹ�:20";
        ManualDamageText.text = "200x(0.1���ݷ�)"; ;
    }
    public void Item1()
    {
        ManualMainText.text = "ü�� ����";
        ManualText.text = "���ø� �������� ü���� ȸ���ȴ�";
        ManualManaText.text = "ü��ȸ�� 50 %"+ "\n"+"���� ���ð�:5��";
        ManualDamageText.text = "";
    }
    public void Item2()
    {
        ManualMainText.text = "���� ����";
        ManualText.text = "���ø� �������� ���ŷ��� ȸ���ȴ�";
        ManualManaText.text = "����ȸ�� 30 %"+ "\n" + "���� ���ð�:5��";
        ManualDamageText.text = "";
    }
    public void Item3()
    {
        ManualMainText.text = "������";
        ManualText.text = "���ø� 10�ʵ���"+"\n"+ "���� ���ݷ°� �̵��ӵ��� ����Ѵ�";
        ManualManaText.text = "���ݷ� 20%" + "�̵��ӵ� 30%"+"\n" + "���� ���ð�:30��";
        ManualDamageText.text = "";
    }
    public void Item4()
    {
        ManualMainText.text = "�����̵� ����";
        ManualText.text = "����ϸ� ������ ��ġ�� �̵��Ѵ�";
        ManualManaText.text = "���� ���ð�:1��";
        ManualDamageText.text = "";
    }
    public void Item5()
    {
        ManualMainText.text = "�Ǽ�";
        ManualText.text = "�ƹ��͵� ������� ���� �⺻����";
        ManualManaText.text = "���⸦ ��� ������̶� ��ȭ�ϸ�"+"\n" + "�޾����� ��������";
        ManualDamageText.text = "";
    }
    public void Item6()
    {
        ManualMainText.text = "�Ѽհ�";
        ManualText.text = "�����ӿ��Լ� ���� ����� �Ѽհ�";
        ManualManaText.text = "���⺸�ٴ� ������ ���������";
        ManualDamageText.text = "100x(0.1���ݷ�)x(0.2������)";
    }
    public void Item7()
    {
        ManualMainText.text = "���� �ϵ�";
        ManualText.text = "����Ʈ�� �Ϸ��ϰ� ���� �ϵ�";
        ManualManaText.text = "��ų�� ����ϱ����ؼ���" +"\n" + "�ʼ��� ����ؾߵ�";
        ManualDamageText.text = "";
    }


    public void Point1Up()
    {
        if (player.LvPoint > 0)
        {
            player.LvPoint -= 1;
            DamageUp += 1;
            weapon.Damage = 100 + (100 * (DamageUp * (float)0.1));
            //magic ���� �Լ��� �ν��Ͻ� �Ǵ� ������Ʈ�̱� ������ ����ϴ� ��ũ��Ʈ���� ���� ������ ����
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
