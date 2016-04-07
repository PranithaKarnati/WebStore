using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace appWebStore.customer
{
    public partial class customerlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCustomerList();
            }

        }

        private void LoadCustomerList()
        {
            var mCustomerList = new clsWebStore.CustomerDB().ListAll();

            var sb = new StringBuilder();

           

            sb.AppendLine("<div class=\"space16\"></div>");
            sb.AppendLine("<div style=\"width: 840px; margin: 0px auto;\">");
            sb.AppendLine("<table style=\"width: 100%; border-collapse:collapse;\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td class=\"tablehead\">NAME</td>");
            sb.AppendLine("<td class=\"tablehead\">ADDRESS</td>");
            sb.AppendLine("<td class=\"tablehead\">CITY</td>");
            sb.AppendLine("<td class=\"tablehead\">COUNTRY</td>");
            sb.AppendLine("</tr>");

            foreach (var mCustomer in mCustomerList)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td class=\"tablerow\">{0}</td>", mCustomer.Name);
                sb.AppendFormat("<td class=\"tablerow\">{0}</td>", mCustomer.Address);
                sb.AppendFormat("<td class=\"tablerow\">{0}</td>", mCustomer.City);
                sb.AppendFormat("<td class=\"tablerow\">{0}</td>", mCustomer.Country);
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</div>");

            divCustomerList.InnerHtml = sb.ToString();
            sb = null;

        }
    }
}