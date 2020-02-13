using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Juego_OCA
{
    public class manejarBD
    {
        /*public string conexion = @"Provider = Microsoft.ACE.OLEDB.4.0; Data Source = ..\..\BD\bdOCA.accdb";
        public string SQL;
        
        OleDbConnection con = new OleDbConnection(conexion);
        OleDbCommand com = new OleDbCommand(SQL, con);*/
        string conexion = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ..\..\BD\bdOCA.accdb";
        string SQL;
        OleDbConnection conectar;
        OleDbCommand command;
        

        List<string> listaPreguntas = new List<string>();
        List<string> listaRespuestas = new List<string>();
        List<int> listaRespCorrectas = new List<int>();

        string pregunta;
        string resp1, resp2, resp3;
        int respCorrecta;

        public List<string> cargarPreguntas()
        {
            SQL = "select Enunciado from tbPreguntas";
            conectar = new OleDbConnection(conexion);
            conectar.Open();
            command = new OleDbCommand(SQL, conectar);
            OleDbDataReader leer = command.ExecuteReader();
            while (leer.Read())
            {
                pregunta = leer[0].ToString();
                listaPreguntas.Add(pregunta + "\n");
            }
            leer.Close();
            conectar.Close();
            return listaPreguntas;
        }

        public List<string> cargarRespuestas()
        {
            SQL = "SELECT Respuesta1, Respuesta2, Respuesta3 FROM tbPreguntas";
            conectar = new OleDbConnection(conexion);
            conectar.Open();
            command = new OleDbCommand(SQL, conectar);
            OleDbDataReader leer = command.ExecuteReader();
            while (leer.Read())
            {
                resp1 = leer[0].ToString();
                resp2 = leer[1].ToString();
                resp3 = leer[2].ToString();
                //listaRespuestas.Add("\n 1) " + resp1 + "\n 2) " + resp2 + "\n 3) " + resp3);
                listaRespuestas.Add(string.Format("{0}1) {1} {0}2) {2} {0}3) {3} ", Environment.NewLine, resp1, resp2, resp3));
            }
            leer.Close();
            conectar.Close();
            return listaRespuestas;
        }

        public List<int> cargarRespCorrectas()
        {
            SQL = "select RespCorrecta from tbPreguntas";
            conectar = new OleDbConnection(conexion);
            conectar.Open();
            command = new OleDbCommand(SQL, conectar);
            OleDbDataReader leer = command.ExecuteReader();
            while (leer.Read())
            {
                respCorrecta = int.Parse(leer[0].ToString());
                listaRespCorrectas.Add(respCorrecta);
            }
            leer.Close();
            conectar.Close();
            return listaRespCorrectas;
        }
    }
}
