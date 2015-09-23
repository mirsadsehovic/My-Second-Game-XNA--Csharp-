using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;


namespace Umbra_development.View
{
    public class View:Model.SoundObserver
    {
        private Model.Levels m_levels = new Model.Levels();
        
        private AshesSystem a_ashSystem = new AshesSystem();
        private Texture2D m_ashes;

        private RainSystem a_rainSystem = new RainSystem();
        private SpriteBatch m_spriteBatch;
        //sprites
        private Texture2D m_playerLeftTexture;
        private Texture2D m_playerRightTexture;
        private Texture2D m_playerJumpTexture;
        private Texture2D m_tileTextures;
        private Texture2D m_tileTextures2;
        private Texture2D m_backGround;
        private Texture2D m_forestBg;
        private Texture2D m_sewerBg;
        private Texture2D m_villageBg;
        private Texture2D m_sewerBgCopy;
        private Texture2D m_housebgOne;
        private Texture2D m_housebgTwo;
        private Texture2D m_backGroundLayerOne;
        private Texture2D m_backGroundLayerOneCopy;
        private Texture2D m_backGroundLayerTwoo;
        private Texture2D m_backGroundLayerTwooCopy;
        private Texture2D m_backGroundLayerThree;
        private Texture2D m_backGroundLayerThreeCopy;
        private Texture2D m_backGroundLayerFour;
        private Texture2D m_backGroundLayerFourCopy;
        private Texture2D m_backGroundLayerFive;
        private Texture2D m_backGroundLayerFiveCopy;
        private Texture2D m_enemyStandingLeftTexture;
        private Texture2D m_enemyStandingRightTexture;
        private Texture2D m_enemyChargeTexture;
        private Texture2D m_rainTexture;
        private Texture2D m_inviButton;
        private Texture2D m_customMousePointer;
        private Texture2D m_menyBackground;
        private Texture2D m_continueLevel;        
        private Texture2D m_thunderTexture;
        private Texture2D m_boltEnemy;
        private Texture2D m_torch;
        private SpriteFont a_hpFont;
        private Texture2D m_bullets;
        private Texture2D m_lightMask2;
        //Sounds
        private Song a_somg;
        private SoundEffect m_screamSound;
        private SoundEffectInstance m_jumpSound;
        private SoundEffect m_song;

        //Shader
        private Texture2D m_blackSquare;
        private Texture2D m_lightmask;
        private RenderTarget2D mainScene;
        private RenderTarget2D lightMask;
        //EFFECT
        private Effect lightningEffect;
        //MOUSE
        private List<Vector2> torchPositions;
        private Vector2 mousePosition = Vector2.Zero;
        const int LIGHTOFFSET_X = 0;
        const int LIGHTOFFSET_Y = 40;       
        PresentationParameters pp;
       
     
        //Controlls
        private KeyboardState m_oldKeyboardState;

        //other
        private Vector2 viewPos;
        private int m_backGroundBasePos;
        private int m_backGroundLayerOnePos;
        private int m_backGroundLayerTwooPos;
        private int m_backGroundLayerThreePos;
        private int m_backGroundLayerFourPos;
        private int m_backGroundLayerFivePos;
        private int m_frameCount;
        private Rectangle TileRectangle;
        private Rectangle destRect;
      
        float timer = 0f;
        private int movement = 1;
        private float m_time = 5;
        float interval = 0.10f;
        private Vector2 hpPos = new Vector2(3.0f, 2.0f);
        private float m_viewScale = 100;
        private bool m_isInAir;
        private Color flashLightColor = Color.White;
        private int m_textureTileSize = 64;

     
        public static int m_level = 1;
        public int m_lifes = 100;

        private enum State
        {
            Standing,
            Walking,
            Jumping
        }

        State m_state = State.Standing;
        Point frameSize = new Point(105, 130);

        private enum PlayerState
        {
            Left,
            Right,
            Up
        }
        PlayerState m_currentState = PlayerState.Right;
        public AvatarAnimation playerRight;
        private Vector2 m_hpPos = new Vector2(3.0f, 2.0f);
        private SpriteFont m_hpFont;

