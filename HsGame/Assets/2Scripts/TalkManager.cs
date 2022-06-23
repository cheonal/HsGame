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
        talkData.Add(1000, new string[] { "��?", "���� ó������ ���̱���","�ȳ��ϼ��� ����","���� �������̿���" });
        talkData.Add(2000, new string[] { "���� �ڽ�����"});

        //����Ʈ 
        talkData.Add(10+1000, new string[] { "����� ���� �����ؿ�", "Ȥ��", "�����ֽǼ� �ֳ���?" });
        talkData.Add(11+2000, new string[] { "���� �����ؿ� �̤�", "�� ������", "��������" });

        talkData.Add(20 + 1000, new string[] { "Ȥ��", "�����ֽǼ� �ֳ���?","�� ������ ã���ּ���" });
        talkData.Add(21 + 2000, new string[] { "���� ������ �ƴϾ�!" });
        talkData.Add(21 + 5000, new string[] { "�������� ���� ������ �̰��ΰ� ����" });
        talkData.Add(22 + 1000, new string[] { "��� ������", "����",});

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
