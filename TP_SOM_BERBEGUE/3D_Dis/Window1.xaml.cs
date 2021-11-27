//------------------------------------------------------------------
// (c) Copywrite Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------

// window class for testing 3d chart using WPF
// version 0.1

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System;
using System.Collections;
using System.Windows.Forms;

namespace WPFChart3D
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {    
        // transform class object for rotate the 3d model
        public WPFChart3D.TransformMatrix m_transformMatrix = new WPFChart3D.TransformMatrix();

        // ***************************** 3d chart ***************************
        private WPFChart3D.Chart3D m_3dChart;       // data for 3d chart
        public int m_nChartModelIndex = -1;         // model index in the Viewport3d
  

        // ***************************** selection rect ***************************
        ViewportRect m_selectRect = new ViewportRect();
        public int m_nRectModelIndex = -1;

        int[] values;

        int kl=0;
        public Window1(int[] v)
        {
            InitializeComponent();
           // selection rect
            m_selectRect.SetRect(new Point(-0.5, -0.5), new Point(-0.5, -0.5));
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            ArrayList meshs = m_selectRect.GetMeshes();
            m_nRectModelIndex = model3d.UpdateModel(meshs, null, m_nRectModelIndex, this.mainViewport);
           
           
           this.b1.Click += new RoutedEventHandler(b1_Click);
           this.b2.Click += new RoutedEventHandler(b2_Click);

           values = (int[])v.Clone();
           int o =(int) Math.Sqrt(v.Length);
           kl = 2 * o + 3; ;
           
           TestSurfacePlot(kl);
        }

        void b1_Click(object sender, RoutedEventArgs e)
        {
            m_transformMatrix.OnKeyDown(2);
            TransformChart();
        }

        void b2_Click(object sender, RoutedEventArgs e)
        {
            m_transformMatrix.OnKeyDown(1);
            TransformChart();
        }


     

        // function for testing surface chart
        public void TestSurfacePlot(int nGridNo)
        {
            int nXNo =  nGridNo;
            int nYNo =  nGridNo;
            // 1. set the surface grid
            m_3dChart = new UniformSurfaceChart3D();
            ((UniformSurfaceChart3D)m_3dChart).SetGrid(nXNo, nYNo, -100, 100, -100, 100);

            // 2. set surface chart z value
            double xC =  m_3dChart.XCenter();
            double yC =  m_3dChart.YCenter();
            int nVertNo = m_3dChart.GetDataNo();

            double xC1 = m_3dChart.XCenter()+10;
            double yC1 = m_3dChart.YCenter()+10;


            int k = 0;
            for (int i = 0; i < nVertNo; i++)
            {
                m_3dChart[i].z = 1;

            }

            for (int i = 2; i <= (kl-3); i += 2)
            {
                for (int j = 2; j <= (kl-3); j = j + 2)
                {
                    int y = kl * i + j;
                    m_3dChart[y].z = values[k];
                    k++;
                }
            }
            m_3dChart.GetDataRange();
           
               
            // 3. set the surface chart color according to z vaule
            double zMin = m_3dChart.ZMin();
            double zMax = m_3dChart.ZMax();
            for (int i = 0; i < nVertNo; i++)
            {
                Vertex3D vert = m_3dChart[i];
                double h = (vert.z - zMin) / (zMax - zMin);

                Color color = WPFChart3D.TextureMapping.PseudoColor(h);
                m_3dChart[i].color = color;
            }

            // 4. Get the Mesh3D array from surface chart
            ArrayList meshs = ((UniformSurfaceChart3D)m_3dChart).GetMeshes();

            // 5. display vertex no and triangle no of this surface chart
            UpdateModelSizeInfo(meshs);
            
            // 6. Set the model display of surface chart
            WPFChart3D.Model3D model3d = new WPFChart3D.Model3D();
            Material backMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
            m_nChartModelIndex = model3d.UpdateModel(meshs, backMaterial, m_nChartModelIndex, this.mainViewport);

            // 7. set projection matrix, so the data is in the display region
            float xMin = m_3dChart.XMin();
            float xMax = m_3dChart.XMax();
            m_transformMatrix.CalculateProjectionMatrix(xMin, xMax, xMin, xMax, zMin, zMax, 0.5);
            TransformChart();
        }

       

       

        // zoom in 3d display
       public void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs args)
        {
            m_transformMatrix.OnKeyDown(0);
            TransformChart();
        }
        public void OnViewportMouseMove(object sender, System.Windows.Input.MouseEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);

            if (args.LeftButton == MouseButtonState.Pressed)                // rotate or drag 3d model
            {
                m_transformMatrix.OnMouseMove(pt, mainViewport);

                TransformChart();
            }
            else if (args.RightButton == MouseButtonState.Pressed)          // select rect
            {
                m_selectRect.OnMouseMove(pt, mainViewport, m_nRectModelIndex);
            }
            else
            {
                /*
                String s1;
                Point pt2 = m_transformMatrix.VertexToScreenPt(new Point3D(0.5, 0.5, 0.3), mainViewport);
                s1 = string.Format("Screen:({0:d},{1:d}), Predicated: ({2:d}, H:{3:d})", 
                    (int)pt.X, (int)pt.Y, (int)pt2.X, (int)pt2.Y);
                this.statusPane.Text = s1;
                */
            }
        }
        public void OnViewportMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);
            if (args.ChangedButton == MouseButton.Left)         // rotate or drag 3d model
            {
                m_transformMatrix.OnLBtnDown(pt);
            }
            else if (args.ChangedButton == MouseButton.Right)   // select rect
            {
                m_selectRect.OnMouseDown(pt, mainViewport, m_nRectModelIndex);
            }
        }

        public void OnViewportMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point pt = args.GetPosition(mainViewport);
            if (args.ChangedButton == MouseButton.Left)
            {
                m_transformMatrix.OnLBtnUp();
            }
            else if (args.ChangedButton == MouseButton.Right)
            {
                if (m_nChartModelIndex == -1) return;
                // 1. get the mesh structure related to the selection rect
                MeshGeometry3D meshGeometry = Model3D.GetGeometry(mainViewport, m_nChartModelIndex);
                if (meshGeometry == null) return;
              
                // 2. set selection in 3d chart
                m_3dChart.Select(m_selectRect, m_transformMatrix, mainViewport);

                // 3. update selection display
                m_3dChart.HighlightSelection(meshGeometry, Color.FromRgb(200, 200, 200));
            }
        }

      

       

       

         private void UpdateModelSizeInfo(ArrayList meshs)
        {
            int nMeshNo = meshs.Count;
            int nChartVertNo = 0;
            int nChartTriangelNo = 0;
            for (int i = 0; i < nMeshNo; i++)
            {
                nChartVertNo += ((Mesh3D)meshs[i]).GetVertexNo();
                nChartTriangelNo += ((Mesh3D)meshs[i]).GetTriangleNo();
            }
          
        }

        // this function is used to rotate, drag and zoom the 3d chart
        private void TransformChart()
        {
            if (m_nChartModelIndex == -1) return;
            ModelVisual3D visual3d = (ModelVisual3D)(this.mainViewport.Children[m_nChartModelIndex]);
            if (visual3d.Content == null) return;
            Transform3DGroup group1 = visual3d.Content.Transform as Transform3DGroup;
            group1.Children.Clear();
            group1.Children.Add(new MatrixTransform3D(m_transformMatrix.m_totalMatrix));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
