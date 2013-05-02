using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GDD2
{
    public class Camera
    {
        protected float zoom; // Camera Zoom
        private Matrix   transform; // Matrix Transform
        private Vector2  pos; // Camera Position
        protected float rotation; // Camera Rotation
        private Vector2 moved;
        private float minX;
        private float maxX;

 
        public Camera(float minX, float maxX)
        {
            this.minX = minX;
            this.maxX = maxX;
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
            moved = Vector2.Zero;
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
        }
 
        //Get set rotation
        public float Rotation
        {
            get {return rotation; }
            set { rotation = value; }
        }
 
        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            pos += amount;
            moved += amount;
        }

       // Get set position
        public Vector2 Pos
        {
             get{ return  pos; }
             set
             {
                 if (value.X < minX)
                     pos = new Vector2(minX, value.Y);
                 else if (value.X > maxX)
                     pos = new Vector2(maxX, value.Y);
                 else
                     pos = value;
             }
        }

        //Get set moved
        public Vector2 Moved
        {
            get { return moved; }
            set { moved = value; }
        }

        //Return the transformation matrix needed to display the camera's viewport
        public Matrix get_transformation(GraphicsDevice graphics)
        {
            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                                 Matrix.CreateRotationZ(Rotation) *
                                                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                 Matrix.CreateTranslation(new Vector3(graphics.Viewport.Width * 0.5f, graphics.Viewport.Height * 0.5f, 0));
            return transform;
        }
    }
}
