using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
public interface ILevelObserver
    {

          //void LevelFinished(int a_level);
          void Death();
          void DidPlayerPressBackToMainMenu();

    }
}
