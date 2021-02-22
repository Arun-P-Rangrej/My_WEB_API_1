using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace My_WEB_API_1.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(@"server=.; database=TestEmployee; Integrated Security=true;");

        // GET api/values
        public String Get()
        {
            //return new string[] { "value1", "value2" };
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count >0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found";
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return new string[] { "value1", "value2" };
            SqlDataAdapter da = new SqlDataAdapter("Select * from Employee where ID  = '"+id+"'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "No data found";
            }
        }

       

        // POST api/values
        public string Post([FromBody]string value)
        {
            SqlCommand cmd = new SqlCommand("Insert into Employee values('" + value + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i==1)
            {
                return "Record Interted with the values as " + value;
            }
            else
            {
                return "Try again. No data inserted";
            }
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Employee SET Name = '" + value + "' WHERE ID = '" + id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "Record updated with the values as " + value + " where id is " + id;
            }
            else
            {
                return "Try again. No data updated";
            }
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE from Employee WHERE ID = '" + id + "'", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "DELETED record where id is " + id;
            }
            else
            {
                return "Try again. No data inserted";
            }
        }
    }
}
