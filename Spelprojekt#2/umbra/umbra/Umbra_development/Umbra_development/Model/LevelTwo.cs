using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
    public class LevelTwo
    {
        private static int g_levelWidth = Levels.g_levelWidth;
        private static int g_levelHeight = Levels.g_levelHeight;
        internal Levels.TileLevel2[,] m_tiles2 = new Levels.TileLevel2[g_levelWidth, g_levelHeight];

        
        internal Levels.TileLevel2[,] GenerateLevel()
        {


                for (int x = 0; x < g_levelWidth; x++)
                {
                    m_tiles2[x, g_levelHeight - 1] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                    m_tiles2[x, g_levelHeight - 2] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                    m_tiles2[x, g_levelHeight - 3] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                    m_tiles2[x, g_levelHeight - 4] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                    m_tiles2[x, g_levelHeight - 5] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                    m_tiles2[x, g_levelHeight - 6] = Levels.TileLevel2.T_EMPTYCOLLIDE;
                   
                }

                     m_tiles2[10, g_levelHeight  -7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[10, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[11, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[11, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[11, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[11, g_levelHeight - 10] = Levels.TileLevel2.T_BOX;
                     m_tiles2[13, g_levelHeight - 7] = Levels.TileLevel2.T_RUSHINGENEMY;

                     m_tiles2[18, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[18, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;

                     m_tiles2[21, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[22, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[22, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[22, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;

                     m_tiles2[25, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[26, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;

                     m_tiles2[27, g_levelHeight - 7] = Levels.TileLevel2.T_RUSHINGENEMY;

                     m_tiles2[27, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[29, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;

                     m_tiles2[30, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[31, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[31, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[32, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[32, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[32, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[33, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[33, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[33, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[33, g_levelHeight - 10] = Levels.TileLevel2.T_BOX;

                     for (int y = 7; y < 10; y++)
                     {
                         m_tiles2[34, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[35, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[35, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[36, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[37, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[38, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[39, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                         m_tiles2[40, g_levelHeight - y] = Levels.TileLevel2.T_BOX;
                     }

                     m_tiles2[37, g_levelHeight - 11] = Levels.TileLevel2.T_RUSHINGENEMY;
                     m_tiles2[40, g_levelHeight - 11] = Levels.TileLevel2.T_ENEMYBOLT;

                     m_tiles2[45, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[46, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[47, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[48, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[49, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[49, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[50, g_levelHeight - 10] = Levels.TileLevel2.T_RUSHINGENEMY;

                     m_tiles2[55, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[55, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;
                     m_tiles2[66, g_levelHeight - 7] = Levels.TileLevel2.T_ENEMYBOLT;
                     m_tiles2[57, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[62, g_levelHeight - 7] = Levels.TileLevel2.T_RUSHINGENEMY;
                     m_tiles2[60, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[63, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;
                     m_tiles2[66, g_levelHeight - 9] = Levels.TileLevel2.T_BOX;

                     m_tiles2[68, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[70, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;

                     m_tiles2[75, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;

                     m_tiles2[80, g_levelHeight - 7] = Levels.TileLevel2.T_ENEMYBOLT;
                     m_tiles2[85, g_levelHeight - 7] = Levels.TileLevel2.T_ENEMYBOLT;
                    
                     m_tiles2[85, g_levelHeight - 7] = Levels.TileLevel2.T_ENEMYBOLT;
                     m_tiles2[90, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[91, g_levelHeight - 7] = Levels.TileLevel2.T_BOX;
                     m_tiles2[91, g_levelHeight - 8] = Levels.TileLevel2.T_BOX;

                    

                    

                    


            return m_tiles2;
        }


    }
}
