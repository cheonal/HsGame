using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Player player;
    public RectTransform skillR;
    public RectTransform skillRGuageFront;
    public RectTransform skillRGuagesucces;
    public RectTransform skill1CoolDown;
    public RectTransform skill2CoolDown;
    public RectTransform skill3CoolDown;
    public RectTransform skill4CoolDown;
    public RectTransform skill5CoolDown;
    public RectTransform PlayerHp;
    public RectTransform PlayerMp;
    public RectTransform PlayerExp;
    public GameObject PlayerState;
    public GameObject scanObject;
    public GameObject NpcTalk;
    public Image NpcImage;
    public Text Lv;
    public Text TalkText;
    public int talkIndex;

    public bool isTalk;

    float skill1;
    float skill2;
    float skill3;
    float skill4;
    float skill5;
    float Guage;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    void Update()
    {
        SkillCoolDown();
        SkillR();
        State();
        LvUp();
    }
    void State()
    {
        PlayerHp.localScale = new Vector2((float)(Player.player.curhealth / Player.player.maxhealth), 1);
        PlayerMp.localScale = new Vector2((float)(Player.player.curmana / Player.player.maxmana), 1);
        PlayerExp.localScale = new Vector2((float)(Player.player.curexp / Player.player.maxexp), 1);
        Lv.text = "" + player.PlayerLv;
    }
    void LvUp()
    {
        if (player.curexp > player.maxexp)
        {
            player.PlayerLv += 1;
            player.curexp -= player.maxexp;
            player.maxexp += 10;
        }
    }
    void SkillCoolDown()
    {
        if (player.skill1coolDown) // skill1
        {
            skill1CoolDown.anchoredPosition = Vector2.zero;
            skill1 += Time.deltaTime;
            skill1CoolDown.localScale = new Vector2(1, (1 - skill1 / 5));
        }
        else
        {
            skill1 = 0;
            skill1CoolDown.anchoredPosition = Vector2.down * 200;
            skill1CoolDown.localScale = new Vector2(1, 1);
        }
        if (player.skill2coolDown) // skill2
        {
            skill2CoolDown.anchoredPosition = Vector2.zero;
            skill2 += Time.deltaTime;
            skill2CoolDown.localScale = new Vector2(1, (1 - skill2 / 2));
        }
        else
        {
            skill2 = 0;
            skill2CoolDown.anchoredPosition = Vector2.down * 200;
            skill2CoolDown.localScale = new Vector2(1, 1);
        }
        if (player.skill3coolDown) //skill3
        {
            skill3CoolDown.anchoredPosition = Vector2.zero;
            skill3 += Time.deltaTime;
            skill3CoolDown.localScale = new Vector2(1, (1 - skill3 / 3));
        }
        else
        {
            skill3 = 0;
            skill3CoolDown.anchoredPosition = Vector2.down * 200;
            skill3CoolDown.localScale = new Vector2(1, 1);
        }
        if (player.skill4coolDown)
        {
            skill4CoolDown.anchoredPosition = Vector2.zero;
            skill4 += Time.deltaTime;
            skill4CoolDown.localScale = new Vector2(1, (1 - skill4 / 1));
        }
        else
        {
            skill4 = 0;
            skill4CoolDown.anchoredPosition = Vector2.down * 200;
            skill4CoolDown.localScale = new Vector2(1, 1);
        }
        if (player.skillRcoolDown)
        {
            skill5CoolDown.anchoredPosition = Vector2.zero;
            skill5 += Time.deltaTime;
            skill5CoolDown.localScale = new Vector2(1, (1 - skill5 / 10));
        }
        else
        {
            skill5 = 0;
            skill5CoolDown.anchoredPosition = Vector2.down * 300;
            skill5CoolDown.localScale = new Vector2(1, 1);
        }
    }
    void SkillR()
    {

        if (player.isCasting == true && Guage < 1f)
        {
            skillR.anchoredPosition = Vector3.up * 350;
            Guage += Time.deltaTime;
            if (Guage > 1f)
            {
                skillRGuagesucces.anchoredPosition = Vector3.up * 350;
            }
        }
        if (player.isCasting == false)
        {
            skillR.anchoredPosition = Vector3.down * 1000;
            skillRGuagesucces.anchoredPosition = Vector3.down * 1000;
            Guage = 0;
        }
        skillRGuageFront.localScale = new Vector3(Guage * 100 / 100, 1, 1);
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objdata = scanObject.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNpc);
        NpcTalk.SetActive(isTalk);
        PlayerState.SetActive(!isTalk);
    }
    void Talk(int id, bool isNpc)
    {
        //데이터 세팅
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex);

        //대화가 끝날때
        if (talkData == null)
        {
            isTalk = false;     
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id)); 
            return;
        }
        if (isNpc)
        {
            TalkText.text = talkData;
        }   
        else
        {
            TalkText.text = talkData;
        }
        isTalk = true;
        talkIndex++;
    }
}


