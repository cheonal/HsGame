using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenrateData();
    }
    void GenrateData()
    {
        questList.Add(10, new QuestData("ù ���� �湮"
            , new int[] { 1000, 2000 }));

        questList.Add(20, new QuestData("�������� ���� ã���ֱ�"
            , new int[] { 1000,5000, 1000 }));
        questList.Add(30, new QuestData("����Ʈ �Ϸ�"
           , new int[] { 0 }));
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
        if(questActionIndex == questList[questId].npcId.Length)
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

    void ControlObject()
    {
        switch (questId)
        {
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                if (questActionIndex == 2)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
