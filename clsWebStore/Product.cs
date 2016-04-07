using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsWebStore
{
    public class ProductBase
    {

        #region PROPERTIES

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }

        #endregion

    }



    public class ProductDB : ProductBase, IDisposable
    {
        #region METHODS (public)


        public int Add(ProductBase mClass)
        {
            int ProductId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "INSERT INTO Product (Name, Description, Price)" +
            " VALUES (@Name, @Description, @Price)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = mClass.Price;
            try
            {
                con.Open();
                ProductId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ProductId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return ProductId;
        }


        public bool Update(ProductBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price " +
            "WHERE ProductId = @ProductId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = mClass.ProductId;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = mClass.Description;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = mClass.Price;
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


        public ProductBase GetById(int ProductId)
        {
            return Get(" WHERE db.ProductId = @ProductId", new SqlParameter("@ProductId", ProductId));
        }


        public List<ProductBase> List()
        {
            string WhereClause = "";
            return List(WhereClause);


        }

        public List<ProductBase> Search(string SearchText)
        {
            return List(" WHERE db.Name LIKE '%' + @SearchText + '%' ORDER BY db.Name;",
                 new SqlParameter("@SearchText", SearchText));

        }

        public bool Delete(int ProductId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM Product WHERE ProductId = " + ProductId.ToString();
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

        private ProductBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            ProductBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.ProductId, db.Name, db.Description, db.Price, db.Created " +
            "FROM Product db " + WhereClause;
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

        private List<ProductBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<ProductBase> mList = new List<ProductBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.ProductId, db.Name, db.Description, db.Price, db.Created " +
            "FROM Product db " + WhereClause;
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


        private ProductBase LoadRow(SqlDataReader rdr)
        {
            return new ProductBase
            {
                ProductId = rdr["ProductId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["ProductId"]),
                Name = rdr["Name"].Equals(DBNull.Value) ? "" : (rdr["Name"].ToString()),
                Description = rdr["Description"].Equals(DBNull.Value) ? "" : (rdr["Description"].ToString()),
                Price = rdr["Price"].Equals(DBNull.Value) ? 0 : Convert.ToDouble(rdr["Price"]),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(ProductBase mClass)
        {
            this.ProductId = mClass.ProductId;
            this.Name = mClass.Name;
            this.Description = mClass.Description;
            this.Price = mClass.Price;
            this.Created = mClass.Created;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public ProductDB()
        {

        }

        public ProductDB(int ProductId)
        {
            Populate(GetById(ProductId));
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
        ~ProductDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

