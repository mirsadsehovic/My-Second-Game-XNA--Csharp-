using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Umbra_development.View;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;

namespace Umbra_development.Model
{
public class Save
{
    StorageDevice device;
    string containerName = "MyGamesStorage";//VILKEN MAPP
    string filename = "mysave.sav";//EN FIL VID NAMNET "musave.sav" SPARS PÅ DATORN
    public Player m_player;
    public StateHandler m_stateHandler;
    private Levels m_levels;
    private Model m_model;
    public View.View m_view;
      
    public Vector2 l_PlayerPos;
    public int l_PlayerHealth;
    public StateHandler.Difficulty l_CurrentDifficulty;
    public int l_Level;

    [Serializable]
    public struct SaveGame
    {
        public Vector2 PlayerPosition;
        public StateHandler.Difficulty Difficulty;
        public int PlayerHp;
        public int CurrentLevel;
        public int CurrentViewLevel;
    }
    //KONSTRUKTORN SOM HÅLLER DE VÄRDEN SOM SKALL SPARAS
    public Save(Player a_player, StateHandler a_stateHandler, Levels a_levels, Model a_model)
    {
        m_model = a_model;
        m_player = a_player;
        m_stateHandler = a_stateHandler;
        m_levels = a_levels;
    }
    public void InitiateSave()
    {
        try
        {
            if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.SaveToDevice, null);
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            //Logger.error(“InvalidOperationException”);
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.SaveToDevice, null);
        }
    }      
   //SPARAR NER SPELARENS POSITION; HÄLSA; VILKEN BANA OCH VILKEN SVÅRIGHETSGRAD
    public void SaveToDevice(IAsyncResult result)
    {

        device = StorageDevice.EndShowSelector(result);
        if (device != null && device.IsConnected)
        {
            SaveGame SaveData = new SaveGame();
            {
                SaveData.PlayerPosition = m_player.GetPosition();
                SaveData.PlayerHp = m_player.GetLife();
                SaveData.CurrentLevel = m_levels.GetLevel();
                SaveData.Difficulty = m_stateHandler.GetDifficulty();
                    
                //  SaveData.Level = m_level.GenerateLevel();
            }
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            if (container.FileExists(filename))
                container.DeleteFile(filename);
            Stream stream = container.CreateFile(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
            serializer.Serialize(stream, SaveData);
            stream.Close();
            container.Dispose();
            result.AsyncWaitHandle.Close();
        }
    }
    public void InitiateLoad()   
    {
        try
        {
            if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.LoadFromDevice, null);
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            //Logger.error(“InvalidOperationException”);
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.LoadFromDevice, null);
        }
    }
    //LÄSER AV DEN NEADSPARADE FILEN OCH LADDAR SEDAN UPP DE SPARADE VÄRDEN
    public  void LoadFromDevice(IAsyncResult result)
    {
        device = StorageDevice.EndShowSelector(result);
        IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
        result.AsyncWaitHandle.WaitOne();
        StorageContainer container = device.EndOpenContainer(r);
        result.AsyncWaitHandle.Close();
        if (container.FileExists(filename))
        {
            Stream stream = container.OpenFile(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
            SaveGame SaveData = (SaveGame)serializer.Deserialize(stream);
            stream.Close();
            container.Dispose();
            //Update the game based on the save game file
            l_Level = SaveData.CurrentLevel;

            l_CurrentDifficulty = SaveData.Difficulty;

            l_PlayerPos = SaveData.PlayerPosition;

            l_PlayerHealth = SaveData.PlayerHp;
                


        }
    }


    public int GetSavedHealth()
    {
        return l_PlayerHealth;
    }

    public int GetSavedLevel()
    {
        return l_Level;
    }

    public StateHandler.Difficulty GetSavedDifficulty()
    {
        return l_CurrentDifficulty;
    }

    public Vector2 GetSavedPosition()
    {
        return l_PlayerPos;
    }




}





}