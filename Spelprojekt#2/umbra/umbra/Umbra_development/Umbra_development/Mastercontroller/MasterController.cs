using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using Umbra_development.View;


namespace Umbra_development.Mastercontroller
{
    class MasterController
    {
        private Model.Model m_model;
        private View.View m_view;
       
        private View.Camera m_camera = new View.Camera();
        private Model.StateHandler m_statehandler = new Model.StateHandler();
        private Model.ILevelObserver m_levelobserver;
       
        private MouseState m_mouseState;
        
       
        public IAsyncResult result;
        private GraphicsDeviceManager m_manager;
        private View.Menu m_menu;
        bool difficultyIsSet;
        private GameController m_gameController;
       
        public MasterController(GraphicsDeviceManager a_manager, ContentManager a_contentManager, GraphicsDevice a_graphics)
        {

            m_view = new View.View(a_manager, a_contentManager, a_graphics);
            m_menu = new View.Menu(a_manager, a_contentManager, m_view);
            m_gameController = new GameController(m_view, m_camera, m_menu, m_manager, a_contentManager);
            m_manager = a_manager;

        }
        //Om spelet är aktivt anropas Draw i GameControllen, Om inte så skall Menu vara utritat.
        internal void Draw(float a_elapsedTime, GraphicsDeviceManager a_manager, GraphicsDevice GraphicsDevice, Model.StateHandler.CharStates a_charState, Model.StateHandler.CharFaceState a_charFaceState)
        {
            if (m_menu.GameActive())
            {
                    m_gameController.Draw(a_elapsedTime, GraphicsDevice, a_manager, a_charFaceState, a_charState, difficultyIsSet);
            } 
            else             
            {
                m_menu.DrawMenu(GraphicsDevice, m_mouseState, difficultyIsSet);
            }

        }

        internal bool DidPlayerPressQuit()
        {
            if (m_menu.isQuitgamePressed())
            {
                return true;
            }
            return false;
        }

        internal void Update(float a_elapsedTime)
        {
            m_mouseState = m_menu.getMouseState();

            if (m_gameController.RunGame(m_levelobserver))
            {
                m_gameController.Update(a_elapsedTime);
                
            }

        }
 
    }
}


