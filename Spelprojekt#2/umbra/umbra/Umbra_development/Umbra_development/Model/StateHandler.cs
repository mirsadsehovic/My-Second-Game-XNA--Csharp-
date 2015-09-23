using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umbra_development.Model
{
   public class StateHandler
    {

       public StateHandler()
       {
       }

        public enum CharFaceState
        {
            LEFT,
            RIGHT

        }

        public enum CharStates
        {
            STANDING,
            FALLING,
            WALKING,
            RUNNING,
            JUMPING,
            DYING,
            DEAD
        }

        public enum LevelState
        {
            LEVEL1 =0,
            LEVEL2 =1,
            LEVEL3 =2,
            LEVEL4=3,
            LEVEL5=5
        }

        public enum GameState
        {
            START,
            ACTIVE,
            MENU,
            PAUSE,
            COMPLETTE
        
        }

        public enum Difficulty
        {
            EASY,
            MEDIUM,
            HARD
        }
        public enum EnemyState
        {
            LEFT,
            RIGHT,
            STANDING
        }

        private Difficulty m_difficulty = Difficulty.EASY;
        private CharFaceState m_CharFaceState = CharFaceState.RIGHT;
        private CharStates m_CharState = CharStates.STANDING;
        private GameState m_GameState = GameState.MENU;
        private LevelState m_LevelState = LevelState.LEVEL1;
        private EnemyState m_enemyState = EnemyState.STANDING;

        public void SetDifficulty(Difficulty a_difficulty)
        {
            m_difficulty = a_difficulty;
        }

        public Difficulty GetDifficulty()
        {
            return m_difficulty;
        }

        public EnemyState GetEnemyState()
        {
            return m_enemyState;
        }

        public void SetEnemyState(EnemyState a_enemyState)
        {
            m_enemyState = a_enemyState;
        }

        public CharFaceState GetFaceState()
        {
            return m_CharFaceState;
        }

        public CharStates GetCharState()
        {
            return m_CharState;
        }

        public GameState GetGameState()
        {
            return m_GameState;
        }

        public LevelState GetLevelState()
        {
            return m_LevelState;
        }


        public void SetCharFaceState(CharFaceState a_CharFaceState)
        {
            m_CharFaceState = a_CharFaceState;

        }

        public void SetCharState(CharStates a_CharState)
        {
            m_CharState = a_CharState;
        }

        public void SetGameState(GameState a_GameState)
        {
            m_GameState = a_GameState;
        }

        public void SetLevelState(LevelState a_LevelState)
        {
            m_LevelState = a_LevelState;
        }
        internal bool IsLevelComplete()
        {
            if (m_GameState == GameState.COMPLETTE)
            {
                return true;
            }
            return false;
        }
       
    }
}
