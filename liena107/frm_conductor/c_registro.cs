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
    class c_registro
    {
        int numero;
        int ci;
        string nombre;
        string apellidos;
        string interno;
        string placa;
        string foto;

        public int Numero { get => numero; set => numero = value; }
        public int Ci { get => ci; set => ci = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Interno { get => interno; set => interno = value; }
        public string Placa { get => placa; set => placa = value; }
        public string Foto { get => foto; set => foto = value; }

        public c_registro() { }
        public c_registro(int n,int c,string nom, string ape,string inte,string pl,string fo)
        {
            this.numero = n;
            this.ci = c;
            this.nombre = nom;
            this.apellidos = ape;
            this.interno = inte;
            this.placa = pl;
            this.foto = fo;

        }
        public void registrar()
        {
            string sql = @"insert into conductor(ci,nombre,apellidos,interno,placa,foto)
                            values
                                 ('{0}','{1}','{2}','{3}','{4}','{5}')";
            sql = string.Format(sql,nombre,apellidos,interno,placa,foto);


        }

       


    }
    
}
