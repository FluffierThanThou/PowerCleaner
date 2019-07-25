// Water.cs
// Copyright Karel Kroeze, -2019

using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class Water : Thing
    {

        [TweakValue( "PowerCleaner", 10f )]
        private static float _ticksPerCell = 60f;
        public static int TicksPerCell => (int) _ticksPerCell;

        private readonly Vector3 _start;
        private readonly Vector3 _target;
        private readonly int     _duration;
        private int     _ticks;
        private Vector3 _drawPos;


        public Water( Vector3 start, Vector3 target )
        {
            _start    = start;
            _target   = target;
            _duration = (int)( target - start ).magnitude * TicksPerCell;

            Position = _start.ToIntVec3();
            Rotation = Rot4.Random;
            _drawPos = _start;
        }

        public override Vector3 DrawPos => _drawPos;

        public override void Tick()
        {
            base.Tick();

            var progress = (float) _ticks++ / _duration;
            if ( progress <= 1f )
            {
                _drawPos = Vector3.Lerp( _start, _target, progress );
            }
            else
            {
                // CleanAndDespawn();
                Destroy();
            }
        }
    }
}