using System;
using System.Collections.Generic;
using System.Text;

namespace DemoGame.ClassID
{
    public class ClassIdConfiguration : IClassIDConfiguration
    {
        private Dictionary<Type,int> _classToIdHash;

        public void Initialize()
        {
            _classToIdHash = new Dictionary<Type, int>();
            _classToIdHash.Add(typeof(Player), BitConverter.ToInt32(Encoding.UTF8.GetBytes("PLYR")));
            _classToIdHash.Add(typeof(MoveCommand), BitConverter.ToInt32(Encoding.UTF8.GetBytes("CMVE")));
        }

        public int GetClassID<T>() => _classToIdHash[typeof(T)];
    }
}