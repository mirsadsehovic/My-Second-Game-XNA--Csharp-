using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Umbra_development.Model
{
public class Player
{
    //Vector2 m_centerBottomPosition = new Vector2(0.0f, 18.5f);
    Vector2 m_startPosition = new Vector2(9.0f, 16.0f);
    Vector2 m_playerPosition;
    Vector2 m_speed = new Vector2(0f, 0f);
    public Vector2 m_sizes = new Vector2(-0.8f, 0.8f);
    //private Vector2 gravityAcceleration = new Vector2(0.0f, 20.82f);
    private Enemy m_enemy = new Enemy();
    private Enemy m_enemyBolt;

    public Player()
    {
        m_playerPosition = m_startPosition;
    }
    public Vector2 PlayerPosition
    {
        get { return m_playerPosition; }
        set { m_playerPosition = value; }
    }
    //EN UPPDATE SOM HÅLLER REDA PÅ SPELAREN GRAVITATION 
    public void Update(float a_elapsedTime)
    {
        Vector2 gravityAcceleration = new Vector2(0.0f, 20.0f);
        //integrate position
        m_playerPosition = m_playerPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;

        //integrate speed
        m_speed = m_speed + a_elapsedTime * gravityAcceleration;

    }
    //SÄTTER SPELAREN HOPP HASTIGHETS OCH RIKTNING
    internal void DoJump()
    {
        // gravityAcceleration = new Vector2(0.0f, 14.0f);
        m_speed.Y = -10.0f; //speed upwards    
    }

    internal void DoRight()
    {
        m_speed.X = 4f;
    }

    internal void DoLeft()
    {
        m_speed.X = -4f;
    }

    public void SetPosition(float a_x, float a_y)
    {
        m_playerPosition.X = a_x;
        m_playerPosition.Y = a_y;
    }

    public Vector2 GetSpeed()
    {

        return m_speed;
    }
    internal Vector2 GetSize()
    {
        return m_sizes;
    }

    public void SetSpeed(float a_x, float a_y)
    {
        m_speed.X = a_x;
        m_speed.Y = a_y;

    }

    public void SetSpeedRight(int a_x)
    {
        m_speed.X = a_x;
    }
    public Vector2 GetPosition()
    {
        return PlayerPosition;

    }
    //SWITCH SATTS SOM SÄTTER START POSITION FÖR SPELAREN BEROENDE PÅ BANA
    public void SetToStartPosition(int a_level)
    {
        switch (a_level)
        {
            case 1:
                m_startPosition.Y = 16;
                break;
            case 2:
                m_startPosition.Y = 23.5f;
                break;
            case 3:
                m_startPosition.Y = 27.5f;
                break;
            default:
                return;
        }
        m_playerPosition = m_startPosition;
    }

    private int m_lives = 10;
    private StateHandler.Difficulty m_gameMode;
    //FUNCKTION SOM SÄTTER SPELARENS LIV BEOROENDE PÅ SVÅRIGHETSGRAD
    internal void SetDifficultyHealth(StateHandler.Difficulty a_difficulty)
    {
        m_gameMode = a_difficulty;
        switch (a_difficulty)
        {
            case StateHandler.Difficulty.EASY:
                m_lives = 200;
                break;
            case StateHandler.Difficulty.MEDIUM:
                m_lives = 100;
                break;
            case StateHandler.Difficulty.HARD:
                m_lives = 50;
                break;
        }

    }

    public double GetMaxHealth(StateHandler.Difficulty a_difficulty)
    {
        double maxHealth = 0;
        switch (a_difficulty)
        {
            case StateHandler.Difficulty.EASY:
                maxHealth = 200;
                break;
            case StateHandler.Difficulty.MEDIUM:
                maxHealth = 100;
                break;
            case StateHandler.Difficulty.HARD:
                maxHealth = 50;
                break;
        }
        return maxHealth;
    }

    internal void SetLife(int a_health)
    {

        m_lives = a_health;

    }
    //FUNKTION SOM KOLLAR OM LIVET MINSKAR
    internal void SetLifeChange(int healthChange, bool isDamage)
    {
            
        if (isDamage)
        {
            m_lives -= healthChange;
        }
        else
        {
            m_lives += healthChange;
        }


    }

    internal bool IsAlive()
    {
        if (m_lives <= 0)
        {
            return false;
        }
        return true;
    }



    internal int GetLife()
    {
        return m_lives;
    }

    internal bool isPlayerFalling()
    {
        if (m_speed.Y > 0)
        { return true; }
        return false;
    }

    /* internal bool isPlayerMoving()
        {
            if (m_speed.X == 0 && m_speed.Y == 0)
            {
                return false;
            
            }
            return true;
        
        }
        */



}
}
