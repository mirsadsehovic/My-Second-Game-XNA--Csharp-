//VAR TIDIGARE I DRAWPLAYER: KANSKE KAN ANVÄNDAS TILL ANNAT SENARE

// Rectangle source;
// Rectangle source = GetSourceRectangleRight(a_elapsedTime);
// int x = CheckSpriteMovementRight(a_elapsedTime);
//Rectangle sourceRectangle = new Rectangle(x, 0, (int)(m_playerRightTexture.Width / 9), (int)(m_playerRightTexture.Height));
// Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale /2), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
// m_spriteBatch.Draw(m_playerRightTexture, destRect, Color.White);
//Check if player is facing left or right
//check in what state char is in and choose the correct sprites.

//View
/* if (a_charFaceState == Model.StateHandler.CharFaceState.LEFT)
 {
     if (a_charState == Model.StateHandler.CharStates.STANDING)
     {

     }
     if (a_charState == Model.StateHandler.CharStates.RUNNING)
     {
         Rectangle sourceRectangle = new Rectangle((int)a_scale * 0, 0, (int)a_scale, (int)a_scale);
         m_frame += a_elapsedTime * 12;

         if (m_frame < 1)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 0, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 2)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 1, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 3)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 2, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 4)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 5)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 4, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 6)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 5, 0, m_textureTileSize, m_textureTileSize);
         }
         else if (m_frame < 7)
         {
             sourceRectangle = new Rectangle(m_textureTileSize * 6, 0, m_textureTileSize, m_textureTileSize);
         }
         else
         {
             m_frame = 0;

         }
                  
     }

     if (a_charState == Model.StateHandler.CharStates.JUMPING)
     {


     }
     if (a_charState == Model.StateHandler.CharStates.FALLING)
     {


     }
     Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 2), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
   //  m_spriteBatch.Draw(m_playerLeftTexture, destRect, sourceRectangle, Color.White);
             
 }

 else
 {
     if (a_charState == Model.StateHandler.CharStates.STANDING)
     {

     }
    if (a_charState == Model.StateHandler.CharStates.FALLING)
     {
         if (a_charFaceState == Model.StateHandler.CharFaceState.RIGHT && a_charState == Model.StateHandler.CharStates.RUNNING)
         {

             Rectangle sourceRectangle = new Rectangle((int)a_scale * 0, 0, (int)a_scale, (int)a_scale);
             m_frame += a_elapsedTime * 12;

             if (m_frame < 1)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 0, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 2)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 1, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 3)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 2, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 4)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 5)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 4, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 6)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 5, 0, m_textureTileSize, m_textureTileSize);
             }
             else if (m_frame < 7)
             {
                 sourceRectangle = new Rectangle(m_textureTileSize * 6, 0, m_textureTileSize, m_textureTileSize);
             }
             else
             {
                 m_frame = 0;


             }
            // Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 2), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
            // m_spriteBatch.Draw(m_playerRightTexture, destRect, sourceRectangle, Color.White);
         }

     }
     if (a_charState == Model.StateHandler.CharStates.JUMPING)
     {

     }
     if (a_charState == Model.StateHandler.CharStates.FALLING)
     {


     }

 }
  */
//VAR I MENU INNAN
/*  public void DrawPause(GraphicsDevice a_graphicsDevice, MouseState a_mouseState)
    {
        if (m_State == GameState.Pause)
        {
            var mouse = Mouse.GetState();
            mousePosition = new Vector2(mouse.X, mouse.Y);
            bool isMouseOver = clickOne.Contains(m_mouseState.X, m_mouseState.Y);
            clickThree = new Rectangle(160, 570, m_customMousePointer.Width + 100, m_customMousePointer.Height);

            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.WhiteSmoke);
            Rectangle pause = new Rectangle(0, 0, (int)(m_pause.Width), (int)(m_pause.Height));
            Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_pause.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_pause.Height / 2), m_pause.Width, m_pause.Height);
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_customMousePointer, clickThree, Color.Black);
            m_spriteBatch.Draw(m_pause, destRect, pause, Color.White);
            m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

            m_spriteBatch.End();
        }
            
    }*/

//Menu
/* public void DrawStageClear(GraphicsDevice a_graphicsDevice, MouseState a_mouseState)
 {
     a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.WhiteSmoke);
     Rectangle pause = new Rectangle(0, 0, (int)(m_pause.Width), (int)(m_pause.Height));
     Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_pause.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_pause.Height / 2), m_pause.Width, m_pause.Height);
     m_spriteBatch.Begin();
     m_spriteBatch.Draw(m_customMousePointer, clickThree, Color.Black);
     m_spriteBatch.Draw(m_pause, destRect, pause, Color.White);
     m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f,                        SpriteEffects.None, 0);
     m_spriteBatch.End();

 }*/
