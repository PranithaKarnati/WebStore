using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace appStore.product
{
    public partial class manageproducts : System.Web.UI.Page
    {

        #region MEMBERS
        
        private int mProductId;
        private clsWebStore.ProductBase mProductBase;

        #endregion

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadProductGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) { return; }

            mProductId = 0;
            if (!string.IsNullOrEmpty(hdfProductId.Value))
            {
                mProductId = Convert.ToInt32(hdfProductId.Value);
            }

            UnloadForm(mProductId);

            if (mProductId > 0)
            {
                bool ok = new clsWebStore.ProductDB().Update(mProductBase);
                if (!ok) mProductId = -1;
            }
            else
            {
                mProductId = new clsWebStore.ProductDB().Add(mProductBase);
                hdfProductId.Value = mProductId.ToString();
            }  

            if (mProductId < 0)
            {
                InputFailed("The database update failed, please try again.");
            }
            else
            {
                LoadProductGrid();
                InputSucceeded();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
      
        protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ProductId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "SelectRow")
            {
                if (grdProduct.Rows[Index] != null)
                {
                    GridViewRow Row = grdProduct.Rows[Index];
                    ProductId = Convert.ToInt32(Row.Cells[0].Text);
                    LoadForm(ProductId);
                }
            }

            if (e.CommandName == "RemoveRow")
            {
                if (grdProduct.Rows[Index] != null)
                {
                    GridViewRow Row = grdProduct.Rows[Index];
                    ProductId = Convert.ToInt32(Row.Cells[0].Text);
                    DeleteProduct(ProductId);
                }
            }
        }

        #endregion

        #region METHODS

        private void LoadForm(int ProductId)
        {
            mProductBase = new clsWebStore.ProductDB().GetById(ProductId);

            hdfProductId.Value = mProductBase.ProductId.ToString();
            txtName.Text = mProductBase.Name;
            txtDescription.Text = mProductBase.Description;
            txtPrice.Text = mProductBase.Price.ToString("N");

        }

        private void UnloadForm(int mProductId)
        {
            mProductBase = new clsWebStore.ProductBase();

            mProductBase.ProductId = mProductId;
            mProductBase.Name = txtName.Text;
            mProductBase.Description = txtDescription.Text;
            mProductBase.Price = Convert.ToDouble(txtPrice.Text);

        }

        private void ResetForm()
        {

            mProductBase = null;
            mProductId = 0;
            hdfProductId.Value = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
        }

        private bool ValidateInput()
        {
            bool r = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                return false;

            }

            return r;

        }

        private void LoadProductGrid()
        {
            grdProduct.DataSource = new clsWebStore.ProductDB().List();
            grdProduct.DataBind();

        }

        private void DeleteProduct(int ProductId)
        {

            bool del = new clsWebStore.ProductDB().Delete(ProductId);

            LoadProductGrid();

        }

        private void InputSucceeded()
        {
            // Return success message
        }

        private void InputFailed(string Msg)
        {
            // return error message
        }

        #endregion

    }
}