using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.Logger;
using Microsoft.Xna.Framework;
using ECSAttempt.ECS;
using ECSAttempt.ECS.Components;

namespace ECSAttemptTest {
    public static class Utils {
        private static LogHelper logHelper = new LogHelper();

        public static void Log(string message, LogType logType = LogType.Info) {
            logHelper.Log(LogTarget.Console, LogPrefix.Game, logType, message);
        }

        public static bool Contains(ValueTuple<SpriteComponent, PositionComponent> vt, Vector2 pos) {
            var v = (sprite: vt.Item1, pos: vt.Item2);

            if (pos.X < v.pos.Position.X)
                return false;
            if (pos.X > (v.pos.Position.X + v.sprite.Texture2D.Width))
                return false;
            if (pos.Y < v.pos.Position.Y)
                return false;
            if (pos.Y > (v.pos.Position.Y + v.sprite.Texture2D.Height))
                return false;

            return true;
        }

        public static bool Intersects(ValueTuple<SpriteComponent, PositionComponent> vt0, ValueTuple<SpriteComponent, PositionComponent> vt1) {
            var v0 = (sprite: vt0.Item1, pos: vt0.Item2);
            var v1 = (sprite: vt0.Item1, pos: vt1.Item2);

            if (v0.pos.Position.X + v0.sprite.Texture2D.Width < v1.pos.Position.X)
                return false;
            if (v1.pos.Position.X + v1.sprite.Texture2D.Width < v0.pos.Position.X)
                return false;
            if (v0.pos.Position.Y + v0.sprite.Texture2D.Height < v1.pos.Position.Y)
                return false;
            if (v1.pos.Position.Y + v1.sprite.Texture2D.Height < v0.pos.Position.Y)
                return false;

            return true;
        }
    }
}