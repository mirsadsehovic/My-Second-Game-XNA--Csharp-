using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace Umbra_development.Model
{
   public class ChaseGhost
    {

        Vector2 m_centerBottomPosition = new Vector2(0.0f, 18f);
        Vector2 m_speed = new Vector2(0, 0);
        public Vector2 m_sizes = new Vector2(0.8f, 0.8f);
        private Vector2 gravityAcceleration = new Vector2(0.0f, 20.82f);


        internal Vector2 GetChasePosition()
        {
            return m_centerBottomPosition;
        }

        internal void Update(float a_elapsedTime)
        {
            //Vector2 gravityAcceleration = new Vector2(0.0f, 20.82f);

            //integrate position
            m_centerBottomPosition = m_centerBottomPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;
            //integrate speed
            m_speed = m_speed + a_elapsedTime * gravityAcceleration;

        }

        internal void DoJump()
        {
            m_speed.Y = -10; //speed upwards
        }

        internal void DoRight()
        {
            m_speed.X = 5;
        }

        internal void DoLeft()
        {
            m_speed.X = -5;
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

        internal void SetSpeedRight(int a_x)
        {
            m_speed.X = a_x;
        }





    }
}
