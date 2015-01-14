using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.CL
{
    public class GameManager
    {
        private static readonly GameManager _instance = new GameManager();
        private Enums.GameState state = Enums.GameState.GameOver;
        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private GameManager() { }

        public void Start()
        {
            state = Enums.GameState.MainMenu;
        }

        public void Update()
        {
            Console.WriteLine(state);
            //switch (state)
            //{
            //    case GameState.MainMenu: break;

            //    case GameState.Intro:
            //        break;

            //    case GameState.GetReady:
            //        break;

            //    case GameState.LevelRunning:
            //        //PlayerInput();
            //        //PlayerMovement();
            //        //EnemyBrain();  // find targets, change directions, movement.. AI stuff
            //        //EnemyMovement();
            //        //ShowPositions(); // transform every moved object (this could be done in the movement part too, but sometimes collisions etc. are easier to handle first and then only update the positions after finishing calculations. eg. a Pool-table with balls.

            //        break;

            //    case GameState.LostLife:
            //        break;

            //    case GameState.GameOver:
            //        break;

            //}
        }

        public void StateChange(Enums.GameState newState)
        {

            switch (newState)
            {
                case Enums.GameState.MainMenu:

                    break;

                case Enums.GameState.Intro:
                    //goIntro.audio.play();
                    //ShowBillboard();
                    break;

                case Enums.GameState.GetReady:
                    break;

                case Enums.GameState.LevelRunning:
                    break;

                case Enums.GameState.LostLife:
                    //playerLife--;
                    //if (playerLife&lt;0)
                    //{   
                    //     StateChange(GameState.GameOver);
                    //}
                    //else
                    //{
                    //  // show death sequence
                    //  // then go to GetReady state  
                    //}
                    break;

                case Enums.GameState.GameOver:
                    //goGameOver.audio.play();
                    // and wait until done...

                    break;

                // etc...

            } state = newState;
        }
    }

    

    
}
