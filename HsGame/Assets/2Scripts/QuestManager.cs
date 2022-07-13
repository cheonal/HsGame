using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public int ChickenPoint;
    public int EnemyPoint;
    public static QuestManager questManager;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questManager = this.GetComponent<QuestManager>();
        questList = new Dictionary<int, QuestData>();
        GenrateData();
    }
    void Update()
    {
        MonesterData();
    }
    void GenrateData()
    {
        questList.Add(10, new QuestData("�����ӿ��� ���� �ɾ��"
            , new int[] { 1000, 1000 }));
        questList.Add(20, new QuestData("�������� ��Ź�� �������"
            , new int[] { 1000, 1000 }));
        questList.Add(30, new QuestData("�ķ�â�� ������ �ߵ��� ȥ������"
           , new int[] { 100 }));
        questList.Add(40, new QuestData("�����ӿ��� ���ư���"
            , new int[] { 1000 }));
        questList.Add(50, new QuestData("������ ���� ���� ������ �������� ã�ư���"
            , new int[] { 2000}));
        questList.Add(60, new QuestData("������ �������� ��Ź�� �������"
             , new int[] { 2000,4000 , 2000 }));
        questList.Add(70, new QuestData("�����ӿ��� ���ư���"
            , new int[] { 1000 }));
        questList.Add(80, new QuestData("��ս������� ã���� ����"
            , new int[] { 3000 }));
        questList.Add(90, new QuestData("����â�� ��ã���� ����"
            , new int[] { 100 }));
        questList.Add(100, new QuestData("�ձ��� ������ ��ո��͸� ��ġ����"
            , new int[] { 3000 }));
    }
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {


        //����Ʈ ���� ����
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        //����Ʈ ������Ʈ ����
        ControlObject();

        //���� ����Ʈ��
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
    void MonesterData()
    {
        if (questId ==30 && ChickenPoint == 7)
        {
            questId += 10;
            questActionIndex = 0;
        }
        if (questId == 90 && EnemyPoint == 10)
        {
            questId += 10;
            questActionIndex = 0;
        }
    }
    void ControlObject()
    {
        switch (questId)
        {
            case 30:
                if (questActionIndex == 0)
                {
                    ChickenPoint = 0;
                }
                break;
            case 60:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                if (questActionIndex == 2)
                    questObject[0].SetActive(false);
                break;
            case 90:
                if (questActionIndex == 0)
                {
                    EnemyPoint = 0;
                }
                break;

        }
    }
}
