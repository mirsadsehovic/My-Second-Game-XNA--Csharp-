using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
    class LevelThree
    {

        private static int g_levelWidth = Levels.g_levelWidth;
        private static int g_levelHeight = Levels.g_levelHeight;
        internal Levels.TileLevel3[,] m_tiles3 = new Levels.TileLevel3[g_levelWidth, g_levelHeight];

        internal Levels.TileLevel3[,] GenerateLevel()
        {
            for (int x = 0; x < g_levelWidth; x++)
            {
                m_tiles3[x, g_levelHeight - 1] = Levels.TileLevel3.T_BLOCKED;
                m_tiles3[x, g_levelHeight - 2] = Levels.TileLevel3.T_BLOCKED;
            }
            m_tiles3[10, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[11, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[11, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[11, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[15, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;
            m_tiles3[20, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;

            m_tiles3[15, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[20, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[35, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[36, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[36, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[40, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;
            m_tiles3[45, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;
            m_tiles3[50, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;
            m_tiles3[55, g_levelHeight - 3] = Levels.TileLevel3.T_RUSHINGENEMY;

            m_tiles3[40, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[45, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[50, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[55, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[60, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[61, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[61, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[61, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[62, g_levelHeight - 3] = Levels.TileLevel3.T_TRAP;
            m_tiles3[63, g_levelHeight - 3] = Levels.TileLevel3.T_TRAP;
            m_tiles3[64, g_levelHeight - 3] = Levels.TileLevel3.T_TRAP;
            m_tiles3[65, g_levelHeight - 3] = Levels.TileLevel3.T_TRAP;

            for (int x = 62; x < 100; x++)
            {
                m_tiles3[x, g_levelHeight - 3] = Levels.TileLevel3.T_TRAP;
            }
            
            m_tiles3[66, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[66, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[66, g_levelHeight - 5] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[70, g_levelHeight - 3] = Levels.TileLevel3.T_BLOCKED;
            m_tiles3[70, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[80, g_levelHeight - 4] = Levels.TileLevel3.T_ENEMYBOLT;

            m_tiles3[75, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;


            m_tiles3[78, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;

            m_tiles3[80, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;

            for (int x = 83; x < 100; x++)
            {
                m_tiles3[x, g_levelHeight - 4] = Levels.TileLevel3.T_BLOCKED;
            }
            

            return m_tiles3;
        }
    }
}