        public View(GraphicsDeviceManager a_manager, ContentManager a_contentLoader, GraphicsDevice a_graphics)
        {
            //Create an instance of spritebatch
         
            GraphicsDeviceManager m_manager = a_manager;
            m_spriteBatch = new SpriteBatch(a_manager.GraphicsDevice);
            ContentManager m_contentloader = a_contentLoader;
            GraphicsDevice m_graphics = a_graphics;
            Viewport viewport = a_manager.GraphicsDevice.Viewport;
            //Load sprites
            m_ashes = a_contentLoader.Load<Texture2D>("Ashes");
            m_tileTextures2 = a_contentLoader.Load<Texture2D>("Tiles2.9");
            m_playerRightTexture = a_contentLoader.Load<Texture2D>("newruncharright");
            m_playerLeftTexture = a_contentLoader.Load<Texture2D>("newruncharleft2");
            m_enemyStandingLeftTexture = a_contentLoader.Load<Texture2D>("ghost");
            m_enemyStandingRightTexture = a_contentLoader.Load<Texture2D>("ghostRight");
            m_enemyChargeTexture = a_contentLoader.Load<Texture2D>("ghost3charge");
            m_rainTexture = a_contentLoader.Load<Texture2D>("raindrop");
            m_playerJumpTexture = a_contentLoader.Load<Texture2D>("charjump");
            m_customMousePointer = a_contentLoader.Load<Texture2D>("handpoint");
         
            m_thunderTexture = a_contentLoader.Load<Texture2D>("thunder");
            m_boltEnemy = a_contentLoader.Load<Texture2D>("ghostbolt1");
            m_torch = a_contentLoader.Load<Texture2D>("torch");
            m_sewerBg = a_contentLoader.Load<Texture2D>("sewerbg");
            m_villageBg = a_contentLoader.Load<Texture2D>("villagebg1");
            m_bullets = a_contentLoader.Load<Texture2D>("Fireball");
//-------------------------------------------SOUND----------------------------------------------------
            m_screamSound = a_contentLoader.Load<SoundEffect>("FemaleScream");
            m_jumpSound = a_contentLoader.Load<SoundEffect>("14").CreateInstance();
            m_jumpSound.Volume = 0.1f;

            m_song = a_contentLoader.Load<SoundEffect>("FemaleScream");
//-------------------------------------------Shader----------------------------------------------------
            m_blackSquare = a_contentLoader.Load<Texture2D>("blacksquare");
            m_lightmask = a_contentLoader.Load<Texture2D>("light2");
            m_lightMask2 = a_contentLoader.Load<Texture2D>("lightmask");
            lightningEffect = a_contentLoader.Load<Effect>("lightningEffect");
            
            var pp = a_manager.GraphicsDevice.PresentationParameters;
            mainScene = new RenderTarget2D(a_manager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            lightMask = new RenderTarget2D(a_manager.GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
//------------------------------------------------------------------------------------------------------
            //Check levelstate and choose right sprites for background
            m_hpFont = a_contentLoader.Load<SpriteFont>("SpriteFont1");

            if(m_level ==1)
           // if (a_levelState == Model.StateHandler.LevelState.LEVEL1)
           {
                m_tileTextures = a_contentLoader.Load<Texture2D>("Tiles3");
                m_forestBg = a_contentLoader.Load<Texture2D>("lvl1bg");
                m_backGroundLayerOne = a_contentLoader.Load<Texture2D>("grasbg");
                m_backGroundLayerOneCopy = a_contentLoader.Load<Texture2D>("grasbg");
                m_backGroundLayerTwoo = a_contentLoader.Load<Texture2D>("lvl1bg1");
                m_backGroundLayerTwooCopy = a_contentLoader.Load<Texture2D>("lvl1bg1");
                m_backGroundLayerThree = a_contentLoader.Load<Texture2D>("lvl1bg2");
                m_backGroundLayerThreeCopy = a_contentLoader.Load<Texture2D>("lvl1bg2");
                m_backGroundLayerFour = a_contentLoader.Load<Texture2D>("lvl1bg3");
                m_backGroundLayerFourCopy = a_contentLoader.Load<Texture2D>("lvl1bg3");
                m_backGroundLayerFive = a_contentLoader.Load<Texture2D>("lvl1bg4");
                m_backGroundLayerFiveCopy = a_contentLoader.Load<Texture2D>("lvl1bg4");
           }
           if(m_level ==2)
           {

                m_tileTextures2 = a_contentLoader.Load<Texture2D>("Tiles2.9");
               
           }
   
                SoundEffect.MasterVolume = 0.5f;

        }

          private void PlaySound(SoundEffect a_sound)
          {
              if (a_sound != null)
              {
                  a_sound.Play();
              }
          }
           
          public void ScreamSound()
          {
              PlaySound(m_screamSound);
          }
          private bool PlaySong(SoundEffect a_song)
          {
              if (a_song != null)
              {
                 a_song.Play();
              }
              return false;
          }
          public void ChaseSound()
          {
              PlaySong(m_song);
          }

          //
          public static int m_playerHp = 100;
         
          public void DrawLevel(GraphicsDevice a_graphicsDevice, Model.Levels a_level, Camera a_camera, Vector2 a_playerPosition, float a_elapsedTime, Model.StateHandler.LevelState a_LevelState, GraphicsDeviceManager a_manager, Model.StateHandler.CharStates a_charState, Model.StateHandler.CharFaceState a_charFaceState, List<Vector2> a_drawEnemy, List<Vector2> a_drawBoltEnemy, int m_playerHp, bool a_isInAir)
        {
            m_isInAir = a_isInAir;
            float scale = a_camera.GetScale();
            Vector2 viewPortSize = new Vector2(a_graphicsDevice.Viewport.Width, a_graphicsDevice.Viewport.Height);
            Vector2 playerViewPos = a_camera.GetViewPosition(a_playerPosition.X, a_playerPosition.Y, viewPortSize);
            Vector2 displacement = new Vector2(a_graphicsDevice.Viewport.Width, a_graphicsDevice.Viewport.Height);
            m_time += a_elapsedTime / 10.0f;
            torchPositions = new List<Vector2>();
            var mouse = Mouse.GetState();
            mousePosition = new Vector2(mouse.X, mouse.Y);
            // m_spriteBatch.Begin();
            a_graphicsDevice.Clear(Color.Transparent);
            //m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,null, null, null, lightningEffect);   
            
            Draw(scale, a_elapsedTime, a_graphicsDevice, a_level, a_camera, a_playerPosition, a_LevelState, a_charState,a_charFaceState, a_drawEnemy, a_drawBoltEnemy);
           
            m_spriteBatch.End();
            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(m_hpFont, "Hope " + m_playerHp.ToString(), m_hpPos, Color.Red);
            
            //DrawUI(m_playerHp);
            m_spriteBatch.End();

              
            a_graphicsDevice.SetRenderTarget(null);
        }

         

        public void drawMain(float a_elapsedTime, GraphicsDevice a_graphics, Model.Levels a_level, Camera a_camera, Vector2 a_playerPosition, Model.StateHandler.LevelState a_LevelState, Model.StateHandler.CharStates a_charState, Model.StateHandler.CharFaceState a_charFaceState, List<Vector2> a_drawEnemy, List<Vector2> a_drawBoltEnemy)
        {
           //SÄTTER ALLT SOM RITAS UT TILL RENDERTARGET MAINSCENE SÅ ATT DET PÅVERKAS RÄTT UTAV FICKLAMPAN
            a_graphics.SetRenderTarget(mainScene);
            a_graphics.Clear(Color.Black);
            torchPositions = new List<Vector2>();

            var mouse = Mouse.GetState();
            mousePosition = new Vector2(mouse.X, mouse.Y);

            float scale = a_camera.GetScale();
            Vector2 viewportSize = new Vector2(a_graphics.Viewport.Width, a_graphics.Viewport.Height);
            Vector2 playerViewPos = a_camera.GetViewPosition(a_playerPosition.X, a_playerPosition.Y, viewportSize);
            Vector2 displacement = new Vector2(a_graphics.Viewport.Width, a_graphics.Viewport.Height);
            m_time += a_elapsedTime / 10.0f;
           
           //TILLDELAR TILES OCH PARTIKEL SYSTEM SOM SKALLF FINNAS PÅ BANA 1
           if(m_level ==1)       
            {
                SetBackground(m_level);
                DrawBackGround(GetBackground(), a_graphics);
             
                for (int x = 0; x < Model.Levels.g_levelWidth; x++)
                {
                    for (int y = 0; y < Model.Levels.g_levelHeight; y++)
                    {
                        Vector2 viewPos = a_camera.GetViewPosition(x, y, viewportSize);
                        DrawTile(viewPos.X, viewPos.Y, scale, a_level.m_tiles[x, y]);
                       
                    }
                }
                a_rainSystem.DrawRainDrops(m_spriteBatch, m_rainTexture, a_elapsedTime); 

            }
          
           //TILLDELAR RÄTT TILES FÖR BANA 2
           if (m_level == 2)
           {
               SetBackground(m_level);
               DrawBackGround(GetBackground(), a_graphics);
           
               for (int x = 0; x < Model.Levels.g_levelWidth; x++)
               {
                   for (int y = 0; y < Model.Levels.g_levelHeight; y++)
                   {
                       Vector2 viewPos = a_camera.GetViewPosition(x, y, viewportSize);
                       //DrawTile(viewPos.X, viewPos.Y, scale, a_level.m_tiles[x, y]);
                       DrawTileLevel2(viewPos.X, viewPos.Y, scale, a_level.m_tiles2[x, y]);
                   }
               }
           }
            //BANA 3, TILLDELAR RÄTT TILE VÄRDEN OCH VILKEN PARTICEL EFFEKT SOM SKALL RITAS UT
           if (m_level == 3)
           {
               SetBackground(m_level); 
               DrawBackGround(GetBackground(), a_graphics);
           
               for (int x = 0; x < Model.Levels.g_levelWidth; x++)
               {
                   for (int y = 0; y < Model.Levels.g_levelHeight; y++)
                   {
                       Vector2 viewPos = a_camera.GetViewPosition(x, y, viewportSize);                     
                       DrawTileLevel3(viewPos.X, viewPos.Y, scale, a_level.m_tiles3[x, y]);
                   }
               }
               a_ashSystem.DrawAshes(m_spriteBatch, m_ashes, a_elapsedTime); 
           }
            //RITAR UT RESTERANDE OBEROENDE VILKEN BANA.
            DrawEnemy(a_drawEnemy, viewportSize, a_camera, scale);
            DrawBoltEnemy(a_drawBoltEnemy, viewportSize, a_camera, scale);
            DrawPlayerAt(playerViewPos, scale, a_elapsedTime, a_charState, a_charFaceState);
            a_graphics.SetRenderTarget(null);
        }


        public void SetLevel(int a_level)
        {
            m_level = a_level;
        }

        public int GetViewLevel()
        {
            return m_level;
        }
 

        Vector2 torchPos = new Vector2(20,20);
        

        public void drawLightMask(float a_elapsedTime, GraphicsDevice a_graphics, Vector2 a_playerPos)
         {
             
            // KEYBOARD
            //RENDER TARGETEN ligtMask TAR HAND OM ALLT SOM HAN MED LJUSET ATT GÖRA OCH HUR VI RÖR MUSEN: INGET ANNAT RITAR UT HÄR.
            a_graphics.SetRenderTarget(lightMask);           
            a_graphics.Clear(Color.Black);
           
            var mouse = Mouse.GetState();
            mousePosition = new Vector2(mouse.X, mouse.Y);
            GamePadState gPad = GamePad.GetState(PlayerIndex.One);
          
            // m_spriteBatch.Draw(m_blackSquare, new Vector2(0, 0), new Rectangle(0, 0, 1600, 800), Color.Black);
            Vector2 lightPosition = new Vector2(a_playerPos.X - LIGHTOFFSET_X, a_playerPos.Y - LIGHTOFFSET_Y);
            Vector2 lightDir;
            Vector2 axisPos;
            float angle;

            // Range_Rectangle
            int l_width = 200;
            int l_height = 200;
            Rectangle lightRect = new Rectangle((int)lightPosition.X, (int)lightPosition.Y, l_width, l_height);
            lightRect.X -= lightRect.Width / 2;
            lightRect.Y -= lightRect.Height / 2;

            Vector2 lightDir_K = mousePosition - lightPosition;
            Vector2 lightDir_G = torchPos - lightPosition;

            if (gPad.IsConnected == true) // Xbox controlls
            {
                lightDir = lightDir_G;
                lightDir.Normalize();

                torchPos.X = MathHelper.Clamp(torchPos.X, lightRect.X, lightRect.X + lightRect.Width);
                torchPos.Y = MathHelper.Clamp(torchPos.Y, lightRect.Y, lightRect.Y + lightRect.Height);
                float maxSpeed = 10.0f;
                Vector2 torchMovement = gPad.ThumbSticks.Right;
                torchMovement.Y *= -1;

                if (torchMovement != Vector2.Zero)
                {
                    torchMovement.Normalize();
                }

                torchPos += torchMovement * maxSpeed;
                axisPos = new Vector2(lightDir.X + 100, lightDir.Y + 125);
                angle = (float)Math.Atan2(torchPos.Y - a_playerPos.Y
                       , torchPos.X - a_playerPos.X);
                //angle = (angle + (((angle - Math.Abs(angle)) / angle) * MathHelper.Pi));
            }
            else // Keyboard controlls
            {


                lightDir = lightDir_K;
                lightDir.Normalize();
                axisPos = new Vector2(lightDir.X + 100, lightDir.Y + 125);
                angle = (float)Math.Atan2(mousePosition.Y - a_playerPos.Y
                       , mousePosition.X - a_playerPos.X);
                
                mousePosition.X = MathHelper.Clamp(mousePosition.X, lightRect.X, lightRect.X + lightRect.Width);
                mousePosition.Y = MathHelper.Clamp(mousePosition.Y, lightRect.Y, lightRect.Y + lightRect.Height);
                Mouse.SetPosition((int)mousePosition.X, (int)mousePosition.Y);
            }

            m_spriteBatch.Draw(m_lightmask, lightPosition, null, flashLightColor, angle, axisPos, lightScale, SpriteEffects.None, 0f);
            a_graphics.SetRenderTarget(null);

         }
        float lightScale = 0;
        public void FlashLightColor(bool belowFortyPercent)
        {
            if (belowFortyPercent)
            {
                lightScale = 0.65f;
                flashLightColor = Color.Red * 0.5f;
            }
            else
            {
                lightScale = 0.9f;
                flashLightColor = Color.White;
            }
        }

        //DRAW METODEN ANROPAR SENDA DRAWMAIN OCH DRAWLIGHTMASK. ÄVEN EFFEKTEN FINNS HÄR I.
        public void Draw(float a_scale, float a_elapsedTime, GraphicsDevice a_graphics, Model.Levels a_level, Camera a_camera, Vector2 a_playerPosition, Model.StateHandler.LevelState      a_LevelState,    Model.StateHandler.CharStates a_charState, Model.StateHandler.CharFaceState a_charFaceState, List<Vector2> a_drawEnemy, List<Vector2> a_drawboltEnemy)
         //public void Draw(float a_elapsedTime, GraphicsDevice a_graphics, Camera a_camera, Vector2 a_playerPosition)
         {
             Vector2 viewportSize = new Vector2(a_graphics.Viewport.Width, a_graphics.Viewport.Height);
             Vector2 playerViewPos = a_camera.GetViewPosition(a_playerPosition.X, a_playerPosition.Y, viewportSize);
             
             m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.DepthRead, RasterizerState.CullNone);

             drawMain(a_elapsedTime, a_graphics, a_level, a_camera, a_playerPosition, a_LevelState, a_charState, a_charFaceState, a_drawEnemy, a_drawboltEnemy);
             drawLightMask(a_elapsedTime, a_graphics, playerViewPos);
             
             a_graphics.Clear(Color.Black);

             lightningEffect.Parameters["lightMask"].SetValue(lightMask);
             lightningEffect.CurrentTechnique.Passes[0].Apply();
             m_spriteBatch.Draw(mainScene, new Vector2(0, 0), Color.Black);

         }
        //
        private void DrawTile(float a_x, float a_y, float a_scale, Model.Levels.Tile a_tile)
        {    
            //Get the source rectangle (pixels on the texture) for the tile type 
            TileRectangle = new Rectangle(m_textureTileSize * (int)a_tile, 0, m_textureTileSize, m_textureTileSize);
            //Destination rectangle in windows coordinates only scaling
             destRect = new Rectangle((int)a_x, (int)a_y, (int)a_scale, (int)a_scale);
             m_spriteBatch.Draw(m_tileTextures, destRect,  TileRectangle, Color.White);
        }
        //RITAR TILS FÖR BANA2
        private void DrawTileLevel2(float a_x, float a_y, float a_scale, Model.Levels.TileLevel2 a_tile)
        {
            //Get the source rectangle (pixels on the texture) for the tile type 
           TileRectangle = new Rectangle(m_textureTileSize * (int)a_tile, 0, m_textureTileSize, m_textureTileSize);
            //Destination rectangle in windows coordinates only scaling
           destRect = new Rectangle((int)a_x, (int)a_y, (int)a_scale, (int)a_scale);
            m_spriteBatch.Draw(m_tileTextures2, destRect,  TileRectangle, Color.White);
        }
        //RITAR UT TILS FÖR BANA3. "Dessa måste ritas"
        private void DrawTileLevel3(float a_x, float a_y, float a_scale, Model.Levels.TileLevel3 a_tile)
        {
            //Get the source rectangle (pixels on the texture) for the tile type 
            TileRectangle = new Rectangle(m_textureTileSize * (int)a_tile, 0, m_textureTileSize, m_textureTileSize);
            //Destination rectangle in windows coordinates only scaling
            destRect = new Rectangle((int)a_x, (int)a_y, (int)a_scale, (int)a_scale);
            m_spriteBatch.Draw(m_tileTextures, destRect, TileRectangle, Color.White);
        }
        //RITAR UT BAKGRUNDEN FÖR BANORNA, BANA2 NÅGOT FEL!
        public void DrawBackGround(Texture2D a_background, GraphicsDevice graphics)
        {
            //Choose the correct background according to level.
         if(m_level == 1)
            {
            Rectangle screenRectangle = new Rectangle(0, 0, graphics.Viewport.Width, a_background.Height);
            Rectangle DestRectangle = new Rectangle(0, 0, graphics.Viewport.Width, a_background.Height - 400);

            Rectangle screenyRectangle = new Rectangle(m_backGroundBasePos, 0, graphics.Viewport.Width, a_background.Height);
            Rectangle screenyRectangleCopy = new Rectangle(m_backGroundBasePos - a_background.Width, 0, graphics.Viewport.Width, a_background.Height);

                Rectangle screenTree1Rectangle = new Rectangle((int)m_backGroundLayerOnePos, 0, graphics.Viewport.Width,a_background.Height);
                Rectangle screenTree1RectangleCopy = new Rectangle((int)m_backGroundLayerOnePos - m_backGroundLayerOne.Width, 0, graphics.Viewport.Width, a_background.Height);

                Rectangle screenTree2Rectangle = new Rectangle(m_backGroundLayerTwooPos, 0, graphics.Viewport.Width, m_backGround.Height);
                Rectangle screenTree2RectangleCopy = new Rectangle(m_backGroundLayerTwooPos - m_backGroundLayerTwoo.Width, 0, graphics.Viewport.Width, a_background.Height);

                Rectangle screenTree3Rectangle = new Rectangle(m_backGroundLayerThreePos , 0, graphics.Viewport.Width, m_backGround.Height);
                Rectangle screenTree3RectangleCopy = new Rectangle(m_backGroundLayerThreePos - m_backGroundLayerThree.Width, 0, graphics.Viewport.Width, a_background.Height);

                Rectangle screenTree4Rectangle = new Rectangle(m_backGroundLayerFourPos, 0, graphics.Viewport.Width, m_backGround.Height);
                Rectangle screenTree4RectangleCopy = new Rectangle(m_backGroundLayerFourPos - m_backGroundLayerFour.Width, 0, graphics.Viewport.Width, a_background.Height);
             
               // m_spriteBatch.Begin();
                m_spriteBatch.Draw(a_background, DestRectangle, screenRectangle, Color.White);

                m_spriteBatch.Draw(m_backGroundLayerFive, DestRectangle, screenTree4Rectangle, Color.White);
                m_spriteBatch.Draw(m_backGroundLayerFiveCopy, DestRectangle, screenTree4RectangleCopy, Color.White);

                m_spriteBatch.Draw(m_backGroundLayerFour, DestRectangle, screenTree3Rectangle, Color.White);
                m_spriteBatch.Draw(m_backGroundLayerFourCopy, DestRectangle, screenTree3RectangleCopy, Color.White);

                m_spriteBatch.Draw(m_backGroundLayerThree, DestRectangle, screenTree2Rectangle, Color.White);
                m_spriteBatch.Draw(m_backGroundLayerThreeCopy, DestRectangle, screenTree2RectangleCopy, Color.White);

                m_spriteBatch.Draw(m_backGroundLayerTwoo, DestRectangle, screenTree1Rectangle, Color.White);
                m_spriteBatch.Draw(m_backGroundLayerTwooCopy, DestRectangle, screenTree1RectangleCopy, Color.White);


                
                m_spriteBatch.Draw(m_backGroundLayerOne, DestRectangle, screenyRectangle, Color.White);
               m_spriteBatch.Draw(m_backGroundLayerOneCopy, DestRectangle, screenyRectangleCopy, Color.White);
            }
            //if (a_levelState == Model.StateHandler.LevelState.LEVEL2)
            if(m_level == 2)
            {
                //m_backGround = m_sewerBg;
                Rectangle screenyRectangle = new Rectangle(m_backGroundBasePos, 2, graphics.Viewport.Width, m_backGround.Height);
                Rectangle screenyRectangleCopy = new Rectangle(m_backGroundBasePos + m_backGround.Width, 0, graphics.Viewport.Width, m_backGround.Height);
                Rectangle DestRectangle = new Rectangle(0, 200, graphics.Viewport.Width, m_backGround.Height-200);
       
                m_spriteBatch.Draw(m_backGround, DestRectangle, screenyRectangle, Color.White);
                m_spriteBatch.Draw(m_backGround, destRect, screenyRectangleCopy, Color.White);
            }
            if (m_level == 3)
            {
                Rectangle screenyRectangle = new Rectangle(m_backGroundBasePos, 0, graphics.Viewport.Width, m_backGround.Height);
                Rectangle screenyRectangleCopy = new Rectangle(m_backGroundBasePos + m_backGround.Width, 0, graphics.Viewport.Width, m_backGround.Height);

                Rectangle DestRectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);

                m_spriteBatch.Draw(m_backGround, DestRectangle, screenyRectangle, Color.White);
                m_spriteBatch.Draw(m_backGround, destRect, screenyRectangleCopy, Color.White);
            }
        }
        public void SetBackground(int a_level)
        {
            if (a_level == 1)
            {
                m_backGround = m_forestBg;
            }
            else if (a_level == 2)
            {
                m_backGround = m_sewerBg;
            }
            else if (m_level == 3)
            {
                m_backGround = m_villageBg;
            }
        }

