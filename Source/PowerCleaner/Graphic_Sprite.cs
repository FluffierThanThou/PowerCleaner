// Graphic_Sprite.cs
// Copyright Karel Kroeze, -2019

using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class Graphic_Sprite : Graphic_Collection
    {
        private static Dictionary<Thing, int> _indeces = new Dictionary<Thing, int>();

        public override Material MatSingleFor( Thing thing )
        {
            return MatSingle;
        }

        public override Material MatSingle => subGraphics[0].MatSingle;

        public override void DrawWorker( Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation )
        {
            subGraphics[GetIndex(thing)].DrawWorker( loc, rot, thingDef, thing, extraRotation );
        }

        public int GetIndex( Thing thing )
        {
            if ( _indeces.TryGetValue( thing, out int index ) )
                return index;
            return 0;
        }

        public void SetIndex( Thing thing, int index )
        {
            _indeces[thing] = index % subGraphics.Length;
        }

        public void Next( Thing thing )
        {
            SetIndex( thing, GetIndex( thing ) + 1 );
        }

        public void Notify_Destroyed( Thing thing )
        {
            _indeces.Remove( thing );
        }
    }
}