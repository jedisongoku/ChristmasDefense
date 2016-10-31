using UnityEngine;
using System.Collections;

public class MonsterHornet : Enemy {

    public Material mainMaterial;
    public Material dodgeMaterial;
    public float dodgeLength;

    private float dodgeTimer;

    public void DodgeHits()
    {
        dodgeTimer = 0;
        enemyMesh.material = dodgeMaterial;
        
        StartCoroutine(DodgeTimer());
    }

    IEnumerator DodgeTimer()
    {
        dodgeTimer += Time.deltaTime;

        yield return new WaitForSeconds(0);

        if(dodgeTimer >= dodgeLength)
        {
            canTakeDamage = true;
            enemyMesh.material = mainMaterial;
            //when the dodge is over
        }
        else
        {
            StartCoroutine(DodgeTimer());
        } 
    }
}
