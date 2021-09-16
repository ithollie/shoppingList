using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace complete
{
    public  class Database
    {
        private string dataBaseName;
        private string passwd;
        private string root;
        private MySqlConnection conn;
        public Database()
        {
            this.dataBaseName = "shopping";
            this.passwd = "hawaibrahB1a1";
            this.root   = "root";
            this.connection();
        }
        public void connection()
        {
            this.conn = new MySqlConnection();
            this.conn.ConnectionString = "Server=localhost;Database='"+this.dataBaseName+"';Uid='"+this.root+"';Pwd='"+passwd+"';";
           
        }
        public void displayItems()
        {
           
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * from shopmanager";

                cmd.Connection = con; con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == false)
                {
                    Console.WriteLine("Data  is  Empty");
                }

                while (reader.Read())
                {
                    Console.WriteLine("[" +  "id => {0}, productname => {1} , category = > {2} , priority => {3} , purchased => {4} , DateInserted => {5}", reader[0], reader[1], reader[2], reader[3], reader[5], reader[4] +  " ] " );
                   
                    Console.WriteLine("");
                }
                
            }
        }
        public void editName(int id,  string name)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                Console.WriteLine("Hey  I got  activated edit");

                con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";

                string sql = "UPDATE shopmanager SET productName = '"+name+"'  WHERE  id ="+id+"";
            
                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void editCategory(int id, string val)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
              
                con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";

                string sql = "UPDATE shopmanager SET category = '"+val+"'  WHERE  id ="+id+"";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void editPrior(int id, string  prior)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                Console.WriteLine("Hey  I got  activated remove");

                con.ConnectionString = "Server=localhost;Database=shopping;Uid=root;Pwd=hawaibrahB1a1;";

                string sql = "UPDATE shopmanager SET priority = '"+prior+"'  WHERE  id ="+id+"";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public int  getCount()
        {

            int temp = 0;

            string sql = "SELECT *  FROM shopmanager ";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                temp++;
            }
            this.conn.Close();
            return temp;
           

        }
        public int  findById(int id)
        {
            int temp = 0;

            string sql = "SELECT id  FROM shopmanager WHERE id ='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();
     
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (Convert.ToInt32(reader[0].ToString()) == id) {

                    temp =  Convert.ToInt32(reader[0].ToString());
                }
                
            }
            this.conn.Close();

            return temp;
          
        }
        public int searchById(int id)
        {
            int temp = 0;

            string sql = "SELECT id FROM shopmanager WHERE id ='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (Convert.ToInt32(reader[0].ToString()) == id)
                {
                    temp = Convert.ToInt32(reader[0].ToString());
                }

            }
            this.conn.Close();

            return temp;
        }
        public string findByName(string name)
        {
           
                string temp = "";

                string sql = "SELECT productName FROM shopmanager WHERE productName ='"+name+"'";

                MySqlCommand cmd = new MySqlCommand(sql, this.conn);

                cmd.Connection = this.conn;

                this.conn.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (reader[0].ToString() == name)
                    {
                        temp = reader[0].ToString();
                    }

                }
                this.conn.Close();
            
                return temp;

        }
        public void RemoveData(int id)
        {
 
       
            string sql = "DELETE  FROM shopmanager WHERE id ="+id+"";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);
               
            cmd.Connection = this.conn;

            this.conn.Open();

            cmd.ExecuteNonQuery();
      
        }
        public bool togglePurchased()
        {
            string state = "false";

            string sqlPurchased = "UPDATE  shopmanager  SET  purchased ='"+state+"' WHERE purchased ='"+true+"'";

            MySqlCommand cmd = new MySqlCommand(sqlPurchased, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            cmd.ExecuteNonQuery();

            this.conn.Close();

            return true;

        }
        public void storeData(int  id  ,  string productName , string category,string priority, string dateAdded, string purchased)
        {
               

                string  sql = "INSERT INTO shopmanager (id, productName, category,  priority, dateAdded, purchased) VALUES (@id, @productName,@category, @priority, @dateAdded, @purchased)";
                
                MySqlCommand cmd = new MySqlCommand(sql,this.conn);
                
                cmd.Parameters.AddWithValue("@id", id);
                
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@category", category);

                cmd.Parameters.AddWithValue("@priority", priority);
                cmd.Parameters.AddWithValue("@dateAdded", dateAdded);
                cmd.Parameters.AddWithValue("@purchased", purchased);

                cmd.Connection =  this.conn;
                this.conn.Open();
                cmd.ExecuteNonQuery();
                this.conn.Close();
               
                
        }
        public void sortByNameDesc()
        {
            

            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY productName DESC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();

           
        }
        public void sortByNameAsc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY productName ASC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();

        }
        public void sortByCategoryDesc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY category DESC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void sortByCategoryAsc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY category ASC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void update(int id, MySqlDataReader reader)
        {
            reader.Close();
            string state = "true";

            string sqlPurchased = "UPDATE  shopmanager  SET  purchased ='"+state+"' WHERE id ='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sqlPurchased, this.conn);

            cmd.Connection = this.conn;

            //this.conn.Open();

            cmd.ExecuteNonQuery();

            this.conn.Close();
        }
        public void sortByPriorityAsc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY priority ASC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void sortByPriorityDesc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER priority BY  DESC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public bool markAsPurchased(int id)
        {
            bool temp = false;
            
            string sql = "SELECT id FROM shopmanager WHERE id ='"+id+"'";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);
            
            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                if (Convert.ToInt32(reader[0].ToString()) == id)
                {
                    int r = Convert.ToInt32(reader[0].ToString());

                    update(Convert.ToInt32(reader[0].ToString()), reader);

                    temp = true;
                }

            }
            this.conn.Close();
            

            return temp;
        }
        public void sortByDateAddedAsc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY dateAdded ASC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void sortByDateAddedDesc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY dateAdded DESC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void sortByPurchasedDateAsc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY dateAdded ASC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
        public void sortByPurchasedDateDesc()
        {
            string sql = "SELECT id, productName, category, priority, dateAdded,  purchased   FROM shopmanager ORDER  BY productName DESC";

            MySqlCommand cmd = new MySqlCommand(sql, this.conn);

            cmd.Connection = this.conn;

            this.conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("id" + reader[0]);
                Console.WriteLine("productName " + reader[1]);
                Console.WriteLine("category " + reader[2]);
                Console.WriteLine("prioprity " + reader[3]);
                Console.WriteLine("dateAdded " + reader[4]);
                Console.WriteLine("purchased " + reader[4]);
            }
            this.conn.Close();
        }
    }
}
