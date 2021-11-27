//------------------------------------------------------------------
// (c) Copywrite Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------

// base class for 3D chart
// version 0.1

using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WPFChart3D
{
    class Chart3D
    {
        public Chart3D()
        {
        }

        public static int SHAPE_NO = 5;

        public WPFChart3D.Vertex3D this[int n]
        {
            get
            {
                return (Vertex3D)m_vertices[n];
            }
            set
            {
                m_vertices[n] = value;
            }
        }

        public float XCenter()
        {
            return (m_xMin + m_xMax) / 2;
        }

        public float YCenter()
        {
            return (m_yMin + m_yMax) / 2;
        }

        public float XRange()
        {
            return m_xMax - m_xMin;
        }
        public float YRange()
        {
            return m_yMax - m_yMin;
        }
        public float ZRange()
        {
            return m_zMax - m_zMin;
        }
        public float XMin()
        {
            return m_xMin;
        }

        public float XMax()
        {
            return m_xMax;
        }
        public float YMin()
        {
            return m_yMin;
        }

        public float YMax()
        {
            return m_yMax;
        }
        public float ZMin()
        {
            return m_zMin;
        }

        public float ZMax()
        {
            return m_zMax;
        }

        public int GetDataNo()
        {
            return m_vertices.Length;
        }

        public void SetDataNo(int nSize)
        {
            m_vertices = new Vertex3D[nSize];
        }

        public void GetDataRange()
        {
            int nDataNo = GetDataNo();
            if (nDataNo == 0) return;
            m_xMin = Single.MaxValue;
            m_yMin = Single.MaxValue;
            m_zMin = Single.MaxValue;
            m_xMax = Single.MinValue;
            m_yMax = Single.MinValue;
            m_zMax = Single.MinValue;
            for (int i = 0; i <nDataNo; i++)
            {
                float xV = this[i].x;
                float yV = this[i].y;
                float zV = this[i].z;
                if (m_xMin > xV) m_xMin = xV;
                if (m_yMin > yV) m_yMin = yV;
                if (m_zMin > zV) m_zMin = zV;
                if (m_xMax < xV) m_xMax = xV;
                if (m_yMax < yV) m_yMax = yV;
                if (m_zMax < zV) m_zMax = zV;
            }
        }

       
        // select 
        public virtual void Select(ViewportRect rect, TransformMatrix matrix, Viewport3D viewport3d)
        {
        }

        // highlight selected model
        public virtual void HighlightSelection(System.Windows.Media.Media3D.MeshGeometry3D meshGeometry, System.Windows.Media.Color selectColor)
        {
        }

        public enum SHAPE { BAR, ELLIPSE, CYLINDER, CONE, PYRAMID };    // shape of the 3d dot in the plot

        protected Vertex3D [] m_vertices;                               // 3d plot data
        protected float m_xMin, m_xMax, m_yMin, m_yMax, m_zMin, m_zMax; // data range

        
       

    }
}
