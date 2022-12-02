/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class GameDataBase : MonoBehaviour
{

    public string urlDataBase, urlDataBase2;
    public string sql, sql2;
    public Text texto1, texto2;

    public List<string> Lnumeros = new List<string>();
    public List<string> Ltextos = new List<string>();

    public int str, hp;

    #region - EXAMPLE -
    /*
     EXAMPLE
         public string PatchToYourDB, TableName;

    void Start()
    {
        string conn = "URI=file:" + Application.dataPath + PatchToYourDB; // localização do seu .db é igual a pasta da aplicação + patch
        IDbConnection dbconn; // declarando a conexao
        dbconn = (IDbConnection)new SqliteConnection(conn); // setando ela como nova conexao com nosso .db
        dbconn.Open(); //Abrindo conexão com seu banco de dados.
        IDbCommand dbcmd = dbconn.CreateCommand(); // declarando o executador de comandos
        string sqlQuery = "SELECT * from " + TableName; // criando uma query
        dbcmd.CommandText = sqlQuery; // executando a query
        IDataReader reader = dbcmd.ExecuteReader(); // lendo a query
        while (reader.Read()) // o while é percorrido N vezes. Sendo N igual ao numero de linhas afetadas pela query
        {
            int id = reader.GetInt32(0);  // linha atual. Campo [0]
            int pow = reader.GetInt32(1); // linha atual. Campo [1]
            int man = reader.GetInt32(2); // linha atual. Campo [2]
            int lif = reader.GetInt32(3); // linha atual. Campo [3]

            Debug.Log("Id do Personagem: " + id + " | Power: " + pow + " | Mana: " + man + " | Vida: " + lif + "."); // escrevendo as estatisticas atuais. Elas mudam em cada loop
        }
        reader.Close(); // fechando e setando tudo como null. É NECESSARIO FECHAR TUDO. Setar como Null é boa pratica mas não é obrigatorio.
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

    }
     
#endregion
*/
     /*
    void Start ()
    {
        IDbConnection _connection = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase);
        IDbConnection _connection2 = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase2);

        IDbCommand _command = _connection.CreateCommand();
        IDbCommand _command2 = _connection2.CreateCommand();

        _connection.Open();
        _connection2.Open();

        sql = "CREATE TABLE IF NOT EXISTS highscores (name TEXT, score INTEGER)";
        sql2 = "CREATE TABLE IF NOT EXISTS charstats (healthpoints INTEGER, strength INTEGER)";

        _command.CommandText = sql;
        _command2.CommandText = sql2;

        _command.ExecuteNonQuery();
        _command2.ExecuteNonQuery();
    }

    public void InserirHighScore()

    {
        IDbConnection _connection = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase);
        IDbCommand _command = _connection.CreateCommand();

        _connection.Open();

        string sql = "DELETE TABLE highscores";
        //string sql = "INSERT INTO highscores(name, score) VALUES ('Master', 999)";

        _command.CommandText = sql;

        _command.ExecuteNonQuery();

        Debug.Log(sql);

        _connection.Close();
    }

    public void InserirPlayerStats()
    {
        IDbConnection _connection2 = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase);
        IDbCommand _command2 = _connection2.CreateCommand();

        _connection2.Open();

        string sql2 = "INSERT INTO charstats(healthpoints, strength) VALUES (100, 10)";

        _command2.CommandText = sql2;

        _command2.ExecuteNonQuery();

        Debug.Log(sql2);

        _connection2.Close();
    }

    public void Recuperar()

    {
        IDbConnection _connection = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase);
        IDbCommand _command = _connection.CreateCommand();

        _connection.Open();

        string sqlQuery = "SELECT score, name FROM highscores ORDER BY score DESC";

        _command.CommandText = sqlQuery;

        IDataReader reader = _command.ExecuteReader();

        while (reader.Read())

        {
            int value = reader.GetInt32(0);

            string name = reader.GetString(1);

            Lnumeros.Add(value.ToString());
            Ltextos.Add(name);
        }

        texto1.text = "" + (Lnumeros[0]) + "-" + "" + (Ltextos[0]) + "\n" +
                     "" + (Lnumeros[1]) + "-" + "" + (Ltextos[1]) + "\n" +
                     "" + (Lnumeros[2]) + "-" + "" + (Ltextos[2]) + "\n" +
                     "" + (Lnumeros[3]) + "-" + "" + (Ltextos[3]) + "\n" +
                     "" + (Lnumeros[4]) + "-" + "" + (Ltextos[4]) + "\n";
                      
        _connection.Close();

    }

    public void PlayerStats()
    {
        IDbConnection _connection2 = new SqliteConnection("URI=file:" + Application.dataPath + urlDataBase);
        IDbCommand _command2 = _connection2.CreateCommand();

        _connection2.Open();

        string sqlQuery = "SELECT healthpoints, strength FROM charstats";

        _command2.CommandText = sqlQuery;

        IDataReader reader = _command2.ExecuteReader();

        while (reader.Read())

        {
            str = reader.GetInt32(0);

            hp = reader.GetInt32(1);
        }

        texto2.text ="Strength = " + str + "   -   " + "HealthPoints = " + hp;
        _connection2.Close();
    }
}
*/