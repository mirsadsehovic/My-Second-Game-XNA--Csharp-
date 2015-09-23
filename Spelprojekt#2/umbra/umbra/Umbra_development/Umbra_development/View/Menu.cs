using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Umbra_development.View
{
    public class Menu : Model.ILevelObserver
    {
        Game1 m_game1 = new Game1();
        private SpriteBatch m_spriteBatch;
        private Texture2D m_inviButton2;
        private Texture2D m_newGameButton;
        private Texture2D m_pause;
        private Texture2D m_stageClear;
        private Texture2D m_howToPlay;
        private Texture2D m_gameOver;

        Rectangle clickOne;
        Rectangle clickTwo;
        Rectangle clickThree;

        Rectangle clickBackToMain;
        Rectangle clickResume;
        Rectangle clickQuit;
        Rectangle clickMenuQuit;
        Rectangle clickBack;

        Rectangle clickEasy;
        Rectangle clickMedium;
        Rectangle clickHard;

        Rectangle clickContinue;
        Rectangle clickExit;

        private Texture2D m_menyBackground;
        private Texture2D m_secondMenuBackground;
        private MouseState m_previousMouseState;
        private MouseState m_mouseState;
        private GamePadState m_gamePadState;
        private Texture2D m_customMousePointer;
        private Vector2 mousePosition = Vector2.Zero;
        private Vector2 gamePadPosition = Vector2.Zero;
        private ButtonState lastButtonState;
        private Texture2D m_difficulty;
        GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
        //STATES FÖR ATT KOLLA I VILKET LÄGE SPELETE ÄR
        public enum GameState
        {
            Start,
            Active,
            Pause,
            Help,
            Options,
            Howtoplay,
            Complete,
            GameOver,
            GameCompleted
        }

        GameState m_State = GameState.Start;
        private int m_level = 1;
        private View m_view;

        public Menu(GraphicsDeviceManager a_manager, ContentManager a_contentLoader, View a_view)
        {

            m_view = a_view;
            m_spriteBatch = new SpriteBatch(a_manager.GraphicsDevice);
            m_menyBackground = a_contentLoader.Load<Texture2D>("meny_remake");
            m_inviButton2 = a_contentLoader.Load<Texture2D>("Invibutton2");
            m_gameOver = a_contentLoader.Load<Texture2D>("gameover");
            m_secondMenuBackground = a_contentLoader.Load<Texture2D>("pausscreen");
            m_customMousePointer = a_contentLoader.Load<Texture2D>("handpoint");
            m_pause = a_contentLoader.Load<Texture2D>("gamepause_remake");
            m_stageClear = a_contentLoader.Load<Texture2D>("LevelDone");
            m_howToPlay = a_contentLoader.Load<Texture2D>("howto_remake2.1");
            m_difficulty = a_contentLoader.Load<Texture2D>("difficulty2");
            m_newGameButton = a_contentLoader.Load<Texture2D>("Invibutton2");


        }

        Vector2 pointerPos = new Vector2(20, 20);


        //DRAW MENU, TAR HAN DOM VAD SOM SKALL RITAS UT, BEROENDE PÅ VAD SPELAREN TRYCKER INNAN OCH UNDER SPELET. SPELET ÄR ALDRIG AKTIVT FÖRREN MAN HAR TRYCKT NEW GAME OCH VALT 
        //SVÅRIGHETSGRAD.
        public void DrawMenu(GraphicsDevice graphics, MouseState a_mouseState, bool difficultyIsSet)
        {
            m_spriteBatch.Begin();
            graphics.Clear(Color.White);
            var mouse = Mouse.GetState();
            //ar gamepad = GamePadState;
            Rectangle invibuttonRectangle = new Rectangle(100, 300, 100, 50);
            mousePosition = new Vector2(mouse.X, mouse.Y);
            //OM GAMEPADEN ÄR CONNECTAD SÅ KAN MAN STYRA MED GAMEPADEN.

            //else
            //{
            //m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f,      SpriteEffects.None, 0);

            // }
            //OM SPELET ÄR PAUSAT SÅ KÖRS PAUSE;
            if (m_State == GameState.Pause)
            {
                //var mouse = Mouse.GetState();
                Rectangle pause = new Rectangle(0, 0, (int)(m_pause.Width), (int)(m_pause.Height));
                Rectangle destRect = new Rectangle((graphics.Viewport.Width / 2 - m_pause.Width / 2), (graphics.Viewport.Height / 2 - m_pause.Height / 2), m_pause.Width, m_pause.Height);
                mousePosition = new Vector2(mouse.X, mouse.Y);
                clickResume = new Rectangle(430, 410, m_inviButton2.Width + 350, m_inviButton2.Height + 55);

                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }
                //m_spriteBatch.Begin();

                bool isPadOverRusme = clickResume.Contains((int)m_gamePad.ThumbSticks.Right.X, (int)m_gamePad.ThumbSticks.Right.Y);
                bool isMouseOverResume = clickResume.Contains(m_mouseState.X, m_mouseState.Y);
                m_spriteBatch.Draw(m_inviButton2, clickResume, Color.White);
                m_spriteBatch.Draw(m_pause, destRect, pause, Color.White);

                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }
                else
                {
                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
                m_spriteBatch.Draw(m_inviButton2, clickResume, Color.White);
            }
            //OM LEVEL ÄR AVKLARAD KLICKA PÅ RESUME FÖR ATT FORTSÄTTA.
            else if (m_State == GameState.Complete)
            {


                Rectangle stageClear = new Rectangle(0, 0, (int)(m_stageClear.Width ), (int)(m_stageClear.Height ));
                Rectangle destRect = new Rectangle((graphics.Viewport.Width / 2 - m_stageClear.Width / 2), (graphics.Viewport.Height / 2 - m_stageClear.Height / 2), m_stageClear.Width, m_stageClear.Height);
                mousePosition = new Vector2(mouse.X, mouse.Y);



                clickContinue = new Rectangle(400, 280, m_inviButton2.Width + 400, m_inviButton2.Height + 100);
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }

                m_spriteBatch.Draw(m_inviButton2, clickContinue, Color.Black);
                m_spriteBatch.Draw(m_stageClear, destRect, stageClear, Color.White);


                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }

                else
                {
                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
            }
            //OM STATE ÄR GAME VER RITAS YOU ARE DEAD UT
            else if (m_State == GameState.GameOver)
            {

                Rectangle stageClear = new Rectangle(0, 0, (int)(m_gameOver.Width), (int)(m_gameOver.Height));
                Rectangle destRect = new Rectangle(0, 0, m_stageClear.Width - 200, m_stageClear.Height - 200);
                mousePosition = new Vector2(mouse.X, mouse.Y);

                clickContinue = new Rectangle(520, 340, m_inviButton2.Width + 170, m_inviButton2.Height + 50);

                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }

                m_spriteBatch.Draw(m_inviButton2, clickContinue, Color.Black);
                m_spriteBatch.Draw(m_gameOver, destRect, stageClear, Color.White);


                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
                else
                {
                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
              
            }

            //OM STATE ÄR HELP, RITAS DETTA UT, HELP ÄR NÄR MAN TRYCKT ESC OCH FÅR ALTERNATIVEN ATT GÅ TILL STARTMENY, FORTSÄTTA ELLER STäNGA av
            else if (m_State == GameState.Help)
            {

                Rectangle secondMenu = new Rectangle(0, 0, (int)(m_secondMenuBackground.Width), (int)m_secondMenuBackground.Height);
                Rectangle destRect = new Rectangle(0, 0, m_secondMenuBackground.Width - 100, m_secondMenuBackground.Height - 200);

                //Rectangle secondMenu = new Rectangle(0, 0, graphics.Viewport.Width, m_secondMenuBackground.Height);
                //Rectangle destRect = new Rectangle(0, 0, graphics.Viewport.Width, m_secondMenuBackground.Height);

                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }
                //RITAR UT HELA MENY BILDEN
                mousePosition = new Vector2(mouse.X, mouse.Y);
                clickBackToMain = new Rectangle(400, 350, m_inviButton2.Width + 500, m_inviButton2.Height + 100);
                clickResume = new Rectangle(470, 500, m_inviButton2.Width + 400, m_inviButton2.Height + 50);
                clickQuit = new Rectangle(500, 650, m_inviButton2.Width + 300, m_inviButton2.Height + 40);


                if (m_gamePad.IsConnected)
                {
                    bool isPadOverBackToMain = clickBackToMain.Contains((int)pointerPos.X, (int)pointerPos.Y);
                    bool isPadOverRusume = clickBackToMain.Contains((int)pointerPos.X, (int)pointerPos.Y);
                    bool isPadOverOverQuit = clickBackToMain.Contains((int)pointerPos.X, (int)pointerPos.Y);
                }
                else
                {

                    bool isMouseOverBackToMain = clickBackToMain.Contains(mouse.X, mouse.Y);
                    bool isMouseOverResume = clickResume.Contains(mouse.X, mouse.Y);
                    bool isMOuseOverQuit = clickQuit.Contains(mouse.X, mouse.Y);
                }
                //RITAR UT DE OLIKA KNAPPARNA
                //RITAR UT MUSPEKAREN(HANDEN)

                m_spriteBatch.Draw(m_inviButton2, clickBackToMain, Color.Black);
                m_spriteBatch.Draw(m_inviButton2, clickResume, Color.Black);
                m_spriteBatch.Draw(m_inviButton2, clickQuit, Color.Black);
                m_spriteBatch.Draw(m_secondMenuBackground, destRect, secondMenu, Color.White);

               
                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }

                else
                {
                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }


            }
            //OM STATE ÄR OPTIONS, RITAS SVÅRIHETSGRADERNA UT
            else if (m_State == GameState.Options)
            {

                Rectangle difficultyRectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);
                mousePosition = new Vector2(mouse.X, mouse.Y);
                Rectangle howToPlay = new Rectangle(0, 0, graphics.Viewport.Width, m_difficulty.Height);
                Rectangle destRect = new Rectangle(0, 0, graphics.Viewport.Width, m_difficulty.Height);
                clickBackToMain = new Rectangle(920, 570, m_inviButton2.Width + 100, m_inviButton2.Height + 10);
                clickEasy = new Rectangle(80, 50, m_inviButton2.Width + 400, m_inviButton2.Height + 100);
                clickMedium = new Rectangle(80, 210, m_inviButton2.Width + 500, m_inviButton2.Height + 100);
                clickHard = new Rectangle(80, 390, m_inviButton2.Width + 400, m_inviButton2.Height + 100);
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);

                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }


                m_spriteBatch.Draw(m_inviButton2, clickBackToMain, Color.Black);
                m_spriteBatch.Draw(m_inviButton2, clickEasy, Color.White);
                m_spriteBatch.Draw(m_inviButton2, clickMedium, Color.White);
                m_spriteBatch.Draw(m_inviButton2, clickHard, Color.White);
                m_spriteBatch.Draw(m_difficulty, difficultyRectangle, Color.White);

                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }

                else
                {

                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
               

            }
            //HOW TO PLAY STATEN, ÄR HOW TO PLAY UNDER MENYN
            else if (m_State == GameState.Howtoplay)
            {
                Rectangle howToPlayRectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);
                mousePosition = new Vector2(mouse.X, mouse.Y);
                Rectangle howToPlay = new Rectangle(0, 0, graphics.Viewport.Width, m_howToPlay.Height);
                Rectangle destRect = new Rectangle(0, 0, graphics.Viewport.Width, m_howToPlay.Height);

                clickBackToMain = new Rectangle(1140, 700, m_inviButton2.Width + 50, m_inviButton2.Height + 30);

                if (m_gamePad.IsConnected)
                {

                    float maxSpeed = 10f;
                    Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                    pointerMovement.Y *= -1;
                    if (pointerMovement != Vector2.Zero)
                    {
                        pointerMovement.Normalize();
                    }
                    pointerPos += pointerMovement * maxSpeed;

                    pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                }

                m_spriteBatch.Draw(m_inviButton2, clickBackToMain, Color.Black);
                m_spriteBatch.Draw(m_howToPlay, howToPlayRectangle, Color.White);

                bool isMouseOverBackToMain = clickBack.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverBackToMain = clickOne.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (m_gamePad.IsConnected)
                {
                    m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }
                else
                {
                    m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                }

            }

            //OM INGET AV DET OVASTÅENDE KÖRS SÅ KÖRS MAIN MENU
            else
            {
                if (m_State == GameState.Start)
                {
                    GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);

                    if (m_gamePad.IsConnected)
                    {

                        float maxSpeed = 10f;
                        Vector2 pointerMovement = m_gamePad.ThumbSticks.Right;
                        pointerMovement.Y *= -1;
                        if (pointerMovement != Vector2.Zero)
                        {
                            pointerMovement.Normalize();
                        }
                        pointerPos += pointerMovement * maxSpeed;

                        pointerMovement = new Vector2(pointerPos.X, pointerPos.Y);
                        m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);

                    }


                    Rectangle backgroudnRectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);

                    Rectangle screenRectangle = new Rectangle(0, 0, graphics.Viewport.Width, m_menyBackground.Height);
                    Rectangle DestRectangle = new Rectangle(0, 0, graphics.Viewport.Width, m_menyBackground.Height);
                    clickOne = new Rectangle(80, 250, m_newGameButton.Width + 400, m_newGameButton.Height + 50);

                    clickTwo = new Rectangle(90, 350, m_newGameButton.Width + 400, m_newGameButton.Height + 50);
                    clickThree = new Rectangle(80, 515, m_newGameButton.Width + 400, m_newGameButton.Height + 50);

                    clickExit = new Rectangle(1150, 700, m_newGameButton.Width + 50, m_newGameButton.Height + 30);

                    bool isMouseOver = clickOne.Contains(m_mouseState.X, m_mouseState.Y);
                    bool isPadOver = clickOne.Contains((int)pointerPos.X, (int)pointerPos.Y);

                    bool isMouseOverTwo = clickTwo.Contains(m_mouseState.X, m_mouseState.Y);
                    bool isPadOverTwo = clickTwo.Contains((int)pointerPos.X, (int)pointerPos.Y);


                    bool isMouseOverThree = clickThree.Contains(m_mouseState.X, m_mouseState.Y);
                    bool isPadOverThree = clickThree.Contains((int)pointerPos.X, (int)pointerPos.Y);

                    bool isMouseOverExit = clickExit.Contains(mouse.X, mouse.Y);
                    bool isPadOverQuit = clickExit.Contains(mouse.X, mouse.Y);
                    // m_spriteBatch.Draw(m_menyBackground, DestRectangle, screenRectangle, Color.White);

                    m_spriteBatch.Draw(m_newGameButton, clickExit, Color.White);
                    m_spriteBatch.Draw(m_newGameButton, clickThree, Color.White);
                    m_spriteBatch.Draw(m_newGameButton, clickOne, Color.White);
                    m_spriteBatch.Draw(m_newGameButton, clickTwo, Color.White);
                    m_spriteBatch.Draw(m_newGameButton, clickThree, Color.White);
                    m_spriteBatch.Draw(m_menyBackground, backgroudnRectangle, Color.White);

                    if (m_gamePad.IsConnected)
                    {
                        m_spriteBatch.Draw(m_customMousePointer, pointerPos, null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                    }

                    else
                    {
                        m_spriteBatch.Draw(m_customMousePointer, new Vector2((float)a_mouseState.X, (float)a_mouseState.Y), null, Color.White, 0, new Vector2(m_customMousePointer.Width / 11, m_customMousePointer.Width / 11), 0.5f, SpriteEffects.None, 0);
                    }

                }
            }


            m_spriteBatch.End();
        }


        public void LevelFinished(int a_level)
        {
            m_State = GameState.Complete;
        }
        /* public void LifesLeft(int a_lifes)
         {
             m_view.SetLifes(a_lifes);
         }*/
        public void Death()
        {
            m_State = GameState.GameOver;
            // m_view.SetLevel(a_level);
        }
        public void DidPlayerPressBackToMainMenu()
        {
            m_State = GameState.Start;
        }

        internal bool IsLevelComplete()
        {
            if (m_State == GameState.Complete)
            {
                return true;
            }
            return false;
        }
        internal bool IsEscapePressed()
        {
            if (m_State == GameState.Help)
            {
                return true;
            }
            return false;
        }

        public MouseState getMouseState()
        {
            return m_mouseState = Mouse.GetState();
        }

        public int GetLevel()
        {
            return m_level;
        }

        public bool DidPlayerPressMouse()
        {

            bool pressed = false;

            m_mouseState = Mouse.GetState();



            if (m_mouseState.LeftButton == ButtonState.Released && m_previousMouseState.LeftButton == ButtonState.Pressed)
            {
                pressed = true;


            }

            m_previousMouseState = m_mouseState;


            return pressed;



        }
        public bool DidPLayerPressGamePadA()
        {

            if (m_gamePad.IsButtonDown(Buttons.A))
            {
                return true;
            }
            return false;

        }

        public Model.StateHandler.GameState isPausedPressed()
        {
            GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);

            bool isMouseOverThree = clickOne.Contains(m_mouseState.X, m_mouseState.Y);
            bool isPadOverThree = isMouseOverThree = clickOne.Contains((int)pointerPos.X, (int)pointerPos.Y);
            if (isMouseOverThree == true && DidPlayerPressMouse() == true || isPadOverThree == true && DidPLayerPressGamePadA() == true)
            {
                return Model.StateHandler.GameState.ACTIVE;
            }
            return Model.StateHandler.GameState.PAUSE;
        }
        public Model.StateHandler.GameState isContinuePressed()
        {
            GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
            bool isMouseOverThree = clickOne.Contains(m_mouseState.X, m_mouseState.Y);
            bool isPadOverThree = isMouseOverThree = clickOne.Contains((int)pointerPos.X, (int)pointerPos.Y);


            if (isPadOverThree == true && DidPlayerPressXboxA() == true)
            {
                return Model.StateHandler.GameState.ACTIVE;
            }


            if (isMouseOverThree == true && DidPlayerPressMouse() == true)
            {
                return Model.StateHandler.GameState.ACTIVE;
                // return true;
                // return Model.StateHandler.GameState.ACTIVE;
            }

            // return false;

            return Model.StateHandler.GameState.COMPLETTE;

        }

        public bool isBackToMainMenuPressed()
        {
            if (m_State == GameState.Help || m_State == GameState.Options || m_State == GameState.Howtoplay)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverBackToMain = clickBackToMain.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverBackToMain = clickBackToMain.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverBackToMain == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Start;
                    return true;
                }


                else if (isMouseOverBackToMain == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Start;
                    return true;
                }
            }
            return false;

        }
        public bool isResumeGamePressed()
        {
            if (m_State == GameState.Help || m_State == GameState.Pause)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverResume = clickResume.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverResume = clickResume.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverResume == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;

                }

                if (isMouseOverResume == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;
        }

        public bool isQuitgamePressed()
        {
            if (m_State == GameState.Start || m_State == GameState.Help)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverQuit = clickQuit.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverQuit = clickQuit.Contains((int)pointerPos.X, (int)pointerPos.Y);
                bool isMouseOverExit = clickExit.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverExit = clickExit.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverQuit == true && DidPlayerPressXboxA() || isPadOverExit == true && DidPlayerPressXboxA() == true)
                {
                    return true;
                }
                if (isMouseOverQuit == true && DidPlayerPressMouse() == true || isMouseOverExit == true && DidPlayerPressMouse() == true)
                {
                    return true;
                }
            }
            return false;

        }

        public bool DidPlayerPressNewGame()
        {
            if (m_State == GameState.Start)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);

                bool isMouseOverOne = clickOne.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverOne = clickOne.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverOne == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Options;
                    return true;
                }
                if (isMouseOverOne == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Options;
                    return true;
                }
            }
            return false;
        }
        public bool DidPlayerPressEasy()
        {
            if (m_State == GameState.Options)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverEasy = clickEasy.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverEasy = clickEasy.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverEasy == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }


                if (isMouseOverEasy == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;
        }
        public bool DidPlayerPressMedium()
        {
            if (m_State == GameState.Options)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverMedium = clickMedium.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverMedium = clickMedium.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverMedium == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }



                if (isMouseOverMedium == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;
        }
        public bool DidPlayerPressHard()
        {
            if (m_State == GameState.Options)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverHard = clickHard.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverHard = clickHard.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverHard == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }


                if (isMouseOverHard == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;
        }
        //KOLLAR OM SPELAREN HAR TRYCK PÅ OPTIONS I MENU
        public bool DidPlayerPressOptions()
        {
            if (m_State == GameState.Start)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverThree = clickThree.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverThree = clickThree.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverThree == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Howtoplay;
                    return true;
                }

                if (isMouseOverThree == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Howtoplay;
                    return true;
                }
            }
            return false;
        }
        public bool DidPlayerPressHowToPlay()
        {
            if (m_State == GameState.Start)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverBackToMain = clickBackToMain.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverBackToMain = clickBackToMain.Contains((int)pointerPos.X, (int)pointerPos.Y);
                if (isPadOverBackToMain == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Howtoplay;
                    return true;
                }

                if (isMouseOverBackToMain == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Howtoplay;
                    return true;
                }
            }
            return false;

        }

        //KOLLAR OM SPELAREN HAR TRYCKT LOAD I MENU
        public bool DidPlayerPressLoad()
        {
            if (m_State == GameState.Start)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverTwo = clickTwo.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverTwo = clickTwo.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverTwo == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;

                }
                if (isMouseOverTwo == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;

        }
        //KOLLAR OM SPELAREN HAR TRYCKT CONTINUE I "GAME PAUSED", DEAD, LEVELCOMPLETE
        public bool DidPlayerPressContinue()
        {
            if (m_State == GameState.Complete || m_State == GameState.GameOver)
            {
                GamePadState m_gamePad = GamePad.GetState(PlayerIndex.One);
                bool isMouseOverContinue = clickContinue.Contains(m_mouseState.X, m_mouseState.Y);
                bool isPadOverContinue = clickContinue.Contains((int)pointerPos.X, (int)pointerPos.Y);

                if (isPadOverContinue == true && DidPlayerPressXboxA() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }

                if (isMouseOverContinue == true && DidPlayerPressMouse() == true)
                {
                    m_State = GameState.Active;
                    return true;
                }
            }
            return false;
        }

        private Model.StateHandler m_stateHandler = new Model.StateHandler();
        //KOLLAR OM SPELARN HAR TRYCKT P, 
        internal bool DidPlayerPressPause()
        {
            Keys pause = Keys.P;
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(pause))
            {

                m_State = GameState.Pause;
                //   m_stateHandler.SetGameState(Model.StateHandler.GameState.PAUSE);
                return true;
            }
            return false;
        }
        internal bool DidPlayerPressStart()
        {
            Keys pause = Keys.K;
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(pause))
            {
                m_State = GameState.Active;
                //   m_stateHandler.SetGameState(Model.StateHandler.GameState.PAUSE);
                return true;
            }
            return false;
        }
        private GamePadState m_oldGamePadState;
        //KLICK FÖR ATT TA SIG TILL SECOND MENY SOM INNEHÅLLER QUIT,MAIN MENU, RESUME
        internal bool DidPlayerPressEscape()
        {
            Keys escape = Keys.Escape;
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();
            if (m_oldGamePadState.IsButtonDown(Buttons.Start))
            {

                m_State = GameState.Help;
                //  return true;
            }


            if (keyState.IsKeyDown(escape))//; || m_oldGamePadState.IsButtonDown(Buttons.Start))
            {

                m_State = GameState.Help;
                return true;
            }
            // else
            // {m_State = GameState.Active; }
            m_oldGamePadState = gamepadState;
            return false;

        }

        internal bool DidPlayerPressXboxA()
        {
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            // Process input only if connected.
            if (currentState.IsConnected)
            {
                // Increase vibration if the player is tapping the A button.
                // Subtract vibration otherwise, even if the player holds down A
                if (currentState.Buttons.A == ButtonState.Pressed)
                {

                    return true;

                }


                return false;

                // Update previous gamepad state.
            }
            return false;
        }

        public bool GamePaused()
        {
            if (m_stateHandler.GetGameState() == Model.StateHandler.GameState.PAUSE)
            {
                m_stateHandler.SetGameState(Model.StateHandler.GameState.PAUSE);
                return true;
            }
            return false;
        }
        public bool GamePausedTwo()
        {
            if (m_State == GameState.Pause)
            {
                return true;
            }
            return false;
        }
        internal bool isDeath()
        {
            if (m_State == GameState.GameOver)
            {
                return true;
            }
            return false;
        }

        public bool GameActive()
        {
            if (m_State == GameState.Active)
            {
                return true;
            }
            return false;
        }

        public bool isGameStarted()
        {
            if (m_State == GameState.Start)
            {
                return true;
            }
            return false;
        }
        public bool IsGameEscaped()
        {
            if (m_State == GameState.Help)
            {
                return true;
            }
            return false;
        }
        public bool isGameHowToPlay()
        {
            if (m_State == GameState.Howtoplay)
            {
                return true;
            }
            return false;
        }

    }
}
