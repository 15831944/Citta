﻿using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class MoveWrapper
    {
        private static LogUtil log = LogUtil.GetInstance("MoveWrapper");
        private int width;
        private int height;
        private Point start, now;
        private bool startDrag;

        private int worldWidth;
        private int worldHeight;
        private Bitmap staticImage;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Factor { get; set; }
        public bool StartDrag { get => startDrag; set => startDrag = value; }
        public int WorldWidth { get => worldWidth; set => worldWidth = value; }
        public int WorldHeight { get => worldHeight; set => worldHeight = value; }
        public Point Start { get => start; set => start = value; }
        public Point Now { get => now; set => now = value; }
        public Bitmap StaticImage { get => staticImage; set => staticImage = value; }

        public MoveWrapper()
        {
            this.worldWidth = 2000;
            this.worldHeight = 1000;
            this.startDrag = false;
        }
        public Bitmap CreateWorldImage()
        {
            Bitmap staticImage = new Bitmap(Convert.ToInt32(this.WorldWidth * Factor), Convert.ToInt32(this.WorldHeight * Factor));
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            g.Dispose();
            return staticImage;
        }

        public void MoveWorldImage(Graphics n)
        {
            // 每次Move都需要画一张新图
            if (this.StaticImage != null)
            {
                this.StaticImage.Dispose();
                this.StaticImage = null;
            }
            this.StaticImage = this.CreateWorldImage();
            ModelDocument currentDoc = Global.GetCurrentDocument();
            Point mapOrigin = currentDoc.WorldMap.MapOrigin;
            float factor = currentDoc.WorldMap.ScreenFactor;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * factor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * factor);
            Graphics g = Graphics.FromImage(StaticImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (ModelRelation mr in currentDoc.ModelRelations)
            {
                PointF s = currentDoc.WorldMap.ScreenToWorldF(mr.StartP, false);
                PointF a = currentDoc.WorldMap.ScreenToWorldF(mr.A, false);
                PointF b = currentDoc.WorldMap.ScreenToWorldF(mr.B, false);
                PointF e = currentDoc.WorldMap.ScreenToWorldF(mr.EndP, false);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            g.Dispose();
            n.DrawImageUnscaled(StaticImage, mapOrigin);
            this.StaticImage.Dispose();
            this.StaticImage = null;
            this.RepaintCtrs();
        }

        public void InitDragWrapper(Size canvasSize, float canvasFactor)
        {
            width = canvasSize.Width;
            height = canvasSize.Height;
            Factor = Global.GetCurrentDocument().WorldMap.ScreenFactor;
        }
        public void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            this.Now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.MoveWorldImage(n);
            Global.GetCanvasPanel().Invalidate();
            n.Dispose();
            this.StartDrag = false;
            this.Start = e.Location;
        }
        public void DragDown(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            this.startDrag = true;
            this.start = e.Location;
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.staticImage = this.CreateWorldImage();
        }
        public void DragMove(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            this.now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();

            this.MoveWorldImage(n);
            n.Dispose();
        }
        /// <summary>
        ///  重绘碰到的控件
        /// </summary>
        private void RepaintCtrs()
        {
            CanvasPanel cp = Global.GetCanvasPanel();
            List<ModelElement> md = Global.GetCurrentDocument().ModelElements;

            foreach (ModelElement me in md)
            {
                Control ctr = me.InnerControl;
                Rectangle ctrRect = new Rectangle(me.Location, ctr.Size);
                cp.Invalidate(ctrRect);
                cp.Update();
            }
        }
    }
}