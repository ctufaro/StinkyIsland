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
    private float totaltime;

    private int walkFor { get; set; }

    public void Start()
    {
        FartStarted = false;
        MonsterFart = false;
        CurrentX = 0;
        CurrentY = 0;
        totaltime = 2001;
        Name = "Monster1";
    }

    public void Update()
    {
        //run some ai code here
        if (!FartStarted && !MonsterFart && (totaltime > walkFor))
        {
            totaltime = 0f;
            walkFor = Random.Range(1, 3);
            int t = Random.Range(1, 10);
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
                print("Moving " + t.ToString());
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
        print("COLLISION!");
        CurrentY = 0;
        CurrentX = 0;
        //if (CurrentX == 1)
        //    CurrentX = -1;
        //if (CurrentX == -1)
        //    CurrentX = 1;
        //if (CurrentY == 1)
        //    CurrentY = -1;
        //if (CurrentY == -1)
        //    CurrentY = 1;
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
