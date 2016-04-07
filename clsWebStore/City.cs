using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace clsWebStore
{
    public class CityBase
    {

        #region PROPERTIES

        public int CityId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        #endregion

    }



    public class CityDB : CityBase, IDisposable
    {
        #region METHODS (public)


        public int Add(CityBase mClass)
        {
            int CityId = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "INSERT INTO City (Name)" +
            " VALUES (@Name)" +
            " SELECT SCOPE_IDENTITY()";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
            try
            {
                con.Open();
                CityId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                CityId = -1;
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
            return CityId;
        }


        public bool Update(CityBase mClass)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "UPDATE City SET Name = @Name " +
            "WHERE CityId = @CityId;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = mClass.CityId;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = mClass.Name;
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


        public CityBase GetById(int CityId)
        {
            return Get(" WHERE db.CityId = @CityId", new SqlParameter("@CityId", CityId));
        }


        public List<CityBase> List()
        {
            string WhereClause = "";
            return List(WhereClause);
        }


        public List<CityBase> Search(string SearchTerm)
        {
            string WhereClause = string.Format("INNER JOIN CONTAINSTABLE(Product, (Name, Description), '{0}') AS t ON db.ProductId = t.[Key] ORDER BY t.[Rank]", SearchTerm);
            return List(WhereClause);
        }



        public bool Delete(int CityId)
        {
            bool mResult = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "DELETE FROM City WHERE CityId = " + CityId.ToString();
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

        private CityBase Get(string WhereClause, params SqlParameter[] commandparameters)
        {
            CityBase mClass = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.CityId, db.Name, db.Created " +
            "FROM City db " + WhereClause;
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

        private List<CityBase> List(string WhereClause, params SqlParameter[] commandparameters)
        {
            List<CityBase> mList = new List<CityBase>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["csStore"].ConnectionString);
            string sql = "SELECT db.CityId, db.Name, db.Created " +
            "FROM City db " + WhereClause;
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


        private CityBase LoadRow(SqlDataReader rdr)
        {
            return new CityBase
            {
                CityId = rdr["CityId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["CityId"]),
                Name = rdr["Name"].Equals(DBNull.Value) ? "" : (rdr["Name"].ToString()),
                Created = rdr["Created"].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(rdr["Created"]),
            };

        }

        private void Populate(CityBase mClass)
        {
            this.CityId = mClass.CityId;
            this.Name = mClass.Name;
            this.Created = mClass.Created;
        }


        #endregion

        #region CONSTRUCTOR/DESTRUCTOR


        private bool disposed = false;

        public CityDB()
        {

        }

        public CityDB(int CityId)
        {
            Populate(GetById(CityId));
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
        ~CityDB()
        {
            Dispose(false);
        }
        #endregion


    }
}

