using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
    class LevelClass
    {

        public enum NewLevel
        {
            LEVEL1,
            LEVEL2,
            LEVEL3
        };
        public NewLevel m_CurrentState;
        public void SetLevel(int i)
        {
            switch (i)
            {
                case 1:
                    m_CurrentState = NewLevel.LEVEL1;
                    break;
                case 2:
                    m_CurrentState = NewLevel.LEVEL2;
                    break;
                case 3:
                    m_CurrentState = NewLevel.LEVEL3;
                    break;
            }
        }

    }
}
