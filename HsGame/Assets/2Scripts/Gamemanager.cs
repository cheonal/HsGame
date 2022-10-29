using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public PlayerStateUi PlayerStateUi;
    public Player player;

    public RectTransform skillR;
    public RectTransform skillRGuageFront;
    public RectTransform skillRGuagesucces;
    public RectTransform skill1CoolDown;
    public RectTransform skill2CoolDown;
    public RectTransform skill3CoolDown;
    public RectTransform skill4CoolDown;
    public RectTransform skill5CoolDown;
    public RectTransform Item1CoolDown;
    public RectTransform Item2CoolDown;
    public RectTransform Item3CoolDown;
    public RectTransform Item4CoolDown;
    public RectTransform PlayerHp;
    public RectTransform PlayerMp;
    public RectTransform PlayerExp;
    public RectTransform BossHp;
    public RectTransform ElixirMiniGuage;
    public GameObject BossGroup;
    public GameObject ElixirMini;
    public GameObject PlayerWand;
    public GameObject PlayerSword;
    public GameObject PlayerHand;
    public GameObject PlayerState;
    public GameObject scanObject;
    public GameObject NpcTalk;
    public Image IconWand;
    public Image IconSword;
    public Image IconHand;
    public Text Lv;
    public Text TalkText;
    public Text QuestText;
    public int talkIndex;
    public bool isTalk;

    [SerializeField] GameObject GameStartState;
    [SerializeField] GameObject GameOverState;
    [SerializeField] GameObject GameStartCamera;
    [SerializeField] GameObject PlayerCamera;
    [SerializeField] GameObject PlayerCh;
    [SerializeField] GameObject slime1;
    [SerializeField] GameObject slime2;
    [SerializeField] GameObject slime3;
    [SerializeField] GameObject GameEndUi;
    [SerializeField] GameObject SwordImg;
    [SerializeField] GameObject WandImg;
    [SerializeField] GameObject SwordImg1;
    [SerializeField] GameObject WandImg1;
    [SerializeField] GameObject MagicImg;
    [SerializeField] GameObject MagicImg1;
    [SerializeField] GameObject Item4Img;
    [SerializeField] GameObject item4img1;
    [SerializeField] GameObject Slime1StartQuest;
    [SerializeField] GameObject Slime1EndQuest;
    [SerializeField] GameObject Slime2StartQuest;
    [SerializeField] GameObject Slime2EndQuest;
    [SerializeField] GameObject Slime3StartQuest;
    [SerializeField] GameObject Slime3EndQuest;
    [SerializeField] GameObject MonsterField1;
    [SerializeField] GameObject MonsterField2;
    [SerializeField] RectTransform BossHpGruop;
    [SerializeField] Transform BossRoom;
    [SerializeField] Transform KingRoom;
    [SerializeField] Text GamePlayTimeText;
    [SerializeField] Text EnemyCountText;
    [SerializeField] Text BossCountText;
    [SerializeField] Text GameOverText;
    [SerializeField] Image GameStartButton;
    [SerializeField] float GamePlayTime;

    AudioSource audioSource;
    float ElixirMiniTime;
    float Item1;
    float Item2;
    float Item3;
    float Item4;
    float skill1;
    float skill2;
    float skill3;
    float skill4;
    float skill5;
    float Guage;
    bool GameOn;
    bool BossOn;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void GameStart()
    {
        PlayerCh.SetActive(true);
        slime2.SetActive(true);
        GameStartCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        GameStartState.SetActive(false);
        PlayerState.SetActive(true);
        GameOn = true;
    }
    void Update()
    {
        Quest();
        CoolDown();
        SkillR();
        Invoke("State",0.5f);
        LvUp();
        GameOver();
        EnemyCount();
        GameEnd();
    }
    public void BossRoomSpawn()
    {
        StartCoroutine("BossRoomSpawn1");
    }
    IEnumerator BossRoomSpawn1()
    {
        isTalk = true;
        BossCountText.text = "3초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "2초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "1초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "";
        player.transform.position = BossRoom.position;
        BossOn = true;
        isTalk = false;
    }
    public void ItemSpawn()
    {
        StartCoroutine("ItemSpawn1");
    }
    IEnumerator ItemSpawn1()
    {
        BossCountText.text = "3초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "2초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "1초뒤에 이동합니다";
        yield return new WaitForSeconds(1f);

        BossCountText.text = "";
        player.transform.position = KingRoom.position;
        BossOn = false;
    }
    void Quest()
    {
        QuestText.text = "" + questManager.CheckQuest();
    }
    void State()
    {
        if (GameOn)
        {
            PlayerHp.localScale = new Vector2((float)(Player.player.curhealth / Player.player.maxhealth), 1);
            PlayerMp.localScale = new Vector2((float)(Player.player.curmana / Player.player.maxmana), 1);
            PlayerExp.localScale = new Vector2((float)(Player.player.curexp / Player.player.maxexp), 1);
            Lv.text = "" + player.PlayerLv;
        }
        if (BossOn)
        {
            BossHpGruop.anchoredPosition = new Vector2(0, 0);
            BossHp.localScale = new Vector2((float)(Boss2.boss.curHp / Boss2.boss.MaxHp), 1);
        }
        else
        {
            BossHpGruop.anchoredPosition = new Vector2(0, 180);
        }
    }
    void EnemyCount()
    {
        switch (questManager.questId)
        {
            case 30:
                EnemyCountText.text = "식량창고를 되찾자" + questManager.ChickenPoint + "/7";
                if(questManager.ChickenPoint == 7)
                {
                    EnemyCountText.text = "퀘스트 완료!";
                }
                break;
            case 50:
                EnemyCountText.text = "";
                break;
            case 90:
                EnemyCountText.text = "무기창고를 되찾자" + questManager.EnemyPoint + "/10";
                if (questManager.EnemyPoint == 10)
                {
                    EnemyCountText.text = "퀘스트 완료!";
                }
                break;
            case 110:
                EnemyCountText.text = "";
                break;

        }
    }
    void LvUp()
    {
        if (player.curexp > player.maxexp)
        {
            audioSource.Play();
            player.PlayerLv += 1;
            player.curexp -= player.maxexp;
            player.maxexp += 10;
            player.LvPoint += 1;
        }
    }

    void GameOver()
    {
        if (player.curhealth <= 0)
        {
            GameOverText.text = "현재 진행중인 퀘스트"+"\n" + ">>"+questManager.CheckQuest();
            PlayerState.SetActive(false);
            GameOverState.SetActive(true);
        }
        
    }
    void CoolDown()
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
        if (PlayerStateUi.Item1CoodDown)
        {
            Item1CoolDown.anchoredPosition = Vector2.zero;
            Item1 += Time.deltaTime;
            Item1CoolDown.localScale = new Vector2(1, (1 - Item1 / 5));
        }
        else
        {
            Item1 = 0;
            Item1CoolDown.anchoredPosition = Vector2.down * 300;
            Item1CoolDown.localScale = new Vector2(1, 1);
        }
        if (PlayerStateUi.Item2CoodDown)
        {
            Item2CoolDown.anchoredPosition = Vector2.zero;
            Item2 += Time.deltaTime;
            Item2CoolDown.localScale = new Vector2(1, (1 - Item2 / 5));
        }
        else
        {
            Item2 = 0;
            Item2CoolDown.anchoredPosition = Vector2.down * 300;
            Item2CoolDown.localScale = new Vector2(1, 1);
        }
        if (PlayerStateUi.Item3CoodDown)
        {
            Item3CoolDown.anchoredPosition = Vector2.zero;
            Item3 += Time.deltaTime;
            Item3CoolDown.localScale = new Vector2(1, (1 - Item3 / 30));
        }
        else
        {
            Item3 = 0;
            Item3CoolDown.anchoredPosition = Vector2.down * 300;
            Item3CoolDown.localScale = new Vector2(1, 1);
        }
        if (PlayerStateUi.Item3PowerUp)
        {
            ElixirMiniGuage.anchoredPosition = Vector2.zero;
            ElixirMini.SetActive(true);
            ElixirMiniTime += Time.deltaTime;
            ElixirMiniGuage.localScale = new Vector2(1 - ElixirMiniTime / 10, 1);
        }
        else
        {
            ElixirMiniTime = 0;
            ElixirMiniGuage.anchoredPosition = Vector2.down * 300;
            ElixirMiniGuage.localScale = new Vector2(1, 1);
            ElixirMini.SetActive(false);
        }
        if (PlayerStateUi.Item4CoodDown)
        {
            Item4CoolDown.anchoredPosition = Vector2.zero;
            Item4 += Time.deltaTime;
            Item4CoolDown.localScale = new Vector2(1, (1 - Item4 / 60));
        }
        else
        {
            Item4 = 0;
            Item4CoolDown.anchoredPosition = Vector2.down * 300;
            Item4CoolDown.localScale = new Vector2(1, 1);
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
    public void ButtonOn()
    {
        PlayerState.SetActive(true);
        GameOverState.SetActive(false);
        player.ReBirth();
    }
    public void TalkExit()
    {
        NpcTalk.SetActive(false);
        PlayerState.SetActive(true);
    }
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objdata = scanObject.GetComponent<ObjData>();
        Talk(objdata.id, objdata.isNpc);
        NpcTalk.SetActive(isTalk);
        PlayerState.SetActive(!isTalk);
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOut()
    {
        Application.Quit();
    }
    void GameEnd()
    {
        if (GameOn)
        {
            GamePlayTime += Time.deltaTime;
        }
        if(questManager.questId == 20 && questManager.questActionIndex ==1)
        {
            SwordImg.SetActive(true);
            SwordImg1.SetActive(true);
        }
        if(questManager.questId == 30)
        {
            MonsterField1.SetActive(true);
            Slime1StartQuest.SetActive(false);
        }
        if (questManager.questId == 40)
        {
            Slime1EndQuest.SetActive(true);
        }
        if (questManager.questId == 50)
        {
            Slime1EndQuest.SetActive(false);
            Slime2StartQuest.SetActive(true);
        }
        if (questManager.questId == 60 && questManager.questActionIndex == 1)
        {
            Slime2StartQuest.SetActive(false);
        }
        if (questManager.questId == 60 && questManager.questActionIndex == 2)
        {
            Slime2EndQuest.SetActive(true);
        }
        if (questManager.questId == 70)
        {
            Slime2EndQuest.SetActive(false);
            Slime1StartQuest.SetActive(true);
            WandImg.SetActive(true);
            WandImg1.SetActive(true);
            MagicImg.SetActive(true);
            MagicImg1.SetActive(true);
        }
        if (questManager.questId == 80)
        {
            Slime1StartQuest.SetActive(false);
            Slime3StartQuest.SetActive(true);
        }
        if(questManager.questId == 90)
        {
            Slime3StartQuest.SetActive(false);
            MonsterField2.SetActive(true);
        }
        if(questManager.questId == 100)
        {
            Slime3EndQuest.SetActive(true);
        }
        if (questManager.questId == 110)
        {
            Slime3EndQuest.SetActive(false);
            Item4Img.SetActive(true);
            item4img1.SetActive(true);
        }
        if(questManager.questId == 120)
        {
            Slime3EndQuest.SetActive(true);
        }
        if (questManager.questId == 130)
        {
            int hour = (int)(GamePlayTime / 3600);
            int min = (int)((GamePlayTime - hour * 3600)/60);
            int sec = (int)(GamePlayTime % 60);       
            GamePlayTimeText.text = "클리어까지 걸린시간" + string.Format("{0:00}",hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);
            GameEndUi.SetActive(true);
            PlayerCh.SetActive(false);
            GameStartCamera.SetActive(true);
            PlayerCamera.SetActive(false);
            GameStartState.SetActive(false);
            PlayerState.SetActive(false);
            GameOn = false;
        }

    }
    void Talk(int id, bool isNpc)
    {
        //데이터 세팅
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex);

        //대화가 끝날때
        if (id == 3000)
        {
            PlayerStateUi.PlayerUI.Item1Count = 5;
            PlayerStateUi.PlayerUI.Item2Count = 5;
            PlayerStateUi.PlayerUI.Item3Count = 5;
        }
        if (talkData == null)
        {
            isTalk = false;     
            talkIndex = 0;
            questManager.CheckQuest(id); 
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


