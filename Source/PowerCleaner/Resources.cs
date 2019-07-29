// Resources.cs
// Copyright Karel Kroeze, -2019

using System.Linq;
using UnityEngine;
using Verse;

namespace PowerCleaner
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        public static readonly Texture2D SprayIcon;
        public static readonly Texture2D[] WaterSprites;

        static Resources()
        {
            SprayIcon = ContentFinder<Texture2D>.Get( "UI/Icons/Spray" );
            WaterSprites = ContentFinder<Texture2D>.GetAllInFolder( "Water" ).ToArray();
        }
    }
}