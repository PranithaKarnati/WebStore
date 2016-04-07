using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appStore.customer
{
    public partial class selectcustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCustomerGrid();
            }
        }

        protected void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int CustomerId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "SelectRow")
            {
                if (grdCustomer.Rows[Index] != null)
                {
                    GridViewRow Row = grdCustomer.Rows[Index];
                    CustomerId = Row.Cells[0].Text.ToInt();
                    Response.Redirect(string.Format("{0}sales/salesorder.aspx?cid={1}", Common.ApplicationPath, CustomerId));
                }
            }
        }

        private void LoadCustomerGrid()
        {
            grdCustomer.DataSource = new clsWebStore.CustomerDB().ListAll();
            grdCustomer.DataBind();
        }

       
    }
}