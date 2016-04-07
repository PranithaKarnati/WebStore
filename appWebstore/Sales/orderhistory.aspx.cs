using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appWebStore.sales
{
    public partial class orderhistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadSalesOrders();
            }
        }

        protected void grdSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int SaleId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "SelectRow")
            {
                if (grdSales.Rows[Index] != null)
                {
                    GridViewRow Row = grdSales.Rows[Index];
                    SaleId = Row.Cells[0].Text.ToInt();
                    Response.Redirect(string.Format("{0}sales/salesorder.aspx?sid={1}", Common.ApplicationPath, SaleId));
                }
            }


            if (e.CommandName == "RemoveRow")
            {
                if (grdSales.Rows[Index] != null)
                {
                    GridViewRow Row = grdSales.Rows[Index];
                    SaleId = Row.Cells[0].Text.ToInt();
                    DeleteSale(SaleId);
                }
            }
        }


        private void LoadSalesOrders()
        {
            grdSales.DataSource = new clsWebStore.SaleDB().List();
            grdSales.DataBind();

        }

        private void DeleteSale(int SaleId)
        {

            bool item = new clsWebStore.SaleItemsDB().DeleteBySaleId(SaleId);
            bool del = new clsWebStore.SaleDB().Delete(SaleId);

            LoadSalesOrders();

        }
       
    }
}