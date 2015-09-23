using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Umbra_development.Model
{
   public class Levels
    {
       
        private StateHandler.LevelState m_levelState;
        private View.View m_view;
        //Tiles för bana 1;
        public enum Tile
        {
            T_EMPTY = 0,
            T_BLOCKED,
            T_BLOCKEDLEFT,
            T_TOPBLOCK,
            T_BLOCKEDRIGHT, 
            T_TEDDY,
            T_GIRL,
            T_VANISH,
            T_TRAP,
            T_SOMETHING,
            T_SOMETHING2,
            T_SOMETHING3,
            T_HEART,
            T_RUSHINGENEMY,
            T_ENEMYBOLT,
        };
        //Tiles för bana 2;
        public enum TileLevel2
        {
            T_EMPTY = 0,
            T_BOX,
            T_EMPTYCOLLIDE,
            T_TOPBLOCK,
            T_BLOCKEDRIGHT,
            T_TEDDY,
            T_GIRL,
            T_VANISH,
            T_TRAP,
            T_ESCAPE,
            T_RUSHINGENEMY,
            T_ENEMYBOLT,

        }
        public enum TileLevel3
        {
            T_EMPTY = 0,
            T_BLOCKED,
            T_BLOCKEDLEFT,
            T_TOPBLOCK,
            T_BLOCKEDRIGHT,
            T_TEDDY,
            T_GIRL,
            T_VANISH,
            T_TRAP,
            T_SOMETHING,
            T_SOMETHING2,
            T_SOMETHING3,
            T_HEART,
            T_RUSHINGENEMY,
            T_ENEMYBOLT,

        }


        // Width and Height
        public const int g_levelWidth = 100;
        public const int g_levelHeight = 30;

        private LevelClass m_levelClass = new LevelClass();
       
        // The 2D array with tiles
        internal Tile[,] m_tiles = new Tile[g_levelWidth, g_levelHeight];
        internal TileLevel2[,] m_tiles2 = new TileLevel2[g_levelWidth, g_levelHeight];
        internal TileLevel3[,] m_tiles3 = new TileLevel3[g_levelWidth, g_levelHeight];
        private List<Vector2> enemyPosition = new List<Vector2>();
        private List<Vector2> enemyBoltPosition = new List<Vector2>();
        private bool lastLevel = false;

        static int m_level = 1;

        public Levels()
        {
           
            GenerateLevel();
        }
        public void NextLevel() 
        {
            m_level++;
            GenerateLevel();
            //a_observer.LevelFinished(m_level);
        }
        public void ResetLevel()
        {
            m_level = 1;
            
        }

        public int GetLevel()
        {
            return m_level;
        }

        public void SetLevel(int a_level)
        {
            m_level = a_level;
        }

       //GENERERAR BANOR.
        public void GenerateLevel()
        {

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    if (m_level == 2)
                    {
                        m_tiles[x, y] = Tile.T_EMPTY;//set every tile to empty
                        m_tiles3[x, y] = TileLevel3.T_EMPTY;//set every tile to empty
                    }
                    else if (m_level == 1)
                    {
                        m_tiles2[x, y] = TileLevel2.T_EMPTY;
                        m_tiles3[x, y] = TileLevel3.T_EMPTY; 
                    }
                    else if (m_level == 3)
                    {
                        m_tiles2[x, y] = TileLevel2.T_EMPTY;
                        m_tiles[x, y] = Tile.T_EMPTY;
                    }
                }
            }
                //OM m_level = 1 SÅ HÄMTAS LEVELONE.
                if (m_level == 1)
                {

                    LevelOne m_level1 = new LevelOne();

                    m_tiles = m_level1.GenerateLevel();
                }

                //OM m_level = 2 SÅ HÄMTAS LEVELTWO
                if (m_level == 2)
                {

                    LevelTwo m_level2 = new LevelTwo();
                    m_tiles2= m_level2.GenerateLevel();
                }


                //OM m_level = 3 SÅ HÄMTAS LEVELTHREE
                if (m_level == 3)
                {

                    LevelThree m_level3 = new LevelThree();
                    m_tiles3 = m_level3.GenerateLevel();
                }
                if (m_level == 4)
                {

                    lastLevel = true;


                }
           // }
        }
        //EN FUNCTION SOM HÅLLER REDA PÅ SAKER MAN KAN KOLLIDERA SOM ÄR STANDARD OCH SAMMA VÄRDEN PÅ: DVS MARKEN MAN GÅR PÅ OCH VILKEN BANA
        public bool IsCollidingAt(Vector2 a_newPos, Vector2 a_size)
        {

            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4f)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.1f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_BLOCKED || m_tiles[x, y]== Tile.T_TOPBLOCK || m_tiles2[x,y] == TileLevel2.T_EMPTYCOLLIDE || m_tiles2[x,y] == TileLevel2.T_BOX || m_tiles3[x,y] == TileLevel3.T_BLOCKED)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        //KOLLAR OM MAN HAR KOLLIDERAT MED FIENDE; OCH PÅ VILKA BANOR DET I SÅ FALL HÄNDER. SÄTTER HUR MAN KOLLIDERAR MED FIENDEN:
        private List<Enemy> m_enemy = new List<Enemy>();
        public bool IsCollidingAtEnemy(Vector2 a_newPos, Vector2 a_size, Player a_player)
        {
            float playerRadius = 0.75f;
            float enemyRadius = 1.5f;
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);
           //Vector2 topLeft = new Vector2(a_newPos.X - a_size.X / 2.0f, a_newPos.Y - a_size.Y);
           //Vector2 bottomRight = new Vector2(a_newPos.X + a_size.X / 2.0f, a_newPos.Y);
         /*   Enemy enemy;
            enemy = new Enemy();
            BoundingSphere enemySphere = new BoundingSphere();
            BoundingSphere playerSphere = new BoundingSphere(new Vector3(a_player.GetPosition().X, a_player.GetPosition().Y - a_player.GetSize().Y / 2, 0), playerRadius);
            for (int i = 0; i < enemyPosition.Count; i++)
            {
                if (m_enemy != null)
                {
                    
                    enemySphere = new BoundingSphere(new Vector3(m_enemy[i].GetPosition().X + 0.5f, m_enemy[i].GetPosition().Y, 0), enemyRadius);
                    if (playerSphere.Intersects(enemySphere))
                    {
                        return true;
                    }
                }
            }
            return false;
            
            }*/

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.1f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_RUSHINGENEMY || m_tiles2[x,y] == TileLevel2.T_RUSHINGENEMY || m_tiles3[x,y] == TileLevel3.T_RUSHINGENEMY)
                    {
                        
                        return true;
                    }
                }
            }

            return false;
        }
        //KOLLAR OM MAN HAR KOLLIDERAT MED TEDDY; OCH PÅ VILKA BANOR DET I SÅ FALL HÄNDER. SÄTTER HUR MAN KOLLIDERAR.
       //HAR MAN KOLLIDERAT MED TEDDY SÅ RITAS FLICKAN UT: KOLLIDERAR MAN SEN MED EMPTY TILE SÅ FÖRSVINNER FLICKAN
        public bool IsCollidingAtTeddy(Vector2 a_newPos, Vector2 a_size, SoundObserver a_soundobserver)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_TEDDY)
                    {
                        m_tiles[25, g_levelHeight - 3] = Tile.T_EMPTY;
                        m_tiles[29, g_levelHeight - 3] = Tile.T_GIRL;
                       
                      
                        a_soundobserver.ScreamSound();
                        return true;
                    }
                    else if (m_tiles[x, y] == Tile.T_VANISH)
                    {
                       m_tiles[29, g_levelHeight - 3] = Tile.T_EMPTY;
                    }
                }
            }
            return false;
        }
        //KOLLAR OM MAN HAR KOLLIDERAT MED HJÄRTAT; OCH PÅ VILKA BANOR DET I SÅ FALL HÄNDER. SÄTTER HUR MAN KOLLIDERAR.
       //OM MAN KOLLIDERAR MED HJÄRTAT SÅ BYTER DEN TILL EN EMPTY TILE FÖR ATT DEN SKA FÖRSVINNA
        public bool IsCollidingAtHeart(Vector2 a_newPos, Vector2 a_size, SoundObserver a_soundobserver)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_HEART)
                    {
                        m_tiles[x, y] = Tile.T_EMPTY;
                        return true;
                    }
                  
                }
            }
            return false;
        }

       //KOLLAR OM MAN HAR KOLLIDERAT MED TRAP; OCH PÅ VILKA BANOR DET I SÅ FALL HÄNDER. SÄTTER HUR MAN KOLLIDERAR.
        public bool IsCollidingAtTrap(Vector2 a_newPos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_TRAP || m_tiles2[x,y] == TileLevel2.T_TRAP || m_tiles3[x,y] == TileLevel3.T_TRAP)
                    {
                       
                        return true;
                    }
                    
                }
            }
            return false;

        }
        public bool IsCollidingAtBoltEnemy(Vector2 a_newPos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_ENEMYBOLT || m_tiles2[x,y] == TileLevel2.T_ENEMYBOLT || m_tiles3[x,y] == TileLevel3.T_ENEMYBOLT)
                    {
                        
                        return true;
                    }

                }
            }
            return false;
        }
       //SÄTTER FIENDERNAS POSITION
        public void SetEnemyPosition()
        {
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    if (m_tiles[x, y] == Tile.T_RUSHINGENEMY || m_tiles2[x, y] == TileLevel2.T_RUSHINGENEMY || m_tiles3[x, y] == TileLevel3.T_RUSHINGENEMY)
                    {
                        enemyPosition.Add(new Vector2(x, y));
                    }
                    //if (m_tiles2[x, y] == TileLevel2.T_RUSHINGENEMY)
                   // {
                   //     enemyPosition.Add(new Vector2(x, y));
                   // }
                }
            }

        }

        public void UnloadEnemyPositions()
        {
            enemyPosition.Clear();
        }
        public void SetEnemyBoltPosition()
        {
           
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    if (m_tiles[x, y] == Tile.T_ENEMYBOLT || m_tiles2[x, y] == TileLevel2.T_ENEMYBOLT || m_tiles3[x, y] == TileLevel3.T_ENEMYBOLT)
                    {
                        enemyBoltPosition.Add(new Vector2(x, y));
                    }
                    /*if (m_tiles2[x, y] == TileLevel2.T_ENEMYBOLT)
                    {
                        enemyBoltPosition.Add(new Vector2(x, y));
                   }*/
                }
            }
           
        }

        internal bool isLastLevel()
        {
            return lastLevel;
        }

        public void GetLevelState(StateHandler.LevelState a_levelState)
        {
            m_levelState = a_levelState;
        }
       //HÄMTAR FIENDENS POSITION
        public List<Vector2> GetEnemyPositions()
        {
            SetEnemyPosition();
            return enemyPosition;
        }
        public List<Vector2> GetEnemyBoltPositions()
        {
            SetEnemyBoltPosition();
            return enemyBoltPosition;
        }

    }
}
