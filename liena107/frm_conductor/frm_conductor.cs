using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace liena107.frm_conductor
{
    public partial class frm_conductor : Form
    {
        private bool ExisteDispositivo = false;
        private FilterInfoCollection DispositivoDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;
  
        public frm_conductor()
        {
            InitializeComponent();
            BuscarDispositivos();
        }
        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            for (int i = 0; i < Dispositivos.Count; i++) ;

            cb_camara.Items.Add(Dispositivos[0].Name.ToString());
            cb_camara.Text = cb_camara.Items[0].ToString();

        }
        public void BuscarDispositivos()
        {
            DispositivoDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DispositivoDeVideo.Count == 0)
            {
                ExisteDispositivo = false;
            }
            else
            {
                ExisteDispositivo = true;
                CargarDispositivos(DispositivoDeVideo);
            }

        }
        public void TerminarFuenteDeVideo()
        {
            if (!(FuenteDeVideo == null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }


        }
        public void Video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pb_foto.Image = Imagen;

        }

        private void Btn_registrar_Click(object sender, EventArgs e)
        {
            int ci = int.Parse(txt_ci.Text);
            string nombre = txt_nombre.Text;
            string apellidos = txt_apellidos.Text;
            string interno = cb_interno.Text;
           string placa = cb_placa.Text;
            string foto = txt_ruta.Text;

            c_registro c = new c_registro(0,ci,nombre,apellidos,interno,placa,foto);
            c.registrar();
            MessageBox.Show("CONDUCTOR REGISTRADO");
            cargar_controles();
            limpiar_controles();
            
            
            
        }
        public void cargar()
        {
            


        }
        public void cargar_controles()
        {
            conexion.conexion c = new conexion.conexion();
            c.cargar_datagrid(dg_conductor, "select * from conductor");
            c.cargar_combox(cb_interno,"select * from interno","interno","interno");
            c.cargar_combox(cb_placa, "select * from auto","placa","placa");

        }
        public void limpiar_controles()
        {
            txt_ci.Clear();
            txt_nombre.Clear();
            txt_numero.Clear();
            txt_apellidos.Clear();
            pb_foto.Refresh();
            cb_placa.Refresh();
            cb_interno.Refresh();



        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            cargar_controles();
        }

        private void Btn_iniciar_Click(object sender, EventArgs e)
        {
             if (btn_iniciar.Text == "INICIAR")

            {
                if (ExisteDispositivo)
                {
                    FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[cb_camara.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                    FuenteDeVideo.Start();
                    
                    btn_iniciar.Text = "Capturar";
                   cb_camara.Enabled = false;
                }
                else
                {
                    Estado.Text = "No Hay Camaras Conectadas";
                    pb_foto.Image = liena107.Properties.Resources.ImgDefecto;

                }
            }
            else
            {
                if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();
                   
                    btn_iniciar.Text = "INICIAR";
                   cb_camara.Enabled = true;
                }
            }


        }

        SqlCommand cmd;
        MemoryStream ms;
        void conv_photo()
        {
            if (pb_foto.Image != null)
            {
                ms = new MemoryStream();
                pb_foto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                cmd.Parameters.AddWithValue("@foto", photo_aray);
            }
        }


    }
}
