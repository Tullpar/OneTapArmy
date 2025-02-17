using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierBrain : MonoBehaviour
{
    public enum UnitTeam
    {
        Blue,
        Yellow,
        Red,
        Purple
    }

    public UnitTeam Team;

    [HideInInspector] public SoldierTarget Target;
    [HideInInspector] public SoldierMovement Movement;
    [HideInInspector] public SoldierAnimation Animation;
    [HideInInspector] public SoldierColor SoldierColor;
    [HideInInspector] public SoldierParticles Particles;
    [HideInInspector] public SoldierHealth Health;
    [HideInInspector] public SoldierAttack Attack;
    [HideInInspector] public UnitUI UI;

    public SoldierData SoldierData;

    public bool IsDead;



    public static int UnitTypeCount = Enum.GetNames(typeof(UnitType)).Length;
    public enum UnitType
    {
        Archer,
        Giant,
        Horseman,
        Swordsman,
        Warrior
    }

    public UnitType unitType;


    private void OnEnable()
    {
        Movement = GetComponent<SoldierMovement>();
        Target = GetComponent<SoldierTarget>();
        Animation = GetComponent<SoldierAnimation>();
        SoldierColor = GetComponent<SoldierColor>();
        Particles = GetComponent<SoldierParticles>();
        Health = GetComponent<SoldierHealth>();
        Attack = GetComponent<SoldierAttack>();
        UI = GetComponentInChildren<UnitUI>();
        InitializeUnit();
    }

    void InitializeUnit()
    {
        Health.SetMaxHealth(SoldierData.Health);
        //Movement.agent.speed = SoldierData.MoveSpeed;
        Attack.SetDamage(SoldierData.Damage);
    }


    public void SetTeam(UnitTeam team)
    {
        Team = team;
        SoldierColor.SetColor(team);
    }

    public void Select()
    {
        if ((Team == UnitTeam.Blue))
        {
            Particles.Select();
        }
    }

    public void Deselect()
    {
        Particles.Deselect();
    }


    public void Die()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;



        Movement.Die();
        SoldierColor.Die();
        Animation.Die();
        UI.Die();


        Movement.enabled = false;
        Target.enabled = false;
        Animation.enabled = false;

    }

    

    public bool IsIdle()
    {
        return !Target.HasTarget();
    }
}
