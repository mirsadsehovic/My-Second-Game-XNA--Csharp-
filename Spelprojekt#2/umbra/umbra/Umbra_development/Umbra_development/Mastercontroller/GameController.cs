
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Umbra_development.Mastercontroller
{
    class GameController
    {
     //   private Model.SoundManager m_sound;
        private Model.Levels m_levels = new Model.Levels();
        Game1 m_game1 = new Game1();
        private View.Menu m_menu;
        private View.View m_view;
        private Model.Model m_model;
        private Model.Player m_player = new Model.Player();
        private View.Camera m_camera;
        private GraphicsDeviceManager m_manager;
        private Model.StateHandler m_statehandler = new Model.StateHandler();
        private Model.Enemy m_enemy = new Model.Enemy();
        private Model.EnemyBolt m_enemyBolt;
        private Model.Save m_save;
        private Model.ILevelObserver m_levelObserver;

        public GameController(View.View a_view, View.Camera a_camera, View.Menu a_menu, GraphicsDeviceManager a_manager, ContentManager a_contentManager)
        {
          
            m_enemyBolt = new Model.EnemyBolt();
            m_view = a_view;
            m_menu = a_menu;
            m_camera = a_camera;
            m_manager = a_manager;
            m_model = new Model.Model(m_player, m_statehandler, m_levels, m_enemy, m_enemyBolt);
        //    m_sound = new Model.SoundManager(a_contentManager);
            m_save = new Model.Save(m_player, m_statehandler, m_levels, m_model);
        }
        
        internal void Draw(float a_elapsedTime, GraphicsDevice GraphicsDevice, GraphicsDeviceManager a_graphicDeviceManager, Model.StateHandler.CharFaceState a_charFaceState, Model.StateHandler.CharStates a_charState, bool difficultyIsSet)
        {
           
                Model.Levels levels = m_model.GetLevel();
                m_camera.CenterOn(m_model.GetPlayerPosition(), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), new Vector2(Model.Levels.g_levelWidth, Model.Levels.g_levelHeight));
                m_camera.SetZoom(50);
                m_view.DrawLevel(GraphicsDevice, levels, m_camera, m_model.GetPlayerPosition(), a_elapsedTime, m_model.GetLevelState(), a_graphicDeviceManager, a_charState, a_charFaceState, m_model.GetEnemyPositions(), m_model.GetBoltEnemyPositions(), m_model.GetLife(), m_model.isPlayerInAir());
               // m_sound.StartSoundTimer();
               // m_sound.PlayBackgroundSound(m_levels.GetLevel());
            
     }
        int difficulty;

        internal void Update(float a_elapsedTime)
        {

            m_view.UpdateEnemyTexture(m_player.GetPosition(), m_enemy.GetPosition());

            if (m_view.GetBackground() != null)
            {
                m_view.UpdateBg(m_manager);
            }

            m_view.MoveBackground(m_model.GetPlayerXSpeed());


            if (m_statehandler.GetGameState() != Model.StateHandler.GameState.ACTIVE)
            {
                if (m_menu.DidPlayerPressEscape() || m_menu.DidPlayerPressPause())
                {
                  //  m_sound.StopSound();
                   // m_sound.StopSoundTimer();
                }               
                else if (m_menu.isDeath() || m_model.EndReached())
                {
                   // m_sound.StopSound();
                   // m_sound.StopSoundTimer();
                }
            }

          //  m_sound.CountSoundTimer(a_elapsedTime);
          ////  if (m_sound.SoundTimer(m_sound.GetSoundTimer()))
           // {
            //    m_sound.PlayRandomSound(m_sound.GetSoundCollection(m_levels.GetLevel()));
           // }

            //Kollar om värdet på liven är under 40 procent. byter sedan färg på ficklampan och sätter igång hjärtljud
            if (m_model.IsHealthBelowFortyPercent() && m_menu.GameActive())
            {
                m_view.FlashLightColor(true);
                m_sound.PlayHeartBeat();
            }
            //Om inte så är det vit lampa och inget ljud
            else
            {
                m_view.FlashLightColor(false);
             //   m_sound.StopHeartBeat();
            }

            //Kollar om man har tryckt hoppa och om han får hoppa. Förhindrar dubbelhopp.
            if (m_view.DidPlayerPressJump())
            {
                if (m_model.CanJump())
                {
                    m_view.DoJump();
                    m_model.DoJump();
                    m_model.GetPlayerSpeed();
                }
            }
            //KOLLAR OM SPELAREN TRYCKT SAVE OCH SAPAR SEDAN.
            if (m_view.DidPlayerPressSave())
            {
                m_model.DoSave(m_player, m_statehandler, m_levels, m_model, m_save);
            }
            //KOLLAR OM MAN HAR TRYCKT PÅ LOADGAME DÄREFTER LADDAR.
            if (m_menu.DidPlayerPressLoad())
            {
                //m_model.DoLoad();
                // m_statehandler.SetGameState(Model.StateHandler.GameState.ACTIVE);
            }
            //Kollar om man har tryckt vänster och flyttar spelaren Vänster.
            if (m_view.DidPlayerPressLeft(m_model.GetPlayerSpeed()) == true)
            {
                m_model.DoLeft();
            }

            //Kollar om man har tryckt höger och flyttar spelaren höger.
            if (m_view.DidPlayerPressRight(m_model.GetPlayerSpeed()) == true)
            {
                m_model.DoRight();
            }
            //KOLLAR OM SPELAREN HAR TRYCKT NEW GAME OCH GENERERAR DÄREFTER BANAN.
            if (m_menu.DidPlayerPressEasy() && m_model.DifficultyIsSet() == false)
            {
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.EASY);
             
            }

            if (m_menu.DidPlayerPressMedium() && m_model.DifficultyIsSet() == false)
            {
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.MEDIUM);

            }

            if (m_menu.DidPlayerPressHard() && m_model.DifficultyIsSet() == false)
            {
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.HARD);
             
            }

            if (m_model.EndReached())
            {
                m_menu.LevelFinished(m_levels.GetLevel());
            }

            m_model.Update(a_elapsedTime, m_view, m_menu, difficulty);

        }
        internal void ContinueGame()
        {
            m_model = new Model.Model(m_player, m_statehandler, m_levels, m_enemy, m_enemyBolt);
            m_view.SetLevel(m_levels.GetLevel());  
        }
        //Resettar värden om man väljer att köra new game.
        internal void NewGame()
        {

            //m_levels.UnloadEnemyPositions();
            m_levels.ResetLevel();
            m_model = new Model.Model(m_player, m_statehandler, m_levels, m_enemy, m_enemyBolt);
            
            m_player.SetToStartPosition(m_levels.GetLevel());
            m_player.SetSpeed(0, 0);

            m_levels.GenerateLevel();
            m_view.SetLevel(m_levels.GetLevel());
        }
        //Laddar in värden om man har tryckt loadgame, så som, vilken bam vilken svårighetsgrad osv.
        internal void LoadGame()
        {
            m_levels.SetLevel(m_save.GetSavedLevel());
            m_model = new Model.Model(m_player, m_statehandler, m_levels, m_enemy, m_enemyBolt);
            m_player.SetPosition(m_save.GetSavedPosition().X, m_save.GetSavedPosition().Y);
            
            m_levels.GenerateLevel();
            
            m_statehandler.SetDifficulty(m_save.GetSavedDifficulty());
            m_model.SetDifficulty(m_save.GetSavedDifficulty());

            m_player.SetLife(m_save.GetSavedHealth());
            m_view.SetLevel(m_levels.GetLevel());
           
            
            
        }
        //Resettar spelet, vid eventuell död.
        internal void ResetGame()
        {
            m_model = new Model.Model(m_player, m_statehandler, m_levels, m_enemy, m_enemyBolt);
        }
        //Kör spelet, beroende på knapptryck
        internal bool RunGame(Model.ILevelObserver a_observer)
        {
            if (m_menu.DidPlayerPressPause())
            {
               return true;
            }
            if (m_menu.DidPlayerPressEscape())
            {
                return true;
            }
            if (m_menu.DidPlayerPressOptions())
            {
                return true;
            }
           
            if(m_menu.isBackToMainMenuPressed())
            {
                return true;
            }
         
            if (m_menu.GameActive())
            {
                return true;
            }

            if (m_view.DidPlayerPressSave())
            {
                return true;
            }

            //Kollar om man inte har nått slutet av banan, eller har, 
            if (m_menu.DidPlayerPressContinue())
            {
                if (!m_model.EndReached())
                {
                    m_model.DoRevive(m_levels.GetLevel());
                }
                else
                {
                    m_levels.NextLevel();
                    ContinueGame();
                    m_player.SetToStartPosition(m_levels.GetLevel());

                    m_model.SetDifficulty(m_statehandler.GetDifficulty());
                }
            }

            if (m_menu.DidPlayerPressNewGame())
            {
              //  ContinueGame();
                return true;
            }
            //KOLLAR OM SPELAREN HAR VALT EASY:SÄTTER DIFFICLYT TILL EASY
            if (m_menu.DidPlayerPressEasy())
            {
                NewGame();
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.EASY);
                m_model.SetDifficulty(m_statehandler.GetDifficulty());
                
                
                //ContinueGame();
                return true;
            }
            //OM MAN HAR KLICKA MEDIUM; SÄTTER DIFFICULTY
            if (m_menu.DidPlayerPressMedium())
            {
                NewGame();
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.MEDIUM);
                m_model.SetDifficulty(m_statehandler.GetDifficulty());
                
                return true;
            }
            //KOLLAR OM SPELAREN HAR VALT HART: SÄTTER DIFFICULT TILL HARD
            if (m_menu.DidPlayerPressHard())
            {
                NewGame();
                m_statehandler.SetDifficulty(Model.StateHandler.Difficulty.HARD);
                m_model.SetDifficulty(m_statehandler.GetDifficulty());
                
                return true;
            }
            //KOLLAR OM SPELAREN TRYCKT LOAD(CONTINUE I MENYN)
            if (m_menu.DidPlayerPressLoad())
            {
                m_model.DoLoad(m_save);
                LoadGame();
                return true;
            }
            
            if (m_menu.isResumeGamePressed())
            {
                return true;
            }
            if (m_menu.IsLevelComplete())
            {
                ContinueGame();
                return true;
            }
            if (m_menu.isDeath())
            {
                ResetGame();
                m_model.SetDifficulty(m_statehandler.GetDifficulty());
                return true;
            }

            return false;

        }
    
    }

    
}
