using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// These are my modifications to a freely available Camera (http://community.monogame.net/t/simple-2d-camera/9135)
/// In this project this is the only third party code.
/// </summary>
namespace ECSAttempt
{
    public class Camera2D
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }

        private float currentMouseWheelValue;
        float previousMouseWheelValue;
        float zoom;
        float previousZoom;

        public Camera2D(Viewport viewport) {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = Vector2.Zero;
        }

        private void UpdateVisibleArea() {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix() {
            // invert -Position.Y to Position.Y so that Y positive is up and Y negative is down.
            // Remember when drawing sprites to make their Y position negative.
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, Position.Y, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition) {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount) {
            Zoom += zoomAmount;
            if (Zoom < .35f) {
                Zoom = .35f;
            }
            if (Zoom > 2f) {
                Zoom = 2f;
            }
        }

        public void Update(Viewport bounds, GameTime gameTime) {
            Bounds = bounds.Bounds;
            UpdateMatrix();

            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;

            if (Zoom > .8f) {
                moveSpeed = 150;
            }
            else if (Zoom < .8f && Zoom >= .6f) {
                moveSpeed = 20;
            }
            else if (Zoom < .6f && Zoom > .35f) {
                moveSpeed = 25;
            }
            else if (Zoom <= .35f) {
                moveSpeed = 30;
            }
            else {
                moveSpeed = 10;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                cameraMovement.Y = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                cameraMovement.Y = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                cameraMovement.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                cameraMovement.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            // temp disabled
            if (currentMouseWheelValue > previousMouseWheelValue) {
                //AdjustZoom(.05f);
            }

            if (currentMouseWheelValue < previousMouseWheelValue) {
                //AdjustZoom(-.05f);
            }

            previousZoom = zoom;
            zoom = Zoom;
            if (previousZoom != zoom) {

            }

            cameraMovement.X = (float)Math.Round(cameraMovement.X);
            cameraMovement.Y = (float)Math.Round(cameraMovement.Y);
            MoveCamera(cameraMovement);
        }
    }
}
