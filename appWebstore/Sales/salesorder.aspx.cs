using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dpUtilityLib;

namespace appWebStore.sales
{
    public partial class salesorder : System.Web.UI.Page
    {
        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["sid"])) { hdfSalesOrderId.Value = Request.QueryString["sid"]; }
                if (!string.IsNullOrEmpty(Request.QueryString["cid"])) { hdfCustomerId.Value = Request.QueryString["cid"]; }

                if (hdfSalesOrderId.Value.ToInt() > 0)
                {
                    LoadExistingSale();

                }
                else
                {
                    txtSaleDate.Text = DateTime.Today.ToString("dd MMM yyyy");
                    LoadCustomer();
                    CreateSalesOrder();
                }

                LoadProductGrid();
            }
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
                    ProductId = Row.Cells[0].Text.ToInt();
                    grdProduct.SelectedIndex = Index;
                    hdfProductId.Value = ProductId.ToString();

                    UpdateSalesOrder();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                // return error
                return;
            }

            AddProductToSale();

        }

        protected void grdSalesOrderItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int SaleItemId = 0;
            int Index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "RemoveRow")
            {
                if (grdSalesOrderItem.Rows[Index] != null)
                {
                    GridViewRow Row = grdSalesOrderItem.Rows[Index];
                    SaleItemId = Row.Cells[0].Text.ToInt();

                    RemoveSaleItem(SaleItemId);
                }
            }
        }

        #endregion

        #region METHODS

        private void LoadExistingSale()
        {
            var mSale = new clsWebStore.SaleDB().GetById(hdfSalesOrderId.Value.ToInt());

            hdfCustomerId.Value = mSale.CustomerId.ToString();
            txtCustomer.Text = mSale.CustomerName;
            txtAddress.Text = mSale.CustomerAddress;
            txtCity.Text = mSale.CustomerCity;
            txtCountry.Text = mSale.CustomerCountry;

            txtSaleDate.Text = mSale.SaleDate.ToString("dd MMM yyyy");

            LoadSalesOrderItemGrid();

        }

        private void CreateSalesOrder()
        {
            int SalesOrderId = hdfSalesOrderId.Value.ToInt();

            if (SalesOrderId == 0)
            {
                var mSalesOrder = new clsWebStore.SaleBase();

                mSalesOrder.CustomerId = hdfCustomerId.Value.ToInt();
                mSalesOrder.Description = "";
                mSalesOrder.TotalPrice = 0;
                mSalesOrder.SaleDate = DateTime.Today;
                SalesOrderId = new clsWebStore.SaleDB().Add(mSalesOrder);

                hdfSalesOrderId.Value = SalesOrderId.ToString();

            }

        }

        private void UpdateSalesOrder()
        {
            int SalesOrderId = hdfSalesOrderId.Value.ToInt();

            if (SalesOrderId > 0)
            {
                string Description = "";
                var mSalesOrder = new clsWebStore.SaleDB().GetById(SalesOrderId);

                var SaleItemList = new clsWebStore.SaleItemsDB().ListBySaleId(SalesOrderId);
                foreach (var item in SaleItemList)
                {
                    Description += " " + item.ProductName;
                }

                double SaleTotal = SaleItemList.Sum(x => x.LineTotal);
                double ItemCount = SaleItemList.Sum(x => x.Quantity);

                mSalesOrder.Description = Description;
                mSalesOrder.TotalPrice = SaleTotal;
                bool ok = new clsWebStore.SaleDB().Update(mSalesOrder);
            }

        }

        private void AddProductToSale()
        {
            int SaleId = hdfSalesOrderId.Value.ToInt();
            int ProductId = hdfProductId.Value.ToInt();

            var mSalesItem = new clsWebStore.SaleItemsDB().GetBySaleProduct(SaleId, ProductId);

            if (mSalesItem != null)
            {
                mSalesItem.SaleId = SaleId;
                mSalesItem.ProductId = ProductId;
                mSalesItem.Quantity = txtQuantity.Text.ToInt();
                mSalesItem.ProductPrice = new clsWebStore.ProductDB().GetById(ProductId).Price;
                mSalesItem.LineTotal = (mSalesItem.ProductPrice * mSalesItem.Quantity);

                bool ok = new clsWebStore.SaleItemsDB().Update(mSalesItem);
            }
            else
            {
                mSalesItem = new clsWebStore.SaleItemsBase();
                mSalesItem.SaleId = SaleId;
                mSalesItem.ProductId = ProductId;
                mSalesItem.Quantity = txtQuantity.Text.ToInt();
                mSalesItem.ProductPrice = new clsWebStore.ProductDB().GetById(ProductId).Price;
                mSalesItem.LineTotal = (mSalesItem.ProductPrice * mSalesItem.Quantity);

                int SaleItemId = new clsWebStore.SaleItemsDB().Add(mSalesItem);
            }

            UpdateSalesOrder();

            LoadSalesOrderItemGrid();

        }

        private void RemoveSaleItem(int SaleItemId)
        {
            bool del = new clsWebStore.SaleItemsDB().Delete(SaleItemId);

            LoadSalesOrderItemGrid();

        }

        private void LoadCustomer()
        {
            var mCustomer = new clsWebStore.CustomerDB().GetById(hdfCustomerId.Value.ToInt());
            if (mCustomer != null)
            {
                txtCustomer.Text = mCustomer.Name;
                txtAddress.Text = mCustomer.Address;
                txtCity.Text = mCustomer.City;
                txtCountry.Text = mCustomer.Country;
            }
        }

        private void LoadSalesOrderItemGrid()
        {
            var SalesItemList = new clsWebStore.SaleItemsDB().ListBySaleId(hdfSalesOrderId.Value.ToInt());

            int TotalItems = SalesItemList.Sum(x => x.Quantity);
            double TotalSale = SalesItemList.Sum(x => x.LineTotal);
            int ItemCount = SalesItemList.Select(x => x.LineTotal).Count();

            grdSalesOrderItem.DataSource = SalesItemList;
            grdSalesOrderItem.DataBind();

        }

        private void LoadProductGrid()
        {
            grdProduct.DataSource = new clsWebStore.ProductDB().List();
            grdProduct.DataBind();
        }

        #endregion
    }
}