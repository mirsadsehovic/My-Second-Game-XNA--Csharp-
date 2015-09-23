using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Umbra_development.Model
{
   public class Enemy
    {
        
        Vector2 m_centerBottomPosition = new Vector2(0, 0);
        Vector2 m_speed = new Vector2(0f, 0);
        public Vector2 m_size = new Vector2(-0.8f, 0.8f);
        Vector2 m_velocity = Vector2.Zero;
        private Vector2 gravityAcceleration = new Vector2(0.0f, 12.5f);
        private int enemyDamage = 0;

        public enum State
        {
            Standing =0,
            Chasing=1
        }
        public State m_CurrentState = State.Standing;
        internal Vector2 GetPosition()
        {
            return m_centerBottomPosition;
        }
        internal void Update(float a_elapsedTime)
        {
            m_centerBottomPosition = m_centerBottomPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;

            m_speed = m_speed + a_elapsedTime * gravityAcceleration;

        }

        internal void SetState(int i)
        {
            if (i == 1) m_CurrentState = State.Standing;

            if (i == 2) m_CurrentState = State.Chasing;
        }
        internal State GetState()
        {
            return m_CurrentState;
        }


        internal void SetPosition(float a_x, float a_y)
        {
            m_centerBottomPosition.X = a_x;
            m_centerBottomPosition.Y = a_y;
        }
        internal Vector2 GetSpeed()
        {
            return m_speed;
          
        }
        internal void SetSpeed(float a_x, float a_y)
        {
            m_speed.X = a_x;
            m_speed.Y = a_y;
        }

        internal void SetEnemyDamage(StateHandler.Difficulty a_difficulty)
        {
            switch (a_difficulty)
            {
                case StateHandler.Difficulty.EASY:
                    enemyDamage = 25;
                    break;
                case StateHandler.Difficulty.MEDIUM:
                    enemyDamage = 30;
                    break;
                case StateHandler.Difficulty.HARD:
                    enemyDamage = 40;
                    break;
                default:
                    enemyDamage = 0;
                    return;
            }

        }

        public int GetEnemyDamage()
        {
            return enemyDamage;
        }

        internal void Update(float a_elapsedTime, Levels a_level)
        {

            Vector2 gravityAcceleration = new Vector2(0.0f, 14.0f);
            //integrate position
            m_centerBottomPosition = m_centerBottomPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;
            //integrate speed
            m_speed = m_speed + a_elapsedTime * gravityAcceleration;
        }
      
       /* internal float CloseToPlayer()
        {
          //  if(m_CurrentState == State.Standing)
         //  {
              //  m_speed.X = 0;
            //}

          //  else if (m_CurrentState == State.Chasing)
          //  {
                if ((m_centerBottomPosition - m_player.GetPosition()).Length() < 0)
                {
                    //m_speed.X = -10;
                  return 2f;
                }
                if ((m_centerBottomPosition - m_player.GetPosition()).Length() > 0)
                {
                   // m_speed.X = 10;
                    return -2f;
                }

          //  }
            return 0f;
        }*/

    }
}
