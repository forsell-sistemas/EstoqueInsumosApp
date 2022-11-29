using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace EstoqueInsumosApp.Model
{
    class Connection
    {
        public static NpgsqlConnection Conn()
        {
            string connectionString = string.Empty;
            connectionString += "Server=dbforsellcloud.cud8okh16ujh.sa-east-1.rds.amazonaws.com;";
            connectionString += "Port=5432;";
            connectionString += "User Id=postgres;";
            connectionString += "Password=F0r53!!S1sT3m45;";
            connectionString += "Database=MADEIRA_TESTE_APP; CommandTimeout=0;";
            connectionString += "SSL Mode=Require;Trust Server Certificate=true";
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            return conn;

        }

        //public static NpgsqlConnection Conn()
        //{
        //    string connectionString = string.Empty;
        //    connectionString += "Server=192.168.3.2;";
        //    connectionString += "Port=5432;";
        //    connectionString += "User Id=postgres;";
        //    connectionString += "Password=inbr*123;";
        //    connectionString += "Database=Dados2; CommandTimeout=0;";
        //    NpgsqlConnection conn = new NpgsqlConnection(connectionString);
        //    return conn;

        //}
    }
}
