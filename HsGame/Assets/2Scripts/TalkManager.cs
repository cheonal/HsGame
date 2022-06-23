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
        talkData.Add(1000, new string[] { "앗?", "여기 처음오신 분이군요","안녕하세요 ㅎㅎ","저는 슬라임이에요" });
        talkData.Add(2000, new string[] { "저는 박스에요"});

        //퀘스트 
        talkData.Add(10+1000, new string[] { "저희는 지금 위험해요", "혹시", "도와주실수 있나요?" });
        talkData.Add(11+2000, new string[] { "저도 위험해요 ㅜㅜ", "비도 내리고", "무서워요" });

        talkData.Add(20 + 1000, new string[] { "혹시", "도와주실수 있나요?","제 버섯을 찾아주세요" });
        talkData.Add(21 + 2000, new string[] { "나는 버섯이 아니야!" });
        talkData.Add(21 + 5000, new string[] { "슬라임이 말한 버섯이 이것인가 보다" });
        talkData.Add(22 + 1000, new string[] { "우와 고마워요", "ㅎㅎ",});

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
