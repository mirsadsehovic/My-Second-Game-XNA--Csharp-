using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Umbra_development.Model
{
  public class Model
    {
       // const float m_rightEdge = 1;

      public enum EnemyState
      {
          STANDING,
          CHASING,

      };
      EnemyState m_enemyState = EnemyState.STANDING;

        public StateHandler m_stateHandler;
        private View.View m_view;
        private Levels m_levels;
        public Player m_player;
        
        public Save m_save;
        private PlayerLevelStatsSave m_PLSSave;
        private Enemy m_modelEnemy;
        private EnemyBolt m_modelEnemyBolt;
        
        private List<Enemy> m_enemy;
        private List<EnemyBolt> m_boltEnemy; 
        // private Enemy m_enemy2 = new Enemy();
        private Song m_song;
        bool m_hasCollidedWithGround = false;
        bool m_enemyCollidedWithGround = false;
        bool m_levelFinished = false;
        //private int m_playerHp;
        private float damageTimer = 0;
        private bool hasBeenHit = false;
        public static int m_livesLeft = 100;
        private bool hasReachedEnd = false;
  
    public Model(Player a_player, StateHandler a_stateHandler, Levels a_levels, Enemy a_enemy, EnemyBolt a_enemyBolt)
    {
        m_enemy = new List<Enemy>();
        m_boltEnemy = new List<EnemyBolt>();
        m_player = a_player;
        m_stateHandler = a_stateHandler;
        m_levels = a_levels;
        m_modelEnemy = a_enemy;
        m_modelEnemyBolt = a_enemyBolt;
        // m_view = new View.View(a_manager, a_contentLoader, a_graphics);
        m_save = new Save(m_player, m_stateHandler, m_levels, this);
        SetEnemies();
        //  m_view = a_view;
  
    }

    public Vector2 GetEnemyPosition()
    {
        return m_modelEnemy.GetPosition();
    }

    public void SetEnemies()
    {
        Levels e_levels = new Levels();
        List<Vector2> enemyPosition = e_levels.GetEnemyPositions();
        List<Vector2> enemyBoltPosition = e_levels.GetEnemyBoltPositions();
        //  List<Vector2> enemyBoltPosition = m_levels.SetEnemyBoltPosition();
        Enemy enemy;
        for (int i = 0; i < enemyPosition.Count; i++)
        {
            enemy = new Enemy();
            enemy.GetEnemyDamage();
            enemy.SetPosition(enemyPosition[i].X, enemyPosition[i].Y);
            m_enemy.Add(enemy);

        }

        EnemyBolt boltEnemy;
        for (int i = 0; i < enemyBoltPosition.Count; i++)
        {
            boltEnemy = new EnemyBolt();
            boltEnemy.SetPosition(enemyBoltPosition[i].X, enemyBoltPosition[i].Y);
            m_boltEnemy.Add(boltEnemy);
        }
    }
    public void Update(float a_elapsedTime ,SoundObserver a_sound, ILevelObserver a_observer, int difficulty)
    {  
        m_levels.GetLevelState(m_stateHandler.GetLevelState());
        UpdatePlayer(a_elapsedTime, a_sound, a_observer, difficulty);
        UpdateEnemy(a_elapsedTime, a_sound);
        UpdateBoltEnemy(a_elapsedTime);
        DamageTimer(a_elapsedTime);
            
    }

    //EN TIMMER SOM HÅLLER REDA PÅ NÄR FIENDERA SKADAR.
    public void DamageTimer(float a_elapsedTime)
    {
        if (hasBeenHit)
        {
            damageTimer += a_elapsedTime;
            if (damageTimer >= 2)
            {

                hasBeenHit = false;
                damageTimer = 0;
            }
            else
            {
                hasBeenHit = true;
            }
        }
         
    }
    //EN UPPDATE SOM HÅLLER REDA PÅ SPELAREN
    public void UpdatePlayer(float a_elapsedTime, SoundObserver a_sound, ILevelObserver a_observer, int difficulty)
    {
          
        Vector2 oldPos = m_player.GetPosition();
        //get a new position for the player
        m_player.Update(a_elapsedTime);
        Vector2 newPos = m_player.GetPosition();
        m_hasCollidedWithGround = false;
        Vector2 speed = m_player.GetSpeed();
        Vector2 afterCollidedPos = Collide(oldPos, newPos, m_player.m_sizes, ref speed, out m_hasCollidedWithGround, a_elapsedTime, a_observer);
        //set the new speed and position after collision
        m_player.SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
        m_player.SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
        m_player.SetSpeed(speed.X, speed.Y);

        //KOLLAR OM MAN HAR KOLLIDERAT MED EN TEDDY
        if (m_levels.IsCollidingAtTeddy(newPos, m_player.m_sizes, a_sound))
        {
               
            m_player.SetSpeed(-2, 0);
            a_sound.ScreamSound();
         
        }
        /*if(m_levels.IsCollidingAtEnemy(newPos, m_player.m_sizes, m_player))
        {
            if(!hasBeenHit)
            {
                m_player.SetLifeChange(m_modelEnemy.GetEnemyDamage(), true);
                hasBeenHit = true;
            }
        }*/
        //KOLLAR OM MAN HAR KOLLIDERAT MED FÄLLORA
        if (m_levels.IsCollidingAtTrap(newPos, m_player.m_sizes))
        {
            DoDeath(a_observer);
        }
        /* if (m_levels.IsCollidingAtBoltEnemy(newPos, m_player.m_sizes))
        {
            m_player.SetPosition(5, 15);
            m_player.SetSpeed(0, 0);
        }*/
        
        //KOLLAR OM MAN KILLIDERAT MED HJÄRTAT; INTE IMPLEMENTERAT MEN FUNKTIONEN FINNS
        if (m_levels.IsCollidingAtHeart(newPos, m_player.m_sizes, a_sound))
        {
            // m_playerHp += 50;
        }
        
        //KOLLAR KOLLISIONEN MED RUSHINGENEMY
        if(CollideWithRushingEnemy(newPos))
        {
            //  m_levels.Restart(a_observer);
            if (!hasBeenHit)
            {
                m_player.SetLifeChange(m_modelEnemy.GetEnemyDamage(), true);
                hasBeenHit = true;
            }
            if (!m_player.IsAlive())
            {
                DoDeath(a_observer);
            }
        }
       
        //KOLLAR KOLLISIONEN MED BOLT ENEMY
        if (CollideWithBoltEnemy(newPos))
        {
            if (!hasBeenHit)
            {
                m_player.SetLifeChange(m_modelEnemyBolt.GetEnemyDamage(), true);
                hasBeenHit = true;
            }
            if (!m_player.IsAlive())
            {
                DoDeath(a_observer);
            }
        }
       
        //KOLLAR OM MAN HAR KOLLIDERAT MED SLUTET AV BANAN.
        if (afterCollidedPos.X > Levels.g_levelWidth - 3)
        {
            speed = new Vector2(0.0f, 0.0f);
            afterCollidedPos = new Vector2(5.0f, 0.0f);
            hasReachedEnd = true;
           
        }
    }
     
    //KOLLAR OM SPELARENS HÄLSA ÄR UNDER 40 PROCENT AV URSPRUNGLIGA LIVEN.
    public bool IsHealthBelowFortyPercent()
    {
        if (m_player.GetLife() <= m_player.GetMaxHealth(m_stateHandler.GetDifficulty()) * 0.4)
        {
            return true;
        }
        return false;
    }

    public bool EndReached()
    {
        if (hasReachedEnd)
        {
            return true;
        }
        return false;
    }

    private void LevelComplete()
    {

        if (m_stateHandler.GetLevelState() == StateHandler.LevelState.LEVEL1)
        {
            m_stateHandler.SetLevelState(StateHandler.LevelState.LEVEL2);
            m_levels.GenerateLevel();
        }
        if (m_stateHandler.GetLevelState() == StateHandler.LevelState.LEVEL2)
        {
            // m_stateHandler.SetLevelState(StateHandler.LevelState.LEVEL3);
        }

        //  m_player.SetPosition(5, 15);
        
    }
    private bool CollideWithRushingEnemy(Vector2 m_playerPos)
    {
        Vector2 speed = m_player.GetSpeed();
        for (int i = 0; i < m_enemy.Count; i++)
        {
            if ((m_enemy[i].GetPosition() - m_playerPos).Length() < 1)
            {
                //m_enemy.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    private bool CollideWithBoltEnemy(Vector2 m_playerPos)
    {
        for (int i = 0; i < m_boltEnemy.Count; i++)
        {
            if ((m_boltEnemy[i].GetPosition() - m_playerPos).Length() < 1)
            { 
                //m_enemy.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
 
      //EN UPPDATE SOM HÅLLER REDA PÅ NÄR CHASE ENEMY SKA JAGA FIENDEN OCH VILKEN HASTIGHET: HHÄR SÄTTS ÄVEN FUNCTIONEN SÅ DEM HAN HOPPA ÖVER TILES.
    internal void UpdateEnemy(float a_elapsedTime, SoundObserver a_soundobserver)
    {
        for (int i = 0; i < m_enemy.Count; i++)
        {           
            float distanceFromPlayer = Vector2.Distance(m_enemy[i].GetPosition(), m_player.GetPosition());  
            //if (m_player.GetPosition > chaseThershold)
            { //}
                Vector2 a_playerPosition = m_player.GetPosition();
                Vector2 lastPosition = m_enemy[i].GetPosition();
                m_enemy[i].Update(a_elapsedTime);
                Vector2 enemyPosition = m_enemy[i].GetPosition();
                Vector2 speed = m_enemy[i].GetSpeed();
                Vector2 afterCollidedPos = EnemyCollide(lastPosition, enemyPosition, m_enemy[i].m_size, ref speed, out m_enemyCollidedWithGround, i, a_elapsedTime);

                    //m_enemy[i].m_CurrentState();
                    /*if (distanceFromPlayer < 10)
                    {
                        speed.X = -10;
                    
                    }*/

                    if (enemyPosition.X - m_player.GetPosition().X < 0.2f && enemyPosition.X - m_player.GetPosition().X > -0.2f)
                    {
                        speed.X = 0;
                    }

                    else if (enemyPosition.X - m_player.GetPosition().X <= 7 && enemyPosition.X - m_player.GetPosition().X >= -7 && enemyPosition.Y - m_player.GetPosition().Y <= 4 && enemyPosition.Y - m_player.GetPosition().Y >= -4)
                    {
                         
                            m_modelEnemy.SetState(2);
                            if (enemyPosition.X > m_player.GetPosition().X)
                            {
                                speed.X = -4;
                            }
                          
                            else if (enemyPosition.X < m_player.GetPosition().X)
                            {

                                speed.X = 3.5f;
                            }

                        }

                    else
                    {
                            m_modelEnemy.SetState(1);
                            speed.X = 0;
                    }
                        

                m_enemyCollidedWithGround = false;
                m_enemy[i].SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
                m_enemy[i].SetSpeed(speed.X, speed.Y);
            }
        }
    }
      //EN UPPDATE FÖR BOLT ENEMY; SOM HÅLLER REDA PÅ NÄR DEN SKA FLYGA MOT SPELAREN
    internal void UpdateBoltEnemy(float a_elapsedTime)
    {
        for (int i = 0; i < m_boltEnemy.Count; i++)
        {

            float distanceFromPlayer = Vector2.Distance(m_boltEnemy[i].GetPosition(), m_player.GetPosition());
                //}
                Vector2 a_playerPosition = m_player.GetPosition();

                Vector2 lastBoltPosition = m_boltEnemy[i].GetPosition();
                m_boltEnemy[i].Update(a_elapsedTime);

                Vector2 newPosition = m_boltEnemy[i].GetPosition();
                Vector2 speed = m_boltEnemy[i].GetSpeed();

                //m_enemy[i].m_CurrentState();
                if (newPosition.X - m_player.GetPosition().X <= 10 && newPosition.Y - m_player.GetPosition().Y <= 2)
                {
                    // if (newPosition.X > m_player.GetPosition().X && newPosition.Y > m_player.GetPosition().Y)
                    //{

                        speed.X = -7;
                    //}
                }

                m_enemyCollidedWithGround = false;

                m_boltEnemy[i].SetSpeed(speed.X, speed.Y);

            }
        }
        

    //TAR HAND OM FIENDRAS KOLLISION
    private Vector2 EnemyCollide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, out bool a_outCollideGround, int i, float a_elapsedTime)
    {
        a_outCollideGround = false;
        //Can we move to the position safely?
        Vector2 speed = m_enemy[i].GetSpeed();
          
        Vector2 lastPosition = m_enemy[i].GetPosition();
        m_enemy[i].Update(a_elapsedTime);


        Vector2 newPosition = m_enemy[i].GetPosition();
        if (m_levels.IsCollidingAt(a_newPos, a_size))
        {
            //if not try only the X movement, indicates that a collision with ground or roof has occured

            Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);

            if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
            {
                xMove.Y = (int)a_oldPos.Y + 0.99f;
            }

            if (m_levels.IsCollidingAt(xMove, a_size) == false)
            {
                //did we collide with ground?
                if (a_velocity.Y > 0)
                {

                    a_outCollideGround = true;
                    // m_enemy[i].SetState();
                    a_velocity.Y = 0; //no bounce
                }
                else
                {
                    //collide with roof
                    a_velocity.Y *= -1.0f; //reverse the y velocity and some speed lost in the collision
                }
                a_velocity.X *= 0.10f;// friction should be time-dependant

                return xMove;
            }
            else
            {
                //try Y movement, indicates that a collision with wall has occured
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_levels.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X *= 0.5f;
                    speed.Y = -8.0f;
                    return yMove;
                }

                if (a_velocity.Y > 0)
                {
                    a_outCollideGround = true;
                }
                a_velocity.X = 0; //no bounce
                a_velocity.Y = -7; //no bounced

            }
            //remain at the same position
            return a_oldPos;
        }

        return a_newPos;
    }

    private Vector2 EnemyBoltCollide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, out bool a_outCollideGround, int i)
    {
        a_outCollideGround = false;
        //Can we move to the position safely?


        if (m_levels.IsCollidingAt(a_newPos, a_size))
        {
            //if not try only the X movement, indicates that a collision with ground or roof has occured

            Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);

            if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
            {
                xMove.Y = (int)a_oldPos.Y + 0.99f;
            }


            if (m_levels.IsCollidingAt(xMove, a_size) == false)
            {
                //did we collide with ground?
                if (a_velocity.Y > 0)
                {

                    a_outCollideGround = true;
                    // m_enemy[i].SetState();
                    a_velocity.Y = 0; //no bounce
                }
                else
                {
                    //collide with roof
                    a_velocity.Y *= -1.0f; //reverse the y velocity and some speed lost in the collision
                }

                a_velocity.X *= 0.10f;// friction should be time-dependant

                return xMove;
            }
            else
            {
                //try Y movement, indicates that a collision with wall has occured
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_levels.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X *= 0.5f;
                    return yMove;
                }

                if (a_velocity.Y > 0)
                {
                    a_outCollideGround = true;
                }
                a_velocity.X = 0; //no bounce
                a_velocity.Y = 0; //no bounce

            }
            //remain at the same position
            return a_oldPos;
        }

        return a_newPos;
    }
    internal List<Vector2> GetEnemyPositions()
    {
        List<Vector2> enemyPositions = new List<Vector2>(m_enemy.Count);
        for (int i = 0; i < m_enemy.Count; i++)
        {
            enemyPositions.Add(m_enemy.ElementAt(i).GetPosition());
        }
        return enemyPositions;
    }
    
    internal List<Vector2> GetBoltEnemyPositions()
    {
        List<Vector2> enemyBoltPositions = new List<Vector2>(m_boltEnemy.Count);
        for (int i = 0; i < m_boltEnemy.Count; i++)
        {
            enemyBoltPositions.Add(m_boltEnemy.ElementAt(i).GetPosition());
        }
        return enemyBoltPositions;
    }

    //TAR HAND OM SPELAREN KOLLISIONE
    private Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, out bool a_outCollideGround, float a_elapsedTime, ILevelObserver a_observer)
    {
        a_outCollideGround = false;
        // m_hasCollidedWithGround = false;
        if (m_levels.IsCollidingAt(a_newPos, a_size))
        {
            //if not try only the X movement, indicates that a collision with ground or roof has occured
            Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);

            if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
            {
                xMove.Y = (int)a_oldPos.Y + 0.99f;
            }
            if (m_levels.IsCollidingAt(xMove, a_size) == false)
            {
                //did we collide with ground?
                if (a_velocity.Y > 0.0f)
                {
                    a_outCollideGround = true;
                    a_velocity.Y = 0; //no bounce
                }
                else
                {
                    //collide with roof
                    a_velocity.Y *= 0.0f; //reverse the y velocity and some speed lost in the collision
                }
                a_velocity.X *= 0.10f;// friction should be time-dependant
                return xMove;
            }
            else
            {
                //try Y movement, indicates that a collision with wall has occured
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_levels.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X *= 0.5f;
                    return yMove;
                }

                if (a_velocity.Y > 0)
                {
                    a_outCollideGround = true;
                }
                a_velocity.X = 0; //no bounce
                a_velocity.Y = 0; //no bounce

            }
            //remain at the same position
            return a_oldPos;
        }
        return a_newPos;
    }

    internal bool CanJump()
    {
        return m_hasCollidedWithGround;
    }

    internal void DoJump()
    {
        m_player.DoJump();
    }
    internal void DoSave(Player a_player, StateHandler a_stateHandler, Levels a_levels, Model a_model, Save a_save)
    {
        a_save = new Save(m_player, a_stateHandler, a_levels, a_model);
        a_save.InitiateSave();
    }

    internal void DoLoad(Save a_save)
    {
        a_save.InitiateLoad();
    }

    internal void DoPLSSave()
    {
        m_PLSSave.InitiateSave();
    }

    internal void DoPLSLoad()
    {
        m_PLSSave.InitiateLoad();
    }

    internal void DoRight()
    {
        // m_player.SetSpeed(, m_player.GetSpeed().Y);
        m_player.DoRight();
    }

    internal void DoLeft()
    {
        //m_player.SetSpeed(-10, m_player.GetSpeed().Y);
        m_player.DoLeft();
    }

    internal bool isLevelComplete()
    {
        return m_levelFinished;
        
    }

    internal void DoDeath(ILevelObserver a_observer)
    {
        a_observer.Death();
    }
    //DÖR MAN KÖRS DENNA FUNCTION IGÅNG OCH RESETTAR LEVEL; LIVEN; , MEN BEHÅLLER SVÅRIGHETEN.
    internal void DoRevive(int a_level)
    {
        m_levels.GenerateLevel();
        m_player.SetToStartPosition(a_level);
        m_player.SetDifficultyHealth(m_stateHandler.GetDifficulty());
        m_player.SetSpeed(0, 0);
    }

    internal Levels GetLevel()
    {
        return m_levels;
    }

    public Vector2 GetPlayerPosition()
    {
        return m_player.GetPosition();
    }

    internal float GetPlayerSpeed()
    {
        return m_player.GetSpeed().Length();
    }

    internal float GetPlayerXSpeed()
    {
        return m_player.GetSpeed().X;
    }

    internal bool isPlayerInAir()
    {
        return m_player.isPlayerFalling();
    }
       
    internal StateHandler.LevelState GetLevelState()
    {
        return m_stateHandler.GetLevelState();
    }
    private bool m_difficultyIsSet = false;

    //FUNCTION SOM SÄTTER SVÅRIGHETSGRADEN:
    internal void SetDifficulty(StateHandler.Difficulty difficulty)
    {
        m_player.SetDifficultyHealth(difficulty);
        m_modelEnemy.SetEnemyDamage(difficulty);
        m_modelEnemyBolt.SetEnemyDamage(difficulty);
        m_difficultyIsSet = true;
    }
     
    internal bool DifficultyIsSet()
    {
        return m_difficultyIsSet;
    }
   
    internal int GetLife()
    {
        return m_player.GetLife();
    }
        
    }
}
