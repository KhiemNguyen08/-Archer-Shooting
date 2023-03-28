using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameSate
    {
        Starting,
        Playing,
        Gameover
    }
    public GameSate state;
    int m_score;
    public TagetController targetPb;
    public GameObject bow;

    public int Score { get => m_score; set { m_score = value;Prefs.BESTSCORE = value; } }

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        base.Start();
        state = GameSate.Starting;
        GameGUIManager.Ins.ShowGameGUI(false);
        GameGUIManager.Ins.UPdateApple(m_score);
        AudioController.Ins.PlayBackgroundMusic();
    }
    IEnumerator SpawnTagetCo()
    {
        float xPos = Random.Range(0f, 8f);
        float yPos = Random.Range(-3f, 3.6f);
        yield return new WaitForSeconds(0.5f);

        if (targetPb)
            Instantiate(targetPb, new Vector3(xPos, yPos, 0), Quaternion.identity);

    }
    public void SpawnTaget()
    {
        if (state == GameSate.Gameover) return;
        StartCoroutine(SpawnTagetCo());
    }
    public void PlayGame()
    {
        state = GameSate.Playing;
        GameGUIManager.Ins.ShowGameGUI(true);
        Instantiate(bow, new Vector3(-7.55f, 0.11f, 0), Quaternion.identity);

        SpawnTaget();
    }
    public void Gameover()
    {
        if (GameGUIManager.Ins.gameoverDialog)
            GameGUIManager.Ins.gameoverDialog.Show(true);
    }
}
