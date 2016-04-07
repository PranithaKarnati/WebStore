using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsWebStore
{
    public class CustomerBase
    {

        #region PROPERTIES

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }

        #endregion

    }



    public class CustomerDB : CustomerBase, IDisposable
    {
        #region METHODS (public)

        public int Add(CustomerBase mClass)
        {
            int CustomerId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "INSERT INTO Customer (Name, Address, CityId, City, Country)" +
            " VALUES (@Name, @Address, @CityId, @City, @Country)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = mClass.Address;
            cmd.Parameters.Add("@CityId", SqlDbType.VarChar).Value = mClass.CityId;
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = mClass.City;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = mClass.Country;
            try
            {
                con.Open();
                CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                CustomerId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return CustomerId;
        }


        public bool Update(CustomerBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "UPDATE Customer SET Name = @Name, Address = @Address, CityId = @CityId, City = @City, Country = @Country " +
            "WHERE CustomerId = @CustomerId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = mClass.CustomerId;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = mClass.Address;
            cmd.Parameters.Add("@CityId", SqlDbType.VarChar).Value = mClass.CityId;
            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = mClass.City;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = mClass.Country;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                mResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mResult;

        }

        public CustomerBase GetById(int CustomerId)
        {
            
            return Get(" WHERE db.CustomerId = @CustomerId",
                new SqlParameter("@CustomerId", CustomerId));
        }

        public List<CustomerBase> ListAll()
        {
            string WhereClause = " ORDER BY db.Name";
            return List(WhereClause);
        }

        public List<CustomerBase> ListByCityId(int CityId)
        {
            return List(" WHERE db.CityId = @CityId ORDER BY db.Name",
                new SqlParameter("@CityId", CityId));
        }

        public bool Delete(int CustomerId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM Customer WHERE CustomerId = " + CustomerId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                mResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mResult;

        }


        #endregion

        #region METHODS (private)

        private CustomerBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            CustomerBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.CustomerId, db.Name, db.Address, db.CityId, db.City, db.Country, db.Created " +
            "FROM Customer db " + WhereClause;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rdr = null;
            foreach (var parameter in commandparameters)
            {
                cmd.Parameters.Add(parameter);
            }
            try
            {
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    mClass = LoadRow(rdr);
                }
            }
            catch (Exception ex)
            {
                mClass = null;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mClass;

        }

        private List<CustomerBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<CustomerBase> mList = new List<CustomerBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.CustomerId, db.Name, db.Address, db.CityId, db.City, db.Country, db.Created " +
            "FROM Customer db " + WhereClause;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rdr = null;
            foreach (var parameter in commandparameters)
            {
                cmd.Parameters.Add(parameter);
            }
            try
            {
                con.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    mList.Add(LoadRow(rdr));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return mList;

        }

        private CustomerBase LoadRow(SqlDataReader rdr)
        {
            return new CustomerBase
            {
                CustomerId = rdr["CustomerId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CustomerId"]),
                Name = rdr["Name"].Equals(DBNull.Value) ? "" : (rdr["Name"].ToString()),
                Address = rdr["Address"].Equals(DBNull.Value) ? "" : (rdr["Address"].ToString()),
                CityId = rdr["CityId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CityId"]),
                City = rdr["City"].Equals(DBNull.Value) ? "" : (rdr["City"].ToString()),
                Country = rdr["Country"].Equals(DBNull.Value) ? "" : (rdr["Country"].ToString()),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(CustomerBase mClass)
        {
            this.CustomerId = mClass.CustomerId;
            this.Name = mClass.Name;
            this.Address = mClass.Address;
            this.CityId = mClass.CityId;
            this.City = mClass.City;
            this.Country = mClass.Country;
            this.Created = mClass.Created;
        }

        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public CustomerDB()
        {

        }

        public CustomerDB(int CustomerId)
        {
            Populate(GetById(CustomerId));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
            }
            disposed = true;
        }
        ~CustomerDB()
        {
            Dispose(false);
        }
        #endregion

    }
}

