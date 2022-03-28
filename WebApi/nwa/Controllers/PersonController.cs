﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using nwa.Models;
using System.Data;
using System.Data.SqlClient;

namespace nwa.Controllers
{
    public class PersonController : ApiController
    {
        static string connectionString = @"Data Source=DESKTOP-V8JBKRE;Initial Catalog=SQLTest;Integrated Security=True";



        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPage()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Person> people = new List<Person>();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Person;", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var person = new Person();

                        person.Id = reader.GetInt32(0);
                        person.FirstName = reader.GetString(1);
                        person.LastName = reader.GetString(2);
                        person.Age = reader.GetInt32(3);
                        person.Gender = reader.GetString(4);

                        people.Add(person);
                    }

                    connection.Close();
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, people);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"EMPTY!");
                }

            }



        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostColumn(Person people)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (connection)
            {
                connection.Open();
                string NewColumn = $"INSERT INTO Person(Id, FirstName, LastName, Age, Gender) VALUES" +
                    $"('{people.Id}'," +
                    $"'{people.FirstName}'," +
                    $"'{people.LastName}'," +
                    $"'{people.Age}'," +
                    $"'{people.Gender}')";

                adapter.InsertCommand = new SqlCommand(NewColumn, connection);
                adapter.InsertCommand.ExecuteNonQuery();

                connection.Close();
                return Request.CreateResponse(HttpStatusCode.OK, people);

            }

        }

    }
}