// Menu
/* public void DrawSecondMenu(GraphicsDevice a_graphicsDevice, MouseState a_mouseState)
  { 
      var mouse = Mouse.GetState();
      bool isMouseOverBackToMain = clickBackToMain.Contains(mouse.X, mouse.Y);
      bool isMouseOverResume = clickResume.Contains(mouse.X, mouse.Y);
      bool isMOuseOverQuit = clickQuit.Contains(mouse.X, mouse.Y);
     //clickThree = new Rectangle(160, 570, m_customMousePointer.Width + 100, m_customMousePointer.Height);

      //SÄTTER UT POSITION FÖR "KNAPPARNA" SOM SKA FINNA I DEN ANDRA MENYN. LITE MODEFIERING BEHÖVS
      clickBackToMain = new Rectangle(600, 320, m_inviButton2.Width + 100, m_inviButton2.Height);
      clickResume = new Rectangle(600, 373, m_inviButton2.Width + 100, m_inviButton2.Height);
      clickQuit = new Rectangle(600, 440, m_inviButton2.Width + 100, m_inviButton2.Height);
      a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
     // Rectangle pause = new Rectangle(0, 0, (int)(m_secondMenuBackground.Width), (int) m_secondMenuBackground.Height);
      Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 -  m_secondMenuBackground.Width / 2), (a_graphicsDevice.Viewport.Height / 2 -  m_secondMenuBackground.Height / 2),  m_secondMenuBackground.Width, m_secondMenuBackground.Height);
      m_spriteBatch.Begin();
      //RITAR UT HELA MENY BILDEN
      m_spriteBatch.Draw( m_secondMenuBackground, destRect, Color.White);
      //RITAR UT DE OLIKA KNAPPARNA
      m_spriteBatch.Draw(m_inviButton2, clickBackToMain, Color.Black);
      m_spriteBatch.Draw(m_inviButton2, clickResume, Color.Black);
      m_spriteBatch.Draw(m_inviButton2, clickQuit, Color.Black);
      //RITAR UT MUSPEKAREN(HANDEN)
      m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f,                        SpriteEffects.None, 0);          
      m_spriteBatch.End();
  }*/

// Get old player position , the model class
/*  Vector2 oldPos = m_player.GetPosition();
  //Get the new player position
  m_player.Update(a_elapsedTime);
  Vector2 newPos = m_player.GetPosition();
  m_hasCollidedWithGround = false;   
  Vector2 speed = m_player.GetSpeed();
  Vector2 afterCollidedPos = Collide(oldPos, newPos, m_player.m_sizes, ref speed, out m_hasCollidedWithGround, a_elapsedTime);
  //set the new speed and position after collision
  m_player.SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
  m_player.SetSpeed(speed.X, speed.Y);
  */


