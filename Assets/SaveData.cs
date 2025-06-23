using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SpawnManagerScriptableObject dataStorage;
    bool isSaved = false;
    public bool isPlayerDead = false;
    Stats playerStats;
    public GameObject deathScreen;
    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
    }
    void Save()
    {
        dataStorage.playerPosition = new Vector3( -8, -1 , -18);
        
        dataStorage.enemyPosition = GameObject.Find("Enemy").transform.position;
        dataStorage.playerHp = 200;
        dataStorage.enemyHp = 100;
    }

   public  void Load()
    {
        //Game stuff
        Time.timeScale = 1f;
        deathScreen.SetActive(false);

        //Objects stuff
        GameObject.Find("Player").GetComponent<Player>().Teleport(dataStorage.playerPosition);
        playerStats.hp = dataStorage.playerHp;
        GameObject enemy = GameObject.Find("Enemy");
        enemy.GetComponentInChildren<Stats>().hp = dataStorage.enemyHp;
        enemy.transform.position = dataStorage.enemyPosition;

        //Reset state variables
        isPlayerDead = false;
    }

    private void Update()
    {
        if (playerStats.hp <= 0 && isPlayerDead == false)
        {
            isPlayerDead = true;
            Debug.Log("Player is dead");
            Time.timeScale = 0f; // Pause the game
            deathScreen.SetActive(true); // Show the death screen
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isSaved == false)
        {
            Debug.Log("Player has saved the game");
            isSaved = true;
            Save();
        }
    }
}
