﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RG;

[RequireComponent(typeof(PlayerStats))]
//[RequireComponent(typeof(Animator))]
public class Skills : MonoBehaviour
{
    private Animator animator;
    private PlayerStats playerStats;
    private AI ai;

    public GameObject bullet;

    

    bool isCreated = false;

    GameObject[] spawnCount;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        ai = GetComponent<AI>();
        
    }
    public IEnumerator Attack()
    {
        if (!ai.hasPlayed){
            var weaponAnim = transform.GetChild(1).GetComponent<Animator>();
            weaponAnim.SetTrigger("Attack");
        Debug.Log("EnemyAttack");
        yield return StartCoroutine(ai.HasPlayed(Attack())) ;
        }
    }

    public IEnumerator RangedAttack()
    {

        
        if (!ai.hasPlayed && !isCreated) {
            var heading = ai.target.position - transform.position;
            var rotation = Quaternion.LookRotation(heading);
            var projectile =Instantiate(bullet, transform.position, rotation);
            //bullet.transform.position = transform.position;
            //get the source of the bullet
            projectile.GetComponent<Bullet>().shooter = this.gameObject;
            projectile.transform.LookAt(ai.target);
            projectile.GetComponent<Rigidbody>().AddForce(heading*5.0f, ForceMode.Impulse);
            //projectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(heading * 5.0f);
            Destroy(projectile, 3);
            isCreated = true;
            Debug.Log("RangedEnemyAttack"); 
            yield return StartCoroutine(ai.HasPlayed(RangedAttack()));
        }   
    }

    public IEnumerator Cure()
    {
        if (!ai.hasPlayed){
        Debug.Log("Enemy Casted cure on itself");
        yield return StartCoroutine(ai.HasPlayed(Cure()));
        }
    }

    public IEnumerator DoNothing()
    {
        if (!ai.hasPlayed)
        {
            yield return StartCoroutine(ai.HasPlayed(DoNothing()));
        }
    }

    public IEnumerator Invisible()
    {
        if (!ai.hasPlayed)
        {
            Debug.Log("Invisible!");
            yield return StartCoroutine(ai.HasPlayed(Invisible()));
        }
    }

    public IEnumerator Barrier()
    {
        if (!ai.hasPlayed)
        {
            Debug.Log("Barrier Cast");
            yield return StartCoroutine(ai.HasPlayed(Barrier()));
        }
    }

    public IEnumerator DeathBlow()
    {
        if (!ai.hasPlayed)
        {
            Debug.Log("DeathBlow Attack!!");
            yield return StartCoroutine(ai.HasPlayed(DeathBlow()));
        }
    }

    public IEnumerator CriticalHitUp()
    {
        if (!ai.hasPlayed)
        {
            Debug.Log("Critical Hit Up");
            yield return StartCoroutine(ai.HasPlayed(CriticalHitUp()));
        }
    }

    public IEnumerator PassiveRangedAttack()
    {
        Debug.Log("passive ranged attack");
        transform.LookAt(ai.target.position);
        var heading = ai.target.position - transform.position;
        var rotation = Quaternion.LookRotation(heading);
        var projectile = Instantiate(bullet, transform.position, rotation);
        //bullet.transform.position = transform.position;
        //get the source of the bullet
        projectile.GetComponent<Bullet>().shooter = this.gameObject;
        projectile.transform.LookAt(ai.target);
        projectile.GetComponent<Rigidbody>().AddForce(heading * 5.0f, ForceMode.Impulse);
        //projectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(heading * 5.0f);
        Destroy(projectile, 3);
        yield return new WaitForSeconds(1.5f);
    }
}