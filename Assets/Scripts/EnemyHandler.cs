using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int health;
    [SerializeField] private int max_health;

    // mislio sam ovako nesto da uradim ali je dosta bolje za performansu ako koristim metodu
    // void Update()
    // {
    //     if (health<=0){
    //         Destroy(gameObject);
    //     }
    // }
    public int increment_health(int amount){
        if (health+amount <= 0){Destroy(gameObject); return 0;} // mora da vrati int
        if (health+amount > max_health){health = max_health;return health;}
        health+=amount;
        return health;
    }
}
