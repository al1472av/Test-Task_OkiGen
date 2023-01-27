using System;
using ExternalTools.Scripts.Types.ObservableValues;

namespace ConveyorGame.Services.PlayerData
{
    [Serializable]
    public class Data
    {
        public ObservableSecureInt Level;
        
        public Data()
        {
            Level = new ObservableSecureInt(0);
        }
    }
}