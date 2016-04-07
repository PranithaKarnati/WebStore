using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsWebStore
{
    public class SaleBase
    {

        #region PROPERTIES

        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }

        #endregion

    }



    public class SaleDB : SaleBase, IDisposable
    {
        #region METHODS (public)


        public int Add(SaleBase mClass)
        {
            int SaleId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "INSERT INTO Sale (CustomerId, Description, TotalPrice, SaleDate)" +
            " VALUES (@CustomerId, @Description, @TotalPrice, @SaleDate)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = mClass.CustomerId;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = mClass.TotalPrice;
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = mClass.SaleDate;
            try
            {
                con.Open();
                SaleId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                SaleId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return SaleId;
        }


        public bool Update(SaleBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "UPDATE Sale SET CustomerId = @CustomerId, Description = @Description, TotalPrice = @TotalPrice, SaleDate = @SaleDate " +
            "WHERE SaleId = @SaleId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = mClass.SaleId;
            cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = mClass.CustomerId;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = mClass.TotalPrice;
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = mClass.SaleDate;
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


        public SaleBase GetById(int SaleId)
        {
            return Get(" WHERE db.SaleId = @SaleId", new SqlParameter("@SaleId", SaleId));
        }


        public List<SaleBase> List()
        {
            string WhereClause = "";
            return List(WhereClause);
        }

        public List<SaleBase> ListByCustomerId(int CustomerId)
        {
            return List(" WHERE db.CustomerId = @CustomerId", new SqlParameter("@CustomerId", CustomerId));
        }

        public bool Delete(int SaleId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM Sale WHERE SaleId = " + SaleId.ToString();
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

        private SaleBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            SaleBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.SaleId, db.CustomerId, db.Description, db.TotalPrice, db.SaleDate, ct.Name AS CustomerName, " +
                "ct.Address AS CustomerAddress, ct.City as CustomerCity, ct.Country AS CustomerCountry " +
               "FROM Sale db " +
               "LEFT OUTER JOIN Customer ct ON ct.CustomerId = db.CustomerId " + WhereClause;
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

        private List<SaleBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<SaleBase> mList = new List<SaleBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.SaleId, db.CustomerId, db.Description, db.TotalPrice, db.SaleDate, ct.Name AS CustomerName, " +
                "ct.Address AS CustomerAddress, ct.City as CustomerCity, ct.Country AS CustomerCountry " +
               "FROM Sale db " +
               "LEFT OUTER JOIN Customer ct ON ct.CustomerId = db.CustomerId " + WhereClause;
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


        private SaleBase LoadRow(SqlDataReader rdr)
        {
            return new SaleBase
            {
                SaleId = rdr["SaleId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["SaleId"]),
                CustomerId = rdr["CustomerId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CustomerId"]),
                Description = rdr["Description"].Equals(DBNull.Value) ? "" : (rdr["Description"].ToString()),
                TotalPrice = rdr["TotalPrice"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(rdr["TotalPrice"]),
                SaleDate = rdr["SaleDate"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["SaleDate"]),
                CustomerName = rdr["CustomerName"].Equals(DBNull.Value) ? "" : (rdr["CustomerName"].ToString()),
                CustomerAddress = rdr["CustomerAddress"].Equals(DBNull.Value) ? "" : (rdr["CustomerAddress"].ToString()),
                CustomerCity = rdr["CustomerCity"].Equals(DBNull.Value) ? "" : (rdr["CustomerCity"].ToString()),
                CustomerCountry = rdr["CustomerCountry"].Equals(DBNull.Value) ? "" : (rdr["CustomerCountry"].ToString()),
            };

        }

        private void Populate(SaleBase mClass)
        {
            this.SaleId = mClass.SaleId;
            this.CustomerId = mClass.CustomerId;
            this.TotalPrice = mClass.TotalPrice;
            this.SaleDate = mClass.SaleDate;
            this.CustomerName = mClass.CustomerName;
            this.CustomerAddress = mClass.CustomerAddress;
            this.CustomerCity = mClass.CustomerCity;
            this.CustomerCountry = mClass.CustomerCountry;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR

        private bool disposed = false;

        public SaleDB()
        {

        }

        public SaleDB(int SaleId)
        {
            Populate(GetById(SaleId));
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
        ~SaleDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

