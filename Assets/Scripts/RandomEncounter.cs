using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncounter : MonoBehaviour
{
    [SerializeField]
    private float encounterChance = 0.01f; // 10% chance per step

    [SerializeField]
    private string battleSceneName = "Battle Scene";

    public void TryEncounter()
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < encounterChance)
        {
            TriggerEncounter();
        }
    }

    private void TriggerEncounter()
    {
        Debug.Log("Encounter triggered! Loading battle scene...");
        SceneManager.LoadScene(battleSceneName);
    }
}
