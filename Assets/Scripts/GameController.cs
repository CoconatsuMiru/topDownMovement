using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    [SerializeField]
    private GameObject battleMenu;

    void Start()
    {
        fighterStats = new List<FighterStats>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentHeroStats = hero.GetComponent<FighterStats>();
        currentHeroStats.CalculateNextTurn(0);
        fighterStats.Add(currentHeroStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();
        this.battleMenu.SetActive(true); // Keep the battle menu active throughout the combat

        NextTurn();
    }

    public void NextTurn()
    {
        if (IsBattleEnded())
        {
            LoadMainScene();
            return;
        }

        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);
        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();

            if (currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);
            }
            else
            {
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "heal";
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        }
        else
        {
            NextTurn();
        }
    }

    public void EndHeroTurn()
    {
        this.battleMenu.SetActive(false);
        NextTurn();
    }

    private bool IsBattleEnded()
    {
        bool heroAlive = false;
        bool enemyAlive = false;

        foreach (FighterStats fighter in fighterStats)
        {
            if (fighter.gameObject.tag == "Hero" && !fighter.GetDead())
            {
                heroAlive = true;
            }
            if (fighter.gameObject.tag == "Enemy" && !fighter.GetDead())
            {
                enemyAlive = true;
            }
        }

        return !heroAlive || !enemyAlive;
    }

    private void LoadMainScene()
    {
        // Replace "MainScene" with the actual name of your main scene
        SceneManager.LoadScene("World");
    }
}
