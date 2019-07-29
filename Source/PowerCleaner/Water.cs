// Water.cs
// Copyright Karel Kroeze, -2019

using System.Linq;
using System.Security.Policy;
using RimWorld;
using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class Water : Thing
    {
        private const int _ticksPerCell = 10;
        private const int _range = 10;
        internal const int _ticksPerSpawn = 3;

        public static int TicksPerCell => (int) _ticksPerCell;
        public Graphic_Sprite Sprite => def.graphicData.Graphic as Graphic_Sprite;

        private const int ProgressSprites = 12;
        private const int SplashSprites = 9;

        private readonly Vector3 _start;
        private readonly Vector3 _target;
        private readonly int     _duration;
        private readonly float _extraRotation;
        private int     _ticks;
        private Vector3 _drawPos;
        
        public Water( Vector3 start, Vector3 target )
        {
            _start    = start;
            if ( ( target - start ).magnitude > _range )
            {
                var dir = ( target - start ).normalized;
                _target = _start + _range * dir;
            }
            else
            {
                _target = target;
            }

            _target.y = _start.y; // set render layer to be same.
            _duration = (int)( ( _target - _start ).magnitude * TicksPerCell );

            Position = _start.ToIntVec3();
            Rotation = Rot4.Random;
            _extraRotation = start.AngleToFlat( target ) + Rand.Gaussian( 0f, 20f );
            _drawPos = _start;
        }

        public override Vector3 DrawPos => _drawPos;

        public override void Tick()
        {
            var progress = (float) _ticks++ / _duration;
            if ( progress <= 1f )
            {
                _drawPos = Vector3.Lerp( _start, _target, progress );
                Sprite.SetIndex( this, (int) ( progress * ProgressSprites ) );
                if ( _drawPos.ToIntVec3() != Position )
                {
                    Position = _drawPos.ToIntVec3();
                    if ( Position.Filled( Map ) )
                    {
                        Impact();
                    }
                    else
                    {
                        MakeWet();
                    }
                }
            }
            else
            {
                Sprite.Next( this );
                if ( Sprite.GetIndex( this ) == 0 )
                {
                    Impact();
                }
            }

            UpdateMesh();
        }

        public override void Destroy( DestroyMode mode = DestroyMode.Vanish )
        {
            base.Destroy( mode );
            Sprite.Notify_Destroyed( this );
        }

        private void UpdateMesh()
        {
            if ( !Spawned )
                return;
            Map.mapDrawer.MapMeshDirty( Position, MapMeshFlag.Things );
        }

        public void Impact()
        {
            // clean filth
            var filth = Position.GetThingList( Map ).OfType<Filth>().FirstOrDefault();
            filth?.ThinFilth();

            MoteMaker.ThrowAirPuffUp( _drawPos, Map );
            MakeWet( true );
            Destroy();
        }

        public void MakeWet( bool alwaysThrowMote = false )
        {
            var pawns = Position.GetThingList( Map ).OfType<Pawn>();
            if ( alwaysThrowMote || pawns.Any() )
            {
                MoteMaker.ThrowAirPuffUp( _drawPos, Map );
            }

            foreach ( var pawn in pawns )
            {
                pawn.needs?.mood?.thoughts?.memories?.TryGainMemoryFast( ThoughtDefOf.SoakingWet );
            }
        }

        public override void DrawAt( Vector3 drawLoc, bool flip = false )
        {
            Graphic.Draw( drawLoc, !flip ? Rotation : Rotation.Opposite, this, _extraRotation );
        }
    }
}