﻿using OpenTK.Graphics.OpenGL;
using Commons;
using System.IO;
using System.Collections.Generic;
using System;
using OpenTK;
using System.Diagnostics;

namespace MainForm.Models {
    /// <summary>
    /// 绘制空间点框架
    /// </summary>
    public class Frame {
        clsMaterials globalMaterial;
        List<Tuple<Vector3, Vector3>> nets;

        /// <summary>
        /// 最终显示结果
        /// </summary>
        public void Draw() {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            globalMaterial.SetMaterial(MaterialType.ActiveBox);
            for (int i = 0; i < nets.Count; i++) {
                Vector3 ori = nets[i].Item1 * 100,
                    tar = nets[i].Item2 * 100;
                DrawPoint(ori);
                DrawOneLine(ori, tar);
                DrawPoint(tar);
                Debug.WriteLine(ori.X.ToString() + "," + ori.Y.ToString() + "," +
                    ori.Z.ToString() + " " + tar.X.ToString() + "," + tar.Y.ToString() +
                    "," + tar.Z.ToString());

            }
        }

        /// <summary>
        /// 画一条直线
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DrawOneLine(Vector3 a, Vector3 b) {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(a);
            GL.Vertex3(b);
            GL.End();
        }

        /// <summary>
        /// 画一个点
        /// </summary>
        /// <param name="p"></param>
        public void DrawPoint(Vector3 p) {
            IntPtr pObj = OpenTK.Graphics.Glu.NewQuadric();
            double _SphereRadius = 1;
            GL.Flush();
            GL.PushMatrix();
            GL.Translate(p.X, p.Y, p.Z);
            OpenTK.Graphics.Glu.Sphere(pObj, _SphereRadius, 10, 10);
            GL.PopMatrix();
            pObj = IntPtr.Zero;

        }

        /// <summary>
        /// 读取DAT文件
        /// </summary>
        /// <param name="path"></param>
        public void GetDatFile(string path) {
            StreamReader streamReader = new StreamReader(path);
            var wholeData = streamReader.ReadToEnd().Split('\n');
            foreach (var point in wholeData) {
                if (point == string.Empty)
                    break;
                var pointSet = point.Split(' ');
                var axis_1 = pointSet[0].Split(',');
                var axis_2 = pointSet[1].Split(',');

                Vector3 o = new Vector3(float.Parse(axis_1[0]),
                    float.Parse(axis_1[1]), float.Parse(axis_1[2])),
                    t = new Vector3(float.Parse(axis_2[0]), float.Parse(axis_2[1]),
                    float.Parse(axis_2[2]));

                nets.Add(new Tuple<Vector3, Vector3>(o, t));
            }
        }

        public Frame(clsMaterials material) {
            nets = new List<Tuple<Vector3, Vector3>>();
            globalMaterial = material;
            globalMaterial = new clsMaterials();
        }

        public Frame() {
            nets = new List<Tuple<Vector3, Vector3>>();
            globalMaterial = new clsMaterials();
        }
    }
}
