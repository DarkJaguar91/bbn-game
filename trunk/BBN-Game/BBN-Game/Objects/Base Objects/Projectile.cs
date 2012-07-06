﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BBN_Game.Objects
{
    class Projectile : DynamicObject
    {
        #region "Globals"
        protected float lifeSpan; // how long the bullet lasts

        public Boolean destroy // does the object need to be destroyed
        {
            get;
            set;
        }
        #endregion

        #region "Constructors"
        protected override void setData()
        {
            this.rollSpeed = 10;
            this.yawSpeed = 2.5f;
            this.pitchSpeed = 2.5f;
            this.maxSpeed = 58;
            this.minSpeed = 0;
            this.mass = 0;
            this.greatestLength = 2f;
            this.shipData.scale = 0.1f;
            this.lifeSpan = 15;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game class</param>
        /// <param name="parent">The parent of the bullet</param>
        public Projectile(Game game, StaticObject parent)
            : base(game, Objects.Team.nutral, parent.Position + Vector3.Transform(new Vector3(0, -parent.getGreatestLength / 4, parent.getGreatestLength / 4), Matrix.CreateFromQuaternion(parent.rotation)))
        {
            this.rotation = parent.rotation;

            numHudLines = 3;
            typeOfLine = PrimitiveType.LineStrip;

            if (Matrix.CreateFromQuaternion(rotation).Up.Y < 0)
            {
                Matrix a = Matrix.CreateFromQuaternion(rotation);

                Quaternion rot = Quaternion.CreateFromAxisAngle(a.Backward, MathHelper.ToRadians(90));

                rotation = rot * rotation;
            }

            this.destroy = false;

            this.shipData.speed = parent.ShipMovementInfo.speed;
        }
        #endregion

        #region "Update"
        public override void Update(GameTime gt)
        {

            this.lifeSpan -= 1 * (float)gt.ElapsedGameTime.TotalSeconds;

            if (lifeSpan <= 0)
                this.destroy = true;

            if (this.destroy)
                Controller.GameController.removeObject(this);

            base.Update(gt);
        }

        protected override void resetModels()
        {
            base.resetModels();
        }        

        public override void controller(GameTime gt)
        {

            base.controller(gt);
        }

        protected override void setVertexPosition(float screenX, float screenY, float radiusOfObject, Color col)
        {
            //Line 1
            targetBoxVertices[0].Position.X = screenX;
            targetBoxVertices[0].Position.Y = screenY + radiusOfObject * 1.8f;
            targetBoxVertices[0].Color = col;

        //    Line 2
            targetBoxVertices[1].Position.X = screenX - radiusOfObject * 1.8f;
            targetBoxVertices[1].Position.Y = screenY - radiusOfObject;
            targetBoxVertices[1].Color = col;

        //    Line 3
            targetBoxVertices[2].Position.X = screenX + radiusOfObject * 1.8f;
            targetBoxVertices[2].Position.Y = screenY - radiusOfObject;
            targetBoxVertices[2].Color = col;

        //    Line 4
            targetBoxVertices[3].Position.X = screenX;
            targetBoxVertices[3].Position.Y = screenY + radiusOfObject * 1.8f;
            targetBoxVertices[3].Color = col;
        }
        #endregion
    }
}