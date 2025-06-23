using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{

    public Vector3 playerPosition;
    public Vector3 enemyPosition;
    public float playerHp = 200f;
    public float enemyHp = 100f;
}