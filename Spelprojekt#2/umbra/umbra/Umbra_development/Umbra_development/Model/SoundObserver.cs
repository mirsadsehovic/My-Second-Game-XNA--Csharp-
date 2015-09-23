using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
    public interface SoundObserver
    {
        void ScreamSound();
        //void JumpSound();
       // void ChaseSound();

        void SetLevel(int lvl);
        //void SetLifes(int a_life);
        
    }
}
