using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Umbra_development.Model
{
   public class LevelOne
    {
      // Width and Height

        // The 2D array with tiles
        private static int g_levelWidth = Levels.g_levelWidth;
        private static int g_levelHeight = Levels.g_levelHeight;
        internal Levels.Tile[,] m_tiles = new Levels.Tile[g_levelWidth, g_levelHeight];

        internal Levels.Tile[,] GenerateLevel()
        {

                for (int x = 0; x < g_levelWidth; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Levels.Tile.T_BLOCKED;
                    m_tiles[x, g_levelHeight - 2] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 2; y < 14; y++)
                {

                    m_tiles[1, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[2, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[3, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[4, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[5, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[6, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[7, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[8, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[9, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[10, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[11, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[12, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[13, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[14, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }

                m_tiles[13, g_levelHeight - 15] = Levels.Tile.T_HEART;

                for (int y = 3; y < 4; y++)
                {
                    m_tiles[15, g_levelHeight - y] = Levels.Tile.T_TRAP;
                    m_tiles[16, g_levelHeight - y] = Levels.Tile.T_TRAP;

                }


                for (int y = 2; y < 14; y++)
                {
                    m_tiles[17, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[18, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[19, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 2; y < 16; y++)
                {
                    m_tiles[20, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 2; y < 18; y++)
                {
                    m_tiles[21, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[22, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[23, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                m_tiles[40, g_levelHeight - 17] = Levels.Tile.T_RUSHINGENEMY;
                m_tiles[40, g_levelHeight - 3] = Levels.Tile.T_ENEMYBOLT;
                m_tiles[25, g_levelHeight - 3] = Levels.Tile.T_TEDDY;
                m_tiles[26, g_levelHeight - 3] = Levels.Tile.T_VANISH;

                //m_tiles[29, g_levelHeight - 3] = Tile.T_GIRL;

                for (int y = 8; y < 13; y++)
                {
                    m_tiles[27, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[28, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[29, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[30, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[31, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[32, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 8; y < 14; y++)
                {
                    m_tiles[33, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[34, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 8; y < 15; y++)
                {
                    m_tiles[35, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[36, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 8; y < 16; y++)
                {
                    m_tiles[37, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[38, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 8; y < 17; y++)
                {
                    m_tiles[39, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[40, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 10; y < 15; y++)
                {
                    m_tiles[41, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[42, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[43, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[43, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }

                for (int y = 1; y < 5; y++)
                {
                    m_tiles[42, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[41, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 1; y < 7; y++)
                {
                    m_tiles[43, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[44, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 1; y < 9; y++)
                {
                    m_tiles[45, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[46, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                m_tiles[47, g_levelHeight - 8] = Levels.Tile.T_BLOCKED;
                m_tiles[47, g_levelHeight - 5] = Levels.Tile.T_BLOCKED;
                m_tiles[47, g_levelHeight - 2] = Levels.Tile.T_BLOCKED;
                for (int y = 6; y < 10; y++)
                {
                    m_tiles[52, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[53, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[54, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[55, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[56, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[57, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[58, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 6; y < 8; y++)
                {
                    m_tiles[59, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[60, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[61, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[62, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[63, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[64, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[65, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[66, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 6; y < 10; y++)
                {
                    m_tiles[67, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[68, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[69, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }

                m_tiles[67, g_levelHeight - 11] = Levels.Tile.T_RUSHINGENEMY;

                for (int y = 6; y < 12; y++)
                {
                    m_tiles[70, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[71, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[72, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                m_tiles[51, g_levelHeight - 8] = Levels.Tile.T_BLOCKED;

                m_tiles[48, g_levelHeight - 1] = Levels.Tile.T_EMPTY; m_tiles[48, g_levelHeight - 2] = Levels.Tile.T_EMPTY;
                m_tiles[49, g_levelHeight - 1] = Levels.Tile.T_EMPTY; m_tiles[49, g_levelHeight - 2] = Levels.Tile.T_EMPTY;
                m_tiles[50, g_levelHeight - 1] = Levels.Tile.T_EMPTY; m_tiles[50, g_levelHeight - 2] = Levels.Tile.T_EMPTY;
                m_tiles[51, g_levelHeight - 1] = Levels.Tile.T_EMPTY; m_tiles[51, g_levelHeight - 2] = Levels.Tile.T_EMPTY;
                m_tiles[52, g_levelHeight - 1] = Levels.Tile.T_EMPTY; m_tiles[52, g_levelHeight - 2] = Levels.Tile.T_EMPTY;
                m_tiles[48, g_levelHeight - 1] = Levels.Tile.T_TRAP;
                m_tiles[49, g_levelHeight - 1] = Levels.Tile.T_TRAP;
                m_tiles[50, g_levelHeight - 1] = Levels.Tile.T_TRAP;
                m_tiles[51, g_levelHeight - 1] = Levels.Tile.T_TRAP;
                m_tiles[52, g_levelHeight - 1] = Levels.Tile.T_TRAP;
                for (int y = 1; y < 2; y++)
                {
                    m_tiles[52, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[53, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[54, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[55, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[56, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                m_tiles[58, g_levelHeight - 3] = Levels.Tile.T_ENEMYBOLT;
                for (int y = 1; y < 3; y++)
                {
                    m_tiles[57, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[58, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 1; y < 5; y++)
                {
                    m_tiles[59, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[60, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }

                for (int y = 3; y < 5; y++)
                {
                    m_tiles[73, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[74, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }

                for (int y = 3; y < 7; y++)
                {
                    m_tiles[75, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[76, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[77, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[78, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                for (int y = 3; y < 9; y++)
                {
                    m_tiles[79, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                    m_tiles[80, g_levelHeight - y] = Levels.Tile.T_BLOCKED;
                }
                return m_tiles;
            }
            
        }
            //Level2

        

    }
