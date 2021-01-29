using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.Events;
using System.IO;
using System;

public class DBMngr
{
    public static readonly string path = Application.dataPath + "/Game/Data/data.db";
    public static readonly string uri = "URI=file:" + path;

    public static void StartTable(string table, string[] rows)
    {
        string rowsStr = rows[0];

        for (int i = 1; i < rows.Length; i++)
        {
            rowsStr += "," + rows[i];
        }

        string query = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1}) ", table, rowsStr);

        Execute(query, (cmd, conn) =>
        {
            try
            {
                cmd.ExecuteReader();
                conn.Close();
            }
            catch { conn.Close(); }
        });
    }


    public static void Read(string query, UnityAction<IDataReader> rowCallback, UnityAction finishCallback)
    {
        Execute(query, (cmd, conn) =>
        {
            using (IDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    while (reader.Read())
                    {
                        rowCallback?.Invoke(reader);
                    }
                    reader.Close();
                    conn.Close();
                    finishCallback?.Invoke();
                }
                catch 
                {
                    reader.Close();
                    conn.Close();
                }
            }
        });
    }

    public static void Write(string query, UnityAction callback)
    {
        Execute(query, (cmd, conn) =>
        {
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                callback?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                conn.Close();
                callback?.Invoke();
            }
        });
    }

    public static void Execute(string query, UnityAction<IDbCommand, IDbConnection> callback)
    {
        if (!File.Exists(path))
        {
            SqliteConnection.CreateFile(path);
        }

        using (IDbConnection conn = new SqliteConnection(uri))
        {
            conn.Open();
            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                callback?.Invoke(cmd, conn);
            }
        }

    }
}
