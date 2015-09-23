using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Umbra_development.Model
{
  public class EnemyBolt
    {
      //  private Player m_player = new Player();
        Vector2 m_centerBottomPosition = new Vector2(0, 0);
        Vector2 m_speed = new Vector2(0.0f, 0f);
        public Vector2 m_size = new Vector2(0.0f, 0.0f);
        Vector2 m_velocity = Vector2.Zero;
        private Vector2 gravityAcceleration = new Vector2(0.0f, 0.0f);
        private int enemyDamage = 0;

        public enum State
        {
            Standing = 0,
            Chasing = 1
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
                    enemyDamage = 50;
                    break;
                case StateHandler.Difficulty.MEDIUM:
                    enemyDamage = 75;
                    break;
                case StateHandler.Difficulty.HARD:
                    enemyDamage = 100;
                    break;
                    default:
                    enemyDamage = 50;
                    return;
            }

        }

        public int GetEnemyDamage()
        {
            return enemyDamage;
        }

        internal float CloseToPlayer()
        {
            //  if(m_CurrentState == State.Standing)
            //  {
            //  m_speed.X = 0;
            //}

            //  else if (m_CurrentState == State.Chasing)
            //  {
            /*if ((m_centerBottomPosition - m_player.GetPosition()).Length() < 0)
            {
                //m_speed.X = -10;
                return 2f;
            }
            if ((m_centerBottomPosition - m_player.GetPosition()).Length() > 0)
            {
                // m_speed.X = 10;
                return -2f;
            }*/

            //  }
            return 0f;
        }

    }
}