/*internal void Draw(float a_elapsedTime, Microsoft.Xna.Framework.Graphics.GraphicsDevice GraphicsDevice, GraphicsDeviceManager a_graphicDeviceManager, Model.StateHandler.CharStates a_charState,    Model.StateHandler.CharFaceState a_charFaceState)
        {
           
            if (m_statehandler.GetGameState() == Model.StateHandler.GameState.ACTIVE)
           //f(m_menu.GameActive())
             //{
            //if(m_menu.DidPlayerPressNewGame())
            {
                    levels = m_model.GetLevel();
                    View.View view = m_view;

                    m_camera.CenterOn(m_model.GetPlayerPosition(), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                    new Vector2(Model.Levels.g_levelWidth, Model.Levels.g_levelHeight));
                    m_camera.SetZoom(50);
                    m_view.DrawLevel(GraphicsDevice, levels, m_camera, m_model.GetPlayerPosition(), a_elapsedTime, m_model.GetLevelState(), a_graphicDeviceManager, a_charState, a_charFaceState, m_model.GetEnemyPositions(), m_model.GetBoltEnemyPositions());

          }

            //else
            //{
             //   m_menu.DrawMenu(GraphicsDevice, m_mouseState);
            ///}
            else if (m_statehandler.GetGameState() == Model.StateHandler.GameState.MENU)
            {
                m_menu.DrawMenu(GraphicsDevice, m_mouseState);
            }
            
            if (m_menu.IsLevelComplete())
            {
                m_menu.DrawStageClear(GraphicsDevice, m_mouseState);
            }

            if (m_statehandler.GetGameState()==Model.StateHandler.GameState.PAUSE) 
            {
                   m_menu.DrawPause(GraphicsDevice, m_mouseState);   
            }
            if(m_menu.IsGameEscaped())
            {
                m_menu.DrawSecondMenu(GraphicsDevice, m_mouseState);
            }
            if (m_menu.isBackToMainMenuPressed())
            {
                m_menu.DrawMenu(GraphicsDevice, m_mouseState);
            }
         
           
           // if(m_statehandler.GetGameState()==Model.StateHandler.GameState.COMPLETTE)
           // {
              //   m_menu.DrawStageClear(GraphicsDevice, m_mouseState);
          //  }
           // /if (m_statehandler.GetGameState() == Model.StateHandler.GameState.COMPLETTE)
            //{
           //     m_menu.DrawStageClear(GraphicsDevice, m_mouseState);
           // }
            

        }
        internal void ContinueGame()
        {
            m_model = new Model.Model();
        }

        public void Update(float a_elapsedTime)
        {
            m_mouseState = m_menu.getMouseState();
            if (m_menu.isBackToMainMenuPressed())
            {
                m_menu.isBackToMainMenuPressed();
            }
            if (RunGame())
            {
            // levels = m_model.GetLevel();
            m_sound.CountSoundTimer(a_elapsedTime);
            if (m_sound.SoundTimer(m_sound.GetSoundTimer()))
            {
                m_sound.PlayRandomSound(m_sound.GetSoundCollection());
            }

            if (m_statehandler.GetGameState() == Model.StateHandler.GameState.ACTIVE)
            {
                m_sound.StartSoundTimer();
                m_sound.PlayBackgroundSound();

            }


            if (m_view.DidPlayerPressJump())
            {
                if (m_model.CanJump())
                {
                    m_model.DoJump();
                    m_model.GetPlayerSpeed();
                    m_statehandler.SetCharState(Model.StateHandler.CharStates.JUMPING);
                }
            }

            if (m_view.DidPlayerPressRight(m_model.GetPlayerSpeed()) == true)
            {
                m_model.DoRight();
                //m_view.MoveBgRight();
                //  m_statehandler.SetCharFaceState(Model.StateHandler.CharFaceState.RIGHT);
                //m_statehandler.SetCharState(Model.StateHandler.CharStates.RUNNING);      
            }
            if (m_view.DidPlayerPressLeft(m_model.GetPlayerXSpeed()) == true)
            {
                m_model.DoLeft();
                //m_view.MoveBgLeft();
                // m_statehandler.SetCharFaceState(Model.StateHandler.CharFaceState.LEFT);
                // m_statehandler.SetCharState(Model.StateHandler.CharStates.RUNNING);
            }

            // if (m_statehandler.GetGameState() == Model.StateHandler.GameState.MENU)
            // {
            //    m_statehandler.SetGameState(m_menu.DidPlayerPressNewGame());
            //    m_model.GetLevel();
            // }
            if (m_statehandler.GetGameState() == Model.StateHandler.GameState.PAUSE)
            {
                m_statehandler.SetGameState(m_menu.isPausedPressed());
            }
            //if (m_statehandler.GetGameState() == Model.StateHandler.GameState.COMPLETTE)
            //  {
            //     m_statehandler.SetGameState(m_menu.isContinuePressed());
            //  }
          

                if (m_menu.IsLevelComplete())
                {
                    ContinueGame();
                }
                if (m_view.DidPlayerPressSave())
                {
                    m_model.DoSave();
                }
                if (m_menu.DidPlayerPressLoad())
                {
                    m_model.DoLoad();
                    // m_statehandler.SetGameState(Model.StateHandler.GameState.ACTIVE);
                }
                if (m_menu.DidPlayerPressNewGame())
                {
                    m_model.GetLevel();
                   // m_statehandler.SetGameState(Model.StateHandler.GameState.ACTIVE);
                }
                if (m_menu.DidPlayerPressPause() == true)
                {
                    m_statehandler.SetGameState(Model.StateHandler.GameState.PAUSE);

                }
                if (m_menu.DidPlayerPressContinue())
                {
                    m_statehandler.SetGameState(Model.StateHandler.GameState.ACTIVE);
                    m_model.GetLevel();
                }
                
                if (m_menu.DidPlayerPressEscape())
                {
                    m_menu.DidPlayerPressEscape();
                }
                
                    
            }
            if (m_menu.IsLevelComplete() == true)
            {
                //m_statehandler.SetGameState(Model.StateHandler.GameState.COMPLETTE);
                ContinueGame();

            }
            
           
            
        

            /*  if (m_model.isLevelComplete())
                   {
                    
                  if (m_statehandler.GetLevelState() == Model.StateHandler.LevelState.LEVEL1)
                       {
                           m_statehandler.SetLevelState(Model.StateHandler.LevelState.LEVEL2);
                       }
                       ContinueGame();
                      */
