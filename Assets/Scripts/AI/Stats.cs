using UnityEngine;
public enum E_States
{
    Normal,
    Stunned
}
public class Stats : MonoBehaviour, IDamagable
{
    public float maxHp;
    public float hp;
    public E_States state;
    [SerializeField] float timeBetweenCanBeStunned = 0.5f;
    public bool canBeStunned = true;

    void Start()
    {
        hp = maxHp;
        state = E_States.Normal;
        canBeStunned = true;
    }
    public void TakeDamage(int damage, float stunnDuration)
    {
        if (gameObject.CompareTag("Player"))
        {
            hp = hp - damage;
        }
        else
        {
            hp = hp - damage;

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

    void UnStun()
    {
       
    }
    void StunReset()
    {
        state = E_States.Normal;
        canBeStunned = true;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
