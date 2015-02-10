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

    private Object[] colliders;
    private Collider2D HeroCollider;
    private Collider2D thismonstercollider;
    
    public void Start()
    {
        FartStarted = false;
        MonsterFart = false;
        CurrentX = 0;
        CurrentY = 0;
        totaltime = 2001;
        Name = "Monster";
        DifficultyLevel = 2;
        Speed = 1f;
        hero = GameObject.Find("Hero");
        thismonster = GameObject.Find(Name);
        HeroCollider = (Collider2D)GameObject.Find("Hero").GetComponent(typeof(Collider2D));
        thismonstercollider = (Collider2D)GameObject.Find(Name).GetComponent(typeof(Collider2D));
        colliders = Object.FindObjectsOfType(typeof(Collider));

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
                    if (d <= 2)
                    {
                        t = FindNextDirection(thismonster.transform.position, hero.transform.position);
                    }
                    else if (d == 3)
                    {
                        t = 5;
                    }
                    else
                    {
                        t = 6;
                    }
                    float dist = System.Math.Abs(hero.transform.position.y - thismonster.transform.position.y);
                    dist += System.Math.Abs(hero.transform.position.x - thismonster.transform.position.x);

                    //increase speed the closer the hero is
                    if (dist < 4)
                        Speed = 3f;
                    if (dist < 3)
                        Speed = 4f;
                    break;
            }

            if (t > 5)
            {
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
                        //left
                        CurrentX = -1;
                        CurrentY = 0;
                        break;
                    case 2:
                        //right
                        CurrentX = 1;
                        CurrentY = 0;
                        break;
                    case 3:
                        //up
                        CurrentX = 0;
                        CurrentY = 1;
                        break;
                    case 4:
                        //down                        
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

    public int FindNextDirection(Vector2 start, Vector2 target)
    {
        int res = 5;

        RaycastHit2D r2d1 = new RaycastHit2D();
		//All of your codes are belong to us.
		//Shit... I meant to say:
		//All of the code here should be using the Vector2 parameters instead of accessing the monster/hero directly
		
        foreach (RaycastHit2D hit in Physics2D.CircleCastAll(thismonster.transform.position, thismonster.renderer.bounds.extents.x, hero.transform.position))
        {
            if (hit.collider != HeroCollider && hit.collider != thismonstercollider)
            {
                r2d1 = hit;
                break;
            }
        }

        if (r2d1.collider != null && r2d1.collider != HeroCollider)
        {
            RaycastHit2D r2d2 = new RaycastHit2D();
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(hero.transform.position, thismonster.renderer.bounds.extents.y, new Vector2(hero.transform.position.x, thismonster.transform.position.y)))
            {
                if (hit.collider != HeroCollider && hit.collider != thismonstercollider)
                {
                    r2d2 = hit;
                    break;
                }
            }

            RaycastHit2D r2d3 = new RaycastHit2D();
            foreach (RaycastHit2D hit in Physics2D.CircleCastAll(thismonster.transform.position, thismonster.renderer.bounds.extents.x, new Vector2(thismonster.transform.position.x, hero.transform.position.y)))
            {
                if (hit.collider != HeroCollider && hit.collider != thismonstercollider)
                {
                    r2d3 = hit;
                    break;
                }
            }

            if (r2d2.distance == r2d3.distance)
            {
                if (Random.Range(1, 2) > 1)
                {
                    if (Random.Range(1, 2) > 1)
                    {
                        res = 2;
                    }
                    else
                    {
                        res = 1;
                    }
                }
                else
                {
                    if (Random.Range(1, 2) > 1)
                    {
                        res = 3;
                    }
                    else
                    {
                        res = 4;
                    }
                }
            }
            else
            {

                if (r2d2.distance < r2d3.distance)
                {
                    //x axis will get us further
                    print("X Axis");
                    if (hero.transform.position.x > thismonster.transform.position.x)
                    {
                        res = 2;

                        //if (r2d2.distance < .1)
                        //{
                        //    if (Random.Range(1, 2) > 1)
                        //    {
                        //        res = 1;
                        //    }
                        //    else
                        //    {
                        //        if (hero.transform.position.y > thismonster.transform.position.y)
                        //        {
                        //            res = 3;
                        //        }
                        //        else
                        //        {
                        //            res = 4;
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        res = 1;

                        //if (r2d2.distance < .1)
                        //{
                        //    if (Random.Range(1, 2) > 1)
                        //    {
                        //        res = 2;
                        //    }
                        //    else
                        //    {
                        //        if (hero.transform.position.y > thismonster.transform.position.y)
                        //        {
                        //            res = 3;
                        //        }
                        //        else
                        //        {
                        //            res = 4;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    //y axis will get us further
                    print("Y Axis");
                    //print(r2d2.distance);
                    //print(r2d3.distance);
                    if (hero.transform.position.y > thismonster.transform.position.y)
                    {
                        res = 3;

                        //if (r2d3.distance < .1)
                        //{
                        //    res = 4;
                        //}
                        //else
                        //{
                        //    if (hero.transform.position.x > thismonster.transform.position.x)
                        //    {
                        //        res = 2;
                        //    }
                        //    else
                        //    {
                        //        res = 1;
                        //    }
                        //}
                    }
                    else
                    {
                        res = 4;

                        //if (r2d3.distance < .1)
                        //{
                        //    res = 3;
                        //}
                        //else
                        //{
                        //    if (hero.transform.position.x > thismonster.transform.position.x)
                        //    {
                        //        res = 2;
                        //    }
                        //    else
                        //    {
                        //        res = 1;
                        //    }
                        //}
                    }
                }
            }
        }
        else
        {
            if (System.Math.Abs(hero.transform.position.x - thismonster.transform.position.x) > System.Math.Abs(hero.transform.position.y - thismonster.transform.position.y))
            {
                if (hero.transform.position.x > thismonster.transform.position.x)
                {
                    res = 2;
                }
                else
                {
                    res = 1;
                }
            }
            else
            {
                if (hero.transform.position.y > thismonster.transform.position.y)
                {
                    res = 3;
                }
                else
                {
                    res = 4;
                }
            }
        }
        
        return res;
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
