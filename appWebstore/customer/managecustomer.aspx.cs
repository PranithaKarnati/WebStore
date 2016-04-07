using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace appWebstore.customer
{
    public partial class managecustomer : System.Web.UI.Page
    {
        #region MEMBERS

        private clsWebStore.CustomerBase mCustomerBase;
        #endregion
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCityList();
                LoadFilterCityList();
                LoadGrid();
            }
        }
        protected void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int CustomerId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (grdCustomer.Rows[Index] != null)
            {
                GridViewRow Row = grdCustomer.Rows[Index];
                grdCustomer.SelectedIndex = Index;
                CustomerId = Convert.ToInt32(Row.Cells[0].Text);
            }

            if (CustomerId == 0) { return; }

            switch (e.CommandName)
            {
                case "SelectRow":
                    LoadForm(CustomerId);
                    break;
                case "RemoveRow":
                    DeleteCustomer(CustomerId);
                    break;
                default:
                    break;
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int mCustomerId = 0;
            if (!string.IsNullOrEmpty(hdfCustomerId.Value))
            {
                mCustomerId = Convert.ToInt32(hdfCustomerId.Value);
            }

            UnloadForm(mCustomerId);

            if (mCustomerId > 0)
            {
                bool ok = new clsWebStore.CustomerDB().Update(mCustomerBase);
                if (!ok) mCustomerId = -1;
            }
            else
            {
                mCustomerId = new clsWebStore.CustomerDB().Add(mCustomerBase);
            }

            if (mCustomerId < 0)
            {

            }
            else
            {
                ResetForm();
                LoadGrid();
            }

        }

        private void LoadFilterCityList()
        {
            cboFilter.Items.Clear();
            cboFilter.Items.Add(new ListItem("Show All", "0"));
            var mCityList = new clsWebStore.CityDB().List().OrderBy(x => x.Name);

            foreach (var mCityId in mCityList.Select(x => x.CityId).Distinct()) 
            {
                var mCity = new clsWebStore.CityDB().GetById(mCityId);
                if(mCity != null){
                    cboFilter.Items.Add(new ListItem(mCity.Name, mCityId.ToString()));

                }
            }
        }

        #endregion

        #region METHODS
        private void LoadForm(int CustomerId)
        {
            mCustomerBase = new clsWebStore.CustomerDB().GetById(CustomerId);

            hdfCustomerId.Value = CustomerId.ToString();
            txtName.Text = mCustomerBase.Name;
            txtAddress.Text = mCustomerBase.Address;

            cboCity.SelectedValue = mCustomerBase.CityId.ToString();

            txtCountry.Text = mCustomerBase.Country;
        }



        
        private void ResetForm()
        {

            mCustomerBase = null;
            hdfCustomerId.Value = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtCountry.Text = "";

        }



        private void LoadGrid()
        {
            int mCityId = Convert.ToInt32(cboFilter.SelectedValue);
            if (mCityId > 0)
            {
                grdCustomer.DataSource = new clsWebStore.CustomerDB().ListByCityId(mCityId);
                grdCustomer.DataBind();
            }
            else {

                grdCustomer.DataSource = new clsWebStore.CustomerDB().ListAll();
                grdCustomer.DataBind();
            }
            String mCityName = cboFilter.SelectedItem.Text;
            divTitle.InnerHtml = string.Format("{0} CUSTOMERS", mCityName.ToUpper());
        }

        private void UnloadForm(int mCustomerId)
        {
            mCustomerBase = new clsWebStore.CustomerBase();

            mCustomerBase.CustomerId = mCustomerId;
            mCustomerBase.Name = txtName.Text;
            mCustomerBase.Address = txtAddress.Text;

            mCustomerBase.CityId = Convert.ToInt32(cboCity.SelectedValue);
            mCustomerBase.City = cboCity.SelectedItem.Text;
            mCustomerBase.Country = txtCountry.Text;
            
        }

        private void LoadCityList()
        {
            cboCity.DataSource = new clsWebStore.CityDB().List();
            cboCity.DataTextField = "Name";
            cboCity.DataValueField = "CityId";
            cboCity.DataBind();
        }
        private void DeleteCustomer(int CustomeriD)
        {

        }

        protected void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }
        #endregion

      
    }


}
