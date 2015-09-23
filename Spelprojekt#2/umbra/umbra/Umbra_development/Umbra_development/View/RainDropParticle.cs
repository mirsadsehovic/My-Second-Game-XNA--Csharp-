using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Umbra_development.View
{
   public class RainDropParticle
    {
        private Vector2 m_position = Vector2.Zero;
        private Vector2 m_velocity = Vector2.Zero;
        private Vector2 m_acceleration = new Vector2(1.0f, 10.0f);
        private readonly float m_maxLife;
        private float m_remainingLife;


        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="a_position">The initial position</param>
        /// <param name="a_velocity">The initial velocity</param>
        /// <param name="a_maxLife">The total lifetime</param>
        public RainDropParticle(Vector2 a_position, Vector2 a_velocity, float a_maxLife)
        {
            m_position = a_position;
            m_velocity = a_velocity;
            m_maxLife = a_maxLife;
            m_remainingLife = a_maxLife;
        }


        /// <summary>
        /// Update the position of the raindrop according to how long time the game have been running.
        /// Decreases the remaining lifetime of the raindrop.
        /// </summary>
        /// <param name="a_elapsedTime">The time since the game was started</param>
        internal void Update(float a_elapsedTime)
        {
            m_remainingLife -= 0.1f;

            m_position += m_velocity * a_elapsedTime + m_acceleration * a_elapsedTime * a_elapsedTime;
            m_velocity += m_acceleration * a_elapsedTime;
        }


        /// <summary>
        /// Getter for position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return m_position;
            }
        }


        /// <summary>
        /// Getter for remaining life
        /// </summary>
        internal float RemainingLife
        {
            get
            {
                return m_remainingLife;
            }
        }


        /// <summary>
        /// Getter for opacity
        /// </summary>
        internal float Opacity
        {
            get
            {
                return m_remainingLife / m_maxLife;
            }
        }
    }
}