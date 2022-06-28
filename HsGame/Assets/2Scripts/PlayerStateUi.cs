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
}
