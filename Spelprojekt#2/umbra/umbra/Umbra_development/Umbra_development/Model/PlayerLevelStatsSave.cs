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
    public class PlayerLevelStatsSave
    {
        StorageDevice device;
        string containerName = "MyGamesStorage";
        string filename = "Levelstats.sav";
        public Player m_player;

        private int playerHP;


        [Serializable]
        public struct SaveGame
        {
            public int playerHealthPoints;

        }

        public PlayerLevelStatsSave(Player a_player)
        {
            m_player = a_player;

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
        //MÅSTE LÖSA SÅ ATT BANAN SPARAS OCKSÅ. 
        public void SaveToDevice(IAsyncResult result)
        {

            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                SaveGame SaveData = new SaveGame();
                {
                    SaveData.playerHealthPoints = m_player.GetLife();

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
        public void LoadFromDevice(IAsyncResult result)
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
                playerHP = SaveData.playerHealthPoints;
                m_player.SetLife(playerHP);


            }
        }

    }





}