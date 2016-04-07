using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsWebStore
{
    public class SaleItemsBase
    {

        #region PROPERTIES

        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public double LineTotal { get; set; }
        public DateTime Created { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        #endregion

    }

    public class SaleItemsDB : SaleItemsBase, IDisposable
    {
        #region METHODS (public)

        public int Add(SaleItemsBase mClass)
        {
            int SaleItemId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "INSERT INTO SaleItems (SaleId, ProductId, Quantity, ProductPrice, LineTotal)" +
            " VALUES (@SaleId, @ProductId, @Quantity, @ProductPrice, @LineTotal)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = mClass.SaleId;
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = mClass.ProductId;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = mClass.Quantity;
            cmd.Parameters.Add("@ProductPrice", SqlDbType.Float).Value = mClass.ProductPrice;
            cmd.Parameters.Add("@LineTotal", SqlDbType.Float).Value = mClass.LineTotal;
            try
            {
                con.Open();
                SaleItemId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                SaleItemId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return SaleItemId;
        }

        public bool Update(SaleItemsBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "UPDATE SaleItems SET SaleId = @SaleId, ProductId = @ProductId, Quantity = @Quantity, ProductPrice = @ProductPrice, LineTotal = @LineTotal " +
            "WHERE SaleItemId = @SaleItemId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@SaleItemId", SqlDbType.Int).Value = mClass.SaleItemId;
            cmd.Parameters.Add("@SaleId", SqlDbType.Int).Value = mClass.SaleId;
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = mClass.ProductId;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = mClass.Quantity;
            cmd.Parameters.Add("@ProductPrice", SqlDbType.Float).Value = mClass.ProductPrice;
            cmd.Parameters.Add("@LineTotal", SqlDbType.Float).Value = mClass.LineTotal;
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

        public SaleItemsBase GetById(int SaleItemId)
        {
            return Get(" WHERE db.SaleItemId = @SaleItemId", new SqlParameter("@SaleItemId", SaleItemId));
        }

        public SaleItemsBase GetBySaleProduct(int SaleId, int ProductId)
        {
            return Get(" WHERE db.SaleId = @SaleId AND db.ProductId = @ProductId",
                new SqlParameter("@SaleId", SaleId),
                new SqlParameter("@ProductId", ProductId));
        }

        public List<SaleItemsBase> List()
        {
            string WhereClause = "";
            return List(WhereClause);
        }

        public List<SaleItemsBase> ListBySaleId(int SaleId)
        {
            return List(" WHERE db.SaleId = @SaleId", 
                new SqlParameter("@SaleId", SaleId));
        }

        public List<SaleItemsBase> ListByProductId(int ProductId)
        {
            return List(" WHERE db.ProductId = @ProductId", 
                new SqlParameter("@ProductId", ProductId));
        }

        public bool Delete(int SaleItemId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM SaleItems WHERE SaleItemId = " + SaleItemId.ToString();
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

        public bool DeleteBySaleId(int SaleId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM SaleItems WHERE SaleId = " + SaleId.ToString();
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

        private SaleItemsBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            SaleItemsBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.SaleItemId, db.SaleId, db.ProductId, db.Quantity, db.ProductPrice, db.LineTotal, db.Created, " + 
                "pd.Name AS ProductName, pd.Description AS ProductDescription, pd.Price AS ProductPrice " +
                "FROM SaleItems db " +
                "LEFT OUTER JOIN Product pd ON pd.ProductId = db.ProductId " + WhereClause;
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

        private List<SaleItemsBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<SaleItemsBase> mList = new List<SaleItemsBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.SaleItemId, db.SaleId, db.ProductId, db.Quantity, db.ProductPrice, db.LineTotal, db.Created, " + 
               "pd.Name AS ProductName, pd.Description AS ProductDescription, pd.Price AS ProductPrice " +
               "FROM SaleItems db " +
               "LEFT OUTER JOIN Product pd ON pd.ProductId = db.ProductId " + WhereClause;
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

        private SaleItemsBase LoadRow(SqlDataReader rdr)
        {
            return new SaleItemsBase
            {
                SaleItemId = rdr["SaleItemId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["SaleItemId"]),
                SaleId = rdr["SaleId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["SaleId"]),
                ProductId = rdr["ProductId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["ProductId"]),
                Quantity = rdr["Quantity"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["Quantity"]),
                ProductPrice = rdr["ProductPrice"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(rdr["ProductPrice"]),
                LineTotal = rdr["LineTotal"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(rdr["LineTotal"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
                ProductName = rdr["ProductName"].Equals(DBNull.Value) ? "" : (rdr["ProductName"].ToString()),
                ProductDescription = rdr["ProductDescription"].Equals(DBNull.Value) ? "" : (rdr["ProductDescription"].ToString()),
            };

        }

        private void Populate(SaleItemsBase mClass)
        {
            this.SaleItemId = mClass.SaleItemId;
            this.SaleId = mClass.SaleId;
            this.ProductId = mClass.ProductId;
            this.Quantity = mClass.Quantity;
            this.ProductPrice = mClass.ProductPrice;
            this.LineTotal = mClass.LineTotal;
            this.Created = mClass.Created;

            this.ProductName = mClass.ProductName;
            this.ProductDescription = mClass.ProductDescription;
        }

        #endregion

        #region CONSTRUCTOR/DESTRUCTOR

        private bool disposed = false;

        public SaleItemsDB()
        {

        }

        public SaleItemsDB(int SaleItemId)
        {
            Populate(GetById(SaleItemId));
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
        ~SaleItemsDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