        public Texture2D GetBackground()
        {
            return m_backGround;
        }

        private Texture2D m_enemyTexture;
        //UPPDATE ENEMMY SOM SKULLE ANIMERA FIENDEN: TYVÄRR INTE HUNNIT IMPLEMENTERA FULLT UT
        public Texture2D UpdateEnemyTexture(Vector2 a_playerPos, Vector2 a_enemyPos)
        {
            if (a_enemyPos.X < a_playerPos.X)
            {
                m_enemyTexture = m_enemyStandingLeftTexture;
            }
            else if (a_enemyPos.X > a_playerPos.X)
            {
                m_enemyTexture = m_enemyStandingRightTexture;
            }
            return m_enemyTexture;
        }

       
        //SÄTTER FIENDE TEXTUREN,
        public void DrawEnemy(List<Vector2> a_position, Vector2 viewportSize, Camera a_camera, float a_scale)
        {
            Rectangle enemySource = new Rectangle(0, 0, (int)(m_enemyTexture.Width), (int)(m_enemyTexture.Height));
            a_scale = 128;
           
            
                for (int i = 0; i < a_position.Count(); i++)
                {
                    Vector2 EnemyViewPos = a_camera.GetViewPosition(a_position[i].X, a_position[i].Y, viewportSize);
                    Rectangle enemyRect = new Rectangle((int)(EnemyViewPos.X - a_scale / 2), (int)(EnemyViewPos.Y - a_scale), (int)a_scale, (int)a_scale);
                    m_spriteBatch.Draw(m_enemyTexture, enemyRect, enemySource, Color.White);
                }
            
        }
        //SÄTTER BOLTENEMYTEXTUREN
        public void DrawBoltEnemy(List<Vector2>a_boltposition, Vector2 viewportSize, Camera a_camera, float a_scale)
        {
          //  Model.EnemyBolt a_boltEnemy = new Model.EnemyBolt();
            Rectangle enemySource = new Rectangle(0, 0, (int)(m_boltEnemy.Width), (int)(m_boltEnemy.Height));
            a_scale = 64;
            for (int i = 0; i < a_boltposition.Count(); i++)
            {
                Vector2 EnemyViewPos = a_camera.GetViewPosition(a_boltposition[i].X, a_boltposition[i].Y, viewportSize);
                Rectangle enemyRect = new Rectangle((int)(EnemyViewPos.X - a_scale / 4), (int)(EnemyViewPos.Y - a_scale/4), (int)a_scale, (int)a_scale);
                m_spriteBatch.Draw(m_boltEnemy, enemyRect, enemySource, Color.White);
            }

        }
       //SÄTTER SPELAREN POSITION, KOLLAR ÄVEN VILKEN SPRITE SOM SKALL RITAS:
        private void DrawPlayerAt(Microsoft.Xna.Framework.Vector2 a_viewBottomCenterPosition, float a_scale, float a_elapsedTime, Model.StateHandler.CharStates a_charState, Model.StateHandler.CharFaceState a_charFaceState)
       {

           a_scale = 64;
           if (m_currentState == PlayerState.Right)
           {
               Rectangle source = GetSourceRectangleRight(a_elapsedTime);
               Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 2), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
               m_spriteBatch.Draw(m_playerRightTexture, destRect, source, Color.White);
           }
           else if (m_currentState == PlayerState.Left)
           {
               Rectangle source = GetSourceRectangleLeft(a_elapsedTime);
               Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 4), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
               m_spriteBatch.Draw(m_playerLeftTexture, destRect, source, Color.White);
           }
           
           
           else   if (m_state == State.Jumping)
           {
               
               Rectangle source = GetSourceRectangleJump(a_elapsedTime);
               Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 4), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
               m_spriteBatch.Draw(m_playerJumpTexture, destRect, source, Color.White);
           }
               
          
       }
        private Rectangle GetSourceRectangleJump(float a_elapsedTIme)
        {
            Rectangle source;
            switch(m_state)
            {
                    
                case State.Jumping:
                    source = new Rectangle(0,0,(int)(m_playerJumpTexture.Width),(int)(m_playerJumpTexture.Height));
                    break;
                default:
                     source = new Rectangle(0, 0, (int)(m_playerJumpTexture.Width), (int)(m_playerJumpTexture.Height));
                    break;
            }
            return source;
        }
        //HÄMTAR RUNNING RIGHT TEXTUREN.
        private Rectangle GetSourceRectangleRight(float a_elapsedTIme)
        {
            Rectangle source;
            switch (m_state)
            {
                case State.Walking:
                    int x = CheckSpriteMovementRight(a_elapsedTIme);
                    source = new Rectangle(x, 0, (int)(m_playerRightTexture.Width / 9), (int)(m_playerRightTexture.Height));
                    break;
                /* case State.Standing:
                      source = new Rectangle(0, y, (int)(m_playerStandTexture.Width), (int)(m_playerStandTexture.Height));
                     break;*/
                default:
                    source = new Rectangle(0, 0, (int)(m_playerRightTexture.Width/9), (int)(m_playerRightTexture.Height));
                    break;
            }
           return source;
        }
        //HÄMTAR LEFT TEXTUREN FÖR SPELAREN
        private Rectangle GetSourceRectangleLeft(float a_elapsedTIme)
        {

            Rectangle source;
            switch (m_state)
            {
                case State.Walking:
                    int x = CheckSpriteMovementLeft(a_elapsedTIme);
                    source = new Rectangle(x, 0, (int)(m_playerLeftTexture.Width / 9), (int)(m_playerLeftTexture.Height));
                    break;
                default:
                    source = new Rectangle(0, 0, (int)(m_playerLeftTexture.Width/9), (int)(m_playerLeftTexture.Height));
                    break;
            }
            return source;
        }
        //KOLLAR SPRITEN, SWITCH SATTS FÖR ATT GÖRA SPELARE ANIMOATIONEN NÄR HAN SPRINGER HÖGER
        public int CheckSpriteMovementRight(float a_elapsedTime)
        {
            int x;
            timer += a_elapsedTime;
            switch (movement)
            {
                case 1:
                    x = 64;
                    sourceRectangle = new Rectangle(m_textureTileSize * 0, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 2:
                    x = 512;
                    new Rectangle(m_textureTileSize * 1, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 3:
                    x = 448;
                    sourceRectangle = new Rectangle(m_textureTileSize * 2, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 4:
                    x =  384;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 5:
                    x = 320;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 6:
                    x = 256;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 7:
                    x =  192;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 8:
                    x = 128;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;

                default:
                    x = 64;
                    movement = 1;
                    break;

            }
            if (timer > interval)
            {
                movement++;
                timer = 0f;
            }
            return x;
        }
        //KOLLAR SPRITEN, SWITCH SATTS FÖR ATT GÖRA SPELARE ANIMOATIONEN NÄR HAN SPRINGER VÄNSTER
        public int CheckSpriteMovementLeft(float a_elapsedTime)
        {
            int x;
            timer += a_elapsedTime;
            switch (movement)
            {
                case 1:
                    x = 64;
                    sourceRectangle = new Rectangle(m_textureTileSize * 0, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 2:
                    x = 128;
                    new Rectangle(m_textureTileSize * 1, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 3:
                    x = 192;
                    sourceRectangle = new Rectangle(m_textureTileSize * 2, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 4:
                    x = 256;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 5:
                    x = 320;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 6:
                    x = 384;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 7:
                    x = 448;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;
                case 8:
                    x = 512;
                    sourceRectangle = new Rectangle(m_textureTileSize * 3, 0, m_textureTileSize, m_textureTileSize);
                    break;

                default:
                    x = 512;
                    movement = 1;
                    break;
            }
            if (timer > interval)
            {
                movement++;
                timer = 0f;
            }
            return x;
        }
        private Rectangle sourceRectangle;
        
        //SÄTTER HOPPLJUD
        internal void DoJump()
        {
            
            m_jumpSound.Play();
        }
        //KOLLAR OM SPELAREN HAR TRYCKT HOPPA
       public bool DidPlayerPressJump()
       {
           Keys jumpKey = Keys.Space;
           bool ret = false;
           GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           KeyboardState newState = Keyboard.GetState();

           //has been pressed and released
           if (m_oldKeyboardState.IsKeyDown(jumpKey) || m_oldGamePadState.IsButtonDown(Buttons.RightShoulder))// && newState.IsKeyUp(jumpKey))
           {
               m_state = State.Jumping;
               
               //m_currentState = PlayerState.Up;
               ret = true;
           }
           else if (m_state != State.Jumping)
           {
               m_state = State.Standing;
           }
           //save state
           //m_oldKeyboardState = newState;
           return ret;
       }
        //UPPDATERA BAKGGRUNDEN FÖR ATT FÅ EN FIN ÖVERGÅNG: SYNS MEST PÅ BANA 1
       public void UpdateBg(GraphicsDeviceManager graphics)
       {

           if (m_backGroundBasePos > m_backGround.Width)
           {
              m_backGroundBasePos -= m_backGround.Width;
           }
           if (m_backGroundBasePos < 0)
           {
               m_backGroundBasePos += m_backGround.Width;
           }

           if (m_backGroundLayerOnePos > m_backGroundLayerOne.Width)
           {
               m_backGroundLayerOnePos -= m_backGroundLayerOne.Width;         
           }
          if (m_backGroundLayerOnePos < 0)
           {
               m_backGroundLayerOnePos += m_backGroundLayerOne.Width;
           }
           if (m_backGroundLayerTwooPos > m_backGroundLayerTwoo.Width)
           {
               m_backGroundLayerTwooPos -= m_backGroundLayerTwoo.Width;
           }
           if (m_backGroundLayerTwooPos < 0)
           {
               m_backGroundLayerTwooPos += m_backGroundLayerTwoo.Width;
           }

           if (m_backGroundLayerThreePos > m_backGroundLayerThree.Width)
           {
               m_backGroundLayerThreePos -= m_backGroundLayerThree.Width;
           }

           if (m_backGroundLayerThreePos < 0)
           {
               m_backGroundLayerThreePos += m_backGroundLayerThree.Width;
           }

           if (m_backGroundLayerFourPos > m_backGroundLayerFour.Width)
           {
               m_backGroundLayerFourPos -= m_backGroundLayerFour.Width;
           }

           if (m_backGroundLayerFourPos <= 0)
           {
               m_backGroundLayerFourPos += m_backGroundLayerFour.Width;
           }

           if (m_backGroundLayerFivePos > m_backGroundLayerFive.Width)
           {
               m_backGroundLayerFivePos -= m_backGroundLayerFive.Width;
           }

           if (m_backGroundLayerFivePos <= 0)
           {
              m_backGroundLayerFivePos += m_backGroundLayerFive.Width;
           }
       }

        //SÄTTER HUR BACKGRUNDEN SKALL RÖRAS:
       public void MoveBackground(float a_playerSpeed)
       {
           if (a_playerSpeed > 1 && m_currentState == PlayerState.Right)
           {
               m_backGroundBasePos += (int)3;
               m_backGroundLayerOnePos += (int)2.8;
               m_backGroundLayerTwooPos += (int)2.4;
               m_backGroundLayerThreePos += (int)2;
               m_backGroundLayerFourPos += (int)1.6;
               m_backGroundLayerFivePos += (int)1;
           }
           else if (a_playerSpeed < -1 && m_currentState == PlayerState.Left)
           {
               m_backGroundBasePos -= (int)3;
               m_backGroundLayerOnePos -= (int)2.8;
               m_backGroundLayerTwooPos -= (int)2.4;
               m_backGroundLayerThreePos -= (int)2;
               m_backGroundLayerFourPos -= (int)1.6;
               m_backGroundLayerFivePos -= (int)1;
           }
       }

        //KOLLAR OM SPELAREN HAR TRYCKT SPARA
      
       public bool DidPlayerPressSave()
       {
           Keys saveKey = Keys.F12;
           GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
           KeyboardState newState = Keyboard.GetState();
           bool ret = false;
           if (m_oldKeyboardState.IsKeyDown(saveKey) || m_oldGamePadState.IsButtonDown(Buttons.Y))
           {
               ret = true;
           }
            m_oldKeyboardState = newState;
            m_oldGamePadState = gamepadState;
           return ret;


       }

        

       public bool DidPlayerPressRight(float a_playerSpeed)
       {
           Keys rightKey = Keys.D;
           Keys rightKeyTwoo = Keys.Right;
           bool ret = false;

           KeyboardState newState = Keyboard.GetState();
           GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           //has been pressed and released
           if (m_oldKeyboardState.IsKeyDown(rightKey) || m_oldKeyboardState.IsKeyDown(rightKeyTwoo) || m_oldGamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
           {
               if(a_playerSpeed != 0)
               {
               m_state = State.Walking;
               m_currentState = PlayerState.Right;
               
               /*m_backGroundBasePos += (int)3;
               m_backGroundLayerOnePos += (int)2.8;
               m_backGroundLayerTwooPos += (int)2.4;
               m_backGroundLayerThreePos += (int)2;
               m_backGroundLayerFourPos += (int)1.6;
               m_backGroundLayerFivePos += (int)1;*/
                   }
               ret = true;

           }
          // else if (m_state != State.Jumping)
          /// {
          //     m_state = State.Standing;
          // }
           //save state
           m_oldKeyboardState = newState;
           m_oldGamePadState = gamePadState;
           return ret;
       }

       private GamePadState m_oldGamePadState;
      
        public bool DidPlayerPressLeft(float a_playerSpeed)
       {
           Keys leftKey = Keys.A;
           Keys leftkeytwoo = Keys.Left; 
           bool ret = false;

           KeyboardState newState = Keyboard.GetState();
           GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
           //has been pressed and released

           if (m_oldKeyboardState.IsKeyDown(leftKey) || m_oldKeyboardState.IsKeyDown(leftkeytwoo) || m_oldGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))// && newState.IsKeyUp(jumpKey))
           {
              // a_charFaceState = Model.StateHandler.CharFaceState.LEFT;
             if(a_playerSpeed != 0)
               {
                       m_state = State.Walking;
                       m_currentState = PlayerState.Left;
                       /*m_backGroundBasePos -= (int)3;
                       m_backGroundLayerOnePos -= (int)2.8;
                       m_backGroundLayerTwooPos -= (int)2.4;
                       m_backGroundLayerThreePos -= (int)2;
                       m_backGroundLayerFourPos -= (int)1.6;
                       m_backGroundLayerFivePos -= (int)1;*/
                }
                 ret = true;
           }
         //  else if (m_state != State.Jumping)
           //{
           //    m_state = State.Standing;
          // }
           //save state
            m_oldKeyboardState = newState;
            m_oldGamePadState = gamePadState;
            return ret;
       }



    }
}
