using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XRpgLibrary.TileEngine
{
    public enum CollisionType
    {
        Passable = 0,
        Impassable = 1
    }

    public class CollisionLayer
    {
        public const int CollisionRadius = 8;

        private readonly Dictionary<Point, CollisionType> collisions;

        public Dictionary<Point, CollisionType> Collisions
        {
            get { return collisions; }
        }

        public CollisionLayer()
        {
            collisions = new Dictionary<Point, CollisionType>();
        }
    }
}
