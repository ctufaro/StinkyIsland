using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Monster : MonoBehaviour, IMonster
{

    public Enums.MonsterType Type { get; set; }
    public string Name { get; set; }
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool MonsterFart;
    public bool FartStarted;
    public int DifficultyLevel { get; set; }
    public float Speed { get; set; }

    private float totaltime;
    private int walkFor { get; set; }

    private GameObject hero;
    private GameObject thismonster;

    public void Start()
    {
        FartStarted = false;
        MonsterFart = false;
        CurrentX = 0;
        CurrentY = 0;
        totaltime = 2001;
        Name = "Monster";
        Speed = 1f;
        hero = GameObject.Find("Hero");
        thismonster = GameObject.Find(Name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            DifficultyLevel = 0;
        if (Input.GetKeyDown(KeyCode.Keypad1))
            DifficultyLevel = 1;
        if (Input.GetKeyDown(KeyCode.Keypad2))
            DifficultyLevel = 2;

        //run some ai code here
        if (!FartStarted && !MonsterFart && (totaltime > walkFor))
        {
            totaltime = 0f;

            walkFor = Random.Range(1, 3);

            //at level 0, the monster is random
            //at level 1 the monster farts twice as much
            //at level 2, the monster gravitates toward the hero

            int t = 0;
            Speed = 1f;
            switch (DifficultyLevel)
            {
                case 0:
                    t = Random.Range(1, 7);
                    break;
                case 1:
                    t = Random.Range(1, 9);
                    Speed = 2f;
                    break;
                case 2:
                    //figure out the direction towards the hero
                    int d = Random.Range(1, 5);
                    Speed = 2f;
                    if (d == 1)
                    {
                        if (hero.rigidbody2D.position.x > thismonster.rigidbody2D.position.x)
                        {
                            t = 2;
                        }
                        else
                        {
                            t = 1;
                        }
                    }
                    else if (d == 2)
                    {
                        if (hero.rigidbody2D.position.y > thismonster.rigidbody2D.position.y)
                        {
                            t = 3;
                        }
                        else
                        {
                            t = 4;
                        }
                    }
                    else if (d == 3)
                    {
                        t = 5;
                    }
                    else
                    {
                        t = 6;
                    }
                    float dist = System.Math.Abs(hero.rigidbody2D.position.y - thismonster.rigidbody2D.position.y);
                    dist += System.Math.Abs(hero.rigidbody2D.position.x - thismonster.rigidbody2D.position.x);

                    //increase speed the closer the hero is
                    if (dist < 4)
                        Speed = 3f;
                    if (dist < 3)
                        Speed = 4f;
                    break;
            }

            if (t > 5)
            {
                //for some reason the moving animation does not stop soon enough for the fart
                CurrentX = 0;
                CurrentY = 0;
                print("Farting");
                totaltime = 0f;
                Fart();

            }
            else
            {
                //print("Moving " + t.ToString());
                switch (t)
                {
                    case 1:
                        CurrentX = -1;
                        CurrentY = 0;
                        break;
                    case 2:
                        CurrentX = 1;
                        CurrentY = 0;
                        break;
                    case 3:
                        CurrentX = 0;
                        CurrentY = 1;
                        break;
                    case 4:
                        CurrentX = 0;
                        CurrentY = -1;
                        break;
                    case 5:
                        CurrentX = 0;
                        CurrentY = 0;
                        break;
                }
            }
        }
        totaltime = totaltime + Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        //print("COLLISION!");
        //CurrentY = 0;
        //CurrentX = 0;
        if (CurrentX == 1)
            CurrentX = -1;
        if (CurrentX == -1)
            CurrentX = 1;
        if (CurrentY == 1)
            CurrentY = -1;
        if (CurrentY == -1)
            CurrentY = 1;
    }

    public void Fart()
    {
        MonsterFart = true;
    }

    public void Damaged()
    {
        //throw new NotImplementedException();
    }
}