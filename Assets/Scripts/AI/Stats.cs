using UnityEngine;
public enum E_States
{
    Normal,
    Stunned
}
public class Stats : MonoBehaviour, IDamagable
{
    public enemyAnimController animController;
    Animator anim;
    public float maxHp;
    public float hp;
    public E_States state;
    [SerializeField] float timeBetweenCanBeStunned = 0.5f;
    public bool canBeStunned = true;
    public bool isDead = false;

    [Header("Spawned Object After Death")]
    public GameObject objectToSpawnAfterDeath;

    public MaterialFader materialFader;
    //Sounds 
    PlayerAudioManager playerAudio;
    //EnemyAudioManager enemyAudio;
    void Start()
    {
        
        if (gameObject.CompareTag("Player"))
        {
            anim = GetComponent<Animator>();
        }
        if (gameObject.CompareTag("Enemy"))
        {
           
            
        }
        hp = maxHp;
        state = E_States.Normal;
        canBeStunned = true;
    }
    public void TakeDamage(int damage, float stunnDuration)
    {
        if (gameObject.CompareTag("Player"))
        {
            playerAudio = GetComponent<PlayerAudioManager>();
            playerAudio.DamageSound();
            anim.SetTrigger("gotHit");
            hp = hp - damage;
        }
        else
        {
            
            animController.Damage();
            playerAudio = GetComponent<PlayerAudioManager>();
            playerAudio.DamageSound();

            if (hp - damage < 0)
            {
                hp = 0;
            }
            else
            {
                hp = hp - damage;
            }
         
            if (canBeStunned)
            {
                canBeStunned = false;
                state = E_States.Stunned;
                
                Invoke(nameof(StunReset), stunnDuration);
            }
            else
            {
                Debug.Log("Entity " + gameObject.name + "cannot be stunned at this moment");
            }
        }
      
    }
    void StunReset()
    {
        state = E_States.Normal;
        canBeStunned = true;
    }

    void Update()
    {
        if (hp <= 0 && !gameObject.CompareTag("Player") && isDead == false)
        {
            isDead = true;

            SpawnObject(objectToSpawnAfterDeath, transform.position, Quaternion.identity);
            materialFader.StartFade();
            playerAudio = GetComponent<PlayerAudioManager>();
            playerAudio.DeathSound();
            Destroy(transform.parent.gameObject, playerAudio.death.length);
        }
    }

    void SpawnObject(GameObject objToSpawn, Vector3 position, Quaternion rotation)
    {
        Instantiate(objToSpawn, position, rotation);
        
    }
}
