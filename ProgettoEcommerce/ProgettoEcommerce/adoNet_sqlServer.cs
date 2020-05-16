using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

// direttive per il web
using System.Web;


namespace adoNetWebSQlServer //Se non metto il namespace della soluzione devo includere la classe come Using
{
    public class adoNet : IDisposable
    {
        private static string cnString;
        private SqlConnection cn;
        private SqlDataReader reader;

        public SqlCommand cmd;
        public SqlDataAdapter adp; //consente di caricare i dati su dataset

        public static void impostaConnessione(string dbName)
        {
            string abs = HttpContext.Current.Server.MapPath(dbName);
            cnString = @"Server=(localdb)\MSSQLLocalDB;attachdbfilename= " + abs + @";Integrated Security=True";                  // 2012
        }

        public adoNet()
        {
            init();
        }

        public adoNet(string dbName)
        {
            impostaConnessione(dbName);
            init();
        }

        //Inizializzazione oggetti
        private void init()
        {
            if (cnString != "")
            {
                cn = new SqlConnection();
                cn.ConnectionString = cnString;
                cmd = new SqlCommand();
                cmd.Connection = cn;
                adp = new SqlDataAdapter(cmd);
                adp.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            }
            else
                throw new Exception("Connection String non inizializzata!");
        }

        public void apriConnessione()
        {
            if (cn != null && cn.ConnectionString != "" & cn.State == ConnectionState.Closed)
                cn.Open();
        }

        public void chiudiConnessione()
        {
            if (cn != null && cn.ConnectionString != "" && cn.State == ConnectionState.Open)
                cn.Close();
        }

        public DataTable eseguiQuery(string sql, CommandType tipo)
        {
            DataTable daTable = new DataTable();
            cmd.CommandText = sql;
            cmd.CommandType = tipo;
            if (daTable.IsInitialized) daTable.Clear();
            //apriConnessione()
            adp.Fill(daTable);
            // chiudiConnessione()
            this.cmd.Parameters.Clear();
            return daTable;
        }

        public int eseguiNonQuery(string sql, CommandType tipo)
        {
            int ris = -1;
            cmd.CommandText = sql;
            cmd.CommandType = tipo;
            apriConnessione();
            ris = cmd.ExecuteNonQuery();
            chiudiConnessione();
            this.cmd.Parameters.Clear();
            return ris;
        }

        public string eseguiScalar(string sql, CommandType tipo)
        {
            Object obj;
            string ris = string.Empty;
            cmd.CommandText = sql;
            cmd.CommandType = tipo;
            apriConnessione();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                ris = obj.ToString();
            chiudiConnessione();
            this.cmd.Parameters.Clear();
            return ris;
        }

        //Rimane aperto: mantiene lo stato attivo, consuma più risorse, si usa solo in casi particolari
        public SqlDataReader creaLettore(string sql, CommandType tipo)
        {
            cmd.CommandText = sql;
            cmd.CommandType = tipo;
            apriConnessione();
            reader = cmd.ExecuteReader();
            return reader;
        }

        public void chiudiLettore()
        {
            chiudiConnessione();
            reader.Dispose();
        }

        public void Dispose()
        {
            if (adp != null) adp.Dispose();
            if (cmd != null) cmd.Dispose();
            if (cn != null) cn.Dispose();
        }

        public void transazioneOrdine(string sqlQuery1, string sqlQuery2, string sqlQuery3, string sqlQuery4, CommandType tipo)
        {
            SqlTransaction transazione = null;
            try
            {
                apriConnessione();
                transazione = cn.BeginTransaction();
                cmd.CommandText = sqlQuery1;
                cmd.CommandType = tipo;
                cmd.Transaction = transazione;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlQuery2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlQuery4;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlQuery3;
                cmd.ExecuteNonQuery();
                transazione.Commit();
            }
            catch (Exception ex)
            {
                transazione.Rollback();
                throw new Exception("Ordine fallito");
            }
        }

        public void transazioneSpedizione(string sqlQuery1, string sqlQuery2, CommandType tipo)
        {
            SqlTransaction transazione = null;
            try
            {
                apriConnessione();
                transazione = cn.BeginTransaction();
                cmd.CommandText = sqlQuery1;
                cmd.CommandType = tipo;
                cmd.Transaction = transazione;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlQuery2;
                cmd.ExecuteNonQuery();
                transazione.Commit();
            }
            catch (Exception ex)
            {
                transazione.Rollback();
                throw new Exception("Spedizione fallita");
            }
        }
    }
}
