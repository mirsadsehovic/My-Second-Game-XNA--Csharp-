using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Umbra_development.View
{
    class AshesSystem
    {
        private const int NUM_OF_PARTICLES = 100;
        private const float DROP_SPEED = 2.0f;
        private readonly Random m_rand; 

        AshesParticle[] m_raindrops = new AshesParticle[NUM_OF_PARTICLES];


        /// <summary>
        /// Default constructor.
        /// Generates a defined amount of raindrops to be displayed and stores them in an array.
        /// </summary>
        public AshesSystem()
        {
            m_rand = new Random(10);

            for (int i = 0; i < NUM_OF_PARTICLES; i++)
            {
                m_raindrops[i] = GenerateRainDrop();
            }
        }

        /// <summary>
        /// Generates a RainDropParticle to be displayed on the screen.
        /// It is given a (more or less) random X-position, speed and lifetime.
        /// </summary>
        /// <returns>A complete RainDropParticle</returns>
        private AshesParticle GenerateRainDrop()
        {
            Vector2 position = new Vector2((float)m_rand.NextDouble(), 0);

            Vector2 velocity = new Vector2(-0.1f, 0.5f);
            velocity *= DROP_SPEED * ((float)m_rand.NextDouble() + 0.5f);

            float lifetime = (float)m_rand.NextDouble() * 10;

            return new AshesParticle(position, velocity, lifetime);
        }


        /// <summary>
        /// Draws the raindrops to the screen.
        /// The longer a raindrop have lived, the more transperant i gets.
        /// When it becomes fully transperant it have expired its lifetime and a new raindrop is generated to replace it.
        /// </summary>
        /// <param name="a_spriteBatch">The SpriteBatch we use to draw with</param>
        /// <param name="a_texture">The texture we want to use for the raindrop</param>
        /// <param name="a_elapsedTime">How much time have elapsed since the game was started</param>
        internal void DrawAshes(SpriteBatch a_spriteBatch, Texture2D a_texture, float a_elapsedTime)
        {
            for (int i = 0; i < NUM_OF_PARTICLES; i++ )
            {
                if (m_raindrops[i].RemainingLife <= 0)
                {
                    m_raindrops[i] = GenerateRainDrop();
                }

                m_raindrops[i].Update(a_elapsedTime / 10.0f);

                Vector2 modelPosition = m_raindrops[i].Position;

                Vector2 viewPosition = modelPosition * 1500.0f + new Vector2(0, -50);

                int dropViewSize = 7;

                float opacity = m_raindrops[i].Opacity;
                Color color = new Color(opacity, opacity, opacity, opacity);
                
                Rectangle destinationRectangle = new Rectangle((int)viewPosition.X - dropViewSize / 2, (int)viewPosition.Y - dropViewSize / 2, dropViewSize, dropViewSize); 
                a_spriteBatch.Draw(a_texture, destinationRectangle, color);
            }
        }
    }
}