/* if(m_statehandler.GetLevelState() == Model.StateHandler.LevelState.LEVEL2)
  {
       m_statehandler.SetLevelState(Model.StateHandler.LevelState.LEVEL2);
  }
// }



m_model.Update(a_elapsedTime, m_view, m_menu);
m_view.UpdateBg(m_manager);


}*/

/*   internal bool RunGame()
   {

       if (m_menu.DidPlayerPressNewGame())
       {
           return true;
       }
       if (m_menu.DidPlayerPressPause())
       {
           return true;
       }
               
       if(m_menu.IsLevelComplete())
       {
           ContinueGame();
            return false;
       }
       if (m_menu.GameActive())
       {
           return true;
       }
       if (m_menu.GamePausedTwo())
       {
           return true;
       }

               

       return false;
   }

}
*/
//I VIEW
/* public void SetLifes(int a_playerHp)
{
    m_playerHp = a_playerHp;
}*/
/* public void DrawLevelComplete(GraphicsDevice graphics, MouseState a_mouseState)
 {
     m_spriteBatch.Begin();
     graphics.Clear(Color.Transparent);
     Rectangle screenRectangle = new Rectangle(0, 0, graphics.Viewport.Width, m_continueLevel.Height);
     Rectangle DestRectangle = new Rectangle(0, 0, graphics.Viewport.Width, m_continueLevel.Height);
     m_spriteBatch.Draw(m_continueLevel, DestRectangle, screenRectangle, Color.White);
     m_spriteBatch.End();
 }*/

//MUSE FUNCTION VIEW
/*if (m_currentState == PlayerState.Right)
{
    Vector2 stopMousePosition = new Vector2(playerViewPos.X + 10, playerViewPos.Y);
    Vector2 reticlePosition = mousePosition;
    if (mousePosition.X < stopMousePosition.X)
    {
        Mouse.SetPosition((int)stopMousePosition.X, (int)mousePosition.Y);
    }
    else
    {
        reticlePosition.X = mousePosition.X;

    }
    m_spriteBatch.Draw(m_torch, reticlePosition, Color.White);
}
else
{
    Vector2 stopMousePosition = new Vector2(playerViewPos.X - 10, playerViewPos.Y);
    Vector2 reticlePosition = mousePosition;
    if (mousePosition.X > stopMousePosition.X)
    {
        Mouse.SetPosition((int)stopMousePosition.X, (int)mousePosition.Y);
    }
    else
    {
        reticlePosition.X = mousePosition.X;

    }
    m_spriteBatch.Draw(m_torch, reticlePosition, Color.White);
}*/

// m_smoke.DrawSmoke(m_time, displacement, m_viewScale, m_spriteBatch, m_smokeTexture);


//  DrawPlayerAt(playerViewPos, scale, a_elapsedTime, a_graphics);
// DrawPlayerAt(playerViewPos, a_scale, a_elapsedTime, a_graphics);
// foreach (var position in torchPositions)
// {
//     m_spriteBatch.Draw(torch, position, Color.White);
// }
// m_spriteBatch.Draw(torch, mousePosition, Color.White);
//m_spriteBatch.End();
//VIEW
/* if (m_level == 2)
        {
            const int LIGHTOFFSET = 115;
            torchPositions.Add(new Vector2(40, 420));
            torchPositions.Add(new Vector2(340, 420));
            torchPositions.Add(new Vector2(640, 420));
            foreach (var position in torchPositions)
            {
                var new_pos = new Vector2(position.X - LIGHTOFFSET, position.Y - LIGHTOFFSET);
                m_spriteBatch.Draw(m_lightMask2, new_pos, Color.Red);
            }
           // m_spriteBatch.Draw(m_lightMask2, torchPositions ,Color.Red);

        }*/
// m_spriteBatch.Draw(m_lightmask, lightPosition, null, Color.Red, angle, axisPos, 0.9f, SpriteEffects.None, 0f);


//m_spriteBatch.Draw(m_lightmask, lightPosition, null, Color.White, angle, axisPos, 0.9f, SpriteEffects.None, 0f);
//  m_spriteBatch.Draw(m_torch, mousePosition, Color.White);
