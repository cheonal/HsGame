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
        talkData.Add(1000, new string[] { "저희 마을을 구해주세요" });
        talkData.Add(2000, new string[] { "어쩌다가 마을이 이렇게 됬는지.."});
        talkData.Add(3000, new string[] { "왕국을 되찾게 도와주시게나" });


        //퀘스트 
        talkData.Add(10+1000, new string[] { "앗 안녕하세요!", "혹시 모험가님이신가요?",
            "아하 무기를 잃어버리고 헤메고 계셨군요" ,"제 부탁을 들어주시면 무기를 드릴게요" });
        talkData.Add(11+1000, new string[] { "다른 몬스터들의 출현으로 다른 저희 왕국을 빼았기고 말았거든요",
            "저희 왕국을 되찾는걸 도와주세요!"});
        talkData.Add(20 + 1000, new string[] { "도와주신다니 감사합니다 ㅎㅎ", "여기 약속한 무기에요",
            "평범한 칼이지만 맘에 드셨으면 좋겠네요",
        "p키를 누르시면 장비와 아이템을 확인하실수 있어요","q키를 누르시면 무기를 착용하실수 있답니다",
            "공격은 좌클릭 또는 ctrl 키로 하실수 있어요"});
        talkData.Add(21 + 1000, new string[] { "처음  목표는 저희의 식량을 갉아먹는 닭들이에요",
            "오른쪽으로 가시면 저희의 식량창고가 있을거에요 ", "가서 닭들을 잡아와주세요",});
        talkData.Add(30 + 1000, new string[] { "아직 덜 퇴치하신거 같아요" });
        talkData.Add(40 + 1000, new string[] { "닭들을 전부 퇴치하신건가요?","감사합니다! 다음 목표는 기계 몬스터들이에요",
        "그치만 검만으로는 물리치기 쉽지 않으실 꺼에요" , "제가 드린 편지를 가지고 마법사 슬라임에게 가보세요","마법사 슬라임은 북동쪽에 있답니다"});
        talkData.Add(50 + 1000, new string[] { "마법사 슬라임에게 가보세요! 북동쪽에 있답니다" });
        talkData.Add(50 + 2000, new string[] { "이곳에는 무슨일인가?","편지? 아아 이런 사정이 있었군",
            "마법을 알려주는 대신 내 부탁을 하나 들어주시게"});
        talkData.Add(60 + 2000, new string[] { "요즘 새로운 마법을 연구하는데 필요한 재료가 떨어져서 그런데",
        "식량창고에 가서 빨간버섯 하나만 가져다 주시게나" });
        talkData.Add(61 + 4000, new string[] { "빨간버섯을 획득했다" });
        talkData.Add(62 + 2000, new string[] { "고맙소 은인에게 내 모든 마법을 알려주지","e키를 눌러서 완드를 장비한 상태에서 사용할수있소",
        "마법에 대한 설명은 p키를 눌러서 확인하면 되오","정신력이 부족하면 마법을 사용할 수 없으니 참고하시게나"});
        talkData.Add(70 + 1000, new string[] { "아 모험가님 마법을 배워서 돌아오셨군요 축하드려요!",
        "이제 다음 목표를 수행하기 위해 저희 대왕슬라임님을 찾으러 같이 가도록 해요!"});
        talkData.Add(80 + 3000, new string[] { "자네는 누구인가?","아아 그런일들이 있었군","우리를 도와주신다니 참으로 감사하오",
        "모든일이 잘 해결되면 우리마을에 동상을 세워서 그대를 기억하도록 하겠소",
            "왕국을 되찾기에 앞서서 우리의 무기 창고를 점령한 기계 몬스터들을 퇴치해주시오"});
        talkData.Add(90 + 3000, new string[] { "적들을 퇴치해 주시게나" });
        //   talkData.Add(21 + 2000, new string[] { "나는 버섯이 아니야!" });
        //   talkData.Add(21 + 5000, new string[] { "슬라임이 말한 버섯이 이것인가 보다" });
        //   talkData.Add(22 + 1000, new string[] { "우와 고마워요", "ㅎㅎ",});

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
