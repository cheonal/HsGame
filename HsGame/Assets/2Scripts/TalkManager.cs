using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "���� ������ �����ּ���" });
        talkData.Add(2000, new string[] { "��¼�ٰ� ������ �̷��� �����.."});
        talkData.Add(3000, new string[] { "�ձ��� ��ã�� �����ֽðԳ�" });


        //����Ʈ 
        talkData.Add(10+1000, new string[] { "�� �ȳ��ϼ���!", "Ȥ�� ���谡���̽Ű���?",
            "���� ���⸦ �Ҿ������ ��ް� ��̱���" ,"�� ��Ź�� ����ֽø� ���⸦ �帱�Կ�" });
        talkData.Add(11+1000, new string[] { "�ٸ� ���͵��� �������� �ٸ� ���� �ձ��� ���ұ�� ���Ұŵ��",
            "���� �ձ��� ��ã�°� �����ּ���!"});
        talkData.Add(20 + 1000, new string[] { "�����ֽŴٴ� �����մϴ� ����", "���� ����� ���⿡��",
            "����� Į������ ���� ������� ���ڳ׿�",
        "pŰ�� �����ø� ���� �������� Ȯ���ϽǼ� �־��","qŰ�� �����ø� ���⸦ �����ϽǼ� �ִ�ϴ�",
            "������ ��Ŭ�� �Ǵ� ctrl Ű�� �ϽǼ� �־��"});
        talkData.Add(21 + 1000, new string[] { "ó��  ��ǥ�� ������ �ķ��� ���ƸԴ� �ߵ��̿���",
            "���������� ���ø� ������ �ķ�â�� �����ſ��� ", "���� �ߵ��� ��ƿ��ּ���",});
        talkData.Add(30 + 1000, new string[] { "���� �� ��ġ�ϽŰ� ���ƿ�" });
        talkData.Add(40 + 1000, new string[] { "�ߵ��� ���� ��ġ�ϽŰǰ���?","�����մϴ�! ���� ��ǥ�� ��� ���͵��̿���",
        "��ġ�� �˸����δ� ����ġ�� ���� ������ ������" , "���� �帰 ������ ������ ������ �����ӿ��� ��������","������ �������� �ϵ��ʿ� �ִ�ϴ�"});
        talkData.Add(50 + 1000, new string[] { "������ �����ӿ��� ��������! �ϵ��ʿ� �ִ�ϴ�" });
        talkData.Add(50 + 2000, new string[] { "�̰����� �������ΰ�?","����? �ƾ� �̷� ������ �־���",
            "������ �˷��ִ� ��� �� ��Ź�� �ϳ� ����ֽð�"});
        talkData.Add(60 + 2000, new string[] { "���� ���ο� ������ �����ϴµ� �ʿ��� ��ᰡ �������� �׷���",
        "�ķ�â�� ���� �������� �ϳ��� ������ �ֽðԳ�" });
        talkData.Add(61 + 4000, new string[] { "���������� ȹ���ߴ�" });
        talkData.Add(62 + 2000, new string[] { "���� ���ο��� �� ��� ������ �˷�����","eŰ�� ������ �ϵ带 ����� ���¿��� ����Ҽ��ּ�",
        "������ ���� ������ pŰ�� ������ Ȯ���ϸ� �ǿ�","���ŷ��� �����ϸ� ������ ����� �� ������ �����ϽðԳ�"});
        talkData.Add(70 + 1000, new string[] { "�� ���谡�� ������ ����� ���ƿ��̱��� ���ϵ����!",
        "���� ���� ��ǥ�� �����ϱ� ���� ���� ��ս����Ӵ��� ã���� ���� ������ �ؿ�!"});
        talkData.Add(80 + 3000, new string[] { "�ڳ״� �����ΰ�?","�ƾ� �׷��ϵ��� �־���","�츮�� �����ֽŴٴ� ������ �����Ͽ�",
        "������� �� �ذ�Ǹ� �츮������ ������ ������ �״븦 ����ϵ��� �ϰڼ�",
            "�ձ��� ��ã�⿡ �ռ��� �츮�� ���� â�� ������ ��� ���͵��� ��ġ���ֽÿ�"});
        talkData.Add(90 + 3000, new string[] { "������ ��ġ�� �ֽðԳ�" });
        //   talkData.Add(21 + 2000, new string[] { "���� ������ �ƴϾ�!" });
        //   talkData.Add(21 + 5000, new string[] { "�������� ���� ������ �̰��ΰ� ����" });
        //   talkData.Add(22 + 1000, new string[] { "��� ������", "����",});

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }
        }
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }


}
