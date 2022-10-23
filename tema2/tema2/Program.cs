using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace tema2
{
    class SimpleWindow3D : GameWindow
    {

        const float rotation_speed = 180.0f;
        float angle;
        bool showPiramide = true;
        KeyboardState lastKeyPress;
        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }
  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.MediumPurple);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0 ,0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 2, 68);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();

			if (keyboard[OpenTK.Input.Key.Escape])
			{
				Exit();
				return;
			}
			else if (keyboard[OpenTK.Input.Key.A] && keyboard[OpenTK.Input.Key.B] && !keyboard.Equals(lastKeyPress))
			{
				// Ascundere comandata prin apasarea tastelor A si B
				if (showPiramide == true)
				{
					showPiramide = false;
				}
				else
				{
					showPiramide = true;
				}
			}
			lastKeyPress = keyboard;

		}
		protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 20, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            angle += rotation_speed * (float)e.Time;
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);
            if (showPiramide == true)
            {
                DrawPiramide();
          
            }

            SwapBuffers();
            
        }

       
        private void DrawPiramide()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Pink);
            GL.Vertex3(0.0f, 5.0f, 0.0f);
            GL.Vertex3(-5.0f, -5.0f, 5.0f);
            GL.Vertex3(5.0f, -5.0f, 5.0f);

           
            GL.Vertex3(0.0f, 5.0f, 0.0f);
            GL.Vertex3(5.0f, -5.0f, -5.0f);
            GL.Vertex3(-5.0f, -5.0f, -5.0f);


            GL.Color3(Color.Honeydew);

            GL.Vertex3(0.0f, 5.0f, 0.0f);
            GL.Vertex3(5.0f, -5.0f, 5.0f);
            GL.Vertex3(5.0f, -5.0f, -5.0f);

            GL.Color3(Color.Pink);
            GL.Vertex3(0.0f, 5.0f, 0.0f);
            GL.Vertex3(-5.0f, -5.0f, -5.0f);
            GL.Vertex3(-5.0f, -5.0f, 5.0f);

            GL.End();
        }
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

}
