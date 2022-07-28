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
        questList.Add(10, new QuestData("슬라임에게 말을 걸어보자"
            , new int[] { 1000, 1000 }));
        questList.Add(20, new QuestData("슬라임의 부탁을 들어주자"
            , new int[] { 1000, 1000 }));
        questList.Add(30, new QuestData("식량창고를 점령한 닭들을 혼내주자"
           , new int[] { 100 }));
        questList.Add(40, new QuestData("슬라임에게 돌아가자"
            , new int[] { 1000 }));
        questList.Add(50, new QuestData("마법사 슬라임을 찾아가자"
            , new int[] { 2000}));
        questList.Add(60, new QuestData("마법사 슬라임의 부탁을 들어주자"
             , new int[] { 2000,4000 , 2000 }));
        questList.Add(70, new QuestData("슬라임에게 돌아가자"
            , new int[] { 1000 }));
        questList.Add(80, new QuestData("대왕슬라임을 찾으러 가자"
            , new int[] { 3000 }));
        questList.Add(90, new QuestData("무기창고를 되찾으러 가자"
            , new int[] { 100 }));
        questList.Add(100, new QuestData("대왕 슬라임에게 돌아가자"
            , new int[] { 3000 }));
        questList.Add(110, new QuestData("왕국을 점령한 악마 몬스터를 해치우자"
    , new int[] { 100 }));
        questList.Add(120, new QuestData("4번키를 눌러 대왕슬라임에게 돌아가자"
    , new int[] { 3000 }));
    }
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {


        //퀘스트 순서 진행
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }


        //다음 퀘스트로
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        //퀘스트 오브젝트 관리
        ControlObject();

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
                    questObject[3].SetActive(true); //식량창고 포탈
                    ChickenPoint = 0;
                }   
                break;
            case 50:
                 if(questActionIndex == 0)
                {
                    questObject[1].SetActive(true); // 마법사 슬라임
                }
                break;
            case 60:
                if (questActionIndex == 1)
                    questObject[0].SetActive(true); //빨간 버섯 on
                if (questActionIndex == 2)
                    questObject[0].SetActive(false); //빨간 버섯 off
                break;
            case 80:
                if (questActionIndex == 0)
                {
                    questObject[2].SetActive(true); //대왕 슬라임
                }
                break;
            case 90:
                {
                    EnemyPoint = 0;
                    questObject[4].SetActive(true); //무기창고 포탈
                }
                break;
            case 110:
                {
                    questObject[5].SetActive(true); // 보스몬스터 포탈
                    questObject[6].SetActive(true); // 보스몬스터 
                    questObject[7].SetActive(false); // 보스몬스터 포탈 앞 나무장애물
                }
                break;
        }
    }
}
