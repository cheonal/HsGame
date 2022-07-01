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
        ManualDamageText.text = "20+" + "������ ������";
    }
    public void ManualSkill2()
    {
        ManualMainText.text = "���̾� ��";
        ManualText.text = "ȭ���� �߻��ϴ� ����!" + "\n" + "ó�� ������ �����׼� �����ϴ� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:5";
        ManualDamageText.text = "50+" + "������ ������";
    }
    public void ManualSkill3()
    {
        ManualMainText.text = "������ ��Ʈ";
        ManualText.text = "�������� �߻��ϴ� ����!" + "\n" + "���� �����ؼ� ���ư��� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:10";
        ManualDamageText.text = "30+" + "������ ������";
    }
    public void ManualSkill4()
    {
        ManualMainText.text = "��� ������";
        ManualText.text = "�ֺ��� ������ ����Ű�� ����!" + "\n" + "�ֺ��� ���ظ� �ְ� ���������� ��ų";
        ManualManaText.text = "���ŷ� �Ҹ�:5";
        ManualDamageText.text = "30+" + "������ ������";
    }
    public void ManualSkill5()
    {
        ManualMainText.text = "������Ż ���ο�";
        ManualText.text = "������ �Ϸ�Ǹ� ������ ȭ�� �߻�!" + "\n" + "2���̻� ��¡�ؼ� �߻��ϴ� ��ų " + "\n" + "5�ʰ� ������ �ڵ����� �߻�";
        ManualManaText.text = "���ŷ� �Ҹ�:20";
        ManualDamageText.text = "200+" + "������ ������";
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
        ManualText.text = "���ø� �����ð�����"+"\n"+ "���ݷ°� �̵��ӵ��� ����Ѵ�";
        ManualManaText.text = "���ݷ� 20%" + "�̵��ӵ� 30%"+"\n" + "���� ���ð�:1��";
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
        ManualDamageText.text = "100+������ ������";
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
