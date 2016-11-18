using UnityEngine;
using System.Collections;

public class MushroomMonster : Enemy {

    public AnimationClip walkAnimation;
    public AnimationClip takeDamageAnimation;
    public AnimationClip dieAnimation;

    public ParticleSystem healParticle;

    public Animation mushroomAnimation;

    void Awake()
    {
        healParticle.Stop();
    }

    public void Heal()
    {
        enemyHealth += Mathf.CeilToInt(GetMaxHealth() * 0.3f);
        healParticle.Play();
        
    }

    public IEnumerator TakeDamageAnimation()
    {
        mushroomAnimation.Play(takeDamageAnimation.name);
        //mushroomAnimation.clip = takeDamageAnimation;
        //mushroomAnimation.Play();

        yield return new WaitForSeconds(0f);

        mushroomAnimation.Play(walkAnimation.name);
    }

    public IEnumerator DieAnimation()
    {
        mushroomAnimation.Play(dieAnimation.name);
        //mushroomAnimation.clip = dieAnimation;
        //mushroomAnimation.Play();

        yield return new WaitForSeconds(1f);

        mushroomAnimation.Stop();
    }
}
